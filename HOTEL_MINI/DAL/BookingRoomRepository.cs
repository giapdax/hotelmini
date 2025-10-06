using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    public class BookingRoomRepository
    {
        private readonly string _cs;

        public BookingRoomRepository()
        {
            _cs = ConfigHelper.GetConnectionString();
        }

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
                if (onParam != null) onParam(cmd);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) list.Add(map(rd));
                }
            }
            return list;
        }

        private static T QuerySingle<T>(string cs, string sql, Action<SqlCommand> onParam, Func<SqlDataReader, T> map) where T : class
        {
            using (var conn = new SqlConnection(cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                if (onParam != null) onParam(cmd);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return map(rd);
                }
            }
        }

        private static BookingRoom MapBookingRoom(SqlDataReader rd)
        {
            return new BookingRoom
            {
                BookingRoomID = rd.GetInt32(0),
                BookingID = rd.GetInt32(1),
                RoomID = rd.GetInt32(2),
                PricingID = rd.GetInt32(3),
                CheckInDate = SafeGet<DateTime?>(rd, 4),
                CheckOutDate = SafeGet<DateTime?>(rd, 5),
                Status = SafeGet<string>(rd, 6) ?? string.Empty
            };
        }

        private static BookingDisplay MapBookingDisplay(SqlDataReader rd)
        {
            return new BookingDisplay
            {
                BookingID = rd.GetInt32(0),
                RoomNumber = SafeGet<string>(rd, 1) ?? string.Empty,
                EmployeeName = SafeGet<string>(rd, 2) ?? string.Empty,
                BookingDate = rd.GetDateTime(3),
                CheckInDate = SafeGet<DateTime?>(rd, 4),
                CheckOutDate = SafeGet<DateTime?>(rd, 5),
                Notes = SafeGet<string>(rd, 6),
                Status = SafeGet<string>(rd, 7) ?? string.Empty,
                CustomerIDNumber = SafeGet<string>(rd, 8)
            };
        }

        public bool IsRoomOverlapped(int roomId, DateTime checkIn, DateTime checkOut)
        {
            const string sql =
                @"SELECT COUNT(1)
                  FROM BookingRooms br
                  WHERE br.RoomID = @RoomID
                    AND br.Status IN ('Booked','CheckedIn')
                    AND br.CheckInDate < @To
                    AND @From < br.CheckOutDate;";
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@RoomID", SqlDbType.Int).Value = roomId;
                cmd.Parameters.Add("@From", SqlDbType.DateTime).Value = checkIn;
                cmd.Parameters.Add("@To", SqlDbType.DateTime).Value = checkOut;
                conn.Open();
                var o = cmd.ExecuteScalar();
                return Convert.ToInt32(o ?? 0) > 0;
            }
        }

        private static void RecalcRoomStatus(SqlConnection conn, SqlTransaction tran, int roomId)
        {
            bool anyIn;
            using (var c1 = new SqlCommand(
                @"SELECT TOP 1 1
                  FROM BookingRooms
                  WHERE RoomID = @R AND Status = 'CheckedIn';", conn, tran))
            {
                c1.Parameters.Add("@R", SqlDbType.Int).Value = roomId;
                anyIn = Convert.ToInt32(c1.ExecuteScalar() ?? 0) == 1;
            }

            bool anyBooked = false;
            if (!anyIn)
            {
                using (var c2 = new SqlCommand(
                    @"SELECT TOP 1 1
                      FROM BookingRooms
                      WHERE RoomID = @R AND Status = 'Booked';", conn, tran))
                {
                    c2.Parameters.Add("@R", SqlDbType.Int).Value = roomId;
                    anyBooked = Convert.ToInt32(c2.ExecuteScalar() ?? 0) == 1;
                }
            }

            var status = anyIn ? "Occupied" : (anyBooked ? "Booked" : "Available");

            using (var upd = new SqlCommand(
                @"UPDATE Rooms
                  SET Status = @S
                  WHERE RoomID = @R;", conn, tran))
            {
                upd.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = status;
                upd.Parameters.Add("@R", SqlDbType.Int).Value = roomId;
                upd.ExecuteNonQuery();
            }
        }

        public List<BookingDisplay> GetActiveBookingDisplays()
        {
            const string sql =
                @"SELECT
                     br.BookingRoomID         AS BookingID,
                     r.RoomNumber,
                     u.FullName               AS EmployeeName,
                     b.BookingDate,
                     br.CheckInDate,
                     br.CheckOutDate,
                     ISNULL(br.Note, b.Notes) AS Notes,
                     br.Status,
                     c.IDNumber               AS CustomerIDNumber
                  FROM BookingRooms br
                  JOIN Bookings  b ON b.BookingID  = br.BookingID
                  JOIN Rooms     r ON r.RoomID     = br.RoomID
                  LEFT JOIN Users u ON u.UserID    = b.CreatedBy
                  JOIN Customers c ON c.CustomerID = b.CustomerID
                  WHERE br.Status IN ('Booked','CheckedIn')
                  ORDER BY b.BookingDate DESC,
                           ISNULL(br.CheckInDate, b.BookingDate) DESC;";
            return QueryList(_cs, sql, null, MapBookingDisplay);
        }

        public List<BookingDisplay> GetTop20LatestBookingDisplays()
        {
            const string sql =
                @"SELECT TOP 20
                     br.BookingRoomID         AS BookingID,
                     r.RoomNumber,
                     u.FullName               AS EmployeeName,
                     b.BookingDate,
                     br.CheckInDate,
                     br.CheckOutDate,
                     ISNULL(br.Note, b.Notes) AS Notes,
                     br.Status,
                     c.IDNumber               AS CustomerIDNumber
                  FROM BookingRooms br
                  JOIN Bookings  b ON b.BookingID  = br.BookingID
                  JOIN Rooms     r ON r.RoomID     = br.RoomID
                  LEFT JOIN Users u ON u.UserID    = b.CreatedBy
                  JOIN Customers c ON c.CustomerID = b.CustomerID
                  WHERE br.Status = 'CheckedOut'
                  ORDER BY br.CheckOutDate DESC, b.BookingDate DESC;";
            return QueryList(_cs, sql, null, MapBookingDisplay);
        }

        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string idNumber)
        {
            const string sql =
                @"SELECT
                     br.BookingRoomID         AS BookingID,
                     r.RoomNumber,
                     u.FullName               AS EmployeeName,
                     b.BookingDate,
                     br.CheckInDate,
                     br.CheckOutDate,
                     ISNULL(br.Note, b.Notes) AS Notes,
                     br.Status,
                     c.IDNumber               AS CustomerIDNumber
                  FROM Customers c
                  JOIN Bookings b      ON b.CustomerID = c.CustomerID
                  JOIN BookingRooms br ON br.BookingID = b.BookingID
                  JOIN Rooms r         ON r.RoomID     = br.RoomID
                  LEFT JOIN Users u    ON u.UserID     = b.CreatedBy
                  WHERE c.IDNumber = @N
                  ORDER BY b.BookingDate DESC,
                           ISNULL(br.CheckInDate, b.BookingDate) DESC;";
            return QueryList(_cs, sql,
                cmd => cmd.Parameters.Add("@N", SqlDbType.NVarChar, 50).Value = idNumber,
                MapBookingDisplay);
        }

        public List<BookingRoom> GetBookingsByCustomerNumber(string idNumber)
        {
            const string sql =
                @"SELECT
                     br.BookingRoomID, br.BookingID, br.RoomID, br.PricingID,
                     br.CheckInDate, br.CheckOutDate, br.Status
                  FROM Customers c
                  JOIN Bookings b      ON b.CustomerID = c.CustomerID
                  JOIN BookingRooms br ON br.BookingID = b.BookingID
                  WHERE c.IDNumber = @ID
                  ORDER BY b.BookingDate DESC,
                           ISNULL(br.CheckInDate, b.BookingDate) DESC;";
            return QueryList(_cs, sql,
                cmd => cmd.Parameters.Add("@ID", SqlDbType.NVarChar, 50).Value = idNumber,
                MapBookingRoom);
        }

        public BookingRoom GetLatestBookingByRoomId(int roomId)
        {
            const string sql =
                @"SELECT TOP 1
                     br.BookingRoomID, br.BookingID, br.RoomID, br.PricingID,
                     br.CheckInDate, br.CheckOutDate, br.Status
                  FROM BookingRooms br
                  JOIN Bookings b ON b.BookingID = br.BookingID
                  WHERE br.RoomID = @R
                  ORDER BY b.BookingDate DESC,
                           ISNULL(br.CheckInDate, b.BookingDate) DESC;";
            return QuerySingle(_cs, sql,
                cmd => cmd.Parameters.Add("@R", SqlDbType.Int).Value = roomId,
                MapBookingRoom);
        }

        public BookingRoom GetActiveBookingByRoomId(int roomId)
        {
            const string sql =
                @"SELECT TOP 1
                     br.BookingRoomID, br.BookingID, br.RoomID, br.PricingID,
                     br.CheckInDate, br.CheckOutDate, br.Status
                  FROM BookingRooms br
                  JOIN Bookings b ON b.BookingID = br.BookingID
                  WHERE br.RoomID = @R
                    AND br.Status IN ('Booked','CheckedIn')
                  ORDER BY ISNULL(br.CheckInDate, b.BookingDate) DESC;";
            return QuerySingle(_cs, sql,
                cmd => cmd.Parameters.Add("@R", SqlDbType.Int).Value = roomId,
                MapBookingRoom);
        }

        public BookingRoom GetBookingById(int bookingRoomId)
        {
            const string sql =
                @"SELECT BookingRoomID, BookingID, RoomID, PricingID,
                         CheckInDate, CheckOutDate, Status
                  FROM BookingRooms
                  WHERE BookingRoomID = @Id;";
            return QuerySingle(_cs, sql,
                cmd => cmd.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId,
                MapBookingRoom);
        }

        public BookingRoom GetBookingRoomById(int bookingRoomId)
        {
            return GetBookingById(bookingRoomId);
        }

        public List<BookingRoom> GetBookingsByHeaderId(int headerBookingId)
        {
            const string sql =
                @"SELECT BookingRoomID, BookingID, RoomID, PricingID,
                         CheckInDate, CheckOutDate, Status
                  FROM BookingRooms
                  WHERE BookingID = @H
                  ORDER BY ISNULL(CheckInDate, GETDATE()) DESC;";
            return QueryList(_cs, sql,
                cmd => cmd.Parameters.Add("@H", SqlDbType.Int).Value = headerBookingId,
                MapBookingRoom);
        }

        public int AddLineForHeader(int headerBookingId, BookingRoom line)
        {
            const string sql =
                @"INSERT INTO BookingRooms
                  (BookingID, RoomID, PricingID, CheckInDate, CheckOutDate, Status, Note)
                  VALUES (@BID, @RID, @PID, @CI, @CO, @S, @N);
                  SELECT CAST(SCOPE_IDENTITY() AS INT);";
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    int newId;
                    using (var cmd = new SqlCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add("@BID", SqlDbType.Int).Value = headerBookingId;
                        cmd.Parameters.Add("@RID", SqlDbType.Int).Value = line.RoomID;
                        cmd.Parameters.Add("@PID", SqlDbType.Int).Value = line.PricingID;
                        cmd.Parameters.Add("@CI", SqlDbType.DateTime).Value = (object)line.CheckInDate ?? DBNull.Value;
                        cmd.Parameters.Add("@CO", SqlDbType.DateTime).Value = (object)line.CheckOutDate ?? DBNull.Value;
                        cmd.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(line.Status) ? "Booked" : line.Status.Trim();
                        cmd.Parameters.Add("@N", SqlDbType.NVarChar, -1).Value = DBNull.Value;
                        newId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    RecalcRoomStatus(conn, tran, line.RoomID);
                    tran.Commit();
                    return newId;
                }
            }
        }

        // Wrapper tự mở kết nối + transaction rồi gọi hàm chính bên dưới
        public Dictionary<int, int> AddLinesForHeader(int headerBookingId, List<BookingRoom> lines)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    try
                    {
                        var map = AddLinesForHeader(conn, tran, headerBookingId, lines);
                        tran.Commit();
                        return map;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                }
            }
        }

        // Hàm thực thi chính: dùng SCOPE_IDENTITY() (không dùng OUTPUT) => an toàn với trigger
        public Dictionary<int, int> AddLinesForHeader(SqlConnection conn, SqlTransaction tran, int headerId, List<BookingRoom> lines)
        {
            var map = new Dictionary<int, int>();
            const string sql =
                @"INSERT INTO BookingRooms
                  (BookingID, RoomID, PricingID, CheckInDate, CheckOutDate, Status, Note)
                  VALUES (@BID, @RID, @PID, @CI, @CO, @S, @N);
                  SELECT CAST(SCOPE_IDENTITY() AS INT);";

            foreach (var b in lines)
            {
                int id;
                using (var cmd = new SqlCommand(sql, conn, tran))
                {
                    cmd.Parameters.Add("@BID", SqlDbType.Int).Value = headerId;
                    cmd.Parameters.Add("@RID", SqlDbType.Int).Value = b.RoomID;
                    cmd.Parameters.Add("@PID", SqlDbType.Int).Value = b.PricingID;
                    cmd.Parameters.Add("@CI", SqlDbType.DateTime).Value = (object)b.CheckInDate ?? DBNull.Value;
                    cmd.Parameters.Add("@CO", SqlDbType.DateTime).Value = (object)b.CheckOutDate ?? DBNull.Value;
                    cmd.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(b.Status) ? "Booked" : b.Status.Trim();
                    cmd.Parameters.Add("@N", SqlDbType.NVarChar, -1).Value = DBNull.Value;
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                RecalcRoomStatus(conn, tran, b.RoomID);
                map[b.RoomID] = id;
            }

            return map;
        }

        public bool Update(BookingRoom booking)
        {
            const string sql =
                @"UPDATE BookingRooms
                  SET Status = @S, CheckOutDate = @CO, Note = @N
                  WHERE BookingRoomID = @Id;";
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                int roomId;
                using (var getR = new SqlCommand("SELECT RoomID FROM BookingRooms WHERE BookingRoomID = @Id;", conn))
                {
                    getR.Parameters.Add("@Id", SqlDbType.Int).Value = booking.BookingRoomID;
                    var o = getR.ExecuteScalar();
                    if (o == null || o == DBNull.Value) return false;
                    roomId = Convert.ToInt32(o);
                }

                using (var tran = conn.BeginTransaction())
                {
                    using (var cmd = new SqlCommand(sql, conn, tran))
                    {
                        cmd.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = string.IsNullOrWhiteSpace(booking.Status) ? "Booked" : booking.Status.Trim();
                        cmd.Parameters.Add("@CO", SqlDbType.DateTime).Value = (object)booking.CheckOutDate ?? DBNull.Value;
                        cmd.Parameters.Add("@N", SqlDbType.NVarChar, -1).Value = DBNull.Value;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = booking.BookingRoomID;
                        if (cmd.ExecuteNonQuery() <= 0) { tran.Rollback(); return false; }
                    }
                    RecalcRoomStatus(conn, tran, roomId);
                    tran.Commit();
                }
                return true;
            }
        }

        public bool UpdateBookingStatusAndCheckOut(int bookingRoomId, string status, DateTime checkoutDate)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                int roomId;
                using (var g = new SqlCommand("SELECT RoomID FROM BookingRooms WHERE BookingRoomID = @Id;", conn))
                {
                    g.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId;
                    var o = g.ExecuteScalar();
                    if (o == null || o == DBNull.Value) return false;
                    roomId = Convert.ToInt32(o);
                }

                using (var tran = conn.BeginTransaction())
                {
                    using (var cmd = new SqlCommand(
                        @"UPDATE BookingRooms
                          SET Status = @S, CheckOutDate = @CO
                          WHERE BookingRoomID = @Id;", conn, tran))
                    {
                        cmd.Parameters.Add("@S", SqlDbType.VarChar, 20).Value = status;
                        cmd.Parameters.Add("@CO", SqlDbType.DateTime).Value = checkoutDate;
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId;
                        if (cmd.ExecuteNonQuery() <= 0) { tran.Rollback(); return false; }
                    }
                    RecalcRoomStatus(conn, tran, roomId);
                    tran.Commit();
                }
                return true;
            }
        }

        public bool CancelBooking(int bookingRoomId)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                int roomId;
                using (var g = new SqlCommand("SELECT RoomID FROM BookingRooms WHERE BookingRoomID = @Id;", conn))
                {
                    g.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId;
                    var o = g.ExecuteScalar();
                    if (o == null || o == DBNull.Value) return false;
                    roomId = Convert.ToInt32(o);
                }

                using (var tran = conn.BeginTransaction())
                {
                    using (var cmd = new SqlCommand(
                        @"UPDATE BookingRooms
                          SET Status = 'Cancelled'
                          WHERE BookingRoomID = @Id;", conn, tran))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId;
                        if (cmd.ExecuteNonQuery() <= 0) { tran.Rollback(); return false; }
                    }
                    RecalcRoomStatus(conn, tran, roomId);
                    tran.Commit();
                }
                return true;
            }
        }

        public bool CheckInBooking(int bookingRoomId, DateTime checkInAt)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                int roomId;
                using (var g = new SqlCommand("SELECT RoomID FROM BookingRooms WHERE BookingRoomID = @Id;", conn))
                {
                    g.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId;
                    var o = g.ExecuteScalar();
                    if (o == null || o == DBNull.Value) return false;
                    roomId = Convert.ToInt32(o);
                }

                using (var tran = conn.BeginTransaction())
                {
                    using (var cmd = new SqlCommand(
                        @"UPDATE br
                          SET br.Status = 'CheckedIn',
                              br.CheckInDate = ISNULL(br.CheckInDate, @CI)
                          FROM BookingRooms br
                          WHERE br.BookingRoomID = @Id AND br.Status = 'Booked';", conn, tran))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId;
                        cmd.Parameters.Add("@CI", SqlDbType.DateTime).Value = checkInAt;
                        if (cmd.ExecuteNonQuery() <= 0) { tran.Rollback(); return false; }
                    }
                    RecalcRoomStatus(conn, tran, roomId);
                    tran.Commit();
                }
                return true;
            }
        }

        public bool SetLinesCheckedOutByHeader(int headerBookingId, DateTime checkoutAt)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    int affected;
                    using (var cmd = new SqlCommand(
                        @"UPDATE br
                          SET br.Status = 'CheckedOut',
                              br.CheckOutDate = ISNULL(br.CheckOutDate, @CO)
                          FROM BookingRooms br
                          WHERE br.BookingID = @H
                            AND br.Status IN ('Booked','CheckedIn');", conn, tran))
                    {
                        cmd.Parameters.Add("@H", SqlDbType.Int).Value = headerBookingId;
                        cmd.Parameters.Add("@CO", SqlDbType.DateTime).Value = checkoutAt;
                        affected = cmd.ExecuteNonQuery();
                    }

                    using (var cmd2 = new SqlCommand(
                        @"UPDATE r
                          SET r.Status = 'Available'
                          FROM Rooms r
                          WHERE r.RoomID IN (
                            SELECT DISTINCT br.RoomID
                            FROM BookingRooms br
                            WHERE br.BookingID = @H
                          )
                          AND NOT EXISTS (
                            SELECT 1
                            FROM BookingRooms x
                            WHERE x.RoomID = r.RoomID
                              AND x.Status IN ('Booked','CheckedIn')
                          );", conn, tran))
                    {
                        cmd2.Parameters.Add("@H", SqlDbType.Int).Value = headerBookingId;
                        cmd2.ExecuteNonQuery();
                    }

                    tran.Commit();
                    return affected > 0;
                }
            }
        }

        public int? GetHeaderIdByBookingRoomId(int bookingRoomId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT BookingID FROM BookingRooms WHERE BookingRoomID = @Id;", conn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = bookingRoomId;
                conn.Open();
                var o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? (int?)null : Convert.ToInt32(o);
            }
        }

        public string GetRoomNumberById(int roomId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT RoomNumber FROM Rooms WHERE RoomID = @Id;", conn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = roomId;
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null ? string.Empty : o.ToString();
            }
        }
    }
}
