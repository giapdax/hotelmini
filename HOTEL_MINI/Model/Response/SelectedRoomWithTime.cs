using System;

public class SelectedRoomWithTime
{
    public int RoomID { get; set; }
    public string RoomNumber { get; set; }
    public int RoomTypeID { get; set; }
    public string RoomTypeName { get; set; }

    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }

    public string PricingType { get; set; }

    public decimal? BaseCost { get; set; }
}
