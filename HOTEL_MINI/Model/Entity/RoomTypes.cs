namespace MiniHotel.Models
{
    public class RoomTypes
    {
        public int RoomTypeID { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return TypeName;
        }

    }
}