using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Response
{
    public class RoomStatistics
    {
        public int TotalRooms { get; set; }
        public int AvailableRooms { get; set; }
        public int BookedRooms { get; set; }
        public int OccupiedRooms { get; set; }
        public int MaintenanceRooms { get; set; }
    }
}
