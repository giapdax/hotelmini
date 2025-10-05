using System;

namespace HOTEL_MINI.Model.Entity
{

    public class Booking
    {
        public int BookingID { get; set; }
        public int CustomerID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }       
        public string Notes { get; set; }
    }
}
