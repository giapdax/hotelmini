using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using MiniHotel.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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
                const string sql = "SELECT RoomTypeID, TypeName, Description FROM RoomTypes";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        listRoomType.Add(new RoomTypes
                        {
                            RoomTypeID = rd.GetInt32(0),
                            TypeName = rd.GetString(1),
                            Description = rd.IsDBNull(2) ? "" : rd.GetString(2),
                        });
                    }
                }
            }
            return listRoomType;
        }

        public RoomStatistics GetRoomStatistics()
        {
            RoomStatistics stats = new RoomStatistics();
            const string query = @"
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
            return stats;
        }

        public bool UpdateRoomStatusAfterCheckout(int roomID, string status)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                const string sql = @"UPDATE Rooms SET Status = @Status WHERE RoomID = @RoomID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@RoomID", roomID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateRoomStatus(int roomID, string status)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                const string sql = "UPDATE Rooms SET Status = @Status WHERE RoomID = @RoomID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@RoomID", roomID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<Room> getRoomList()
        {
            var listRoom = new List<Room>();
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                const string sql = "SELECT RoomID, RoomNumber, RoomTypeID, Status, Note FROM Rooms";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        listRoom.Add(new Room
                        {
                            RoomID = rd.GetInt32(0),
                            RoomNumber = rd.GetString(1),
                            RoomTypeID = rd.GetInt32(2),
                            RoomStatus = rd.GetString(3),
                            Note = rd.IsDBNull(4) ? null : rd.GetString(4),
                        });
                    }
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
                const string sql = "SELECT Value FROM RoomStatusEnum";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) listStatus.Add(rd.GetString(0));
                }
            }
            return listStatus;
        }

        public List<string> getAllPricingType()
        {
            var list = new List<string>();
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                const string sql = "SELECT Value FROM PricingTypeEnum";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) list.Add(rd.GetString(0));
                }
            }
            return list;
        }

        public string getPricingTypeByID(int pricingID)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                const string sql = "SELECT PricingType FROM RoomPricing WHERE PricingID = @PricingID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PricingID", pricingID);
                    object val = cmd.ExecuteScalar();
                    return val == null ? string.Empty : val.ToString();
                }
            }
        }

        public RoomPricing getPricingID(string pricingType, int roomType)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                const string sql = @"
                    SELECT PricingID, Price 
                    FROM RoomPricing 
                    WHERE PricingType = @PricingType AND RoomTypeID = @RoomTypeID AND IsActive = 1";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PricingType", pricingType);
                    cmd.Parameters.AddWithValue("@RoomTypeID", roomType);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            return new RoomPricing
                            {
                                PricingID = rd.GetInt32(0),
                                RoomTypeID = roomType,
                                PricingType = pricingType,
                                Price = rd.GetDecimal(1)
                            };
                        }
                    }
                }
            }
            return null;
        }

        private bool IsRoomNumberUnique(string roomNumber, int currentRoomId)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                const string sql = "SELECT COUNT(*) FROM Rooms WHERE RoomNumber = @RoomNumber AND RoomID <> @RoomID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomNumber", (roomNumber ?? "").Trim());
                    cmd.Parameters.AddWithValue("@RoomID", currentRoomId);
                    int count = (int)cmd.ExecuteScalar();
                    return count == 0;
                }
            }
        }

        public bool AddRoom(Room room)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                if (!IsRoomNumberUnique(room.RoomNumber, 0)) return false;

                const string sql = @"INSERT INTO Rooms (RoomNumber, RoomTypeID, Status, Note) 
                                     VALUES (@RoomNumber, @RoomTypeID, @Status, @Note)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomNumber", (room.RoomNumber ?? "").Trim());
                    cmd.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeID);
                    cmd.Parameters.AddWithValue("@Status", room.RoomStatus ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Note", (object)(room.Note ?? (string)null) ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateRoom(Room room)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                if (!IsRoomNumberUnique(room.RoomNumber, room.RoomID)) return false;

                const string sql = @"UPDATE Rooms 
                                     SET RoomNumber=@RoomNumber, RoomTypeID=@RoomTypeID, Status=@Status, Note=@Note 
                                     WHERE RoomID=@RoomID";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomID", room.RoomID);
                    cmd.Parameters.AddWithValue("@RoomNumber", (room.RoomNumber ?? "").Trim());
                    cmd.Parameters.AddWithValue("@RoomTypeID", room.RoomTypeID);
                    cmd.Parameters.AddWithValue("@Status", room.RoomStatus ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Note", (object)(room.Note ?? (string)null) ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public List<RoomBrowsePriceItem> SearchRoomsWithPrices(DateTime from, DateTime to, int? roomTypeId, string status)
        {
            var list = new List<RoomBrowsePriceItem>();
            const string sql = @"
SELECT 
    r.RoomID,
    r.RoomNumber,
    r.RoomTypeID,
    rt.TypeName,
    r.Status,
    r.Note,
    MAX(CASE WHEN rp.PricingType = 'Hourly'  THEN rp.Price END) AS PriceHourly,
    MAX(CASE WHEN rp.PricingType = 'Nightly' THEN rp.Price END) AS PriceNightly,
    MAX(CASE WHEN rp.PricingType = 'Daily'   THEN rp.Price END) AS PriceDaily,
    MAX(CASE WHEN rp.PricingType = 'Weekly'  THEN rp.Price END) AS PriceWeekly,
    CAST(CASE 
        WHEN EXISTS (
            SELECT 1
            FROM BookingRooms br
            WHERE br.RoomID = r.RoomID
              AND br.Status IN ('Booked','CheckedIn')
              AND br.CheckInDate < @To
              AND @From < br.CheckOutDate
        ) THEN 0 ELSE 1
    END AS bit) AS AvailableAtRange
FROM Rooms r
JOIN RoomTypes rt ON rt.RoomTypeID = r.RoomTypeID
LEFT JOIN RoomPricing rp ON rp.RoomTypeID = r.RoomTypeID AND rp.IsActive = 1
WHERE (@RoomTypeID IS NULL OR r.RoomTypeID = @RoomTypeID)
  AND (@Status     IS NULL OR r.Status     = @Status)
GROUP BY r.RoomID, r.RoomNumber, r.RoomTypeID, rt.TypeName, r.Status, r.Note
ORDER BY r.RoomNumber;";

            using (SqlConnection conn = new SqlConnection(_stringConnection))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                cmd.Parameters.AddWithValue("@RoomTypeID", (object)roomTypeId ?? DBNull.Value);

                string normalizedStatus = string.IsNullOrWhiteSpace(status) ||
                                   status.Equals("(Tất cả)", StringComparison.OrdinalIgnoreCase)
                                   ? null : status;
                cmd.Parameters.AddWithValue("@Status", (object)normalizedStatus ?? DBNull.Value);

                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new RoomBrowsePriceItem
                        {
                            RoomID = rd.GetInt32(0),
                            RoomNumber = rd.GetString(1),
                            RoomTypeID = rd.GetInt32(2),
                            RoomTypeName = rd.GetString(3),
                            Status = rd.GetString(4),
                            Note = rd.IsDBNull(5) ? "" : rd.GetString(5),
                            PriceHourly = rd.IsDBNull(6) ? (decimal?)null : rd.GetDecimal(6),
                            PriceNightly = rd.IsDBNull(7) ? (decimal?)null : rd.GetDecimal(7),
                            PriceDaily = rd.IsDBNull(8) ? (decimal?)null : rd.GetDecimal(8),
                            PriceWeekly = rd.IsDBNull(9) ? (decimal?)null : rd.GetDecimal(9),
                            AvailableAtRange = rd.GetBoolean(10)
                        });
                    }
                }
            }
            return list;
        }

        public List<RoomBrowsePriceItem> SearchRoomsWithPrices(int? roomTypeId, string status)
        {
            var list = new List<RoomBrowsePriceItem>();
            const string sql = @"
SELECT 
    r.RoomID,
    r.RoomNumber,
    r.RoomTypeID,
    rt.TypeName,
    r.Status,
    r.Note,
    MAX(CASE WHEN rp.PricingType = 'Hourly'  THEN rp.Price END) AS PriceHourly,
    MAX(CASE WHEN rp.PricingType = 'Nightly' THEN rp.Price END) AS PriceNightly,
    MAX(CASE WHEN rp.PricingType = 'Daily'   THEN rp.Price END) AS PriceDaily,
    MAX(CASE WHEN rp.PricingType = 'Weekly'  THEN rp.Price END) AS PriceWeekly
FROM Rooms r
JOIN RoomTypes rt ON rt.RoomTypeID = r.RoomTypeID
LEFT JOIN RoomPricing rp ON rp.RoomTypeID = r.RoomTypeID AND rp.IsActive = 1
WHERE (@RoomTypeID IS NULL OR r.RoomTypeID = @RoomTypeID)
  AND (@Status     IS NULL OR r.Status     = @Status)
GROUP BY r.RoomID, r.RoomNumber, r.RoomTypeID, rt.TypeName, r.Status, r.Note
ORDER BY r.RoomNumber;";

            using (SqlConnection conn = new SqlConnection(_stringConnection))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RoomTypeID", (object)roomTypeId ?? DBNull.Value);

                string normalizedStatus = string.IsNullOrWhiteSpace(status) ||
                                   status.Equals("(Tất cả)", StringComparison.OrdinalIgnoreCase)
                                   ? null : status;
                cmd.Parameters.AddWithValue("@Status", (object)normalizedStatus ?? DBNull.Value);

                conn.Open();
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new RoomBrowsePriceItem
                        {
                            RoomID = rd.GetInt32(0),
                            RoomNumber = rd.GetString(1),
                            RoomTypeID = rd.GetInt32(2),
                            RoomTypeName = rd.GetString(3),
                            Status = rd.GetString(4),
                            Note = rd.IsDBNull(5) ? "" : rd.GetString(5),
                            PriceHourly = rd.IsDBNull(6) ? (decimal?)null : rd.GetDecimal(6),
                            PriceNightly = rd.IsDBNull(7) ? (decimal?)null : rd.GetDecimal(7),
                            PriceDaily = rd.IsDBNull(8) ? (decimal?)null : rd.GetDecimal(8),
                            PriceWeekly = rd.IsDBNull(9) ? (decimal?)null : rd.GetDecimal(9),
                        });
                    }
                }
            }
            return list;
        }

        public bool IsRoomAvailable(int roomId, DateTime from, DateTime to)
        {
            const string sql = @"
SELECT CASE WHEN EXISTS (
    SELECT 1
    FROM BookingRooms br
    WHERE br.RoomID = @RoomID
      AND br.Status IN ('Booked','CheckedIn')
      AND br.CheckInDate < @To
      AND @From < br.CheckOutDate
) THEN 0 ELSE 1 END;";
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RoomID", roomId);
                cmd.Parameters.AddWithValue("@From", from);
                cmd.Parameters.AddWithValue("@To", to);
                conn.Open();
                int val = (int)cmd.ExecuteScalar();
                return val == 1;
            }
        }

        public List<RoomBrowseItem> SearchRooms(DateTime from, DateTime to, int? roomTypeId, string status)
        {
            var result = new List<RoomBrowseItem>();
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                StringBuilder sql = new StringBuilder(@"
SELECT 
    r.RoomID,
    r.RoomNumber,
    r.RoomTypeID,
    rt.TypeName,
    r.Status,
    r.Note,
    CAST(CASE 
        WHEN EXISTS (
            SELECT 1 
            FROM BookingRooms br
            WHERE br.RoomID = r.RoomID
              AND br.Status IN ('Booked','CheckedIn')
              AND br.CheckInDate < @To
              AND @From < br.CheckOutDate
        ) THEN 0 ELSE 1 
    END AS bit) AS AvailableAtRange
FROM Rooms r
JOIN RoomTypes rt ON rt.RoomTypeID = r.RoomTypeID
WHERE 1=1
");
                if (roomTypeId.HasValue && roomTypeId.Value > 0)
                    sql.Append(" AND r.RoomTypeID = @RoomTypeID ");
                if (!string.IsNullOrWhiteSpace(status) && !status.Equals("(Tất cả)", StringComparison.OrdinalIgnoreCase))
                    sql.Append(" AND r.Status = @Status ");

                sql.Append(" ORDER BY r.RoomNumber ");

                using (SqlCommand cmd = new SqlCommand(sql.ToString(), conn))
                {
                    cmd.Parameters.AddWithValue("@From", from);
                    cmd.Parameters.AddWithValue("@To", to);
                    if (roomTypeId.HasValue && roomTypeId.Value > 0)
                        cmd.Parameters.AddWithValue("@RoomTypeID", roomTypeId.Value);
                    if (!string.IsNullOrWhiteSpace(status) && !status.Equals("(Tất cả)", StringComparison.OrdinalIgnoreCase))
                        cmd.Parameters.AddWithValue("@Status", status);

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            result.Add(new RoomBrowseItem
                            {
                                RoomID = rd.GetInt32(0),
                                RoomNumber = rd.GetString(1),
                                RoomTypeID = rd.GetInt32(2),
                                RoomTypeName = rd.GetString(3),
                                Status = rd.GetString(4),
                                Note = rd.IsDBNull(5) ? "" : rd.GetString(5),
                                AvailableAtRange = rd.GetBoolean(6)
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}
