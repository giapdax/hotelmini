using System;

namespace HOTEL_MINI.Model.Response
{

    public class BookingFlatDisplay
    {
        public int HeaderBookingID { get; set; }
        public int BookingRoomID { get; set; }
        public string CustomerIDNumber { get; set; }

        public string CustomerName { get; set; } 
        public string CustomerPhone { get; set; }

        public string RoomNumber { get; set; }
        public string EmployeeName { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
    }

}