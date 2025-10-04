namespace HOTEL_MINI.Model.Response
{
    public class RoomBrowsePriceItem
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }

        public decimal? PriceHourly { get; set; }
        public decimal? PriceNightly { get; set; }
        public decimal? PriceDaily { get; set; }
        public decimal? PriceWeekly { get; set; }

        public bool AvailableAtRange { get; set; }
    }
}
