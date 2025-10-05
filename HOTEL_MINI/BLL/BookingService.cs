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
    /// Service: chịu trách nhiệm validate/nghiệp vụ. KHÔNG truy cập UI/MessageBox.
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
                b.CustomerID = customerId;
                b.CreatedBy = createdBy;
                b.Status = string.IsNullOrWhiteSpace(b.Status) ? "Booked" : b.Status.Trim();
                ValidateBookingCore(b);
            }
        }

        // ========================= Public APIs ===========================

        public Booking AddBooking(Booking booking)
        {
            ValidateBookingCore(booking);
            return _bookingRepository.AddBooking(booking);
        }

        public Dictionary<int, int> AddBookingGroup(int customerId, int createdBy, List<Booking> roomBookings, DataTable usedServices)
        {
            ValidateBookingsForGroup(customerId, createdBy, roomBookings);
            // Repository sẽ tạo header + từng BookingRooms + (nếu có) dịch vụ
            return _bookingRepository.AddMultiRoomsBooking(customerId, createdBy, DateTime.Now, "CheckedIn", roomBookings, usedServices);
        }

        // Queries / convenience
        public List<BookingDisplay> GetTop20LatestBookingDisplays() => _bookingRepository.GetTop20LatestBookingDisplays();
        public List<BookingDisplay> GetActiveBookingDisplays() => _bookingRepository.GetActiveBookingDisplays();
        public List<BookingDisplay> GetAllBookingDisplays() => _bookingRepository.GetActiveBookingDisplays(); // mặc định
        public Booking GetLatestBookingByRoomId(int roomId) => _bookingRepository.GetLatestBookingByRoomId(roomId);
        public Booking GetBookingById(int bookingId) => _bookingRepository.GetBookingById(bookingId);
        public Booking GetActiveBookingByRoomId(int roomId) => _bookingRepository.GetActiveBookingByRoomId(roomId);

        public bool UpdateBooking(Booking b)
        {
            Ensure(b != null && b.BookingID > 0, "BookingID không hợp lệ.");
            return _bookingRepository.Update(b);
        }

        // =================== Services (attach/detach) ===================

        public List<UsedServiceDto> GetUsedServicesByBookingID(int bookingID)
            => _bookingRepository.GetUsedServicesByBookingID(bookingID);

        public bool AddOrUpdateServiceForBooking(int bookingRoomId, int serviceID, int quantity)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            Ensure(serviceID > 0, "ServiceID không hợp lệ.");
            Ensure(quantity >= 0, "Số lượng không thể âm.");
            return _bookingRepository.AddOrUpdateServiceForBooking(bookingRoomId, serviceID, quantity);
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

        public int GetAvailableServiceQuantity(int bookingRoomId, int serviceId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            Ensure(serviceId > 0, "ServiceID không hợp lệ.");
            return _bookingRepository.GetAvailableServiceQuantity(bookingRoomId, serviceId);
        }

        // =================== Tra cứu theo CCCD ===================

        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string numberID)
        {
            Ensure(!string.IsNullOrWhiteSpace(numberID), "Thiếu số CCCD/ID.");
            return _bookingRepository.GetBookingDisplaysByCustomerNumber(numberID.Trim());
        }

        public List<Booking> GetBookingsByCustomerNumberID(string customerID)
        {
            Ensure(!string.IsNullOrWhiteSpace(customerID), "Thiếu số CCCD/ID.");
            return _bookingRepository.getBookingsByCustomerNumber(customerID.Trim()); // giữ API cũ
        }

        public List<string> getPaymentMethods() => _bookingRepository.getPaymentMethods();

        // =================== Tính tiền phòng ===================

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

            // Nightly / Daily / Flat ...
            return pricing.Price;
        }

        // ======================== Check-in / Check-out ========================

        public bool CheckInBooking(int bookingRoomId)
        {
            var bk = _bookingRepository.GetBookingById(bookingRoomId);
            if (bk == null) throw new ArgumentException("Không tìm thấy booking.");
            if (!string.Equals(bk.Status, "Booked", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Chỉ có thể nhận phòng khi booking đang 'Booked'.");

            var ok = _bookingRepository.CheckInBooking(bookingRoomId, DateTime.Now);
            if (!ok) return false;

            return _roomRepository.UpdateRoomStatus(bk.RoomID, "Occupied");
        }

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
                    Note = "Checkout completed."
                };
                var invoiceID = _invoice_repository_Add(invoice); // wrapper để không phá code cũ

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

        // =============== Thanh toán theo Header (nhiều phòng) ===============

        public int? GetHeaderIdByBookingRoomId(int bookingRoomId)
            => _bookingRepository.GetHeaderIdByBookingRoomId(bookingRoomId);

        public List<Booking> GetBookingsByHeaderId(int headerBookingId)
            => _bookingRepository.GetBookingsByHeaderId(headerBookingId);

        public Booking GetBookingRoomById(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _bookingRepository.GetBookingRoomById(bookingRoomId);
        }

        /// <summary>
        /// Thanh toán theo Header; có thể trả 1 phần hoặc "trả đủ".
        /// Trả về invoiceId để UI hiển thị hoá đơn/lịch sử.
        /// </summary>
        public int PayForBookingHeader(
            List<int> bookingRoomIds,
            decimal discount,
            decimal surcharge,
            decimal payNowAmount,
            string paymentMethod,
            int userId)
        {
            Ensure(bookingRoomIds != null && bookingRoomIds.Count > 0, "Không có phòng nào được chọn.");
            Ensure(userId > 0, "UserID không hợp lệ.");

            // 1) Xác định Header
            var headerIds = bookingRoomIds.Distinct().Select(id => _bookingRepository.GetHeaderIdByBookingRoomId(id)).ToList();
            if (headerIds.Any(h => !h.HasValue)) throw new InvalidOperationException("Có phòng không hợp lệ.");
            var headerId = headerIds.First().Value;
            if (headerIds.Any(h => h.Value != headerId)) throw new InvalidOperationException("Các phòng không cùng một Booking.");

            // 2) Lấy TẤT CẢ dòng phòng thuộc header để tính
            var allLines = _bookingRepository.GetBookingsByHeaderId(headerId) ?? new List<Booking>();
            decimal roomTotal = 0m, svcTotal = 0m;

            foreach (var line in allLines)
            {
                if (!line.CheckOutDate.HasValue && line.CheckInDate.HasValue && DateTime.Now > line.CheckInDate.Value)
                    line.CheckOutDate = DateTime.Now;

                roomTotal += GetRoomCharge(line);

                var svcs = GetUsedServicesByBookingID(line.BookingID) ?? new List<UsedServiceDto>();
                foreach (var s in svcs) svcTotal += (s.Price * s.Quantity);
            }

            // 3) Tổng invoice
            var invoiceTotal = roomTotal + svcTotal + surcharge - discount;
            if (invoiceTotal < 0) invoiceTotal = 0;

            // 4) Upsert invoice (header = 1 invoice)
            var invoiceId = _invoiceRepository.UpsertInvoiceTotals(new Invoice
            {
                BookingID = headerId,
                RoomCharge = roomTotal,
                ServiceCharge = svcTotal,
                Discount = discount,
                Surcharge = surcharge,
                TotalAmount = invoiceTotal,
                IssuedBy = userId,
                IssuedAt = DateTime.Now,
                Status = "Unpaid"
            });

            // 5) Thu tiền hiện tại (nếu có)
            if (payNowAmount > 0)
            {
                _paymentRepository.AddPayment(new Payment
                {
                    InvoiceID = invoiceId,
                    Amount = payNowAmount,
                    Method = string.IsNullOrWhiteSpace(paymentMethod) ? "Cash" : paymentMethod,
                    PaymentDate = DateTime.Now,
                    Status = "Paid"
                });
            }

            // 6) Cập nhật trạng thái invoice
            var paid = _invoiceRepository.GetPaidAmount(invoiceId);
            var newStatus = (paid <= 0m) ? "Unpaid" : (paid < invoiceTotal ? "PartiallyPaid" : "Paid");
            _invoiceRepository.UpdateInvoiceStatus(invoiceId, newStatus);

            // 7) Nếu Paid => Checkout tất cả dòng phòng + chốt header + trả phòng về Available
            if (newStatus == "Paid")
            {
                var co = DateTime.Now;
                foreach (var line in allLines)
                {
                    var checkoutAt = line.CheckOutDate ?? co;
                    _bookingRepository.UpdateBookingStatusAndCheckOut(line.BookingID, "CheckedOut", checkoutAt);
                    _roomRepository.UpdateRoomStatusAfterCheckout(line.RoomID, "Available");
                }
                _bookingRepository.UpdateHeaderStatus(headerId, "CheckedOut");
            }

            return invoiceId;
        }

        public bool CancelBooking(int bookingID)
        {
            Ensure(bookingID > 0, "BookingID không hợp lệ.");
            return _bookingRepository.CancelBooking(bookingID);
        }

        // ---- small adapter để không phụ thuộc tên method private của InvoiceRepository khác version
        private int _invoice_repository_Add(Invoice invoice)
        {
            // Nếu repo có AddInvoice thì dùng; nếu không có, quăng rõ ràng
            return _invoiceRepository.AddInvoice(invoice);
        }
    }
}
