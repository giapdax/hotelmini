using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    /// <summary>
    /// Repository: chỉ thao tác DB, KHÔNG hiển thị MessageBox và KHÔNG nuốt Exception.
    /// Không viết nghiệp vụ ở đây.
    /// </summary>
    public class BookingRepository
    {
        private readonly string _cs;

        public BookingRepository()
        {
            _cs = ConfigHelper.GetConnectionString();
        }

        // =========================================================
        // Generic helpers (để reuse, tránh duplicate)
        // =========================================================
        private static T SafeGet<T>(SqlDataReader rd, int ordinal)
        {
            return rd.IsDBNull(ordinal) ? default(T) : (T)rd.GetValue(ordinal);
        }

        private static Booking MapBooking(SqlDataReader rd)
        {
            // Cột có thể theo select alias chuẩn đã dùng
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

        private List<T> QueryList<T>(string sql, Action<SqlCommand> onParam, Func<SqlDataReader, T> mapper)
        {
            var list = new List<T>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (onParam != null) onParam(cmd);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) list.Add(mapper(rd));
                }
            }
            return list;
        }

        private T QuerySingle<T>(string sql, Action<SqlCommand> onParam, Func<SqlDataReader, T> mapper)
            where T : class
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (onParam != null) onParam(cmd);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return mapper(rd);
                }
            }
        }

        // =========================================================
        // Utilities
        // =========================================================
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
                var n = (int)cmd.ExecuteScalar();
                return n > 0;
            }
        }

        // =========================================================
        // Queries (DISPLAY)
        // =========================================================
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
FROM    BookingRooms br
JOIN    Bookings     b  ON b.BookingID  = br.BookingID
JOIN    Rooms        r  ON r.RoomID     = br.RoomID
JOIN    Users        u  ON u.UserID     = b.CreatedBy
JOIN    Customers    c  ON c.CustomerID = b.CustomerID
WHERE   br.Status = 'CheckedOut'
ORDER BY br.CheckOutDate DESC, b.BookingDate DESC";
            return QueryList(sql, null, MapBookingDisplay);
        }

        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string numberID)
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
FROM    Customers     c
JOIN    Bookings      b   ON b.CustomerID  = c.CustomerID
JOIN    BookingRooms  br  ON br.BookingID  = b.BookingID
JOIN    Rooms         r   ON r.RoomID      = br.RoomID
JOIN    Users         u   ON u.UserID      = b.CreatedBy
WHERE   c.IDNumber = @NumberID
ORDER BY b.BookingDate DESC, br.CheckInDate DESC";
            return QueryList(sql, cmd => cmd.Parameters.AddWithValue("@NumberID", numberID), MapBookingDisplay);
        }

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
FROM    BookingRooms br
JOIN    Bookings     b  ON b.BookingID  = br.BookingID
JOIN    Rooms        r  ON r.RoomID     = br.RoomID
JOIN    Users        u  ON u.UserID     = b.CreatedBy
JOIN    Customers    c  ON c.CustomerID = b.CustomerID
WHERE   br.Status IN ('Booked','CheckedIn')
ORDER BY b.BookingDate DESC, br.CheckInDate DESC";
            return QueryList(sql, null, MapBookingDisplay);
        }

        // =========================================================
        // Queries (Booking entity)
        // =========================================================
        public List<Booking> getBookingsByCustomerNumber(string numberID) // giữ API cũ
            => GetBookingsByCustomerNumber(numberID);

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
FROM    Customers     c
JOIN    Bookings      b   ON b.CustomerID  = c.CustomerID
JOIN    BookingRooms  br  ON br.BookingID  = b.BookingID
WHERE   c.IDNumber = @NumberID
ORDER BY b.BookingDate DESC, br.CheckInDate DESC";
            return QueryList(sql, cmd => cmd.Parameters.AddWithValue("@NumberID", numberID), MapBooking);
        }

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
FROM    BookingRooms br
JOIN    Bookings     b ON b.BookingID = br.BookingID
WHERE   br.RoomID = @RoomID
ORDER BY b.BookingDate DESC, br.CheckInDate DESC";
            return QuerySingle(sql, cmd => cmd.Parameters.AddWithValue("@RoomID", roomId), MapBooking);
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
FROM    BookingRooms br
JOIN    Bookings     b ON b.BookingID = br.BookingID
WHERE   br.BookingRoomID = @Id";
            return QuerySingle(sql, cmd => cmd.Parameters.AddWithValue("@Id", bookingId), MapBooking);
        }

        // =========================================================
        // Mutations
        // =========================================================
        public Booking AddBooking(Booking booking)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    const string sqlHeader = @"
