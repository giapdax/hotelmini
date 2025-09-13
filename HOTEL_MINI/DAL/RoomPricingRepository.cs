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
    public class RoomPricingRepository
    {
        private readonly string _connectionString;
        public RoomPricingRepository()
        {
            _connectionString = ConfigHelper.GetConnectionString();
        }
        public RoomPricing GetPricingTypeById(int pricingId)
        {
            const string query = "SELECT PricingID, RoomTypeID, PricingType, DurationValues, Price, IsActive " +
                                 "FROM RoomPricing WHERE PricingID = @PricingID";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PricingID", pricingId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new RoomPricing
                        {
                            PricingID = (int)reader["PricingID"],
                            RoomTypeID = (int)reader["RoomTypeID"],
                            PricingType = reader["PricingType"].ToString(),
                            DurationValues = reader["DurationValues"] != DBNull.Value ? (int)reader["DurationValues"] : 0,
                            Price = (decimal)reader["Price"],
                            IsActive = (bool)reader["IsActive"]
                        };
                    }
                }
            }

            return null; // không tìm thấy thì trả null
        }
    }
}
