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
        }
        

        private void btnCheckExistCCCD_Click(object sender, EventArgs e)
        {
            string ccccd = txtCCCD.Text;
            if (string.IsNullOrWhiteSpace(ccccd))
            {
                MessageBox.Show("Please enter a valid ID number.");
                return;
            }
            var exists = _customerService.checkExistNumberID(ccccd);
            if (exists)
            {
                MessageBox.Show("Customer found. Please proceed with booking.");
                // Optionally, you can populate customer details here if needed.
            }
            else
            {
                MessageBox.Show("Customer not found. Please enter new customer details.");
                txtDiachi.Clear();
                txtTen.Clear();
                txtEmail.Clear();
                txtSDT.Clear();
                txtGender.Clear();
                return;
            }
            //var customer = _customerService.getCustomerByIDNumber(ccccd);
            //if (customer != null)
            //{
            //    MessageBox.Show("Customer found and details populated.");
            //    txtCCCD.Text = customer.IDNumber;
            //    txtTen.Text = customer.FullName;
            //    txtDiachi.Text = customer.Address;
            //    txtEmail.Text = customer.Email;
            //    txtSDT.Text = customer.Phone;
            //    txtGender.Text = customer.Gender;
            //    txtCCCD.ReadOnly = true;
            //    txtSDT.ReadOnly = true;
            //    txtTen.ReadOnly = true;
            //    txtDiachi.ReadOnly = true;
            //    txtEmail.ReadOnly = true;
            //    txtGender.ReadOnly = true;    
            //}
            //else
            //{
            //    MessageBox.Show("Customer not found. Please enter new customer details.");
            //    txtDiachi.Clear();
            //    txtTen.Clear();
            //    txtEmail.Clear();
            //    txtSDT.Clear();
            //    txtGender.Clear();
            //    return;
            //}
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

        private void rdbDattruoc_CheckedChanged(object sender, EventArgs e)
        {
            dtpCheckinTime.Enabled = false;
            dtpCheckoutTime.Enabled = false;
        }

        private void rdbCheckinNow_CheckedChanged(object sender, EventArgs e)
        {
            dtpCheckinTime.Enabled = true;
            dtpCheckoutTime.Enabled = true;
        }

        private void btnBookConfirm_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTen.Text) || !string.IsNullOrWhiteSpace(txtDiachi.Text) || !string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    !string.IsNullOrWhiteSpace(txtSDT.Text) || !string.IsNullOrWhiteSpace(txtGender.Text))
            {
                string ten = txtTen.Text;
                string diachi = txtDiachi.Text;
                string sdt = txtSDT.Text;
                string email = txtEmail.Text;
                string gender = txtGender.Text;
                string idNumber = txtCCCD.Text;

                Customer newCustomer = new Customer
                {
                    FullName = ten,
                    Address = diachi,
                    Email = email,
                    Gender = gender,
                    IDNumber = idNumber,
                    Phone = sdt
                };
                var addedCustomer = addNewCustomer(newCustomer);
                Close();
            }

            //var user = _form1.GetCurrentUser();
        }
    }
}
