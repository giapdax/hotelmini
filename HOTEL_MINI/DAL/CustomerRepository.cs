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
        public Customer AddNewCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"
            INSERT INTO Customers (FullName, Gender, Phone, Email, Address, IDNumber)
            OUTPUT INSERTED.CustomerID, INSERTED.FullName, INSERTED.Gender, 
                   INSERTED.Phone, INSERTED.Email, INSERTED.Address, 
                   INSERTED.IDNumber, INSERTED.CreatedAt
            VALUES (@FullName, @Gender, @Phone, @Email, @Address, @IDNumber)";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", (object)customer.FullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", (object)customer.Gender ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object)customer.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)customer.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object)customer.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IDNumber", (object)customer.IDNumber ?? DBNull.Value);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Customer
                            {
                                CustomerID = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Gender = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Email = reader.IsDBNull(4) ? null : reader.GetString(4),
                                Address = reader.IsDBNull(5) ? null : reader.GetString(5),
                                IDNumber = reader.IsDBNull(6) ? null : reader.GetString(6),
                                CreatedAt = reader.GetDateTime(7)
                            };
                        }
                    }
                }
            }

            return null; // nếu insert thất bại
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
                if (reader.Read())
                {
                    return new Customer
                    {
                        CustomerID = (int)reader["CustomerID"],
                        FullName = reader["FullName"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Address = reader["Address"].ToString(),
                        IDNumber = reader["IDNumber"].ToString(),
                        CreatedAt = (DateTime)reader["CreatedAt"]
                    };
                }
                return null;
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
