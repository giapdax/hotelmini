using System;

namespace HOTEL_MINI.Model.Entity
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public Decimal Price { get; set; }
        public bool IsActive { get; set; } 

        public string PriceFormatted => string.Format("{0:N0}đ", Price);
    }
}