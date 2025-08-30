using System;
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

        public User GetUserByUsername(string username)
        {
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                const string sql = @"SELECT UserID, Username, PasswordHash, RoleID, Fullname, Phone, Email, Status
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
                                FullName = reader.IsDBNull(reader.GetOrdinal("Fullname")) ? string.Empty : reader.GetString(reader.GetOrdinal("Fullname")),
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