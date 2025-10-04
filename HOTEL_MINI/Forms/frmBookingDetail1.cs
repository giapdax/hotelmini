using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using MiniHotel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmBookingDetail1 : Form
    {
        // ======= Repos/Services =======
        private readonly ServicesRepository _servicesRepo = new ServicesRepository();
        private readonly RoomPricingRepository _pricingRepo = new RoomPricingRepository();
        private readonly RoomRepository _roomRepo = new RoomRepository();
        private readonly BookingService _bookingSvc = new BookingService();
        private readonly CustomerService _customerSvc = new CustomerService();

        // ======= Dữ liệu cho chế độ ĐẶT PHÒNG (book) =======
        private readonly List<SelectedRoomWithTime> _rooms;
        private readonly Dictionary<int, RoomPlan> _plans;
        private readonly Dictionary<int, List<RoomPricing>> _pricingCache = new Dictionary<int, List<RoomPricing>>();

        // ======= Dữ liệu cho chế độ CHECKOUT =======
        private bool _checkoutMode;
        private List<int> _bookingRoomIdsForCheckout = new List<int>();
        private BindingList<CheckoutRow> _checkoutRows;

        // ======= DataTable dịch vụ dùng chung =======
        private DataTable _usedServicesTable;

        // ======= Cột hiển thị (book mode) =======
        private const string COL_ROOM_PICK = "colRoomPick";
        private const string COL_ROOM_TYPE = "colPlanType";
        private const string COL_ROOM_UNIT = "colUnit";
        private const string COL_ROOM_BASE = "colBaseTotal";
        private const string COL_ROOM_SVC = "colServiceTotal";
        private const string COL_ROOM_GRAND = "colGrandTotal";

        // ======= Thông tin KH & user =======
        private int _customerId;
        private string _customerIdNumber;
        private readonly int _currentUserId;

        // ===================== Constructors =====================

        // ĐẶT PHÒNG (book)
        public frmBookingDetail1(List<SelectedRoomWithTime> rooms,
                                 Dictionary<int, RoomPlan> plans,
                                 int customerId,
                                 string customerIdNumber,
                                 int currentUserId)
        {
            InitializeComponent();

            _rooms = rooms ?? new List<SelectedRoomWithTime>();
            _plans = plans ?? new Dictionary<int, RoomPlan>();
            _customerId = customerId;
            _customerIdNumber = customerIdNumber ?? string.Empty;
            _currentUserId = currentUserId;

            this.Load += FrmBookingDetail1_Load;

            btnClose.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            btnDatphong.Click += BtnDatphong_Click;   // sẽ đổi thành "Trả phòng" ở checkout mode
            btnIncrease.Click += BtnIncrease_Click;
            btnReduce.Click += BtnReduce_Click;
            btnCheck.Click += BtnCheck_Click;

            nbrIncrease.Minimum = 1; nbrIncrease.Maximum = 100; nbrIncrease.Value = 1;
            nbrReduce.Minimum = 1; nbrReduce.Maximum = 100; nbrReduce.Value = 1;
        }

        // Overload dành cho CHECKOUT: mở theo list BookingRoomID
        public frmBookingDetail1(List<int> bookingRoomIds, int currentUserId)
            : this(new List<SelectedRoomWithTime>(), new Dictionary<int, RoomPlan>(), 0, string.Empty, currentUserId)
        {
            _checkoutMode = true;
            _bookingRoomIdsForCheckout = bookingRoomIds ?? new List<int>();
        }

        // Overload cũ (để không lỗi code khác đang gọi)
        public frmBookingDetail1(List<SelectedRoomWithTime> rooms, Dictionary<int, RoomPlan> plans)
            : this(rooms, plans, 0, string.Empty, 0) { }

        // ===================== Load =====================
        private void FrmBookingDetail1_Load(object sender, EventArgs e)
        {
            if (_checkoutMode)
            {
                try
                {
                    LoadForCheckoutByRoomIds(_bookingRoomIdsForCheckout);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load checkout: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                return;
            }

            // ----- ĐẶT PHÒNG (BOOK) -----
            if (!string.IsNullOrWhiteSpace(_customerIdNumber))
                txtCCCD.Text = _customerIdNumber;

            if (_rooms.Count > 0)
                lblBookingID.Text = $"Đặt {_rooms.Count} phòng: {string.Join(", ", _rooms.Select(r => r.RoomNumber))}";

            SetupRoomsGrid_BookMode();
            LoadRoomsGrid_BookMode();

            SetupServicesMenu();
            SetupUsedServicesTable();
            BindUsedServicesGrid();

            RecalcRoomBaseTotals();
            RecalcRoomServiceTotals();
            UpdateGrandTotals();
        }

        // =========================================================
        // =============== CHECKOUT MODE IMPLEMENTATION ============
        // =========================================================

        private class CheckoutRow
        {
            public bool Pick { get; set; } = true;
            public int BookingRoomID { get; set; }
            public int RoomID { get; set; }
            public string RoomNumber { get; set; }
            public DateTime? CheckIn { get; set; }
            public DateTime? CheckOut { get; set; }
            public string PricingType { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal RoomCharge { get; set; }
            public decimal ServiceTotal { get; set; }
            public decimal GrandTotal { get { return RoomCharge + ServiceTotal; } }
        }

        private void SetupRoomsGrid_Checkout()
        {
            var gv = dataGridView1;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.MultiSelect = false;
            gv.ReadOnly = false;
            gv.RowHeadersVisible = false;
            gv.Columns.Clear();

            var money = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" };
            var dt = new DataGridViewCellStyle { Format = "dd/MM HH:mm" };

            gv.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = nameof(CheckoutRow.Pick), HeaderText = "Chọn", Width = 50, ReadOnly = false });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.RoomNumber), HeaderText = "Phòng", Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.CheckIn), HeaderText = "Check-in", DefaultCellStyle = dt, Width = 110, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.CheckOut), HeaderText = "Check-out", DefaultCellStyle = dt, Width = 110, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.PricingType), HeaderText = "Giá theo", Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.UnitPrice), HeaderText = "Đơn giá", DefaultCellStyle = money, Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.RoomCharge), HeaderText = "Tiền phòng", DefaultCellStyle = money, Width = 110, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.ServiceTotal), HeaderText = "Tổng DV", DefaultCellStyle = money, Width = 110, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.GrandTotal), HeaderText = "Tổng cộng", DefaultCellStyle = money, Width = 120, ReadOnly = true });
        }

        private void LoadForCheckoutByRoomIds(List<int> bookingRoomIds)
        {
            if (bookingRoomIds == null || bookingRoomIds.Count == 0)
                throw new InvalidOperationException("Không có BookingRoomID nào.");

            // ==== KIỂM TRA: Tất cả phải cùng 1 header BookingID ====
            var headerIds = bookingRoomIds
                .Distinct()
                .Select(id => _bookingSvc.GetHeaderIdByBookingRoomId(id))
                .ToList();

            if (headerIds.Any(h => !h.HasValue))
            {
                throw new InvalidOperationException("Có phòng không thuộc đơn đặt phòng hợp lệ.");
            }

            var firstHeader = headerIds.First().Value;
            if (headerIds.Any(h => h.Value != firstHeader))
            {
                MessageBox.Show("Các phòng bạn chọn không cùng đơn đặt phòng, vui lòng chọn lại.",
                    "Sai nhóm đơn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }
            // ================================================

            // Chuẩn bị UI
            SetupServicesMenu();
            SetupUsedServicesTable();
            BindUsedServicesGrid();
            SetupRoomsGrid_Checkout();

            _checkoutRows = new BindingList<CheckoutRow>();
            dataGridView1.DataSource = _checkoutRows;

            // Lấy KH từ booking đầu tiên
            var firstBk = _bookingSvc.GetBookingById(bookingRoomIds[0]);
            if (firstBk != null)
            {
                var cust = new BookingRepository().GetCustomerBasicById(firstBk.CustomerID);
                if (cust != null)
                {
                    _customerId = cust.CustomerID;
                    _customerIdNumber = cust.IDNumber ?? "";
                    txtCCCD.Text = _customerIdNumber;
                    txtTen.Text = cust.FullName ?? "";

                    // Load thêm thông tin KH còn thiếu
                    var cFull = _customerSvc.getCustomerByIDNumber(_customerIdNumber);
                    if (cFull != null)
                    {
                        txtDiachi.Text = cFull.Address ?? "";
                        txtGender.Text = cFull.Gender ?? "";
                        txtEmail.Text = cFull.Email ?? "";
                        txtSDT.Text = cFull.Phone ?? "";
                    }
                }
            }

            // Lặp từng bookingRoomId
            foreach (var bid in bookingRoomIds.Distinct())
            {
                var b = _bookingSvc.GetBookingById(bid);
                if (b == null) continue;

                var roomNo = new BookingRepository().GetRoomNumberById(b.RoomID);
                var pricing = _pricingRepo.GetPricingTypeById(b.PricingID);
                var unit = pricing != null ? pricing.Price : 0m;
                var ptype = pricing != null ? pricing.PricingType : "";

                // đảm bảo có CheckOutDate để tính tiền phòng
                if (!b.CheckOutDate.HasValue || (b.CheckInDate.HasValue && b.CheckOutDate <= b.CheckInDate))
                    b.CheckOutDate = DateTime.Now;

                decimal roomCharge = 0m;
                try { roomCharge = _bookingSvc.GetRoomCharge(b); } catch { }

                var svcs = _bookingSvc.GetUsedServicesByBookingID(b.BookingID);
                decimal svcTotal = svcs.Sum(x => x.Price * x.Quantity);

                _checkoutRows.Add(new CheckoutRow
                {
                    Pick = true,
                    BookingRoomID = b.BookingID,
                    RoomID = b.RoomID,
                    RoomNumber = roomNo,
                    CheckIn = b.CheckInDate,
                    CheckOut = b.CheckOutDate,
                    PricingType = ptype,
                    UnitPrice = unit,
                    RoomCharge = roomCharge,
                    ServiceTotal = svcTotal
                });

                // Đổ dịch vụ vào lưới DV (chỉ hiển thị)
                foreach (var s in svcs)
                {
                    var row = _usedServicesTable.NewRow();
                    row["RoomID"] = b.RoomID;
                    row["RoomNumber"] = roomNo;
                    row["ServiceID"] = s.ServiceID;
                    row["ServiceName"] = s.ServiceName;
                    row["UnitPrice"] = s.Price;
                    row["Quantity"] = s.Quantity;
                    _usedServicesTable.Rows.Add(row);
                }
            }

            txtPricingType.Text = string.Join(", ",
                _checkoutRows.Select(r => r.PricingType).Distinct().Where(x => !string.IsNullOrWhiteSpace(x)));

            lblBookingID.Text = "Trả phòng - " + _checkoutRows.Count + " phòng";

            // Đổi label nút thành Trả phòng và đổi handler:
            btnDatphong.Text = "Trả phòng";
            btnDatphong.Click -= BtnDatphong_Click;
            btnDatphong.Click += BtnCheckout_OpenPayment_Click;

            // Disable nhập KH khi checkout
            btnCheck.Enabled = false;
            txtCCCD.ReadOnly = true;
            txtPricingType.ReadOnly = true;
        }

        /// <summary>
        /// Nút "Trả phòng" trong frmBookingDetail1: 
        /// -> KHÔNG đổi trạng thái tại đây 
        /// -> Chỉ mở frmCheckout1 cho phòng đang chọn để thanh toán.
        /// </summary>
        /// /// <summary>
        /// Ghi các dịch vụ đang hiển thị trong _usedServicesTable xuống DB cho 1 bookingRoomId.
        /// </summary>
        // Thay cho BtnCheckout_OpenPayment_Click cũ
        private void BtnCheckout_OpenPayment_Click(object sender, EventArgs e)
        {
            var picks = (_checkoutRows ?? new BindingList<CheckoutRow>())
                        .Where(x => x.Pick).ToList();
            if (picks.Count == 0)
            {
                MessageBox.Show("Chọn ít nhất 1 phòng để trả.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lưu dịch vụ đang thấy ở lưới sang DB cho từng phòng
            try
            {
                foreach (var p in picks)
                    PersistServicesForBooking(p.BookingRoomID, p.RoomID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không lưu được dịch vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var ids = picks.Select(x => x.BookingRoomID).Distinct().ToList();
            using (var f = new frmCheckout1(ids, _currentUserId)) // ✅ dùng 1 form cho N phòng
            {
                f.StartPosition = FormStartPosition.CenterParent;
                var rs = f.ShowDialog(this);
                if (rs == DialogResult.OK) { this.DialogResult = DialogResult.OK; this.Close(); }
            }
        }

        private void PersistServicesForBooking(int bookingRoomId, int roomId)
        {
            if (_usedServicesTable == null || _usedServicesTable.Rows.Count == 0) return;

            var repo = new BookingRepository();
            var rows = _usedServicesTable.AsEnumerable()
                .Where(r => r.Field<int>("RoomID") == roomId)
                .ToList();

            foreach (var r in rows)
            {
                int serviceId = r.Field<int>("ServiceID");
                int qty = r.Field<int>("Quantity");
                repo.AddOrUpdateServiceForBooking(bookingRoomId, serviceId, qty);
            }
        }



        // =========================================================
        // ================== BOOK MODE IMPLEMENTATION =============
        // =========================================================

        private void SetupRoomsGrid_BookMode()
        {
            var gv = dataGridView1;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.MultiSelect = false;
            gv.ReadOnly = false;
            gv.RowHeadersVisible = false;
            gv.AllowUserToResizeRows = false;
            gv.AllowUserToResizeColumns = false;
            gv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gv.ScrollBars = ScrollBars.Vertical;
            gv.Columns.Clear();

            var moneyStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" };

            gv.Columns.Add(new DataGridViewCheckBoxColumn { HeaderText = "Chọn", Name = COL_ROOM_PICK, ReadOnly = false, FillWeight = 7 });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(SelectedRoomWithTime.RoomNumber), HeaderText = "Phòng", Name = "colRoomNumber", ReadOnly = true, FillWeight = 12 });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(SelectedRoomWithTime.RoomTypeName), HeaderText = "Loại", Name = "colRoomTypeName", ReadOnly = true, FillWeight = 16 });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(SelectedRoomWithTime.CheckIn), HeaderText = "Check-in", Name = "colCheckIn", ReadOnly = true, FillWeight = 17, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM HH:mm" } });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(SelectedRoomWithTime.CheckOut), HeaderText = "Check-out", Name = "colCheckOut", ReadOnly = true, FillWeight = 17, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM HH:mm" } });
            gv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hình thức", Name = COL_ROOM_TYPE, ReadOnly = true, FillWeight = 12 });
            gv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Đơn giá", Name = COL_ROOM_UNIT, ReadOnly = true, DefaultCellStyle = moneyStyle, FillWeight = 9 });
            gv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tiền phòng", Name = COL_ROOM_BASE, ReadOnly = true, DefaultCellStyle = moneyStyle, FillWeight = 10 });
            gv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tổng DV", Name = COL_ROOM_SVC, ReadOnly = true, DefaultCellStyle = moneyStyle, FillWeight = 10 });
            gv.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tổng cộng", Name = COL_ROOM_GRAND, ReadOnly = true, DefaultCellStyle = moneyStyle, FillWeight = 10 });

            gv.CellContentClick += (s, e2) =>
            {
                if (e2.RowIndex < 0) return;
                if (gv.Columns[e2.ColumnIndex].Name != COL_ROOM_PICK) return;
                gv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
        }

        private void LoadRoomsGrid_BookMode()
        {
            dataGridView1.DataSource = new BindingList<SelectedRoomWithTime>(_rooms);
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.Cells[COL_ROOM_PICK].Value = false;

                var item = r.DataBoundItem as SelectedRoomWithTime;
                if (item == null) continue;

                RoomPlan plan;
                if (_plans.TryGetValue(item.RoomID, out plan))
                {
                    r.Cells[COL_ROOM_TYPE].Value = plan.PricingType ?? "";
                    r.Cells[COL_ROOM_UNIT].Value = plan.UnitPrice.HasValue ? (object)plan.UnitPrice.Value : DBNull.Value;
                }
                else
                {
                    r.Cells[COL_ROOM_TYPE].Value = "";
                    r.Cells[COL_ROOM_UNIT].Value = DBNull.Value;
                }
            }
        }

        private List<SelectedRoomWithTime> GetCheckedRooms()
        {
            var list = new List<SelectedRoomWithTime>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool tick = false;
                var val = row.Cells[COL_ROOM_PICK].Value;
                if (val is bool) tick = (bool)val;
                if (tick)
                {
                    var item = row.DataBoundItem as SelectedRoomWithTime;
                    if (item != null) list.Add(item);
                }
            }
            return list;
        }

        private void SetCellMoney(DataGridViewRow row, string colName, decimal value)
        {
            row.Cells[colName].Value = value;
        }

        private List<RoomPricing> GetActivePricingByTypeId(int roomTypeId)
        {
            List<RoomPricing> list;
            if (!_pricingCache.TryGetValue(roomTypeId, out list))
            {
                list = _pricingRepo.GetByRoomType(roomTypeId).Where(p => p.IsActive).ToList();
                _pricingCache[roomTypeId] = list;
            }
            return list;
        }

        private static decimal? FindRate(IEnumerable<RoomPricing> list, string pricingType)
        {
            var p = list.FirstOrDefault(x => x.PricingType != null && x.PricingType.Equals(pricingType, StringComparison.OrdinalIgnoreCase));
            if (p != null) return p.Price;
            return null;
        }

        private static int CeilToInt(double v) { return (int)Math.Ceiling(v); }

        private decimal CalcCostByPlan(SelectedRoomWithTime item)
        {
            RoomPlan plan;
            if (!_plans.TryGetValue(item.RoomID, out plan) || string.IsNullOrWhiteSpace(plan.PricingType))
                return CalcBestCost(item);

            DateTime ci = plan.CheckIn;
            DateTime co = plan.CheckOut;
            if (co <= ci) return 0m;

            decimal unit = plan.UnitPrice.HasValue ? plan.UnitPrice.Value : 0m;

            if (string.Equals(plan.PricingType, "Hourly", StringComparison.OrdinalIgnoreCase))
            {
                int hours = CeilToInt((co - ci).TotalHours);
                return unit * hours;
            }
            if (string.Equals(plan.PricingType, "Daily", StringComparison.OrdinalIgnoreCase))
            {
                int days = CeilToInt((co - ci).TotalDays);
                if (days < 1) days = 1;
                return unit * days;
            }
            if (string.Equals(plan.PricingType, "Weekly", StringComparison.OrdinalIgnoreCase))
            {
                int weeks = CeilToInt((co - ci).TotalDays / 7.0);
                if (weeks < 1) weeks = 1;
                return unit * weeks;
            }
            if (string.Equals(plan.PricingType, "Nightly", StringComparison.OrdinalIgnoreCase))
            {
                return unit;
            }
            return 0m;
        }

        private decimal CalcBestCost(SelectedRoomWithTime item)
        {
            DateTime ci = item.CheckIn;
            DateTime co = item.CheckOut;
            if (co <= ci) return 0m;

            var pricing = GetActivePricingByTypeId(item.RoomTypeID);
            decimal? hourly = FindRate(pricing, "Hourly");
            decimal? daily = FindRate(pricing, "Daily");
            decimal? weekly = FindRate(pricing, "Weekly");
            decimal? nightly = FindRate(pricing, "Nightly");

            var candidates = new List<decimal>();
            double totalHours = (co - ci).TotalHours;

            if (hourly.HasValue) candidates.Add(CeilToInt(totalHours) * hourly.Value);

            if (weekly.HasValue || daily.HasValue || hourly.HasValue)
            {
                decimal cost = 0m;
                double rem = totalHours;

                if (weekly.HasValue)
                {
                    double w = Math.Floor(rem / (7 * 24.0));
                    cost += (decimal)w * weekly.Value;
                    rem -= w * 7 * 24.0;
                }
                if (daily.HasValue)
                {
                    double d = Math.Floor(rem / 24.0);
                    cost += (decimal)d * daily.Value;
                    rem -= d * 24.0;
                }
                if (hourly.HasValue)
                {
                    cost += CeilToInt(rem) * hourly.Value;
                    rem = 0;
                }
                candidates.Add(cost);
            }

            if (nightly.HasValue)
            {
                int nights = (co.Date - ci.Date).Days;
                if (nights < 0) nights = 0;

                decimal nightCost = nights * nightly.Value;
                double leftoverHours = totalHours - nights * 24.0;
                if (leftoverHours < 0) leftoverHours = 0;

                if (hourly.HasValue && leftoverHours > 0)
                    candidates.Add(nightCost + CeilToInt(leftoverHours) * hourly.Value);
                else if (leftoverHours <= 0.0001)
                    candidates.Add(nightCost);
            }

            return candidates.Count == 0 ? 0m : candidates.Min();
        }

        private void RecalcRoomBaseTotals()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var item = row.DataBoundItem as SelectedRoomWithTime;
                if (item == null) continue;

                decimal baseCost = CalcCostByPlan(item);
                SetCellMoney(row, COL_ROOM_BASE, baseCost);

                RoomPlan plan;
                if (_plans.TryGetValue(item.RoomID, out plan))
                {
                    row.Cells[COL_ROOM_TYPE].Value = plan.PricingType ?? "";
                    row.Cells[COL_ROOM_UNIT].Value = plan.UnitPrice.HasValue ? (object)plan.UnitPrice.Value : DBNull.Value;
                }
            }
        }

        // =========================================================
        // ================== SERVICES COMMON PART =================
        // =========================================================

        private void SetupServicesMenu()
        {
            var gv = dgvHotelServices;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.MultiSelect = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ServiceMenuItem.ServiceID), HeaderText = "ID", Width = 50, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ServiceMenuItem.ServiceName), HeaderText = "Dịch vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ServiceMenuItem.Price), HeaderText = "Đơn giá", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });

            var list = _servicesRepo.GetAllServices()
                                    .Where(s => s.IsActive)
                                    .Select(s => new ServiceMenuItem
                                    {
                                        ServiceID = s.ServiceID,
                                        ServiceName = s.ServiceName,
                                        Price = s.Price
                                    })
                                    .ToList();

            gv.DataSource = new BindingList<ServiceMenuItem>(list);
        }

        private void SetupUsedServicesTable()
        {
            _usedServicesTable = new DataTable();
            _usedServicesTable.Columns.Add("RoomID", typeof(int));
            _usedServicesTable.Columns.Add("RoomNumber", typeof(string));
            _usedServicesTable.Columns.Add("ServiceID", typeof(int));
            _usedServicesTable.Columns.Add("ServiceName", typeof(string));
            _usedServicesTable.Columns.Add("UnitPrice", typeof(decimal));
            _usedServicesTable.Columns.Add("Quantity", typeof(int));
            _usedServicesTable.Columns.Add("Total", typeof(decimal), "UnitPrice * Quantity");
        }

        private void BindUsedServicesGrid()
        {
            var gv = dgvUsedServices;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.MultiSelect = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomNumber", HeaderText = "Phòng", Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServiceName", HeaderText = "Dịch vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Đơn giá", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "SL", Width = 60, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Total", HeaderText = "Thành tiền", Width = 110, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });

            dgvUsedServices.DataSource = _usedServicesTable;
        }

        private void RecalcRoomServiceTotals()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                var item = row.DataBoundItem as SelectedRoomWithTime;
                if (item == null) continue;

                decimal sum = 0m;
                if (_usedServicesTable != null && _usedServicesTable.Rows.Count > 0)
                {
                    sum = _usedServicesTable.AsEnumerable()
                        .Where(r => r.Field<int>("RoomID") == item.RoomID)
                        .Sum(r => r.Field<decimal>("UnitPrice") * r.Field<int>("Quantity"));
                }
                SetCellMoney(row, COL_ROOM_SVC, sum);
            }
        }

        private void UpdateGrandTotals()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                decimal baseCost = 0m, svcCost = 0m;
                if (row.Cells[COL_ROOM_BASE].Value != null)
                    decimal.TryParse(row.Cells[COL_ROOM_BASE].Value.ToString(), out baseCost);
                if (row.Cells[COL_ROOM_SVC].Value != null)
                    decimal.TryParse(row.Cells[COL_ROOM_SVC].Value.ToString(), out svcCost);

                SetCellMoney(row, COL_ROOM_GRAND, baseCost + svcCost);
            }
        }

        private void BtnIncrease_Click(object sender, EventArgs e)
        {
            var m = dgvHotelServices.CurrentRow != null ? dgvHotelServices.CurrentRow.DataBoundItem as ServiceMenuItem : null;
            if (m == null)
            {
                MessageBox.Show("Chọn 1 dịch vụ trong menu bên phải.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Khi checkout, cho phép thêm nhanh theo từng phòng hiển thị
            int qty = (int)nbrIncrease.Value;
            if (qty <= 0) qty = 1;

            if (_checkoutMode)
            {
                foreach (var row in (_checkoutRows ?? new BindingList<CheckoutRow>()))
                {
                    var found = _usedServicesTable.AsEnumerable()
                        .FirstOrDefault(r => r.Field<int>("RoomID") == row.RoomID &&
                                             r.Field<int>("ServiceID") == m.ServiceID);

                    if (found == null)
                    {
                        var n = _usedServicesTable.NewRow();
                        n["RoomID"] = row.RoomID;
                        n["RoomNumber"] = row.RoomNumber;
                        n["ServiceID"] = m.ServiceID;
                        n["ServiceName"] = m.ServiceName;
                        n["UnitPrice"] = m.Price;
                        n["Quantity"] = qty;
                        _usedServicesTable.Rows.Add(n);
                    }
                    else
                    {
                        found["Quantity"] = found.Field<int>("Quantity") + qty;
                    }

                    row.ServiceTotal += m.Price * qty;
                }
                dgvUsedServices.Refresh();
                dataGridView1.Refresh();
                return;
            }

            // BOOK MODE: thêm cho các phòng đã tick
            var rooms = GetCheckedRooms();
            if (rooms.Count == 0)
            {
                MessageBox.Show("Hãy tick phòng ở bảng trên (cột 'Chọn') trước khi thêm dịch vụ.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (var room in rooms)
            {
                var found = _usedServicesTable.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("RoomID") == room.RoomID &&
                                         r.Field<int>("ServiceID") == m.ServiceID);

                if (found == null)
                {
                    var row = _usedServicesTable.NewRow();
                    row["RoomID"] = room.RoomID;
                    row["RoomNumber"] = room.RoomNumber;
                    row["ServiceID"] = m.ServiceID;
                    row["ServiceName"] = m.ServiceName;
                    row["UnitPrice"] = m.Price;
                    row["Quantity"] = qty;
                    _usedServicesTable.Rows.Add(row);
                }
                else
                {
                    found["Quantity"] = found.Field<int>("Quantity") + qty;
                }
            }

            RecalcRoomServiceTotals();
            UpdateGrandTotals();
        }

        private void BtnReduce_Click(object sender, EventArgs e)
        {
            if (dgvUsedServices.CurrentRow == null) return;
            var drv = dgvUsedServices.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            int qty = (int)nbrReduce.Value;
            if (qty <= 0) qty = 1;

            var row = drv.Row;
            int cur = Convert.ToInt32(row["Quantity"]);
            cur -= qty;
            if (cur <= 0) _usedServicesTable.Rows.Remove(row);
            else row["Quantity"] = cur;

            if (_checkoutMode)
            {
                // cập nhật tổng DV ở checkout rows
                var roomId = Convert.ToInt32(drv.Row["RoomID"]);
                var unit = Convert.ToDecimal(drv.Row["UnitPrice"]);
                var found = (_checkoutRows ?? new BindingList<CheckoutRow>()).FirstOrDefault(x => x.RoomID == roomId);
                if (found != null)
                {
                    found.ServiceTotal -= unit * qty;
                    if (found.ServiceTotal < 0) found.ServiceTotal = 0;
                }
                dataGridView1.Refresh();
                return;
            }

            RecalcRoomServiceTotals();
            UpdateGrandTotals();
        }

        // =========================================================
        // ====================== CUSTOMER PART ====================
        // =========================================================

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            var idNumber = (txtCCCD.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(idNumber))
            {
                MessageBox.Show("Nhập CCCD trước khi kiểm tra.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var c = _customerSvc.getCustomerByIDNumber(idNumber);
            if (c == null)
            {
                _customerId = 0;
                _customerIdNumber = idNumber;
                MessageBox.Show("Chưa có khách này. Vui lòng nhập các thông tin còn lại rồi bấm Đặt phòng để lưu khách mới.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTen.Focus();
                return;
            }

            _customerId = c.CustomerID;
            _customerIdNumber = c.IDNumber ?? idNumber;

            txtTen.Text = c.FullName ?? "";
            txtGender.Text = c.Gender ?? "";
            txtSDT.Text = c.Phone ?? "";
            txtEmail.Text = c.Email ?? "";
            txtDiachi.Text = c.Address ?? "";

            MessageBox.Show("Đã tìm thấy khách hàng và điền thông tin.", "OK",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool EnsureCustomerFromForm()
        {
            var idNumber = (txtCCCD.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(idNumber))
            {
                MessageBox.Show("Vui lòng nhập CCCD.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCCCD.Focus();
                return false;
            }

            if (_customerId > 0) return true;

            var existed = _customerSvc.getCustomerByIDNumber(idNumber);
            if (existed != null)
            {
                _customerId = existed.CustomerID;
                _customerIdNumber = existed.IDNumber ?? idNumber;
                return true;
            }

            var fullName = (txtTen.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Vui lòng nhập Tên khách hàng.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return false;
            }

            var newC = new Customer
            {
                FullName = fullName,
                Gender = (txtGender.Text ?? "").Trim(),
                Phone = (txtSDT.Text ?? "").Trim(),
                Email = (txtEmail.Text ?? "").Trim(),
                Address = (txtDiachi.Text ?? "").Trim(),
                IDNumber = idNumber
            };

            var inserted = _customerSvc.addNewCustomer(newC);
            if (inserted == null)
            {
                MessageBox.Show("Không thể lưu khách hàng mới.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _customerId = inserted.CustomerID;
            _customerIdNumber = inserted.IDNumber ?? idNumber;
            return true;
        }

        // =========================================================
        // ===================== BOOKING COMMIT ====================
        // =========================================================

        private RoomPlan GetPlanOrDefault(SelectedRoomWithTime r)
        {
            RoomPlan p;
            if (_plans.TryGetValue(r.RoomID, out p) && !string.IsNullOrWhiteSpace(p.PricingType))
                return p;

            var pr = FindPricing(r.RoomTypeID, "Daily");
            var ci = r.CheckIn;
            var co = r.CheckOut <= r.CheckIn ? r.CheckIn.AddDays(1) : r.CheckOut;

            return new RoomPlan
            {
                RoomID = r.RoomID,
                CheckIn = ci,
                CheckOut = co,
                PricingType = pr != null ? pr.PricingType : "Daily",
                UnitPrice = pr != null ? (decimal?)pr.Price : null,
                CalculatedCost = 0m
            };
        }

        private RoomPricing FindPricing(int roomTypeId, string pricingType)
        {
            var list = GetActivePricingByTypeId(roomTypeId);
            return list.FirstOrDefault(x => x.PricingType != null &&
                                            x.PricingType.Equals(pricingType, StringComparison.OrdinalIgnoreCase));
        }

        private void BtnDatphong_Click(object sender, EventArgs e)
        {
            if (_rooms.Count == 0)
            {
                MessageBox.Show("Chưa có phòng nào để đặt.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_currentUserId <= 0)
            {
                MessageBox.Show("Thiếu UserID đăng nhập. Hãy truyền CurrentUserId từ màn hình chính.", "Thiếu thông tin",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EnsureCustomerFromForm()) return;

            var requests = new List<Booking>();
            foreach (var r in _rooms)
            {
                var plan = GetPlanOrDefault(r);
                if (plan.CheckOut <= plan.CheckIn)
                {
                    MessageBox.Show("Phòng " + r.RoomNumber + ": Check-out phải > Check-in.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var pr = _pricingRepo.GetByRoomType(r.RoomTypeID)
                                     .FirstOrDefault(x => x.IsActive && string.Equals(x.PricingType, plan.PricingType, StringComparison.OrdinalIgnoreCase));
                if (pr == null)
                {
                    MessageBox.Show("Phòng " + r.RoomNumber + ": Không tìm thấy đơn giá '" + plan.PricingType + "'.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                requests.Add(new Booking
                {
                    CustomerID = _customerId,
                    RoomID = r.RoomID,
                    PricingID = pr.PricingID,
                    CreatedBy = _currentUserId,
                    BookingDate = DateTime.Now,
                    CheckInDate = plan.CheckIn,
                    CheckOutDate = plan.CheckOut,
                    Status = "Booked",
                    Notes = txtNote.Text != null ? txtNote.Text.Trim() : ""
                });
            }

            try
            {
                var map = _bookingSvc.AddBookingGroup(_customerId, _currentUserId, requests, _usedServicesTable);

                MessageBox.Show("Đặt thành công " + map.Count + "/" + requests.Count + " phòng.",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đặt phòng thất bại: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // =========================================================
        // ===================== Inner RoomPlan ====================
        // =========================================================
        public class RoomPlan
        {
            public int RoomID { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public string PricingType { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal CalculatedCost { get; set; }
        }
    }

    // menu dịch vụ hiển thị ở lưới phải
    public class ServiceMenuItem
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public decimal Price { get; set; }
    }
}
