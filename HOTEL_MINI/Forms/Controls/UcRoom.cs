using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;   // Room, RoomPricing
using MiniHotel.Models;          // RoomTypes

namespace HOTEL_MINI.Forms
{
    public partial class UcRoom : UserControl
    {
        private readonly RoomService _roomSvc;
        private readonly RoomTypeService _rtSvc;
        private readonly RoomPricingService _pricingSvc;

        private enum FormMode { View, Adding, Editing }
        private FormMode _mode = FormMode.View;

        private int _currentRoomId = 0;
        private List<RoomTypes> _allTypes = new List<RoomTypes>();
        private List<Room> _allRooms = new List<Room>();

        // Lớp hiển thị cho grid
        private class RoomRow
        {
            public int RoomID { get; set; }
            public string RoomNumber { get; set; }
            public int RoomTypeID { get; set; }
            public string RoomTypeName { get; set; }
            public string RoomStatus { get; set; }
            public string Note { get; set; }
        }

        private class TypeFilterItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public override string ToString() => Name;
        }

        public UcRoom(RoomService roomSvc, RoomTypeService rtSvc, RoomPricingService pricingSvc)
        {
            InitializeComponent();
            _roomSvc = roomSvc;
            _rtSvc = rtSvc;
            _pricingSvc = pricingSvc;

            Load += UcRoom_Load;

            // filter
            txtSearch.TextChanged += (s, e) => LoadRooms();
            cboRoomTypeNameSearch.SelectedIndexChanged += (s, e) => LoadRooms();
            cbmRoomStatusSearch.SelectedIndexChanged += (s, e) => LoadRooms();

            // chọn row -> hiện info
            dgvRoom.SelectionChanged += (s, e) =>
            {
                if (_mode == FormMode.View) LoadRightPanelInfo();
            };

            // CRUD
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            // KHÔNG cần chức năng xóa nữa -> bỏ KeyDown/Delete

            SetMode(FormMode.View);
        }

        private void UcRoom_Load(object sender, EventArgs e)
        {
            SetupFilters();
            SetupRoomGrid();
            LoadRooms();
        }

        // ------- UI state -------
        private void SetMode(FormMode m)
        {
            _mode = m;
            bool edit = (m != FormMode.View);

            txtRoomNumber.ReadOnly = !edit;
            txtNote.ReadOnly = !edit;
            cboRoomTypeName.Enabled = edit;
            cboRoomStatus.Enabled = edit;

            btnAdd.Enabled = m == FormMode.View;
            btnEdit.Enabled = m == FormMode.View && dgvRoom.Rows.Count > 0;
            btnSave.Enabled = edit;
            btnCancel.Enabled = edit;
        }

        private void ClearForm()
        {
            _currentRoomId = 0;
            txtRoomNumber.Text = "";
            txtNote.Text = "";
            if (cboRoomTypeName.Items.Count > 0) cboRoomTypeName.SelectedIndex = 0;
            if (cboRoomStatus.Items.Count > 0) cboRoomStatus.SelectedIndex = 0;

            lblRoomNumber.Text = "";
            txtHourlyPrice.Clear();
            txtNightlyPrice.Clear();
            txtDayPrice.Clear();
            txtWeeklyPrice.Clear();
        }

        private Room ReadForm()
        {
            return new Room
            {
                RoomID = _currentRoomId,
                RoomNumber = (txtRoomNumber.Text ?? "").Trim(),
                RoomTypeID = (cboRoomTypeName.SelectedValue is int v) ? v : 0,
                RoomStatus = (cboRoomStatus.SelectedItem as string) ?? "",
                Note = txtNote.Text ?? ""
            };
        }

        private void FillFormFromRow(RoomRow row)
        {
            if (row == null) return;
            _currentRoomId = row.RoomID;
            txtRoomNumber.Text = row.RoomNumber;
            try { cboRoomTypeName.SelectedValue = row.RoomTypeID; } catch { }
            try { cboRoomStatus.SelectedItem = row.RoomStatus; } catch { }
            txtNote.Text = row.Note;
            lblRoomNumber.Text = row.RoomNumber;
            LoadPricesByRoomType(row.RoomTypeID);
        }

