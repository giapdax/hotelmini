using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using MiniHotel.Models;

namespace HOTEL_MINI.Forms
{
    public partial class UcRoomType_Pricing : UserControl
    {
        // BLL
        RoomService _roomSvc = new RoomService();
        RoomTypeService _rtSvc = new RoomTypeService();
        RoomPricingService _prSvc = new RoomPricingService();

        // Cờ chế độ
        bool AddRt = false;
        bool AddPr = false;

        // Id đang chọn
        int _curRoomTypeId = 0;
        int _curPricingId = 0;

        public event Action<int> RoomTypeChanged;

        public UcRoomType_Pricing()
        {
            InitializeComponent();
            Load += Uc_Load;

            // RoomType
            dgvRoomType.CellClick += dgvRoomType_CellClick;
            btnAddRoomType.Click += btnAddRoomType_Click;
            btnEditRoomType.Click += btnEditRoomType_Click;
            btnSaveRoomType.Click += btnSaveRoomType_Click;
            btnCancelRoomType.Click += btnCancelRoomType_Click;

            // Pricing
            btnAddPricing.Click += btnAddPricing_Click;
            btnEditPricing.Click += btnEditPricing_Click;
            btnSavaPricing.Click += btnSavaPricing_Click;
            btnCancelpricing.Click += btnCancelpricing_Click;

            cboRoomType.SelectedIndexChanged += (_, __) => ShowPricingForSelection();
            cboPricingType.SelectedIndexChanged += (_, __) => ShowPricingForSelection();
        }

        // ===== LOAD =====
        private void Uc_Load(object sender, EventArgs e)
        {
            SetupRoomTypeGrid();
            LoadRoomTypeGrid();
            BindRoomTypeInputs();

            LoadRoomTypeCombo();
            LoadPricingTypeCombo();

            SetRtButtons(view: true);
            SetPrButtons(view: true);

            ShowPricingForSelection();
        }

