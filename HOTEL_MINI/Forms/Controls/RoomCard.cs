using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms.Controls
{
    public partial class RoomCard : UserControl
    {
        private readonly Room _room;
        private readonly Form1 _form1;
        public RoomCard(Room room)//au tạo constructor thì thêm Form1 form1,
        {
            InitializeComponent();
            //_form1 = form1;
            _room = room;
            LoadUiRoomCard();
        }
        public void LoadUiRoomCard()
        {
            lblRoomNumber.Text = $"{_room.RoomNumber}";
            var roomStatus = _room.RoomStatus;
            lblRoomStatus.Text = roomStatus;
            if (roomStatus == "Available")
            {
                this.BackColor = Color.LightGreen;
                btnDetail.Visible = false;
            }
            else if(roomStatus == "Booked") 
            {
                this.BackColor = Color.Yellow;
                btnDetail.Visible = true;
            }
            else if(roomStatus == "Occupied") 
            {
                this.BackColor = Color.Blue;
                btnBook.Visible = false;
            }
            else 
            {
                this.BackColor = Color.Gray;
                btnBook.Visible = false;
                btnDetail.Visible = false;
            }
        }
    }
}
