using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniHotel.Models
{
    public class RoomTypes
    {
        public int RoomTypeID { get; set;}
        public string TypeName {get; set;}
        public string Description { get; set;}
        public override string ToString()
        {
            return TypeName; // hiển thị tên trong ComboBox
        }
    }
}

