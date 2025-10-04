using System;
using System.Windows.Forms;
using HOTEL_MINI.Forms.Controls;

namespace HOTEL_MINI.Forms
{
    public partial class frmBooking : Form
    {
        // giữ instance để không tạo lại nhiều lần
        private UcBookRoom _ucBookRoom;
        private UcBookingRoom _ucBookingRoom;

        public frmBooking()
        {
            InitializeComponent();

            // giảm giật màn
            this.DoubleBuffered = true;

            // khi đổi tab thì đảm bảo control của tab đó đã được nạp
            this.tabBooking.SelectedIndexChanged += (s, e) =>
            {
                EnsureTabContent();
            };

            // lần đầu mở form: nạp nội dung của tab đang được chọn
            this.Load += (s, e) => EnsureTabContent();
        }

        private void EnsureTabContent()
        {
            var current = tabBooking.SelectedTab;

            if (current == btnBookRoom)
            {
                if (_ucBookRoom == null)
                {
                    _ucBookRoom = new UcBookRoom { Dock = DockStyle.Fill };
                    btnBookRoom.Controls.Add(_ucBookRoom);
                }
            }
            else if (current == btnBookingRoom)
            {
                if (_ucBookingRoom == null)
                {
                    _ucBookingRoom = new UcBookingRoom { Dock = DockStyle.Fill };
                    btnBookingRoom.Controls.Add(_ucBookingRoom);
                }
            }
        }
    }
}
