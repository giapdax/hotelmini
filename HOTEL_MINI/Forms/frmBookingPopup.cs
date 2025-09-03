using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmBookingPopup : Form
    {
        private readonly Room _room;
        private readonly frmApplication _form1;
        private readonly CustomerService _customerService;
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
        private Customer _customer = null;
        public frmBookingPopup(frmApplication frmApplication, Room room)
        {
            InitializeComponent();
            _form1 = frmApplication;
            _room = room;
            _customerService = new CustomerService();
            _bookingService = new BookingService();
            _roomService = new RoomService();
            lblRoomNumber.Text = $"Room: {_room.RoomNumber}";
            dtpCheckinTime.Enabled = false;
            dtpCheckoutTime.Enabled = false;
            LoadRoomStatus();
            inAccessible();
            cbxPricingType.SelectedIndexChanged += cbxPricingType_SelectedIndexChanged;
        }
        private void cbxPricingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxPricingType.SelectedItem == null) return;

            string pricingType = cbxPricingType.SelectedItem.ToString();

            switch (pricingType)
            {
                case "Hourly":
                    SetupHourlyPricing();
                    break;
                case "Nightly":
                    SetupNightlyPricing();
                    break;
                case "Weekly":
                    SetupWeeklyPricing();
                    break;
            }
        }

        private void SetupHourlyPricing()
        {
            // Checkin ngay lập tức, checkout để null (chọn sau)
            dtpCheckinTime.Value = DateTime.Now;
            dtpCheckinTime.Enabled = false; // Không cho chỉnh sửa vì là giờ hiện tại

            // Để checkout time disabled hoặc null
            dtpCheckoutTime.Enabled = false;
            dtpCheckoutTime.Value = DateTime.Now; // Có thể set thành min value hoặc now
        }

        private void SetupNightlyPricing()
        {
            // Checkin: 9h tối hôm nay (dù hiện tại là mấy giờ)
            DateTime todayEvening = DateTime.Today.AddHours(21); // 9h tối
            dtpCheckinTime.Value = todayEvening;
            dtpCheckinTime.Enabled = false; // Không cho chỉnh sửa

            // Checkout: 7h sáng hôm sau
            DateTime tomorrowMorning = DateTime.Today.AddDays(1).AddHours(7);
            dtpCheckoutTime.Value = tomorrowMorning;
            dtpCheckoutTime.Enabled = false; // Không cho chỉnh sửa
        }

        private void SetupWeeklyPricing()
        {
            // Checkin: giờ hiện tại
            dtpCheckinTime.Value = DateTime.Now;
            dtpCheckinTime.Enabled = false;

            // Checkout: 7 ngày sau
            dtpCheckoutTime.Value = DateTime.Now.AddDays(7);
            dtpCheckoutTime.Enabled = false;
        }
        private void addNewBooking(int CustomerID, int currentUserID)
        {
            //int pricingID = getPricingID(cbxPricingType.SelectedItem.ToString(), _room.RoomTypeID);
            //if (pricingID == -1)
            //{
            //    MessageBox.Show("Không thể tìm thấy PricingID. Vui lòng chọn lại kiểu giá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //Booking booking = new Booking
            //{
            //    CustomerID = CustomerID,
            //    RoomID = _room.RoomID,
            //    PricingID = pricingID,  
            //    CreatedBy = currentUserID,
            //    BookingDate = DateTime.Now,
            //    CheckInDate = rbtnNhanngay.Checked ? DateTime.Now : dtpCheckinTime.Value,
            //    CheckOutDate = rbtnNhanngay.Checked
            //    ? (DateTime?)null
            //    : dtpCheckoutTime.Value,
            //    Status = rbtnNhanngay.Checked ? "CheckedIn" : "Booked",
            //    Notes = txtNote.Text
            //};
            int pricingID = getPricingID(cbxPricingType.SelectedItem.ToString(), _room.RoomTypeID);
            if (pricingID == -1)
            {
                MessageBox.Show("Không thể tìm thấy PricingID. Vui lòng chọn lại kiểu giá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Xác định thời gian checkin/checkout dựa vào loại giá
            DateTime? checkOutDate = null;
            string pricingType = cbxPricingType.SelectedItem.ToString();

            if (pricingType == "Hourly")
            {
                // Hourly: checkout time sẽ null, set sau
                checkOutDate = null;
            }
            else
            {
                // Nightly và Weekly: lấy giá trị từ datetimepicker
                checkOutDate = dtpCheckoutTime.Value;
            }

            Booking booking = new Booking
            {
                CustomerID = CustomerID,
                RoomID = _room.RoomID,
                PricingID = pricingID,
                CreatedBy = currentUserID,
                BookingDate = DateTime.Now,
                CheckInDate = dtpCheckinTime.Value,
                CheckOutDate = checkOutDate,  // Có thể null cho Hourly
                Status = "Booked", // Luôn là "Booked" ban đầu
                Notes = txtNote.Text
            };


            var bookingResult = _bookingService.AddBooking(booking);  
            if (bookingResult == null)
            {
 
                MessageBox.Show("Đặt phòng thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                MessageBox.Show("Đặt phòng thành công! BookingID: " + bookingResult.BookingID);
                var changeStatus = _roomService.updateRoomStatus(_room.RoomID, rbtnNhanngay.Checked ? "Occupied" : "Booked");
                if (changeStatus)
                {
                    MessageBox.Show("Cap nhat trang thai phong thanh cong");
                    return; 

                }
                else
                {
                    MessageBox.Show("Không thể cập nhật trạng thái phòng. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnBookConfirm_Click(object sender, EventArgs e)
        {
            if (!ValidateCustomerInputs())
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin khách hàng");

                return;
            }
            else
            {
                if (_customer != null)
                {
                    addNewBooking(_customer.CustomerID, _form1.GetCurrentUser().UserID);
                }
                else
                {
                    Customer newCustomer = new Customer
                    {
                        FullName = txtTen.Text,
                        Address = txtDiachi.Text,
                        Email = txtEmail.Text,
                        Gender = txtGender.Text,
                        IDNumber = txtCCCD.Text,
                        Phone = txtSDT.Text
                    };
                    addNewCustomer(newCustomer);
                    if (_customer == null)
                    {
                        MessageBox.Show("Không thể tạo khách hàng mới. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    addNewBooking(_customer.CustomerID, _form1.GetCurrentUser().UserID);
                }
            }
        }
        private void btnCheckExistCCCD_Click(object sender, EventArgs e)
        {
            string ccccd = txtCCCD.Text;
            if (string.IsNullOrWhiteSpace(ccccd))
            {
                MessageBox.Show("Please enter a valid ID number.");
                return;
            }
            var customer = _customerService.getCustomerByIDNumber(ccccd);
            if (customer != null)
            {
                MessageBox.Show($"Khach tồn tại và tự đọng động fill {customer.FullName} {customer.Email}  {customer.Email}");
                _customer = customer;
                Accessible();
                IDNumberExistUI(customer);
                return;
            }
            else
            {
                MessageBox.Show("Không thấy khách, hãy điền các thông tin của khách");
                Accessible();
                return;
            }
        }
        private void addNewCustomer(Customer customer)
        {
            var customerResult = _customerService.addNewCustomer(customer);
            if (customerResult != null)
            {
                MessageBox.Show("User added successfully.");
                _customer = customerResult;
                return;
            }
            else
            {
                MessageBox.Show("Failed to add user.");
                return;
            }
        }
        public int getPricingID(string pricingType, int roomType)
        {
            var pricing = _roomService.getPricingID(pricingType, roomType);
            if (pricing != null)
            {
                return pricing.PricingID;
            }
            else
            {
                MessageBox.Show("Không tìm thấy Record trong bảng RoomPricing.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }
        //private void rbtnNhanngay_CheckedChanged(object sender, EventArgs e)
        //{
        //    dtpCheckinTime.Enabled = true;
        //    dtpCheckoutTime.Enabled = true;
        //}
        //private void rbtnDattruoc_CheckedChanged(object sender, EventArgs e)
        //{
        //    dtpCheckinTime.Enabled = false;
        //    dtpCheckoutTime.Enabled = false;
        //}
        private void rbtnNhanngay_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnNhanngay.Checked)
            {
                // Nếu là nhận ngay, checkin time luôn là giờ hiện tại
                dtpCheckinTime.Value = DateTime.Now;
                dtpCheckinTime.Enabled = false;

                // Cho phép chọn checkout time nếu không phải Hourly
                if (cbxPricingType.SelectedItem != null &&
                    cbxPricingType.SelectedItem.ToString() != "Hourly")
                {
                    dtpCheckoutTime.Enabled = true;
                }
            }
        }

        private void rbtnDattruoc_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDattruoc.Checked)
            {
                // Đặt trước: cho phép chọn cả checkin và checkout time
                dtpCheckinTime.Enabled = true;
                dtpCheckoutTime.Enabled = true;
            }
        }
        private void IDNumberExistUI(Customer customer)
        {
            txtCCCD.Text = customer.IDNumber;
            txtTen.Text = customer.FullName;
            txtDiachi.Text = customer.Address;
            txtEmail.Text = customer.Email;
            txtSDT.Text = customer.Phone;
            txtGender.Text = customer.Gender;
            txtCCCD.ReadOnly = false;
            txtSDT.ReadOnly = true;
            txtTen.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtGender.ReadOnly = true;
        }
        private void LoadRoomStatus()
        {
            var pricingType = _roomService.getAllPricingType();
            foreach (var status in pricingType)
            {
                cbxPricingType.Items.Add(status);
            }
        }
        private void inAccessible()
        {
            dtpCheckinTime.Enabled = false;
            dtpCheckoutTime.Enabled = false;
            //txtCCCD.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtTen.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtGender.ReadOnly = true;
            txtNote.ReadOnly = true;
            cbxPricingType.Enabled = false;
            rbtnDattruoc.Enabled = false;
            rbtnNhanngay.Enabled = false;
            btnBookConfirm.Enabled = false;
        }
        private void Accessible()
        {
            dtpCheckinTime.Enabled = true;
            dtpCheckoutTime.Enabled = true;
            //txtCCCD.ReadOnly = true;
            txtSDT.ReadOnly = false;
            txtTen.ReadOnly = false;
            txtDiachi.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtGender.ReadOnly = false;
            txtNote.ReadOnly = false;
            cbxPricingType.Enabled = true;
            rbtnDattruoc.Enabled = true;
            rbtnNhanngay.Enabled = true;
            btnBookConfirm.Enabled = true;
        }
        private bool ValidateCustomerInputs()
        {
            return !string.IsNullOrWhiteSpace(txtTen.Text) &&
                   !string.IsNullOrWhiteSpace(txtDiachi.Text) &&
                   !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtSDT.Text) &&
                   !string.IsNullOrWhiteSpace(txtGender.Text) &&
                   !string.IsNullOrWhiteSpace(txtCCCD.Text)&&
                   cbxPricingType.SelectedItem != null; // Thêm validation cho pricing type;
        }
        private void IDNumberNotExistUI()
        {
            txtCCCD.ReadOnly = false;
            txtSDT.ReadOnly = false;
            txtTen.ReadOnly = false;
            txtDiachi.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtGender.ReadOnly = false;
            txtDiachi.Clear();
            txtTen.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            txtGender.Clear();
        }
    }
}
