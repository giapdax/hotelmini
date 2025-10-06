using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;

namespace HOTEL_MINI.BLL
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _repo = new InvoiceRepository();

        // ===== Invoices =====
        public int CreateInvoice(Invoice inv) => _repo.AddInvoice(inv);
        public bool UpdateInvoiceTotals(Invoice inv) => _repo.UpdateInvoiceTotals(inv);
        public void UpdateStatus(int invoiceId, string status, int? issuedByIfPaid = null)
            => _repo.UpdateInvoiceStatus(invoiceId, status, issuedByIfPaid);
        public Invoice GetInvoice(int invoiceId) => _repo.GetInvoiceById(invoiceId);
        public Invoice GetLatestInvoiceByBooking(int bookingId) => _repo.GetLatestInvoiceByBookingID(bookingId);
        public List<Invoice> GetAllInvoices() => _repo.GetAllInvoices();

        // Form đang dùng các hàm này:
        public int CreateOrGetOpenInvoice(int bookingId, decimal roomCharge, decimal serviceCharge, decimal discount, decimal surcharge, int issuedByUserIfPaid = 0)
            => _repo.CreateOrGetOpenInvoice(bookingId, roomCharge, serviceCharge, discount, surcharge, issuedByUserIfPaid);

        public (decimal Total, decimal Paid, decimal Remain, string Status) GetInvoiceTotals(int invoiceId)
            => _repo.GetInvoiceTotals(invoiceId);

        public Invoice GetInvoiceByBookingID(int bookingID)
            => _repo.GetInvoiceByBookingID(bookingID);

        public List<Payment> GetPaymentsByInvoiceId(int invoiceId)
            => _repo.GetPaymentsByInvoiceId(invoiceId) ?? new List<Payment>();

        public string GetFullNameByInvoiceID(int invoiceID)
            => _repo.getFullNameByInvoiceID(invoiceID);

        // ===== Payment (1–1 với Invoice) =====
        public Payment GetPaymentForInvoice(int invoiceId) => _repo.GetPaymentForInvoice(invoiceId);
        public int UpsertPayment(int invoiceId, decimal amount, DateTime paidAt, string method = "Cash", string status = "Paid", bool allowPartial = false)
            => _repo.UpsertPaymentForInvoice(invoiceId, amount, paidAt, method, status, allowPartial);
        public void DeletePayment(int invoiceId) => _repo.DeletePaymentForInvoice(invoiceId);
        public void RefreshInvoiceStatusFromPayment(int invoiceId, int? issuedByIfPaid = null)
            => _repo.RefreshInvoiceStatusFromPayment(invoiceId, issuedByIfPaid);

        // ===== Reports (nếu dùng) =====
        public DataTable GetRevenueLast6Months() => _repo.GetRevenueLast6Months();
        public DataTable GetRevenueByMonth(int year) => _repo.GetRevenueByMonth(year);
        public DataTable GetRevenueByWeek() => _repo.GetRevenueByCurrentWeek();
        public RevenueSummary GetRevenueSummary() => _repo.GetRevenueSummary();
        public List<RevenueRoomDTO> GetRevenueByRoom(int month, int year)
            => _repo.GetRevenueByRoom(month, year) ?? new List<RevenueRoomDTO>();

    }
}
