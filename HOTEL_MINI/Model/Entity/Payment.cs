using System;

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
        public string Note { get; set; }  
    }
}
