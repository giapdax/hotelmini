using System;

namespace HOTEL_MINI.Model.Entity
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int BookingID { get; set; }

        public decimal RoomCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Surcharge { get; set; }
        public decimal Discount { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? IssuedAt { get; set; }   
        public int? IssuedBy { get; set; }  
        public string Status { get; set; }     
        public string Note { get; set; }
    }
}
