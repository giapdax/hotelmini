using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.DAL
{
    public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository()
        {
            _connectionString = ConfigHelper.GetConnectionString();
        }
        public User getUserbyUsername(string username)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"SELECT UserID, Username, PasswordHash, RoleID, Fullname
                        FROM Users
                        Where Username = @Username";
                SqlCommand sqlCommand = new SqlCommand(sql, conn);
                sqlCommand.Parameters.AddWithValue("@Username", username);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (!reader.Read()) { return null; }
                    return new User()
                    {
                        UserID = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        FullName = reader.GetString(4),
                        PasswordHash = reader.GetString(2),
                        Role = reader.GetInt32(3)
                    };
                }
            }
        }
    }
}
