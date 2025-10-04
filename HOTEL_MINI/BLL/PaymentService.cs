using System.Collections.Generic;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;

namespace HOTEL_MINI.BLL
{
    public class PaymentService
    {
        private readonly PaymentRepository _repo = new PaymentRepository();

        public List<Payment> GetPaymentsByHeader(int headerBookingId)
            => _repo.GetPaymentsByHeaderId(headerBookingId);

        public List<Payment> GetPaymentsByInvoice(int invoiceId)
            => _repo.GetPaymentsByInvoiceId(invoiceId);

        public decimal GetPaidAmountByHeader(int headerBookingId)
            => _repo.GetPaidAmountByHeaderId(headerBookingId);
    }
}
