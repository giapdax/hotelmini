using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;   // RoomPricing
using MiniHotel.Models;          // RoomTypes

namespace HOTEL_MINI.Forms
{
    public partial class UcRoomType_Pricing : UserControl
    {
        private readonly RoomService _roomService;
        private readonly RoomTypeService _roomTypeSvc;
        private readonly RoomPricingService _pricingSvc;

        private readonly BindingSource _bsRoomType = new BindingSource();

        private enum RtMode { None, Adding, Editing }
        private RtMode _mode = RtMode.None;
        private int _currentRoomTypeId = 0;

        private enum PrMode { None, Adding, Editing }
        private PrMode _prMode = PrMode.None;
        private int _currentPricingId = 0;

        // đơn giản: bắn ra RoomTypeID, form mẹ bắt để sync tab Phòng
        public event Action<int> RoomTypeChanged;

        public UcRoomType_Pricing(RoomService roomService, RoomTypeService roomTypeSvc, RoomPricingService pricingSvc)
        {
            InitializeComponent();
            _roomService = roomService;
            _roomTypeSvc = roomTypeSvc;
            _pricingSvc = pricingSvc;

            Load += Uc_Load;

            // ROOM TYPE
            btnAddRoomType.Click += btnAddRoomType_Click;
            btnEditRoomType.Click += btnEditRoomType_Click;
            btnSaveRoomType.Click += btnSaveRoomType_Click;
            btnCancelRoomType.Click += (s, e) => ResetRtForm();

            // PRICING
            btnAddPricing.Click += btnAddPricing_Click;
            btnEditPricing.Click += btnEditPricing_Click;
            btnSavaPricing.Click += btnSavaPricing_Click;
            btnCancelpricing.Click += (s, e) => ResetPricingForm();

            cboRoomType.SelectedIndexChanged += (s, e) => OnPricingSelectionChanged();
            cboPricingType.SelectedIndexChanged += (s, e) => OnPricingSelectionChanged();
        }

        private void Uc_Load(object sender, EventArgs e)
        {
            SetupDgvRoomType();
            dgvRoomType.DataSource = _bsRoomType;

            LoadRoomTypeSummary();
            BindRoomTypeTextboxes();

            LoadPricingCombos();

            SetRtMode(RtMode.None);
            SetPrMode(PrMode.None);

            OnPricingSelectionChanged();
        }

