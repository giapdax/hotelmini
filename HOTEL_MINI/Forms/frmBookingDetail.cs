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
    public partial class frmBookingDetail : Form
    {
        private readonly frmApplication _form1;
        private readonly frmRoom _frmRoom;
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
        private readonly CustomerService _customerService;
        private readonly Booking _booking;
        private readonly Room _room;
        public frmBookingDetail(Booking booking, Room room)
        {
            InitializeComponent();
            _booking = booking;
            _roomService = new RoomService();
            _customerService = new CustomerService();   
            _bookingService = new BookingService();
            _room = room;
            LoadInforBooking();
        }

        private void LoadInforBooking()
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
    }
}
