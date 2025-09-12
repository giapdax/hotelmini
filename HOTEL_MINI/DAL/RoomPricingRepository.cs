// File: HOTEL_MINI/DAL/RoomPricingRepository.cs
using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    internal class RoomPricingRepository
    {
        private readonly string _connectionString;
        public RoomPricingRepository()
        {
            _connectionString = ConfigHelper.GetConnectionString();
        }
        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        public List<RoomPricing> GetAll()
        {
            var list = new List<RoomPricing>();
            const string sql = @"SELECT PricingID, RoomTypeID, PricingType, Price, IsActive FROM RoomPricing";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new RoomPricing
                        {
                            PricingID = rd.GetInt32(0),
                            RoomTypeID = rd.GetInt32(1),
                            PricingType = rd.GetString(2),
                            Price = rd.GetDecimal(3),
                            IsActive = rd.GetBoolean(4)
                        });
                    }
                }
            }
            return list;
        }

        public RoomPricing GetById(int pricingId)
        {
            const string sql = @"SELECT PricingID, RoomTypeID, PricingType, Price, IsActive FROM RoomPricing WHERE PricingID=@id";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", pricingId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new RoomPricing
                    {
                        PricingID = rd.GetInt32(0),
                        RoomTypeID = rd.GetInt32(1),
                        PricingType = rd.GetString(2),
                        Price = rd.GetDecimal(3),
                        IsActive = rd.GetBoolean(4)
                    };
                }
            }
        }

        public RoomPricing GetByRoomTypeAndType(int roomTypeId, string pricingType)
        {
            const string sql = @"
SELECT PricingID, RoomTypeID, PricingType, Price, IsActive
FROM RoomPricing
WHERE RoomTypeID=@rtId AND PricingType=@ptype";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@rtId", roomTypeId);
                cmd.Parameters.AddWithValue("@ptype", pricingType);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (!rd.Read()) return null;
                    return new RoomPricing
                    {
                        PricingID = rd.GetInt32(0),
                        RoomTypeID = rd.GetInt32(1),
                        PricingType = rd.GetString(2),
                        Price = rd.GetDecimal(3),
                        IsActive = rd.GetBoolean(4)
                    };
                }
            }
        }

        public bool Add(RoomPricing p)
        {
            const string sql = @"
INSERT INTO RoomPricing (RoomTypeID, PricingType, Price, IsActive)
VALUES (@RoomTypeID, @PricingType, @Price, @IsActive)";
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
            const string sql = @"
UPDATE RoomPricing
SET RoomTypeID=@RoomTypeID, PricingType=@PricingType, Price=@Price, IsActive=@IsActive
WHERE PricingID=@PricingID";
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

        public bool Delete(int pricingId)
        {
            const string sql = @"DELETE FROM RoomPricing WHERE PricingID=@id";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", pricingId);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public List<string> GetPricingTypes()
        {
            var list = new List<string>();
            const string sql = @"SELECT Value FROM PricingTypeEnum ORDER BY Value";
            using (var conn = CreateConnection())
            using (var cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                    while (rd.Read()) list.Add(rd.GetString(0));
            }
            return list;
        }
    }
}
