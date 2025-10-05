using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System.Collections.Generic;
using System.Data;

namespace HOTEL_MINI.BLL
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _invoiceRepo;
        public InvoiceService()
        {
            _invoiceRepo = new InvoiceRepository();
        }

        // Reports
        public DataTable GetRevenueLast6Months() => _invoiceRepo.GetRevenueLast6Months();
        public DataTable GetRevenueByMonth(int year) => _invoiceRepo.GetRevenueByMonth(year);
        public DataTable GetRevenueByWeek() => _invoiceRepo.GetRevenueByCurrentWeek();
        public RevenueSummary GetRevenueSummary() => _invoiceRepo.GetRevenueSummary();
        public List<RevenueRoomDTO> GetRevenueByRoom(int month, int year)
            => _invoiceRepo.GetRevenueByRoom(month, year) ?? new List<RevenueRoomDTO>();

        // Queries
        public Invoice GetInvoiceByBookingID(int bookingID) => _invoiceRepo.GetInvoiceByBookingID(bookingID);
        public string GetPaymentByInvoiceID(int invoiceID) => _invoiceRepo.GetPaymentByInvoice(invoiceID);
        public string getFullNameByInvoiceID(int invoiceID) => _invoiceRepo.getFullNameByInvoiceID(invoiceID);
        public List<Invoice> GetAllInvoices() => _invoiceRepo.getAllInvoices();

        // Mutations / Totals
        public int UpsertInvoiceTotals(Invoice header) => _invoiceRepo.UpsertInvoiceTotals(header);
        public int CreateOrGetOpenInvoice(int bookingHeaderId, decimal roomCharge, decimal serviceCharge,
                                          decimal discount, decimal surcharge, int issuedByUserIfPaid = 0)
            => _invoiceRepo.CreateOrGetOpenInvoice(bookingHeaderId, roomCharge, serviceCharge, discount, surcharge, issuedByUserIfPaid);

        public (decimal Total, decimal Paid, decimal Remain, string Status) GetInvoiceTotals(int invoiceId)
            => _invoiceRepo.GetInvoiceTotals(invoiceId);

        public void UpdateInvoiceStatusIfNeeded(int invoiceId, int issuedByUserIdIfPaid = 0)
            => _invoiceRepo.UpdateInvoiceStatusIfNeeded(invoiceId, issuedByUserIdIfPaid);

        public decimal GetPaidAmount(int invoiceId) => _invoiceRepo.GetPaidAmount(invoiceId);
        public void UpdateStatus(int invoiceId, string status) => _invoiceRepo.UpdateInvoiceStatus(invoiceId, status);
    }
}
