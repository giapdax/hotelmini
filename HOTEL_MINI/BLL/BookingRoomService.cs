using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;

namespace HOTEL_MINI.BLL
{
    /// <summary>
    /// Nghiệp vụ theo PHÒNG (BookingRooms / line).
    /// - Chỉ làm việc với BookingRoomRepository.
    /// - KHÔNG xử lý invoice/payment.
    /// </summary>
    public class BookingRoomService
    {
        private readonly BookingRoomRepository _repo;
        private readonly BookingRoomServiceRepository _svcRepo; // nếu có: lấy dịch vụ đã dùng để show UI

        public BookingRoomService()
        {
            _repo = new BookingRoomRepository();
            _svcRepo = new BookingRoomServiceRepository();
        }

        private static void Ensure(bool cond, string msg)
        {
            if (!cond) throw new ArgumentException(msg);
        }

        private static void ValidateLineCore(BookingRoom line, bool requireDates = false)
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

        // --------- DISPLAY ----------
        public List<BookingDisplay> GetActiveBookingDisplays() => _repo.GetActiveBookingDisplays();
        public List<BookingDisplay> GetTop20LatestBookingDisplays() => _repo.GetTop20LatestBookingDisplays();
        public List<BookingDisplay> GetBookingDisplaysByCustomerNumber(string idNumber)
        {
            Ensure(!string.IsNullOrWhiteSpace(idNumber), "Thiếu CCCD/ID.");
            return _repo.GetBookingDisplaysByCustomerNumber(idNumber.Trim());
        }

        // --------- QUERIES ----------
        public List<BookingRoom> GetBookingsByCustomerNumber(string numberID)
        {
            Ensure(!string.IsNullOrWhiteSpace(numberID), "Thiếu CCCD/ID.");
            return _repo.GetBookingsByCustomerNumber(numberID.Trim());
        }

        public BookingRoom GetBookingById(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _repo.GetBookingById(bookingRoomId);
        }

        public BookingRoom GetLatestBookingByRoomId(int roomId)
        {
            Ensure(roomId > 0, "RoomID không hợp lệ.");
            return _repo.GetLatestBookingByRoomId(roomId);
        }

        public BookingRoom GetActiveBookingByRoomId(int roomId)
        {
            Ensure(roomId > 0, "RoomID không hợp lệ.");
            return _repo.GetActiveBookingByRoomId(roomId);
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

        public List<BookingRoom> GetLinesByHeaderId(int headerBookingId)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            return _repo.GetBookingsByHeaderId(headerBookingId);
        }

        public List<UsedServiceDto> GetUsedServicesByBookingRoomId(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _svcRepo.GetUsedServicesByBookingRoomId(bookingRoomId);
        }

        public bool IsRoomOverlapped(int roomId, DateTime checkIn, DateTime checkOut)
        {
            Ensure(roomId > 0, "RoomID không hợp lệ.");
            Ensure(checkOut > checkIn, "Check-out phải sau Check-in.");
            return _repo.IsRoomOverlapped(roomId, checkIn, checkOut);
        }

        // --------- COMMANDS ----------
        public int AddLineForHeader(int headerBookingId, BookingRoom line)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            ValidateLineCore(line, requireDates: false);

            if (line.CheckInDate.HasValue && line.CheckOutDate.HasValue)
            {
                bool overlapped = _repo.IsRoomOverlapped(line.RoomID, line.CheckInDate.Value, line.CheckOutDate.Value);
                Ensure(!overlapped, "Khoảng thời gian đã có booking khác.");
            }

            if (string.IsNullOrWhiteSpace(line.Status)) line.Status = "Booked";
            return _repo.AddLineForHeader(headerBookingId, line);
        }

        public Dictionary<int, int> AddLinesForHeader(int headerBookingId, List<BookingRoom> roomBookings)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            Ensure(roomBookings != null && roomBookings.Count > 0, "Danh sách phòng trống.");

            foreach (var b in roomBookings)
            {
                ValidateLineCore(b, requireDates: false);
                if (string.IsNullOrWhiteSpace(b.Status)) b.Status = "Booked";

                if (b.CheckInDate.HasValue && b.CheckOutDate.HasValue)
                {
                    bool overlapped = _repo.IsRoomOverlapped(b.RoomID, b.CheckInDate.Value, b.CheckOutDate.Value);
                    if (overlapped)
                        throw new InvalidOperationException($"Phòng {b.RoomID} bị trùng lịch.");
                }
            }

            return _repo.AddLinesForHeader(headerBookingId, roomBookings);
        }

        public bool UpdateLine(BookingRoom bookingRoom)
        {
            Ensure(bookingRoom != null && bookingRoom.BookingRoomID > 0, "Line không hợp lệ.");
            if (bookingRoom.CheckInDate.HasValue && bookingRoom.CheckOutDate.HasValue)
                Ensure(bookingRoom.CheckOutDate.Value > bookingRoom.CheckInDate.Value, "Check-out phải sau Check-in.");
            if (string.IsNullOrWhiteSpace(bookingRoom.Status)) bookingRoom.Status = "Booked";
            return _repo.Update(bookingRoom);
        }

        public bool CancelLine(int bookingRoomId)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _repo.CancelBooking(bookingRoomId);
        }

        public bool CheckInLine(int bookingRoomId, DateTime? at = null)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _repo.CheckInBooking(bookingRoomId, at ?? DateTime.Now);
        }

        public bool UpdateLineStatusAndCheckout(int bookingRoomId, DateTime checkoutAt)
        {
            Ensure(bookingRoomId > 0, "BookingRoomID không hợp lệ.");
            return _repo.UpdateBookingStatusAndCheckOut(bookingRoomId, "CheckedOut", checkoutAt);
        }

        public bool SetLinesCheckedOutByHeader(int headerBookingId, DateTime? checkoutAt = null)
        {
            Ensure(headerBookingId > 0, "Header BookingID không hợp lệ.");
            return _repo.SetLinesCheckedOutByHeader(headerBookingId, checkoutAt ?? DateTime.Now);
        }
    }
}
