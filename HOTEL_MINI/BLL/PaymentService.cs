using HOTEL_MINI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.BLL
{
    public class PaymentService
    {
        private readonly PaymentRepository _paymentRepository;
        public PaymentService()
        {
            _paymentRepository = new PaymentRepository();
        }
    }
}
