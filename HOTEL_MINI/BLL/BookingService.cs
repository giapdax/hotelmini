using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;

namespace HOTEL_MINI.BLL
{
    /// <summary>
    /// BLL cấp Booking (header) + các truy vấn/tác vụ line cần cho UI.
    /// - Không truy vấn trực tiếp SQL; dùng Repository.
    /// - Không UI/MessageBox; ném exception ra ngoài.
    /// </summary>
    public class BookingService
    {
        private readonly BookingRepository _bookingRepo;
        private readonly BookingRoomRepository _lineRepo;
        private readonly RoomPricingRepository _pricingRepo;
        private readonly RoomRepository _roomRepo;
        private readonly BookingRoomServiceRepository _brsRepo; // đọc/ghi dịch vụ theo line

        public BookingService()
        {
            _bookingRepo = new BookingRepository();
            _lineRepo = new BookingRoomRepository();
            _pricingRepo = new RoomPricingRepository();
            _roomRepo = new RoomRepository();
            _brsRepo = new BookingRoomServiceRepository();
        }

        private static void Ensure(bool cond, string msg)
        {
            if (!cond) throw new ArgumentException(msg);
        }

        // ================= LIST/QUERY cho UIs (UcBookingRoom, UcInvoice, ...) =================

        /// <summary>Danh sách booking-ROOM đang hoạt động (Booked/CheckedIn).</summary>
        public List<BookingDisplay> GetActiveBookingDisplays()
            => _lineRepo.GetActiveBookingDisplays();

        /// <summary>Top-N booking gần nhất (phục vụ hoá đơn, lịch sử).</summary>
        public List<BookingDisplay> GetTop20LatestBookingDisplays(bool onlyCheckedOut = false)
            => _bookingRepo.GetTopLatestBookingDisplays(20, onlyCheckedOut);

        /// <summary>BookingDisplay theo CCCD/IDNumber (ghép cho filter).</summary>
        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string idNumber)
        {
            Ensure(!string.IsNullOrWhiteSpace(idNumber), "Thiếu CCCD/ID.");
            return _bookingRepo.GetBookingDisplaysByCustomerNumber(idNumber.Trim());
        }

        // ================= Hành động line =================

        public bool CheckInBookingRoom(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");

            var line = _lineRepo.GetBookingById(bookingRoomId);
            if (line == null) throw new ArgumentException("Không tìm thấy booking.");
            if (!string.Equals(line.Status, "Booked", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Chỉ nhận phòng khi line đang 'Booked'.");

            return _lineRepo.CheckInBooking(bookingRoomId, DateTime.Now);
        }

        public bool CancelBooking(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _lineRepo.CancelBooking(bookingRoomId);
        }

        // ================= Tiện ích dùng ở nhiều form =================

        public Booking GetBookingById(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _lineRepo.GetBookingById(bookingRoomId);
        }

        public int? GetHeaderIdByBookingRoomId(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _lineRepo.GetHeaderIdByBookingRoomId(bookingRoomId);
        }

        public Customer GetCustomerByHeaderId(int headerBookingId)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            return _bookingRepo.GetCustomerByHeaderId(headerBookingId);
        }

        /// <summary>Trả về tất cả các line (BookingRooms) dưới cùng một header.</summary>
        public List<Booking> GetBookingsByHeaderId(int headerBookingId)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            return _bookingRepo.GetBookingsByHeaderId(headerBookingId);
        }

        /// <summary>Danh sách dịch vụ đã dùng cho một line (BookingRoomID).</summary>
        public List<UsedServiceDto> GetUsedServicesByBookingID(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _brsRepo.GetUsedServicesByBookingRoomId(bookingRoomId);
        }

        /// <summary>Danh sách phương thức thanh toán (enum).</summary>
        public List<string> GetPaymentMethods() => _bookingRepo.GetPaymentMethods();

        // Giữ thêm tên cũ để tương thích code cũ.
        public List<string> getPaymentMethods() => GetPaymentMethods();

        /// <summary>
        /// Tính tiền phòng cho 1 line dựa theo PricingID + khoảng thời gian.
        /// Giản lược: Hourly = ceil(hours) * unit; Daily/Nightly/Weekly = unit.
        /// </summary>
        public decimal GetRoomCharge(Booking line)
        {
            Ensure(line != null, "Line rỗng.");
            Ensure(line.PricingID > 0, "PricingID không hợp lệ.");
            Ensure(line.CheckInDate.HasValue, "Thiếu Check-in.");

            var pricing = _pricingRepo.GetPricingTypeById(line.PricingID);
            Ensure(pricing != null, "Không thấy thông tin giá.");

            if (pricing.PricingType.Equals("Hourly", StringComparison.OrdinalIgnoreCase))
            {
                Ensure(line.CheckOutDate.HasValue, "Thiếu Check-out để tính giờ.");
                var hours = (line.CheckOutDate.Value - line.CheckInDate.Value).TotalHours;
                if (hours <= 0) return 0m;
                var rounded = (int)Math.Ceiling(hours);
                return rounded * pricing.Price;
            }

            // Nightly/Daily/Weekly: bản đơn giản trả về đơn giá (tùy nghiệp vụ có thể nâng cấp).
            return pricing.Price;
        }

        public Dictionary<int, int> AddBookingGroup(
            int customerId,
            int createdBy,
            List<Booking> roomBookings,
            DataTable usedServices = null)
        {
            // ---- Validate ----
            Ensure(customerId > 0, "CustomerID không hợp lệ.");
            Ensure(createdBy > 0, "UserID không hợp lệ.");
            Ensure(roomBookings != null && roomBookings.Count > 0, "Chưa có phòng để đặt.");

            foreach (var b in roomBookings)
            {
                Ensure(b.RoomID > 0, "RoomID không hợp lệ.");
                Ensure(b.PricingID > 0, "PricingID không hợp lệ.");
                Ensure(b.CheckInDate.HasValue && b.CheckOutDate.HasValue, "Thiếu thời gian Check-in/out.");
                if (b.CheckOutDate.Value <= b.CheckInDate.Value)
                    throw new ArgumentException("Check-out phải lớn hơn Check-in.");

                // check overlap
                if (_lineRepo.IsRoomOverlapped(b.RoomID, b.CheckInDate.Value, b.CheckOutDate.Value))
                    throw new InvalidOperationException($"Phòng {b.RoomID} đã có lịch trong khoảng thời gian chọn.");
            }

            // ---- Create header ----
            var notesHeader = "";
            foreach (var b in roomBookings)
                if (!string.IsNullOrWhiteSpace(b.Notes)) { notesHeader = b.Notes; break; }

            var headerId = _bookingRepo.AddHeader(
                customerId: customerId,
                createdBy: createdBy,
                bookingDate: DateTime.Now,
                status: "Booked",
                notes: notesHeader
            );

            // ---- Normalize lines ----
            var normalized = new List<Booking>();
            foreach (var b in roomBookings)
            {
                normalized.Add(new Booking
                {
                    RoomID = b.RoomID,
                    PricingID = b.PricingID,
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    Status = string.IsNullOrWhiteSpace(b.Status) ? "Booked" : b.Status,
                    Notes = b.Notes
                });
            }

            // ---- Insert lines (transaction ở repo) ----
            var mapRoomToLine = _lineRepo.AddLinesForHeader(headerId, normalized);

            // ---- Bulk upsert services nếu có ----
            if (usedServices != null && usedServices.Rows.Count > 0)
            {
                _brsRepo.BulkUpsertServices(usedServices, mapRoomToLine);
            }

            return mapRoomToLine;
        }
    }
}
