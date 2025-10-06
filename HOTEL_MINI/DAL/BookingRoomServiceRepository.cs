using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Response;

namespace HOTEL_MINI.DAL
{
    public class BookingRoomServiceRepository
    {
        private readonly string _cs;
        public BookingRoomServiceRepository() { _cs = ConfigHelper.GetConnectionString(); }
        private SqlConnection Conn() => new SqlConnection(_cs);

        public bool BookingRoomExists(int bookingRoomId)
        {
            const string sql = "SELECT 1 FROM BookingRooms WHERE BookingRoomID=@Id";
            using (var conn = Conn())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", bookingRoomId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o != null;
            }
        }

        public List<UsedServiceDto> GetUsedServicesByBookingRoomId(int bookingRoomId)
        {
            var list = new List<UsedServiceDto>();
            const string sql =
                "SELECT brs.BookingRoomServiceID, brs.BookingRoomID AS BookingID, s.ServiceID, s.ServiceName, s.Price, brs.Quantity " +
                "FROM BookingRoomServices brs JOIN Services s ON s.ServiceID=brs.ServiceID WHERE brs.BookingRoomID=@Id ORDER BY s.ServiceName";
            using (var conn = Conn())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", bookingRoomId);
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

        public int GetCurrentQuantity(int bookingRoomId, int serviceId)
        {
            const string sql = "SELECT ISNULL((SELECT Quantity FROM BookingRoomServices WHERE BookingRoomID=@B AND ServiceID=@S),0)";
            using (var conn = Conn())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@B", bookingRoomId);
                cmd.Parameters.AddWithValue("@S", serviceId);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            }
        }

