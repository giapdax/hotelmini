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

        // Property hiển thị định dạng
        public string PriceFormatted => Price.ToString("N0") + " đ";

        // Tổng tiền của dịch vụ đó
        public string TotalFormatted => (Price * Quantity).ToString("N0") + " đ";
    }
}
