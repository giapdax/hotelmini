using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Forms.Dialogs;
using HOTEL_MINI.Model.Response;
using MiniHotel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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
                LoadRooms();
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

            var types = _rtSvc.GetAllRoomTypes() ?? new List<MiniHotel.Models.RoomTypes>();
            types.Insert(0, new MiniHotel.Models.RoomTypes { RoomTypeID = 0, TypeName = "(Tất cả loại)" });
            cboRoomType.DataSource = types;
            cboRoomType.DisplayMember = nameof(MiniHotel.Models.RoomTypes.TypeName);
            cboRoomType.ValueMember = nameof(MiniHotel.Models.RoomTypes.RoomTypeID);

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
            int? typeId = null;
            var rt = cboRoomType.SelectedItem as MiniHotel.Models.RoomTypes;
            if (rt != null && rt.RoomTypeID > 0) typeId = rt.RoomTypeID;

            string status = cboStatus.SelectedItem != null ? cboStatus.SelectedItem.ToString() : null;

            var items = _roomSvc.SearchRoomsWithPrices(typeId, status) ?? new List<RoomBrowsePriceItem>();
            var availList = _roomRepo.SearchRooms(dtpFrom.Value, dtpTo.Value, typeId, status) ?? new List<RoomBrowseItem>();
            var availMap = availList.ToDictionary(x => x.RoomID, x => x.AvailableAtRange);

            foreach (var it in items)
            {
                it.AvailableAtRange = availMap.ContainsKey(it.RoomID) && availMap[it.RoomID];
                _roomCache[it.RoomID] = it;
            }

            _data = new BindingList<RoomBrowsePriceItem>(items.OrderBy(x => x.RoomNumber).ToList());
            dgvRoom.DataSource = _data;

            foreach (DataGridViewRow row in dgvRoom.Rows)
            {
                var it = row.DataBoundItem as RoomBrowsePriceItem;
                if (it == null) continue;

                var selCell = row.Cells["colSelect"];
                selCell.ReadOnly = !it.AvailableAtRange;

                if (!it.AvailableAtRange)
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    selCell.ToolTipText = "Phòng bận trong khoảng thời gian đang lọc.";
                }
                else
                {
                    row.DefaultCellStyle.BackColor = dgvRoom.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.ForeColor = dgvRoom.DefaultCellStyle.ForeColor;
                    selCell.ToolTipText = string.Empty;
                }

                selCell.Value = _selectedRoomIds.Contains(it.RoomID);

                RoomPlan plan;
                row.Cells["colPlanType"].Value =
                    (_selectedPlans.TryGetValue(it.RoomID, out plan) && !string.IsNullOrWhiteSpace(plan.PricingType))
                    ? plan.PricingType : "";
            }
        }

        private void DgvRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var item = dgvRoom.Rows[e.RowIndex].DataBoundItem as RoomBrowsePriceItem;
            if (item == null) return;

            var colName = dgvRoom.Columns[e.ColumnIndex].Name;

            if (colName == "colTime")
            {
                DateTime baseIn = dtpFrom.Value, baseOut = dtpTo.Value;
                Tuple<DateTime, DateTime> t;
                if (_selectedTimes.TryGetValue(item.RoomID, out t)) { baseIn = t.Item1; baseOut = t.Item2; }

                using (var dlg = new dlgRoomTime(item.RoomTypeID, baseIn, baseOut, "Phòng " + item.RoomNumber))
                {
                    if (dlg.ShowDialog(this.FindForm()) == DialogResult.OK)
                    {
                        _selectedTimes[item.RoomID] = Tuple.Create(dlg.CheckIn, dlg.CheckOut);
                        _selectedPlans[item.RoomID] = new RoomPlan
                        {
                            RoomID = item.RoomID,
                            CheckIn = dlg.CheckIn,
                            CheckOut = dlg.CheckOut,
                            PricingType = dlg.PricingType,
                            UnitPrice = dlg.UnitPrice,
                            CalculatedCost = dlg.CalculatedCost
                        };
                        dgvRoom.Rows[e.RowIndex].Cells["colPlanType"].Value = dlg.PricingType;
                    }
                }
                return;
            }

            if (colName == "colSelect")
            {
                var cell = dgvRoom.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.ReadOnly) return;

                dgvRoom.CommitEdit(DataGridViewDataErrorContexts.Commit);
                bool tick = cell.Value != null && (bool)cell.Value;

                if (tick)
                {
                    _selectedRoomIds.Add(item.RoomID);
                    if (!_selectedTimes.ContainsKey(item.RoomID))
                        _selectedTimes[item.RoomID] = Tuple.Create(dtpFrom.Value, dtpTo.Value);
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

            var plans = new Dictionary<int, frmBookingDetail1.RoomPlan>();
            foreach (var kv in _selectedPlans)
            {
                if (!_selectedRoomIds.Contains(kv.Key)) continue;
                var p = kv.Value;
                plans[kv.Key] = new frmBookingDetail1.RoomPlan
                {
                    RoomID = p.RoomID,
                    CheckIn = p.CheckIn,
                    CheckOut = p.CheckOut,
                    PricingType = p.PricingType,
                    UnitPrice = p.UnitPrice,
                    CalculatedCost = p.CalculatedCost
                };
            }

            using (var f = new frmBookingDetail1(selected, plans, 0, "", CurrentUserId))
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
        }
    }
}
