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
    public class RoomRepository
    {
        private readonly string _stringConnection;
        public RoomRepository()
        {
            _stringConnection = ConfigHelper.GetConnectionString();
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
    }
}
