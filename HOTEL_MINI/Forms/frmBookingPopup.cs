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
        private int countClickCheckExistCCCD = 0;
        private readonly frmRoom _frmRoom;
        public frmBookingPopup(frmApplication frmApplication, Room room, frmRoom frmRoom)
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
            LoadPricingType();
            LoadGenderType();
            inAccessible();
            cbxPricingType.SelectedIndexChanged += cbxPricingType_SelectedIndexChanged;
            dtpCheckinTime.ValueChanged += dtpCheckinTime_ValueChanged;
            _frmRoom = frmRoom;

            // Ẩn textbox gender ban đầu, hiện combobox
            txtGender.Visible = false;
            cbxGender.Visible = true;
        }
        private void LoadGenderType()
        {
            cbxGender.Items.Clear();
            var genderType = _customerService.getAllGender();
            foreach (var gen in genderType)
            {
                cbxGender.Items.Add(gen);
            }

        }
        private void ApplyPricing()
        {
            if (cbxPricingType.SelectedItem == null) return;

            string pricingType = cbxPricingType.SelectedItem.ToString();
            bool isNhanNgay = rbtnNhanngay.Checked;

            // chuẩn bị DateTime mặc định
            DateTime now = DateTime.Now;
            DateTime checkin, checkout;

            switch (pricingType)
            {
                case "Hourly":
                    dtpCheckinTime.Enabled = true;
                    dtpCheckoutTime.Enabled = false;
                    checkin = dtpCheckinTime.Value; // cho chọn ngày + giờ
                    dtpCheckoutTime.Value = now;   // giữ chỗ thôi, ko dùng
                    dtpCheckoutTime.Enabled = false; // ko cho chọn
                    break;

                case "Weekly":
                    dtpCheckinTime.Enabled = true;
                    dtpCheckoutTime.Enabled = false;
                    checkin = dtpCheckinTime.Value;
                    dtpCheckoutTime.Value = checkin.AddDays(7); // auto +7 ngày
                    break;

                case "Nightly":
                    dtpCheckoutTime.Enabled = false;
                    if (isNhanNgay)
                    {
                        // Nhận ngay: hôm nay 21h
                        checkin = DateTime.Today.AddHours(21);
                    }
                    else
                    {
                        // Đặt trước: user chọn ngày, fix giờ = 21h
                        checkin = dtpCheckinTime.Value.Date.AddHours(21);
                    }
                    dtpCheckinTime.Value = checkin;
                    dtpCheckinTime.Enabled = true;
                    dtpCheckoutTime.Value = checkin.Date.AddDays(1).AddHours(7);
                    break;

                case "Daily":
                    dtpCheckoutTime.Enabled = false;
                    if (isNhanNgay)
                    {
                        // Nhận ngay: hôm nay 14h
                        checkin = DateTime.Today.AddHours(14);
                    }
                    else
                    {
                        // Đặt trước: user chọn ngày, fix giờ = 14h
                        checkin = dtpCheckinTime.Value.Date.AddHours(14);
                    }
                    dtpCheckinTime.Value = checkin;
                    dtpCheckinTime.Enabled = true;
                    dtpCheckoutTime.Value = checkin.Date.AddDays(1).AddHours(12);
                    break;

                default:
                    throw new ArgumentException("Loại pricing không hợp lệ");
            }

            // đảm bảo hiển thị dd/MM/yyyy HH
            this.dtpCheckinTime.CustomFormat = "dd/MM/yyyy HH";
            this.dtpCheckinTime.ShowUpDown = true;
            this.dtpCheckoutTime.CustomFormat = "dd/MM/yyyy HH";
            this.dtpCheckoutTime.ShowUpDown = true;
        }

        private void dtpCheckinTime_ValueChanged(object sender, EventArgs e)
        {
            if (rbtnDattruoc.Checked) // chỉ áp dụng khi đặt trước
                ApplyPricing();
        }
        private void cbxPricingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyPricing();
        }
       
        private void addNewBooking(int CustomerID, int currentUserID)
        {
            int pricingID = getPricingID(cbxPricingType.SelectedItem.ToString(), _room.RoomTypeID);
            if (pricingID == -1)
            {
                MessageBox.Show("Không thể tìm thấy PricingID. Vui lòng chọn lại kiểu giá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime? checkOutDate = null;
            string pricingType = cbxPricingType.SelectedItem.ToString();

            if (pricingType == "Hourly")
            {
                checkOutDate = null;
            }
            else
            {
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
                CheckOutDate = checkOutDate, 
                Status = rbtnNhanngay.Checked ? "CheckedIn" : "Booked",
                Notes = txtNote.Text
            };
            var bookingResult = _bookingService.AddBooking(booking);
            if (bookingResult == null)
            {

                MessageBox.Show("Đặt phòng thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                MessageBox.Show("Đặt phòng thành công! BookingID: " + bookingResult.BookingID);
                var changeStatus = _roomService.UpdateRoomStatus(_room.RoomID, rbtnNhanngay.Checked ? "Occupied" : "Booked");
                if (changeStatus)
                {
                    MessageBox.Show("Cap nhat trang thai phong thanh cong");
                    _frmRoom.RefreshRoomList();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật trạng thái phòng. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
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
            countClickCheckExistCCCD++;
            if (countClickCheckExistCCCD > 1)
            {
                clickBtnCheckExistCCCDAgain();
                countClickCheckExistCCCD = 0;
                return;
            }
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
        private void clickBtnCheckExistCCCDAgain()
        {
            _customer = null;
            txtTen.Clear();
            txtDiachi.Clear();
            txtEmail.Clear();
            txtSDT.Clear();
            txtGender.Clear();

            // Mở khóa các field và hiện combobox gender
            txtCCCD.ReadOnly = false;
            txtSDT.ReadOnly = false;
            txtTen.ReadOnly = false;
            txtDiachi.ReadOnly = false;
            txtEmail.ReadOnly = false;

            // Ẩn textbox gender và hiện combobox
            txtGender.Visible = false;
            cbxGender.Visible = true;
            cbxGender.SelectedIndex = -1; // Reset selection
        }
        //private void clickBtnCheckExistCCCDAgain()
        //{
        //    _customer = null;
        //    txtTen.Clear();
        //    txtDiachi.Clear();
        //    txtEmail.Clear();
        //    txtSDT.Clear();
        //    txtGender.Clear();
        //}
        //private void addNewCustomer(Customer customer)
        //{
        //    var customerResult = _customerService.addNewCustomer(customer);
        //    if (customerResult != null)
        //    {
        //        MessageBox.Show("User added successfully.");
        //        _customer = customerResult;
        //        return;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Failed to add user.");
        //        return;
        //    }
        //}
        private void addNewCustomer(Customer customer)
        {
            // Lấy gender từ combobox nếu khách mới
            if (_customer == null && cbxGender.SelectedItem != null)
            {
                customer.Gender = cbxGender.SelectedItem.ToString();
            }

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
        private void rbtnNhanngay_CheckedChanged(object sender, EventArgs e)
        {
            ApplyPricing();
        }
        private void rbtnDattruoc_CheckedChanged(object sender, EventArgs e)
        {
            ApplyPricing();
        }
        private void IDNumberExistUI(Customer customer)
        {
            txtCCCD.Text = customer.IDNumber;
            txtTen.Text = customer.FullName;
            txtDiachi.Text = customer.Address;
            txtEmail.Text = customer.Email;
            txtSDT.Text = customer.Phone;

            // Hiển thị gender trong textbox thay vì combobox
            txtGender.Text = customer.Gender;

            // Khóa các field thông tin khách hàng
            txtCCCD.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtTen.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtGender.ReadOnly = true;

            // Ẩn combobox gender và hiện textbox
            cbxGender.Visible = false;
            txtGender.Visible = true;
        }
        private void LoadPricingType()
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
            txtSDT.ReadOnly = true;
            txtTen.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtGender.ReadOnly = true;
            txtNote.ReadOnly = true;
            cbxPricingType.Enabled = false;
            cbxGender.Enabled = false;
            rbtnDattruoc.Enabled = false;
            rbtnNhanngay.Enabled = false;
            btnBookConfirm.Enabled = false;
        }
        private void Accessible()
        {
            dtpCheckinTime.Enabled = true;
            dtpCheckoutTime.Enabled = true;
            txtSDT.ReadOnly = false;
            txtTen.ReadOnly = false;
            txtDiachi.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtNote.ReadOnly = false;
            cbxPricingType.Enabled = true;
            rbtnDattruoc.Enabled = true;
            rbtnNhanngay.Enabled = true;
            btnBookConfirm.Enabled = true;

            // Chỉ enable gender control dựa trên trạng thái khách hàng
            if (_customer != null)
            {
                txtGender.ReadOnly = true;
                txtGender.Visible = true;
                cbxGender.Visible = false;
            }
            else
            {
                cbxGender.Enabled = true;
                txtGender.Visible = false;
                cbxGender.Visible = true;
            }
        }
        private bool ValidateCustomerInputs()
        {
            bool genderValid;

            if (_customer != null) // Khách đã tồn tại
            {
                genderValid = !string.IsNullOrWhiteSpace(txtGender.Text);
            }
            else // Khách mới
            {
                genderValid = cbxGender.SelectedItem != null;
            }

            return !string.IsNullOrWhiteSpace(txtTen.Text) &&
                   !string.IsNullOrWhiteSpace(txtDiachi.Text) &&
                   !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtSDT.Text) &&
                   !string.IsNullOrWhiteSpace(txtCCCD.Text) &&
                   genderValid &&
                   cbxPricingType.SelectedItem != null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
