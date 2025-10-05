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
            => _repo.GetPaymentsByInvoice(invoiceId);

        public decimal GetPaidAmountByHeader(int headerBookingId)
            => _repo.GetPaidAmountByHeaderId(headerBookingId);

        // Thêm tiện ích cho UI gọi khi thanh toán
        public void AddPayment(Payment payment) => _repo.AddPayment(payment);

        public int AddPaymentSafe(int invoiceId, decimal amount, string method, string status, int issuedByIfPaid)
            => _repo.AddPaymentSafe(invoiceId, amount, method, status, issuedByIfPaid);
    }
}