        // ------- Filters & Grid -------
        private void SetupFilters()
        {
            _allTypes = _rtSvc.GetAllRoomTypes();

            var filterItems = new List<TypeFilterItem> { new TypeFilterItem { Id = 0, Name = "Tất cả" } };
            foreach (var t in _allTypes) filterItems.Add(new TypeFilterItem { Id = t.RoomTypesID, Name = t.TypeName });

            cboRoomTypeNameSearch.DisplayMember = "Name";
            cboRoomTypeNameSearch.ValueMember = "Id";
            cboRoomTypeNameSearch.DataSource = filterItems;
            cboRoomTypeNameSearch.SelectedIndex = 0;

            cboRoomTypeName.DisplayMember = "TypeName";
            cboRoomTypeName.ValueMember = "RoomTypesID";
            cboRoomTypeName.DataSource = _allTypes.ToList();

            var statuses = new List<string> { "Available", "Booked", "Maintenance", "Occupied" };
            cbmRoomStatusSearch.DataSource = new List<string> { "Tất cả" }.Concat(statuses).ToList();
            cboRoomStatus.DataSource = statuses;
        }

        private void SetupRoomGrid()
        {
            dgvRoom.AutoGenerateColumns = false;
            dgvRoom.Columns.Clear();

            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { Name = "RoomID", DataPropertyName = "RoomID", HeaderText = "ID", Visible = false });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { Name = "RoomNumber", DataPropertyName = "RoomNumber", HeaderText = "Phòng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { Name = "RoomTypeName", DataPropertyName = "RoomTypeName", HeaderText = "Loại", Width = 150 });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { Name = "RoomStatus", DataPropertyName = "RoomStatus", HeaderText = "Trạng thái", Width = 130 });

            dgvRoom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoom.MultiSelect = false;
        }

        private void LoadRooms(int? keepSelectedId = null)
        {
            _allRooms = _roomSvc.getAllRoom(); // List<Room>

            string kw = (txtSearch.Text ?? "").Trim().ToLowerInvariant();
            int typeId = (cboRoomTypeNameSearch.SelectedValue is int v) ? v : 0;
            string st = cbmRoomStatusSearch.SelectedItem as string;

            var typeDict = _allTypes.ToDictionary(t => t.RoomTypesID, t => t.TypeName ?? "");

            var view = new List<RoomRow>();
            foreach (var r in _allRooms)
            {
                if (typeId != 0 && r.RoomTypeID != typeId) continue;
                if (!string.IsNullOrEmpty(st) && st != "Tất cả" &&
                    !string.Equals(r.RoomStatus ?? "", st, StringComparison.OrdinalIgnoreCase)) continue;
                if (kw != "" && (r.RoomNumber ?? "").ToLowerInvariant().IndexOf(kw) < 0) continue;

                view.Add(new RoomRow
                {
                    RoomID = r.RoomID,
                    RoomNumber = r.RoomNumber ?? "",
                    RoomTypeID = r.RoomTypeID,
                    RoomTypeName = typeDict.ContainsKey(r.RoomTypeID) ? typeDict[r.RoomTypeID] : "",
                    RoomStatus = r.RoomStatus ?? "",
                    Note = r.Note ?? ""
                });
            }

            dgvRoom.DataSource = view;

            if (keepSelectedId.HasValue) SelectRowById(keepSelectedId.Value);
            if (_mode == FormMode.View) LoadRightPanelInfo();
        }

        private void LoadRightPanelInfo()
        {
            if (dgvRoom.CurrentRow == null) { ClearForm(); return; }
            var row = dgvRoom.CurrentRow.DataBoundItem as RoomRow;
            if (row == null) { ClearForm(); return; }
            FillFormFromRow(row);
            txtNote.Text = ""; // nếu có field Note trong DB thì bind vào đây
        }

        private void LoadPricesByRoomType(int roomTypeId)
        {
            txtHourlyPrice.Clear();
            txtNightlyPrice.Clear();
            txtDayPrice.Clear();
            txtWeeklyPrice.Clear();

            var kinds = _pricingSvc.GetPricingTypes(); // Hourly, Nightly, Daily, Weekly (hoặc tiếng Việt)
            foreach (var k in kinds)
            {
                var p = _pricingSvc.GetByRoomTypeAndType(roomTypeId, k);
                if (p == null) continue;

                var key = (k ?? "").ToLowerInvariant();
                if (key.Contains("hour")) txtHourlyPrice.Text = p.Price.ToString("0.##");
                else if (key.Contains("night")) txtNightlyPrice.Text = p.Price.ToString("0.##");
                else if (key.Contains("day")) txtDayPrice.Text = p.Price.ToString("0.##");
                else if (key.Contains("week")) txtWeeklyPrice.Text = p.Price.ToString("0.##");
            }
        }

