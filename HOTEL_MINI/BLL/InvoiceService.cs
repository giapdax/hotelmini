using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.BLL
{
    public class InvoiceService
    {
        private readonly InvoiceRepository _invoiceRepo;
        public InvoiceService()
        {
            _invoiceRepo = new InvoiceRepository();
        }
        public DataTable GetRevenueLast6Months()
        {
            return _invoiceRepo.GetRevenueLast6Months();
        }
        public DataTable GetRevenueByMonth(int year)
        {
            return _invoiceRepo.GetRevenueByMonth(year);
        }
        public DataTable GetRevenueByWeek()
        {
            return _invoiceRepo.GetRevenueByCurrentWeek();
        }
        public RevenueSummary GetRevenueSummary()
        {
            return _invoiceRepo.GetRevenueSummary();
        }
        public Invoice GetInvoiceByBookingID(int bookingID)
        {
            return _invoiceRepo.GetInvoiceByBookingID(bookingID);
        }
        public string GetPaymentByInvoiceID(int invoiceID)
        {
            return _invoiceRepo.GetPaymentByInvoice(invoiceID);
        }
        public string getFullNameByInvoiceID(int invoiceID)
        {
            return _invoiceRepo.getFullNameByInvoiceID(invoiceID);
        }
        public List<Invoice> GetAllInvoices()
        {
            return _invoiceRepo.getAllInvoices();
        }
        public List<RevenueRoomDTO> GetRevenueByRoom(int month, int year)
        {
            var result =  _invoiceRepo.GetRevenueByRoom(month, year);
            return result ?? new List<RevenueRoomDTO>();
        }
    }
}