        public int GetAvailableServiceQuantity(int bookingRoomId, int serviceId)
        {
            const string sql =
                "DECLARE @stock INT=(SELECT Quantity FROM Services WHERE ServiceID=@S); IF @stock IS NULL THROW 50001,'Service not found',1; " +
                "DECLARE @usedOther INT=ISNULL((SELECT SUM(Quantity) FROM BookingRoomServices WHERE ServiceID=@S AND BookingRoomID<>@B),0); " +
                "SELECT CASE WHEN @stock-@usedOther<0 THEN 0 ELSE @stock-@usedOther END";
            using (var conn = Conn())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@S", serviceId);
                cmd.Parameters.AddWithValue("@B", bookingRoomId);
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            }
        }

        public void AddOrIncrease(SqlConnection conn, SqlTransaction tran, int bookingRoomId, int serviceId, int addQty)
        {
            const string sql =
                "IF EXISTS(SELECT 1 FROM BookingRoomServices WITH(UPDLOCK,ROWLOCK) WHERE BookingRoomID=@B AND ServiceID=@S) " +
                "UPDATE BookingRoomServices SET Quantity=Quantity+@Q WHERE BookingRoomID=@B AND ServiceID=@S " +
                "ELSE INSERT INTO BookingRoomServices(BookingRoomID,ServiceID,Quantity) VALUES(@B,@S,@Q)";
            using (var cmd = new SqlCommand(sql, conn, tran))
            {
                cmd.Parameters.AddWithValue("@B", bookingRoomId);
                cmd.Parameters.AddWithValue("@S", serviceId);
                cmd.Parameters.AddWithValue("@Q", addQty);
                cmd.ExecuteNonQuery();
            }
        }

        public int ReduceQuantity(SqlConnection conn, SqlTransaction tran, int bookingRoomId, int serviceId, int reduceBy)
        {
            const string get = "SELECT Quantity FROM BookingRoomServices WITH(UPDLOCK,ROWLOCK) WHERE BookingRoomID=@B AND ServiceID=@S";
            int current = 0;
            using (var g = new SqlCommand(get, conn, tran))
            {
                g.Parameters.AddWithValue("@B", bookingRoomId);
                g.Parameters.AddWithValue("@S", serviceId);
                var o = g.ExecuteScalar();
                if (o == null || o == DBNull.Value) return 0;
                current = Convert.ToInt32(o);
            }

            int removed = Math.Min(current, reduceBy);
            int left = current - removed;
            if (removed <= 0) return 0;

            if (left <= 0)
            {
                const string del = "DELETE FROM BookingRoomServices WHERE BookingRoomID=@B AND ServiceID=@S";
                using (var d = new SqlCommand(del, conn, tran))
                {
                    d.Parameters.AddWithValue("@B", bookingRoomId);
                    d.Parameters.AddWithValue("@S", serviceId);
                    d.ExecuteNonQuery();
                }
            }
            else
            {
                const string upd = "UPDATE BookingRoomServices SET Quantity=@Q WHERE BookingRoomID=@B AND ServiceID=@S";
                using (var u = new SqlCommand(upd, conn, tran))
                {
                    u.Parameters.AddWithValue("@Q", left);
                    u.Parameters.AddWithValue("@B", bookingRoomId);
                    u.Parameters.AddWithValue("@S", serviceId);
                    u.ExecuteNonQuery();
                }
            }
            return removed;
        }

        public bool AddOrUpdateServiceForBooking(int bookingID, int serviceID, int quantity)
        {
            if (quantity < 0) quantity = 0;
            using (var conn = Conn())
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    int stock;
                    using (var cmd = new SqlCommand("SELECT Quantity FROM Services WHERE ServiceID=@S", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@S", serviceID);
                        var o = cmd.ExecuteScalar();
                        if (o == null || o == DBNull.Value) throw new Exception("Dịch vụ không tồn tại.");
                        stock = Convert.ToInt32(o);
                    }
                    int usedOther;
                    using (var cmd = new SqlCommand("SELECT ISNULL(SUM(Quantity),0) FROM BookingRoomServices WHERE ServiceID=@S AND BookingRoomID<>@B", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@S", serviceID);
                        cmd.Parameters.AddWithValue("@B", bookingID);
                        usedOther = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }
                    if (usedOther + quantity > stock)
                    {
                        tran.Rollback();
                        int available = Math.Max(0, stock - usedOther);
                        throw new InvalidOperationException("Vượt tồn kho. Tối đa còn: " + available);
                    }

                    int existed;
                    using (var cmd = new SqlCommand("SELECT COUNT(1) FROM BookingRoomServices WHERE BookingRoomID=@B AND ServiceID=@S", conn, tran))
                    {
                        cmd.Parameters.AddWithValue("@B", bookingID);
                        cmd.Parameters.AddWithValue("@S", serviceID);
                        existed = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
                    }

                    if (existed > 0)
                    {
                        using (var up = new SqlCommand("UPDATE BookingRoomServices SET Quantity=@Q WHERE BookingRoomID=@B AND ServiceID=@S", conn, tran))
                        {
                            up.Parameters.AddWithValue("@Q", quantity);
                            up.Parameters.AddWithValue("@B", bookingID);
                            up.Parameters.AddWithValue("@S", serviceID);
                            up.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (var ins = new SqlCommand("INSERT INTO BookingRoomServices(BookingRoomID,ServiceID,Quantity) VALUES(@B,@S,@Q)", conn, tran))
                        {
                            ins.Parameters.AddWithValue("@B", bookingID);
                            ins.Parameters.AddWithValue("@S", serviceID);
                            ins.Parameters.AddWithValue("@Q", quantity);
                            ins.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                    return true;
                }
            }
        }

        public DataTable GetServiceMenuWithStock()
        {
            var dt = new DataTable();
            const string sql = "SELECT s.ServiceID AS ID, s.ServiceName AS [Dịch vụ], s.Price AS [Đơn giá], s.Quantity AS [Tồn kho] FROM Services s ORDER BY s.ServiceName";
            using (var conn = Conn())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                new SqlDataAdapter(cmd).Fill(dt);
            }
            return dt;
        }
    }
}
