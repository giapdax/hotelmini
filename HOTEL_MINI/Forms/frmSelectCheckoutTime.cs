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

namespace HOTEL_MINI.Forms
{
    public partial class frmSelectCheckoutTime : Form
    {
        private readonly Room _room;
        private readonly Booking _booking;
        private frmApplication _frmApplication;

        public frmSelectCheckoutTime(Room room, Booking booking, frmApplication frmApplication)
        {
            _room = room;
            _booking = booking;
            _frmApplication = frmApplication;
            InitializeComponent();
            LoadInfor();
        }

        private void frmSelectCheckoutTime_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadInfor()
        {
            MessageBox.Show($"Mơ phòng {_room.RoomNumber} trong select");
            lblRoomNumber.Text = _room.RoomNumber;
            dtpCheckinTime.Value = _booking.CheckInDate.Value;
            dtpCheckinTime.Enabled = false;

            // mặc định checkout là giờ hiện tại hoặc sau checkin 1h
            dtpCheckoutTime.Value = DateTime.Now > dtpCheckinTime.Value
                ? DateTime.Now
                : dtpCheckinTime.Value.AddHours(1);
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                var checkoutTime = dtpCheckoutTime.Value;
                var checkinTime = dtpCheckinTime.Value;

                if (checkoutTime <= checkinTime)
                {
                    MessageBox.Show("Giờ trả phòng phải sau giờ nhận phòng!",
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (checkoutTime > DateTime.Now.AddHours(1)) // Không cho chọn giờ trong tương lai xa
                {
                    MessageBox.Show("Giờ trả phòng không hợp lệ!",
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _booking.CheckOutDate = checkoutTime;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
