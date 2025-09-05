using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.BLL
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        public BookingService()
        {
            _bookingRepository = new BookingRepository();
        }
        public Booking AddBooking(Model.Entity.Booking booking)
        {
            return _bookingRepository.AddBooking(booking);
        }
        public Booking GetLatestBookingByRoomId(int roomId) => _bookingRepository.GetLatestBookingByRoomId(roomId);
        public bool UpdateBooking(Booking b) => _bookingRepository.Update(b);
    }
}
