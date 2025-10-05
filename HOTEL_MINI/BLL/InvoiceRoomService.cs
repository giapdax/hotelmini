using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;

namespace HOTEL_MINI.BLL
{
    /// <summary>
    /// Nghiệp vụ gắn/phân phòng cho hóa đơn.
    /// </summary>
    public class InvoiceRoomService
    {
        private readonly InvoiceRoomRepository _repo;
        public InvoiceRoomService()
        {
            _repo = new InvoiceRoomRepository();
        }

        public void AddRoomsToInvoice(int invoiceId, IEnumerable<int> bookingRoomIds)
        {
            if (invoiceId <= 0) throw new ArgumentException("InvoiceID không hợp lệ.");
            if (bookingRoomIds == null) throw new ArgumentException("Danh sách phòng trống.");
            _repo.AddRoomsToInvoice(invoiceId, bookingRoomIds);
        }

        public void RemoveRoomFromInvoice(int invoiceId, int bookingRoomId)
        {
            if (invoiceId <= 0 || bookingRoomId <= 0) throw new ArgumentException("ID không hợp lệ.");
            _repo.RemoveRoomFromInvoice(invoiceId, bookingRoomId);
        }

        public void ClearRooms(int invoiceId)
        {
            if (invoiceId <= 0) throw new ArgumentException("InvoiceID không hợp lệ.");
            _repo.ClearRoomsForInvoice(invoiceId);
        }

        public List<int> GetBookingRoomIds(int invoiceId)
        {
            if (invoiceId <= 0) throw new ArgumentException("InvoiceID không hợp lệ.");
            return _repo.GetBookingRoomIdsByInvoice(invoiceId);
        }

        public List<BookingFlatDisplay> GetInvoiceRoomsFlat(int invoiceId)
        {
            if (invoiceId <= 0) throw new ArgumentException("InvoiceID không hợp lệ.");
            return _repo.GetInvoiceRoomsFlat(invoiceId);
        }
    }
}
