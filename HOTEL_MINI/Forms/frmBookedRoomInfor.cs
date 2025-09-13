using HOTEL_MINI.BLL;
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
    public partial class frmBookedRoomInfor : Form
    {
        private frmApplication _frmApplication;
        private Booking _booking;
        private Room _room;
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
        private readonly CustomerService _customerService;
        public frmBookedRoomInfor(Booking booking, Room room, frmApplication frmApplication)
        {
            _booking = booking;
            _room = room;
            _frmApplication = frmApplication;
            _bookingService = new BookingService();
            _roomService = new RoomService();
            _customerService = new CustomerService();

            InitializeComponent();
            LoadInforBooking();
        }
        private void LoadInforBooking()
        {
            lblRoomNumber.Text = _room.RoomNumber;

            try
            {
                var customer = _customerService.getCustomerByCustomerID(_booking.CustomerID);
                if (customer != null)
                {
                    txtTen.Text = customer.FullName;
                    txtCCCD.Text = customer.IDNumber;
                    txtSDT.Text = customer.Phone;
                    txtEmail.Text = customer.Email;
                    txtDiachi.Text = customer.Address;
                    txtGender.Text = customer.Gender;
                }
                else
                {
                    MessageBox.Show("Khách hàng không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy thông tin khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            txtPricingType.Text = _roomService.getPricingTypeByID(_booking.PricingID);
            dtpCheckinTime.Value = _booking.CheckInDate.HasValue ? _booking.CheckInDate.Value : DateTime.Now;
            if (_booking.CheckOutDate == null)
            {
                //dtpCheckoutTime.Value = DateTime.Now;
                //dtpCheckoutTime.Enabled = true;
                dtpCheckoutTime.Visible = false;
                lblCHECKOUT.Visible = false;
            }
            else
            {
                dtpCheckoutTime.Value = _booking.CheckOutDate.Value;
            }
            //dtpCheckoutTime.Value = _booking.CheckOutDate.HasValue ? _booking.CheckInDate.Value : DateTime.Now;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            // Xác nhận hủy booking
            var confirmResult = MessageBox.Show("Xác nhận hủy booking này?", "XÁC NHẬN",
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    // Gọi service hủy booking
                    bool success = _bookingService.CancelBooking(_booking.BookingID);
                    _roomService.UpdateRoomStatus(_room.RoomID, "Available");

                    if (success)
                    {
                        MessageBox.Show("Hủy booking thành công!", "Thành công",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Trả về OK để form cha biết đã có thay đổi
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hủy booking thất bại!", "Lỗi",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi hủy booking: {ex.Message}", "Lỗi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
