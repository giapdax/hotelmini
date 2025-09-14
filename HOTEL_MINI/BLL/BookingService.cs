using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace HOTEL_MINI.BLL
{
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly RoomPricingRepository _roomPricingRepository;
        private readonly InvoiceRepository _invoiceRepository;
        private readonly PaymentRepository _paymentRepository;
        private readonly RoomRepository _roomRepository;
        public BookingService()
        {
            _bookingRepository = new BookingRepository();
            _roomPricingRepository = new RoomPricingRepository();
            _invoiceRepository = new InvoiceRepository();
            _paymentRepository = new PaymentRepository();
            _roomRepository = new RoomRepository();
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
        public decimal GetRoomCharge(Booking booking)
        {
            var pricing = _roomPricingRepository.GetPricingTypeById(booking.PricingID);
            if (pricing == null)
                throw new Exception("Không thấy thông tin Pricing");

            if (!booking.CheckInDate.HasValue)
                throw new Exception("Booking chưa có CheckInDate.");

            // --- Hourly ---
            if (string.Equals(pricing.PricingType, "Hourly", StringComparison.OrdinalIgnoreCase))
            {
                if (!booking.CheckOutDate.HasValue)
                    throw new Exception("Booking chưa có CheckOutDate. Vui lòng chọn giờ checkout để tính tiền.");

                DateTime checkIn = booking.CheckInDate.Value;
                DateTime checkOut = booking.CheckOutDate.Value;

                if (checkOut <= checkIn) return 0m;

                double totalHours = (checkOut - checkIn).TotalHours;
                int roundedHours = (int)Math.Ceiling(totalHours);

                return roundedHours * pricing.Price;
            }

            // --- Các loại khác (Nightly, Weekly, ...) ---
            return pricing.Price;
        }
        public List<string> getPaymentMethods() => _bookingRepository.getPaymentMethods();
        public void Checkout(int bookingID, int roomID, decimal roomCharge, decimal serviceCharge,
                            decimal discount, decimal surcharge, string paymentMethod, int currentUserID)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    // 1. Tạo Invoice
                    var invoice = new Invoice
                    {
                        BookingID = bookingID,
                        RoomCharge = roomCharge,
                        ServiceCharge = serviceCharge,
                        Surcharge = surcharge,
                        Discount = discount,
                        TotalAmount = roomCharge + serviceCharge + surcharge - discount,
                        IssuedAt = DateTime.Now,
                        IssuedBy = currentUserID,
                        Status = "Paid",
                        Note = $"Checkout completed at {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                    };

                    int invoiceID = _invoiceRepository.AddInvoice(invoice);

                    // 2. Tạo Payment
                    var payment = new Payment
                    {
                        InvoiceID = invoiceID,
                        Amount = invoice.TotalAmount,
                        PaymentDate = DateTime.Now,
                        Method = paymentMethod,
                        Status = "Completed"
                    };

                    _paymentRepository.AddPayment(payment);

                    bool bookingUpdated = _bookingRepository.UpdateBookingStatus(bookingID, "CheckedOut");
                    if (!bookingUpdated)
                    {
                        throw new Exception("Không thể cập nhật trạng thái booking");
                    }

                    // 5. Cập nhật trạng thái phòng
                    bool roomUpdated = _roomRepository.UpdateRoomStatusAfterCheckout(roomID, "Available");
                    if (!roomUpdated)
                    {
                        throw new Exception("Không thể cập nhật trạng thái phòng");
                    }



                    transaction.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Checkout failed: {ex.Message}", ex);
                }
            }
        }
        public bool CancelBooking(int bookingID)
        {
            return _bookingRepository.CancelBooking(bookingID);
        }
        //public void Checkout(Booking booking, decimal surcharge, decimal discount, string paymentMethod, int currentUserId)
        //{
        //    if (booking.CheckOutDate == null)
        //    {
        //        booking.CheckOutDate = DateTime.Now;
        //    }

        //    // Tính room charge & service charge
        //    decimal roomCharge = GetRoomCharge(booking);
        //    decimal serviceCharge = GetUsedServicesByBookingID(booking.BookingID).Sum(s => s.Total);

        //    decimal totalAmount = roomCharge + serviceCharge + surcharge - discount;
        //    if (totalAmount < 0) totalAmount = 0;

        //    // Tạo invoice
        //    var invoice = new Invoice
        //    {
        //        BookingID = booking.BookingID,
        //        RoomCharge = roomCharge,
        //        ServiceCharge = serviceCharge,
        //        ExtraFee = surcharge,
        //        Discount = discount,
        //        TotalAmount = totalAmount,
        //        IssuedAt = DateTime.Now,
        //        IssuedBy = currentUserId,
        //        Status = "Unpaid",
        //        Note = ""
        //    };
        //    int invoiceId = _invoiceRepository.AddInvoice(invoice);

        //    // Tạo payment
        //    var payment = new Payment
        //    {
        //         = invoiceId,
        //        Amount = totalAmount,
        //        PaymentDate = DateTime.Now,
        //        Method = paymentMethod,
        //        Status = "Completed"
        //    };
        //    _paymentRepo.AddPayment(payment);

        //    // Cập nhật trạng thái booking
        //    booking.Status = "CheckedOut";
        //    _bookingRepo.UpdateBooking(booking);

        //    // Cập nhật trạng thái phòng
        //    _roomService.UpdateRoomStatus(booking.RoomID, "Available");
        //}
    }
}
