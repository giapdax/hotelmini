using System;

namespace HOTEL_MINI.Model.Response
{
    public class BookingDisplay
    {
        public int BookingID { get; set; }
        public string RoomNumber { get; set; }      // Tên phòng
        public string EmployeeName { get; set; }    // Tên nhân viên
        public DateTime BookingDate { get; set; }   // Ngày book
        public DateTime? CheckInDate { get; set; }  // Ngày check-in
        public DateTime? CheckOutDate { get; set; } // Ngày check-out
        public string Notes { get; set; }           // Ghi chú
        public string Status { get; set; }          // Trạng thái

        // CCCD/CMND khách (QUAN TRỌNG: phải SELECT trong query)
        public string CustomerIDNumber { get; set; }

        // Format cho UI nếu bạn muốn xài dạng text thay vì DefaultCellStyle
        public string BookingDateDisplay => BookingDate.ToString("dd/MM/yyyy HH:mm");
        public string CheckInDateDisplay => CheckInDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
        public string CheckOutDateDisplay => CheckOutDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
    }
}
