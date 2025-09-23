using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using MiniHotel.Models;

namespace HOTEL_MINI.Forms
{
    public partial class UcRoom : UserControl
    {
        RoomService _roomSvc = new RoomService();
        RoomTypeService _rtSvc = new RoomTypeService();
        RoomPricingService _pricingSvc = new RoomPricingService();

        Room _room = new Room();
        bool _add = false;

        List<RoomTypes> _allTypes = new List<RoomTypes>();
        List<Room> _allRooms = new List<Room>();

        public UcRoom()
        {
            InitializeComponent();

            Load += UcRoom_Load;
            dgvRoom.CellClick += dgvRoom_CellClick;

            txtSearch.TextChanged += (_, __) => LoadData();
            cboRoomTypeNameSearch.SelectedIndexChanged += (_, __) => LoadData();
            cbmRoomStatusSearch.SelectedIndexChanged += (_, __) => LoadData();

            cboRoomTypeName.SelectedIndexChanged += (_, __) =>
            {
                var id = cboRoomTypeName.SelectedValue is int v ? v : 0;
                LoadPricesByRoomType(id);
            };
        }

        void UcRoom_Load(object sender, EventArgs e)
        {
            _allTypes = _rtSvc.GetAllRoomTypes() ?? new List<RoomTypes>();
            cboRoomTypeName.DisplayMember = "TypeName";
            cboRoomTypeName.ValueMember = "RoomTypeID";
            cboRoomTypeName.DataSource = _allTypes.ToList();

            var statuses = new List<string> { "Available", "Booked", "Maintenance", "Occupied" };
            cboRoomStatus.DataSource = statuses;

            var filterTypes = new List<object> { new { Id = 0, Name = "Tất cả" } }
                .Concat(_allTypes.Select(t => new { Id = t.RoomTypeID, Name = t.TypeName }))
                .ToList();
            cboRoomTypeNameSearch.DisplayMember = "Name";
            cboRoomTypeNameSearch.ValueMember = "Id";
            cboRoomTypeNameSearch.DataSource = filterTypes;
            cbmRoomStatusSearch.DataSource = new List<string> { "Tất cả" }.Concat(statuses).ToList();

            LoadData();
            SetViewMode();
        }

        void LoadData()
        {
            _allRooms = _roomSvc.getAllRoom() ?? new List<Room>();

            string kw = (txtSearch.Text ?? "").Trim().ToLowerInvariant();
            int typeId = (cboRoomTypeNameSearch.SelectedValue is int v) ? v : 0;
            string st = cbmRoomStatusSearch.SelectedItem as string;

            var typeDict = _allTypes.ToDictionary(t => t.RoomTypeID, t => t.TypeName ?? "");

            var view = _allRooms
                .Where(r => typeId == 0 || r.RoomTypeID == typeId)
                .Where(r => string.IsNullOrEmpty(st) || st == "Tất cả" ||
                            string.Equals(r.RoomStatus ?? "", st, StringComparison.OrdinalIgnoreCase))
                .Where(r => string.IsNullOrEmpty(kw) || (r.RoomNumber ?? "").ToLowerInvariant().Contains(kw))
                .Select(r => new
                {
                    r.RoomID,
                    r.RoomNumber,
                    r.RoomTypeID,
                    RoomTypeName = typeDict.ContainsKey(r.RoomTypeID) ? typeDict[r.RoomTypeID] : "",
                    r.RoomStatus,
                    r.Note
                })
                .ToList();

            dgvRoom.ReadOnly = true;
            dgvRoom.AutoGenerateColumns = true;
            dgvRoom.DataSource = view;

            if (dgvRoom.Rows.Count > 0)
            {
                dgvRoom.Rows[0].Selected = true;
                dgvRoom_CellClick(dgvRoom, new DataGridViewCellEventArgs(0, 0));
            }
            else
            {
                ClearForm();
            }
        }

        void ClearForm()
        {
            _room = new Room { RoomID = 0 };
            txtRoomNumber.Clear();
            txtNote.Clear();
            lblRoomNumber.Text = "";
            if (cboRoomTypeName.Items.Count > 0) cboRoomTypeName.SelectedIndex = 0;
            if (cboRoomStatus.Items.Count > 0) cboRoomStatus.SelectedIndex = 0;

            txtHourlyPrice.Clear();
            txtNightlyPrice.Clear();
            txtDayPrice.Clear();
            txtWeeklyPrice.Clear();
        }

        void FillFormFromRow(object row)
        {
            if (row == null) { ClearForm(); return; }

            int roomId = (int)row.GetType().GetProperty("RoomID").GetValue(row);
            string roomNum = row.GetType().GetProperty("RoomNumber").GetValue(row)?.ToString() ?? "";
            int typeId = (int)row.GetType().GetProperty("RoomTypeID").GetValue(row);
            string status = row.GetType().GetProperty("RoomStatus").GetValue(row)?.ToString() ?? "";
            string note = row.GetType().GetProperty("Note").GetValue(row)?.ToString() ?? "";

            _room.RoomID = roomId;
            _room.RoomNumber = roomNum;
            _room.RoomTypeID = typeId;
            _room.RoomStatus = status;
            _room.Note = note;

            txtRoomNumber.Text = roomNum;
            try { cboRoomTypeName.SelectedValue = typeId; } catch { }
            try { cboRoomStatus.SelectedItem = status; } catch { }
            txtNote.Text = note;

            lblRoomNumber.Text = roomNum;
            LoadPricesByRoomType(typeId);
        }

