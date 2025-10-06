using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Response
{
    public class RevenueRoomDTO
    {
        public string RoomNumber { get; set; } 
        public int Month { get; set; }
        public int Year { get; set; }   
        public decimal RoomRevenue { get; set; } 
        public decimal ServiceRevenue { get; set; } 
        public decimal TotalRevenue { get; set; }  
    }
}
