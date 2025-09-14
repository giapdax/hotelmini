using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    public class InvoiceRepository
    {
        private readonly string _stringConnection;

        public InvoiceRepository()
        {
            _stringConnection = ConfigHelper.GetConnectionString();
        }

        public int AddInvoice(Invoice invoice)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = @"INSERT INTO Invoices (BookingID, RoomCharge, ServiceCharge, Discount, Surcharge, TotalAmount, IssuedBy, IssuedAt, Status, Note)
                           VALUES (@BookingID, @RoomCharge, @ServiceCharge, @Discount, @Surcharge, @TotalAmount, @IssuedBy, @IssuedAt, @Status, @Note);
                           SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BookingID", invoice.BookingID);
                cmd.Parameters.AddWithValue("@RoomCharge", invoice.RoomCharge);
                cmd.Parameters.AddWithValue("@ServiceCharge", invoice.ServiceCharge);
                cmd.Parameters.AddWithValue("@Discount", invoice.Discount);
                cmd.Parameters.AddWithValue("@Surcharge", invoice.Surcharge);
                cmd.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                cmd.Parameters.AddWithValue("@IssuedBy", invoice.IssuedBy);
                cmd.Parameters.AddWithValue("@IssuedAt", invoice.IssuedAt);
                cmd.Parameters.AddWithValue("@Status", invoice.Status);
                cmd.Parameters.AddWithValue("@Note", invoice.Note ?? (object)DBNull.Value);

                var result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public Invoice GetInvoiceByBookingID(int bookingID)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = "SELECT * FROM Invoices WHERE BookingID = @BookingID";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Invoice
                        {
                            InvoiceID = Convert.ToInt32(reader["InvoiceID"]),
                            BookingID = Convert.ToInt32(reader["BookingID"]),
                            RoomCharge = Convert.ToDecimal(reader["RoomCharge"]),
                            ServiceCharge = Convert.ToDecimal(reader["ServiceCharge"]),
                            Surcharge = Convert.ToDecimal(reader["Surcharge"]),
                            Discount = Convert.ToDecimal(reader["Discount"]),
                            TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                            IssuedAt = Convert.ToDateTime(reader["IssuedAt"]),
                            IssuedBy = Convert.ToInt32(reader["IssuedBy"]),
                            Status = reader["Status"].ToString(),
                            Note = reader["Note"] == DBNull.Value ? null : reader["Note"].ToString()
                        };
                    }
                }
                return null;
            }
        }
    }
}