using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;

namespace HOTEL_MINI.DAL
{
    internal class RoleRepository
    {
        public List<Role> GetAllRoles()
        {
            var roles = new List<Role>();
            const string sql = "SELECT RoleID, RoleName FROM Roles";

            using (var conn = new SqlConnection(ConfigHelper.GetConnectionString()))
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var reader = cmd.ExecuteReader())
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

            return roles;
        }
    }
}
