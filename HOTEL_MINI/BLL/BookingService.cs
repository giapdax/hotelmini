using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
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
        public List<UsedServiceDto> GetUsedServicesByBookingID(int bookingID)
        {
            return _bookingRepository.GetUsedServicesByBookingID(bookingID);
        }
        public bool AddOrUpdateServiceForBooking(int bookingID, int serviceID, int quantity)
        {
            return _bookingRepository.AddOrUpdateServiceForBooking(bookingID, serviceID, quantity);
        }
        public void RemoveServiceFromBooking(int bookingServiceId)
        {
            _bookingRepository.RemoveServiceFromBooking(bookingServiceId);
        }
        public void UpdateServiceQuantity(int bookingServiceId, int newQuantity)
        {
            _bookingRepository.UpdateServiceQuantity(bookingServiceId, newQuantity);
        }
    }
}
