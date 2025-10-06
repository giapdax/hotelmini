using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    public class BookingRepository
    {
        private readonly string _cs;

        public BookingRepository()
        {
            _cs = ConfigHelper.GetConnectionString();
        }

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
                    while (rd.Read()) list.Add(map(rd));
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
                    return rd.Read() ? map(rd) : null;
            }
        }

        private static Booking MapHeader(SqlDataReader rd) => new Booking
        {
            BookingID = rd.GetInt32(0),
            CustomerID = rd.GetInt32(1),
            CreatedBy = rd.GetInt32(2),
            BookingDate = rd.GetDateTime(3),
            Status = SafeGet<string>(rd, 4) ?? "",
            Notes = SafeGet<string>(rd, 5) ?? ""
        };

        private static BookingRoom MapLine(SqlDataReader rd) => new BookingRoom
        {
            BookingRoomID = rd.GetInt32(0),
            BookingID = rd.GetInt32(1),
            RoomID = rd.GetInt32(2),
            PricingID = rd.GetInt32(3),
            CheckInDate = rd.IsDBNull(4) ? (DateTime?)null : rd.GetDateTime(4),
            CheckOutDate = rd.IsDBNull(5) ? (DateTime?)null : rd.GetDateTime(5),
            Status = SafeGet<string>(rd, 6) ?? "",
            Note = SafeGet<string>(rd, 7) ?? ""
        };

        // ---------- AddHeader (transactional core) ----------
        public int AddHeader(SqlConnection conn, SqlTransaction tran,
                             int customerId, int createdBy, DateTime bookingDate,
                             string status, string notes)
        {
            const string sql =
                @"INSERT INTO Bookings (CustomerID, CreatedBy, BookingDate, Status, Notes)
                  VALUES (@C, @U, @D, @S, @N);
                  SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.Add("@C", SqlDbType.Int).Value = customerId;
                cmd.Parameters.Add("@U", SqlDbType.Int).Value = createdBy;
                cmd.Parameters.Add("@D", SqlDbType.DateTime).Value = bookingDate == default(DateTime) ? DateTime.Now : bookingDate;
                cmd.Parameters.Add("@S", SqlDbType.NVarChar, 50).Value = string.IsNullOrWhiteSpace(status) ? "Booked" : status;
                cmd.Parameters.Add("@N", SqlDbType.NVarChar, 255).Value = (object)notes ?? DBNull.Value;
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }


        public Customer GetCustomerByHeaderId(int headerBookingId)
        {
            const string sql =
                @"SELECT c.CustomerID, c.FullName, c.Gender, c.Phone, c.Email, c.Address, c.IDNumber
                  FROM Bookings b
                  JOIN Customers c ON c.CustomerID = b.CustomerID
                  WHERE b.BookingID = @id;";
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


        public List<BookingDisplay> GetTopLatestBookingDisplays(int top, bool onlyCheckedOut)
        {
            const string sql =
                @"SELECT TOP (@Top)
                         br.BookingRoomID AS BookingID,
                         r.RoomNumber,
                         u.FullName AS EmployeeName,
                         b.BookingDate,
                         br.CheckInDate,
                         br.CheckOutDate,
                         ISNULL(br.Note, b.Notes) AS Notes,
                         br.Status
                  FROM BookingRooms br
                  JOIN Bookings b ON b.BookingID = br.BookingID
                  JOIN Rooms r ON r.RoomID = br.RoomID
                  LEFT JOIN Users u ON u.UserID = b.CreatedBy
                  WHERE (@Only = 0 OR br.Status = 'CheckedOut')
                  ORDER BY ISNULL(br.CheckOutDate, br.CheckInDate) DESC, b.BookingDate DESC;";
            return QueryList(_cs, sql,
                cmd => { cmd.Parameters.Add("@Top", SqlDbType.Int).Value = top; cmd.Parameters.Add("@Only", SqlDbType.Bit).Value = onlyCheckedOut ? 1 : 0; },
                rd => new BookingDisplay
                {
                    BookingID = rd.GetInt32(0),
                    RoomNumber = SafeGet<string>(rd, 1) ?? "",
                    EmployeeName = SafeGet<string>(rd, 2) ?? "",
                    BookingDate = rd.IsDBNull(3) ? DateTime.MinValue : rd.GetDateTime(3),
                    CheckInDate = rd.IsDBNull(4) ? DateTime.MinValue : rd.GetDateTime(4),
                    CheckOutDate = rd.IsDBNull(5) ? DateTime.MinValue : rd.GetDateTime(5),
                    Notes = SafeGet<string>(rd, 6) ?? "",
                    Status = SafeGet<string>(rd, 7) ?? ""
                });
        }

        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string idNumber)
        {
            const string sql =
                @"SELECT br.BookingRoomID AS BookingID,
                         r.RoomNumber,
                         u.FullName AS EmployeeName,
                         b.BookingDate,
                         br.CheckInDate,
                         br.CheckOutDate,
                         ISNULL(br.Note, b.Notes) AS Notes,
                         br.Status
                  FROM Customers c
                  JOIN Bookings b ON b.CustomerID = c.CustomerID
                  JOIN BookingRooms br ON br.BookingID = b.BookingID
                  JOIN Rooms r ON r.RoomID = br.RoomID
                  LEFT JOIN Users u ON u.UserID = b.CreatedBy
                  WHERE c.IDNumber = @ID
                  ORDER BY ISNULL(br.CheckOutDate, br.CheckInDate) DESC, b.BookingDate DESC;";
            return QueryList(_cs, sql,
                cmd => cmd.Parameters.AddWithValue("@ID", idNumber),
                rd => new BookingDisplay
                {
                    BookingID = rd.GetInt32(0),
                    RoomNumber = SafeGet<string>(rd, 1) ?? "",
                    EmployeeName = SafeGet<string>(rd, 2) ?? "",
                    BookingDate = rd.IsDBNull(3) ? DateTime.MinValue : rd.GetDateTime(3),
                    CheckInDate = rd.IsDBNull(4) ? DateTime.MinValue : rd.GetDateTime(4),
                    CheckOutDate = rd.IsDBNull(5) ? DateTime.MinValue : rd.GetDateTime(5),
                    Notes = SafeGet<string>(rd, 6) ?? "",
                    Status = SafeGet<string>(rd, 7) ?? ""
                });
        }
        public List<BookingFlatDisplay> GetAllBookingFlatDisplays()
        {
            const string sql =
                @"SELECT b.BookingID AS HeaderBookingID,
                 br.BookingRoomID,
                 c.IDNumber   AS CustomerIDNumber,
                 c.FullName   AS CustomerName,   -- <-- THÊM
                 c.Phone      AS CustomerPhone,  -- <-- THÊM
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
                CustomerIDNumber = rd.IsDBNull(2) ? "" : rd.GetString(2),
                CustomerName = rd.IsDBNull(3) ? "" : rd.GetString(3), 
                CustomerPhone = rd.IsDBNull(4) ? "" : rd.GetString(4),
                RoomNumber = rd.GetString(5),
                EmployeeName = rd.IsDBNull(6) ? "" : rd.GetString(6),
                BookingDate = rd.GetDateTime(7),
                CheckInDate = rd.IsDBNull(8) ? (DateTime?)null : rd.GetDateTime(8),
                CheckOutDate = rd.IsDBNull(9) ? (DateTime?)null : rd.GetDateTime(9),
                Notes = rd.IsDBNull(10) ? "" : rd.GetString(10),
                Status = rd.IsDBNull(11) ? "" : rd.GetString(11)
            });
        }

        private static BookingRoom MapBookingRoom(SqlDataReader rd)
        {
            return new BookingRoom
            {
                BookingRoomID = rd.GetInt32(0),
                BookingID = rd.GetInt32(1),
                RoomID = rd.GetInt32(2),
                PricingID = rd.GetInt32(3),
                CheckInDate = rd.IsDBNull(4) ? (DateTime?)null : rd.GetDateTime(4),
                CheckOutDate = rd.IsDBNull(5) ? (DateTime?)null : rd.GetDateTime(5),
                Status = rd.IsDBNull(6) ? "" : rd.GetString(6)
            };
        }
        public BookingRoom GetBookingById(int bookingRoomId)
        {
            const string sql = @"SELECT BookingRoomID, BookingID, RoomID, PricingID,
                                CheckInDate, CheckOutDate, Status
                         FROM BookingRooms
                         WHERE BookingRoomID = @Id;";
            return QuerySingle(_cs, sql,
                cmd => cmd.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId,
                MapBookingRoom);
        }
        public List<int> GetBookingRoomIdsByHeader(int headerBookingId)
        {
            const string sql =
                @"SELECT br.BookingRoomID
                  FROM BookingRooms br
                  WHERE br.BookingID = @H
                  ORDER BY br.BookingRoomID;";
            return QueryList(_cs, sql, cmd => cmd.Parameters.AddWithValue("@H", headerBookingId), rd => rd.GetInt32(0));
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

        public List<string> GetPaymentMethods()
        {
            const string sql = @"SELECT Value FROM PaymentMethodEnum;";
            return QueryList(_cs, sql, null, rd => rd.GetString(0));
        }

        public string GetUserFullNameById(int userId)
            => new UserRepository().GetUserById(userId)?.FullName ?? "";

        public string GetRoomNumberById(int roomId)
            => new RoomRepository().getRoomNumberById(roomId);
    }
}
