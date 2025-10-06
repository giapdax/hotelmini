using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using MiniHotel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace HOTEL_MINI.Forms
{
    public partial class frmBookingCreate : Form
    {
        private readonly ServicesService _svc = new ServicesService();
        private readonly RoomPricingRepository _pricingRepo = new RoomPricingRepository();
        private readonly BookingService _bookingSvc = new BookingService();
        private readonly CustomerService _customerSvc = new CustomerService();

        private readonly List<SelectedRoomWithTime> _rooms;
        private readonly Dictionary<int, RoomPlan> _plans;
        private readonly Dictionary<int, List<RoomPricing>> _pricingCache = new Dictionary<int, List<RoomPricing>>();

        private int _customerId;
        private string _customerIdNumber;
        private readonly int _currentUserId;

        private BindingList<ServiceVM> _serviceVMs;
        private DataTable _usedServicesTable;

        private const string COL_ROOM_PICK = "colRoomPick";
        private const string COL_ROOM_TYPE = "colPlanType";
        private const string COL_ROOM_UNIT = "colUnit";
        private const string COL_ROOM_BASE = "colBaseTotal";
        private const string COL_ROOM_SVC = "colServiceTotal";
        private const string COL_ROOM_GRAND = "colGrandTotal";

        public frmBookingCreate(List<SelectedRoomWithTime> rooms,
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

            Load += FrmBookingCreate_Load;
            btnClose.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };


            nbrIncrease.Minimum = 1; nbrIncrease.Maximum = 999; nbrIncrease.Value = 1;
            nbrReduce.Minimum = 1; nbrReduce.Maximum = 999; nbrReduce.Value = 1;

            cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGender.Items.Clear();
            cboGender.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
        }

        public frmBookingCreate(List<SelectedRoomWithTime> rooms, Dictionary<int, RoomPlan> plans)
            : this(rooms, plans, 0, string.Empty, 0) { }

        public frmBookingCreate()
            : this(new List<SelectedRoomWithTime>(), new Dictionary<int, RoomPlan>(), 0, string.Empty, 0) { }

        private class ServiceVM : INotifyPropertyChanged
        {
            public int ServiceID { get; set; }
            public string ServiceName { get; set; }
            public decimal Price { get; set; }
            public int DbQuantity { get; set; }
            private int _plannedDelta;
            public int PlannedDelta
            {
                get => _plannedDelta;
                set { if (_plannedDelta != value) { _plannedDelta = Math.Max(0, value); OnPropertyChanged(); OnPropertyChanged(nameof(DisplayQuantity)); } }
            }
            public int DisplayQuantity => Math.Max(0, DbQuantity - PlannedDelta);

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string name = null)
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void FrmBookingCreate_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_customerIdNumber)) txtCCCD.Text = _customerIdNumber;
            if (_rooms.Count > 0)
                lblBookingID.Text = $"Đặt {_rooms.Count} phòng: {string.Join(", ", _rooms.Select(r => r.RoomNumber))}";

            SetupRoomsGrid();
            LoadRoomsGrid();
            SetupServicesMenu();
            SetupUsedServicesTable();
            BindUsedServicesGrid();

            RecalcRoomBaseTotals();
            RecalcRoomServiceTotals();
            UpdateGrandTotals();
        }

        private void SetupRoomsGrid()
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

        private void LoadRoomsGrid()
        {
            dataGridView1.DataSource = new BindingList<SelectedRoomWithTime>(_rooms);
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                r.Cells[COL_ROOM_PICK].Value = false;
                if (r.DataBoundItem is SelectedRoomWithTime item)
                {
                    if (_plans.TryGetValue(item.RoomID, out var plan))
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
        }

        private List<SelectedRoomWithTime> GetCheckedRooms()
        {
            var list = new List<SelectedRoomWithTime>();
            if (!dataGridView1.Columns.Contains(COL_ROOM_PICK)) return list;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                bool tick = row.Cells[COL_ROOM_PICK].Value is bool b && b;
                if (tick && row.DataBoundItem is SelectedRoomWithTime item) list.Add(item);
            }
            return list;
        }

        private void SetCellMoney(DataGridViewRow row, string colName, decimal value)
        {
            if (!dataGridView1.Columns.Contains(colName)) return;
            row.Cells[colName].Value = value;
        }

        private List<RoomPricing> GetActivePricingByTypeId(int roomTypeId)
        {
            if (!_pricingCache.TryGetValue(roomTypeId, out var list))
            {
                list = _pricingRepo.GetByRoomType(roomTypeId).Where(p => p.IsActive).ToList();
                _pricingCache[roomTypeId] = list;
            }
            return list;
        }

        private static decimal? FindRate(IEnumerable<RoomPricing> list, string pricingType)
        {
            var p = list.FirstOrDefault(x => x.PricingType != null && x.PricingType.Equals(pricingType, StringComparison.OrdinalIgnoreCase));
            return p?.Price;
        }

        private static int CeilToInt(double v) => (int)Math.Ceiling(v);

        private decimal CalcCostByPlan(SelectedRoomWithTime item)
        {
            if (!_plans.TryGetValue(item.RoomID, out var plan) || string.IsNullOrWhiteSpace(plan.PricingType))
                return CalcBestCost(item);

            DateTime ci = plan.CheckIn;
            DateTime co = plan.CheckOut;
            if (co <= ci) return 0m;

            decimal unit = plan.UnitPrice ?? 0m;

            if (string.Equals(plan.PricingType, "Hourly", StringComparison.OrdinalIgnoreCase))
                return unit * CeilToInt((co - ci).TotalHours);

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
                return unit;

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

                if (_plans.TryGetValue(item.RoomID, out var plan))
                {
                    row.Cells[COL_ROOM_TYPE].Value = plan.PricingType ?? "";
                    row.Cells[COL_ROOM_UNIT].Value = plan.UnitPrice.HasValue ? (object)plan.UnitPrice.Value : DBNull.Value;
                }
            }
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
                if (dataGridView1.Columns.Contains(COL_ROOM_BASE) && row.Cells[COL_ROOM_BASE].Value != null)
                    decimal.TryParse(row.Cells[COL_ROOM_BASE].Value?.ToString(), out baseCost);
                if (dataGridView1.Columns.Contains(COL_ROOM_SVC) && row.Cells[COL_ROOM_SVC].Value != null)
                    decimal.TryParse(row.Cells[COL_ROOM_SVC].Value?.ToString(), out svcCost);

                SetCellMoney(row, COL_ROOM_GRAND, baseCost + svcCost);
            }
        }

        private void SetupServicesMenu()
        {
            var services = _svc.GetAllServices().Where(s => s.IsActive).ToList();
            _serviceVMs = new BindingList<ServiceVM>(
                services.Select(s => new ServiceVM
                {
                    ServiceID = s.ServiceID,
                    ServiceName = s.ServiceName,
                    Price = s.Price,
                    DbQuantity = s.Quantity,
                    PlannedDelta = 0
                }).ToList()
            );

            var gv = dgvHotelServices;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.MultiSelect = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ServiceVM.ServiceID), HeaderText = "ID", Width = 50, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ServiceVM.ServiceName), HeaderText = "Dịch vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ServiceVM.Price), HeaderText = "Đơn giá", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(ServiceVM.DisplayQuantity), HeaderText = "Tồn (hiển thị)", Width = 100, ReadOnly = true });

            gv.DataSource = _serviceVMs;
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

        private void BtnIncrease_Click(object sender, EventArgs e)
        {
            var vm = dgvHotelServices.CurrentRow?.DataBoundItem as ServiceVM;
            if (vm == null)
            {
                MessageBox.Show("Chọn 1 dịch vụ trong menu bên phải.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int qty = (int)nbrIncrease.Value;
            if (qty <= 0) qty = 1;

            var rooms = GetCheckedRooms();
            if (rooms.Count == 0)
            {
                MessageBox.Show("Hãy tick phòng (cột 'Chọn') trước khi thêm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int available = Math.Max(0, vm.DbQuantity - vm.PlannedDelta);
            if (available <= 0)
            {
                MessageBox.Show($"Dịch vụ '{vm.ServiceName}' đã hết hàng.", "Hết hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int totalNeed = qty * rooms.Count;

            if (totalNeed > available)
            {
                int maxPerRoom = available / rooms.Count;
                if (maxPerRoom <= 0)
                {
                    MessageBox.Show($"Dịch vụ '{vm.ServiceName}' chỉ còn {available} – không đủ cho {rooms.Count} phòng.", "Không đủ tồn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show($"Không đủ tồn. Tự động điều chỉnh số lượng mỗi phòng xuống {maxPerRoom}.", "Điều chỉnh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                qty = maxPerRoom;
                totalNeed = qty * rooms.Count;
            }

            foreach (var room in rooms)
            {
                var existing = _usedServicesTable.AsEnumerable()
                    .FirstOrDefault(r => r.Field<int>("RoomID") == room.RoomID &&
                                         r.Field<int>("ServiceID") == vm.ServiceID);
                if (existing != null)
                {
                    existing["Quantity"] = existing.Field<int>("Quantity") + qty;
                }
                else
                {
                    var row = _usedServicesTable.NewRow();
                    row["RoomID"] = room.RoomID;
                    row["RoomNumber"] = room.RoomNumber;
                    row["ServiceID"] = vm.ServiceID;
                    row["ServiceName"] = vm.ServiceName;
                    row["UnitPrice"] = vm.Price;
                    row["Quantity"] = qty;
                    _usedServicesTable.Rows.Add(row);
                }
            }

            vm.PlannedDelta += totalNeed;

            RecalcRoomServiceTotals();
            UpdateGrandTotals();
        }

        private void BtnReduce_Click(object sender, EventArgs e)
        {
            if (dgvUsedServices.CurrentRow == null)
            {
                MessageBox.Show("Chọn dịch vụ muốn giảm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var drv = dgvUsedServices.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            int reduceBy = (int)nbrReduce.Value;
            if (reduceBy <= 0) reduceBy = 1;

            int serviceId = Convert.ToInt32(drv.Row["ServiceID"]);
            int currentQty = Convert.ToInt32(drv.Row["Quantity"]);
            int actually = Math.Min(reduceBy, currentQty);

            int left = currentQty - actually;
            if (left <= 0) _usedServicesTable.Rows.Remove(drv.Row);
            else drv.Row["Quantity"] = left;

            var vm = _serviceVMs.FirstOrDefault(x => x.ServiceID == serviceId);
            if (vm != null)
                vm.PlannedDelta = Math.Max(0, vm.PlannedDelta - actually);

            RecalcRoomServiceTotals();
            UpdateGrandTotals();
        }

        private void BtnCheck_Click(object sender, EventArgs e)
        {
            var idNumber = (txtCCCD.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(idNumber))
            {
                MessageBox.Show("Nhập CCCD trước khi kiểm tra.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            cboGender.SelectedItem = string.IsNullOrWhiteSpace(c.Gender) ? null :
                (new[] { "Nam", "Nữ", "Khác" }.Contains(c.Gender) ? c.Gender : "Khác");
            txtSDT.Text = c.Phone ?? "";
            txtEmail.Text = c.Email ?? "";
            txtDiachi.Text = c.Address ?? "";

            MessageBox.Show("Đã tìm thấy khách hàng và điền thông tin.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool EnsureCustomerFromForm()
        {
            var idNumber = (txtCCCD.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(idNumber))
            {
                MessageBox.Show("Vui lòng nhập CCCD.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            var phone = (txtSDT.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show("Khách mới: vui lòng nhập Tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTen.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Khách mới: vui lòng nhập SĐT.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            var newC = new Customer
            {
                FullName = fullName,
                Gender = cboGender.SelectedItem?.ToString() ?? "",
                Phone = phone,
                Email = (txtEmail.Text ?? "").Trim(),
                Address = (txtDiachi.Text ?? "").Trim(),
                IDNumber = idNumber
            };

            var inserted = _customerSvc.addNewCustomer(newC);
            if (inserted == null)
            {
                MessageBox.Show("Không thể lưu khách hàng mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _customerId = inserted.CustomerID;
            _customerIdNumber = inserted.IDNumber ?? idNumber;
            return true;
        }

        private RoomPlan GetPlanOrDefault(SelectedRoomWithTime r)
        {
            if (_plans.TryGetValue(r.RoomID, out var p) && !string.IsNullOrWhiteSpace(p.PricingType))
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
                UnitPrice = pr?.Price,
                CalculatedCost = 0m
            };
        }

        private RoomPricing FindPricing(int roomTypeId, string pricingType)
        {
            var list = GetActivePricingByTypeId(roomTypeId);
            return list.FirstOrDefault(x => x.PricingType != null &&
                                            x.PricingType.Equals(pricingType, StringComparison.OrdinalIgnoreCase));
        }

        private Dictionary<int, int> BuildPlannedServiceNeeds()
        {
            var dict = new Dictionary<int, int>();
            if (_usedServicesTable == null || _usedServicesTable.Rows.Count == 0) return dict;

            foreach (DataRow r in _usedServicesTable.Rows)
            {
                int sid = r.Field<int>("ServiceID");
                int q = r.Field<int>("Quantity");
                if (!dict.ContainsKey(sid)) dict[sid] = 0;
                dict[sid] += q;
            }
            return dict;
        }

        private bool PreflightCheckStock(out string failMessage)
        {
            failMessage = null;
            var needs = BuildPlannedServiceNeeds();
            if (needs.Count == 0) return true;

            foreach (var kv in needs)
            {
                int serviceId = kv.Key;
                int need = kv.Value;

                var vm = _serviceVMs.FirstOrDefault(x => x.ServiceID == serviceId);
                int stockNow = vm?.DbQuantity ?? 0;
                if (need > stockNow)
                {
                    var name = vm?.ServiceName ?? ("Service#" + serviceId);
                    failMessage = $"Không đủ tồn kho cho '{name}'. Cần {need}, còn {stockNow}.";
                    return false;
                }
            }
            return true;
        }

        private void BtnDatphong_Click(object sender, EventArgs e)
        {
            if (_rooms.Count == 0)
            {
                MessageBox.Show("Chưa có phòng nào để đặt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_currentUserId <= 0)
            {
                MessageBox.Show("Thiếu UserID đăng nhập. Hãy truyền CurrentUserId từ màn hình chính.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EnsureCustomerFromForm()) return;

            if (!PreflightCheckStock(out var msg))
            {
                MessageBox.Show(msg, "Thiếu hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var requests = new List<BookingRoom>();
            var lines = new List<string>();

            foreach (var r in _rooms)
            {
                var plan = GetPlanOrDefault(r);
                if (plan.CheckOut <= plan.CheckIn)
                {
                    MessageBox.Show($"Phòng {r.RoomNumber}: Check-out phải > Check-in.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var pr = _pricingRepo.GetByRoomType(r.RoomTypeID)
                                     .FirstOrDefault(x => x.IsActive &&
                                                          string.Equals(x.PricingType, plan.PricingType, StringComparison.OrdinalIgnoreCase));
                if (pr == null)
                {
                    MessageBox.Show($"Phòng {r.RoomNumber}: Không tìm thấy đơn giá '{plan.PricingType}'.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                requests.Add(new BookingRoom
                {
                    RoomID = r.RoomID,
                    PricingID = pr.PricingID,
                    CheckInDate = plan.CheckIn,
                    CheckOutDate = plan.CheckOut,
                    Status = plan.IsReceiveNow ? "CheckedIn" : "Booked",
                    Note = (txtNote.Text ?? "").Trim()
                });

                lines.Add($"- Phòng {r.RoomNumber} ({plan.PricingType}): {plan.CheckIn:dd/MM/yyyy HH:mm} → {plan.CheckOut:dd/MM/yyyy HH:mm} {(plan.IsReceiveNow ? "[Nhận ngay]" : "[Giữ chỗ]")}");
            }

            var confirmText = "Bạn có muốn đặt các phòng sau không?\n\n" + string.Join("\n", lines);
            var confirm = MessageBox.Show(confirmText, "Xác nhận đặt phòng", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (confirm != DialogResult.Yes) return;

            try
            {
                var map = _bookingSvc.AddBookingGroup(_customerId, _currentUserId, requests, _usedServicesTable);
                SetupServicesMenu();
                foreach (var vm in _serviceVMs) vm.PlannedDelta = 0;

                MessageBox.Show($"Đặt thành công {map.Count}/{requests.Count} phòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đặt phòng thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public class RoomPlan
        {
            public int RoomID { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public string PricingType { get; set; }
            public decimal? UnitPrice { get; set; }
            public decimal CalculatedCost { get; set; }
            public bool IsReceiveNow { get; set; } = true;
        }
    }
}
