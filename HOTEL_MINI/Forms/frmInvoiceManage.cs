using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Forms.Controls;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
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

            this.Load += frmInvoiceManage_Load;
            dgvBookings.CellDoubleClick += dgvBookings_CellDoubleClick;
            dgvBookings.CellContentClick += dgvBookings_CellContentClick;
            btnSearch.Click += btnSearch_Click;
            btnReset.Click += btnReset_Click;
        }

        // ========================= Events =========================

        private void frmInvoiceManage_Load(object sender, EventArgs e)
        {
            LoadTop20Bookings();
            StyleSearchPanel();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var cccd = (txtCustomerID.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(cccd))
            {
                MessageBox.Show("Vui lòng nhập CCCD/ID để tìm hóa đơn.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var customer = _customerService.GetCustomerByNumberID(cccd);
            if (customer == null)
            {
                MessageBox.Show("Không tìm thấy khách hàng với CCCD/ID này.", "Không có dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _currentCustomer = customer;
            LoadCustomerInfo(customer);
            LoadCustomerBookings(cccd);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadTop20Bookings();
        }

        private void dgvBookings_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvBookings.Rows[e.RowIndex];
            var idObj = row.Cells["BookingID"].Value;
            if (idObj == null) return;

            int bookingRoomId = Convert.ToInt32(idObj);
            var roomNumber = row.Cells["RoomNumber"].Value?.ToString() ?? "";
            OpenInvoiceByBookingRoomId(bookingRoomId, roomNumber);
        }

        private void dgvBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (dgvBookings.Columns[e.ColumnIndex].Name != "colViewInvoice") return;

            var row = dgvBookings.Rows[e.RowIndex];
            var idObj = row.Cells["BookingID"].Value;
            if (idObj == null)
            {
                MessageBox.Show("Thiếu BookingID.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int bookingRoomId = Convert.ToInt32(idObj);
            var roomNumber = row.Cells["RoomNumber"].Value?.ToString() ?? "";
            OpenInvoiceByBookingRoomId(bookingRoomId, roomNumber);
        }

        // ========================= UI Helpers =========================

        private void StyleSearchPanel()
        {
            label1.Font = new Font("Segoe UI Semibold", 11f, FontStyle.Bold);
            txtCustomerID.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            btnSearch.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnReset.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
        }

        private void ConfigureDataGridView()
        {
            var gv = dgvBookings;

            gv.AutoGenerateColumns = false;
            gv.ReadOnly = true;
            gv.MultiSelect = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.RowHeadersVisible = false;
            gv.AllowUserToAddRows = false;
            gv.AllowUserToDeleteRows = false;
            gv.BackgroundColor = Color.White;
            gv.BorderStyle = BorderStyle.None;
            gv.EnableHeadersVisualStyles = false;
            gv.GridColor = Color.Gainsboro;

            gv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(3, 76, 95);
            gv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            gv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            gv.ColumnHeadersHeight = 36;

            gv.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            gv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 243, 246);
            gv.DefaultCellStyle.SelectionForeColor = Color.Black;
            gv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 251);

            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingID",
                Name = "BookingID",
                HeaderText = "BookingID",
                Width = 110
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RoomNumber",
                HeaderText = "Phòng",
                Width = 90
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EmployeeName",
                HeaderText = "Nhân viên",
                Width = 140
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingDateDisplay",
                HeaderText = "Ngày book",
                Width = 130
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckInDateDisplay",
                HeaderText = "Check-in",
                Width = 130
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckOutDateDisplay",
                HeaderText = "Check-out",
                Width = 130
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Trạng thái",
                Width = 110
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Notes",
                HeaderText = "Ghi chú",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            var btnCol = new DataGridViewButtonColumn
            {
                Name = "colViewInvoice",
                HeaderText = "",
                Text = "Xem hóa đơn",
                UseColumnTextForButtonValue = true,
                Width = 130,
                FlatStyle = FlatStyle.Popup
            };
            gv.Columns.Add(btnCol);
        }

        // ========================= Data Loads =========================

        private void LoadTop20Bookings()
        {
            try
            {
                // Nếu bạn có overload onlyCheckedOut thì dùng, không có thì lấy top 20 bình thường rồi lọc CheckedOut.
                var list = _bookingService.GetTop20LatestBookingDisplays(onlyCheckedOut: true) ?? new List<BookingDisplay>();
                dgvBookings.DataSource = list;
                ConfigureDataGridView();

                _currentCustomer = null;
                txtName.Clear();
                txtGender.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                txtDiachi.Clear();
                txtCountBookingByNumberID.Clear();
                txtCustomerID.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải top booking: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomerBookings(string customerId)
        {
            try
            {
                var list = _bookingService
                    .GetBookingDisplaysByCustomerNumber(customerId)
                    .Where(b => string.Equals(b.Status, "CheckedOut", StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(b => b.CheckOutDate)
                    .ToList();

                dgvBookings.DataSource = list;
                ConfigureDataGridView();

                txtCountBookingByNumberID.Text = list.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải booking: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomerInfo(Customer customer)
        {
            if (customer == null) return;

            txtName.Text = customer.FullName ?? "";
            txtGender.Text = customer.Gender ?? "";
            txtPhone.Text = customer.Phone ?? "";
            txtEmail.Text = customer.Email ?? "";
            txtDiachi.Text = customer.Address ?? "";
        }

        // ========================= Open Invoice =========================

        private void OpenInvoiceByBookingRoomId(int bookingRoomId, string roomNumber)
        {
            try
            {
                var booking = _bookingService.GetBookingById(bookingRoomId);
                if (booking == null)
                {
                    MessageBox.Show("Không tìm thấy booking.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customer = _customerService.getCustomerByCustomerID(booking.CustomerID);
                if (customer != null)
                {
                    _currentCustomer = customer;
                    LoadCustomerInfo(customer);
                }
                else if (_currentCustomer == null)
                {
                    MessageBox.Show("Thiếu thông tin khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var invoice = _invoiceService.GetInvoiceByBookingID(bookingRoomId);
                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn cho booking này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var usedSvcs = _bookingService.GetUsedServicesByBookingID(bookingRoomId) ?? new List<UsedServiceDto>();
                var serviceRows = usedSvcs
                    .GroupBy(x => new { x.ServiceName, x.Price })
                    .Select(g => new frmInvoice.InvoiceServiceRow
                    {
                        ServiceName = g.Key.ServiceName,
                        Price = g.Key.Price,
                        Quantity = g.Sum(x => x.Quantity)
                    })
                    .OrderBy(x => x.ServiceName)
                    .ToList();

                string pricingType = "";
                try { pricingType = new RoomPricingRepository().GetPricingTypeById(booking.PricingID)?.PricingType ?? ""; } catch { }

                var resolvedRoom = !string.IsNullOrWhiteSpace(roomNumber)
                    ? roomNumber
                    : new BookingRepository().GetRoomNumberById(booking.RoomID);

                var roomRows = new List<frmInvoice.RoomRow>
                {
                    new frmInvoice.RoomRow
                    {
                        RoomNumber = resolvedRoom,
                        PricingType = pricingType,
                        CheckIn  = booking.CheckInDate?.ToString("dd/MM/yyyy HH:mm") ?? "",
                        CheckOut = booking.CheckOutDate?.ToString("dd/MM/yyyy HH:mm") ?? ""
                    }
                };

                decimal roomCharge = 0m;
                try { roomCharge = _bookingService.GetRoomCharge(booking); } catch { }
                decimal serviceCharge = serviceRows.Sum(x => x.Total);

                decimal discount = invoice.Discount;
                decimal surcharge = invoice.Surcharge;
                decimal total = invoice.TotalAmount > 0 ? invoice.TotalAmount : (roomCharge + serviceCharge + surcharge - discount);

                string employeeName = "";
                try
                {
                    var full = _invoiceService.GetFullNameByInvoiceID(invoice.InvoiceID);
                    employeeName = string.IsNullOrWhiteSpace(full) ? "" : full;
                }
                catch { }

                var payments = _invoiceService.GetPaymentsByInvoiceId(invoice.InvoiceID) ?? new List<Payment>();
                var lastMethod = payments.LastOrDefault()?.Method ?? "";

                var vm = new frmInvoice.InvoiceVm
                {
                    CustomerName = _currentCustomer?.FullName ?? "",
                    CustomerIdNumber = _currentCustomer?.IDNumber ?? "",
                    CheckIn = booking.CheckInDate ?? default,
                    CheckOut = booking.CheckOutDate ?? default,
                    RoomCharge = roomCharge,
                    ServiceCharge = serviceCharge,
                    Discount = discount,
                    Surcharge = surcharge,
                    Total = total,
                    EmployeeName = string.IsNullOrWhiteSpace(employeeName) ? "—" : employeeName,
                    PaymentMethod = string.IsNullOrWhiteSpace(lastMethod) ? "—" : lastMethod,
                    Note = booking.Notes ?? ""
                };

                var f = new frmInvoice
                {
                    StartPosition = FormStartPosition.CenterParent,
                    Text = $"Hóa đơn - Phòng {resolvedRoom}"
                };
                f.BindFrom(vm, serviceRows, roomRows, payments);
                f.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi hiển thị hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
