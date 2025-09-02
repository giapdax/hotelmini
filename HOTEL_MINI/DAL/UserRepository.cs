using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using BCrypt.Net;
using HOTEL_MINI.BLL;

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
                                Status = (UserStatus)Enum.Parse(typeof(UserStatus), reader.GetString(reader.GetOrdinal("Status")))
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
                            string statusString = reader.GetString(reader.GetOrdinal("Status"));
                            // Chuyển đổi chuỗi thành enum
                            UserStatus userStatus;
                            Enum.TryParse(statusString, out userStatus);

                            userList.Add(new User
                            {
                                UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FullName")),
                                Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? string.Empty : reader.GetString(reader.GetOrdinal("Phone")),
                                Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                Role = reader.GetInt32(reader.GetOrdinal("RoleID")),
                                Status = userStatus
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

        public bool AddUser(User user)
        {
            // Hoàn chỉnh câu lệnh SQL, liệt kê đầy đủ các cột và các tham số
            string query = @"INSERT INTO Users (
                        Username, 
                        PasswordHash, 
                        RoleID, 
                        FullName, 
                        Phone, 
                        Email, 
                        Status
                    )
                    VALUES (
                        @Username, 
                        @PasswordHash, 
                        @RoleID, 
                        @FullName, 
                        @Phone, 
                        @Email, 
                        @Status
                    )";

            // Sử dụng try-catch-finally để đảm bảo tài nguyên được giải phóng
            try
            {
                // Kiểm tra kết nối và mở nếu cần
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                using (var command = new SqlCommand(query, _connection))
                {
                    // Thêm các tham số từ đối tượng User
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    command.Parameters.AddWithValue("@RoleID", user.Role);

                    // Sử dụng DBNull.Value cho các trường có thể null để tránh lỗi
                    command.Parameters.AddWithValue("@FullName", (object)user.FullName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", (object)user.Phone ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Email", (object)user.Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Status", user.Status.ToString());

                    // ExecuteNonQuery trả về số hàng bị ảnh hưởng
                    int result = command.ExecuteNonQuery();

                    // Trả về true nếu có ít nhất 1 hàng được thêm thành công
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi để debug thay vì hiển thị MessageBox
                // MessageBox.Show() không thuộc về tầng DAL.
                Console.WriteLine($"Lỗi khi thêm người dùng: {ex.Message}");

                // Trả về false khi có bất kỳ lỗi nào xảy ra
                return false;
            }
            finally
            {
                // Đảm bảo kết nối luôn được đóng
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
        public bool DeleteUser(int UserId)
        {
            string query = "DELETE FROM Users WHERE UserID = @UserID ";
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                using (var command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@UserID", UserId);
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa người dùng: {ex.Message}");
                return false;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
        public bool UpdateUser(User user)
        {
            string query = @"UPDATE Users SET
                      Username = @Username,
                      FullName = @FullName,
                      Phone = @Phone,
                      Email = @Email,
                      RoleID = @RoleID,
                      Status = @Status";

            // Thêm PasswordHash vào câu lệnh chỉ khi có giá trị
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                query += ", PasswordHash = @PasswordHash";
            }

            query += " WHERE UserID = @UserID";
            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                using (var command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@UserID", user.UserID);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@RoleID", user.Role);
                    command.Parameters.AddWithValue("@FullName", (object)user.FullName ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", (object)user.Phone ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Email", (object)user.Email ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Status", user.Status.ToString());

                    // 🌟 Chỉ thêm tham số PasswordHash nếu nó có giá trị
                    if (!string.IsNullOrWhiteSpace(user.PasswordHash))
                    {
                        command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    }

                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật người dùng: {ex.Message}");
                return false;
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
            }
        }
        ~UserRepository()
        {
            Dispose(false);
        }
    }
}