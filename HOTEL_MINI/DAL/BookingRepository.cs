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
    /// </summary>
    public class BookingRepository
    {
        private readonly string _cs;
        public BookingRepository()
        {
            _cs = ConfigHelper.GetConnectionString();
        }

        // ----------------- Utilities -----------------
        public bool IsRoomOverlapped(int roomId, DateTime checkIn, DateTime checkOut)
        {
            const string sql = @"
SELECT COUNT(1)
FROM BookingRooms br WITH (NOLOCK)
WHERE br.RoomID = @RoomID
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

        // ----------------- Queries (DISPLAY) -----------------

        public List<BookingDisplay> GetTop20LatestBookingDisplays()
        {
            var list = new List<BookingDisplay>();
            const string sql = @"
SELECT TOP 20 
    br.BookingRoomID AS BookingID,
    r.RoomNumber,
    u.FullName AS EmployeeName,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    ISNULL(br.Note, b.Notes) AS Notes,
    br.Status,
    c.IDNumber AS CustomerIDNumber
FROM BookingRooms br
JOIN Bookings b  ON b.BookingID = br.BookingID
JOIN Rooms r     ON r.RoomID    = br.RoomID
JOIN Users u     ON u.UserID    = b.CreatedBy
JOIN Customers c ON c.CustomerID = b.CustomerID
WHERE br.Status = 'CheckedOut'
ORDER BY br.CheckOutDate DESC, b.BookingDate DESC";

            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new BookingDisplay
                        {
                            BookingID = rd.GetInt32(0),
                            RoomNumber = rd.GetString(1),
                            EmployeeName = rd.GetString(2),
                            BookingDate = rd.GetDateTime(3),
                            CheckInDate = rd.IsDBNull(4) ? (DateTime?)null : rd.GetDateTime(4),
                            CheckOutDate = rd.IsDBNull(5) ? (DateTime?)null : rd.GetDateTime(5),
                            Notes = rd.IsDBNull(6) ? null : rd.GetString(6),
                            Status = rd.GetString(7),
                            CustomerIDNumber = rd.IsDBNull(8) ? null : rd.GetString(8)
                        });
                    }
                }
            }
            return list;
        }

        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string numberID)
        {
            var list = new List<BookingDisplay>();
            const string sql = @"
SELECT 
    br.BookingRoomID AS BookingID,
    r.RoomNumber,
    u.FullName AS EmployeeName,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    ISNULL(br.Note, b.Notes) AS Notes,
    br.Status,
    c.IDNumber AS CustomerIDNumber
FROM Customers c
JOIN Bookings b       ON b.CustomerID = c.CustomerID
JOIN BookingRooms br  ON br.BookingID = b.BookingID
JOIN Rooms r          ON r.RoomID     = br.RoomID
JOIN Users u          ON u.UserID     = b.CreatedBy
WHERE c.IDNumber = @NumberID
ORDER BY b.BookingDate DESC, br.CheckInDate DESC";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@NumberID", numberID);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new BookingDisplay
                        {
                            BookingID = rd.GetInt32(0),
                            RoomNumber = rd.GetString(1),
                            EmployeeName = rd.GetString(2),
                            BookingDate = rd.GetDateTime(3),
                            CheckInDate = rd.IsDBNull(4) ? (DateTime?)null : rd.GetDateTime(4),
                            CheckOutDate = rd.IsDBNull(5) ? (DateTime?)null : rd.GetDateTime(5),
                            Notes = rd.IsDBNull(6) ? null : rd.GetString(6),
                            Status = rd.GetString(7),
                            CustomerIDNumber = rd.IsDBNull(8) ? null : rd.GetString(8)
                        });
                    }
                }
            }
            return list;
        }

        // >>> NEW/FIX: Active displays phải có CCCD
        public List<BookingDisplay> GetActiveBookingDisplays()
        {
            var list = new List<BookingDisplay>();
            const string sql = @"
SELECT 
    br.BookingRoomID AS BookingID,
    r.RoomNumber,
    u.FullName AS EmployeeName,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    ISNULL(br.Note, b.Notes) AS Notes,
    br.Status,
    c.IDNumber AS CustomerIDNumber
FROM BookingRooms br
JOIN Bookings b  ON b.BookingID = br.BookingID
JOIN Rooms r     ON r.RoomID    = br.RoomID
JOIN Users u     ON u.UserID    = b.CreatedBy
JOIN Customers c ON c.CustomerID = b.CustomerID
WHERE br.Status IN ('Booked','CheckedIn')
ORDER BY b.BookingDate DESC, br.CheckInDate DESC";

            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new BookingDisplay
                        {
                            BookingID = rd.GetInt32(0),
                            RoomNumber = rd.GetString(1),
                            EmployeeName = rd.GetString(2),
                            BookingDate = rd.GetDateTime(3),
                            CheckInDate = rd.IsDBNull(4) ? (DateTime?)null : rd.GetDateTime(4),
                            CheckOutDate = rd.IsDBNull(5) ? (DateTime?)null : rd.GetDateTime(5),
                            Notes = rd.IsDBNull(6) ? null : rd.GetString(6),
                            Status = rd.GetString(7),
                            CustomerIDNumber = rd.IsDBNull(8) ? null : rd.GetString(8)
                        });
                    }
                }
            }
            return list;
        }

        // ----------------- Queries (Booking entity) -----------------

        public List<Booking> getBookingsByCustomerNumber(string numberID)
        {
            var list = new List<Booking>();
            const string sql = @"
SELECT 
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
FROM Customers c
JOIN Bookings b       ON b.CustomerID = c.CustomerID
JOIN BookingRooms br  ON br.BookingID = b.BookingID
WHERE c.IDNumber = @NumberID
ORDER BY b.BookingDate DESC, br.CheckInDate DESC";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@NumberID", numberID);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Booking
                        {
                            BookingID = rd.GetInt32(0),
                            CustomerID = rd.GetInt32(1),
                            RoomID = rd.GetInt32(2),
                            PricingID = rd.GetInt32(3),
                            CreatedBy = rd.GetInt32(4),
                            BookingDate = rd.GetDateTime(5),
                            CheckInDate = rd.IsDBNull(6) ? (DateTime?)null : rd.GetDateTime(6),
                            CheckOutDate = rd.IsDBNull(7) ? (DateTime?)null : rd.GetDateTime(7),
                            Status = rd.GetString(8),
                            Notes = rd.IsDBNull(9) ? null : rd.GetString(9)
                        });
                    }
                }
            }
            return list;
        }

        public Booking GetLatestBookingByRoomId(int roomId)
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
ORDER BY b.BookingDate DESC, br.CheckInDate DESC";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Booking
                    {
                        BookingID = rd.GetInt32(0),
                        CustomerID = rd.GetInt32(1),
                        RoomID = rd.GetInt32(2),
                        PricingID = rd.GetInt32(3),
                        CreatedBy = rd.GetInt32(4),
                        BookingDate = rd.GetDateTime(5),
                        CheckInDate = rd.IsDBNull(6) ? (DateTime?)null : rd.GetDateTime(6),
                        CheckOutDate = rd.IsDBNull(7) ? (DateTime?)null : rd.GetDateTime(7),
                        Status = rd.GetString(8),
                        Notes = rd.IsDBNull(9) ? null : rd.GetString(9)
                    };
                }
            }
        }

        public Booking GetBookingById(int bookingId)
        {
            const string sql = @"
SELECT 
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
WHERE br.BookingRoomID = @Id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", bookingId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Booking
                    {
                        BookingID = rd.GetInt32(0),
                        CustomerID = rd.GetInt32(1),
                        RoomID = rd.GetInt32(2),
                        PricingID = rd.GetInt32(3),
                        CreatedBy = rd.GetInt32(4),
                        BookingDate = rd.GetDateTime(5),
                        CheckInDate = rd.IsDBNull(6) ? (DateTime?)null : rd.GetDateTime(6),
                        CheckOutDate = rd.IsDBNull(7) ? (DateTime?)null : rd.GetDateTime(7),
                        Status = rd.GetString(8),
                        Notes = rd.IsDBNull(9) ? null : rd.GetString(9)
                    };
                }
            }
        }

        // ----------------- Mutations -----------------

        public Booking AddBooking(Booking booking)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    const string sqlHeader = @"
