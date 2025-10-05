// frmCheckout.cs
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
    public partial class frmCheckout : Form
    {
        public event Action<bool, int> PaidCompleted;
        public event Action<int> RequestPrintInvoice;

        private readonly BookingService _bookingSvc = new BookingService();
        private readonly InvoiceRepository _invRepo = new InvoiceRepository();
        private readonly PaymentRepository _payRepo = new PaymentRepository();
        private readonly PaymentService _paySvc = new PaymentService();

        private readonly int _headerBookingId;
        private readonly List<int> _selectedBookingRoomIds;
        private readonly int _currentUserId;

        private List<Booking> _allRooms;
        private decimal _roomTotal, _serviceTotal, _subtotal, _daTra, _conLai;
        private int _invoiceId;

        private readonly CultureInfo vi = new CultureInfo("vi-VN");

        private static int ResolveHeaderId(List<int> bookingIds)
        {
            if (bookingIds == null || bookingIds.Count == 0) throw new ArgumentException("Danh sách BookingID trống.");
            return bookingIds[0];
        }

        public frmCheckout(List<int> bookingIds, int currentUserId)
            : this(ResolveHeaderId(bookingIds), bookingIds, currentUserId) { }

        public frmCheckout(int headerBookingId, List<int> selectedBookingRoomIds, int currentUserId)
        {
            InitializeComponent();
            _headerBookingId = headerBookingId;
            _selectedBookingRoomIds = selectedBookingRoomIds ?? new List<int>();
            _currentUserId = currentUserId;
            BuildUiBehavior();
            LoadDataAndBind();
        }

        private void BuildUiBehavior()
        {
            dgvRoom.ReadOnly = true;
            dgvRoom.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoom.RowHeadersVisible = false;
            dgvRoom.AllowUserToAddRows = false;
            dgvRoom.AllowUserToDeleteRows = false;

            dgvUsedService.ReadOnly = true;
            dgvUsedService.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsedService.RowHeadersVisible = false;
            dgvUsedService.AllowUserToAddRows = false;
            dgvUsedService.AllowUserToDeleteRows = false;

            dgvPayments.ReadOnly = true;
            dgvPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPayments.RowHeadersVisible = false;
            dgvPayments.AllowUserToAddRows = false;
            dgvPayments.AllowUserToDeleteRows = false;
            dgvPayments.Dock = DockStyle.Fill;

            try
            {
                var methods = _bookingSvc.getPaymentMethods();
                if (methods != null && methods.Count > 0) cbxPaymentMethod.DataSource = methods;
                else cbxPaymentMethod.Items.AddRange(new object[] { "Cash", "Card", "Transfer" });
            }
            catch
            {
                cbxPaymentMethod.Items.AddRange(new object[] { "Cash", "Card", "Transfer" });
            }

            cboPayOption.Items.Clear();
            cboPayOption.Items.AddRange(new object[] { "Trả đủ", "Trả một phần" });
            cboPayOption.SelectedIndex = 0;
            cboPayOption.SelectedIndexChanged += (s, e) => ApplyPayOption();

            txtDiscount.Text = "0";
            txtSurcharge.Text = "0";
            txtPayNow.Text = "0";

            txtDiscount.TextChanged += (s, e) => RecalcTotals();
            txtSurcharge.TextChanged += (s, e) => RecalcTotals();
            txtDiscount.KeyPress += NumericOnly_KeyPress;
            txtSurcharge.KeyPress += NumericOnly_KeyPress;
            txtPayNow.KeyPress += NumericOnly_KeyPress;

            btnConfirm.Text = "Thanh toán";
            btnCancel.Text = "Đóng";
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += (s, e) => Close();

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

        private int ResolveBookingIdForInvoice()
        {
            // 1) Nếu đã có danh sách phòng theo header, lấy BookingID đầu tiên
            var b = _allRooms?.FirstOrDefault();
            if (b != null && b.BookingID > 0) return b.BookingID;

            // 2) Nếu người gọi lỡ truyền BookingRoomID → map sang BookingID
            if (_selectedBookingRoomIds != null && _selectedBookingRoomIds.Count > 0)
            {
                var bookingId = new BookingRepository().GetBookingIdByBookingRoomId(_selectedBookingRoomIds[0]);
                if (bookingId > 0) return bookingId;
            }

            // 3) Cuối cùng: thử dùng thẳng _headerBookingId như BookingID
            return _headerBookingId;
        }

        private void LoadDataAndBind()
        {
            _allRooms = _bookingSvc.GetBookingsByHeaderId(_headerBookingId) ?? new List<Booking>();
            if (_allRooms.Count == 0)
            {
                var single = _bookingSvc.GetBookingById(_headerBookingId);
                if (single != null) _allRooms.Add(single);
            }

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

            SetMoney(txtServiceCharge, _serviceTotal);
            SetMoney(txtTongtien, _roomTotal);
            SetMoney(txtRoomTotal2, _roomTotal);
            SetMoney(txtServiceTotal2, _serviceTotal);

            RecalcTotals();
            LoadPayments();
            ApplyPayOption();

            try
            {
                var userName = new BookingRepository().GetUserFullNameById(_currentUserId);
                txtEmployeeName.Text = string.IsNullOrWhiteSpace(userName) ? $"User#{_currentUserId}" : userName;
            }
            catch { txtEmployeeName.Text = $"User#{_currentUserId}"; }
        }

        private void RecalcTotals()
        {
            var surcharge = ParseMoney(txtSurcharge.Text);
            var discount = ParseMoney(txtDiscount.Text);

            _subtotal = _roomTotal + _serviceTotal + surcharge - discount;
            if (_subtotal < 0) _subtotal = 0;

            var bookingIdForInvoice = ResolveBookingIdForInvoice();
            if (bookingIdForInvoice <= 0)
                throw new Exception("Không tìm thấy BookingID hợp lệ để tạo/đọc hóa đơn.");

            // Tạo/lấy hóa đơn theo BookingID CHUẨN
            _invoiceId = _invRepo.CreateOrGetOpenInvoice(
                bookingId: bookingIdForInvoice,
                roomCharge: _roomTotal,
                serviceCharge: _serviceTotal,
                discount: discount,
                surcharge: surcharge,
                issuedByUserIfPaid: 0
            );

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
                var list = _invRepo.GetPaymentsByInvoiceId(_invoiceId) ?? new List<Payment>();
                dgvPayments.DataSource = list.Select(x => new
                {
                    Ngày = x.PaymentDate.ToString("dd/MM/yyyy HH:mm"),
                    HìnhThức = x.Method,
                    SốTiền = x.Amount.ToString("#,0", vi),
                    TrạngThái = x.Status
                }).ToList();
            }
            catch { }
        }

        private void ApplyPayOption()
        {
            var full = cboPayOption.SelectedItem?.ToString() == "Trả đủ";
            txtPayNow.Enabled = !full;
            if (full) SetMoney(txtPayNow, _conLai);
            else if (ParseMoney(txtPayNow.Text) <= 0) txtPayNow.Text = "0";
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            var method = cbxPaymentMethod.SelectedItem?.ToString() ?? "Cash";
            var isFullOption = (cboPayOption.SelectedItem?.ToString() == "Trả đủ");

            var payNow = isFullOption ? _conLai : ParseMoney(txtPayNow.Text);
            if (payNow < 0) payNow = 0;

            if (payNow == 0 && _conLai > 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền > 0.", "Thiếu số tiền", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (payNow > _conLai + 0.0001m)
            {
                MessageBox.Show("Số tiền thanh toán vượt quá số còn lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmMsg = isFullOption ? "Bạn có xác nhận thanh toán cho đơn đặt phòng này không?" : "Bạn có xác nhận thanh toán trước 1 phần cho đơn đặt phòng này không?";
            if (!AskConfirm(confirmMsg)) return;

            try
            {
                _payRepo.AddPaymentSafe(_invoiceId, payNow, method, "Completed", _currentUserId);

                RecalcTotals();
                LoadPayments();

                var isFullAfter = _conLai <= 0m;
                if (isFullAfter)
                {
                    MessageBox.Show($"Thanh toán thành công.\nTổng: {_subtotal.ToString("#,0", vi)}", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PaidCompleted?.Invoke(true, _invoiceId);
                    RequestPrintInvoice?.Invoke(_invoiceId);

                    try { OpenInvoiceForCurrentHeader(); }
                    catch (Exception exShow)
                    {
                        MessageBox.Show("Không mở được hóa đơn: " + exShow.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                MessageBox.Show($"Thanh toán thành công {payNow.ToString("#,0", vi)}.\nCòn lại: {_conLai.ToString("#,0", vi)}", "Đã ghi nhận", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PaidCompleted?.Invoke(false, _invoiceId);
                ApplyPayOption();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AskConfirm(string message)
        {
            var rs = MessageBox.Show(message, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return rs == DialogResult.Yes;
        }

        private void OpenInvoiceForCurrentHeader()
        {
            var bookingIdForInvoice = ResolveBookingIdForInvoice();
            var invoice = _invRepo.GetInvoiceByBookingID(bookingIdForInvoice);
            if (invoice == null) throw new Exception("Không tìm thấy hóa đơn.");

            var roomRows = new List<frmInvoice.RoomRow>();
            foreach (var b in _allRooms)
            {
                string roomNumber = "";
                try { roomNumber = new BookingRepository().GetRoomNumberById(b.RoomID); } catch { }
                string pricingType = "";
                try { pricingType = new RoomPricingRepository().GetPricingTypeById(b.PricingID)?.PricingType ?? ""; } catch { }

                roomRows.Add(new frmInvoice.RoomRow
                {
                    RoomNumber = roomNumber,
                    PricingType = pricingType,
                    CheckIn = b.CheckInDate?.ToString("dd/MM/yyyy HH:mm") ?? "",
                    CheckOut = b.CheckOutDate?.ToString("dd/MM/yyyy HH:mm") ?? ""
                });
            }

            var usedAll = _allRooms
                .SelectMany(b => _bookingSvc.GetUsedServicesByBookingID(b.BookingID) ?? new List<UsedServiceDto>())
                .GroupBy(x => new { x.ServiceName, x.Price })
                .Select(g => new frmInvoice.InvoiceServiceRow
                {
                    ServiceName = g.Key.ServiceName,
                    Price = g.Key.Price,
                    Quantity = g.Sum(x => x.Quantity)
                })
                .OrderBy(x => x.ServiceName)
                .ToList();

            var payments = _invRepo.GetPaymentsByInvoiceId(invoice.InvoiceID) ?? new List<Payment>();

            string employeeName = "—";
            try
            {
                var full = new InvoiceRepository().getFullNameByInvoiceID(invoice.InvoiceID);
                employeeName = string.IsNullOrWhiteSpace(full) ? "—" : full;
            }
            catch { }

            var minIn = _allRooms.Where(x => x.CheckInDate.HasValue).Select(x => x.CheckInDate.Value).DefaultIfEmpty(DateTime.Now).Min();
            var maxOut = _allRooms.Where(x => x.CheckOutDate.HasValue).Select(x => x.CheckOutDate.Value).DefaultIfEmpty(DateTime.Now).Max();

            var vm = new frmInvoice.InvoiceVm
            {
                CustomerName = txtCusName.Text,
                CustomerIdNumber = txtCusId.Text,
                CheckIn = minIn,
                CheckOut = maxOut,
                RoomCharge = _roomTotal,
                ServiceCharge = _serviceTotal,
                Discount = ParseMoney(txtDiscount.Text),
                Surcharge = ParseMoney(txtSurcharge.Text),
                Total = invoice.TotalAmount > 0 ? invoice.TotalAmount : _subtotal,
                EmployeeName = employeeName,
                PaymentMethod = payments.LastOrDefault()?.Method ?? "—",
                Note = ""
            };

            var f = new frmInvoice
            {
                StartPosition = FormStartPosition.CenterParent,
                Text = "Hóa đơn thanh toán"
            };
            f.BindFrom(vm, usedAll, roomRows, payments);
            f.ShowDialog(this);
        }
    }
}
