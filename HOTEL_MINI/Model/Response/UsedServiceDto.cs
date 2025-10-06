using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Response
{
    public class UsedServiceDto
    {
        public int BookingServiceID { get; set; }
        public int BookingID { get; set; }
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
