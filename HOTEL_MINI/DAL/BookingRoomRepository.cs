using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response; // BookingDisplay, UsedServiceDto nếu cần
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    /// <summary>
    /// LINES:
    /// - Quản lý BookingRooms (display, overlap, CRUD line, checkin/checkout line).
    /// - KHÔNG xử lý enum/payment; KHÔNG ghi vào Bookings (trừ khi cần đọc join).
    /// - Dịch vụ (BookingRoomServices) để ở BookingRoomServiceRepository.
    /// </summary>
    public class BookingRoomRepository
    {
        private readonly string _cs;

        public BookingRoomRepository()
        {
            _cs = ConfigHelper.GetConnectionString();
        }

        // ---------------- Helpers ----------------
        private static T SafeGet<T>(SqlDataReader rd, int ordinal)
        {
            return rd.IsDBNull(ordinal) ? default(T) : (T)rd.GetValue(ordinal);
        }

        private static List<T> QueryList<T>(string cs, string sql, Action<SqlCommand> onParam, Func<SqlDataReader, T> map)
        {
            var list = new List<T>();
            using (var conn = new SqlConnection(cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                onParam?.Invoke(cmd);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        list.Add(map(rd));
                }
            }
            return list;
        }

        private static T QuerySingle<T>(string cs, string sql, Action<SqlCommand> onParam, Func<SqlDataReader, T> map) where T : class
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                onParam?.Invoke(cmd);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return map(rd);
                }
            }
        }

        private static Booking MapBooking(SqlDataReader rd)
        {
            return new Booking
            {
                BookingID = rd.GetInt32(0),
                CustomerID = rd.GetInt32(1),
                RoomID = rd.GetInt32(2),
                PricingID = rd.GetInt32(3),
                CreatedBy = rd.GetInt32(4),
                BookingDate = rd.GetDateTime(5),
                CheckInDate = SafeGet<DateTime?>(rd, 6),
                CheckOutDate = SafeGet<DateTime?>(rd, 7),
                Status = rd.GetString(8),
                Notes = SafeGet<string>(rd, 9)
            };
        }

        private static BookingDisplay MapBookingDisplay(SqlDataReader rd)
        {
            return new BookingDisplay
            {
                BookingID = rd.GetInt32(0),
                RoomNumber = rd.GetString(1),
                EmployeeName = rd.GetString(2),
                BookingDate = rd.GetDateTime(3),
                CheckInDate = SafeGet<DateTime?>(rd, 4),
                CheckOutDate = SafeGet<DateTime?>(rd, 5),
                Notes = SafeGet<string>(rd, 6),
                Status = rd.GetString(7),
                CustomerIDNumber = SafeGet<string>(rd, 8)
            };
        }

        // ---------------- Utilities ----------------
        public bool IsRoomOverlapped(int roomId, DateTime checkIn, DateTime checkOut)
        {
            const string sql = @"
SELECT  COUNT(1)
FROM    BookingRooms br WITH (NOLOCK)
WHERE   br.RoomID = @RoomID
    AND br.Status IN ('Booked','CheckedIn')
    AND br.CheckInDate < @To
    AND @From < br.CheckOutDate";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                cmd.Parameters.AddWithValue("@From", checkIn);
                cmd.Parameters.AddWithValue("@To", checkOut);
                conn.Open();
                var n = Convert.ToInt32(cmd.ExecuteScalar());
                return n > 0;
            }
        }

        // ---------------- DISPLAY queries ----------------
        public List<BookingDisplay> GetActiveBookingDisplays()
        {
            const string sql = @"
SELECT
    br.BookingRoomID           AS BookingID,
    r.RoomNumber,
    u.FullName                 AS EmployeeName,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    ISNULL(br.Note, b.Notes)   AS Notes,
    br.Status,
    c.IDNumber                 AS CustomerIDNumber
FROM BookingRooms br
JOIN Bookings  b ON b.BookingID  = br.BookingID
JOIN Rooms     r ON r.RoomID     = br.RoomID
JOIN Users     u ON u.UserID     = b.CreatedBy
JOIN Customers c ON c.CustomerID = b.CustomerID
WHERE br.Status IN ('Booked','CheckedIn')
ORDER BY b.BookingDate DESC, br.CheckInDate DESC;";
            return QueryList(_cs, sql, null, MapBookingDisplay);
        }

        public List<BookingDisplay> GetTop20LatestBookingDisplays()
        {
            const string sql = @"
SELECT TOP 20
    br.BookingRoomID           AS BookingID,
    r.RoomNumber,
    u.FullName                 AS EmployeeName,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    ISNULL(br.Note, b.Notes)   AS Notes,
    br.Status,
    c.IDNumber                 AS CustomerIDNumber
FROM BookingRooms br
JOIN Bookings     b  ON b.BookingID  = br.BookingID
JOIN Rooms        r  ON r.RoomID     = br.RoomID
JOIN Users        u  ON u.UserID     = b.CreatedBy
JOIN Customers    c  ON c.CustomerID = b.CustomerID
WHERE br.Status = 'CheckedOut'
ORDER BY br.CheckOutDate DESC, b.BookingDate DESC;";
            return QueryList(_cs, sql, null, MapBookingDisplay);
        }

        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string idNumber)
        {
            const string sql = @"
SELECT
    br.BookingRoomID           AS BookingID,
    r.RoomNumber,
    u.FullName                 AS EmployeeName,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    ISNULL(br.Note, b.Notes)   AS Notes,
    br.Status,
    c.IDNumber                 AS CustomerIDNumber
FROM Customers c
JOIN Bookings b      ON b.CustomerID  = c.CustomerID
JOIN BookingRooms br ON br.BookingID  = b.BookingID
JOIN Rooms r         ON r.RoomID      = br.RoomID
JOIN Users u         ON u.UserID      = b.CreatedBy
WHERE c.IDNumber = @N
ORDER BY b.BookingDate DESC, br.CheckInDate DESC;";
            return QueryList(_cs, sql, cmd => cmd.Parameters.AddWithValue("@N", idNumber), MapBookingDisplay);
        }

        // ---------------- Booking entity queries ----------------
        public List<Booking> GetBookingsByCustomerNumber(string numberID)
        {
            const string sql = @"
SELECT
    br.BookingRoomID           AS BookingID,
    b.CustomerID,
    br.RoomID,
    br.PricingID,
    b.CreatedBy,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    br.Status,
    ISNULL(br.Note, b.Notes)   AS Notes
FROM Customers     c
JOIN Bookings      b   ON b.CustomerID  = c.CustomerID
JOIN BookingRooms  br  ON br.BookingID  = b.BookingID
WHERE c.IDNumber = @NumberID
ORDER BY b.BookingDate DESC, br.CheckInDate DESC;";
            return QueryList(_cs, sql, cmd => cmd.Parameters.AddWithValue("@NumberID", numberID), MapBooking);
        }

        // alias để giữ tương thích cũ
        public List<Booking> getBookingsByCustomerNumber(string numberID) => GetBookingsByCustomerNumber(numberID);

        public Booking GetLatestBookingByRoomId(int roomId)
        {
            const string sql = @"
SELECT TOP 1
    br.BookingRoomID           AS BookingID,
    b.CustomerID,
    br.RoomID,
    br.PricingID,
    b.CreatedBy,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    br.Status,
    ISNULL(br.Note, b.Notes)   AS Notes
FROM BookingRooms br
JOIN Bookings     b ON b.BookingID = br.BookingID
WHERE br.RoomID = @RoomID
ORDER BY b.BookingDate DESC, br.CheckInDate DESC;";
            return QuerySingle(_cs, sql, cmd => cmd.Parameters.AddWithValue("@RoomID", roomId), MapBooking);
        }

        public Booking GetBookingById(int bookingId)
        {
            const string sql = @"
SELECT
    br.BookingRoomID           AS BookingID,
    b.CustomerID,
    br.RoomID,
    br.PricingID,
    b.CreatedBy,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    br.Status,
    ISNULL(br.Note, b.Notes)   AS Notes
FROM BookingRooms br
JOIN Bookings     b ON b.BookingID = br.BookingID
WHERE br.BookingRoomID = @Id;";
            return QuerySingle(_cs, sql, cmd => cmd.Parameters.AddWithValue("@Id", bookingId), MapBooking);
        }

        public Booking GetBookingRoomById(int bookingRoomId)
        {
            const string sql = @"
SELECT
    br.BookingRoomID AS BookingID,
    br.RoomID,
    br.PricingID,
    br.CheckInDate,
    br.CheckOutDate,
    br.Status,
    b.CustomerID,
    b.CreatedBy,
    b.BookingDate,
    b.Notes
FROM BookingRooms br
JOIN Bookings     b ON b.BookingID = br.BookingID
WHERE br.BookingRoomID = @Id;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", bookingRoomId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Booking
                    {
                        BookingID = rd.GetInt32(0),
                        RoomID = rd.GetInt32(1),
                        PricingID = rd.GetInt32(2),
                        CheckInDate = SafeGet<DateTime?>(rd, 3),
                        CheckOutDate = SafeGet<DateTime?>(rd, 4),
                        Status = rd.GetString(5),
                        CustomerID = rd.GetInt32(6),
                        CreatedBy = rd.GetInt32(7),
                        BookingDate = rd.GetDateTime(8),
                        Notes = SafeGet<string>(rd, 9)
                    };
                }
            }
        }

        public Booking GetActiveBookingByRoomId(int roomId)
        {
            const string sql = @"
SELECT TOP 1
    br.BookingRoomID AS BookingID,
    b.CustomerID,
    br.RoomID,
    br.PricingID,
    b.CreatedBy,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    br.Status,
    ISNULL(br.Note, b.Notes) AS Notes
FROM BookingRooms br
JOIN Bookings b ON b.BookingID = br.BookingID
WHERE br.RoomID = @RoomID
  AND br.Status IN ('Booked','CheckedIn')
ORDER BY ISNULL(br.CheckInDate, b.BookingDate) DESC;";
            return QuerySingle(_cs, sql, cmd => cmd.Parameters.AddWithValue("@RoomID", roomId), MapBooking);
        }

        // ---------------- Mutations (line-level) ----------------

        /// <summary>Thêm 1 line vào header có sẵn. Trả về BookingRoomID (line id).</summary>
        public int AddLineForHeader(int headerBookingId, Booking line)
        {
            const string sql = @"
INSERT INTO BookingRooms (BookingID, RoomID, PricingID, CheckInDate, CheckOutDate, Status, Note)
VALUES (@BID, @RoomID, @PricingID, @CI, @CO, @S, @Note);
SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@BID", headerBookingId);
                cmd.Parameters.AddWithValue("@RoomID", line.RoomID);
                cmd.Parameters.AddWithValue("@PricingID", line.PricingID);
                cmd.Parameters.AddWithValue("@CI", (object)line.CheckInDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CO", (object)line.CheckOutDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@S", string.IsNullOrWhiteSpace(line.Status) ? "Booked" : line.Status);
                cmd.Parameters.AddWithValue("@Note", (object)line.Notes ?? DBNull.Value);
                conn.Open();
                var lineId = Convert.ToInt32(cmd.ExecuteScalar());

                // cập nhật trạng thái phòng
                using (var upd = new SqlCommand("UPDATE Rooms SET Status = 'Booked' WHERE RoomID = @R", conn))
                {
                    upd.Parameters.AddWithValue("@R", line.RoomID);
                    upd.ExecuteNonQuery();
                }

                return lineId;
            }
        }

        /// <summary>Thêm nhiều line cho 1 header. Trả về map RoomID -> BookingRoomID.</summary>
        public Dictionary<int, int> AddLinesForHeader(int headerBookingId, List<Booking> roomBookings)
        {
            var map = new Dictionary<int, int>();
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    foreach (var b in roomBookings)
                    {
                        int bookingRoomId;
                        using (var cmd = new SqlCommand(@"
INSERT INTO BookingRooms (BookingID, RoomID, PricingID, CheckInDate, CheckOutDate, Status, Note)
VALUES (@BID, @RID, @PID, @CI, @CO, @S, @N);
SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@BID", headerBookingId);
                            cmd.Parameters.AddWithValue("@RID", b.RoomID);
                            cmd.Parameters.AddWithValue("@PID", b.PricingID);
                            cmd.Parameters.AddWithValue("@CI", (object)b.CheckInDate ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@CO", (object)b.CheckOutDate ?? DBNull.Value);
                            cmd.Parameters.AddWithValue("@S", string.IsNullOrWhiteSpace(b.Status) ? "Booked" : b.Status);
                            cmd.Parameters.AddWithValue("@N", (object)b.Notes ?? DBNull.Value);
                            bookingRoomId = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        using (var cmdU = new SqlCommand("UPDATE Rooms SET Status = 'Booked' WHERE RoomID = @R", conn, tran))
                        {
                            cmdU.Parameters.AddWithValue("@R", b.RoomID);
                            cmdU.ExecuteNonQuery();
                        }

                        map[b.RoomID] = bookingRoomId;
                    }

                    tran.Commit();
                }
            }
            return map;
        }

        public bool Update(Booking booking)
        {
            const string sql = @"
UPDATE  BookingRooms
SET     Status       = @Status,
        CheckOutDate = @CheckOutDate,
        Note         = @Note
WHERE   BookingRoomID = @Id;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", booking.BookingID);
                cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(booking.Status) ? "Booked" : booking.Status);
                cmd.Parameters.AddWithValue("@CheckOutDate", (object)booking.CheckOutDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Note", (object)booking.Notes ?? DBNull.Value);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateBookingStatusAndCheckOut(int bookingID, string status, DateTime checkoutDate)
        {
            const string sql = @"
UPDATE  BookingRooms
SET     Status = @Status,
        CheckOutDate = @CheckOutDate
WHERE   BookingRoomID = @Id;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@CheckOutDate", checkoutDate);
                cmd.Parameters.AddWithValue("@Id", bookingID);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CancelBooking(int bookingID)
        {
            const string sql = @"
UPDATE  BookingRooms
SET     Status = 'Cancelled'
WHERE   BookingRoomID = @Id;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", bookingID);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool CheckInBooking(int bookingID, DateTime checkInAt)
        {
            const string sql = @"
UPDATE br
SET br.Status      = 'CheckedIn',
    br.CheckInDate = ISNULL(br.CheckInDate, @CheckInAt)
FROM BookingRooms br
WHERE br.BookingRoomID = @Id AND br.Status = 'Booked';";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", bookingID);
                cmd.Parameters.AddWithValue("@CheckInAt", checkInAt);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>Checkout toàn bộ lines dưới một header.</summary>
        public bool SetLinesCheckedOutByHeader(int headerBookingId, DateTime checkoutAt)
        {
            const string sql = @"
UPDATE  br
SET     br.Status       = 'CheckedOut',
        br.CheckOutDate = ISNULL(br.CheckOutDate, @CO)
FROM    BookingRooms br
WHERE   br.BookingID = @HID
  AND   br.Status IN ('Booked','CheckedIn');";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@HID", headerBookingId);
                cmd.Parameters.AddWithValue("@CO", checkoutAt);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ---------------- Misc ----------------
        public int? GetHeaderIdByBookingRoomId(int bookingRoomId)
        {
            const string sql = @"SELECT BookingID FROM BookingRooms WHERE BookingRoomID = @id;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", bookingRoomId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? (int?)null : Convert.ToInt32(o);
            }
        }

        public string GetRoomNumberById(int roomId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT RoomNumber FROM Rooms WHERE RoomID = @id;", conn))
            {
                cmd.Parameters.AddWithValue("@id", roomId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null ? "" : o.ToString();
            }
        }
        public List<Booking> GetBookingsByHeaderId(int headerBookingId)
        {
            const string sql = @"
SELECT
    br.BookingRoomID           AS BookingID,
    b.CustomerID,
    br.RoomID,
    br.PricingID,
    b.CreatedBy,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    br.Status,
    ISNULL(br.Note, b.Notes)   AS Notes
FROM BookingRooms br
JOIN Bookings     b ON b.BookingID = br.BookingID
WHERE br.BookingID = @HID
ORDER BY ISNULL(br.CheckInDate, b.BookingDate) DESC;";
            return QueryList(_cs, sql,
                cmd => cmd.Parameters.AddWithValue("@HID", headerBookingId),
                MapBooking);
        }
    }
}
