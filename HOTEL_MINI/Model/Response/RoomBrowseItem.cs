namespace HOTEL_MINI.Model.Response
{
    public class RoomBrowseItem
    {
        public int RoomID { get; set; }
        public string RoomNumber { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string Status { get; set; }
        public string Note { get; set; }
        public bool AvailableAtRange { get; set; }
    }
}
