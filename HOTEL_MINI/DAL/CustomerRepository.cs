using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

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

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
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
                }
                return null;
            }
        }
        public Customer GetCustomerByCustomerID(int CustomerID)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customers WHERE CustomerID = @CustomerID", conn);
                cmd.Parameters.AddWithValue("@CustomerID", CustomerID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) 
                    {
                        return new Customer
                        {
                            CustomerID = CustomerID,
                            FullName = reader["FullName"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Address = reader["Address"].ToString(),
                            IDNumber = reader["IDNumber"].ToString(),
                            CreatedAt = (DateTime)reader["CreatedAt"]
                        };
                    }
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
        public List<Customer> GetAllCustomers()
        {
            var list = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"SELECT CustomerID, FullName, Gender, Phone, Email, Address, IDNumber, CreatedAt
                       FROM Customers
                       ORDER BY CreatedAt DESC, CustomerID DESC";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        list.Add(new Customer
                        {
                            CustomerID = r.GetInt32(0),
                            FullName = r.IsDBNull(1) ? null : r.GetString(1),
                            Gender = r.IsDBNull(2) ? null : r.GetString(2),
                            Phone = r.IsDBNull(3) ? null : r.GetString(3),
                            Email = r.IsDBNull(4) ? null : r.GetString(4),
                            Address = r.IsDBNull(5) ? null : r.GetString(5),
                            IDNumber = r.IsDBNull(6) ? null : r.GetString(6),
                            CreatedAt = r.GetDateTime(7)
                        });
                    }
                }
            }
            return list;
        }

        public bool UpdateCustomer(Customer c)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"UPDATE Customers SET
                            FullName = @FullName,
                            Gender   = @Gender,
                            Phone    = @Phone,
                            Email    = @Email,
                            Address  = @Address,
                            IDNumber = @IDNumber
                        WHERE CustomerID = @CustomerID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", c.CustomerID);
                    cmd.Parameters.AddWithValue("@FullName", (object)c.FullName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", (object)c.Gender ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object)c.Phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)c.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Address", (object)c.Address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IDNumber", (object)c.IDNumber ?? DBNull.Value);

                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }

        public List<string> getAllGender() //hàm này để lấy toàn bộ giưới tính trong bảng GenderEnum với cột Value
        {
            List<string> listGender = new List<string>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Value FROM GenderEnum", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listGender.Add(reader["Value"].ToString());
                    }
                }
            }
            return listGender;
        }
        public Dictionary<int, int> GetBookingCounts()
        {
            var dict = new Dictionary<int, int>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                // Nếu muốn chỉ đếm booking hợp lệ thì thêm WHERE Status <> 'Cancelled' tùy schema của bạn
                string sql = @"SELECT CustomerID, COUNT(*) AS Cnt 
                       FROM Bookings 
                       GROUP BY CustomerID";
                using (var cmd = new SqlCommand(sql, conn))
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        int customerId = rd.GetInt32(0);
                        int cnt = rd.GetInt32(1);
                        dict[customerId] = cnt;
                    }
                }
            }
            return dict;
        }

    }
}
