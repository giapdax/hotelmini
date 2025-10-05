using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.DAL
{
    /// <summary>
    /// Quản lý gắn/phân phòng (BookingRooms) cho từng Invoice qua bảng InvoiceRooms.
    /// </summary>
    public class InvoiceRoomRepository
    {
        private readonly string _cs;
        public InvoiceRoomRepository()
        {
            _cs = ConfigHelper.GetConnectionString();
        }

        public void AddRoomsToInvoice(int invoiceId, IEnumerable<int> bookingRoomIds)
        {
            using (var conn = new SqlConnection(_cs))
            {
                conn.Open();

                // Khóa Invoice tồn tại?
                using (var chk = new SqlCommand("SELECT COUNT(1) FROM Invoices WHERE InvoiceID=@i", conn))
                {
                    chk.Parameters.Add("@i", SqlDbType.Int).Value = invoiceId;
                    if (Convert.ToInt32(chk.ExecuteScalar() ?? 0) <= 0)
                        throw new InvalidOperationException("Invoice không tồn tại.");
                }

                foreach (var lineId in bookingRoomIds)
                {
                    using (var cmd = new SqlCommand(@"
IF NOT EXISTS(SELECT 1 FROM InvoiceRooms WHERE InvoiceID=@I AND BookingRoomID=@BR)
    INSERT INTO InvoiceRooms(InvoiceID, BookingRoomID) VALUES(@I,@BR);", conn))
                    {
                        cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                        cmd.Parameters.Add("@BR", SqlDbType.Int).Value = lineId;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public void RemoveRoomFromInvoice(int invoiceId, int bookingRoomId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("DELETE FROM InvoiceRooms WHERE InvoiceID=@I AND BookingRoomID=@BR", conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                cmd.Parameters.Add("@BR", SqlDbType.Int).Value = bookingRoomId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ClearRoomsForInvoice(int invoiceId)
        {
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("DELETE FROM InvoiceRooms WHERE InvoiceID=@I", conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<int> GetBookingRoomIdsByInvoice(int invoiceId)
        {
            var list = new List<int>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand("SELECT BookingRoomID FROM InvoiceRooms WHERE InvoiceID=@I ORDER BY BookingRoomID", conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) list.Add(rd.GetInt32(0));
                }
            }
            return list;
        }

        /// <summary>
        /// Trả về thông tin phẳng phòng trong invoice (để UI hiển thị/tính tiền phía BLL).
        /// </summary>
        public List<BookingFlatDisplay> GetInvoiceRoomsFlat(int invoiceId)
        {
            const string sql = @"
SELECT 
    b.BookingID          AS HeaderBookingID,
    br.BookingRoomID,
    c.IDNumber           AS CustomerIDNumber,
    r.RoomNumber,
    u.FullName           AS EmployeeName,
    b.BookingDate,
    br.CheckInDate,
    br.CheckOutDate,
    ISNULL(br.Note, b.Notes) AS Notes,
    br.Status
FROM InvoiceRooms ir
JOIN BookingRooms br ON br.BookingRoomID = ir.BookingRoomID
JOIN Bookings b      ON b.BookingID      = br.BookingID
JOIN Customers c     ON c.CustomerID     = b.CustomerID
JOIN Rooms r         ON r.RoomID         = br.RoomID
LEFT JOIN Users u    ON u.UserID         = b.CreatedBy
WHERE ir.InvoiceID = @I
ORDER BY r.RoomNumber, br.BookingRoomID;";
            var list = new List<BookingFlatDisplay>();
            using (var conn = new SqlConnection(_cs))
            using (var cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.Add("@I", SqlDbType.Int).Value = invoiceId;
                conn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(new BookingFlatDisplay
                        {
                            HeaderBookingID = rd.GetInt32(0),
                            BookingRoomID = rd.GetInt32(1),
                            CustomerIDNumber = rd.IsDBNull(2) ? "" : rd.GetString(2),
                            RoomNumber = rd.GetString(3),
                            EmployeeName = rd.IsDBNull(4) ? "" : rd.GetString(4),
                            BookingDate = rd.GetDateTime(5),
                            CheckInDate = rd.IsDBNull(6) ? (DateTime?)null : rd.GetDateTime(6),
                            CheckOutDate = rd.IsDBNull(7) ? (DateTime?)null : rd.GetDateTime(7),
                            Notes = rd.IsDBNull(8) ? "" : rd.GetString(8),
                            Status = rd.IsDBNull(9) ? "" : rd.GetString(9)
                        });
                    }
                }
            }
            return list;
        }
    }
}
