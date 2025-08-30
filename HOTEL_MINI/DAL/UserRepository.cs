using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using BCrypt.Net;

namespace HOTEL_MINI.DAL
{
    public class UserRepository : IDisposable
    {
        private readonly SqlConnection _connection;
        private bool _disposed = false;

        public UserRepository()
        {
            _connection = new SqlConnection(ConfigHelper.GetConnectionString());
        }

        // Phương thức Lấy thông tin một người dùng theo Username (cần có cho chức năng đăng nhập)
        public User GetUserByUsername(string username)
        {
            // Code phương thức này không thay đổi, vẫn truy vấn PasswordHash để xác thực
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                const string sql = @"SELECT UserID, Username, PasswordHash, RoleID, FullName, Phone, Email, Status
                                     FROM Users 
                                     WHERE Username = @Username";

                using (var command = new SqlCommand(sql, _connection))
                {
                    command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FullName")),
                                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                                Role = reader.GetInt32(reader.GetOrdinal("RoleID")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? string.Empty : reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Status = reader.GetString(reader.GetOrdinal("Status"))
                            };
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }

        // 🌟 Phương thức Lấy tất cả người dùng (không lấy PasswordHash)
        public List<User> GetAllUsers()
        {
            List<User> userList = new List<User>();
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                // Lấy tất cả cột ngoại trừ PasswordHash
                const string sql = @"SELECT UserID, Username, FullName, Phone, Email, RoleID, Status FROM Users";

                using (var command = new SqlCommand(sql, _connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            userList.Add(new User
                            {
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FullName")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? string.Empty : reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.GetInt32(reader.GetOrdinal("RoleID")),
                                Status = reader.GetString(reader.GetOrdinal("Status"))
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi truy vấn cơ sở dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
            return userList;
        }

        // Phương thức tạo người dùng admin (giữ nguyên)
        public void CreateAdminUserIfNotExist()
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                const string checkSql = "SELECT COUNT(*) FROM Users WHERE Username = @Username";
                using (var checkCommand = new SqlCommand(checkSql, _connection))
                {
                    checkCommand.Parameters.AddWithValue("@Username", "admin");
                    int userCount = (int)checkCommand.ExecuteScalar();

                    if (userCount == 0)
                    {
                        const string insertSql = @"INSERT INTO Users (Username, PasswordHash, RoleID, FullName, Status) 
                                                   VALUES (@Username, @PasswordHash, @RoleID, @FullName, @Status)";

                        using (var insertCommand = new SqlCommand(insertSql, _connection))
                        {
                            string passwordHash = BCrypt.Net.BCrypt.HashPassword("123456");

                            insertCommand.Parameters.AddWithValue("@Username", "admin");
                            insertCommand.Parameters.AddWithValue("@PasswordHash", passwordHash);
                            insertCommand.Parameters.AddWithValue("@RoleID", 1);
                            insertCommand.Parameters.AddWithValue("@FullName", "Administrator");
                            insertCommand.Parameters.AddWithValue("@Status", "Active");

                            insertCommand.ExecuteNonQuery();
                            MessageBox.Show("Người dùng 'admin' đã được tạo thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo user admin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
        }

        // Cần giữ lại các phương thức này để tránh lỗi
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_connection != null)
                    {
                        if (_connection.State == ConnectionState.Open)
                            _connection.Close();
                        _connection.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        ~UserRepository()
        {
            Dispose(false);
        }
    }
}