using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace HOTEL_MINI.DAL
{
    /// <summary>
    /// HEADER-ONLY:
    /// - Chỉ thao tác bảng Bookings + truy vấn đọc phục vụ UI/invoice.
    /// - KHÔNG ghi BookingRooms/BookingRoomServices.
    /// - Không UI/MessageBox, không nuốt exception.
    /// </summary>
    public class BookingRepository
    {
        private readonly string _cs;

        public BookingRepository()
        {
            _cs = ConfigHelper.GetConnectionString();
        }

        // ---------------- Helpers ----------------
        private static T SafeGet<T>(SqlDataReader rd, int ordinal)
            => rd.IsDBNull(ordinal) ? default : (T)rd.GetValue(ordinal);

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
            // Luôn lấy DateTime non-nullable để không vướng lỗi DateTime? -> DateTime
            var bookingDate = rd.IsDBNull(5) ? DateTime.MinValue : rd.GetDateTime(5);
            var checkInDate = rd.IsDBNull(6) ? DateTime.MinValue : rd.GetDateTime(6);
            var checkOutDate = rd.IsDBNull(7) ? DateTime.MinValue : rd.GetDateTime(7);

            return new Booking
            {
                // Lưu ý: BookingID ở đây chính là BookingRoomID (line)
                BookingID = rd.GetInt32(0),
                CustomerID = rd.GetInt32(1),
                RoomID = rd.GetInt32(2),
                PricingID = rd.GetInt32(3),
                CreatedBy = rd.GetInt32(4),
                BookingDate = bookingDate,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                Status = SafeGet<string>(rd, 8) ?? "",
                Notes = SafeGet<string>(rd, 9) ?? ""
            };
        }

        // --------------- Header CRUD ---------------
        /// <summary>Tạo header booking (KHÔNG tạo line). Trả về BookingID (header).</summary>
        public int AddHeader(int customerId, int createdBy, DateTime bookingDate, string status, string notes)
        {
            const string sql = @"
INSERT INTO Bookings (CustomerID, CreatedBy, BookingDate, Status, Notes)
VALUES (@C, @U, @D, @S, @N);
SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@C", customerId);
                cmd.Parameters.AddWithValue("@U", createdBy);
                cmd.Parameters.AddWithValue("@D", bookingDate == default(DateTime) ? DateTime.Now : bookingDate);
                cmd.Parameters.AddWithValue("@S", string.IsNullOrWhiteSpace(status) ? "Booked" : status);
                cmd.Parameters.AddWithValue("@N", (object)notes ?? DBNull.Value);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        /// <summary>Cập nhật trạng thái header.</summary>
        public bool UpdateHeaderStatus(int headerBookingId, string status)
        {
            const string sql = @"UPDATE Bookings SET Status = @S WHERE BookingID = @Id;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@S", status);
                cmd.Parameters.AddWithValue("@Id", headerBookingId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        /// <summary>Lấy thông tin khách theo header.</summary>
        public Customer GetCustomerByHeaderId(int headerBookingId)
        {
            const string sql = @"
SELECT c.CustomerID, c.FullName, c.Gender, c.Phone, c.Email, c.Address, c.IDNumber
FROM   Bookings b
JOIN   Customers c ON c.CustomerID = b.CustomerID
WHERE  b.BookingID = @id;";
            return QuerySingle(_cs, sql,
                cmd => cmd.Parameters.AddWithValue("@id", headerBookingId),
                rd => new Customer
                {
                    CustomerID = rd.GetInt32(0),
                    FullName = SafeGet<string>(rd, 1) ?? "",
                    Gender = SafeGet<string>(rd, 2) ?? "",
                    Phone = SafeGet<string>(rd, 3) ?? "",
                    Email = SafeGet<string>(rd, 4) ?? "",
                    Address = SafeGet<string>(rd, 5) ?? "",
                    IDNumber = SafeGet<string>(rd, 6) ?? ""
                });
        }

        // --------------- Queries header/line cơ bản ---------------
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
WHERE br.BookingID = @Id
ORDER BY ISNULL(br.CheckOutDate, br.CheckInDate) DESC, b.BookingDate DESC;";
            return QueryList(_cs, sql,
                cmd => cmd.Parameters.AddWithValue("@Id", headerBookingId),
                MapBooking);
        }

        // --------------- BookingDisplay cho UI/Invoice ---------------
        public List<BookingDisplay> GetTopLatestBookingDisplays(int top, bool onlyCheckedOut)
        {
            const string sql = @"
SELECT TOP (@Top)
    br.BookingRoomID                                       AS BookingID,
    r.RoomNumber,
    u.FullName                                             AS EmployeeName,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    ISNULL(br.Note, b.Notes)                               AS Notes,
    br.Status
FROM BookingRooms br
JOIN Bookings     b ON b.BookingID = br.BookingID
JOIN Rooms        r ON r.RoomID     = br.RoomID
LEFT JOIN Users   u ON u.UserID     = b.CreatedBy
WHERE (@OnlyCheckedOut = 0 OR br.Status = 'CheckedOut')
ORDER BY ISNULL(br.CheckOutDate, br.CheckInDate) DESC, b.BookingDate DESC;";

            return QueryList(_cs, sql,
                cmd =>
                {
                    cmd.Parameters.Add("@Top", SqlDbType.Int).Value = top;
                    cmd.Parameters.Add("@OnlyCheckedOut", SqlDbType.Bit).Value = onlyCheckedOut ? 1 : 0;
                },
                rd =>
                {
                    // Gán DateTime (non-null) để không gặp lỗi DateTime? -> DateTime
                    var bookingDate = rd.IsDBNull(3) ? DateTime.MinValue : rd.GetDateTime(3);
                    var checkInDate = rd.IsDBNull(4) ? DateTime.MinValue : rd.GetDateTime(4);
                    var checkOutDate = rd.IsDBNull(5) ? DateTime.MinValue : rd.GetDateTime(5);

                    return new BookingDisplay
                    {
                        BookingID = rd.GetInt32(0),
                        RoomNumber = SafeGet<string>(rd, 1) ?? "",
                        EmployeeName = SafeGet<string>(rd, 2) ?? "",
                        BookingDate = bookingDate,
                        CheckInDate = checkInDate,
                        CheckOutDate = checkOutDate,
                        Notes = SafeGet<string>(rd, 6) ?? "",
                        Status = SafeGet<string>(rd, 7) ?? ""
                        // KHÔNG set *Display vì là readonly (chỉ getter)
                    };
                });
        }

        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string idNumber)
        {
            const string sql = @"
SELECT
    br.BookingRoomID                                       AS BookingID,
    r.RoomNumber,
    u.FullName                                             AS EmployeeName,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    ISNULL(br.Note, b.Notes)                               AS Notes,
    br.Status
FROM Customers c
JOIN Bookings b      ON b.CustomerID = c.CustomerID
JOIN BookingRooms br ON br.BookingID = b.BookingID
JOIN Rooms r         ON r.RoomID     = br.RoomID
LEFT JOIN Users u    ON u.UserID     = b.CreatedBy
WHERE c.IDNumber = @ID
ORDER BY ISNULL(br.CheckOutDate, br.CheckInDate) DESC, b.BookingDate DESC;";

            return QueryList(_cs, sql,
                cmd => cmd.Parameters.AddWithValue("@ID", idNumber),
                rd =>
                {
                    var bookingDate = rd.IsDBNull(3) ? DateTime.MinValue : rd.GetDateTime(3);
                    var checkInDate = rd.IsDBNull(4) ? DateTime.MinValue : rd.GetDateTime(4);
                    var checkOutDate = rd.IsDBNull(5) ? DateTime.MinValue : rd.GetDateTime(5);

                    return new BookingDisplay
                    {
                        BookingID = rd.GetInt32(0),
                        RoomNumber = SafeGet<string>(rd, 1) ?? "",
                        EmployeeName = SafeGet<string>(rd, 2) ?? "",
                        BookingDate = bookingDate,
                        CheckInDate = checkInDate,
                        CheckOutDate = checkOutDate,
                        Notes = SafeGet<string>(rd, 6) ?? "",
                        Status = SafeGet<string>(rd, 7) ?? ""
                    };
                });
        }

        // --------------- Payments ---------------
        public List<Payment> GetPaymentsByInvoice(int invoiceId)
        {
            const string sql = @"
SELECT PaymentID, InvoiceID, Amount, PaymentDate, Method, Status
FROM   Payments
WHERE  InvoiceID = @I
ORDER BY PaymentDate;";
            return QueryList(_cs, sql,
                cmd => cmd.Parameters.AddWithValue("@I", invoiceId),
                rd => new Payment
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
            const string sql = @"
INSERT INTO Payments(InvoiceID, Amount, PaymentDate, Method, Status)
VALUES(@I, @A, @D, @M, @S);";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@I", p.InvoiceID);
                cmd.Parameters.AddWithValue("@A", p.Amount);
                cmd.Parameters.AddWithValue("@D", p.PaymentDate);
                cmd.Parameters.AddWithValue("@M", string.IsNullOrWhiteSpace(p.Method) ? "Cash" : p.Method);
                cmd.Parameters.AddWithValue("@S", string.IsNullOrWhiteSpace(p.Status) ? "Paid" : p.Status);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // DAL
        public List<BookingFlatDisplay> GetAllBookingFlatDisplays()
        {
            const string sql = @"
SELECT 
    b.BookingID AS HeaderBookingID,
    br.BookingRoomID,
    c.IDNumber AS CustomerIDNumber,
    r.RoomNumber,
    u.FullName AS EmployeeName,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    ISNULL(br.Note, b.Notes) AS Notes,
    br.Status
FROM Bookings b
JOIN Customers c ON c.CustomerID = b.CustomerID
JOIN BookingRooms br ON br.BookingID = b.BookingID
JOIN Rooms r ON r.RoomID = br.RoomID
LEFT JOIN Users u ON u.UserID = b.CreatedBy
ORDER BY b.BookingDate DESC;";

            return QueryList(_cs, sql, null, rd => new BookingFlatDisplay
            {
                HeaderBookingID = rd.GetInt32(0),
                BookingRoomID = rd.GetInt32(1),
                CustomerIDNumber = rd.GetString(2),
                RoomNumber = rd.GetString(3),
                EmployeeName = rd.IsDBNull(4) ? "" : rd.GetString(4),
                BookingDate = rd.GetDateTime(5),
                CheckInDate = rd.IsDBNull(6) ? (DateTime?)null : rd.GetDateTime(6),
                CheckOutDate = rd.IsDBNull(7) ? (DateTime?)null : rd.GetDateTime(7),
                Notes = rd.IsDBNull(8) ? "" : rd.GetString(8),
                Status = rd.IsDBNull(9) ? "" : rd.GetString(9)
            });
        }
        public List<int> GetBookingRoomIdsByHeader(int headerBookingId)
        {
            const string sql = @"
SELECT br.BookingRoomID
FROM   BookingRooms br
WHERE  br.BookingID = @H
ORDER BY br.BookingRoomID;";

            return QueryList(_cs, sql,
                cmd => cmd.Parameters.AddWithValue("@H", headerBookingId),
                rd => rd.GetInt32(0));
        }
        public int GetBookingIdByBookingRoomId(int bookingRoomId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT BookingID FROM BookingRooms WHERE BookingRoomID=@id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = bookingRoomId;
                conn.Open();
                var o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? 0 : Convert.ToInt32(o);
            }
        }
        // --------------- Tiện ích khác ---------------
        public List<string> GetPaymentMethods()
        {
            const string sql = "SELECT Value FROM PaymentMethodEnum;";
            return QueryList(_cs, sql, null, rd => rd.GetString(0));
        }

        public string GetUserFullNameById(int userId)
        {
         
           return new UserRepository().GetUserById(userId)?.FullName ?? "";
        }

        public Customer GetCustomerBasicById(int customerId)
            => new CustomerRepository().GetCustomerByCustomerID(customerId);

        public string GetRoomNumberById(int roomId)
            => new RoomRepository().getRoomNumberById(roomId);
    }
}
