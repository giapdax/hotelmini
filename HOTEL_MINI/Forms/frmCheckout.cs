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
        private readonly BLL.BookingRoomService _brSvc = new BLL.BookingRoomService();
        private readonly RoomPricingService _pricingSvc = new RoomPricingService();
        private readonly InvoiceService _invSvc = new InvoiceService();
        private readonly PaymentService _payService = new PaymentService();

        private readonly int _headerBookingId;
        private readonly List<int> _selectedBookingRoomIds;
        private readonly int _currentUserId;

        private List<BookingRoom> _allRooms = new List<BookingRoom>();

        private decimal _roomTotal, _serviceTotal, _subtotal, _daTra, _conLai;
        private int _invoiceId;

        private readonly CultureInfo vi = new CultureInfo("vi-VN");

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

            txtDiscount.Text = "0";
            txtSurcharge.Text = "0";
            txtDiscount.KeyPress += NumericOnly_KeyPress;
            txtSurcharge.KeyPress += NumericOnly_KeyPress;
            txtDiscount.TextChanged += (s, e) => RecalcTotals();
            txtSurcharge.TextChanged += (s, e) => RecalcTotals();

            btnConfirm.Text = "Thanh toán";
            btnCancel.Text = "Đóng";
            btnConfirm.Click += BtnConfirm_Click;
            btnCancel.Click += (s, e) => Close();
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

        private void LoadDataAndBind()
        {
            _allRooms = _selectedBookingRoomIds?.Distinct()
                .Select(id => _bookingSvc.GetBookingById(id))
                .Where(x => x != null)
                .ToList() ?? new List<BookingRoom>();

            if (_allRooms.Count == 0)
                throw new InvalidOperationException("Không có phòng nào được chọn.");

            foreach (var line in _allRooms)
            {
                var h = _bookingSvc.GetHeaderIdByBookingRoomId(line.BookingRoomID);
                if (!h.HasValue || h.Value != _headerBookingId)
                    throw new InvalidOperationException("Phòng được chọn không thuộc cùng một Booking.");
            }

            try
            {
                var cust = _bookingSvc.GetCustomerByHeaderId(_headerBookingId);
                txtCusName.Text = cust?.FullName ?? "";
                txtCusId.Text = cust?.IDNumber ?? "";
            }
            catch { }

            var minIn = _allRooms.Where(x => x.CheckInDate.HasValue).Select(x => x.CheckInDate.Value).DefaultIfEmpty(DateTime.Now).Min();
            var maxOut = _allRooms.Where(x => x.CheckOutDate.HasValue).Select(x => x.CheckOutDate.Value).DefaultIfEmpty(DateTime.Now).Max();

            var tbIn = Controls.Find("txtCheckin", true).FirstOrDefault() as TextBox;
            var tbOut = Controls.Find("txtCheckout", true).FirstOrDefault() as TextBox;
            if (tbIn != null) tbIn.Text = minIn.ToString("dd/MM/yyyy HH:mm");
            if (tbOut != null) tbOut.Text = maxOut.ToString("dd/MM/yyyy HH:mm");

            var rows = new List<dynamic>();
            _roomTotal = 0m; _serviceTotal = 0m;

            foreach (var line in _allRooms)
            {
                if (!line.CheckOutDate.HasValue && line.CheckInDate.HasValue && DateTime.Now > line.CheckInDate.Value)
                    line.CheckOutDate = DateTime.Now;

                var roomSub = _bookingSvc.GetRoomCharge(line);
                var used = _bookingSvc.GetUsedServicesByBookingID(line.BookingRoomID) ?? new List<UsedServiceDto>();
                var svcTotal = used.Sum(x => x.Price * x.Quantity);

                string roomNumber = "";
                try { roomNumber = _brSvc.GetRoomNumberById(line.RoomID); } catch { }

                string pricingType = ""; decimal unitPrice = 0m;
                try
                {
                    var pr = _pricingSvc.GetById(line.PricingID);
                    if (pr != null) { pricingType = pr.PricingType; unitPrice = pr.Price; }
                }
                catch { }

                _roomTotal += roomSub;
                _serviceTotal += svcTotal;

                rows.Add(new
                {
                    Phòng = roomNumber,
                    CheckIn = line.CheckInDate?.ToString("dd/MM HH:mm"),
                    CheckOut = line.CheckOutDate?.ToString("dd/MM HH:mm"),
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
                .SelectMany(b => _bookingSvc.GetUsedServicesByBookingID(b.BookingRoomID) ?? new List<UsedServiceDto>())
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

            _invoiceId = _invSvc.CreateOrGetOpenInvoice(
                bookingId: _headerBookingId,
                roomCharge: _roomTotal,
                serviceCharge: _serviceTotal,
                discount: discount,
                surcharge: surcharge,
                issuedByUserIfPaid: 0
            );

            var t = _invSvc.GetInvoiceTotals(_invoiceId);
            _daTra = t.Paid;
            _conLai = Math.Max(0m, t.Total - t.Paid);

            SetMoney(txtSubtotal, _subtotal);
            SetMoney(txtTotalAmount, _subtotal);

            btnConfirm.Enabled = (_daTra <= 0m);
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (_daTra > 0m)
            {
                MessageBox.Show("Hóa đơn đã có thanh toán trước. Hệ thống chỉ cho phép thanh toán 1 lần.", "Không thể thanh toán", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_conLai <= 0m)
            {
                MessageBox.Show("Không còn số tiền cần thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var method = cbxPaymentMethod.SelectedItem?.ToString() ?? "Cash";
            var confirmMsg = $"Xác nhận thanh toán toàn bộ: {_conLai.ToString("#,0", vi)} ?";
            if (MessageBox.Show(confirmMsg, "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _payService.AddPaymentSafe(
                    invoiceId: _invoiceId,
                    amount: _conLai,
                    method: method,
                    status: "Paid",
                    issuedByIfPaid: _currentUserId,
                    bookingRoomIdsToCheckout: _selectedBookingRoomIds
                );

                MessageBox.Show($"Thanh toán thành công.\nTổng: {_subtotal.ToString("#,0", vi)}", "Hoàn tất", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PaidCompleted?.Invoke(true, _invoiceId);
                RequestPrintInvoice?.Invoke(_invoiceId);

                if (MessageBox.Show("Bạn có muốn xuất hóa đơn ra file PDF không?", "Xuất hóa đơn", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (var sfd = new SaveFileDialog
                    {
                        Filter = "PDF Files (*.pdf)|*.pdf",
                        FileName = $"Invoice_{_invoiceId}_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
                    })
                    {
                        if (sfd.ShowDialog(this) == DialogResult.OK)
                        {
                            try
                            {
                                var invoice = _invSvc.GetInvoice(_invoiceId);
                                if (invoice == null)
                                {
                                    MessageBox.Show("Không tìm thấy thông tin hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                                var roomLines = new List<(BookingRoom Room, string RoomNumber, string PricingType, decimal UnitPrice, int Quantity)>();

                                foreach (var line in _allRooms)
                                {
                                    string roomNumber = "";
                                    try { roomNumber = _brSvc.GetRoomNumberById(line.RoomID); } catch { }

                                    string pricingType = ""; decimal unitPrice = 0m;
                                    try
                                    {
                                        var pr = new RoomPricingRepository().GetPricingTypeById(line.PricingID);
                                        if (pr != null) { pricingType = pr.PricingType ?? ""; unitPrice = pr.Price; }
                                    }
                                    catch { }

                                    decimal roomCharge = 0m;
                                    try { roomCharge = _bookingSvc.GetRoomCharge(line); } catch { }

                                    int qty = 1;
                                    if (unitPrice > 0m)
                                    {
                                        var q = Math.Round(roomCharge / unitPrice, MidpointRounding.AwayFromZero);
                                        if (q >= 1 && q <= int.MaxValue) qty = (int)q;
                                        else { unitPrice = roomCharge; qty = 1; }
                                    }
                                    else
                                    {
                                        unitPrice = roomCharge; // fallback: 1 dòng = tổng tiền phòng
                                        qty = 1;
                                    }

                                    roomLines.Add((line, roomNumber, pricingType, unitPrice, qty));
                                }

                                // gộp toàn bộ dịch vụ của các phòng đã chọn
                                var usedServices = _allRooms
                                    .SelectMany(b => _bookingSvc.GetUsedServicesByBookingID(b.BookingRoomID) ?? new List<UsedServiceDto>())
                                    .GroupBy(x => new { x.ServiceName, x.Price })
                                    .Select(g => new UsedServiceDto
                                    {
                                        ServiceName = g.Key.ServiceName,
                                        Price = g.Key.Price,
                                        Quantity = g.Sum(x => x.Quantity)
                                    })
                                    .ToList();

                                var pdf = new PdfExportService(txtCusName.Text, txtCusId.Text);
                                pdf.ExportInvoiceToPdf(invoice, roomLines, usedServices, sfd.FileName);

                                MessageBox.Show("Xuất hóa đơn PDF thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi khi xuất hóa đơn PDF:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}