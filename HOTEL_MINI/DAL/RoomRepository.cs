    using HOTEL_MINI.Common;
    using HOTEL_MINI.Model.Entity;
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
                using(SqlConnection conn = new SqlConnection(_stringConnection))
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
            public bool UpdateRoomStatus(int roomID, string status)
            {
                using(SqlConnection conn = new SqlConnection(_stringConnection))
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
            public RoomPricing getPricingID(string pricingType, int roomType)
            {
                using (SqlConnection conn = new SqlConnection(_stringConnection))
                {
                    conn.Open();
                    string sql = "SELECT PricingID, DurationValues, Price FROM RoomPricing WHERE PricingType = @PricingType AND RoomTypeID = @RoomTypeID";
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
        public bool AddRoom(Room m)
        {
            using (var cn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(
                @"INSERT INTO Rooms (RoomNumber, RoomTypeID, Status, Note)
                  OUTPUT INSERTED.RoomID
                  VALUES (@num, @type, @st, @note)", cn))
            {
                cmd.Parameters.AddWithValue("@num", m.RoomNumber);
                cmd.Parameters.AddWithValue("@type", m.RoomTypeID);
                cmd.Parameters.AddWithValue("@st", m.RoomStatus);
                cmd.Parameters.AddWithValue("@note", (object)(m.Note ?? "") ?? DBNull.Value);

                cn.Open();
                var newId = cmd.ExecuteScalar();
                if (newId != null && int.TryParse(newId.ToString(), out var id))
                {
                    m.RoomID = id;  // để UI có thể re-select
                    return true;
                }
                return false;
            }
        }
        public bool UpdateRoom(Room m)
        {
            using (var cn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(
                @"UPDATE Rooms
                  SET RoomNumber=@num, RoomTypeID=@type, Status=@st, Note=@note
                  WHERE RoomID=@id", cn))
            {
                cmd.Parameters.AddWithValue("@num", m.RoomNumber);
                cmd.Parameters.AddWithValue("@type", m.RoomTypeID);
                cmd.Parameters.AddWithValue("@st", m.RoomStatus);
                cmd.Parameters.AddWithValue("@note", (object)(m.Note ?? "") ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", m.RoomID);

                cn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
    }
