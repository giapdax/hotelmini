using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmCheckout1 : Form
    {
        // ===== Input =====
        private readonly List<int> _bookingRoomIds;  // có thể 1 hoặc nhiều
        private readonly int _currentUserId;

        // ===== Services/Repos =====
        private readonly BookingService _bookingSvc = new BookingService();
        private readonly RoomService _roomSvc = new RoomService();
        private readonly BookingRepository _repo = new BookingRepository();

        // ===== Data tổng hợp =====
        private class Line
        {
            public int BookingRoomID { get; set; }
            public int RoomID { get; set; }
            public string RoomNumber { get; set; }
            public string PricingType { get; set; }
            public DateTime? CheckIn { get; set; }
            public DateTime? CheckOut { get; set; }
            public decimal RoomCharge { get; set; }
            public decimal ServiceTotal { get; set; }
            public decimal SubTotal => RoomCharge + ServiceTotal;
        }

        private readonly List<Line> _lines = new List<Line>();
        private readonly List<UsedServiceDto> _services = new List<UsedServiceDto>();
        private decimal _grandRoom = 0m, _grandService = 0m;

        // ===== UI controls (đã có trong Designer) =====
        //  Gợi ý: form bạn đã có các control sau theo ảnh/sẵn code:
        //  txtCusName, txtCusId, txtCheckin, txtCheckout, txtRoomCharge, txtServiceCharge, txtTotalAmount
        //  txtSurcharge, txtDiscount, txtEmployeeName, txtNote
        //  cbxPaymentMethod, dgvUsedService, dgvRoom, btnConfirm, btnCancel

        // --- Constructors ---
        public frmCheckout1(int bookingRoomId, int currentUserId)
            : this(new List<int> { bookingRoomId }, currentUserId) { }

        public frmCheckout1(List<int> bookingRoomIds, int currentUserId)
        {
            InitializeComponent();
            _bookingRoomIds = bookingRoomIds?.Distinct().ToList() ?? new List<int>();
            _currentUserId = currentUserId;

            this.Load += frmCheckout1_Load;
            btnConfirm.Click += btnConfirm_Click;
            btnCancel.Click += (s, e) => this.Close();
            txtDiscount.TextChanged += RecalcTotal;
            txtSurcharge.TextChanged += RecalcTotal;

            cbxPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // ================= LOAD =================
        private void frmCheckout1_Load(object sender, EventArgs e)
        {
            if (_bookingRoomIds.Count == 0)
            {
                MessageBox.Show("Không có phòng để thanh toán.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close(); return;
            }

            // Nếu các phòng thuộc nhiều header khác nhau -> cảnh báo (nên cùng một Booking header)
            var headerIds = _bookingRoomIds.Select(id => _repo.GetHeaderIdByBookingRoomId(id)).ToList();
            if (headerIds.Any(h => !h.HasValue) || headerIds.Select(h => h.Value).Distinct().Count() > 1)
            {
                MessageBox.Show("Các phòng không thuộc cùng một đơn đặt phòng.", "Sai nhóm đơn",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Khách + NV + phương thức
            var firstBooking = _bookingSvc.GetBookingById(_bookingRoomIds[0]);
            if (firstBooking != null)
            {
                var cust = _repo.GetCustomerBasicById(firstBooking.CustomerID);
                txtCusName.Text = cust?.FullName ?? "";
                txtCusId.Text = cust?.IDNumber ?? "";
            }
            txtEmployeeName.Text = _repo.GetUserFullNameById(_currentUserId) ?? "";
            cbxPaymentMethod.DataSource = _bookingSvc.getPaymentMethods();

            // Gom dữ liệu các phòng + dịch vụ
            var serviceRows = new List<dynamic>();

            foreach (var bid in _bookingRoomIds)
            {
                var b = _bookingSvc.GetBookingById(bid);
                if (b == null) continue;

                if (!b.CheckOutDate.HasValue || (b.CheckInDate.HasValue && b.CheckOutDate <= b.CheckInDate))
                    b.CheckOutDate = DateTime.Now;

                var roomNo = _repo.GetRoomNumberById(b.RoomID);
                var pricingType = _roomSvc.getPricingTypeByID(b.PricingID);
                var roomCharge = _bookingSvc.GetRoomCharge(b);

                var svcs = _bookingSvc.GetUsedServicesByBookingID(bid) ?? new List<UsedServiceDto>();
                var svcTotal = svcs.Sum(x => x.Price * x.Quantity);

                _lines.Add(new Line
                {
                    BookingRoomID = bid,
                    RoomID = b.RoomID,
                    RoomNumber = roomNo,
                    PricingType = pricingType,
                    CheckIn = b.CheckInDate,
                    CheckOut = b.CheckOutDate,
                    RoomCharge = roomCharge,
                    ServiceTotal = svcTotal
                });

                foreach (var s in svcs)
                {
                    _services.Add(s);
                    serviceRows.Add(new
                    {
                        RoomNumber = roomNo,
                        s.ServiceName,
                        s.Price,
                        s.Quantity,
                        Total = s.Price * s.Quantity
                    });
                }
            }

            // Tính tổng
            _grandRoom = _lines.Sum(x => x.RoomCharge);
            _grandService = _lines.Sum(x => x.ServiceTotal);

            // Bind lưới phòng
            BindRoomsGrid();

            // Bind lưới dịch vụ
            dgvUsedService.AutoGenerateColumns = false;
            dgvUsedService.Columns.Clear();
            var money = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomNumber", HeaderText = "Phòng", Width = 80 });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServiceName", HeaderText = "Dịch vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Đơn giá", Width = 90, DefaultCellStyle = money });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "SL", Width = 50 });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Total", HeaderText = "Thành tiền", Width = 110, DefaultCellStyle = money });
            dgvUsedService.DataSource = serviceRows;

            // Hiển thị phần “đầu tiên” (giữ tương thích với UI cũ)
            txtCheckin.Text = _lines.FirstOrDefault()?.CheckIn?.ToString("dd/MM/yyyy HH:mm");
            txtCheckout.Text = _lines.FirstOrDefault()?.CheckOut?.ToString("dd/MM/yyyy HH:mm");
            txtRoomCharge.Text = _grandRoom.ToString("N0");
            txtServiceCharge.Text = _grandService.ToString("N0");

            txtSurcharge.Text = "0";
            txtDiscount.Text = "0";
            RecalcTotal(null, null);
        }

        private void BindRoomsGrid()
        {
            dgvRoom.AutoGenerateColumns = false;
            dgvRoom.Columns.Clear();
            var money = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            var dtFmt = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" };
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Line.RoomNumber), HeaderText = "Phòng", Width = 80 });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Line.PricingType), HeaderText = "Giá theo", Width = 90 });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Line.CheckIn), HeaderText = "Check-in", Width = 130, DefaultCellStyle = dtFmt });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Line.CheckOut), HeaderText = "Check-out", Width = 130, DefaultCellStyle = dtFmt });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Line.RoomCharge), HeaderText = "Tiền phòng", Width = 110, DefaultCellStyle = money });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Line.ServiceTotal), HeaderText = "Tổng DV", Width = 110, DefaultCellStyle = money });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(Line.SubTotal), HeaderText = "Tạm tính", Width = 120, DefaultCellStyle = money });
            dgvRoom.DataSource = _lines;
        }

        // ================= TÍNH TỔNG =================
        private void RecalcTotal(object sender, EventArgs e)
        {
            decimal.TryParse((txtSurcharge.Text ?? "0").Replace(",", ""), out var surcharge);
            decimal.TryParse((txtDiscount.Text ?? "0").Replace(",", ""), out var discount);
            var total = _grandRoom + _grandService + surcharge - discount;
            if (total < 0) total = 0;
            txtTotalAmount.Text = total.ToString("N0");
        }

        // ================= CONFIRM =================
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cbxPaymentMethod.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn hình thức thanh toán.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal.TryParse((txtSurcharge.Text ?? "0").Replace(",", ""), out var surcharge);
            decimal.TryParse((txtDiscount.Text ?? "0").Replace(",", ""), out var discount);
            var paymentMethod = cbxPaymentMethod.SelectedItem.ToString();

            try
            {
                // Cho phép sửa giờ checkout của DÒNG ĐẦU – các dòng khác vẫn giữ nguyên
                if (DateTime.TryParse(txtCheckout.Text, out var co) && _lines.Count > 0)
                {
                    var b0 = _bookingSvc.GetBookingById(_lines[0].BookingRoomID);
                    if (b0 != null) b0.CheckOutDate = co;
                }

                if (_lines.Count == 1)
                {
                    var ln = _lines[0];
                    var b = _bookingSvc.GetBookingById(ln.BookingRoomID);
                    if (!b.CheckOutDate.HasValue || (b.CheckInDate.HasValue && b.CheckOutDate <= b.CheckInDate))
                        b.CheckOutDate = DateTime.Now;

                    _bookingSvc.Checkout(b, ln.RoomCharge, ln.ServiceTotal, discount, surcharge, paymentMethod, _currentUserId);
                }
                else
                {
                    // Chia phụ phí/giảm giá theo tỉ lệ tạm tính
                    var weights = _lines.Select(x => x.SubTotal <= 0 ? 0.0001m : x.SubTotal).ToList();
                    var surShares = SplitProportionally(surcharge, weights);
                    var disShares = SplitProportionally(discount, weights);

                    for (int i = 0; i < _lines.Count; i++)
                    {
                        var ln = _lines[i];
                        var b = _bookingSvc.GetBookingById(ln.BookingRoomID);
                        if (b == null) continue;
                        if (!b.CheckOutDate.HasValue || (b.CheckInDate.HasValue && b.CheckOutDate <= b.CheckInDate))
                            b.CheckOutDate = DateTime.Now;

                        _bookingSvc.Checkout(b, ln.RoomCharge, ln.ServiceTotal,
                                             disShares[i], surShares[i], paymentMethod, _currentUserId);
                    }
                }

                // (Tuỳ chọn) hiện 1 hoá đơn tổng hợp – nếu bạn muốn 1 bill duy nhất thì xem phần NOTE ở dưới.

                MessageBox.Show("Thanh toán thành công.", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thanh toán thất bại: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static List<decimal> SplitProportionally(decimal total, List<decimal> weights)
        {
            var n = weights.Count;
            var shares = new List<decimal>(new decimal[n]);
            if (n == 0 || total == 0) return shares;

            var sum = weights.Sum();
            if (sum == 0)
            {
                var even = Math.Round(total / n, 0, MidpointRounding.AwayFromZero);
                for (int i = 0; i < n; i++) shares[i] = even;
            }
            else
            {
                decimal acc = 0;
                for (int i = 0; i < n; i++)
                {
                    var raw = total * (weights[i] / sum);
                    shares[i] = Math.Round(raw, 0, MidpointRounding.AwayFromZero);
                    acc += shares[i];
                }
                var diff = total - acc;
                if (diff != 0 && n > 0) shares[0] += diff;
            }
            return shares;
        }
    }
}
