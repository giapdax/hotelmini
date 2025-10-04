using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
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
            // thêm Quantity luôn cho nhất quán (giả sử cột có default 0 cũng ok)
            const string query =
                "INSERT INTO Services (ServiceName, Price, IsActive, Quantity) " +
                "VALUES (@ServiceName, @Price, @IsActive, @Quantity)";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ServiceName", service.ServiceName);
                command.Parameters.AddWithValue("@Price", service.Price);
                command.Parameters.AddWithValue("@IsActive", service.IsActive);
                command.Parameters.AddWithValue("@Quantity", service.Quantity);

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
                command.Parameters.AddWithValue("@ServiceID", service.ServiceID);
                command.Parameters.AddWithValue("@ServiceName", service.ServiceName);
                command.Parameters.AddWithValue("@Price", service.Price);
                command.Parameters.AddWithValue("@IsActive", service.IsActive);
                command.Parameters.AddWithValue("@Quantity", service.Quantity);

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
                command.Parameters.AddWithValue("@ServiceID", serviceId);
                command.Parameters.AddWithValue("@Quantity", quantity);

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
                command.Parameters.AddWithValue("@ServiceID", serviceId);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public int GetQuantity(int serviceId)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "SELECT Quantity FROM Services WHERE ServiceID=@id", conn))
            {
                cmd.Parameters.AddWithValue("@id", serviceId);
                conn.Open();
                return (int)(cmd.ExecuteScalar() ?? 0);
            }
        }
    }
}
