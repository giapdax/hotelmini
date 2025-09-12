using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Entity
{
    public class RoomPricing
    {
        public int PricingID { get; set; }
        public int RoomTypeID { get; set; }
        public string PricingType { get; set; } 
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
