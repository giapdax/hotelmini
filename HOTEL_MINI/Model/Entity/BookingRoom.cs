using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
