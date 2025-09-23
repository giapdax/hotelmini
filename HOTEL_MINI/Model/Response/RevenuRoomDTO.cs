using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Response
{
    public class RevenueRoomDTO
    {
        public string RoomNumber { get; set; }   // Số phòng
        public int Month { get; set; }           // Tháng
        public int Year { get; set; }            // Năm
        public decimal RoomRevenue { get; set; } // Tiền phòng
        public decimal ServiceRevenue { get; set; } // Tiền dịch vụ
        public decimal TotalRevenue { get; set; }   // Tổng doanh thu
    }
}
