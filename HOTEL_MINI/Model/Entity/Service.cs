using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Entity
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public Decimal Price { get; set; }
        public string PriceFormatted => string.Format("{0:N0}đ", Price);
    }
}