        private void SelectRowById(int id)
        {
            foreach (DataGridViewRow r in dgvRoom.Rows)
            {
                var row = r.DataBoundItem as RoomRow;
                if (row != null && row.RoomID == id)
                {
                    r.Selected = true;
                    if (r.Cells["RoomNumber"] != null) dgvRoom.CurrentCell = r.Cells["RoomNumber"];
                    break;
                }
            }
        }

        // ------- CRUD -------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetMode(FormMode.Adding);
            txtRoomNumber.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvRoom.CurrentRow == null) { MessageBox.Show("Chọn một phòng để sửa."); return; }
            var row = dgvRoom.CurrentRow.DataBoundItem as RoomRow;
            if (row == null) return;
            FillFormFromRow(row);
            SetMode(FormMode.Editing);
            txtRoomNumber.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var m = ReadForm();

            if (string.IsNullOrWhiteSpace(m.RoomNumber))
            { MessageBox.Show("Nhập Số phòng."); txtRoomNumber.Focus(); return; }
            if (m.RoomTypeID <= 0)
            { MessageBox.Show("Chọn Loại phòng."); cboRoomTypeName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(m.RoomStatus))
            { MessageBox.Show("Chọn Trạng thái."); cboRoomStatus.Focus(); return; }

            bool ok = false;
            if (_mode == FormMode.Adding) ok = AddRoomToService(m);
            else if (_mode == FormMode.Editing) ok = UpdateRoomInService(m);
            else { MessageBox.Show("Không ở chế độ Thêm/Sửa."); return; }

            if (ok)
            {
                MessageBox.Show("Lưu thành công!");
                SetMode(FormMode.View);
                LoadRooms(m.RoomID);
            }
            else MessageBox.Show("Không lưu được bản ghi (chưa cài đặt hàm BLL?).");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetMode(FormMode.View);
            LoadRightPanelInfo();
        }

        // ------- Wrapper gọi BLL theo tên hàm có sẵn -------
        private bool AddRoomToService(Room r)
        {
            var t = _roomSvc.GetType();
            foreach (var name in new[] { "AddRoom", "InsertRoom", "CreateRoom", "Add", "Insert", "SaveRoom" })
            {
                var mi = t.GetMethod(name, new[] { typeof(Room) });
                if (mi != null) return (bool)mi.Invoke(_roomSvc, new object[] { r });
            }
            return false;
        }

        private bool UpdateRoomInService(Room r)
        {
            var t = _roomSvc.GetType();
            foreach (var name in new[] { "UpdateRoom", "EditRoom", "Update", "SaveRoom" })
            {
                var mi = t.GetMethod(name, new[] { typeof(Room) });
                if (mi != null) return (bool)mi.Invoke(_roomSvc, new object[] { r });
            }
            return false;
        }
        // Cho form mẹ gọi khi tab "Loại phòng & Giá" đổi lựa chọn
        public void SelectRoomType(int roomTypeId)
        {
            // đặt filter theo loại phòng
            try { cboRoomTypeNameSearch.SelectedValue = roomTypeId; } catch { }

            // nạp lại danh sách theo filter
            LoadRooms();

            // chọn dòng đầu tiên có RoomTypeID trùng khớp
            foreach (DataGridViewRow r in dgvRoom.Rows)
            {
                if (r.DataBoundItem is RoomRow rr && rr.RoomTypeID == roomTypeId)
                {
                    r.Selected = true;
                    dgvRoom.CurrentCell = r.Cells["RoomNumber"];
                    LoadRightPanelInfo();
                    break;
                }
            }
        }

        // handlers rỗng do designer đã gán
        private void label4_Click(object sender, EventArgs e) { }
        private void txtRoomNumber_TextChanged(object sender, EventArgs e) { }
        private void lblRoomTypeName_Click(object sender, EventArgs e) { }
        private void cboRoomTypeName_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lblNote_Click(object sender, EventArgs e) { }
        private void txtNote_TextChanged(object sender, EventArgs e) { }
    }
}
