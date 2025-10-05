using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Forms;                 // <-- thêm để dùng frmBookingCreate
using HOTEL_MINI.Forms.Dialogs;
using HOTEL_MINI.Model.Response;
using MiniHotel.Models;

namespace HOTEL_MINI.Forms.Controls
{
    public partial class UcBookRoom : UserControl
    {
        private readonly RoomService _roomSvc = new RoomService();
        private readonly RoomTypeService _rtSvc = new RoomTypeService();
        private readonly RoomRepository _roomRepo = new RoomRepository();

        private BindingList<RoomBrowsePriceItem> _data = new BindingList<RoomBrowsePriceItem>();

        private readonly HashSet<int> _selectedRoomIds = new HashSet<int>();
        private readonly Dictionary<int, Tuple<DateTime, DateTime>> _selectedTimes = new Dictionary<int, Tuple<DateTime, DateTime>>();
        private readonly Dictionary<int, RoomPlan> _selectedPlans = new Dictionary<int, RoomPlan>();
        private readonly Dictionary<int, RoomBrowsePriceItem> _roomCache = new Dictionary<int, RoomBrowsePriceItem>();

        private int _currentCustomerId;
        private string _currentIdNumber = "";

        public int CurrentUserId { get; set; }  // set từ frmBooking

        public UcBookRoom()
        {
            InitializeComponent();
            this.Load += UcBookRoom_Load;
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

                // trì hoãn gọi sau khi dtp đã set giá trị mặc định xong
                this.BeginInvoke(new Action(() => LoadRooms()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải màn hình đặt phòng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupFilters()
        {
            var statuses = _roomSvc.getAllRoomStatus() ?? new List<string>();
            statuses.Insert(0, "(Tất cả)");
            cboStatus.DataSource = statuses;
            cboStatus.SelectedIndex = 0;

            var types = _rtSvc.GetAllRoomTypes() ?? new List<RoomTypes>();
            types.Insert(0, new RoomTypes { RoomTypeID = 0, TypeName = "(Tất cả loại)" });
            cboRoomType.DataSource = types;
            cboRoomType.DisplayMember = nameof(RoomTypes.TypeName);
            cboRoomType.ValueMember = nameof(RoomTypes.RoomTypeID);

            var today = DateTime.Today;
            dtpFrom.Format = DateTimePickerFormat.Custom;
            dtpFrom.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFrom.Value = today.AddHours(14);

            dtpTo.Format = DateTimePickerFormat.Custom;
            dtpTo.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpTo.Value = today.AddDays(1).AddHours(12);
        }

        private void SetupGrid()
        {
            dgvRoom.AutoGenerateColumns = false;
            dgvRoom.AllowUserToAddRows = false;
            dgvRoom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoom.MultiSelect = false;
            dgvRoom.ReadOnly = false;
            dgvRoom.Columns.Clear();

            var colSel = new DataGridViewCheckBoxColumn { HeaderText = "Chọn", Name = "colSelect", Width = 55 };
            dgvRoom.Columns.Add(colSel);

            var colTimeBtn = new DataGridViewButtonColumn
            {
                HeaderText = "Thời gian",
                Name = "colTime",
                Text = "Đặt TG",
                UseColumnTextForButtonValue = true,
                Width = 80
            };
            dgvRoom.Columns.Add(colTimeBtn);

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
            dgvRoom.DataSource = _data;
        }

        private void WireEvents()
        {
            cboStatus.SelectedIndexChanged += delegate { LoadRooms(); };
            cboRoomType.SelectedIndexChanged += delegate { LoadRooms(); };
            dtpFrom.ValueChanged += delegate { LoadRooms(); };
            dtpTo.ValueChanged += delegate { LoadRooms(); };

            btnBooking.Click += BtnBooking_Click;
            btnDetail.Click += BtnDetail_Click;
        }

        private void LoadRooms()
        {
            // roomType filter
            int? typeId = null;
            var rt = cboRoomType.SelectedItem as RoomTypes;
            if (rt != null && rt.RoomTypeID > 0) typeId = rt.RoomTypeID;

            // selected status from combo
            var selectedStatus = cboStatus.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedStatus) || selectedStatus == "(Tất cả)")
                selectedStatus = null;

            // 1) get price items (no date)
            var items = _roomSvc.SearchRoomsWithPrices(typeId, null) ?? new List<RoomBrowsePriceItem>();

            // 2) get status per room in the given date range
            var availList = _roomRepo.SearchRooms(dtpFrom.Value, dtpTo.Value, typeId, null) ?? new List<RoomBrowseItem>();
            var statusMap = availList.ToDictionary(x => x.RoomID, x => x);

            // 3) merge: assign Status (StatusAtRange) and AvailableAtRange into price items
            foreach (var it in items)
            {
                if (statusMap.ContainsKey(it.RoomID))
                {
                    var s = statusMap[it.RoomID];
                    it.Status = s.Status; // StatusAtRange from SearchRooms
                    it.AvailableAtRange = s.AvailableAtRange;
                }
                else
                {
                    // fallback: if repo didn't return info, assume available
                    it.AvailableAtRange = true;
                }

                // update cache
                _roomCache[it.RoomID] = it;
            }

            // 4) apply status filter (if any)
            var final = items;
            if (!string.IsNullOrWhiteSpace(selectedStatus))
            {
                final = final.Where(x => string.Equals(x.Status, selectedStatus, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // 5) bind
            _data = new BindingList<RoomBrowsePriceItem>(final.OrderBy(x => x.RoomNumber).ToList());
            dgvRoom.DataSource = _data;

            // 6) update rows UI: selectable, colors, tooltip, and clear selections for unavailable rooms
            foreach (DataGridViewRow row in dgvRoom.Rows)
            {
                var it = row.DataBoundItem as RoomBrowsePriceItem;
                if (it == null) continue;

                var selCell = row.Cells["colSelect"];
                // treat Maintenance as not selectable
                bool canSelect = it.AvailableAtRange && !string.Equals(it.Status, "Maintenance", StringComparison.OrdinalIgnoreCase);
                selCell.ReadOnly = !canSelect;

                // if previously selected but now not available -> remove selection & plans/times
                if (!canSelect && _selectedRoomIds.Contains(it.RoomID))
                {
                    _selectedRoomIds.Remove(it.RoomID);
                    _selectedTimes.Remove(it.RoomID);
                    _selectedPlans.Remove(it.RoomID);
                }

                // visual
                if (string.Equals(it.Status, "Maintenance", StringComparison.OrdinalIgnoreCase))
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    selCell.ToolTipText = "Phòng đang bảo trì.";
                }
                else if (!it.AvailableAtRange)
                {
                    // Booked or Occupied in the selected interval
                    if (string.Equals(it.Status, "Booked", StringComparison.OrdinalIgnoreCase))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightYellow;
                        row.DefaultCellStyle.ForeColor = dgvRoom.DefaultCellStyle.ForeColor;
                        selCell.ToolTipText = "Phòng đã đặt (Booked) trong khoảng thời gian đang lọc.";
                    }
                    else // Occupied or other non-available
                    {
                        row.DefaultCellStyle.BackColor = Color.MistyRose;
                        row.DefaultCellStyle.ForeColor = Color.DarkRed;
                        selCell.ToolTipText = "Phòng đang bận (Occupied) trong khoảng thời gian đang lọc.";
                    }
                }
                else
                {
                    // available
                    row.DefaultCellStyle.BackColor = dgvRoom.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dgvRoom.DefaultCellStyle.ForeColor;
                    selCell.ToolTipText = string.Empty;
                }

                // restore checkbox value if still selected
                selCell.Value = _selectedRoomIds.Contains(it.RoomID);

                // show plan type if any
                RoomPlan plan;
                row.Cells["colPlanType"].Value =
                    (_selectedPlans.TryGetValue(it.RoomID, out plan) && !string.IsNullOrWhiteSpace(plan.PricingType))
                    ? plan.PricingType : "";
            }
        }

        //private void DgvRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (e.RowIndex < 0) return;

        //    var item = dgvRoom.Rows[e.RowIndex].DataBoundItem as RoomBrowsePriceItem;
        //    if (item == null) return;

        //    var colName = dgvRoom.Columns[e.ColumnIndex].Name;

        //    if (colName == "colTime")
        //    {
        //        DateTime baseIn = dtpFrom.Value, baseOut = dtpTo.Value;
        //        Tuple<DateTime, DateTime> t;
        //        if (_selectedTimes.TryGetValue(item.RoomID, out t)) { baseIn = t.Item1; baseOut = t.Item2; }

        //        using (var dlg = new dlgRoomTime(item.RoomTypeID, baseIn, baseOut, "Phòng " + item.RoomNumber))
        //        {
        //            if (dlg.ShowDialog(this.FindForm()) == DialogResult.OK)
        //            {
        //                _selectedTimes[item.RoomID] = Tuple.Create(dlg.CheckIn, dlg.CheckOut);
        //                _selectedPlans[item.RoomID] = new RoomPlan
        //                {
        //                    RoomID = item.RoomID,
        //                    CheckIn = dlg.CheckIn,
        //                    CheckOut = dlg.CheckOut,
        //                    PricingType = dlg.PricingType,
        //                    UnitPrice = dlg.UnitPrice,
        //                    CalculatedCost = dlg.CalculatedCost,
        //                    IsReceiveNow = dlg.IsReceiveNow
        //                };
        //                dgvRoom.Rows[e.RowIndex].Cells["colPlanType"].Value = dlg.PricingType;
        //            }
        //        }
        //        return;
        //    }


        //    if (colName == "colSelect")
        //    {
        //        var cell = dgvRoom.Rows[e.RowIndex].Cells[e.ColumnIndex];
        //        if (cell.ReadOnly) return;

        //        dgvRoom.CommitEdit(DataGridViewDataErrorContexts.Commit);
        //        bool tick = cell.Value != null && (bool)cell.Value;

        //        if (tick)
        //        {
        //            _selectedRoomIds.Add(item.RoomID);
        //            if (!_selectedTimes.ContainsKey(item.RoomID))
        //                _selectedTimes[item.RoomID] = Tuple.Create(dtpFrom.Value, dtpTo.Value);
        //        }
        //        else
        //        {
        //            _selectedRoomIds.Remove(item.RoomID);
        //            _selectedTimes.Remove(item.RoomID);
        //            _selectedPlans.Remove(item.RoomID);
        //            dgvRoom.Rows[e.RowIndex].Cells["colPlanType"].Value = "";
        //        }
        //    }
        //}
        private void DgvRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var item = dgvRoom.Rows[e.RowIndex].DataBoundItem as RoomBrowsePriceItem;
            if (item == null) return;

            var colName = dgvRoom.Columns[e.ColumnIndex].Name;

            // Xử lý khi click chọn thời gian
            if (colName == "colTime")
            {
                // Dòng chưa tick thì không được mở dlg
                if (!_selectedRoomIds.Contains(item.RoomID))
                {
                    MessageBox.Show("Hãy tick chọn dòng trước khi chọn thời gian.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!_selectedRoomIds.Any()) return; // an toàn

                // Lấy RoomID dòng đầu tiên tick
                var firstSelectedRoomId = _selectedRoomIds.First();
                var firstItem = _roomCache[firstSelectedRoomId];

                // Lấy thời gian base từ kế hoạch trước đó nếu có
                DateTime baseIn, baseOut;
                if (_selectedTimes.TryGetValue(firstSelectedRoomId, out var t))
                {
                    baseIn = t.Item1;
                    baseOut = t.Item2;
                }
                else
                {
                    baseIn = dtpFrom.Value;
                    baseOut = dtpTo.Value;
                }

                using (var dlg = new dlgRoomTime(firstItem.RoomTypeID, baseIn, baseOut, "Phòng " + firstItem.RoomNumber))
                {
                    if (dlg.ShowDialog(this.FindForm()) == DialogResult.OK)
                    {
                        foreach (var roomId in _selectedRoomIds)
                        {
                            // Cập nhật thời gian và kế hoạch cho tất cả dòng tick
                            _selectedTimes[roomId] = Tuple.Create(dlg.CheckIn, dlg.CheckOut);
                            _selectedPlans[roomId] = new RoomPlan
                            {
                                RoomID = roomId,
                                CheckIn = dlg.CheckIn,
                                CheckOut = dlg.CheckOut,
                                PricingType = dlg.PricingType,
                                UnitPrice = dlg.UnitPrice,
                                CalculatedCost = dlg.CalculatedCost,
                                IsReceiveNow = dlg.IsReceiveNow
                            };

                            // Cập nhật hiển thị trên grid
                            var row = dgvRoom.Rows
                                .Cast<DataGridViewRow>()
                                .FirstOrDefault(r => ((RoomBrowsePriceItem)r.DataBoundItem).RoomID == roomId);

                            if (row != null)
                                row.Cells["colPlanType"].Value = dlg.PricingType;
                        }
                    }
                }

                return;
            }

            // Xử lý khi tick chọn/deselect
            if (colName == "colSelect")
            {
                var cell = dgvRoom.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.ReadOnly) return;

                dgvRoom.CommitEdit(DataGridViewDataErrorContexts.Commit);
                bool tick = cell.Value != null && (bool)cell.Value;

                if (tick)
                {
                    // Thêm phòng vào danh sách
                    _selectedRoomIds.Add(item.RoomID);

                    // Khởi tạo thời gian mặc định nếu chưa có
                    if (!_selectedTimes.ContainsKey(item.RoomID))
                        _selectedTimes[item.RoomID] = Tuple.Create(dtpFrom.Value, dtpTo.Value);
                }
                else
                {
                    // Bỏ phòng khỏi danh sách và xóa dữ liệu liên quan
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
                MessageBox.Show("Hãy chọn 1 phòng để xem chi tiết.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Func<decimal?, string> P = v => v.HasValue ? v.Value.ToString("N0") : "-";

            string planType = "";
            RoomPlan plan;
            if (_selectedPlans.TryGetValue(cur.RoomID, out plan)) planType = plan.PricingType;

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
                MessageBox.Show("Chọn ít nhất 1 phòng để đặt.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (CurrentUserId <= 0)
            {
                MessageBox.Show("Thiếu UserID đăng nhập. Hãy truyền CurrentUserId từ màn hình chính.",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selected = _selectedRoomIds
                .Where(id => _roomCache.ContainsKey(id))
                .Select(id =>
                {
                    var r = _roomCache[id];
                    Tuple<DateTime, DateTime> t;
                    DateTime ci, co;
                    if (_selectedTimes.TryGetValue(id, out t)) { ci = t.Item1; co = t.Item2; }
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

            // Map kế hoạch nội bộ -> kế hoạch của frmBookingCreate
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

            // MỞ ĐÚNG FORM ĐẶT PHÒNG
            using (var f = new frmBookingCreate(selected, plans, _currentCustomerId, _currentIdNumber, CurrentUserId))
            {
                f.StartPosition = FormStartPosition.CenterParent;
                var rs = f.ShowDialog(this.FindForm());
                if (rs == DialogResult.OK)
                {
                    ClearSelectionsAndReload();
                }
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

        // DTO nội bộ: lưu lựa chọn hình thức thuê
        public class RoomPlan
        {
            public int RoomID { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public string PricingType { get; set; }   // Hourly/Nightly/Daily/Weekly
            public decimal? UnitPrice { get; set; }
            public decimal CalculatedCost { get; set; }
            public bool IsReceiveNow { get; set; } = true;  // true = Nhận ngay, false = Nhận sau
        }
    }
}
