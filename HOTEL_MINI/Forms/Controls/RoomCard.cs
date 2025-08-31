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
        private readonly frmApplication _form1;
        public RoomCard(frmApplication frmApplication,Room room)//au tạo constructor thì thêm Form1 form1,
        {
            InitializeComponent();
            _form1 = frmApplication;
            _room = room;
            LoadUiRoomCard();
        }
        public void LoadUiRoomCard()
        {
            lblRoomNumber.Text = $"{_room.RoomNumber}";
            var roomStatus = _room.RoomStatus;
            if(roomStatus == "Available")
            {
                this.BackColor = Color.LightGreen;
            }
            else if(roomStatus == "Booked") 
            {
                this.BackColor = Color.Yellow;
            }
            else if(roomStatus == "Occupied") 
            {
                this.BackColor = Color.Blue;
            }
            else 
            {
                this.BackColor = Color.Gray;
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if(_room.RoomStatus == "Available")
            {
                frmBookingPopup bookingPopup = new frmBookingPopup(_form1, _room);
                bookingPopup.ShowDialog();
            }
            else
            {
                MessageBox.Show("Room is not available for booking.");
            }
        }
    }
}
