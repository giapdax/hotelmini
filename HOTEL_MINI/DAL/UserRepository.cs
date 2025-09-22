using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;

namespace HOTEL_MINI.DAL
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigHelper.GetConnectionString();
        }

        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        public User GetUserByUsername(string username)
        {
            const string sql = @"SELECT UserID, Username, PasswordHash, RoleID, FullName, Phone, Email, Status
                                 FROM Users 
                                 WHERE Username = @Username";
            try
            {
                using (var _connection = CreateConnection())
                {
                    _connection.Open();

                    using (var command = new SqlCommand(sql, _connection))
                    {
                        command.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = username;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var statusStr = reader.GetString(reader.GetOrdinal("Status"));
                                var status = UserStatus.Active;
                                Enum.TryParse(statusStr, true, out status);

                                return new User
                                {
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    Username = reader.GetString(reader.GetOrdinal("Username")),
                                    FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FullName")),
                                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                                    Role = reader.GetInt32(reader.GetOrdinal("RoleID")),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? string.Empty : reader.GetString(reader.GetOrdinal("Phone")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                    Status = status
                                };
                            }
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
        }

        public User GetUserById(int userId)
        {
            const string sql = @"SELECT UserID, Username, PasswordHash, RoleID, FullName, Phone, Email, Status
                                 FROM Users 
                                 WHERE UserID = @UserID";
            try
            {
                using (var _connection = CreateConnection())
                {
                    _connection.Open();

                    using (var command = new SqlCommand(sql, _connection))
                    {
                        command.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var statusStr = reader.GetString(reader.GetOrdinal("Status"));
                                var status = UserStatus.Active;
                                Enum.TryParse(statusStr, true, out status);

                                return new User
                                {
                                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                                    Username = reader.GetString(reader.GetOrdinal("Username")),
                                    FullName = reader.IsDBNull(reader.GetOrdinal("FullName")) ? string.Empty : reader.GetString(reader.GetOrdinal("FullName")),
                                    PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                                    Role = reader.GetInt32(reader.GetOrdinal("RoleID")),
                                    Phone = reader.IsDBNull(reader.GetOrdinal("Phone")) ? string.Empty : reader.GetString(reader.GetOrdinal("Phone")),
                                    Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? string.Empty : reader.GetString(reader.GetOrdinal("Email")),
                                    Status = status
                                };
                            }
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
        }

        public List<User> GetAllUsers()
        {
            var userList = new List<User>();
            const string sql = @"SELECT UserID, Username, FullName, Phone, Email, RoleID, Status FROM Users";

            try
            {
                using (var _connection = CreateConnection())
                {
                    _connection.Open();

                    using (var command = new SqlCommand(sql, _connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string statusString = reader.GetString(reader.GetOrdinal("Status"));
                            UserStatus userStatus;
                            Enum.TryParse(statusString, true, out userStatus);

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

            return userList;
        }

        // hàm tạo admin nếu chưa có 
        public void CreateAdminUserIfNotExist()
        {
            const string sql = @"
                DECLARE @RoleId INT;

                SELECT @RoleId = RoleID FROM Roles WHERE RoleName = N'admin';
                IF @RoleId IS NULL
                BEGIN
                    INSERT INTO Roles(RoleName, Description)
                    VALUES (N'admin', N'Quyền quản trị');
                    SET @RoleId = SCOPE_IDENTITY();
                END

                IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
                BEGIN
                    INSERT INTO Users (Username, PasswordHash, RoleID, FullName, Status)
                    VALUES (@Username, @PasswordHash, @RoleId, @FullName, @Status);
                    SELECT CAST(1 AS INT) AS CreatedFlag;
                END
                ELSE
                BEGIN
                    SELECT CAST(0 AS INT) AS CreatedFlag;
                END";

            try
            {
                using (var _connection = CreateConnection())
                {
                    _connection.Open();

                    using (var tran = _connection.BeginTransaction(IsolationLevel.Serializable))
                    using (var cmd = new SqlCommand(sql, _connection, tran))
                    {
                        string passwordHash = BCrypt.Net.BCrypt.HashPassword("Matkhau1234", workFactor: 12);

                        cmd.Parameters.Add("@Username", SqlDbType.VarChar, 50).Value = "admin";
                        cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 255).Value = passwordHash;
                        cmd.Parameters.Add("@FullName", SqlDbType.NVarChar, 100).Value = "Administrator";
                        cmd.Parameters.Add("@Status", SqlDbType.VarChar, 20).Value = "Active";

                        object result = cmd.ExecuteScalar();
                        tran.Commit();

                        int created = (result is int i) ? i : Convert.ToInt32(result);
                        if (created == 1)
                            MessageBox.Show("Đã tạo user 'admin'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo user admin: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool AddUser(User user)
        {
            const string query = @"INSERT INTO Users (Username, PasswordHash, RoleID, FullName, Phone, Email, Status) 
                                   VALUES (@Username, @PasswordHash, @RoleID, @FullName, @Phone, @Email, @Status)";
            try
            {
                using (var _connection = CreateConnection())
                {
                    _connection.Open();

                    using (var command = new SqlCommand(query, _connection))
                    {
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                        command.Parameters.AddWithValue("@RoleID", user.Role);
                        command.Parameters.AddWithValue("@FullName", (object)user.FullName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", (object)user.Phone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)user.Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Status", user.Status.ToString());
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm người dùng: {ex.Message}");
                return false;
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

            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                query += ", PasswordHash = @PasswordHash";
            }

            query += " WHERE UserID = @UserID";

            try
            {
                using (var _connection = CreateConnection())
                {
                    _connection.Open();

                    using (var command = new SqlCommand(query, _connection))
                    {
                        command.Parameters.AddWithValue("@UserID", user.UserID);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@RoleID", user.Role);
                        command.Parameters.AddWithValue("@FullName", (object)user.FullName ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Phone", (object)user.Phone ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Email", (object)user.Email ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Status", user.Status.ToString());

                        if (!string.IsNullOrWhiteSpace(user.PasswordHash))
                            command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);

                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật người dùng: {ex.Message}");
                return false;
            }
        }

        public bool UpdatePassword(int userId, string newPasswordHash)
        {
            const string sql = @"UPDATE Users SET PasswordHash = @PasswordHash WHERE UserID = @UserID";
            try
            {
                using (var _connection = CreateConnection())
                {
                    _connection.Open();

                    using (var cmd = new SqlCommand(sql, _connection))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userId);
                        cmd.Parameters.AddWithValue("@PasswordHash", newPasswordHash);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi update password: {ex.Message}");
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            const string query = "DELETE FROM Users WHERE UserID = @UserID";
            try
            {
                using (var _connection = CreateConnection())
                {
                    _connection.Open();

                    using (var command = new SqlCommand(query, _connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userId);
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa người dùng: {ex.Message}");
                return false;
            }
        }
    }
}
