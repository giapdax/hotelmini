using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmService : Form
    {
        private readonly ServicesService _servicesService = new ServicesService();
        private BindingList<Service> _bindingListServices;
        private BindingSource _bindingSource = new BindingSource();

        private Service _current = new Service();
        private bool _isAdd = false;

        public frmService()
        {
            InitializeComponent();
            this.datagridViewService.CellFormatting += datagridViewService_CellFormatting;
            this.datagridViewService.CellClick += datagridViewService_CellClick;
        }

        private void frmService_Load(object sender, EventArgs e)
        {
            SetupGrid();
            ReloadAll();
            ResetUI();
        }


        private void SetupGrid()
        {
            datagridViewService.AutoGenerateColumns = false;
            datagridViewService.DataSource = _bindingSource;
            datagridViewService.Columns.Clear();

            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ServiceID",
                Visible = false
            });
            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ServiceName",
                HeaderText = "Tên Dịch Vụ",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Giá",
                DefaultCellStyle = { Format = "N0" }
            });
            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Số Lượng Tồn",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader
            });
            datagridViewService.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsActive",
                HeaderText = "Hoạt Động"
            });
        }

        private void ReloadAll()
        {
            var list = _servicesService.GetAllServices();
            if (_bindingListServices == null)
            {
                _bindingListServices = new BindingList<Service>(list.ToList());
                _bindingSource.DataSource = _bindingListServices;
            }
            else
            {
                _bindingListServices.Clear();
                foreach (var s in list) _bindingListServices.Add(s);
            }

            PopulateComboBox();
        }

        private void PopulateComboBox()
        {
            var services = _servicesService.GetAllServices();

            cboServiceName.DataSource = services;
            cboServiceName.DisplayMember = "ServiceName";
            cboServiceName.ValueMember = "ServiceID";
            cboServiceName.SelectedIndex = -1;

            var ac = new AutoCompleteStringCollection();
            foreach (var s in services) ac.Add(s.ServiceName);
            cboServiceName.AutoCompleteCustomSource = ac;
        }

        private void ResetUI()
        {
            SetInputsEnabled(false);

            btnAddService.Enabled = true;
            btnEditService.Enabled = true;
            btnDeleteService.Enabled = true;
            btnSave.Enabled = true;

            datagridViewService.ReadOnly = true;
            datagridViewService.ClearSelection();
        }

        private void SetInputsEnabled(bool on)
        {
            txtServiceName.Enabled = on;
            txtPrice.Enabled = on;
            txtQuantity.Enabled = on;
            chkIsActive.Enabled = on;
        }

        private void ClearInputs()
        {
            txtServiceName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            chkIsActive.Checked = false;
        }

        private void ReadInputsToCurrent()
        {
            decimal.TryParse(txtPrice.Text?.Trim(), out var p);
            int.TryParse(txtQuantity.Text?.Trim(), out var q);

            _current.ServiceName = txtServiceName.Text?.Trim();
            _current.Price = p;
            _current.Quantity = q;
            _current.IsActive = chkIsActive.Checked;
        }

        private void WriteCurrentToInputs()
        {
            txtServiceName.Text = _current?.ServiceName ?? "";
            txtPrice.Text = (_current?.Price ?? 0m).ToString();
            txtQuantity.Text = (_current?.Quantity ?? 0).ToString();
            chkIsActive.Checked = _current?.IsActive ?? false;
        }


        private void datagridViewService_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (datagridViewService.Columns[e.ColumnIndex].DataPropertyName == "Price"
                && e.Value is decimal price)
            {
                e.Value = $"{price:N0} VNĐ";
                e.FormattingApplied = true;
            }
        }

        private void datagridViewService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || datagridViewService.Rows[e.RowIndex].IsNewRow) return;

            if (datagridViewService.Rows[e.RowIndex].DataBoundItem is Service s)
            {
                _current = new Service
                {
                    ServiceID = s.ServiceID,
                    ServiceName = s.ServiceName,
                    Price = s.Price,
                    Quantity = s.Quantity,
                    IsActive = s.IsActive
                };
                WriteCurrentToInputs();
            }
        }

        private void txtSearchService_TextChanged(object sender, EventArgs e)
        {
            var key = txtSearchService.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(key))
            {
                _bindingSource.DataSource = _bindingListServices;
            }
            else
            {
                var filtered = _bindingListServices
                    .Where(s => (s.ServiceName ?? "").ToLower().Contains(key)
                             || s.Price.ToString().Contains(key)
                             || s.Quantity.ToString().Contains(key))
                    .ToList();

                _bindingSource.DataSource = new BindingList<Service>(filtered);
            }
        }


        private void btnAddService_Click(object sender, EventArgs e)
        {
            _isAdd = true;
            _current = new Service();
            ClearInputs();
            SetInputsEnabled(true);
            txtServiceName.Focus();

            btnEditService.Enabled = false;
            btnDeleteService.Enabled = false;
        }

        private void btnEditService_Click(object sender, EventArgs e)
        {
            if (_current == null || _current.ServiceID <= 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để sửa.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _isAdd = false;
            SetInputsEnabled(true);
            txtServiceName.Focus();

            btnAddService.Enabled = false;
            btnDeleteService.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ReadInputsToCurrent();

                bool ok = _isAdd
                    ? _servicesService.AddService(_current)
                    : _servicesService.UpdateService(_current);

                if (ok)
                {
                    ReloadAll();
                    MessageBox.Show(_isAdd ? "Thêm dịch vụ thành công!" : "Cập nhật thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    _isAdd = false;
                    ResetUI();
                }
                else
                {
                    MessageBox.Show("Thao tác thất bại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex) 
            {
                MessageBox.Show(ex.Message, "Dữ liệu chưa hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReloadAll();
            ResetUI();
            ClearInputs();
            _isAdd = false;
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            if (_current == null || _current.ServiceID <= 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ để xóa.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc muốn xóa dịch vụ này?",
                "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (confirm == DialogResult.OK)
            {
                try
                {
                    var ok = _servicesService.DeleteService(_current.ServiceID);
                    if (ok)
                    {
                        ReloadAll();
                        ClearInputs();
                        _current = new Service();
                        MessageBox.Show("Xóa thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại.", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ===== Inventory =====

        private void btnSaveInventory_Click_1(object sender, EventArgs e)
        {
            var selected = cboServiceName.SelectedItem as Service;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần nhập kho.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtQuantityAdd.Text?.Trim(), out var add) || add <= 0)
            {
                MessageBox.Show("SL nhập phải là số nguyên dương.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy lại số lượng hiện tại từ DS (đảm bảo mới nhất)
            var current = _servicesService.GetAllServices()
                                          .FirstOrDefault(x => x.ServiceID == selected.ServiceID);
            var curQty = current?.Quantity ?? selected.Quantity;
            var newQty = curQty + add;

            try
            {
                if (_servicesService.UpdateServiceQuantity(selected.ServiceID, newQty))
                {
                    MessageBox.Show($"Đã cập nhật số lượng cho '{selected.ServiceName}'.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadAll();
                    txtQuantityAdd.Clear();
                    cboServiceName.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Cập nhật tồn kho thất bại.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Dữ liệu chưa hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboServiceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantityAdd.Clear();
        }
    }
}
