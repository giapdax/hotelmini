// File: HOTEL_MINI/Forms/UcRoom.cs
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

        // [LOAD] cache dữ liệu hiển thị
        private List<RoomTypes> _allTypes = new List<RoomTypes>();
        private List<Room> _allRooms = new List<Room>();

        // ======= Model hiển thị cho grid =======
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

            // [LOAD] filter thay đổi → nạp lại danh sách
            txtSearch.TextChanged += (s, e) => LoadRooms();
            cboRoomTypeNameSearch.SelectedIndexChanged += (s, e) => LoadRooms();
            cbmRoomStatusSearch.SelectedIndexChanged += (s, e) => LoadRooms();

            // [LOAD] chọn row → hiện info bên phải
            dgvRoom.SelectionChanged += (s, e) =>
            {
                if (_mode == FormMode.View) LoadRightPanelInfo();
            };

            // CRUD
            btnAdd.Click += btnAdd_Click;       // [ADD]
            btnEdit.Click += btnEdit_Click;     // [UPDATE]
            btnCancel.Click += btnCancel_Click; // [LOAD] reset view

            SetMode(FormMode.View);
        }

        private void UcRoom_Load(object sender, EventArgs e)
        {
            SetupFilters();   // [LOAD]
            SetupRoomGrid();  // [LOAD]
            LoadRooms();      // [LOAD]
        }

        // ======= UI state =======
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

        // ======= Đọc dữ liệu từ form (dùng cho add/update) =======
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

        // ======= Đổ dữ liệu vào form khi chọn 1 dòng =======  [LOAD]
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

        // ================== [LOAD] Filters & Grid ==================
        private void SetupFilters()
        {
            _allTypes = _rtSvc.GetAllRoomTypes();

            var filterItems = new List<TypeFilterItem> { new TypeFilterItem { Id = 0, Name = "Tất cả" } };
            foreach (var t in _allTypes) filterItems.Add(new TypeFilterItem { Id = t.RoomTypeID, Name = t.TypeName });

            cboRoomTypeNameSearch.DisplayMember = "Name";
            cboRoomTypeNameSearch.ValueMember = "Id";
            cboRoomTypeNameSearch.DataSource = filterItems;
            cboRoomTypeNameSearch.SelectedIndex = 0;

            cboRoomTypeName.DisplayMember = "TypeName";
            cboRoomTypeName.ValueMember = "RoomTypeID";
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

            // 👇 Thêm cột Ghi chú (yêu cầu của bạn)
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Note",
                DataPropertyName = "Note",
                HeaderText = "Ghi chú",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvRoom.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoom.MultiSelect = false;
        }

        // ======= [LOAD] Nạp danh sách phòng & filter =======
        private void LoadRooms(int? keepSelectedId = null)
        {
            _allRooms = _roomSvc.getAllRoom();

            string kw = (txtSearch.Text ?? "").Trim().ToLowerInvariant();
            int typeId = (cboRoomTypeNameSearch.SelectedValue is int v) ? v : 0;
            string st = cbmRoomStatusSearch.SelectedItem as string;

            // ⚠️ Dùng đúng RoomTypesID
            var typeDict = _allTypes.ToDictionary(t => t.RoomTypeID, t => t.TypeName ?? "");

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

            // chọn dòng phù hợp / hoặc dòng đầu
            if (keepSelectedId.HasValue) SelectRowById(keepSelectedId.Value);
            else if (dgvRoom.Rows.Count > 0 && dgvRoom.CurrentRow == null)
            {
                dgvRoom.Rows[0].Selected = true;
                dgvRoom.CurrentCell = dgvRoom.Rows[0].Cells["RoomNumber"];
            }

            if (_mode == FormMode.View) LoadRightPanelInfo();

            // 🔔 Quan trọng: bật lại trạng thái nút theo data hiện tại
            btnEdit.Enabled = _mode == FormMode.View && dgvRoom.Rows.Count > 0;
            btnAdd.Enabled = _mode == FormMode.View;
            btnSave.Enabled = _mode != FormMode.View;
            btnCancel.Enabled = _mode != FormMode.View;
        }


        // ======= [LOAD] Hiển thị panel phải theo dòng chọn =======
        private void LoadRightPanelInfo()
        {
            if (dgvRoom.CurrentRow == null) { ClearForm(); return; }
            var row = dgvRoom.CurrentRow.DataBoundItem as RoomRow;
            if (row == null) { ClearForm(); return; }
            FillFormFromRow(row);

            // KHÔNG xóa txtNote nữa – để hiển thị dữ liệu
            // txtNote.Text = "";
        }

        // ======= [LOAD] Giá theo loại phòng =======
        private static string NormalizePricingKey(string s)
        {
            var k = (s ?? "").Trim().ToLowerInvariant();
            // EN
            if (k.StartsWith("hour") || k.Contains("per hour")) return "hourly";
            if (k.StartsWith("night")) return "nightly";
            if (k.StartsWith("day") || k.Contains("daily") || k.Contains("per day")) return "daily";
            if (k.StartsWith("week") || k.Contains("weekly")) return "weekly";
            // VI
            if (k.Contains("giờ")) return "hourly";
            if (k.Contains("đêm")) return "nightly";
            if (k.Contains("ngày")) return "daily";
            if (k.Contains("tuần")) return "weekly";
            return "";
        }

        private void SetPriceTextbox(string key, decimal price)
        {
            var val = price.ToString("0.##");
            switch (key)
            {
                case "hourly": txtHourlyPrice.Text = val; break;
                case "nightly": txtNightlyPrice.Text = val; break;
                case "daily": txtDayPrice.Text = val; break;
                case "weekly": txtWeeklyPrice.Text = val; break;
            }
        }

        private void LoadPricesByRoomType(int roomTypeId)
        {
            txtHourlyPrice.Clear();
            txtNightlyPrice.Clear();
            txtDayPrice.Clear();
            txtWeeklyPrice.Clear();

            // Lấy danh sách loại giá có trong enum/service
            var kinds = _pricingSvc.GetPricingTypes(); // ví dụ: Hourly, Nightly, Daily, Weekly (hoặc tiếng Việt)
            foreach (var k in kinds)
            {
                var p = _pricingSvc.GetByRoomTypeAndType(roomTypeId, k);
                if (p == null) continue;

                var key = NormalizePricingKey(k);
                SetPriceTextbox(key, p.Price);
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

        // ================== [ADD] / [UPDATE] ==================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetMode(FormMode.Adding);   // [ADD]
            txtRoomNumber.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvRoom.CurrentRow == null) { MessageBox.Show("Chọn một phòng để sửa."); return; }
            var row = dgvRoom.CurrentRow.DataBoundItem as RoomRow;
            if (row == null) return;
            FillFormFromRow(row);
            SetMode(FormMode.Editing);  // [UPDATE]
            txtRoomNumber.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var m = ReadForm();

            // ==== VALIDATE CƠ BẢN ====
            if (string.IsNullOrWhiteSpace(m.RoomNumber))
            { MessageBox.Show("Nhập Số phòng."); txtRoomNumber.Focus(); return; }
            if (m.RoomTypeID <= 0)
            { MessageBox.Show("Chọn Loại phòng."); cboRoomTypeName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(m.RoomStatus))
            { MessageBox.Show("Chọn Trạng thái."); cboRoomStatus.Focus(); return; }

            // ==== CHECK TRÙNG SỐ PHÒNG (UI) ====
            var num = (m.RoomNumber ?? "").Trim();
            var all = _roomSvc.getAllRoom(); // lấy lại để chắc cú
            bool duplicate = all.Any(r =>
                string.Equals((r.RoomNumber ?? "").Trim(), num, StringComparison.OrdinalIgnoreCase)
                && r.RoomID != m.RoomID);

            if (duplicate)
            {
                MessageBox.Show("Số phòng này đã tồn tại. Vui lòng nhập số khác.",
                                "Trùng số phòng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoomNumber.Focus();
                txtRoomNumber.SelectAll();
                return;
            }

            // ==== LƯU ====
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
            else
            {
                // fallback: nếu DAL vẫn trả false (ví dụ va chạm UNIQUE ở DB)
                MessageBox.Show("Không lưu được. Có thể số phòng đã tồn tại. Vui lòng kiểm tra lại.",
                                "Lỗi lưu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRoomNumber.Focus();
                txtRoomNumber.SelectAll();
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetMode(FormMode.View);  // [LOAD]
            LoadRightPanelInfo();    // [LOAD]
        }

        // ======= [ADD] gọi xuống BLL với tên hàm linh hoạt =======
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

        // ======= [UPDATE] gọi xuống BLL với tên hàm linh hoạt =======
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
        // Thêm vào trong class UcRoom (cùng cấp với các hàm khác)
        public void SelectRoomType(int roomTypeId)
        {
            // 1) Đặt filter theo loại phòng
            try { cboRoomTypeNameSearch.SelectedValue = roomTypeId; } catch { }

            // 2) Nạp lại danh sách theo filter
            LoadRooms();

            // 3) Chọn dòng đầu tiên có RoomTypeID khớp và hiển thị info bên phải
            foreach (DataGridViewRow r in dgvRoom.Rows)
            {
                if (r.DataBoundItem is RoomRow rr && rr.RoomTypeID == roomTypeId)
                {
                    r.Selected = true;
                    if (r.Cells["RoomNumber"] != null)
                        dgvRoom.CurrentCell = r.Cells["RoomNumber"];
                    LoadRightPanelInfo();
                    break;
                }
            }
        }


        // ======= handlers rỗng do designer đã gán =======
        private void label4_Click(object sender, EventArgs e) { }
        private void txtRoomNumber_TextChanged(object sender, EventArgs e) { }
        private void lblRoomTypeName_Click(object sender, EventArgs e) { }
        private void cboRoomTypeName_SelectedIndexChanged(object sender, EventArgs e) { }
        private void lblNote_Click(object sender, EventArgs e) { }
        private void txtNote_TextChanged(object sender, EventArgs e) { }
    }
}
