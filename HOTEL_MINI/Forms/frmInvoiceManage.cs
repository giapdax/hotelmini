using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
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

            Load += frmInvoiceManage_Load;


            dgvBookings.SelectionChanged += dgvBookings_SelectionChanged;
        }

        private void frmInvoiceManage_Load(object sender, EventArgs e)
        {
            StyleSearchPanel();
            LoadBookings();
        }

        private void dgvBookings_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null) return;

            try
            {
                int bookingRoomId;
                var bound = dgvBookings.CurrentRow.DataBoundItem as BookingFlatDisplay; // <-- SỬA

                if (bound != null)
                {
                    bookingRoomId = bound.BookingRoomID; // <-- SỬA
                }
                else
                {
                    var idObj = dgvBookings.CurrentRow.Cells["BookingRoomID"]?.Value;   // <-- SỬA
                    if (idObj == null) return;
                    bookingRoomId = Convert.ToInt32(idObj);
                }

                var headerBookingId = new BookingRepository().GetBookingIdByBookingRoomId(bookingRoomId);
                if (headerBookingId <= 0) return;

                var customer = _bookingService.GetCustomerByHeaderId(headerBookingId);
                if (customer == null) return;

                _currentCustomer = customer;
                UpdateCustomerPanel(customer);

                if (!string.IsNullOrWhiteSpace(customer.IDNumber))
                {
                    var allById = _bookingService.GetBookingDisplaysByCustomerNumber(customer.IDNumber) ?? new List<BookingDisplay>();
                    txtCountBookingByNumberID.Text = allById.Count.ToString();
                    txtCountBookingByNumberID.Visible = true;
                    lblCountBookingByNumberID.Visible = true;
                }
                else
                {
                    txtCountBookingByNumberID.Clear();
                    txtCountBookingByNumberID.Visible = false;
                    lblCountBookingByNumberID.Visible = false;
                }
            }
            catch { }
        }


        private void StyleSearchPanel()
        {
            label1.Font = new Font("Segoe UI Semibold", 11f, FontStyle.Bold);
            txtSearch.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            btnSearch.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnReset.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnt.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnXuatHoaDon.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
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

            gv.Columns.Clear();

            // ID line phòng (đúng là BookingRoomID)
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingRoomID",   // <-- SỬA
                Name = "BookingRoomID",               // <-- SỬA
                HeaderText = "BookingID",
                Width = 110
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RoomNumber",
                Name = "RoomNumber",
                HeaderText = "Phòng",
                Width = 90
            });

            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "EmployeeName",
                HeaderText = "Nhân viên",
                Width = 140
            });

            // 3 cột ngày: dùng đúng tên trường, KHÔNG có *Display
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingDate",     // <-- SỬA
                HeaderText = "Ngày book",
                Width = 130,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckInDate",     // <-- SỬA
                HeaderText = "Check-in",
                Width = 130,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckOutDate",    // <-- SỬA
                HeaderText = "Check-out",
                Width = 130,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
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

            // 3 cột thông tin khách hàng
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerIDNumber",
                Name = "CustomerIDNumber",
                HeaderText = "CCCD",
                Width = 120
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerName",    // <-- PHẢI CÓ trong model
                Name = "CustomerName",
                HeaderText = "Khách hàng",
                Width = 140
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerPhone",   // <-- PHẢI CÓ trong model
                Name = "CustomerPhone",
                HeaderText = "SĐT",
                Width = 120
            });
        }


        private void LoadBookings()
        {
            try
            {
                var list = _bookingService.GetAllBookingFlatDisplays()
                .Where(b => string.Equals(b.Status, "CheckedOut", StringComparison.OrdinalIgnoreCase))
                 .OrderByDescending(b => b.CheckOutDate ?? b.CheckInDate)
                 .ToList();

                ConfigureDataGridView();
                dgvBookings.DataSource = list;

                _currentCustomer = null;
                txtName.Clear();
                txtGender.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                txtDiachi.Clear();
                txtCountBookingByNumberID.Clear();
                txtSearch.Clear();
                lblCountBookingByNumberID.Visible = false;
                txtCountBookingByNumberID.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải top booking: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                ConfigureDataGridView();
                dgvBookings.DataSource = list;

                txtCountBookingByNumberID.Text = list.Count.ToString();
                lblCountBookingByNumberID.Visible = true;
                txtCountBookingByNumberID.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải booking: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCustomerPanel(Customer customer)
        {
            if (customer == null) return;
            txtName.Text = customer.FullName ?? "";
            txtGender.Text = customer.Gender ?? "";
            txtPhone.Text = customer.Phone ?? "";
            txtEmail.Text = customer.Email ?? "";
            txtDiachi.Text = customer.Address ?? "";
        }

        private void OpenInvoiceByBookingRoomId(int bookingRoomId, string roomNumberHint)
        {
            try
            {
                var headerBookingId = new BookingRepository().GetBookingIdByBookingRoomId(bookingRoomId);
                if (headerBookingId <= 0)
                {
                    MessageBox.Show("Không tìm thấy Header BookingID từ BookingRoomID.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customer = _bookingService.GetCustomerByHeaderId(headerBookingId);
                if (customer == null)
                {
                    MessageBox.Show("Thiếu thông tin khách hàng.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var line = new BookingRoomRepository().GetBookingRoomById(bookingRoomId);
                if (line == null)
                {
                    MessageBox.Show("Không tìm thấy booking line.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var invoice = _invoiceService.GetInvoiceByBookingID(headerBookingId);
                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn cho booking này.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var usedSvcs = _bookingService.GetUsedServicesByBookingID(bookingRoomId) ?? new List<UsedServiceDto>();

                string pricingType = "";
                decimal unitPrice = 0m;
                try
                {
                    var pr = new RoomPricingRepository().GetPricingTypeById(line.PricingID);
                    if (pr != null) { pricingType = pr.PricingType; unitPrice = pr.Price; }
                }
                catch { }

                var resolvedRoom = !string.IsNullOrWhiteSpace(roomNumberHint)
                    ? roomNumberHint
                    : new BookingRepository().GetRoomNumberById(line.RoomID);

                decimal roomCharge = 0m;
                try { roomCharge = _bookingService.GetRoomCharge(line); } catch { }

                int quantity = 1;
                if (unitPrice > 0m)
                {
                    var q = Math.Round(roomCharge / unitPrice, MidpointRounding.AwayFromZero);
                    if (q >= 1 && q <= int.MaxValue) quantity = (int)q;
                }

                var ask = MessageBox.Show("Bạn có muốn xuất hóa đơn ra file PDF không?",
                    "Xuất hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (ask != DialogResult.Yes) return;

                using (var sfd = new SaveFileDialog
                {
                    Filter = "PDF File|*.pdf",
                    FileName = $"Invoice_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var export = new PdfExportService(customer.FullName, customer.IDNumber);

                        var roomLines = new List<(BookingRoom Room, string RoomNumber, string PricingType, decimal UnitPrice, int Quantity)>
                        {
                            (line, resolvedRoom, pricingType, unitPrice, quantity)
                        };

                        export.ExportInvoiceToPdf(
                            invoice,
                            roomLines,
                            usedSvcs,
                            sfd.FileName
                        );

                        MessageBox.Show("Đã xuất hóa đơn thành công.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = (txtSearch.Text ?? "").Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadBookings();
                return;
            }

            try
            {
                var filtered = _bookingService
                    .GetAllBookingFlatDisplays()             // <-- SỬA
                    ?.Where(b => string.Equals(b.Status, "CheckedOut", StringComparison.OrdinalIgnoreCase))
                    .Where(b =>
                        (b.RoomNumber?.ToLower().Contains(keyword) ?? false) ||
                        (b.EmployeeName?.ToLower().Contains(keyword) ?? false) ||
                        (b.Status?.ToLower().Contains(keyword) ?? false) ||
                        (b.Notes?.ToLower().Contains(keyword) ?? false) ||
                        (b.CustomerIDNumber?.ToLower().Contains(keyword) ?? false) ||
                        (b.CustomerName?.ToLower().Contains(keyword) ?? false) ||
                        (b.CustomerPhone?.ToLower().Contains(keyword) ?? false))
                    .OrderByDescending(b => b.CheckOutDate ?? b.CheckInDate)
                    .ToList();

                dgvBookings.DataSource = filtered ?? new List<BookingFlatDisplay>(); // <-- SỬA kiểu danh sách
                _currentCustomer = null;
                txtName.Clear(); txtGender.Clear(); txtPhone.Clear(); txtEmail.Clear(); txtDiachi.Clear();
                txtCountBookingByNumberID.Clear();
                lblCountBookingByNumberID.Visible = false;
                txtCountBookingByNumberID.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadBookings();
        }

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null)
            {
                MessageBox.Show("Hãy chọn một dòng để xuất hóa đơn.", "Thiếu lựa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var row = dgvBookings.CurrentRow;

                int bookingRoomId;
                string roomNumber;

                var bound = row.DataBoundItem as BookingFlatDisplay; 
                if (bound != null)
                {
                    bookingRoomId = bound.BookingRoomID;      
                    roomNumber = bound.RoomNumber ?? "";
                }
                else
                {
                    var idObj = row.Cells["BookingRoomID"]?.Value;   
                    if (idObj == null)
                    {
                        MessageBox.Show("Thiếu BookingRoomID.", "Lỗi dữ liệu",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    bookingRoomId = Convert.ToInt32(idObj);
                    roomNumber = dgvBookings.Columns.Contains("RoomNumber")
                                 ? row.Cells["RoomNumber"]?.Value?.ToString() ?? ""
                                 : "";
                }

                OpenInvoiceByBookingRoomId(bookingRoomId, roomNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý xuất hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
