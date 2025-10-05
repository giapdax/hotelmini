using HOTEL_MINI.BLL;
using HOTEL_MINI.Forms.Controls;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

            // nếu Designer chưa wire, đảm bảo vẫn có:
            this.Load += frmInvoiceManage_Load;
            dgvBookings.CellDoubleClick += dgvBookings_CellDoubleClick;

            // nếu Designer có wire CellContentClick tới method cũ -> ở dưới mình có stub.
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCustomerID.Text))
            {
                MessageBox.Show("Vui lòng nhập ID khách hàng");
                return;
            }

            if (!int.TryParse(txtCustomerID.Text, out int _))
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
                    .Where(b => string.Equals(b.Status, "CheckedOut", StringComparison.OrdinalIgnoreCase))
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

            var bookingRoomIdObj = dgvBookings.Rows[e.RowIndex].Cells["BookingID"].Value;
            if (bookingRoomIdObj == null) return;

            var bookingRoomId = Convert.ToInt32(bookingRoomIdObj);

            var booking = _bookingService.GetBookingById(bookingRoomId);
            if (booking == null)
            {
                MessageBox.Show("Không tìm thấy booking");
                return;
            }

            var customer = _customerService.getCustomerByCustomerID(booking.CustomerID);
            if (customer != null)
            {
                _currentCustomer = customer;
                LoadCustomerInfo(customer);
            }

            var roomNumber = dgvBookings.Rows[e.RowIndex].Cells["RoomNumber"].Value?.ToString() ?? "";
            ShowInvoiceDetails(booking, roomNumber);
        }

        private void ShowInvoiceDetails(Booking booking, string roomNumber)
        {
            try
            {
                var invoice = _invoiceService.GetInvoiceByBookingID(booking.BookingID);
                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn cho booking này");
                    return;
                }
                if (_currentCustomer == null)
                {
                    MessageBox.Show("Không có thông tin khách hàng hiện tại");
                    return;
                }

                // ==== Chuẩn UI: tạo VM & bind vào UcInvoice (không dùng ctor 5 tham số) ====
                var vm = new UcInvoice.InvoiceVm
                {
                    CustomerName = _currentCustomer.FullName ?? "",
                    CustomerIdNumber = _currentCustomer.IDNumber ?? "",
                    CheckIn = booking.CheckInDate ?? default,
                    CheckOut = booking.CheckOutDate ?? default,

                    // Tuỳ entity Invoice của bạn, nếu có các field này thì gán; nếu không, để 0/"" là OK
                    RoomCharge = 0m,
                    ServiceCharge = 0m,
                    Discount = 0m,
                    Surcharge = 0m,
                    Total = 0m,
                    EmployeeName = "",    // có thể lấy từ invoice hoặc user hiện tại
                    PaymentMethod = "",    // nếu invoice có thì gán
                    Note = booking.Notes ?? ""
                };

                var roomRows = new List<UcInvoice.RoomRow>
                {
                    new UcInvoice.RoomRow
                    {
                        RoomNumber = roomNumber,
                        PricingType = "", // nếu muốn thì tra qua PricingRepository rồi gán
                        CheckIn = booking.CheckInDate?.ToString("dd/MM/yyyy HH:mm") ?? "",
                        CheckOut = booking.CheckOutDate?.ToString("dd/MM/yyyy HH:mm") ?? ""
                    }
                };

                // Nếu muốn hiển thị dịch vụ đã dùng, map từ DTO của bạn tại đây
                var serviceRows = new List<UcInvoice.InvoiceServiceRow>();
                // Ví dụ (nếu có BLL method):
                // var used = _bookingService.GetUsedServicesByBookingID(booking.BookingID);
                // foreach (var s in used) serviceRows.Add(new UcInvoice.InvoiceServiceRow { ServiceName = s.ServiceName, Price = s.UnitPrice, Quantity = s.Quantity });

                var payRows = new List<Payment>(); // nếu có lịch sử thanh toán thì fill

                var invoiceControl = new UcInvoice();
                invoiceControl.BindFrom(vm, serviceRows, roomRows, payRows);

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

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadTop20Bookings();
        }

        private void LoadTop20Bookings()
        {
            try
            {
                var bookingDisplays = _bookingService.GetTop20LatestBookingDisplays();
                dgvBookings.DataSource = bookingDisplays;
                dgvBookings.AutoGenerateColumns = false;
                ConfigureDataGridView();

                _currentCustomer = null;
                txtName.Text = txtGender.Text = txtPhone.Text = txtEmail.Text = txtDiachi.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải top 20 booking: {ex.Message}");
            }
        }

        private void frmInvoiceManage_Load(object sender, EventArgs e)
        {
            LoadTop20Bookings();
        }

        // ===== Stub handler để khớp với Designer nếu đã wire =====
        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // không làm gì; để tránh lỗi compile nếu Designer đã đăng ký event này
        }
    }
}
