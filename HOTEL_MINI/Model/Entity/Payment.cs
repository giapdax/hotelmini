using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.Model.Entity
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int InvoiceID { get; set; }    
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
    }

}
