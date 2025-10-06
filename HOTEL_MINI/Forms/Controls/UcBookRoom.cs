using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HOTEL_MINI.BLL;
using HOTEL_MINI.Forms;
using HOTEL_MINI.Forms.Dialogs;
using HOTEL_MINI.Model.Response;
using MiniHotel.Models;
using WinFormsTimer = System.Windows.Forms.Timer;

namespace HOTEL_MINI.Forms.Controls
{
    public partial class UcBookRoom : UserControl
    {
        private readonly RoomService _roomSvc = new RoomService();
        private readonly RoomTypeService _rtSvc = new RoomTypeService();

        private BindingList<RoomBrowsePriceItem> _data = new BindingList<RoomBrowsePriceItem>();
        private readonly HashSet<int> _selectedRoomIds = new HashSet<int>();
        private readonly Dictionary<int, Tuple<DateTime, DateTime>> _selectedTimes = new Dictionary<int, Tuple<DateTime, DateTime>>();
        private readonly Dictionary<int, RoomPlan> _selectedPlans = new Dictionary<int, RoomPlan>();
        private readonly Dictionary<int, RoomBrowsePriceItem> _roomCache = new Dictionary<int, RoomBrowsePriceItem>();

        private readonly WinFormsTimer _debounceTimer;
        private int _currentCustomerId;
        private string _currentIdNumber = "";

        public int CurrentUserId { get; set; }

        public UcBookRoom()
        {
            InitializeComponent();

            _debounceTimer = new WinFormsTimer { Interval = 300 };
            _debounceTimer.Tick += (s, e) =>
            {
                _debounceTimer.Stop();
                LoadRooms();
            };

            Load += UcBookRoom_Load;
        }

        public void SetCustomer(int customerId, string idNumber)
        {
            _currentCustomerId = customerId;
            _currentIdNumber = idNumber ?? "";
        }

