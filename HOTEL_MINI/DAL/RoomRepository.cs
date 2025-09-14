using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using MiniHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.DAL
{
    public class RoomRepository
    {
        private readonly string _stringConnection;
        public RoomRepository()
        {
            _stringConnection = ConfigHelper.GetConnectionString();
        }
        public List<RoomTypes> getRoomTypeList()
        {
            var listRoomType = new List<RoomTypes>();
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "SELECT RoomTypeID, TypeName, Description FROM RoomTypes";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    listRoomType.Add(new RoomTypes
                    {
                        RoomTypesID = sqlDataReader.GetInt32(0),
                        TypeName = sqlDataReader.GetString(1),
                        Description = sqlDataReader.GetString(2),
                    });
                }
            }
            return listRoomType;
        }
        public RoomStatistics GetRoomStatistics()
        {
            RoomStatistics stats = new RoomStatistics();

            string query = @"
            SELECT 
                COUNT(*) as TotalRooms,
                SUM(CASE WHEN Status = 'Available' THEN 1 ELSE 0 END) as AvailableRooms,
                SUM(CASE WHEN Status = 'Booked' THEN 1 ELSE 0 END) as BookedRooms,
                SUM(CASE WHEN Status = 'Occupied' THEN 1 ELSE 0 END) as OccupiedRooms,
                SUM(CASE WHEN Status = 'Maintenance' THEN 1 ELSE 0 END) as MaintenanceRooms
            FROM Rooms";

            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats.TotalRooms = reader.GetInt32(0);
                            stats.AvailableRooms = reader.GetInt32(1);
                            stats.BookedRooms = reader.GetInt32(2);
                            stats.OccupiedRooms = reader.GetInt32(3);
                            stats.MaintenanceRooms = reader.GetInt32(4);
                        }
                    }
                }
            }

            return stats;
        }
        public bool UpdateRoomStatusAfterCheckout(int roomID, string status)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = @"UPDATE Rooms 
                          SET Status = @Status 
                          WHERE RoomID = @RoomID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@RoomID", roomID);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateRoomStatus(int roomID, string status)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "UPDATE Rooms SET Status = @Status WHERE RoomID = @RoomID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@RoomID", roomID);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public List<Room> getRoomList()
        {
            var listRoom = new List<Room>();
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "SELECT RoomID, RoomNumber, RoomTypeID, Status, Note FROM Rooms";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    listRoom.Add(new Room
                    {
                        RoomID = sqlDataReader.GetInt32(0),
                        RoomNumber = sqlDataReader.GetString(1),
                        RoomTypeID = sqlDataReader.GetInt32(2),
                        RoomStatus = sqlDataReader.GetString(3),
                        Note = sqlDataReader.GetString(4),
                    });
                }
            }

            return listRoom;
        }
        public List<string> getRoomStatus()
        {
            var listStatus = new List<string>();
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "SELECT Value FROM RoomStatusEnum";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    listStatus.Add(sqlDataReader.GetString(0));
                }
            }
            return listStatus;
        }
        public List<string> getAllPricingType()
        {
            var listPricingType = new List<string>();
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "SELECT Value FROM PricingTypeEnum";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    listPricingType.Add(sqlDataReader.GetString(0));
                }
            }
            return listPricingType;
        }
        public string getPricingTypeByID(int pricingID)
        {
            string pricingType = string.Empty;
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "SELECT PricingType FROM RoomPricing WHERE PricingID = @PricingID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PricingID", pricingID);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    pricingType = sqlDataReader.GetString(0);
                }
            }
            return pricingType;
        }

        public RoomPricing getPricingID(string pricingType, int roomType)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "SELECT PricingID, Price FROM RoomPricing WHERE PricingType = @PricingType AND RoomTypeID = @RoomTypeID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@PricingType", pricingType);
                cmd.Parameters.AddWithValue("@RoomTypeID", roomType);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                if (sqlDataReader.Read())
                {
                    return new RoomPricing
                    {
                        PricingID = sqlDataReader.GetInt32(0),
                        RoomTypeID = roomType,
                        PricingType = pricingType,
                        Price = sqlDataReader.GetDecimal(2)
                    };
                    //pricingID = sqlDataReader.GetInt32(0);
                }
            }
            return null;
        }
        public bool AddRoom(Room room)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "INSERT INTO Rooms (RoomNumber, RoomTypeID, Status, Note) " +
                             "VALUES (@RoomNumber, @RoomTypeID, @Status, @Note)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeID);
                cmd.Parameters.AddWithValue("@Status", room.RoomStatus);
                cmd.Parameters.AddWithValue("@Note", room.Note ?? (object)DBNull.Value);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        public bool UpdateRoom(Room room)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "UPDATE Rooms SET RoomNumber = @RoomNumber, RoomTypeID = @RoomTypeID, Status = @Status, Note = @Note " +
                             "WHERE RoomID = @RoomID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@RoomID", room.RoomID);
                cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
                cmd.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeID);
                cmd.Parameters.AddWithValue("@Status", room.RoomStatus);
                cmd.Parameters.AddWithValue("@Note", room.Note ?? (object)DBNull.Value);
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
    }
}