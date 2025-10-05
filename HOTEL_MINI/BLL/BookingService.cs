using HOTEL_MINI.Common;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HOTEL_MINI.BLL
{
    /// <summary>
    /// BLL cấp Booking (header) + các thao tác/tiện ích cho lines (BookingRooms) phục vụ UI.
    /// - Không truy vấn trực tiếp SQL; chỉ gọi Repository.
    /// - Không hiển thị UI; ném exception lên trên.
    /// </summary>
    public class BookingService
    {
        private readonly BookingRepository _bookingRepo;              // CRUD header, view phẳng, lookup khách
        private readonly BookingRoomRepository _lineRepo;             // CRUD line
        private readonly RoomPricingRepository _pricingRepo;          // tra giá
        private readonly BookingRoomServiceRepository _brsRepo;       // dịch vụ theo line (nếu dùng)

        public BookingService()
        {
            _bookingRepo = new BookingRepository();
            _lineRepo = new BookingRoomRepository();
            _pricingRepo = new RoomPricingRepository();
            _brsRepo = new BookingRoomServiceRepository();
        }

        private static void Ensure(bool cond, string msg)
        {
            if (!cond) throw new ArgumentException(msg);
        }

        // ================= LIST/QUERY cho UI =================

        /// <summary>Danh sách booking-ROOM đang hoạt động (Booked/CheckedIn).</summary>
        public List<BookingDisplay> GetActiveBookingDisplays()
            => _lineRepo.GetActiveBookingDisplays();

        /// <summary>Top 20 booking gần nhất. onlyCheckedOut=true để lấy lịch sử đã trả.</summary>
        public List<BookingDisplay> GetTop20LatestBookingDisplays(bool onlyCheckedOut = false)
            => _bookingRepo.GetTopLatestBookingDisplays(20, onlyCheckedOut);

        /// <summary>BookingDisplay theo CCCD/IDNumber.</summary>
        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string idNumber)
        {
            Ensure(!string.IsNullOrWhiteSpace(idNumber), "Thiếu CCCD/ID.");
            return _bookingRepo.GetBookingDisplaysByCustomerNumber(idNumber.Trim());
        }

        /// <summary>Trả về tất cả dòng phòng (BookingRooms) dưới cùng một header.</summary>
        public List<BookingRoom> GetBookingsByHeaderId(int headerBookingId)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            return _lineRepo.GetBookingsByHeaderId(headerBookingId);
        }

        /// <summary>Lấy 1 dòng phòng theo BookingRoomID.</summary>
        public BookingRoom GetBookingById(int bookingRoomId)
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

        public List<UsedServiceDto> GetUsedServicesByBookingID(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _brsRepo.GetUsedServicesByBookingRoomId(bookingRoomId);
        }

        public List<string> GetPaymentMethods() => _bookingRepo.GetPaymentMethods();
        // Giữ tên cũ cho code legacy
        public List<string> getPaymentMethods() => GetPaymentMethods();

        // ================= HÀNH ĐỘNG TRÊN LINE =================

        public bool CheckInBookingRoom(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");

            var line = _lineRepo.GetBookingById(bookingRoomId); // BookingRoom
            if (line == null) throw new ArgumentException("Không tìm thấy booking line.");
            if (!string.Equals(line.Status, "Booked", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Chỉ nhận phòng khi line đang 'Booked'.");

            return _lineRepo.CheckInBooking(bookingRoomId, DateTime.Now);
        }

        public bool CancelBooking(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _lineRepo.CancelBooking(bookingRoomId);
        }

        public bool UpdateLineStatusAndCheckout(int bookingRoomId, DateTime checkoutAt)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _lineRepo.UpdateBookingStatusAndCheckOut(bookingRoomId, "CheckedOut", checkoutAt);
        }

        public bool SetLinesCheckedOutByHeader(int headerBookingId, DateTime? checkoutAt = null)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            return _lineRepo.SetLinesCheckedOutByHeader(headerBookingId, checkoutAt ?? DateTime.Now);
        }

        // ================= TÍNH GIÁ / TẠO NHÓM BOOKING =================

        /// <summary>
        /// Tính tiền phòng cho 1 LINE dựa theo PricingID + khoảng thời gian.
        /// - Hourly = ceil(hours) * unit
        /// - Daily/Nightly/Weekly = unit (bản đơn giản)
        /// </summary>
        public decimal GetRoomCharge(BookingRoom line)
        {
            Ensure(line != null, "Line rỗng.");
            Ensure(line.PricingID > 0, "PricingID không hợp lệ.");
            Ensure(line.CheckInDate.HasValue, "Thiếu Check-in.");

            var pricing = _pricingRepo.GetPricingTypeById(line.PricingID);
            Ensure(pricing != null, "Không thấy thông tin đơn giá.");

            if (pricing.PricingType.Equals("Hourly", StringComparison.OrdinalIgnoreCase))
            {
                Ensure(line.CheckOutDate.HasValue, "Thiếu Check-out để tính giờ.");
                var hours = (line.CheckOutDate.Value - line.CheckInDate.Value).TotalHours;
                if (hours <= 0) return 0m;
                var rounded = (int)Math.Ceiling(hours);
                return rounded * pricing.Price;
            }

            return pricing.Price;
        }

        /// <summary>
        /// Tạo một booking header + nhiều lines.
        /// Trả về map (RoomID -> BookingRoomID) đã tạo.
        /// </summary>
        public Dictionary<int, int> AddBookingGroup(
            int customerId,
            int createdBy,
            List<BookingRoom> roomBookings,
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

                // chặn trùng lịch
                if (_lineRepo.IsRoomOverlapped(b.RoomID, b.CheckInDate.Value, b.CheckOutDate.Value))
                    throw new InvalidOperationException($"Phòng {b.RoomID} đã có lịch trong khoảng thời gian chọn.");
            }

            // ---- Create header ----
            var notesHeader = "";
            foreach (var b in roomBookings)
                if (!string.IsNullOrWhiteSpace(b.Note)) { notesHeader = b.Note; break; }

            var headerId = _bookingRepo.AddHeader(
                customerId: customerId,
                createdBy: createdBy,
                bookingDate: DateTime.Now,
                status: "CheckedIn",
                notes: notesHeader
            );

            // ---- Normalize lines ----
            var normalized = new List<BookingRoom>();
            foreach (var b in roomBookings)
            {
                normalized.Add(new BookingRoom
                {
                    RoomID = b.RoomID,
                    PricingID = b.PricingID,
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    Status = string.IsNullOrWhiteSpace(b.Status) ? "Booked" : b.Status,
                    Note = b.Note
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

        // ================= TIỆN ÍCH DÙNG CHUNG =================

        public List<BookingFlatDisplay> GetAllBookingFlatDisplays()
            => _bookingRepo.GetAllBookingFlatDisplays();

        public List<int> GetBookingRoomIdsByHeader(int headerBookingId)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            return _bookingRepo.GetBookingRoomIdsByHeader(headerBookingId);
        }

        /// <summary>
        /// Tạo (hoặc lấy) hóa đơn mở từ 1 BookingRoomID.
        /// Trả về InvoiceID. Chỉ resolve về BookingID (header) rồi gọi InvoiceRepository.
        /// </summary>
        public int CreateInvoiceFromRoomLine(int bookingRoomId, int issuedBy)
        {
            using (var conn = new SqlConnection(ConfigHelper.GetConnectionString()))
            {
                conn.Open();
                int bookingId;
                using (var cmd = new SqlCommand(
                    "SELECT BookingID FROM BookingRooms WHERE BookingRoomID=@id", conn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = bookingRoomId;
                    var o = cmd.ExecuteScalar();
                    if (o == null) throw new InvalidOperationException($"BookingRoomID {bookingRoomId} không tồn tại.");
                    bookingId = Convert.ToInt32(o);
                }

                var invRepo = new InvoiceRepository();
                return invRepo.CreateOrGetOpenInvoice(bookingId, 0m, 0m, 0m, 0m, issuedBy);
            }
        }
    }
}
