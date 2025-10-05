using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    internal class ServicesRepository
    {
        private readonly string _connectionString;

        public ServicesRepository()
        {
            _connectionString = ConfigHelper.GetConnectionString();
        }

        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        public List<Service> GetAllServices()
        {
            var services = new List<Service>();
            const string query =
                "SELECT ServiceID, ServiceName, Price, IsActive, Quantity FROM Services";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        services.Add(new Service
                        {
                            ServiceID = reader.GetInt32(0),
                            ServiceName = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            IsActive = reader.GetBoolean(3),
                            Quantity = reader.IsDBNull(4) ? 0 : reader.GetInt32(4)
                        });
                    }
                }
            }
            return services;
        }

        public bool AddService(Service service)
        {
            const string query =
                "INSERT INTO Services (ServiceName, Price, IsActive, Quantity) " +
                "VALUES (@ServiceName, @Price, @IsActive, @Quantity)";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 100).Value = service.ServiceName;
                command.Parameters.Add("@Price", SqlDbType.Decimal).Value = service.Price;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = service.IsActive;
                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = service.Quantity;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateService(Service service)
        {
            const string query =
                "UPDATE Services SET ServiceName=@ServiceName, Price=@Price, " +
                "IsActive=@IsActive, Quantity=@Quantity WHERE ServiceID=@ServiceID";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = service.ServiceID;
                command.Parameters.Add("@ServiceName", SqlDbType.NVarChar, 100).Value = service.ServiceName;
                command.Parameters.Add("@Price", SqlDbType.Decimal).Value = service.Price;
                command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = service.IsActive;
                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = service.Quantity;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateServiceQuantity(int serviceId, int quantity)
        {
            const string query =
                "UPDATE Services SET Quantity=@Quantity WHERE ServiceID=@ServiceID";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = serviceId;
                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = quantity;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteService(int serviceId)
        {
            const string query = "DELETE FROM Services WHERE ServiceID=@ServiceID";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@ServiceID", SqlDbType.Int).Value = serviceId;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public int GetQuantity(int serviceId)
        {
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(
                "SELECT Quantity FROM Services WHERE ServiceID=@id", conn))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = serviceId;
                conn.Open();
                return Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            }
        }

        // ====== OPTIONAL cho UI: tồn còn lại = stock - used ======
        public int GetAvailableQuantity(int serviceId)
        {
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(@"
SELECT s.Quantity - ISNULL((SELECT SUM(Quantity) 
                            FROM BookingRoomServices 
                            WHERE ServiceID=@sid), 0)
FROM Services s WHERE s.ServiceID=@sid", conn))
            {
                cmd.Parameters.Add("@sid", SqlDbType.Int).Value = serviceId;
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null || o == DBNull.Value ? 0 : Math.Max(0, Convert.ToInt32(o));
            }
        }
    }
}
