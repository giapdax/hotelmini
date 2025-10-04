using System;

public class SelectedRoomWithTime
{
    public int RoomID { get; set; }
    public string RoomNumber { get; set; }
    public int RoomTypeID { get; set; }
    public string RoomTypeName { get; set; }

    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }

    /// <summary>Hourly | Nightly | Daily | Weekly (nếu null: chưa chọn, sẽ tính theo phương án tối ưu)</summary>
    public string PricingType { get; set; }

    /// <summary>Tiền phòng tính sẵn theo PricingType (nếu có). Nếu null sẽ để form chi tiết tự tính.</summary>
    public decimal? BaseCost { get; set; }
}