INSERT INTO Bookings (CustomerID, CreatedBy, BookingDate, Status, Notes)
VALUES (@CustomerID, @CreatedBy, @BookingDate, @Status, @Notes);
SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    int headerId;
                    using (var cmd = new SqlCommand(sqlHeader, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", booking.CustomerID);
                        cmd.Parameters.AddWithValue("@CreatedBy", booking.CreatedBy);
                        cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate == default(DateTime) ? DateTime.Now : booking.BookingDate);
                        cmd.Parameters.AddWithValue("@Status", booking.Status ?? "Booked");
                        cmd.Parameters.AddWithValue("@Notes", (object)booking.Notes ?? DBNull.Value);
                        headerId = (int)cmd.ExecuteScalar();
                    }

                    const string sqlRoom = @"
INSERT INTO BookingRooms (BookingID, RoomID, PricingID, CheckInDate, CheckOutDate, Status, Note)
VALUES (@BookingID, @RoomID, @PricingID, @CheckInDate, @CheckOutDate, @Status, @Note);
SELECT CAST(SCOPE_IDENTITY() AS INT);";
                    int bookingRoomId;
                    using (var cmd = new SqlCommand(sqlRoom, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@BookingID", headerId);
                        cmd.Parameters.AddWithValue("@RoomID", booking.RoomID);
                        cmd.Parameters.AddWithValue("@PricingID", booking.PricingID);
                        cmd.Parameters.AddWithValue("@CheckInDate", (object)booking.CheckInDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@CheckOutDate", (object)booking.CheckOutDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Status", booking.Status ?? "Booked");
                        cmd.Parameters.AddWithValue("@Note", (object)booking.Notes ?? DBNull.Value);
                        bookingRoomId = (int)cmd.ExecuteScalar();
                    }

                    using (var cmd2 = new SqlCommand("UPDATE Rooms SET Status = 'Booked' WHERE RoomID = @RoomID", conn, tran))
                    {
                        cmd2.Parameters.AddWithValue("@RoomID", booking.RoomID);
                        cmd2.ExecuteNonQuery();
                    }

                    tran.Commit();

                    booking.BookingID = bookingRoomId;
                    booking.BookingDate = booking.BookingDate == default(DateTime) ? DateTime.Now : booking.BookingDate;
                    return booking;
                }
            }
        }

        public Dictionary<int, int> AddMultiRoomsBooking(
            int customerId,
            int createdBy,
            DateTime bookingDate,
            string status,
            List<Booking> roomBookings,
            DataTable usedServices)
        {
            var map = new Dictionary<int, int>();
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    int headerId;
                    using (var cmd = new SqlCommand(@"
INSERT INTO Bookings (CustomerID, CreatedBy, BookingDate, Status, Notes)
VALUES (@CustomerID, @CreatedBy, @BookingDate, @Status, @Notes);
SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", customerId);
                        cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                        cmd.Parameters.AddWithValue("@BookingDate", bookingDate);
                        cmd.Parameters.AddWithValue("@Status", status ?? "Booked");
                        cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                        headerId = (int)cmd.ExecuteScalar();
                    }

                    foreach (var b in roomBookings)
                    {
                        int bookingRoomId;
                        using (var cmdR = new SqlCommand(@"
INSERT INTO BookingRooms (BookingID, RoomID, PricingID, CheckInDate, CheckOutDate, Status, Note)
VALUES (@BookingID, @RoomID, @PricingID, @CheckInDate, @CheckOutDate, @Status, @Note);
SELECT CAST(SCOPE_IDENTITY() AS INT);", conn, tran))
                        {
                            cmdR.Parameters.AddWithValue("@BookingID", headerId);
                            cmdR.Parameters.AddWithValue("@RoomID", b.RoomID);
                            cmdR.Parameters.AddWithValue("@PricingID", b.PricingID);
                            cmdR.Parameters.AddWithValue("@CheckInDate", (object)b.CheckInDate ?? DBNull.Value);
                            cmdR.Parameters.AddWithValue("@CheckOutDate", (object)b.CheckOutDate ?? DBNull.Value);
                            cmdR.Parameters.AddWithValue("@Status", b.Status ?? "Booked");
                            cmdR.Parameters.AddWithValue("@Note", (object)b.Notes ?? DBNull.Value);
                            bookingRoomId = (int)cmdR.ExecuteScalar();
                        }

                        using (var cmdU = new SqlCommand("UPDATE Rooms SET Status = 'Booked' WHERE RoomID = @RoomID", conn, tran))
                        {
                            cmdU.Parameters.AddWithValue("@RoomID", b.RoomID);
                            cmdU.ExecuteNonQuery();
                        }

                        map[b.RoomID] = bookingRoomId;
                    }

                    if (usedServices != null && usedServices.Rows.Count > 0)
                    {
                        foreach (DataRow r in usedServices.Rows)
                        {
                            int roomId = r.Field<int>("RoomID");
                            int bookingRoomId;
                            if (!map.TryGetValue(roomId, out bookingRoomId)) continue;

                            int serviceId = r.Field<int>("ServiceID");
                            int qty = r.Field<int>("Quantity");

                            using (var cmdS = new SqlCommand(@"
MERGE BookingRoomServices AS T
USING (SELECT @BID AS BookingRoomID, @SID AS ServiceID) AS S
   ON (T.BookingRoomID = S.BookingRoomID AND T.ServiceID = S.ServiceID)
WHEN MATCHED THEN
   UPDATE SET Quantity = @Q
WHEN NOT MATCHED THEN
   INSERT (BookingRoomID, ServiceID, Quantity) VALUES (@BID, @SID, @Q);", conn, tran))
                            {
                                cmdS.Parameters.AddWithValue("@BID", bookingRoomId);
                                cmdS.Parameters.AddWithValue("@SID", serviceId);
                                cmdS.Parameters.AddWithValue("@Q", qty);
                                cmdS.ExecuteNonQuery();
                            }
                        }
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
WHERE   BookingRoomID = @Id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", booking.BookingID);
                cmd.Parameters.AddWithValue("@Status", booking.Status ?? "Booked");
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
WHERE   BookingRoomID = @Id";
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
WHERE   BookingRoomID = @Id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", bookingID);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // =========================================================
        // Services (attach/detach)
        // =========================================================
        public List<UsedServiceDto> GetUsedServicesByBookingID(int bookingID)
        {
            const string sql = @"
SELECT  brs.BookingRoomServiceID,
        brs.BookingRoomID   AS BookingID,
        s.ServiceID,
        s.ServiceName,
        s.Price,
        brs.Quantity
FROM    BookingRoomServices brs
JOIN    Services            s   ON s.ServiceID = brs.ServiceID
WHERE   brs.BookingRoomID = @Id";
            return QueryList(sql, cmd => cmd.Parameters.AddWithValue("@Id", bookingID), rd => new UsedServiceDto
            {
                BookingServiceID = rd.GetInt32(0),
                BookingID = rd.GetInt32(1),
                ServiceID = rd.GetInt32(2),
                ServiceName = rd.GetString(3),
                Price = rd.GetDecimal(4),
                Quantity = rd.GetInt32(5)
            });
        }

        public int GetAvailableServiceQuantity(int bookingRoomId, int serviceId)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"
DECLARE @stock INT = (SELECT Quantity FROM Services WHERE ServiceID=@sid);
IF @stock IS NULL THROW 50001, 'Service not found', 1;

DECLARE @usedOther INT = ISNULL((
    SELECT SUM(Quantity) FROM BookingRoomServices
    WHERE ServiceID=@sid AND BookingRoomID <> @bid
), 0);

SELECT CASE WHEN @stock - @usedOther < 0 THEN 0 ELSE @stock - @usedOther END;", conn))
                {
                    cmd.Parameters.AddWithValue("@sid", serviceId);
                    cmd.Parameters.AddWithValue("@bid", bookingRoomId);
                    return Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                }
            }
        }

        public bool AddOrUpdateServiceForBooking(int bookingID, int serviceID, int quantity)
        {
            if (quantity < 0) quantity = 0;

            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    // Stock
                    int stock;
                    using (var cmd = new SqlCommand(
                        "SELECT Quantity FROM Services WHERE ServiceID = @sid", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@sid", serviceID);
                        var o = cmd.ExecuteScalar();
                        if (o == null || o == DBNull.Value) throw new Exception("Dịch vụ không tồn tại.");
                        stock = Convert.ToInt32(o);
                    }

                    // Đã dùng ở các phòng khác
                    int usedOther;
                    using (var cmd = new SqlCommand(@"
SELECT  ISNULL(SUM(Quantity), 0)
FROM    BookingRoomServices
WHERE   ServiceID = @sid
  AND   BookingRoomID <> @bid", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@sid", serviceID);
                        cmd.Parameters.AddWithValue("@bid", bookingID);
                        usedOther = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }

                    if (usedOther + quantity > stock)
                    {
                        tran.Rollback();
                        int available = Math.Max(0, stock - usedOther);
                        throw new InvalidOperationException("Vượt tồn kho. Tối đa còn lại: " + available);
                    }

                    // Upsert: SET Quantity = quantity
                    int existed;
                    using (var cmd = new SqlCommand(@"
SELECT COUNT(1) FROM BookingRoomServices
WHERE BookingRoomID=@bid AND ServiceID=@sid", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@bid", bookingID);
                        cmd.Parameters.AddWithValue("@sid", serviceID);
                        existed = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }

                    if (existed > 0)
                    {
                        using (var up = new SqlCommand(@"
UPDATE BookingRoomServices
SET Quantity = @q
WHERE BookingRoomID=@bid AND ServiceID=@sid", conn, tran))
                        {
                            up.Parameters.AddWithValue("@q", quantity);
                            up.Parameters.AddWithValue("@bid", bookingID);
                            up.Parameters.AddWithValue("@sid", serviceID);
                            up.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (var ins = new SqlCommand(@"
INSERT INTO BookingRoomServices (BookingRoomID, ServiceID, Quantity)
VALUES (@bid, @sid, @q)", conn, tran))
                        {
                            ins.Parameters.AddWithValue("@bid", bookingID);
                            ins.Parameters.AddWithValue("@sid", serviceID);
                            ins.Parameters.AddWithValue("@q", quantity);
                            ins.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    return true;
                }
            }
        }

        public void RemoveServiceFromBooking(int bookingServiceId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("DELETE FROM BookingRoomServices WHERE BookingRoomServiceID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", bookingServiceId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateServiceQuantity(int bookingServiceId, int newQuantity)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("UPDATE BookingRoomServices SET Quantity = @qty WHERE BookingRoomServiceID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@qty", newQuantity);
                cmd.Parameters.AddWithValue("@id", bookingServiceId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // =========================================================
        // Misc helpers
        // =========================================================
        public int? GetHeaderIdByBookingRoomId(int bookingRoomId)
        {
            const string sql = @"SELECT BookingID FROM BookingRooms WHERE BookingRoomID = @id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", bookingRoomId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? (int?)null : Convert.ToInt32(o);
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
FROM    BookingRooms br
JOIN    Bookings     b ON b.BookingID = br.BookingID
WHERE   br.BookingID = @HID
ORDER BY ISNULL(br.CheckInDate, b.BookingDate) DESC";
            return QueryList(sql, cmd => cmd.Parameters.AddWithValue("@HID", headerBookingId), MapBooking);
        }

        public class CustomerBasic
        {
            public int CustomerID { get; set; }
            public string FullName { get; set; }
            public string IDNumber { get; set; }
        }

        public CustomerBasic GetCustomerBasicById(int customerId)
        {
            const string sql = @"
SELECT  CustomerID, FullName, IDNumber
FROM    Customers
WHERE   CustomerID = @Id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", customerId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new CustomerBasic
                    {
                        CustomerID = rd.GetInt32(0),
                        FullName = SafeGet<string>(rd, 1) ?? "",
                        IDNumber = SafeGet<string>(rd, 2) ?? ""
                    };
                }
            }
        }

        public Customer GetCustomerByHeaderId(int headerBookingId)
        {
            const string sql = @"
SELECT  c.CustomerID, c.FullName, c.Gender, c.Phone, c.Email, c.Address, c.IDNumber
FROM    Bookings  b
JOIN    Customers c ON c.CustomerID = b.CustomerID
WHERE   b.BookingID = @id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", headerBookingId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Customer
                    {
                        CustomerID = rd.GetInt32(0),
                        FullName = SafeGet<string>(rd, 1) ?? "",
                        Gender = SafeGet<string>(rd, 2) ?? "",
                        Phone = SafeGet<string>(rd, 3) ?? "",
                        Email = SafeGet<string>(rd, 4) ?? "",
                        Address = SafeGet<string>(rd, 5) ?? "",
                        IDNumber = SafeGet<string>(rd, 6) ?? "",
                    };
                }
            }
        }

        public string GetRoomNumberById(int roomId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT RoomNumber FROM Rooms WHERE RoomID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", roomId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null ? "" : o.ToString();
            }
        }

        public List<string> getPaymentMethods() // giữ API cũ
            => GetPaymentMethods();

        public List<string> GetPaymentMethods()
        {
            return QueryList("SELECT Value FROM PaymentMethodEnum", null, rd => rd.GetString(0));
        }

        public string GetUserFullNameById(int userId)
        {
            const string sql = @"SELECT FullName FROM Users WHERE UserID = @Id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", userId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? "" : o.ToString();
            }
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
FROM    BookingRooms br
JOIN    Bookings     b ON b.BookingID = br.BookingID
WHERE   br.BookingRoomID = @Id";
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

        public bool UpdateHeaderStatus(int headerBookingId, string status)
        {
            const string sql = @"
UPDATE  Bookings
SET     Status = @Status
WHERE   BookingID = @Id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Id", headerBookingId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetServiceMenuWithStock()
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
SELECT  s.ServiceID   AS ID,
        s.ServiceName AS [Dịch vụ],
        s.Price       AS [Đơn giá],
        s.Quantity    AS [Tồn kho]
FROM    Services s
ORDER BY s.ServiceName;", conn))
            {
                conn.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
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
ORDER BY ISNULL(br.CheckInDate, b.BookingDate) DESC";
            return QuerySingle(sql, cmd => cmd.Parameters.AddWithValue("@RoomID", roomId), MapBooking);
        }

        public bool CheckInBooking(int bookingID, DateTime checkInAt)
        {
            const string sql = @"
UPDATE br
SET br.Status      = 'CheckedIn',
    br.CheckInDate = ISNULL(br.CheckInDate, @CheckInAt)
FROM BookingRooms br
WHERE br.BookingRoomID = @Id AND br.Status = 'Booked'";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", bookingID);
                cmd.Parameters.AddWithValue("@CheckInAt", checkInAt);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

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

        // ====== (Optional) Payments đọc nhanh (nếu chưa có PaymentRepository) ======
        public List<Payment> GetPaymentsByInvoice(int invoiceId)
        {
            const string sql = @"
SELECT PaymentID, InvoiceID, Amount, PaymentDate, Method, Status
FROM   Payments
WHERE  InvoiceID=@I
ORDER BY PaymentDate";
            return QueryList(sql, cmd => cmd.Parameters.AddWithValue("@I", invoiceId), rd => new Payment
            {
                PaymentID = rd.GetInt32(0),
                InvoiceID = rd.GetInt32(1),
                Amount = rd.GetDecimal(2),
                PaymentDate = rd.GetDateTime(3),
                Method = rd.GetString(4),
                Status = rd.GetString(5)
            });
        }

        public void AddPayment(Payment p)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"
INSERT INTO Payments(InvoiceID, Amount, PaymentDate, Method, Status)
VALUES(@I,@A,@D,@M,@S)", conn))
            {
                cmd.Parameters.AddWithValue("@I", p.InvoiceID);
                cmd.Parameters.AddWithValue("@A", p.Amount);
                cmd.Parameters.AddWithValue("@D", p.PaymentDate);
                cmd.Parameters.AddWithValue("@M", p.Method ?? "Cash");
                cmd.Parameters.AddWithValue("@S", p.Status ?? "Paid");
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
