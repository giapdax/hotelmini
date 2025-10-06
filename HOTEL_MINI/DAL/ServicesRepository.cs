using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    public class ServicesRepository
    {
        private readonly string _cs;
        public ServicesRepository() { _cs = ConfigHelper.GetConnectionString(); }
        private SqlConnection Conn() => new SqlConnection(_cs);

        public List<Service> GetAllServices()
        {
            var list = new List<Service>();
            const string sql = "SELECT ServiceID, ServiceName, Price, IsActive, Quantity FROM Services";
            using (var conn = Conn())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Service
                        {
                            ServiceID = rd.GetInt32(0),
                            ServiceName = rd.GetString(1),
                            Price = rd.GetDecimal(2),
                            IsActive = rd.GetBoolean(3),
                            Quantity = rd.IsDBNull(4) ? 0 : rd.GetInt32(4)
                        });
                    }
                }
            }
            return list;
        }

        public bool AddService(Service s)
        {
            const string sql = "INSERT INTO Services(ServiceName, Price, IsActive, Quantity) VALUES (@Name,@Price,@Active,@Qty)";
            using (var conn = Conn())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = s.ServiceName ?? "";
                cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = s.Price;
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = s.IsActive;
                cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = s.Quantity;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateService(Service s)
        {
            const string sql = "UPDATE Services SET ServiceName=@Name, Price=@Price, IsActive=@Active, Quantity=@Qty WHERE ServiceID=@Id";
            using (var conn = Conn())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = s.ServiceID;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = s.ServiceName ?? "";
                cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = s.Price;
                cmd.Parameters.Add("@Active", SqlDbType.Bit).Value = s.IsActive;
                cmd.Parameters.Add("@Qty", SqlDbType.Int).Value = s.Quantity;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteService(int serviceId)
        {
            const string sql = "DELETE FROM Services WHERE ServiceID=@Id";
            using (var conn = Conn())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = serviceId;
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int GetQuantity(int serviceId)
        {
            const string sql = "SELECT Quantity FROM Services WHERE ServiceID=@Id";
            using (var conn = Conn())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = serviceId;
                conn.Open();
                var o = cmd.ExecuteScalar();
                return (o == null || o == DBNull.Value) ? 0 : Convert.ToInt32(o);
            }
        }

        public bool TryReserveStock(SqlConnection extConn, SqlTransaction extTran, int serviceId, int amount)
        {
            if (amount <= 0) throw new ArgumentException("amount phải > 0", nameof(amount));
            const string sql = "UPDATE Services SET Quantity = Quantity - @Amt WHERE ServiceID = @Id AND Quantity >= @Amt";
            using (var cmd = new SqlCommand(sql, extConn, extTran))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = serviceId;
                cmd.Parameters.Add("@Amt", SqlDbType.Int).Value = amount;
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public void ReleaseStock(SqlConnection extConn, SqlTransaction extTran, int serviceId, int amount)
        {
            if (amount <= 0) throw new ArgumentException("amount phải > 0", nameof(amount));
            const string sql = "UPDATE Services SET Quantity = Quantity + @Amt WHERE ServiceID = @Id";
            using (var cmd = new SqlCommand(sql, extConn, extTran))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = serviceId;
                cmd.Parameters.Add("@Amt", SqlDbType.Int).Value = amount;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
