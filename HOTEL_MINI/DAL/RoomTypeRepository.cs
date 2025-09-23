// File: RoomTypeRepository.cs
using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using MiniHotel.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    internal class RoomTypeRepository
    {
        private readonly string _connectionString;

        public RoomTypeRepository()
        {
            _connectionString = ConfigHelper.GetConnectionString();
        }

        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        public List<RoomTypes> GetAllRoomTypes()
        {
            var list = new List<RoomTypes>();
            const string query = "SELECT RoomTypeID, TypeName, Description FROM RoomTypes";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new RoomTypes
                        {
                            RoomTypeID = reader.GetInt32(0),
                            TypeName = reader.GetString(1),
                            //Description = reader.IsDBNull(2) ? "" : reader.GetString(2)
                            Description = reader.IsDBNull(2) ? string.Empty : reader.GetString(2)
                        });
                    }
                }
            }
            return list;
        }

        public RoomTypes GetById(int roomTypeId)
        {
            const string query = "SELECT RoomTypeID, TypeName, Description FROM RoomTypes WHERE RoomTypeID=@id";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", roomTypeId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;
                    return new RoomTypes
                    {
                        RoomTypeID = reader.GetInt32(0),
                        TypeName = reader.GetString(1),
                        Description = reader.IsDBNull(2) ? "" : reader.GetString(2)
                    };
                }
            }
        }
        public bool AddRoomType(RoomTypes roomType)
        {
            const string query ="INSERT INTO RoomTypes (TypeName, Description) " +
                "VALUES (@TypeName, @Description)";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@TypeName", roomType.TypeName);
                command.Parameters.AddWithValue("@Description", (object)roomType.Description ?? "");

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateRoomType(RoomTypes roomType)
        {
            const string query ="UPDATE RoomTypes SET TypeName=@TypeName, Description=@Description " +
                "WHERE RoomTypeID=@RoomTypeID";

            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@RoomTypeID", roomType.RoomTypeID);
                command.Parameters.AddWithValue("@TypeName", roomType.TypeName);
                command.Parameters.AddWithValue("@Description", (object)roomType.Description ?? "");

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

    }
}
