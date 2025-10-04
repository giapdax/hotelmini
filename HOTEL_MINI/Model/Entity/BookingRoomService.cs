using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Entity
{
    public class BookingRoomService
    {
        public int BookingRoomServiceID { get; set; }
        public int BookingRoomID { get; set; }  
        public int ServiceID { get; set; }
        public int Quantity { get; set; }
    }
}
