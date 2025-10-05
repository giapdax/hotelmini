namespace HOTEL_MINI.Model.Entity
{
    public class InvoiceRoom
    {
        public int InvoiceID { get; set; }
        public int BookingRoomID { get; set; }
        public decimal? RoomCharge { get; set; }
        public decimal? ServiceCharge { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Surcharge { get; set; }
    }
}
