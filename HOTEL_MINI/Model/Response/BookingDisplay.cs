using System;

namespace HOTEL_MINI.Model.Response
{
    public class BookingDisplay
    {
        public int BookingID { get; set; }                 // = BookingRoomID (line)
        public string RoomNumber { get; set; } = "";
        public string EmployeeName { get; set; } = "";

        public DateTime BookingDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

        public string Notes { get; set; } = "";
        public string Status { get; set; } = "";

        // >>> THÊM: số CCCD/ID của khách
        public string CustomerIDNumber { get; set; } = "";

        // ---- Display-only (read-only) ----
        public string BookingDateDisplay => BookingDate == default ? "" : BookingDate.ToString("dd/MM/yyyy HH:mm");
        public string CheckInDateDisplay => CheckInDate.HasValue ? CheckInDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
        public string CheckOutDateDisplay => CheckOutDate.HasValue ? CheckOutDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
    }
}
