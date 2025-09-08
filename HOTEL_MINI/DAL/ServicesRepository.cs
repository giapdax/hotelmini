// File: ServicesRepository.cs
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
            // Thêm Quantity vào câu truy vấn SELECT
            const string query = "SELECT ServiceID, ServiceName, Price, IsActive, Quantity FROM Services";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                try
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
                                // Đọc giá trị Quantity
                                Quantity = reader.GetInt32(4)
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in GetAllServices: {ex.Message}");
                    throw;
                }
            }
            return services;
        }

        public bool AddService(Service service)
        {
            // Thêm Quantity vào câu truy vấn INSERT
            const string query = "INSERT INTO Services (ServiceName, Price, IsActive, Quantity) VALUES (@ServiceName, @Price, @IsActive, @Quantity)";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ServiceName", service.ServiceName);
                command.Parameters.AddWithValue("@Price", service.Price);
                command.Parameters.AddWithValue("@IsActive", service.IsActive);
                // Thêm tham số Quantity
                command.Parameters.AddWithValue("@Quantity", service.Quantity);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in AddService: {ex.Message}");
                    return false;
                }
            }
        }

        public bool UpdateService(Service service)
        {
            // Thêm Quantity vào câu truy vấn UPDATE
            const string query = "UPDATE Services SET ServiceName = @ServiceName, Price = @Price, IsActive = @IsActive, Quantity = @Quantity WHERE ServiceID = @ServiceID";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ServiceID", service.ServiceID);
                command.Parameters.AddWithValue("@ServiceName", service.ServiceName);
                command.Parameters.AddWithValue("@Price", service.Price);
                command.Parameters.AddWithValue("@IsActive", service.IsActive);
                // Thêm tham số Quantity
                command.Parameters.AddWithValue("@Quantity", service.Quantity);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in UpdateService: {ex.Message}");
                    return false;
                }
            }
        }

        // Hàm mới để cập nhật số lượng
        public bool UpdateServiceQuantity(int serviceId, int quantity)
        {
            const string query = "UPDATE Services SET Quantity = @Quantity WHERE ServiceID = @ServiceID";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ServiceID", serviceId);
                command.Parameters.AddWithValue("@Quantity", quantity);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in UpdateServiceQuantity: {ex.Message}");
                    return false;
                }
            }
        }

        public bool DeleteService(int serviceId)
        {
            const string query = "DELETE FROM Services WHERE ServiceID = @ServiceID";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ServiceID", serviceId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in DeleteService: {ex.Message}");
                    return false;
                }
            }
        }
    }
}