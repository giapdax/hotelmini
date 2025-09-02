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
    public partial class frmBookingPopup : Form
    {
        private readonly Room _room;
        private readonly frmApplication _form1;
        private readonly CustomerService _customerService;
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
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
        }
        private void LoadRoomStatus()
        {
            MessageBox.Show($"Loading room status...{_form1.GetCurrentUser().FullName}   {_room.RoomNumber}");
            var pricingType = _roomService.getAllPricingType();
            foreach (var status in pricingType)
            {
                cbxPricingType.Items.Add(status);
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
            txtCCCD.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtTen.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtGender.ReadOnly = true;
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


                IDNumberExistUI(customer);
                return;
            }
            else
            {
                MessageBox.Show("Không thấy khách");               
                IDNumberNotExistUI();
                return;
            }
        }
        private Customer addNewCustomer(Customer customer)
        {
            var customerResult = _customerService.addNewCustomer(customer);
            if (customerResult != null)
            {
                MessageBox.Show("User added successfully.");
                return customerResult;
            }
            else
            {
                MessageBox.Show("Failed to add user.");
                return null;
            }
        }
        private void addNewBooking(int CustomerID, int currentUserID)
        {
            Booking booking = new Booking
            {
                CustomerID = CustomerID,
                RoomID = _room.RoomID,
                PricingID = 11, //SAu này sửa đoạn này
                CreatedBy = currentUserID,
                BookingDate = DateTime.Now,
                CheckInDate = rbtnNhanngay.Checked ? DateTime.Now : dtpCheckinTime.Value,
                CheckOutDate = rbtnNhanngay.Checked
                ? (DateTime?)null   // nhận ngay thì để null, sau này khi checkout mới cập nhật
                : dtpCheckoutTime.Value,
                Status = rbtnNhanngay.Checked ? "CheckedIn" : "Booked",
                Notes = txtNote.Text
            };
            //dtpCheckoutTime.IsAccessible = false;

            var bookingResult = _bookingService.AddBooking(booking); // gọi service/repo lưu DB
            if (bookingResult == null)
            {
                // Lưu booking thất bại
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
                    return;// reload UI phòng

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
                var customer = _customerService.getCustomerByIDNumber(txtCCCD.Text);
                if (customer!=null)
                {
                    addNewBooking(customer.CustomerID, _form1.GetCurrentUser().UserID);
                }
                else
                {
                    IDNumberNotExistUI();
                    Customer newCustomer = new Customer
                    {
                        FullName = txtTen.Text,
                        Address = txtDiachi.Text,
                        Email = txtEmail.Text,
                        Gender = txtGender.Text,
                        IDNumber = txtCCCD.Text,
                        Phone = txtSDT.Text
                    };
                    var addedCustomer = addNewCustomer(newCustomer);
                    if(addedCustomer == null)
                    {
                        MessageBox.Show("Không thể tạo khách hàng mới. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    addNewBooking(addedCustomer.CustomerID, _form1.GetCurrentUser().UserID);
                }
            }
        }


        private bool ValidateCustomerInputs()
        {
            return !string.IsNullOrWhiteSpace(txtTen.Text) &&
                   !string.IsNullOrWhiteSpace(txtDiachi.Text) &&
                   !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                   !string.IsNullOrWhiteSpace(txtSDT.Text) &&
                   !string.IsNullOrWhiteSpace(txtGender.Text) &&
                   !string.IsNullOrWhiteSpace(txtCCCD.Text);
        }
        private void rbtnNhanngay_CheckedChanged(object sender, EventArgs e)
        {
            dtpCheckinTime.Enabled = true;
            dtpCheckoutTime.Enabled = true;
        }
        private void rbtnDattruoc_CheckedChanged(object sender, EventArgs e)
        {
            dtpCheckinTime.Enabled = false;
            dtpCheckoutTime.Enabled = false;
        }
    }
}
