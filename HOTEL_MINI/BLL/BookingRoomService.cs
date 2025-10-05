using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response; 
using System;
using System.Collections.Generic;
using System.Linq;

namespace HOTEL_MINI.BLL
{

    public class BookingRoomService
    {
        private readonly BookingRoomRepository _repo;

        public BookingRoomService()
        {
            _repo = new BookingRoomRepository();
        }

        // ------------- Helpers -------------
        private static void Ensure(bool cond, string msg)
        {
            if (!cond) throw new ArgumentException(msg);
        }

        private static void ValidateLineCore(Booking line, bool requireDates = false)
        {
            Ensure(line != null, "Line rỗng.");
            Ensure(line.RoomID > 0, "RoomID không hợp lệ.");
            Ensure(line.PricingID > 0, "PricingID không hợp lệ.");

            if (requireDates)
            {
                Ensure(line.CheckInDate.HasValue, "Thiếu Check-in.");
                Ensure(line.CheckOutDate.HasValue, "Thiếu Check-out.");
                Ensure(line.CheckOutDate.Value > line.CheckInDate.Value, "Check-out phải sau Check-in.");
            }
            else if (line.CheckInDate.HasValue && line.CheckOutDate.HasValue)
            {
                Ensure(line.CheckOutDate.Value > line.CheckInDate.Value, "Check-out phải sau Check-in.");
            }
        }

        // ------------- Queries (Display/Lookup) -------------
        public List<BookingDisplay> GetActiveBookingDisplays()
            => _repo.GetActiveBookingDisplays();

        public List<BookingDisplay> GetTop20LatestBookingDisplays()
            => _repo.GetTop20LatestBookingDisplays();

        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string idNumber)
        {
            Ensure(!string.IsNullOrWhiteSpace(idNumber), "Thiếu CCCD/ID.");
            return _repo.GetBookingDisplaysByCustomerNumber(idNumber.Trim());
        }

        public List<Booking> GetBookingsByCustomerNumber(string numberID)
        {
            Ensure(!string.IsNullOrWhiteSpace(numberID), "Thiếu CCCD/ID.");
            return _repo.GetBookingsByCustomerNumber(numberID.Trim());
        }

        public Booking GetBookingById(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _repo.GetBookingById(bookingRoomId);
        }

        public Booking GetBookingRoomById(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _repo.GetBookingRoomById(bookingRoomId);
        }

        public Booking GetLatestBookingByRoomId(int roomId)
        {
            Ensure(roomId > 0, "RoomID không hợp lệ.");
            return _repo.GetLatestBookingByRoomId(roomId);
        }

        public Booking GetActiveBookingByRoomId(int roomId)
        {
            Ensure(roomId > 0, "RoomID không hợp lệ.");
            return _repo.GetActiveBookingByRoomId(roomId);
        }

        public bool IsRoomOverlapped(int roomId, DateTime checkIn, DateTime checkOut)
        {
            Ensure(roomId > 0, "RoomID không hợp lệ.");
            Ensure(checkOut > checkIn, "Check-out phải sau Check-in.");
            return _repo.IsRoomOverlapped(roomId, checkIn, checkOut);
        }

        public int? GetHeaderIdByBookingRoomId(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _repo.GetHeaderIdByBookingRoomId(bookingRoomId);
        }

        public string GetRoomNumberById(int roomId)
        {
            Ensure(roomId > 0, "RoomID không hợp lệ.");
            return _repo.GetRoomNumberById(roomId);
        }

        // ------------- Commands (line-level) -------------

        /// <summary>
        /// Thêm 1 line vào header có sẵn. Trả về BookingRoomID.
        /// - Nếu có cả CheckIn/CheckOut thì sẽ chặn overlap.
        /// - Nếu chưa có ngày thì skip check overlap (cho phép đặt tạm).
        /// </summary>
        public int AddLineForHeader(int headerBookingId, Booking line)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            ValidateLineCore(line, requireDates: false);

            // Check overlap nếu có đủ ngày
            if (line.CheckInDate.HasValue && line.CheckOutDate.HasValue)
            {
                bool overlapped = _repo.IsRoomOverlapped(line.RoomID, line.CheckInDate.Value, line.CheckOutDate.Value);
                Ensure(!overlapped, "Khoảng thời gian đã có booking khác.");
            }

            if (string.IsNullOrWhiteSpace(line.Status)) line.Status = "Booked";
            return _repo.AddLineForHeader(headerBookingId, line);
        }

        /// <summary>
        /// Thêm nhiều line cho 1 header. Trả về map RoomID -> BookingRoomID.
        /// - Validate cơ bản từng line.
        /// - Với line có đủ ngày thì check overlap.
        /// </summary>
        public Dictionary<int, int> AddLinesForHeader(int headerBookingId, List<Booking> roomBookings)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            Ensure(roomBookings != null && roomBookings.Count > 0, "Danh sách phòng trống.");

            // tránh trùng phòng do nhập lỗi
            var normalized = roomBookings.Where(b => b != null).ToList();
            Ensure(normalized.Count > 0, "Danh sách phòng trống.");

            foreach (var b in normalized)
            {
                ValidateLineCore(b, requireDates: false);
                if (string.IsNullOrWhiteSpace(b.Status)) b.Status = "Booked";

                if (b.CheckInDate.HasValue && b.CheckOutDate.HasValue)
                {
                    bool overlapped = _repo.IsRoomOverlapped(b.RoomID, b.CheckInDate.Value, b.CheckOutDate.Value);
                    if (overlapped)
                        throw new InvalidOperationException($"Phòng {b.RoomID} đã có booking trùng thời gian.");
                }
            }

            return _repo.AddLinesForHeader(headerBookingId, normalized);
        }

        /// <summary>
        /// Cập nhật nhanh status/checkout/note cho 1 line.
        /// </summary>
        public bool UpdateLine(Booking booking)
        {
            Ensure(booking != null && booking.BookingID > 0, "Line không hợp lệ.");
            if (booking.CheckInDate.HasValue && booking.CheckOutDate.HasValue)
            {
                Ensure(booking.CheckOutDate.Value > booking.CheckInDate.Value, "Check-out phải sau Check-in.");
            }
            if (string.IsNullOrWhiteSpace(booking.Status)) booking.Status = "Booked";
            return _repo.Update(booking);
        }

        /// <summary>
        /// Đổi trạng thái và set CheckOutDate cho 1 line (ví dụ 'CheckedOut').
        /// </summary>
        public bool UpdateLineStatusAndCheckout(int bookingRoomId, string status, DateTime checkoutAt)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            Ensure(!string.IsNullOrWhiteSpace(status), "Status không được trống.");
            return _repo.UpdateBookingStatusAndCheckOut(bookingRoomId, status.Trim(), checkoutAt);
        }

        /// <summary>
        /// Hủy 1 line (Status = 'Cancelled').
        /// </summary>
        public bool CancelLine(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _repo.CancelBooking(bookingRoomId);
        }

        /// <summary>
        /// Check-in cho 1 line (chỉ khi đang 'Booked').
        /// </summary>
        public bool CheckInLine(int bookingRoomId, DateTime? at = null)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            var when = at ?? DateTime.Now;
            return _repo.CheckInBooking(bookingRoomId, when);
        }

        /// <summary>
        /// Checkout toàn bộ lines theo header.
        /// </summary>
        public bool CheckoutAllLinesByHeader(int headerBookingId, DateTime? checkoutAt = null)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            var at = checkoutAt ?? DateTime.Now;
            return _repo.SetLinesCheckedOutByHeader(headerBookingId, at);
        }
    }
}