        private void UcBookRoom_Load(object sender, EventArgs e)
        {
            try
            {
                SetupFilters();
                SetupGrid();
                WireEvents();
                BeginInvoke(new Action(LoadRooms));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải màn hình đặt phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ReloadRoomsNow() => LoadRooms();

        private void SetupFilters()
        {
            var statuses = _roomSvc.GetAllRoomStatus() ?? new List<string>();
            statuses.Insert(0, "(Tất cả)");
            cboStatus.DataSource = statuses;
            cboStatus.SelectedIndex = 0;

            var types = _rtSvc.GetAllRoomTypes() ?? new List<RoomTypes>();
            types.Insert(0, new RoomTypes { RoomTypeID = 0, TypeName = "(Tất cả loại)" });
            cboRoomType.DataSource = types;
            cboRoomType.DisplayMember = nameof(RoomTypes.TypeName);
            cboRoomType.ValueMember = nameof(RoomTypes.RoomTypeID);

            var now = DateTime.Now;

            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFrom.Value = now;

            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTo.Value = now.AddDays(1);
        }

        private void SetupGrid()
        {
            dgvRoom.AutoGenerateColumns = false;
            dgvRoom.AllowUserToAddRows = false;
            dgvRoom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoom.MultiSelect = false;
            dgvRoom.ReadOnly = false;
            dgvRoom.Columns.Clear();

            var colSel = new DataGridViewCheckBoxColumn { HeaderText = "Chọn phòng ", Name = "colSelect", Width = 55 };
            dgvRoom.Columns.Add(colSel);

            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Hình thức", Name = "colPlanType", ReadOnly = true, Width = 100 });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBrowsePriceItem.RoomID), HeaderText = "RoomID", Name = "colRoomID", Visible = false });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBrowsePriceItem.RoomNumber), HeaderText = "Phòng", ReadOnly = true, Width = 90 });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBrowsePriceItem.RoomTypeName), HeaderText = "Loại", ReadOnly = true, Width = 140 });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBrowsePriceItem.Status), HeaderText = "Trạng thái", ReadOnly = true, Width = 110 });

            var money = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight };
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBrowsePriceItem.PriceHourly), HeaderText = "Giá/giờ", ReadOnly = true, Width = 90, DefaultCellStyle = money });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBrowsePriceItem.PriceNightly), HeaderText = "Giá/đêm", ReadOnly = true, Width = 90, DefaultCellStyle = money });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBrowsePriceItem.PriceDaily), HeaderText = "Giá/ngày", ReadOnly = true, Width = 90, DefaultCellStyle = money });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBrowsePriceItem.PriceWeekly), HeaderText = "Giá/tuần", ReadOnly = true, Width = 90, DefaultCellStyle = money });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBrowsePriceItem.Note), HeaderText = "Ghi chú", ReadOnly = true, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            dgvRoom.CellContentClick += DgvRoom_CellContentClick;
            dgvRoom.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dgvRoom.CurrentCell is DataGridViewCheckBoxCell) dgvRoom.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            dgvRoom.DataSource = _data;
        }

        private void WireEvents()
        {
            cboStatus.SelectedIndexChanged += (s, e) => _debounceTimer.Start();
            cboRoomType.SelectedIndexChanged += (s, e) => _debounceTimer.Start();
            dtpFrom.ValueChanged += (s, e) => _debounceTimer.Start();
            dtpTo.ValueChanged += (s, e) => _debounceTimer.Start();
            btnBooking.Click += BtnBooking_Click;
            btnDetail.Click += BtnDetail_Click;
        }

        private void LoadRooms()
        {
            int? typeId = null;
            var rt = cboRoomType.SelectedItem as RoomTypes;
            if (rt != null && rt.RoomTypeID > 0) typeId = rt.RoomTypeID;

            var selectedStatus = cboStatus.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedStatus) || selectedStatus == "(Tất cả)") selectedStatus = null;

            var items = _roomSvc.SearchRoomsWithPrices(typeId, null) ?? new List<RoomBrowsePriceItem>();
            var availList = _roomSvc.SearchRooms(dtpFrom.Value, dtpTo.Value, typeId, null) ?? new List<RoomBrowseItem>();
            var statusMap = availList.ToDictionary(x => x.RoomID, x => x);

            foreach (var it in items)
            {
                if (statusMap.TryGetValue(it.RoomID, out var s))
                {
                    it.Status = s.Status;
                    it.AvailableAtRange = s.AvailableAtRange;
                }
                else
                {
                    it.AvailableAtRange = true;
                }
                _roomCache[it.RoomID] = it;
            }

            var final = items;
            if (!string.IsNullOrWhiteSpace(selectedStatus))
                final = final.Where(x => string.Equals(x.Status, selectedStatus, StringComparison.OrdinalIgnoreCase)).ToList();

            _data = new BindingList<RoomBrowsePriceItem>(final.OrderBy(x => x.RoomNumber).ToList());
            dgvRoom.DataSource = _data;

            foreach (DataGridViewRow row in dgvRoom.Rows)
            {
                var it = row.DataBoundItem as RoomBrowsePriceItem;
                if (it == null) continue;

                var selCell = row.Cells["colSelect"];
                var canSelect = it.AvailableAtRange && !string.Equals(it.Status, "Maintenance", StringComparison.OrdinalIgnoreCase);
                selCell.ReadOnly = !canSelect;

                if (!canSelect && _selectedRoomIds.Contains(it.RoomID))
                {
                    _selectedRoomIds.Remove(it.RoomID);
                    _selectedTimes.Remove(it.RoomID);
                    _selectedPlans.Remove(it.RoomID);
                }

                if (string.Equals(it.Status, "Maintenance", StringComparison.OrdinalIgnoreCase))
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    selCell.ToolTipText = "Phòng đang bảo trì.";
                }
                else if (!it.AvailableAtRange)
                {
                    if (string.Equals(it.Status, "Booked", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = dgvRoom.DefaultCellStyle.ForeColor;
                        selCell.ToolTipText = "Phòng đã đặt (Booked) trong khoảng thời gian lọc.";
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                        selCell.ToolTipText = "Phòng đang bận (Occupied) trong khoảng thời gian lọc.";
                    }
                }
                else
                {
                    row.DefaultCellStyle.BackColor = dgvRoom.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dgvRoom.DefaultCellStyle.ForeColor;
                    selCell.ToolTipText = string.Empty;
                }

                selCell.Value = _selectedRoomIds.Contains(it.RoomID);

                if (_selectedPlans.TryGetValue(it.RoomID, out var plan))
                    row.Cells["colPlanType"].Value = string.IsNullOrWhiteSpace(plan.PricingType) ? "" : plan.PricingType;
                else
                    row.Cells["colPlanType"].Value = "";
            }
        }

        private void DgvRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var item = dgvRoom.Rows[e.RowIndex].DataBoundItem as RoomBrowsePriceItem;
            if (item == null) return;

            var colName = dgvRoom.Columns[e.ColumnIndex].Name;

            if (colName == "colSelect")
            {
                var cell = dgvRoom.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.ReadOnly) return;

                dgvRoom.CommitEdit(DataGridViewDataErrorContexts.Commit);
                var isChecked = Convert.ToBoolean(cell.EditedFormattedValue ?? false);

                if (isChecked)
                {
                    var ok = OpenRoomTimeDialogAndApply(item, e.RowIndex);
                    if (!ok)
                    {
                        cell.Value = false;
                        dgvRoom.EndEdit();
                        return;
                    }
                }
                else
                {
                    _selectedRoomIds.Remove(item.RoomID);
                    _selectedTimes.Remove(item.RoomID);
                    _selectedPlans.Remove(item.RoomID);
                    dgvRoom.Rows[e.RowIndex].Cells["colPlanType"].Value = "";
                }
            }
        }


        private void BtnDetail_Click(object sender, EventArgs e)
        {
            var cur = dgvRoom.CurrentRow != null ? dgvRoom.CurrentRow.DataBoundItem as RoomBrowsePriceItem : null;
            if (cur == null)
            {
                MessageBox.Show("Hãy chọn 1 phòng để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Func<decimal?, string> P = v => v.HasValue ? v.Value.ToString("N0") : "-";

            var planType = "";
            if (_selectedPlans.TryGetValue(cur.RoomID, out var plan)) planType = plan.PricingType;

            MessageBox.Show(
                "Phòng: " + cur.RoomNumber +
                "\nLoại: " + cur.RoomTypeName +
                "\nTrạng thái: " + cur.Status +
                "\nGiá/giờ: " + P(cur.PriceHourly) +
                "\nGiá/đêm: " + P(cur.PriceNightly) +
                "\nGiá/ngày: " + P(cur.PriceDaily) +
                "\nGiá/tuần: " + P(cur.PriceWeekly) +
                "\nĐã chọn: " + (string.IsNullOrWhiteSpace(planType) ? "(chưa chọn)" : planType) +
                "\nGhi chú: " + cur.Note,
                "Chi tiết phòng", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBooking_Click(object sender, EventArgs e)
        {
            if (_selectedRoomIds.Count == 0)
            {
                MessageBox.Show("Chọn ít nhất 1 phòng để đặt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (CurrentUserId <= 0)
            {
                MessageBox.Show("Thiếu UserID đăng nhập. Hãy truyền CurrentUserId từ màn hình chính.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var missing = _selectedRoomIds.Where(id => !_selectedPlans.ContainsKey(id) || !_selectedTimes.ContainsKey(id)).ToList();
            if (missing.Count > 0)
            {
                MessageBox.Show("Một hoặc nhiều phòng chưa có hình thức và thời gian.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = _selectedRoomIds
                .Where(id => _roomCache.ContainsKey(id))
                .Select(id =>
                {
                    var r = _roomCache[id];
                    DateTime ci, co;
                    if (_selectedTimes.TryGetValue(id, out var t)) { ci = t.Item1; co = t.Item2; }
                    else { ci = dtpFrom.Value; co = dtpTo.Value; }

                    return new SelectedRoomWithTime
                    {
                        RoomID = r.RoomID,
                        RoomNumber = r.RoomNumber,
                        RoomTypeName = r.RoomTypeName,
                        RoomTypeID = r.RoomTypeID,
                        CheckIn = ci,
                        CheckOut = co
                    };
                })
                .ToList();

            var plans = new Dictionary<int, frmBookingCreate.RoomPlan>();
            foreach (var kv in _selectedPlans)
            {
                if (!_selectedRoomIds.Contains(kv.Key)) continue;
                var p = kv.Value;
                plans[kv.Key] = new frmBookingCreate.RoomPlan
                {
                    RoomID = p.RoomID,
                    CheckIn = p.CheckIn,
                    CheckOut = p.CheckOut,
                    PricingType = p.PricingType,
                    UnitPrice = p.UnitPrice,
                    CalculatedCost = p.CalculatedCost,
                    IsReceiveNow = p.IsReceiveNow
                };
            }

            using (var f = new frmBookingCreate(selected, plans, _currentCustomerId, _currentIdNumber, CurrentUserId))
            {
                f.StartPosition = FormStartPosition.CenterParent;
                var rs = f.ShowDialog(FindForm());
                if (rs == DialogResult.OK) ClearSelectionsAndReload();
            }
        }

        private void ClearSelectionsAndReload()
        {
            _selectedRoomIds.Clear();
            _selectedTimes.Clear();
            _selectedPlans.Clear();

            foreach (DataGridViewRow row in dgvRoom.Rows)
            {
                var cell = row.Cells["colSelect"];
                if (cell != null && !cell.ReadOnly) cell.Value = false;
                row.Cells["colPlanType"].Value = "";
            }

            LoadRooms();
        }

        private bool OpenRoomTimeDialogAndApply(RoomBrowsePriceItem item, int rowIndex)
        {
            var baseIn = dtpFrom.Value;
            var baseOut = dtpTo.Value;
            if (_selectedTimes.TryGetValue(item.RoomID, out var t))
            {
                baseIn = t.Item1;
                baseOut = t.Item2;
            }

            using (var dlg = new dlgRoomTime(item.RoomTypeID, baseIn, baseOut, "Chọn phòng để đặt"))
            {
                if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                {
                    _selectedTimes[item.RoomID] = Tuple.Create(dlg.CheckIn, dlg.CheckOut);
                    _selectedPlans[item.RoomID] = new RoomPlan
                    {
                        RoomID = item.RoomID,
                        CheckIn = dlg.CheckIn,
                        CheckOut = dlg.CheckOut,
                        PricingType = dlg.PricingType,
                        UnitPrice = dlg.UnitPrice,
                        CalculatedCost = dlg.CalculatedCost,
                        IsReceiveNow = dlg.IsReceiveNow
                    };

                    dgvRoom.Rows[rowIndex].Cells["colPlanType"].Value = dlg.PricingType;

                    _selectedRoomIds.Add(item.RoomID);
                    var selCell = dgvRoom.Rows[rowIndex].Cells["colSelect"];
                    if (!selCell.ReadOnly) selCell.Value = true;

                    return true;
                }
            }
            return false;
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
