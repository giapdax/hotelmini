using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    public class PaymentRepository
    {
        private readonly string _stringConnection;

        public PaymentRepository()
        {
            _stringConnection = ConfigHelper.GetConnectionString();
        }

        public int AddPayment(Payment payment)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = @"INSERT INTO Payments (InvoiceID, Amount, PaymentDate, Method, Status)
                           VALUES (@InvoiceID, @Amount, @PaymentDate, @Method, @Status);
                           SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@InvoiceID", payment.InvoiceID);
                cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                cmd.Parameters.AddWithValue("@Method", payment.Method);
                cmd.Parameters.AddWithValue("@Status", payment.Status);

                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }
    }
}