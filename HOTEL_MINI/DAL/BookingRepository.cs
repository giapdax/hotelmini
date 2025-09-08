using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
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
        public List<UsedServiceDto> GetUsedServicesByBookingID(int bookingID)
        {
            var list = new List<UsedServiceDto>();

            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();

                string sql = @"
            SELECT bs.BookingServiceID, bs.BookingID, s.ServiceID, 
                   s.ServiceName, s.Price, bs.Quantity
            FROM BookingServices bs
            INNER JOIN Services s ON bs.ServiceID = s.ServiceID
            WHERE bs.BookingID = @BookingID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BookingID", bookingID);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new UsedServiceDto
                        {
                            BookingServiceID = (int)reader["BookingServiceID"],
                            BookingID = (int)reader["BookingID"],
                            ServiceID = (int)reader["ServiceID"],
                            ServiceName = reader["ServiceName"].ToString(),
                            Price = (decimal)reader["Price"],
                            Quantity = (int)reader["Quantity"]
                        });
                    }
                }
            }
            return list;
        }
        public bool AddOrUpdateServiceForBooking(int bookingID, int serviceID, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(_stringConnection))
            {
                conn.Open();

                // Kiểm tra xem Service đã tồn tại trong Booking chưa
                string checkSql = "SELECT Quantity FROM BookingServices WHERE BookingID = @BookingID AND ServiceID = @ServiceID";
                SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                checkCmd.Parameters.AddWithValue("@BookingID", bookingID);
                checkCmd.Parameters.AddWithValue("@ServiceID", serviceID);

                object result = checkCmd.ExecuteScalar();

                if (result != null) // đã tồn tại → UPDATE
                {
                    int currentQuantity = Convert.ToInt32(result);
                    int newQuantity = currentQuantity + quantity;

                    string updateSql = "UPDATE BookingServices SET Quantity = @Quantity WHERE BookingID = @BookingID AND ServiceID = @ServiceID";
                    SqlCommand updateCmd = new SqlCommand(updateSql, conn);
                    updateCmd.Parameters.AddWithValue("@Quantity", newQuantity);
                    updateCmd.Parameters.AddWithValue("@BookingID", bookingID);
                    updateCmd.Parameters.AddWithValue("@ServiceID", serviceID);

                    return updateCmd.ExecuteNonQuery() > 0;
                }
                else // chưa tồn tại → INSERT
                {
                    string insertSql = @"INSERT INTO BookingServices (BookingID, ServiceID, Quantity)
                                 VALUES (@BookingID, @ServiceID, @Quantity)";
                    SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                    insertCmd.Parameters.AddWithValue("@BookingID", bookingID);
                    insertCmd.Parameters.AddWithValue("@ServiceID", serviceID);
                    insertCmd.Parameters.AddWithValue("@Quantity", quantity);

                    return insertCmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public void RemoveServiceFromBooking(int bookingServiceId)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand("DELETE FROM BookingServices WHERE BookingServiceID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", bookingServiceId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateServiceQuantity(int bookingServiceId, int newQuantity)
        {
            using (var conn = new SqlConnection(_stringConnection))
            using (var cmd = new SqlCommand("UPDATE BookingServices SET Quantity = @qty WHERE BookingServiceID = @id", conn))
            {
                cmd.Parameters.AddWithValue("@qty", newQuantity);
                cmd.Parameters.AddWithValue("@id", bookingServiceId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
