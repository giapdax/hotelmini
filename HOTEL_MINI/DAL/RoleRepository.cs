// File: RoleRepository.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;

namespace HOTEL_MINI.DAL
{
    internal class RoleRepository : IDisposable
    {
        private readonly SqlConnection _connection;
        private bool _disposed = false;

        public RoleRepository()
        {
            _connection = new SqlConnection(ConfigHelper.GetConnectionString());
        }

        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();
            try
            {
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();

                string sql = "SELECT RoleID, RoleName FROM Roles";
                using (var command = new SqlCommand(sql, _connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role
                        {
                            RoleID = reader.GetInt32(reader.GetOrdinal("RoleID")),
                            RoleName = reader.GetString(reader.GetOrdinal("RoleName"))
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                throw new Exception("Lỗi khi lấy danh sách Role: " + ex.Message, ex);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return roles;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
                _connection.Dispose();
                _disposed = true;
            }
        }
    }
}