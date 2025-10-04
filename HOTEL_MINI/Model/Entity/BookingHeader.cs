using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Entity
{
    public class BookingHeader
    {
        public int BookingID { get; set; }      // PK của đơn
        public int CustomerID { get; set; }
        public int CreatedBy { get; set; }      // UserID
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }      // Booked/CheckedIn/CheckedOut/Cancelled
        public string Notes { get; set; }
    }
}
