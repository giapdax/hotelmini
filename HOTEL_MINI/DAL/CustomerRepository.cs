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
    public class CustomerRepository
    {
        private readonly string _connectionString;
        public CustomerRepository()
        {
            _connectionString = ConfigHelper.GetConnectionString();
        }
        public Customer GetCustomerByIDNumber(string idNumber)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE IDNumber = @IDNumber", conn);
                cmd.Parameters.AddWithValue("@IDNumber", idNumber);

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                
                return new Customer
                {
                    CustomerID = (int)reader["CustomerID"],
                    FullName = reader["FullName"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    IDNumber = reader["IDNumber"].ToString(),
                    CreatedAt = (DateTime)reader["CreatedAt"]
                };
                
            }
        }
        public bool checkExistNumberID(string idNumber)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Customers WHERE IDNumber = @IDNumber", conn);
                cmd.Parameters.AddWithValue("@IDNumber", idNumber);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

    }
}