        void LayDLPhong()
        {
            _room.RoomNumber = txtRoomNumber.Text?.Trim();
            _room.RoomTypeID = cboRoomTypeName.SelectedValue is int v ? v : 0;
            _room.RoomStatus = cboRoomStatus.SelectedItem as string ?? "";
            _room.Note = txtNote.Text ?? "";
        }

        void btnAdd_Click(object sender, EventArgs e)
        {
            _add = true;
            ClearForm();
            SetEditMode();
            txtRoomNumber.Focus();
        }

        void btnEdit_Click(object sender, EventArgs e)
        {
            if (_room.RoomID <= 0) return;
            _add = false;
            SetEditMode();
            txtRoomNumber.Focus();
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            LayDLPhong();

            if (string.IsNullOrWhiteSpace(_room.RoomNumber))
            { MessageBox.Show("Nhập Số phòng."); txtRoomNumber.Focus(); return; }
            if (_room.RoomTypeID <= 0)
            { MessageBox.Show("Chọn Loại phòng."); cboRoomTypeName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(_room.RoomStatus))
            { MessageBox.Show("Chọn Trạng thái."); cboRoomStatus.Focus(); return; }

            bool ok;
            if (_add)
            {
                var all = _roomSvc.getAllRoom() ?? new List<Room>();
                var num = (_room.RoomNumber ?? "").Trim();
                if (all.Any(r => string.Equals((r.RoomNumber ?? "").Trim(), num, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Số phòng đã tồn tại."); txtRoomNumber.Focus(); txtRoomNumber.SelectAll(); return;
                }
                ok = CallAdd(_room);
            }
            else
            {
                ok = CallUpdate(_room);
            }

            if (!ok)
            {
                MessageBox.Show("Lưu thất bại.");
                return;
            }

            MessageBox.Show(_add ? "Thêm thành công!" : "Cập nhật thành công!");
            _add = false;
            SetViewMode();
            LoadData();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            _add = false;
            SetViewMode();
            LoadData();
        }

        void dgvRoom_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvRoom.CurrentRow == null) return;
            FillFormFromRow(dgvRoom.CurrentRow.DataBoundItem);
        }

        void SetViewMode()
        {
            btnAdd.Enabled = true;
            btnEdit.Enabled = dgvRoom.Rows.Count > 0;
            btnSave.Enabled = false;
            btnCancel.Enabled = false;

            txtRoomNumber.ReadOnly = true;
            txtNote.ReadOnly = true;
            cboRoomTypeName.Enabled = false;
            cboRoomStatus.Enabled = false;
        }

        void SetEditMode()
        {
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            txtRoomNumber.ReadOnly = false;
            txtNote.ReadOnly = false;
            cboRoomTypeName.Enabled = true;
            cboRoomStatus.Enabled = true;
        }

        bool CallAdd(Room r)
        {
            var t = _roomSvc.GetType();
            foreach (var name in new[] { "AddRoom", "InsertRoom", "CreateRoom", "Add", "Insert", "SaveRoom" })
            {
                var mi = t.GetMethod(name, new[] { typeof(Room) });
                if (mi != null) return (bool)mi.Invoke(_roomSvc, new object[] { r });
            }
            return false;
        }

        bool CallUpdate(Room r)
        {
            var t = _roomSvc.GetType();
            foreach (var name in new[] { "UpdateRoom", "EditRoom", "Update", "SaveRoom" })
            {
                var mi = t.GetMethod(name, new[] { typeof(Room) });
                if (mi != null) return (bool)mi.Invoke(_roomSvc, new object[] { r });
            }
            return false;
        }

        void LoadPricesByRoomType(int roomTypeId)
        {
            txtHourlyPrice.Clear();
            txtNightlyPrice.Clear();
            txtDayPrice.Clear();
            txtWeeklyPrice.Clear();
            if (roomTypeId <= 0) return;

            var kinds = _pricingSvc.GetPricingTypes() ?? new List<string>();
            foreach (var k in kinds)
            {
                var p = _pricingSvc.GetByRoomTypeAndType(roomTypeId, k);
                if (p == null) continue;

                var key = (k ?? "").Trim().ToLowerInvariant();
                if (key.Contains("hour") || key.Contains("giờ")) txtHourlyPrice.Text = p.Price.ToString("0.##");
                else if (key.Contains("night") || key.Contains("đêm")) txtNightlyPrice.Text = p.Price.ToString("0.##");
                else if (key.Contains("day") || key.Contains("ngày") || key.Contains("daily")) txtDayPrice.Text = p.Price.ToString("0.##");
                else if (key.Contains("week") || key.Contains("tuần")) txtWeeklyPrice.Text = p.Price.ToString("0.##");
            }
        }

        public void SelectRoomType(int roomTypeId)
        {
            try { cboRoomTypeNameSearch.SelectedValue = roomTypeId; } catch { }
            LoadData();

            foreach (DataGridViewRow r in dgvRoom.Rows)
            {
                var data = r.DataBoundItem;
                if (data == null) continue;

                var prop = data.GetType().GetProperty("RoomTypeID");
                if (prop != null && prop.GetValue(data) is int id && id == roomTypeId)
                {
                    r.Selected = true;
                    if (r.Cells["RoomNumber"] != null)
                        dgvRoom.CurrentCell = r.Cells["RoomNumber"];
                    break;
                }
            }
        }
    }
}
