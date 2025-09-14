using HOTEL_MINI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.BLL
{
    public class CheckoutService
    {
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
        private readonly ServicesRepository _servicesRepository;
        public CheckoutService()
        {
            _bookingService = new BookingService();
            _roomService = new RoomService();
            _servicesRepository = new ServicesRepository();
        }
    }
}
