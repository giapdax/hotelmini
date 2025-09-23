using HOTEL_MINI.BLL;
using HOTEL_MINI.Forms.Controls;
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
    public partial class frmInvoiceManage : Form
    {
        private readonly CustomerService _customerService;
        private readonly BookingService _bookingService;
        private readonly InvoiceService _invoiceService;
        private readonly frmApplication _mainForm;
        private Customer _currentCustomer = null;
        public frmInvoiceManage(frmApplication mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            _customerService = new CustomerService();
            _bookingService = new BookingService();
            _invoiceService = new InvoiceService();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerID.Text))
            {
                MessageBox.Show("Vui lòng nhập ID khách hàng");
                return;
            }

            if (!int.TryParse(txtCustomerID.Text, out int customerId))
            {
                MessageBox.Show("ID khách hàng phải là số");
                return;
            }
            var customer = _customerService.GetCustomerByNumberID(txtCustomerID.Text);
            
            if (customer == null)
            {
                MessageBox.Show("Không tìm thấy khách hàng với ID đã nhập");
                return;
            }
            _currentCustomer = customer;
            LoadCustomerInfo(customer);

            LoadCustomerBookings(txtCustomerID.Text);
        }
        public void LoadCustomerInfo(Customer customer)
        {
            if (customer == null) return;

            txtName.Text = customer.FullName ?? "";
            txtGender.Text = customer.Gender ?? "";
            txtPhone.Text = customer.Phone ?? "";
            txtEmail.Text = customer.Email ?? "";
            txtDiachi.Text = customer.Address ?? "";
        }
        private void LoadCustomerBookings(string customerId)
        {
            try
            {
                var bookingDisplays = _bookingService.GetBookingDisplaysByCustomerNumber(customerId)
                    .Where(b => b.Status == "CheckedOut") // Chỉ hiện booking đã checkout
                    .ToList();

                dgvBookings.DataSource = bookingDisplays;
                dgvBookings.AutoGenerateColumns = false;
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải booking: {ex.Message}");
            }
        }
        private void ConfigureDataGridView()
        {
            dgvBookings.Columns.Clear();

            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RoomNumber",
                Name = "RoomNumber",
                HeaderText = "Phòng",
                Width = 80
            });

            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EmployeeName",
                HeaderText = "Nhân viên",
                Width = 120
            });

            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingDateDisplay",
                HeaderText = "Ngày book",
                Width = 120
            });

            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckInDateDisplay",
                HeaderText = "Check-in",
                Width = 120
            });

            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckOutDateDisplay",
                HeaderText = "Check-out",
                Width = 120
            });

            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Notes",
                HeaderText = "Ghi chú",
                Width = 150
            });

            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Trạng thái",
                Width = 100
            });

            // Ẩn BookingID nhưng vẫn giữ để truy vấn
            dgvBookings.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingID",
                Name = "BookingID",
                HeaderText = "BookingID",
                Visible = false
            });
        }
        private void dgvBookings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Lấy BookingID từ hidden column
            var bookingId = (int)dgvBookings.Rows[e.RowIndex].Cells["BookingID"].Value;
            var roomNumber = dgvBookings.Rows[e.RowIndex].Cells["RoomNumber"].Value.ToString();

            // Lấy thông tin đầy đủ từ service
            var booking = _bookingService.GetBookingById(bookingId);
            if (booking != null)
            {
                ShowInvoiceDetails(booking, roomNumber);
            }
        }
        private void ShowInvoiceDetails(Booking booking, string roomNumber)
        {
            try
            {
                // Lấy thông tin liên quan
                var invoice = _invoiceService.GetInvoiceByBookingID(booking.BookingID);

                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn cho booking này");
                    return;
                }
                //if (_currentCustomer == null)
                //{
                //    MessageBox.Show("Không có thông tin khách hàng hiện tại");
                //    return;
                //}
                //MessageBox.Show($"{_currentCustomer.FullName}");
                // Tạo và hiển thị UC Invoice
                var invoiceControl = new UcInvoice(booking, roomNumber, invoice, _currentCustomer.FullName, _currentCustomer.IDNumber);

                // Tạo form popup
                var popupForm = new Form
                {
                    Text = $"Hóa đơn - Phòng {roomNumber}",
                    Size = new Size(648, 1116),
                    StartPosition = FormStartPosition.CenterScreen
                };

                popupForm.Controls.Add(invoiceControl);
                invoiceControl.Dock = DockStyle.Fill;

                popupForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị hóa đơn: {ex.Message}");
            }
        }
    }
}
