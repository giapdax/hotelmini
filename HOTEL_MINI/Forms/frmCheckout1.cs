// frmCheckout1.cs
using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmCheckout1 : Form
    {
        // ===== Events cho form cha/UC =====
        public event Action<bool, int> PaidCompleted;  // isFull, invoiceId
        public event Action<int> RequestPrintInvoice;  // invoiceId

        // ===== Services/Repos =====
        private readonly BookingService _bookingSvc = new BookingService();
        private readonly InvoiceRepository _invRepo = new InvoiceRepository();
        private readonly PaymentRepository _payRepo = new PaymentRepository();
        private readonly PaymentService _paySvc = new PaymentService(); // dùng để load lịch sử theo header nếu bạn đã có

        // ===== State =====
        private readonly int _headerBookingId;              // BookingID (header)
        private readonly List<int> _selectedBookingRoomIds; // các BookingID/phòng được checkout
        private readonly int _currentUserId;

        // Tổng hợp
        private List<Booking> _allRooms;
        private decimal _roomTotal, _serviceTotal, _subtotal, _daTra, _conLai;
        private int _invoiceId;

        private readonly CultureInfo vi = new CultureInfo("vi-VN");

        // --------- Helper: nếu form gọi new frmCheckout1(ids, userId) ----------
        private static int ResolveHeaderId(List<int> bookingIds)
        {
            if (bookingIds == null || bookingIds.Count == 0)
                throw new ArgumentException("Danh sách BookingID trống.");
            // TODO: nếu có bảng header thật sự thì map id tại đây
            return bookingIds[0];
        }

        public frmCheckout1(List<int> bookingIds, int currentUserId)
            : this(ResolveHeaderId(bookingIds), bookingIds, currentUserId) { }

        public frmCheckout1(int headerBookingId, List<int> selectedBookingRoomIds, int currentUserId)
        {
            InitializeComponent();

            _headerBookingId = headerBookingId;
            _selectedBookingRoomIds = selectedBookingRoomIds ?? new List<int>();
            _currentUserId = currentUserId;

            BuildUiBehavior();
            LoadDataAndBind(); // tính tổng + tạo/tìm invoice
        }

        // ============================ UI ============================
        private void BuildUiBehavior()
        {
            // Room grid
            dgvRoom.ReadOnly = true;
            dgvRoom.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoom.RowHeadersVisible = false;
            dgvRoom.AllowUserToAddRows = false;
            dgvRoom.AllowUserToDeleteRows = false;

            // Service grid
            dgvUsedService.ReadOnly = true;
            dgvUsedService.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsedService.RowHeadersVisible = false;
            dgvUsedService.AllowUserToAddRows = false;
            dgvUsedService.AllowUserToDeleteRows = false;

            // Payment history grid (đã đặt sẵn trong Designer)
            dgvPayments.ReadOnly = true;
            dgvPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPayments.RowHeadersVisible = false;
            dgvPayments.AllowUserToAddRows = false;
            dgvPayments.AllowUserToDeleteRows = false;
            dgvPayments.Dock = DockStyle.Fill;

            // Payment methods
            try
            {
                var methods = _bookingSvc.getPaymentMethods();
                if (methods != null && methods.Count > 0)
                    cbxPaymentMethod.DataSource = methods;
                else
                    cbxPaymentMethod.Items.AddRange(new object[] { "Cash", "Card", "Transfer" });
            }
            catch
            {
                cbxPaymentMethod.Items.AddRange(new object[] { "Cash", "Card", "Transfer" });
            }

            // Pay options
            cboPayOption.Items.Clear();
            cboPayOption.Items.AddRange(new object[] { "Trả đủ", "Trả một phần" });
            cboPayOption.SelectedIndex = 0;
            cboPayOption.SelectedIndexChanged += (s, e) => ApplyPayOption();

            // Inputs
            txtDiscount.Text = "0";
            txtSurcharge.Text = "0";
            txtPayNow.Text = "0";

            txtDiscount.TextChanged += (s, e) => RecalcTotals();
            txtSurcharge.TextChanged += (s, e) => RecalcTotals();
            txtDiscount.KeyPress += NumericOnly_KeyPress;
            txtSurcharge.KeyPress += NumericOnly_KeyPress;
            txtPayNow.KeyPress += NumericOnly_KeyPress;

            // Buttons
            btnConfirm.Text = "Thanh toán";
            btnCancel.Text = "Đóng";
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += (s, e) => Close();

            // fix: tránh nút bị khuất
            buttonsPanel.WrapContents = false;
            buttonsPanel.AutoSize = true;
            buttonsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnConfirm.Anchor = AnchorStyles.Right;
            btnCancel.Anchor = AnchorStyles.Right;
        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private decimal ParseMoney(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0m;
            if (decimal.TryParse(s, NumberStyles.AllowThousands, vi, out var v)) return v;
            if (decimal.TryParse(s, out v)) return v;
            return 0m;
        }

        private void SetMoney(TextBox txt, decimal value, bool bold = false)
        {
            txt.Text = value.ToString("#,0", vi);
            txt.Font = new Font(txt.Font, bold ? FontStyle.Bold : FontStyle.Regular);
        }

        // ====================== Load & bind ======================
        private void LoadDataAndBind()
        {
            // 1) Load các booking thuộc header
            _allRooms = _bookingSvc.GetBookingsByHeaderId(_headerBookingId) ?? new List<Booking>();

            // 2) Header info
            var first = _allRooms.FirstOrDefault();
            if (first != null)
            {
                try
                {
                    var cust = new BookingRepository().GetCustomerBasicById(first.CustomerID);
                    txtCusName.Text = cust?.FullName ?? "";
                    txtCusId.Text = cust?.IDNumber ?? "";
                }
                catch { }

                txtCheckin.Text = first.CheckInDate?.ToString("dd/MM/yyyy HH:mm") ?? "";
                txtCheckout.Text = (first.CheckOutDate ?? DateTime.Now).ToString("dd/MM/yyyy HH:mm");
            }

            // 3) Bảng phòng + tính tiền
            var rows = new List<dynamic>();
            _roomTotal = 0; _serviceTotal = 0;

            foreach (var b in _allRooms)
            {
                if (!b.CheckOutDate.HasValue && b.CheckInDate.HasValue && DateTime.Now > b.CheckInDate.Value)
                    b.CheckOutDate = DateTime.Now;

                var roomSub = _bookingSvc.GetRoomCharge(b);
                var svcs = _bookingSvc.GetUsedServicesByBookingID(b.BookingID) ?? new List<UsedServiceDto>();
                var svcTotal = svcs.Sum(x => x.Price * x.Quantity);

                string pricingType = "";
                decimal unitPrice = 0m;
                try
                {
                    var pr = new RoomPricingRepository().GetPricingTypeById(b.PricingID);
                    if (pr != null) { pricingType = pr.PricingType; unitPrice = pr.Price; }
                }
                catch { }

                string roomNumber = "";
                try { roomNumber = new BookingRepository().GetRoomNumberById(b.RoomID); } catch { }

                _roomTotal += roomSub;
                _serviceTotal += svcTotal;

                rows.Add(new
                {
                    Phòng = roomNumber,
                    CheckIn = b.CheckInDate?.ToString("dd/MM HH:mm"),
                    CheckOut = b.CheckOutDate?.ToString("dd/MM HH:mm"),
                    GiáTheo = pricingType,
                    ĐơnGiá = unitPrice.ToString("#,0", vi),
                    TiềnPhòng = roomSub.ToString("#,0", vi),
                    TổngDV = svcTotal.ToString("#,0", vi),
                    TổngCộng = (roomSub + svcTotal).ToString("#,0", vi)
                });
            }

            dgvRoom.AutoGenerateColumns = true;
            dgvRoom.DataSource = rows;

            // 4) Bảng dịch vụ (gộp)
            var allSvc = _allRooms
                .SelectMany(b => _bookingSvc.GetUsedServicesByBookingID(b.BookingID) ?? new List<UsedServiceDto>())
                .GroupBy(x => x.ServiceName)
                .Select(g => new
                {
                    DịchVụ = g.Key,
                    ĐơnGiá = g.First().Price.ToString("#,0", vi),
                    SL = g.Sum(x => x.Quantity),
                    ThànhTiền = g.Sum(x => x.Price * x.Quantity).ToString("#,0", vi)
                })
                .ToList();

            dgvUsedService.AutoGenerateColumns = true;
            dgvUsedService.DataSource = allSvc;

            // 5) Tổng số hiển thị
            SetMoney(txtServiceCharge, _serviceTotal);
            SetMoney(txtTongtien, _roomTotal);
            SetMoney(txtRoomTotal2, _roomTotal);
            SetMoney(txtServiceTotal2, _serviceTotal);

            // 6) Tính & tạo/lấy invoice
            RecalcTotals();       // => _invoiceId, _daTra, _conLai…
            LoadPayments();       // load lịch sử
            ApplyPayOption();     // set txtPayNow theo lựa chọn

            // 7) Nhân viên
            try
            {
                var userName = new BookingRepository().GetUserFullNameById(_currentUserId);
                txtEmployeeName.Text = string.IsNullOrWhiteSpace(userName) ? $"User#{_currentUserId}" : userName;
            }
            catch { txtEmployeeName.Text = $"User#{_currentUserId}"; }
        }

        // ====================== Totals & invoice ======================
        private void RecalcTotals()
        {
            var surcharge = ParseMoney(txtSurcharge.Text);
            var discount = ParseMoney(txtDiscount.Text);

            _subtotal = _roomTotal + _serviceTotal + surcharge - discount;
            if (_subtotal < 0) _subtotal = 0;

            // Tạo hoặc lấy invoice đang mở cho header này
            _invoiceId = _invRepo.CreateOrGetOpenInvoice(
                bookingHeaderId: _headerBookingId,
                roomCharge: _roomTotal,
                serviceCharge: _serviceTotal,
                discount: discount,
                surcharge: surcharge,
                issuedByUserIfPaid: 0
            );

            // Đọc lại tổng/đã trả/còn lại
            var t = _invRepo.GetInvoiceTotals(_invoiceId);
            _daTra = t.Paid;
            _conLai = t.Remain;

            SetMoney(txtSubtotal, _subtotal);
            SetMoney(txtTotalAmount, _subtotal);
            SetMoney(txtDaTra, _daTra);
            SetMoney(txtConLai, _conLai, bold: true);
        }

        private void LoadPayments()
        {
            try
            {
                // Nếu bạn có hàm theo Invoice, thay bằng GetPaymentsByInvoice(_invoiceId)
                var list = _paySvc.GetPaymentsByHeader(_headerBookingId) ?? new List<Payment>();
                dgvPayments.DataSource = list.Select(x => new
                {
                    Ngày = x.PaymentDate.ToString("dd/MM/yyyy HH:mm"),
                    HìnhThức = x.Method,
                    SốTiền = x.Amount.ToString("#,0", vi),
                    TrạngThái = x.Status
                }).ToList();
            }
            catch { /* ignore */ }
        }

        private void ApplyPayOption()
        {
            var full = cboPayOption.SelectedItem?.ToString() == "Trả đủ";
            txtPayNow.Enabled = !full;
            if (full) SetMoney(txtPayNow, _conLai);
            else if (ParseMoney(txtPayNow.Text) <= 0) txtPayNow.Text = "0";
        }

        // ====================== Confirm ======================
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            var method = cbxPaymentMethod.SelectedItem?.ToString() ?? "Cash";
            var payNow = (cboPayOption.SelectedItem?.ToString() == "Trả đủ")
                ? _conLai
                : ParseMoney(txtPayNow.Text);

            if (payNow < 0) payNow = 0;
            if (payNow == 0 && _conLai > 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền > 0.", "Thiếu số tiền",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (payNow > _conLai + 0.0001m)
            {
                MessageBox.Show("Số tiền thanh toán vượt quá số còn lại.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Ghi 1 lần thanh toán an toàn (transaction + lock + update status)
                _payRepo.AddPaymentSafe(_invoiceId, payNow, method, "Completed", _currentUserId);

                // Reload lại số liệu sau khi thanh toán
                RecalcTotals();
                LoadPayments();

                var isFull = _conLai <= 0m;
                if (isFull)
                {
                    MessageBox.Show(
                        $"Đã thanh toán đủ.\nTổng: {_subtotal.ToString("#,0", vi)}",
                        "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    PaidCompleted?.Invoke(true, _invoiceId);
                    RequestPrintInvoice?.Invoke(_invoiceId);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        $"Đã ghi nhận {payNow.ToString("#,0", vi)}.\nCòn lại: {_conLai.ToString("#,0", vi)}",
                        "Đã ghi nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    PaidCompleted?.Invoke(false, _invoiceId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
