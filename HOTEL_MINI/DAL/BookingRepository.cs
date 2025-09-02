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
    }
}
