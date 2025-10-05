using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;

namespace HOTEL_MINI.BLL
{
    /// <summary>
    /// Nghiệp vụ hóa đơn + thanh toán (1–1).
    /// </summary>
    public class InvoiceService
    {
        private readonly InvoiceRepository _repo;

        public InvoiceService()
        {
            _repo = new InvoiceRepository();
        }

        // ======== Reports ========
        public DataTable GetRevenueLast6Months() => _repo.GetRevenueLast6Months();
        public DataTable GetRevenueByMonth(int year) => _repo.GetRevenueByMonth(year);
        public DataTable GetRevenueByWeek() => _repo.GetRevenueByCurrentWeek();
        public RevenueSummary GetRevenueSummary() => _repo.GetRevenueSummary();
        public List<RevenueRoomDTO> GetRevenueByRoom(int month, int year)
            => _repo.GetRevenueByRoom(month, year) ?? new List<RevenueRoomDTO>();

        // ======== Invoices ========
        public int CreateInvoice(Invoice inv) => _repo.AddInvoice(inv);
        public bool UpdateInvoiceTotals(Invoice inv) => _repo.UpdateInvoiceTotals(inv);
        public void UpdateStatus(int invoiceId, string status, int? issuedByIfPaid = null)
            => _repo.UpdateInvoiceStatus(invoiceId, status, issuedByIfPaid);
        public Invoice GetInvoice(int invoiceId) => _repo.GetInvoiceById(invoiceId);
        public Invoice GetLatestInvoiceByBooking(int bookingId) => _repo.GetLatestInvoiceByBookingID(bookingId);
        public List<Invoice> GetAllInvoices() => _repo.GetAllInvoices();

        // ======== Payment (1–1 với Invoice) ========
        public Payment GetPaymentForInvoice(int invoiceId) => _repo.GetPaymentForInvoice(invoiceId);

        /// <summary>
        /// Upsert payment (1–1). Mặc định bắt buộc thanh toán đủ tiền (allowPartial=false).
        /// </summary>
        public int UpsertPayment(int invoiceId, decimal amount, DateTime paidAt, string method = "Cash", string status = "Paid", bool allowPartial = false)
            => _repo.UpsertPaymentForInvoice(invoiceId, amount, paidAt, method, status, allowPartial);

        public void DeletePayment(int invoiceId) => _repo.DeletePaymentForInvoice(invoiceId);

        public void RefreshInvoiceStatusFromPayment(int invoiceId, int? issuedByIfPaid = null)
            => _repo.RefreshInvoiceStatusFromPayment(invoiceId, issuedByIfPaid);

        // ======== Joins for lists ========
        public List<InvoiceRepository.InvoiceListItem> GetAllInvoicesWithCustomer()
            => _repo.GetAllInvoicesWithCustomer() ?? new List<InvoiceRepository.InvoiceListItem>();

        public List<InvoiceRepository.InvoiceListItem> GetInvoicesByCustomerNumber(string idNumber)
            => _repo.GetInvoicesByCustomerNumber(idNumber) ?? new List<InvoiceRepository.InvoiceListItem>();

        public InvoiceRepository.InvoiceListItem GetInvoiceWithCustomerByInvoiceId(int invoiceId)
            => _repo.GetInvoiceWithCustomerByInvoiceId(invoiceId);
        public Invoice GetInvoiceByBookingID(int bookingID)
    => _repo.GetInvoiceByBookingID(bookingID);

        public string GetFullNameByInvoiceID(int invoiceID)
            => _repo.getFullNameByInvoiceID(invoiceID);

        public List<Payment> GetPaymentsByInvoiceId(int invoiceId)
            => _repo.GetPaymentsByInvoiceId(invoiceId) ?? new List<Payment>();
    }
}