        // ===== ROOM TYPE GRID =====
        private void SetupDgvRoomType()
        {
            dgvRoomType.AutoGenerateColumns = false;
            dgvRoomType.Columns.Clear();
            dgvRoomType.Columns.Add(new DataGridViewTextBoxColumn { Name = "RoomTypesID", DataPropertyName = "RoomTypesID", HeaderText = "ID", Visible = false });
            dgvRoomType.Columns.Add(new DataGridViewTextBoxColumn { Name = "TypeName", DataPropertyName = "TypeName", HeaderText = "Loại phòng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvRoomType.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", DataPropertyName = "Description", HeaderText = "Mô tả", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvRoomType.Columns.Add(new DataGridViewTextBoxColumn { Name = "RoomCount", DataPropertyName = "RoomCount", HeaderText = "Số lượng", Width = 120 });

            dgvRoomType.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoomType.MultiSelect = false;
            dgvRoomType.SelectionChanged += (s, e) => UpdateCurrentRoomTypeId();
        }

        private void LoadRoomTypeSummary()
        {
            var types = _roomTypeSvc.GetAllRoomTypes();               // List<RoomTypes>
            var rooms = _roomService.getAllRoom();                    // List<Room> (giả định có RoomTypeID)

            var countByType = rooms.GroupBy(r => r.RoomTypeID)
                                   .ToDictionary(g => g.Key, g => g.Count());

            var dt = new DataTable();
            dt.Columns.Add("RoomTypesID", typeof(int));
            dt.Columns.Add("TypeName", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("RoomCount", typeof(int));

            foreach (var t in types)
            {
                int id = t.RoomTypesID;
                int count = countByType.ContainsKey(id) ? countByType[id] : 0;
                dt.Rows.Add(id, t.TypeName ?? "", t.Description ?? "", count);
            }

            _bsRoomType.DataSource = dt;
            UpdateCurrentRoomTypeId();
        }

        private void BindRoomTypeTextboxes()
        {
            txtRoomTypeName.DataBindings.Clear();
            txtDescription.DataBindings.Clear();
            txtRoomTypeName.DataBindings.Add("Text", _bsRoomType, "TypeName", true, DataSourceUpdateMode.Never);
            txtDescription.DataBindings.Add("Text", _bsRoomType, "Description", true, DataSourceUpdateMode.Never);
            _bsRoomType.CurrentChanged += (s, e) => UpdateCurrentRoomTypeId();
        }

        private void UpdateCurrentRoomTypeId()
        {
            _currentRoomTypeId = 0;
            if (_bsRoomType.Current is DataRowView drv)
                _currentRoomTypeId = Convert.ToInt32(drv["RoomTypesID"]);
        }

        private void SetRtMode(RtMode mode)
        {
            _mode = mode;
            btnAddRoomType.Enabled = mode == RtMode.None;
            btnEditRoomType.Enabled = mode == RtMode.None && dgvRoomType.Rows.Count > 0;
            btnSaveRoomType.Enabled = mode != RtMode.None;
            btnCancelRoomType.Enabled = mode != RtMode.None;

            txtRoomTypeName.ReadOnly = mode == RtMode.None;
            txtDescription.ReadOnly = mode == RtMode.None;
            if (mode == RtMode.Editing) UpdateCurrentRoomTypeId();
        }

        private void ClearRtInputs()
        {
            _currentRoomTypeId = 0;
            txtRoomTypeName.Text = "";
            txtDescription.Text = "";
        }

        private void ResetRtForm()
        {
            ClearRtInputs();
            SetRtMode(RtMode.None);
            _bsRoomType.ResetBindings(false);
        }

        private void btnAddRoomType_Click(object sender, EventArgs e)
        {
            ClearRtInputs();
            SetRtMode(RtMode.Adding);
            txtRoomTypeName.Focus();
        }

        private void btnEditRoomType_Click(object sender, EventArgs e)
        {
            if (_bsRoomType.Count == 0 || _currentRoomTypeId <= 0)
            { MessageBox.Show("Chọn một loại phòng để sửa."); return; }
            SetRtMode(RtMode.Editing);
            txtRoomTypeName.Focus();
        }

        private void btnSaveRoomType_Click(object sender, EventArgs e)
        {
            try
            {
                var name = (txtRoomTypeName.Text ?? "").Trim();
                var desc = (txtDescription.Text ?? "").Trim();
                if (string.IsNullOrWhiteSpace(name)) { MessageBox.Show("Nhập Tên loại phòng."); txtRoomTypeName.Focus(); return; }
                if (string.IsNullOrWhiteSpace(desc)) { MessageBox.Show("Nhập Mô tả."); txtDescription.Focus(); return; }

                if (_mode == RtMode.Adding)
                {
                    var model = new RoomTypes { TypeName = name, Description = desc };
                    if (_roomTypeSvc.AddRoomType(model)) { MessageBox.Show("Thêm loại phòng thành công!"); ResetRtForm(); LoadRoomTypeSummary(); }
                    else MessageBox.Show("Không thêm được loại phòng.");
                }
                else if (_mode == RtMode.Editing)
                {
                    if (_currentRoomTypeId <= 0) { MessageBox.Show("Không xác định ID loại phòng."); return; }
                    var model = new RoomTypes { RoomTypesID = _currentRoomTypeId, TypeName = name, Description = desc };
                    if (_roomTypeSvc.UpdateRoomType(model)) { MessageBox.Show("Cập nhật loại phòng thành công!"); ResetRtForm(); LoadRoomTypeSummary(); }
                    else MessageBox.Show("Không cập nhật được loại phòng.");
                }
                else MessageBox.Show("Chưa chọn chế độ Thêm/Sửa.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== PRICING =====
        private void LoadPricingCombos()
        {
            var roomTypes = _roomTypeSvc.GetAllRoomTypes();
            cboRoomType.DataSource = roomTypes;
            cboRoomType.DisplayMember = "TypeName";
            cboRoomType.ValueMember = "RoomTypesID";
            cboRoomType.SelectedIndex = roomTypes.Count > 0 ? 0 : -1;

            var pricingTypes = _pricingSvc.GetPricingTypes(); // List<string>
            cboPricingType.DataSource = pricingTypes;
            cboPricingType.SelectedIndex = pricingTypes.Count > 0 ? 0 : -1;

            txtPrice.Text = "";
            chkActive.Checked = true;
        }

        private void UpdatePricingEditState()
        {
            if (_prMode != PrMode.None) return;
            btnEditPricing.Enabled = _currentPricingId > 0;
            txtPrice.ReadOnly = true;
            chkActive.Enabled = false;
        }

        private void OnPricingSelectionChanged()
        {
            if (_prMode != PrMode.None) return;

            int roomTypeId = (cboRoomType.SelectedValue is int v) ? v : 0;
            string pricingType = cboPricingType.SelectedItem as string;

            if (roomTypeId <= 0 || string.IsNullOrWhiteSpace(pricingType))
            {
                _currentPricingId = 0;
                txtPrice.Text = "";
                chkActive.Checked = false;
                UpdatePricingEditState();
                return;
            }

            var pricing = _pricingSvc.GetByRoomTypeAndType(roomTypeId, pricingType);
            if (pricing == null)
            {
                _currentPricingId = 0;
                txtPrice.Text = "Chưa có giá cho tổ hợp này";
                chkActive.Checked = false;
            }
            else
            {
                _currentPricingId = pricing.PricingID;
                txtPrice.Text = pricing.Price.ToString("0.##");
                chkActive.Checked = pricing.IsActive;

                // báo cho tab Phòng (nếu cần sync)
                RoomTypeChanged?.Invoke(roomTypeId);
            }

            UpdatePricingEditState();
        }

        private RoomPricing ReadPricingFromForm(int? pricingId = null)
        {
            int roomTypeId = (cboRoomType.SelectedValue is int v) ? v : 0;
            string pricingType = cboPricingType.SelectedItem as string ?? "";
            decimal price = 0;
            decimal.TryParse(txtPrice.Text.Trim(), out price);

            return new RoomPricing
            {
                PricingID = pricingId ?? 0,
                RoomTypeID = roomTypeId,
                PricingType = pricingType,
                Price = price,
                IsActive = chkActive.Checked
            };
        }

        private void SetPrMode(PrMode mode)
        {
            _prMode = mode;
            btnAddPricing.Enabled = mode == PrMode.None;
            btnSavaPricing.Enabled = mode != PrMode.None;
            btnCancelpricing.Enabled = mode != PrMode.None;

            cboRoomType.Enabled = true;
            cboPricingType.Enabled = true;

            if (mode == PrMode.None)
            {
                btnEditPricing.Enabled = _currentPricingId > 0;
                txtPrice.ReadOnly = true;
                chkActive.Enabled = false;
            }
            else
            {
                btnEditPricing.Enabled = false;
                txtPrice.ReadOnly = false;
                chkActive.Enabled = true;
            }
        }

        private void ResetPricingForm()
        {
            _currentPricingId = 0;
            txtPrice.Text = "";
            chkActive.Checked = true;
            SetPrMode(PrMode.None);
            OnPricingSelectionChanged();
        }

        private void btnAddPricing_Click(object sender, EventArgs e)
        {
            SetPrMode(PrMode.Adding);
            if (!decimal.TryParse(txtPrice.Text, out _)) txtPrice.Text = "";
            cboRoomType.Focus();
        }

        private void btnEditPricing_Click(object sender, EventArgs e)
        {
            if (_currentPricingId <= 0) { MessageBox.Show("Chọn tổ hợp đã có giá để sửa."); return; }
            SetPrMode(PrMode.Editing);
            if (!decimal.TryParse(txtPrice.Text, out _)) txtPrice.Text = "";
            txtPrice.Focus();
        }

        private void btnSavaPricing_Click(object sender, EventArgs e)
        {
            try
            {
                if (_prMode == PrMode.Adding)
                {
                    var model = ReadPricingFromForm();
                    if (_pricingSvc.Add(model)) { MessageBox.Show("Thêm loại giá thành công!"); ResetPricingForm(); }
                    else MessageBox.Show("Không thêm được loại giá.");
                }
                else if (_prMode == PrMode.Editing)
                {
                    if (_currentPricingId <= 0) { MessageBox.Show("Không xác định bản ghi giá."); return; }
                    var model = ReadPricingFromForm(_currentPricingId);
                    if (_pricingSvc.Update(model)) { MessageBox.Show("Cập nhật loại giá thành công!"); ResetPricingForm(); }
                    else MessageBox.Show("Không cập nhật được loại giá.");
                }
                else MessageBox.Show("Chưa chọn chế độ Thêm/Sửa.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
