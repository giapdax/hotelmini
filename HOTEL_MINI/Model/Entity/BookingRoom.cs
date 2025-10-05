using System;

namespace HOTEL_MINI.Model.Entity
{
    public class BookingRoom
    {
        public int BookingRoomID { get; set; }
        public int BookingID { get; set; }
        public int RoomID { get; set; }
        public int PricingID { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string Status { get; set; }   
        public string Note { get; set; }          
    }
}