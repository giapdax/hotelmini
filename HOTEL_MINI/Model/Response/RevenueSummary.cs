using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Response
{
    public class RevenueSummary
    {
        public decimal RoomCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Discount { get; set; }
        public decimal Surcharge { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

