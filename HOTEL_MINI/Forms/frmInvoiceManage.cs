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

        private Customer _currentCustomer;

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
            LoadInvoices();
        }

        private void StyleSearchPanel()
        {
            label1.Font = new Font("Segoe UI Semibold", 11f, FontStyle.Bold);
            txtSearch.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            btnSearch.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnReset.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            btnXuatHoaDon.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
        }

        private void ConfigureInvoiceGrid()
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

            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "InvoiceID", Name = "InvoiceID", HeaderText = "Mã HĐ", Width = 90 });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BookingID", Name = "BookingID", HeaderText = "Booking", Width = 90 });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerName", Name = "CustomerName", HeaderText = "Khách hàng", Width = 160 });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerIDNumber", Name = "CustomerIDNumber", HeaderText = "CCCD", Width = 120 });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IssuedAt",
                Name = "IssuedAt",
                HeaderText = "Ngày xuất",
                Width = 140,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalAmount",
                Name = "TotalAmount",
                HeaderText = "Tổng tiền",
                Width = 120,
                DefaultCellStyle = { Format = "#,0" }
            });
        }

        private void LoadInvoices()
        {
            try
            {
                var invoices = _invoiceService.GetAllInvoices() ?? new List<Invoice>();
                var displays = invoices.Select(inv =>
                {
                    Customer cust = null;
                    try { cust = _bookingService.GetCustomerByHeaderId(inv.BookingID); } catch { }
                    return new InvoiceDisplay
                    {
                        InvoiceID = inv.InvoiceID,
                        BookingID = inv.BookingID,
                        CustomerName = cust?.FullName ?? "",
                        CustomerIDNumber = cust?.IDNumber ?? "",
                        IssuedAt = inv.IssuedAt,
                        TotalAmount = inv.TotalAmount
                    };
                })
                .OrderByDescending(x => x.IssuedAt ?? DateTime.MinValue)
                .ToList();

                ConfigureInvoiceGrid();
                dgvBookings.DataSource = displays;

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
                MessageBox.Show($"Lỗi khi tải hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dgvBookings_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null) return;

            try
            {
                int invoiceId;
                if (dgvBookings.CurrentRow.DataBoundItem is InvoiceDisplay disp)
                    invoiceId = disp.InvoiceID;
                else
                {
                    var idObj = dgvBookings.CurrentRow.Cells["InvoiceID"]?.Value;
                    if (idObj == null) return;
                    invoiceId = Convert.ToInt32(idObj);
                }

                var inv = _invoiceService.GetInvoice(invoiceId);
                if (inv == null) return;

                var customer = _bookingService.GetCustomerByHeaderId(inv.BookingID);
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

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var keyword = (txtSearch.Text ?? "").Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadInvoices();
                return;
            }

            try
            {
                var invoices = _invoiceService.GetAllInvoices() ?? new List<Invoice>();
                var filtered = invoices.Select(inv =>
                {
                    Customer cust = null;
                    try { cust = _bookingService.GetCustomerByHeaderId(inv.BookingID); } catch { }
                    return new InvoiceDisplay
                    {
                        InvoiceID = inv.InvoiceID,
                        BookingID = inv.BookingID,
                        CustomerName = cust?.FullName ?? "",
                        CustomerIDNumber = cust?.IDNumber ?? "",
                        IssuedAt = inv.IssuedAt,
                        TotalAmount = inv.TotalAmount
                    };
                })
                .Where(d =>
                    d.InvoiceID.ToString().Contains(keyword) ||
                    d.BookingID.ToString().Contains(keyword) ||
                    (d.CustomerName ?? "").ToLower().Contains(keyword) ||
                    (d.CustomerIDNumber ?? "").ToLower().Contains(keyword)
                )
                .OrderByDescending(d => d.IssuedAt ?? DateTime.MinValue)
                .ToList();

                dgvBookings.DataSource = filtered ?? new List<InvoiceDisplay>();
                _currentCustomer = null;
                txtName.Clear(); txtGender.Clear(); txtPhone.Clear(); txtEmail.Clear(); txtDiachi.Clear();
                txtCountBookingByNumberID.Clear();
                lblCountBookingByNumberID.Visible = false;
                txtCountBookingByNumberID.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            if (dgvBookings.CurrentRow == null)
            {
                MessageBox.Show("Hãy chọn một hóa đơn để xuất PDF.", "Thiếu lựa chọn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // 1) Lấy InvoiceID từ row đang chọn
                int invoiceId;
                if (dgvBookings.CurrentRow.DataBoundItem is InvoiceDisplay disp)
                    invoiceId = disp.InvoiceID;
                else
                {
                    var idObj = dgvBookings.CurrentRow.Cells["InvoiceID"]?.Value;
                    if (idObj == null)
                    {
                        MessageBox.Show("Thiếu InvoiceID.", "Lỗi dữ liệu",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    invoiceId = Convert.ToInt32(idObj);
                }

                // 2) Lấy invoice + customer
                var inv = _invoiceService.GetInvoice(invoiceId);
                if (inv == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var customer = _bookingService.GetCustomerByHeaderId(inv.BookingID);
                if (customer == null)
                {
                    MessageBox.Show("Thiếu thông tin khách hàng.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Bạn có muốn xuất hóa đơn ra file PDF không?",
                    "Xuất hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                using (var sfd = new SaveFileDialog
                {
                    Filter = "PDF File|*.pdf",
                    FileName = $"Invoice_{invoiceId}_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                })
                {
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    // 3) Build chi tiết phòng + dịch vụ cho HÓA ĐƠN đang chọn
                    // ƯU TIÊN: nếu đã có mapping Invoice -> BookingRoom
                    var roomIds = new List<int>();
                    try
                    {
                        // Bật dòng này nếu bạn đã implement trong InvoiceService:
                        // roomIds = _invoiceService.GetBookingRoomIdsByInvoiceId(invoiceId) ?? new List<int>();
                    }
                    catch { /* bỏ qua nếu chưa có hàm */ }

                    // FALLBACK: lấy tất cả phòng thuộc Booking của invoice
                    if (roomIds == null || roomIds.Count == 0)
                    {
                        try
                        {
                            var brRepo = new BookingRoomRepository();
                            roomIds = brRepo.GetBookingRoomIdsByBookingId(inv.BookingID) ?? new List<int>();
                        }
                        catch { roomIds = new List<int>(); }
                    }

                    var roomLines = new List<(BookingRoom Room, string RoomNumber, string PricingType, decimal UnitPrice, int Quantity)>();
                    var usedServices = new List<UsedServiceDto>();

                    if (roomIds.Count > 0)
                    {
                        var bookingRepo = new BookingRepository();
                        var brRepo = new BookingRoomRepository();
                        var pricingRepo = new RoomPricingRepository();

                        foreach (var id in roomIds.Distinct())
                        {
                            var line = brRepo.GetBookingRoomById(id);
                            if (line == null) continue;

                            string roomNumber = "";
                            try { roomNumber = bookingRepo.GetRoomNumberById(line.RoomID); } catch { }

                            string pricingType = "";
                            decimal unitPrice = 0m;
                            try
                            {
                                var pr = pricingRepo.GetPricingTypeById(line.PricingID);
                                if (pr != null) { pricingType = pr.PricingType ?? ""; unitPrice = pr.Price; }
                            }
                            catch { }

                            decimal roomCharge = 0m;
                            try { roomCharge = _bookingService.GetRoomCharge(line); } catch { }

                            // Quy đổi để hiển thị: nếu có đơn giá thì suy ra quantity ~ số "đơn vị" giá
                            int qty;
                            if (unitPrice > 0m)
                            {
                                var q = Math.Round(roomCharge / unitPrice, MidpointRounding.AwayFromZero);
                                qty = (q >= 1 && q <= int.MaxValue) ? (int)q : 1;
                            }
                            else
                            {
                                // Không có đơn giá: cho unitPrice = roomCharge, qty = 1 để tổng = roomCharge
                                unitPrice = roomCharge;
                                qty = 1;
                            }

                            roomLines.Add((line, roomNumber, pricingType, unitPrice, qty));
                        }

                        // Gộp dịch vụ theo (ServiceName, Price) cho tất cả phòng thuộc invoice/booking
                        try
                        {
                            usedServices = roomIds
                                .SelectMany(id => _bookingService.GetUsedServicesByBookingID(id) ?? new List<UsedServiceDto>())
                                .GroupBy(x => new { x.ServiceName, x.Price })
                                .Select(g => new UsedServiceDto
                                {
                                    ServiceName = g.Key.ServiceName,
                                    Price = g.Key.Price,
                                    Quantity = g.Sum(x => x.Quantity)
                                })
                                .ToList();
                        }
                        catch { usedServices = new List<UsedServiceDto>(); }
                    }

                    // 4) Gọi đúng HÀM EXPORT CHUNG (không viết hàm export mới)
                    var pdf = new PdfExportService(customer.FullName, customer.IDNumber);
                    pdf.ExportInvoiceToPdf(inv, roomLines, usedServices, sfd.FileName);

                    MessageBox.Show("Đã xuất hóa đơn thành công.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xử lý xuất hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private class InvoiceDisplay
        {
            public int InvoiceID { get; set; }
            public int BookingID { get; set; }
            public string CustomerName { get; set; }
            public string CustomerIDNumber { get; set; }
            public DateTime? IssuedAt { get; set; }
            public decimal TotalAmount { get; set; }
        }
    }
}
