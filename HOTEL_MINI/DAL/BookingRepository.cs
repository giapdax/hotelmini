using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTEL_MINI.DAL
{
    public class BookingRepository
    {
        private readonly string _stringConnection;
        public BookingRepository()
        {
            _stringConnection = ConfigHelper.GetConnectionString();
        }
        public Booking AddBooking(Booking booking)
        {
            using(SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = @"INSERT INTO Bookings (CustomerID, RoomID, PricingID, CreatedBy, BookingDate, CheckInDate, CheckOutDate, Status, Notes)
                               VALUES (@CustomerID, @RoomID, @PricingID, @CreatedBy, @BookingDate, @CheckInDate, @CheckOutDate, @Status, @Notes);
                               SELECT SCOPE_IDENTITY();"; // Lấy ID của bản ghi vừa chèn
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CustomerID", booking.CustomerID);
                cmd.Parameters.AddWithValue("@RoomID", booking.RoomID);
                cmd.Parameters.AddWithValue("@PricingID", booking.PricingID);
                cmd.Parameters.AddWithValue("@CreatedBy", booking.CreatedBy);
                cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                cmd.Parameters.AddWithValue("@CheckInDate", (object)booking.CheckInDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CheckOutDate", (object)booking.CheckOutDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", booking.Status);
                cmd.Parameters.AddWithValue("@Notes", booking.Notes ?? (object)DBNull.Value);
                // Thực thi lệnh và lấy ID của bản ghi vừa chèn
                var result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int newBookingId))
                {
                    booking.BookingID = newBookingId;
                    return booking;
                }
                else
                {
                    //throw new Exception("Failed to retrieve the new Booking ID.");
                    MessageBox.Show("Không thể tạo Booking mới. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
        }
        public Booking GetLatestBookingByRoomId(int roomId)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = @"SELECT TOP 1 * FROM Bookings 
                           WHERE RoomID = @RoomID 
                           ORDER BY BookingDate DESC";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@RoomID", roomId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Booking
                            {
                                BookingID = (int)reader["BookingID"],
                                RoomID = (int)reader["RoomID"],
                                CustomerID = (int)reader["CustomerID"],
                                PricingID = (int)reader["PricingID"],
                                CreatedBy = (int)reader["CreatedBy"],
                                BookingDate = (DateTime)reader["BookingDate"],
                                CheckInDate = (DateTime)reader["CheckInDate"],
                                CheckOutDate = reader["CheckOutDate"] == DBNull.Value ? null : (DateTime?)reader["CheckOutDate"],
                                Status = reader["Status"].ToString(),
                                Notes = reader["Notes"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public bool Update(Booking booking)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();
                string sql = @"UPDATE Bookings 
                       SET Status = @Status,
                           CheckOutDate = @CheckOutDate,
                           Notes = @Notes
                       WHERE BookingID = @BookingID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@BookingID", booking.BookingID);
                    cmd.Parameters.AddWithValue("@Status", booking.Status);

                    if (booking.CheckOutDate.HasValue)
                        cmd.Parameters.AddWithValue("@CheckOutDate", booking.CheckOutDate.Value);
                    else
                        cmd.Parameters.AddWithValue("@CheckOutDate", DBNull.Value);

                    if (string.IsNullOrEmpty(booking.Notes))
                        cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Notes", booking.Notes);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

    }
}