INSERT INTO Bookings(CustomerID, CreatedBy, BookingDate, Status, Notes)
VALUES(@CustomerID, @CreatedBy, @BookingDate, @Status, @Notes);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    int headerId;
                    using (var cmd = new SqlCommand(sqlHeader, conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@CustomerID", booking.CustomerID);
                        cmd.Parameters.AddWithValue("@CreatedBy", booking.CreatedBy);
                        cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                        cmd.Parameters.AddWithValue("@Status", booking.Status ?? "Booked");
                        cmd.Parameters.AddWithValue("@Notes", (object)booking.Notes ?? DBNull.Value);
                        headerId = (int)cmd.ExecuteScalar();
                    }

                    const string sqlRoom = @"
INSERT INTO BookingRooms(BookingID, RoomID, PricingID, CheckInDate, CheckOutDate, Status, Note)
VALUES(@BookingID, @RoomID, @PricingID, @CheckInDate, @CheckOutDate, @Status, @Note);
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

                    using (var cmd2 = new SqlCommand("UPDATE Rooms SET Status = 'Booked' WHERE RoomID=@RoomID", conn, tran))
                    {
                        cmd2.Parameters.AddWithValue("@RoomID", booking.RoomID);
                        cmd2.ExecuteNonQuery();
                    }

                    tran.Commit();

                    booking.BookingID = bookingRoomId;
                    booking.BookingDate = booking.BookingDate == default ? DateTime.Now : booking.BookingDate;
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
INSERT INTO Bookings(CustomerID, CreatedBy, BookingDate, Status, Notes)
VALUES(@CustomerID, @CreatedBy, @BookingDate, @Status, @Notes);
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
INSERT INTO BookingRooms(BookingID, RoomID, PricingID, CheckInDate, CheckOutDate, Status, Note)
VALUES(@BookingID, @RoomID, @PricingID, @CheckInDate, @CheckOutDate, @Status, @Note);
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

                        using (var cmdU = new SqlCommand("UPDATE Rooms SET Status='Booked' WHERE RoomID=@RoomID", conn, tran))
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
                            if (!map.TryGetValue(roomId, out var bookingRoomId)) continue;

                            int serviceId = r.Field<int>("ServiceID");
                            int qty = r.Field<int>("Quantity");

                            using (var cmdS = new SqlCommand(@"
MERGE BookingRoomServices AS T
USING (SELECT @BID AS BookingRoomID, @SID AS ServiceID) AS S
ON (T.BookingRoomID = S.BookingRoomID AND T.ServiceID = S.ServiceID)
WHEN MATCHED THEN UPDATE SET Quantity = T.Quantity + @Q
WHEN NOT MATCHED THEN INSERT(BookingRoomID, ServiceID, Quantity) VALUES(@BID, @SID, @Q);", conn, tran))
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
UPDATE BookingRooms
SET Status = @Status,
    CheckOutDate = @CheckOutDate,
    Note = @Note
WHERE BookingRoomID = @Id";
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
            const string sql = @"UPDATE BookingRooms 
                                 SET Status = @Status, CheckOutDate = @CheckOutDate
                                 WHERE BookingRoomID = @Id";
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
            const string sql = @"UPDATE BookingRooms SET Status = 'Cancelled' WHERE BookingRoomID = @Id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", bookingID);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ----------------- Services (attach/detach) -----------------

        public List<UsedServiceDto> GetUsedServicesByBookingID(int bookingID)
        {
            var list = new List<UsedServiceDto>();
            const string sql = @"
SELECT brs.BookingRoomServiceID, brs.BookingRoomID AS BookingID, s.ServiceID,
       s.ServiceName, s.Price, brs.Quantity
FROM BookingRoomServices brs
JOIN Services s ON s.ServiceID = brs.ServiceID
WHERE brs.BookingRoomID = @Id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", bookingID);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new UsedServiceDto
                        {
                            BookingServiceID = rd.GetInt32(0),
                            BookingID = rd.GetInt32(1),
                            ServiceID = rd.GetInt32(2),
                            ServiceName = rd.GetString(3),
                            Price = rd.GetDecimal(4),
                            Quantity = rd.GetInt32(5)
                        });
                    }
                }
            }
            return list;
        }

        public bool AddOrUpdateServiceForBooking(int bookingID, int serviceID, int quantity)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();

                const string checkSql = @"SELECT Quantity 
                                          FROM BookingRoomServices 
                                          WHERE BookingRoomID = @BID AND ServiceID = @SID";
                using (var checkCmd = new SqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@BID", bookingID);
                    checkCmd.Parameters.AddWithValue("@SID", serviceID);
                    object obj = checkCmd.ExecuteScalar();

                    if (obj != null)
                    {
                        int cur = Convert.ToInt32(obj);
                        int newQty = cur + quantity;

                        const string upSql = @"UPDATE BookingRoomServices 
                                               SET Quantity = @Q 
                                               WHERE BookingRoomID = @BID AND ServiceID = @SID";
                        using (var upCmd = new SqlCommand(upSql, conn))
                        {
                            upCmd.Parameters.AddWithValue("@Q", newQty);
                            upCmd.Parameters.AddWithValue("@BID", bookingID);
                            upCmd.Parameters.AddWithValue("@SID", serviceID);
                            return upCmd.ExecuteNonQuery() > 0;
                        }
                    }
                    else
                    {
                        const string insSql = @"INSERT INTO BookingRoomServices(BookingRoomID, ServiceID, Quantity)
                                                VALUES(@BID, @SID, @QTY)";
                        using (var insCmd = new SqlCommand(insSql, conn))
                        {
                            insCmd.Parameters.AddWithValue("@BID", bookingID);
                            insCmd.Parameters.AddWithValue("@SID", serviceID);
                            insCmd.Parameters.AddWithValue("@QTY", quantity);
                            return insCmd.ExecuteNonQuery() > 0;
                        }
                    }
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
ORDER BY ISNULL(br.CheckInDate, b.BookingDate) DESC";

            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new Booking
                    {
                        BookingID = rd.GetInt32(0),
                        CustomerID = rd.GetInt32(1),
                        RoomID = rd.GetInt32(2),
                        PricingID = rd.GetInt32(3),
                        CreatedBy = rd.GetInt32(4),
                        BookingDate = rd.GetDateTime(5),
                        CheckInDate = rd.IsDBNull(6) ? (DateTime?)null : rd.GetDateTime(6),
                        CheckOutDate = rd.IsDBNull(7) ? (DateTime?)null : rd.GetDateTime(7),
                        Status = rd.GetString(8),
                        Notes = rd.IsDBNull(9) ? null : rd.GetString(9)
                    };
                }
            }
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

        // ----------------- Misc helpers -----------------
        public int? GetHeaderIdByBookingRoomId(int bookingRoomId)
        {
            const string sql = @"SELECT BookingID FROM BookingRooms WHERE BookingRoomID=@id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", bookingRoomId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null || o == DBNull.Value ? (int?)null : Convert.ToInt32(o);
            }
        }

        /// <summary>
        /// Lấy TẤT CẢ BookingRooms (dạng Booking) thuộc 1 header BookingID
        /// </summary>
        public List<Booking> GetBookingsByHeaderId(int headerBookingId)
        {
            var list = new List<Booking>();
            const string sql = @"
SELECT 
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
WHERE br.BookingID = @HID
ORDER BY ISNULL(br.CheckInDate, b.BookingDate) DESC";

            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@HID", headerBookingId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Booking
                        {
                            BookingID = rd.GetInt32(0),
                            CustomerID = rd.GetInt32(1),
                            RoomID = rd.GetInt32(2),
                            PricingID = rd.GetInt32(3),
                            CreatedBy = rd.GetInt32(4),
                            BookingDate = rd.GetDateTime(5),
                            CheckInDate = rd.IsDBNull(6) ? (DateTime?)null : rd.GetDateTime(6),
                            CheckOutDate = rd.IsDBNull(7) ? (DateTime?)null : rd.GetDateTime(7),
                            Status = rd.GetString(8),
                            Notes = rd.IsDBNull(9) ? null : rd.GetString(9)
                        });
                    }
                }
            }
            return list;
        }
        public class CustomerBasic
        {
            public int CustomerID { get; set; }
            public string FullName { get; set; }
            public string IDNumber { get; set; }
        }

        public CustomerBasic GetCustomerBasicById(int customerId)
        {
            const string sql = "SELECT CustomerID, FullName, IDNumber FROM Customers WHERE CustomerID = @Id";
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
                        FullName = rd.IsDBNull(1) ? "" : rd.GetString(1),
                        IDNumber = rd.IsDBNull(2) ? "" : rd.GetString(2)
                    };
                }
            }
        }
        // LẤY KHÁCH HÀNG TỪ HEADER BOOKINGID
        public Customer GetCustomerByHeaderId(int headerBookingId)
        {
            const string sql = @"
SELECT c.CustomerID, c.FullName, c.Gender, c.Phone, c.Email, c.Address, c.IDNumber
FROM Bookings b
JOIN Customers c ON c.CustomerID = b.CustomerID
WHERE b.BookingID = @id";
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
                        FullName = rd.IsDBNull(1) ? "" : rd.GetString(1),
                        Gender = rd.IsDBNull(2) ? "" : rd.GetString(2),
                        Phone = rd.IsDBNull(3) ? "" : rd.GetString(3),
                        Email = rd.IsDBNull(4) ? "" : rd.GetString(4),
                        Address = rd.IsDBNull(5) ? "" : rd.GetString(5),
                        IDNumber = rd.IsDBNull(6) ? "" : rd.GetString(6),
                    };
                }
            }
        }

        public string GetRoomNumberById(int roomId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT RoomNumber FROM Rooms WHERE RoomID=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", roomId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null ? "" : o.ToString();
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

        public List<string> getPaymentMethods()
        {
            var list = new List<string>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(@"SELECT Value FROM PaymentMethodEnum", conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) list.Add(rd.GetString(0));
                }
            }
            return list;
        }
        public string GetUserFullNameById(int userId)
        {
            const string sql = "SELECT FullName FROM Users WHERE UserID = @Id";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", userId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null || o == DBNull.Value ? "" : o.ToString();
            }
        }

    }
}
