using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HOTEL_MINI.BLL
{
    /// <summary>
    /// Toàn bộ validate nằm ở Service. Repository chỉ làm việc DB và KHÔNG hiển thị UI / MessageBox.
    /// </summary>
    public class BookingService
    {
        private readonly BookingRepository _bookingRepository;
        private readonly RoomPricingRepository _roomPricingRepository;
        private readonly RoomRepository _roomRepository;
        private readonly InvoiceRepository _invoiceRepository;
        private readonly PaymentRepository _paymentRepository;

        public BookingService()
        {
            _bookingRepository = new BookingRepository();
            _roomPricingRepository = new RoomPricingRepository();
            _roomRepository = new RoomRepository();
            _invoiceRepository = new InvoiceRepository();
            _paymentRepository = new PaymentRepository();
        }

        // ====================== Helpers & Validation ======================

        private static void Ensure(bool condition, string message)
        {
            if (!condition) throw new ArgumentException(message);
        }

        private void ValidateBookingCore(Booking b)
        {
            Ensure(b != null, "Booking rỗng.");

            Ensure(b.CustomerID > 0, "CustomerID không hợp lệ.");
            Ensure(b.RoomID > 0, "RoomID không hợp lệ.");
            Ensure(b.PricingID > 0, "PricingID không hợp lệ.");
            Ensure(b.CreatedBy > 0, "CreatedBy (UserID) không hợp lệ.");

            Ensure(b.CheckInDate.HasValue, "Thiếu Check-in.");
            Ensure(b.CheckOutDate.HasValue, "Thiếu Check-out.");
            Ensure(b.CheckOutDate.Value > b.CheckInDate.Value, "Check-out phải lớn hơn Check-in.");

            // phòng rảnh?
            bool available = _roomRepository.IsRoomAvailable(b.RoomID, b.CheckInDate.Value, b.CheckOutDate.Value);
            Ensure(available, "Khoảng thời gian đã có booking khác.");
        }

        private void ValidateBookingsForGroup(int customerId, int createdBy, IList<Booking> items)
        {
            Ensure(customerId > 0, "CustomerID không hợp lệ.");
            Ensure(createdBy > 0, "CreatedBy (UserID) không hợp lệ.");
            Ensure(items != null && items.Count > 0, "Danh sách phòng trống.");

            foreach (var b in items)
            {
                // áp chuẩn các trường bắt buộc cho group
                b.CustomerID = customerId;
                b.CreatedBy = createdBy;
                b.Status = string.IsNullOrWhiteSpace(b.Status) ? "Booked" : b.Status?.Trim();

                ValidateBookingCore(b);
            }
        }

        // ========================= Public APIs ===========================

        public Booking AddBooking(Booking booking)
        {
            ValidateBookingCore(booking);
            return _bookingRepository.AddBooking(booking);
        }

        /// <summary>
        /// Đặt nhiều phòng trong cùng 1 header booking. Trả về map RoomID -> BookingRoomID.
        /// </summary>
        public Dictionary<int, int> AddBookingGroup(int customerId, int createdBy, List<Booking> roomBookings, DataTable usedServices)
        {
            ValidateBookingsForGroup(customerId, createdBy, roomBookings);
            // Repository sẽ tạo header + từng BookingRooms + (nếu có) dịch vụ
            return _bookingRepository.AddMultiRoomsBooking(customerId, createdBy, DateTime.Now, "CheckedIn", roomBookings, usedServices);
        }

        // ==================== Query / Update convenience =================

        public List<BookingDisplay> GetTop20LatestBookingDisplays() => _bookingRepository.GetTop20LatestBookingDisplays();
        public Booking GetLatestBookingByRoomId(int roomId) => _bookingRepository.GetLatestBookingByRoomId(roomId);
        public Booking GetBookingById(int bookingId) => _bookingRepository.GetBookingById(bookingId);
        public bool UpdateBooking(Booking b)
        {
            Ensure(b != null && b.BookingID > 0, "BookingID không hợp lệ.");
            return _bookingRepository.Update(b);
        }

        public List<UsedServiceDto> GetUsedServicesByBookingID(int bookingID)
            => _bookingRepository.GetUsedServicesByBookingID(bookingID);

        public bool AddOrUpdateServiceForBooking(int bookingID, int serviceID, int quantity)
        {
            Ensure(bookingID > 0, "BookingID không hợp lệ.");
            Ensure(serviceID > 0, "ServiceID không hợp lệ.");
            Ensure(quantity > 0, "Số lượng phải > 0.");
            return _bookingRepository.AddOrUpdateServiceForBooking(bookingID, serviceID, quantity);
        }

        public void RemoveServiceFromBooking(int bookingServiceId)
        {
            Ensure(bookingServiceId > 0, "BookingServiceID không hợp lệ.");
            _bookingRepository.RemoveServiceFromBooking(bookingServiceId);
        }

        public void UpdateServiceQuantity(int bookingServiceId, int newQuantity)
        {
            Ensure(bookingServiceId > 0, "BookingServiceID không hợp lệ.");
            Ensure(newQuantity >= 0, "Số lượng không thể âm.");
            _bookingRepository.UpdateServiceQuantity(bookingServiceId, newQuantity);
        }

        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string numberID)
        {
            Ensure(!string.IsNullOrWhiteSpace(numberID), "Thiếu số CCCD/ID.");
            return _bookingRepository.GetBookingDisplaysByCustomerNumber(numberID.Trim());
        }

        public List<Booking> GetBookingsByCustomerNumberID(string customerID)
        {
            Ensure(!string.IsNullOrWhiteSpace(customerID), "Thiếu số CCCD/ID.");
            return _bookingRepository.getBookingsByCustomerNumber(customerID.Trim());
        }

        public List<string> getPaymentMethods() => _bookingRepository.getPaymentMethods();

        // =============== Tính tiền phòng (tham chiếu Pricing) ===============
        public decimal GetRoomCharge(Booking booking)
        {
            Ensure(booking != null, "Booking rỗng.");
            Ensure(booking.PricingID > 0, "PricingID không hợp lệ.");
            Ensure(booking.CheckInDate.HasValue, "Thiếu Check-in.");

            var pricing = _roomPricingRepository.GetPricingTypeById(booking.PricingID);
            Ensure(pricing != null, "Không thấy thông tin giá (Pricing).");

            if (pricing.PricingType.Equals("Hourly", StringComparison.OrdinalIgnoreCase))
            {
                Ensure(booking.CheckOutDate.HasValue, "Thiếu Check-out để tính tiền theo giờ.");
                var hours = (booking.CheckOutDate.Value - booking.CheckInDate.Value).TotalHours;
                if (hours <= 0) return 0m;
                var rounded = (int)Math.Ceiling(hours);
                return rounded * pricing.Price;
            }

            // Nightly/Daily/Weekly… -> đơn giá niêm yết
            return pricing.Price;
        }

        // ======================== Checkout =========================
        public void Checkout(Booking booking, decimal roomCharge, decimal serviceCharge,
                             decimal discount, decimal surcharge, string paymentMethod, int currentUserID)
        {
            Ensure(booking != null && booking.BookingID > 0, "Booking không hợp lệ.");
            Ensure(currentUserID > 0, "UserID không hợp lệ.");
            Ensure(booking.CheckOutDate.HasValue, "Thiếu Check-out.");

            using (var scope = new System.Transactions.TransactionScope())
            {
                var total = roomCharge + serviceCharge + surcharge - discount;
                if (total < 0) total = 0;

                var invoice = new Invoice
                {
                    BookingID = booking.BookingID,
                    RoomCharge = roomCharge,
                    ServiceCharge = serviceCharge,
                    Surcharge = surcharge,
                    Discount = discount,
                    TotalAmount = total,
                    IssuedAt = DateTime.Now,
                    IssuedBy = currentUserID,
                    Status = "Paid",
                    Note = $"Checkout completed at {booking.CheckOutDate}!"
                };
                var invoiceID = _invoiceRepository.AddInvoice(invoice);

                var payment = new Payment
                {
                    InvoiceID = invoiceID,
                    Amount = total,
                    PaymentDate = DateTime.Now,
                    Method = paymentMethod,
                    Status = "Paid"
                };
                _paymentRepository.AddPayment(payment);

                var ok = _bookingRepository.UpdateBookingStatusAndCheckOut(booking.BookingID, "CheckedOut", booking.CheckOutDate.Value);
                Ensure(ok, "Không thể cập nhật trạng thái booking.");

                ok = _roomRepository.UpdateRoomStatusAfterCheckout(booking.RoomID, "Available");
                Ensure(ok, "Không thể cập nhật trạng thái phòng.");

                scope.Complete();
            }
        }
        public Booking GetActiveBookingByRoomId(int roomId) => _bookingRepository.GetActiveBookingByRoomId(roomId);

        public bool CheckInBooking(int bookingID)
        {
            var bk = _bookingRepository.GetBookingById(bookingID);
            if (bk == null) throw new ArgumentException("Không tìm thấy booking.");
            if (!string.Equals(bk.Status, "Booked", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Chỉ có thể nhận phòng khi booking đang 'Booked'.");

            var ok = _bookingRepository.CheckInBooking(bookingID, DateTime.Now);
            if (!ok) return false;

            // cập nhật trạng thái phòng sang Occupied
            return _roomRepository.UpdateRoomStatus(bk.RoomID, "Occupied");
        }


        public bool CancelBooking(int bookingID)
        {
            Ensure(bookingID > 0, "BookingID không hợp lệ.");
            return _bookingRepository.CancelBooking(bookingID);
        }
    }
}
