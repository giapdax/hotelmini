using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Response
{
    public class CheckoutDto
    {
        // Thông tin phòng & booking
        public int BookingID { get; set; }
        public string RoomName { get; set; }
        public decimal RoomCharge { get; set; }

        // Dịch vụ
        public List<UsedServiceDto> Services { get; set; } = new List<UsedServiceDto>();
        public decimal ServiceCharge { get; set; }

        // Các khoản thêm/bớt
        public decimal ExtraFee { get; set; } = 0;
        public decimal Discount { get; set; } = 0;

        // Tổng tiền
        public decimal TotalAmount => RoomCharge + ServiceCharge + ExtraFee - Discount;

        // Nhân viên + thanh toán
        public string StaffName { get; set; }
        public string PaymentMethod { get; set; }
        public string Note { get; set; }
    }
}

