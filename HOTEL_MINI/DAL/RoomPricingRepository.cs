using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    public class RoomPricingRepository
    {
        private readonly string _connectionString;
        public RoomPricingRepository() { _connectionString = ConfigHelper.GetConnectionString(); }
        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        public List<RoomPricing> GetByRoomType(int roomTypeId)
        {
            var list = new List<RoomPricing>();
            const string sql = "SELECT PricingID, RoomTypeID, PricingType, Price, IsActive FROM RoomPricing WHERE RoomTypeID=@rtId";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@rtId", roomTypeId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                    while (rd.Read())
                        list.Add(new RoomPricing
                        {
                            PricingID = (int)rd["PricingID"],
                            RoomTypeID = (int)rd["RoomTypeID"],
                            PricingType = (string)rd["PricingType"],
                            Price = (decimal)rd["Price"],
                            IsActive = (bool)rd["IsActive"]
                        });
            }
            return list;
        }

        public RoomPricing GetPricingTypeById(int pricingId)
        {
            const string sql = "SELECT PricingID, RoomTypeID, PricingType, Price, IsActive FROM RoomPricing WHERE PricingID=@PricingID";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@PricingID", pricingId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                    if (rd.Read())
                        return new RoomPricing
                        {
                            PricingID = (int)rd["PricingID"],
                            RoomTypeID = (int)rd["RoomTypeID"],
                            PricingType = (string)rd["PricingType"],
                            Price = (decimal)rd["Price"],
                            IsActive = (bool)rd["IsActive"]
                        };
            }
            return null;
        }

        public RoomPricing GetByRoomTypeAndType(int roomTypeId, string pricingType)
        {
            const string sql = "SELECT PricingID, RoomTypeID, PricingType, Price, IsActive FROM RoomPricing WHERE RoomTypeID=@rtId AND PricingType=@ptype";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@rtId", roomTypeId);
                cmd.Parameters.AddWithValue("@ptype", pricingType);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                    if (rd.Read())
                        return new RoomPricing
                        {
                            PricingID = (int)rd["PricingID"],
                            RoomTypeID = (int)rd["RoomTypeID"],
                            PricingType = (string)rd["PricingType"],
                            Price = (decimal)rd["Price"],
                            IsActive = (bool)rd["IsActive"]
                        };
            }
            return null;
        }

        public bool Add(RoomPricing p)
        {
            const string sql = "INSERT INTO RoomPricing (RoomTypeID, PricingType, Price, IsActive) VALUES (@RoomTypeID,@PricingType,@Price,@IsActive)";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@RoomTypeID", p.RoomTypeID);
                cmd.Parameters.AddWithValue("@PricingType", p.PricingType);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@IsActive", p.IsActive);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(RoomPricing p)
        {
            const string sql = "UPDATE RoomPricing SET RoomTypeID=@RoomTypeID, PricingType=@PricingType, Price=@Price, IsActive=@IsActive WHERE PricingID=@PricingID";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@PricingID", p.PricingID);
                cmd.Parameters.AddWithValue("@RoomTypeID", p.RoomTypeID);
                cmd.Parameters.AddWithValue("@PricingType", p.PricingType);
                cmd.Parameters.AddWithValue("@Price", p.Price);
                cmd.Parameters.AddWithValue("@IsActive", p.IsActive);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<string> GetPricingTypes()
        {
            var list = new List<string>();
            const string sql = "SELECT Value FROM PricingTypeEnum ORDER BY Value";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                    while (rd.Read()) list.Add((string)rd["Value"]);
            }
            return list;
        }
    }
}
