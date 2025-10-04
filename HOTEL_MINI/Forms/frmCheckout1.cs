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
        // isFull: đã thanh toán đủ?  invoiceId: id hoá đơn
        public event Action<bool, int> PaidCompleted;
        // Yêu cầu in/xem hoá đơn sau khi thanh toán đủ
        public event Action<int> RequestPrintInvoice;

        // ===== Services =====
        private readonly BookingService _svc = new BookingService();
        private readonly PaymentService _paySvc = new PaymentService();

        // ===== State =====
        private readonly int _headerBookingId;              // BookingID (header)
        private readonly List<int> _selectedBookingRoomIds; // các BookingRoomID/BookingID được checkout
        private readonly int _currentUserId;

        private List<Booking> _allRooms;
        private decimal _roomTotal, _serviceTotal, _subtotal, _daTra, _conLai;
        private int _invoiceId;

        // Lưới lịch sử thanh toán (tạo runtime gắn vào summaryLayout)
        private DataGridView dgvPayments;

        // ===== Overload để gọi new frmCheckout1(ids, _currentUserId) =====
        private static int ResolveHeaderId(List<int> bookingIds)
        {
            if (bookingIds == null || bookingIds.Count == 0)
                throw new ArgumentException("Danh sách BookingID trống.");
            // Nếu dự án có header riêng, map bookingRoomId -> headerId ở đây
            return bookingIds[0];
        }

        public frmCheckout1(List<int> bookingIds, int currentUserId)
            : this(ResolveHeaderId(bookingIds), bookingIds, currentUserId)
        {
        }

        public frmCheckout1(int headerBookingId, List<int> selectedBookingRoomIds, int currentUserId)
        {
            InitializeComponent();
            _headerBookingId = headerBookingId;
            _selectedBookingRoomIds = selectedBookingRoomIds ?? new List<int>();
            _currentUserId = currentUserId;

            BuildUiBehavior();
            EnsurePaymentHistoryUi();   // tạo khối "Lịch sử thanh toán"
            LoadDataAndBind();
        }

        #region UI helpers
        private readonly CultureInfo vi = new CultureInfo("vi-VN");

        private void BuildUiBehavior()
        {
            // Rooms grid
            dgvRoom.ReadOnly = true;
            dgvRoom.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoom.RowHeadersVisible = false;
            dgvRoom.AllowUserToAddRows = false;
            dgvRoom.AllowUserToDeleteRows = false;

            // Services grid
            dgvUsedService.ReadOnly = true;
            dgvUsedService.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsedService.RowHeadersVisible = false;
            dgvUsedService.AllowUserToAddRows = false;
            dgvUsedService.AllowUserToDeleteRows = false;

            // Payment methods
            try
            {
                var methods = _svc.getPaymentMethods();
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
            txtDiscount.TextChanged += (s, e) => RecalcTotals();
            txtSurcharge.TextChanged += (s, e) => RecalcTotals();
            txtDiscount.KeyPress += NumericOnly_KeyPress;
            txtSurcharge.KeyPress += NumericOnly_KeyPress;
            txtPayNow.KeyPress += NumericOnly_KeyPress;

            // Buttons
            btnConfirm.Text = "Thanh toán";
            btnCancel.Text = "Hủy";
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += (s, e) => Close();
        }

        private void EnsurePaymentHistoryUi()
        {
            // Tạo group + grid
            var grp = new GroupBox { Text = "Lịch sử thanh toán", Dock = DockStyle.Fill };
            dgvPayments = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            grp.Controls.Add(dgvPayments);

            // summaryLayout trong Designer hiện có 7 rows (6 absolute 36f + 1 percent, buttonsPanel ở hàng cuối)
            // Chèn group vào ngay TRƯỚC buttonsPanel
            // 1) Lấy row của buttonsPanel
            int buttonsRow = summaryLayout.GetRow(buttonsPanel);

            // 2) Tăng RowCount + thêm RowStyle
            summaryLayout.RowCount += 1;
            summaryLayout.RowStyles.Insert(buttonsRow, new RowStyle(SizeType.Percent, 100F));

            // 3) Di chuyển buttonsPanel xuống 1 hàng, add grp vào vị trí cũ
            summaryLayout.Controls.Remove(buttonsPanel);
            summaryLayout.Controls.Add(grp, 0, buttonsRow);
            summaryLayout.SetColumnSpan(grp, 4);

            summaryLayout.Controls.Add(buttonsPanel, 0, buttonsRow + 1);
            summaryLayout.SetColumnSpan(buttonsPanel, 4);
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
        #endregion

        #region Load & bind
        private void LoadDataAndBind()
        {
            // 1) Lấy toàn bộ dòng phòng thuộc header
            _allRooms = _svc.GetBookingsByHeaderId(_headerBookingId) ?? new List<Booking>();

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

            // 3) Bảng phòng chi tiết
            var rows = new List<dynamic>();
            _roomTotal = 0; _serviceTotal = 0;

            foreach (var b in _allRooms)
            {
                if (!b.CheckOutDate.HasValue && b.CheckInDate.HasValue && DateTime.Now > b.CheckInDate.Value)
                    b.CheckOutDate = DateTime.Now;

                var roomSub = _svc.GetRoomCharge(b);
                var svcs = _svc.GetUsedServicesByBookingID(b.BookingID) ?? new List<UsedServiceDto>();
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
                .SelectMany(b => _svc.GetUsedServicesByBookingID(b.BookingID) ?? new List<UsedServiceDto>())
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

            // 5) Tổng số
            SetMoney(txtTongtien, _roomTotal);
            SetMoney(txtServiceCharge, _serviceTotal);
            SetMoney(txtRoomTotal2, _roomTotal);
            SetMoney(txtServiceTotal2, _serviceTotal);

            // 6) Tính Subtotal + Đã trả + Còn lại + Lịch sử thanh toán
            RecalcTotals();
            ApplyPayOption();
            LoadPayments();

            // 7) Hiển thị tên nhân viên theo _currentUserId
            try
            {
                var userName = new BookingRepository().GetUserFullNameById(_currentUserId);
                txtEmployeeName.Text = string.IsNullOrWhiteSpace(userName) ? $"User#{_currentUserId}" : userName;
            }
            catch
            {
                txtEmployeeName.Text = $"User#{_currentUserId}";
            }
        }
        #endregion

        #region Tổng & invoice
        private void RecalcTotals()
        {
            var surcharge = ParseMoney(txtSurcharge.Text);
            var discount = ParseMoney(txtDiscount.Text);

            _subtotal = _roomTotal + _serviceTotal + surcharge - discount;
            if (_subtotal < 0) _subtotal = 0;

            try
            {
                var invRepo = new InvoiceRepository();
                _invoiceId = invRepo.UpsertInvoiceTotals(new Invoice
                {
                    BookingID = _headerBookingId,
                    RoomCharge = _roomTotal,
                    ServiceCharge = _serviceTotal,
                    Surcharge = surcharge,
                    Discount = discount,
                    TotalAmount = _subtotal,
                    IssuedAt = DateTime.Now,
                    IssuedBy = _currentUserId,
                    Status = "Unpaid"
                });

                // Đừng dùng biến tạm, luôn đọc đã trả theo HEADER để ổn định
                _daTra = _paySvc.GetPaidAmountByHeader(_headerBookingId);
            }
            catch
            {
                _daTra = 0;
            }

            _conLai = Math.Max(0, _subtotal - _daTra);

            SetMoney(txtTotalAmount, _subtotal);
            SetMoney(txtSubtotal, _subtotal);
            SetMoney(txtDaTra, _daTra);
            SetMoney(txtConLai, _conLai, bold: true);
        }

        private void LoadPayments()
        {
            try
            {
                var list = _paySvc.GetPaymentsByHeader(_headerBookingId) ?? new List<Payment>();
                dgvPayments.DataSource = list.Select(x => new
                {
                    Ngày = x.PaymentDate.ToString("dd/MM/yyyy HH:mm"),
                    HìnhThức = x.Method,
                    SốTiền = x.Amount.ToString("#,0", vi),
                    TrạngThái = x.Status
                }).ToList();
            }
            catch
            {
                // ignore
            }
        }

        private void ApplyPayOption()
        {
            if (cboPayOption.SelectedItem?.ToString() == "Trả đủ")
            {
                txtPayNow.Enabled = false;
                SetMoney(txtPayNow, _conLai);
            }
            else
            {
                txtPayNow.Enabled = true;
                if (ParseMoney(txtPayNow.Text) <= 0) txtPayNow.Text = "0";
            }
        }
        #endregion

        #region Confirm
        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            var method = cbxPaymentMethod.SelectedItem?.ToString() ?? "Cash";
            var payNow = (cboPayOption.SelectedItem?.ToString() == "Trả đủ")
                ? _conLai
                : ParseMoney(txtPayNow.Text);

            if (payNow < 0) payNow = 0;
            if (payNow > _conLai)
            {
                MessageBox.Show("Số tiền thanh toán vượt quá số còn lại.", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var surcharge = ParseMoney(txtSurcharge.Text);
                var discount = ParseMoney(txtDiscount.Text);

                // Ghi thanh toán (service sẽ cập nhật trạng thái invoice, set CheckedOut nếu đủ)
                var invoiceId = _svc.PayForBookingHeader(
                    _selectedBookingRoomIds,
                    discount,
                    surcharge,
                    payNow,
                    method,
                    _currentUserId
                );

                // Sau khi ghi, luôn reload lại số liệu từ DB để tránh sai lệch
                RecalcTotals();   // -> cũng đọc lại đã trả theo header
                LoadPayments();   // reload lịch sử
                var isFull = _conLai <= 0m;

                if (isFull)
                {
                    MessageBox.Show(
                        $"Đã thanh toán đủ.\nTổng: {_subtotal.ToString("#,0", vi)}",
                        "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Bắn event cho ngoài xử lý (reload UC, đổi trạng thái, in…)
                    PaidCompleted?.Invoke(true, invoiceId);
                    RequestPrintInvoice?.Invoke(invoiceId);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        $"Thanh toán thành công {payNow.ToString("#,0", vi)}.\n" +
                        $"Còn lại: {_conLai.ToString("#,0", vi)}",
                        "Đã ghi nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Chưa đủ thì chỉ bắn event (isFull=false) để ngoài tuỳ ý reload
                    PaidCompleted?.Invoke(false, invoiceId);
                    // Không đóng form để có thể tiếp tục thu tiếp nếu muốn
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
