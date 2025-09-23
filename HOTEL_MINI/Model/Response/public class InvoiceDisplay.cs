namespace HOTEL_MINI.Model.Response
{
    public class InvoiceDisplay
    {
        public int InvoiceID { get; set; }
        public int BookingID { get; set; }

        public string CustomerName { get; set; }
        public string IDNumber { get; set; } 
        public string RoomNumber { get; set; }

        public System.DateTime BookingDate { get; set; }
        public System.DateTime? CheckInDate { get; set; }
        public System.DateTime? CheckOutDate { get; set; }

        public decimal RoomCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Surcharge { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }

        public System.DateTime IssuedAt { get; set; }
        public string Status { get; set; }

        public string PaymentMethod { get; set; } 
        public string IssuedByName { get; set; } 
    }
}
