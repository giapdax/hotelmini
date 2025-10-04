using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
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
            using (var conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                var sql = @"INSERT INTO Payments (InvoiceID, Amount, PaymentDate, Method, Status)
                            VALUES (@InvoiceID, @Amount, @PaymentDate, @Method, @Status);
                            SELECT CAST(SCOPE_IDENTITY() AS INT);";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceID", payment.InvoiceID);
                    cmd.Parameters.AddWithValue("@Amount", payment.Amount);
                    cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    cmd.Parameters.AddWithValue("@Method", payment.Method ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", payment.Status ?? "Paid");
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        // Lấy payments theo InvoiceID
        public List<Payment> GetPaymentsByInvoiceId(int invoiceId)
        {
            var list = new List<Payment>();
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(
                   @"SELECT PaymentID, InvoiceID, Amount, PaymentDate, Method, Status
                     FROM Payments WHERE InvoiceID=@id ORDER BY PaymentDate ASC", conn))
            {
                cmd.Parameters.AddWithValue("@id", invoiceId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Payment
                        {
                            PaymentID = rd.GetInt32(0),
                            InvoiceID = rd.GetInt32(1),
                            Amount = rd.GetDecimal(2),
                            PaymentDate = rd.GetDateTime(3),
                            Method = rd.IsDBNull(4) ? "" : rd.GetString(4),
                            Status = rd.IsDBNull(5) ? "" : rd.GetString(5),
                        });
                    }
                }
            }
            return list;
        }

        // Lấy payments theo Booking HeaderID (join Invoices)
        public List<Payment> GetPaymentsByHeaderId(int headerBookingId)
        {
            var list = new List<Payment>();
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(
                   @"SELECT p.PaymentID, p.InvoiceID, p.Amount, p.PaymentDate, p.Method, p.Status
                     FROM Payments p
                     JOIN Invoices i ON i.InvoiceID = p.InvoiceID
                     WHERE i.BookingID = @bid
                     ORDER BY p.PaymentDate ASC", conn))
            {
                cmd.Parameters.AddWithValue("@bid", headerBookingId);
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new Payment
                        {
                            PaymentID = rd.GetInt32(0),
                            InvoiceID = rd.GetInt32(1),
                            Amount = rd.GetDecimal(2),
                            PaymentDate = rd.GetDateTime(3),
                            Method = rd.IsDBNull(4) ? "" : rd.GetString(4),
                            Status = rd.IsDBNull(5) ? "" : rd.GetString(5),
                        });
                    }
                }
            }
            return list;
        }

        // Tổng đã trả theo Header (sum tất cả payment của invoice thuộc header)
        public decimal GetPaidAmountByHeaderId(int headerBookingId)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand(
                   @"SELECT ISNULL(SUM(p.Amount),0)
                     FROM Payments p
                     JOIN Invoices i ON i.InvoiceID = p.InvoiceID
                     WHERE i.BookingID = @bid", conn))
            {
                cmd.Parameters.AddWithValue("@bid", headerBookingId);
                conn.Open();
                var o = cmd.ExecuteScalar();
                return o == null || o == DBNull.Value ? 0m : Convert.ToDecimal(o);
            }
        }
    }
}
