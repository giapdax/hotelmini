// using ...

using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    // Không cần IDisposable nữa vì chúng ta không giữ tài nguyên nào ở cấp độ class
    internal class ServicesRepository
    {
        private readonly string _connectionString;

        public ServicesRepository()
        {
            _connectionString = ConfigHelper.GetConnectionString();
        }

        // Helper để tạo kết nối, giúp code gọn hơn
        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        public List<Service> GetAllServices()
        {
            var services = new List<Service>();
            const string query = "SELECT ServiceID, ServiceName, Price, IsActive FROM Services";

            // "using" đảm bảo kết nối được tạo, mở, và đóng/giải phóng một cách an toàn
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
                                IsActive = reader.GetBoolean(3)
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Ghi log và ném lại exception để lớp BLL/UI có thể xử lý
                    Console.WriteLine($"Error in GetAllServices: {ex.Message}");
                    throw;
                }
            }
            return services;
        }

        public bool AddService(Service service)
        {
            const string query = "INSERT INTO Services (ServiceName, Price, IsActive) VALUES (@ServiceName, @Price, @IsActive)";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ServiceName", service.ServiceName);
                command.Parameters.AddWithValue("@Price", service.Price);
                command.Parameters.AddWithValue("@IsActive", service.IsActive);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in AddService: {ex.Message}");
                    // Ném lại lỗi hoặc trả về false tùy theo logic bạn muốn
                    return false;
                }
            }
        }

        public bool UpdateService(Service service)
        {
            const string query = "UPDATE Services SET ServiceName = @ServiceName, Price = @Price, IsActive = @IsActive WHERE ServiceID = @ServiceID";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ServiceID", service.ServiceID);
                command.Parameters.AddWithValue("@ServiceName", service.ServiceName);
                command.Parameters.AddWithValue("@Price", service.Price);
                command.Parameters.AddWithValue("@IsActive", service.IsActive);

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