        // ===== ROOM TYPE =====
        private void SetupRoomTypeGrid()
        {
            dgvRoomType.AutoGenerateColumns = false;
            dgvRoomType.Columns.Clear();
            dgvRoomType.Columns.Add(new DataGridViewTextBoxColumn { Name = "RoomTypesID", DataPropertyName = "RoomTypesID", HeaderText = "ID", Visible = false });
            dgvRoomType.Columns.Add(new DataGridViewTextBoxColumn { Name = "TypeName", DataPropertyName = "TypeName", HeaderText = "Loại phòng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvRoomType.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", DataPropertyName = "Description", HeaderText = "Mô tả", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvRoomType.Columns.Add(new DataGridViewTextBoxColumn { Name = "RoomCount", DataPropertyName = "RoomCount", HeaderText = "Số lượng", Width = 100 });
            dgvRoomType.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoomType.MultiSelect = false;
        }

        private void LoadRoomTypeGrid()
        {
            var types = _rtSvc.GetAllRoomTypes() ?? new List<RoomTypes>();
            var rooms = _roomSvc.getAllRoom() ?? new List<Room>();
            var countByType = rooms.GroupBy(r => r.RoomTypeID).ToDictionary(g => g.Key, g => g.Count());

            var dt = new DataTable();
            dt.Columns.Add("RoomTypesID", typeof(int));
            dt.Columns.Add("TypeName", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("RoomCount", typeof(int));

            foreach (var t in types)
            {
                int id = t.RoomTypeID;
                dt.Rows.Add(id, t.TypeName ?? "", t.Description ?? "", countByType.ContainsKey(id) ? countByType[id] : 0);
            }
            dgvRoomType.DataSource = dt;

            if (dgvRoomType.Rows.Count > 0)
            {
                dgvRoomType.Rows[0].Selected = true;
                dgvRoomType_CellClick(dgvRoomType, new DataGridViewCellEventArgs(0, 0));
            }
        }

        private void BindRoomTypeInputs()
        {
            txtRoomTypeName.DataBindings.Clear();
            txtDescription.DataBindings.Clear();
            txtRoomTypeName.DataBindings.Add("Text", dgvRoomType.DataSource, "TypeName");
            txtDescription.DataBindings.Add("Text", dgvRoomType.DataSource, "Description");
        }

        private void dgvRoomType_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var drv = (dgvRoomType.Rows[e.RowIndex].DataBoundItem as DataRowView);
            _curRoomTypeId = drv != null ? Convert.ToInt32(drv.Row["RoomTypesID"]) : 0;
            RoomTypeChanged?.Invoke(_curRoomTypeId);
            SetRtButtons(view: !AddRt); // cập nhật trạng thái nút
        }

        private void btnAddRoomType_Click(object sender, EventArgs e)
        {
            txtRoomTypeName.Clear();
            txtDescription.Clear();
            txtRoomTypeName.Focus();
            AddRt = true;
            SetRtButtons(view: false);
        }

        private void btnEditRoomType_Click(object sender, EventArgs e)
        {
            if (_curRoomTypeId <= 0) return;
            AddRt = false;
            SetRtButtons(view: false);
            txtRoomTypeName.Focus();
        }

        private void btnSaveRoomType_Click(object sender, EventArgs e)
        {
            var name = (txtRoomTypeName.Text ?? "").Trim();
            var desc = (txtDescription.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Nhập Tên loại phòng."); txtRoomTypeName.Focus(); return; }
            if (string.IsNullOrWhiteSpace(desc)) { MessageBox.Show("Nhập Mô tả."); txtDescription.Focus(); return; }

            try
            {
                bool ok = AddRt
                    ? _rtSvc.AddRoomType(new RoomTypes { TypeName = name, Description = desc })
                    : _rtSvc.UpdateRoomType(new RoomTypes { RoomTypeID = _curRoomTypeId, TypeName = name, Description = desc });

                if (!ok) { MessageBox.Show("Lưu loại phòng thất bại."); return; }

                MessageBox.Show("Đã lưu loại phòng!");
                AddRt = false;
                LoadRoomTypeGrid();
                LoadRoomTypeCombo();
                SetRtButtons(view: true);
                ShowPricingForSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelRoomType_Click(object sender, EventArgs e)
        {
            AddRt = false;
            LoadRoomTypeGrid();
            SetRtButtons(view: true);
        }

        private void SetRtButtons(bool view)
        {
            btnAddRoomType.Enabled = view;
            btnEditRoomType.Enabled = view && _curRoomTypeId > 0;
            btnSaveRoomType.Enabled = !view;
            btnCancelRoomType.Enabled = !view;

            txtRoomTypeName.ReadOnly = view;
            txtDescription.ReadOnly = view;
        }

        // ===== PRICING =====
        private void LoadRoomTypeCombo()
        {
            var list = _rtSvc.GetAllRoomTypes() ?? new List<RoomTypes>();
            cboRoomType.DataSource = null;
            cboRoomType.DisplayMember = "TypeName";
            cboRoomType.ValueMember = "RoomTypeID";
            cboRoomType.DataSource = list;
            if (list.Count > 0) cboRoomType.SelectedIndex = 0; else cboRoomType.SelectedIndex = -1;
        }

        private void LoadPricingTypeCombo()
        {
            var types = _prSvc.GetPricingTypes() ?? new List<string>();
            cboPricingType.DataSource = types;
            if (types.Count > 0) cboPricingType.SelectedIndex = 0; else cboPricingType.SelectedIndex = -1;
        }

        private void ShowPricingForSelection()
        {
            if (AddPr) return;

            int rtId = (cboRoomType.SelectedValue is int v) ? v : 0;
            string ptype = cboPricingType.SelectedItem as string;
            if (rtId <= 0 || string.IsNullOrWhiteSpace(ptype))
            {
                _curPricingId = 0;
                txtPrice.Clear();
                chkActive.Checked = false;
                SetPrButtons(view: true);
                return;
            }

            var p = _prSvc.GetByRoomTypeAndType(rtId, ptype);
            if (p == null)
            {
                _curPricingId = 0;
                txtPrice.Clear();
                chkActive.Checked = false;
            }
            else
            {
                _curPricingId = p.PricingID;
                txtPrice.Text = p.Price.ToString("0.##");
                chkActive.Checked = p.IsActive;
            }
            SetPrButtons(view: true);
        }

        private void btnAddPricing_Click(object sender, EventArgs e)
        {
            AddPr = true;
            txtPrice.Clear();
            chkActive.Checked = true;
            SetPrButtons(view: false);
            txtPrice.Focus();
        }

        private void btnEditPricing_Click(object sender, EventArgs e)
        {
            if (_curPricingId <= 0) { MessageBox.Show("Chọn tổ hợp đã có giá để sửa."); return; }
            AddPr = false;
            SetPrButtons(view: false);
            txtPrice.Focus();
        }

        private void btnSavaPricing_Click(object sender, EventArgs e)
        {
            try
            {
                int rtId = (cboRoomType.SelectedValue is int v) ? v : 0;
                string ptype = cboPricingType.SelectedItem as string ?? "";
                decimal price = 0; decimal.TryParse((txtPrice.Text ?? "").Trim(), out price);

                bool ok = AddPr
                    ? _prSvc.Add(new RoomPricing { RoomTypeID = rtId, PricingType = ptype, Price = price, IsActive = chkActive.Checked })
                    : _prSvc.Update(new RoomPricing { PricingID = _curPricingId, RoomTypeID = rtId, PricingType = ptype, Price = price, IsActive = chkActive.Checked });

                if (!ok) { MessageBox.Show("Lưu giá thất bại."); return; }

                MessageBox.Show("Đã lưu giá!");
                AddPr = false;
                SetPrButtons(view: true);
                ShowPricingForSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelpricing_Click(object sender, EventArgs e)
        {
            AddPr = false;
            SetPrButtons(view: true);
            ShowPricingForSelection();
        }

        private void SetPrButtons(bool view)
        {
            btnAddPricing.Enabled = view;
            btnEditPricing.Enabled = view && _curPricingId > 0;
            btnSavaPricing.Enabled = !view;
            btnCancelpricing.Enabled = !view;
            cboRoomType.Enabled = true;
            cboPricingType.Enabled = true;
            txtPrice.ReadOnly = view;
            chkActive.Enabled = !view;
        }
    }
}
