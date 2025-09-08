using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmService : Form
    {
        // Enum để quản lý trạng thái của form
        private enum FormState { Viewing, Adding, Editing }
        private FormState _currentState;

        private readonly ServicesService _servicesService;
        private BindingList<Service> _bindingListServices;
        private BindingSource _bindingSource;
        private decimal price;

        public frmService()
        {
            InitializeComponent();
            _servicesService = new ServicesService();
            _bindingSource = new BindingSource();

            // Chỉ gán những event mà Designer KHÔNG gán
            this.datagridViewService.CellClick += datagridViewService_CellClick;
            this.datagridViewService.CellFormatting += datagridViewService_CellFormatting;

            // Không gán Load, Click, TextChanged… ở đây nữa vì Designer đã gán
            SetFormState(FormState.Viewing);
        }


        private void frmService_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadAllServices();
            PopulateComboBox();
        }

        // Hàm này thiết lập trạng thái của form và điều chỉnh hiển thị/enable của controls
        private void SetFormState(FormState newState)
        {
            _currentState = newState;

            bool enableInput = false;
            bool enableGridAndSearch = false;

            btnAddService.Visible = _currentState == FormState.Viewing;
            btnEditService.Visible = _currentState == FormState.Viewing;
            btnDeleteService.Visible = _currentState == FormState.Viewing;

            btnSave.Visible = _currentState == FormState.Adding || _currentState == FormState.Editing;
            btnCancel.Visible = _currentState == FormState.Adding || _currentState == FormState.Editing;

            datagridViewService.Enabled = _currentState == FormState.Viewing;
            txtSearchService.Enabled = _currentState == FormState.Viewing;

            // Bật/Tắt các control của phần nhập liệu chính
            switch (newState)
            {
                case FormState.Viewing:
                    enableInput = false;
                    enableGridAndSearch = true;
                    datagridViewService.ClearSelection();
                    break;
                case FormState.Adding:
                    enableInput = true;
                    enableGridAndSearch = false;
                    ClearFields();
                    break;
                case FormState.Editing:
                    enableInput = true;
                    enableGridAndSearch = false;
                    break;
            }

            // Áp dụng thuộc tính Enabled cho các control nhập liệu
            txtServiceName.Enabled = enableInput;
            txtPrice.Enabled = enableInput;
            chkIsActive.Enabled = enableInput;
            txtQuantity.Enabled = enableInput;
        }

        private void SetupDataGridView()
        {
            datagridViewService.AutoGenerateColumns = false;
            datagridViewService.DataSource = _bindingSource;
            datagridViewService.Columns.Clear();
            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServiceID", Visible = false });
            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServiceName", HeaderText = "Tên Dịch Vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Giá", DefaultCellStyle = { Format = "N0" } });
            // Thêm cột Quantity để hiển thị số lượng tồn kho
            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Số Lượng Tồn", AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader });
            datagridViewService.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "IsActive", HeaderText = "Hoạt Động" });
        }

        private void LoadAllServices()
        {
            var services = _servicesService.GetAllServices();

            if (_bindingListServices == null)
            {
                _bindingListServices = new BindingList<Service>(services.ToList());
                _bindingSource = new BindingSource(_bindingListServices, null);
                datagridViewService.DataSource = _bindingSource;
            }
            else
            {
                // ✅ Cập nhật lại list thay vì tạo mới
                _bindingListServices.Clear();
                foreach (var s in services)
                {
                    _bindingListServices.Add(s);
                }
            }
        }


        private void PopulateComboBox()
        {
            var services = _servicesService.GetAllServices();

            cboServiceName.DataSource = services;
            cboServiceName.DisplayMember = "ServiceName";
            cboServiceName.ValueMember = "ServiceID";
            cboServiceName.SelectedIndex = -1;

            // Tạo danh sách autocomplete
            AutoCompleteStringCollection autoCompleteData = new AutoCompleteStringCollection();
            foreach (var s in services)
            {
                autoCompleteData.Add(s.ServiceName);
            }
            cboServiceName.AutoCompleteCustomSource = autoCompleteData;
        }


        private void datagridViewService_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (datagridViewService.Columns[e.ColumnIndex].DataPropertyName == "Price")
            {
                if (e.Value is decimal price)
                {
                    e.Value = $"{price:N0} VNĐ";
                    e.FormattingApplied = true;
                }
            }
        }

        private void datagridViewService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_currentState != FormState.Viewing)
            {
                return;
            }

            if (e.RowIndex < 0 || datagridViewService.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }

            if (datagridViewService.Rows[e.RowIndex].DataBoundItem is Service selectedService)
            {
                txtServiceName.Text = selectedService.ServiceName;
                txtPrice.Text = selectedService.Price.ToString();
                txtQuantity.Text = selectedService.Quantity.ToString(); // Thêm dòng này để hiển thị số lượng
                chkIsActive.Checked = selectedService.IsActive;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtServiceName.Text))
            {
                MessageBox.Show("Tên dịch vụ không được để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtServiceName.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPrice.Text, out price) || price <= 0)
            {
                MessageBox.Show("Giá phải là một số dương hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Số lượng phải là một số nguyên không âm hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateInventoryInput()
        {
            if (cboServiceName.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboServiceName.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantityAdd.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Số lượng thêm phải là một số nguyên dương hợp lệ.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantityAdd.Focus();
                return false;
            }
            return true;
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            SetFormState(FormState.Adding);
            txtServiceName.Focus();
        }

        private void btnEditService_Click(object sender, EventArgs e)
        {
            if (!(_bindingSource.Current is Service selectedService))
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetFormState(FormState.Editing);
            txtServiceName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            if (_currentState == FormState.Adding)
            {
                HandleAddService();
            }
            else if (_currentState == FormState.Editing)
            {
                HandleEditService();
            }
        }

        private void HandleAddService()
        {
            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                quantity = 0; // Gán mặc định nếu không hợp lệ
            }

            var newService = new Service
            {
                ServiceName = txtServiceName.Text,
                Price = price,
                IsActive = chkIsActive.Checked,
                Quantity = quantity // Thêm Quantity
            };

            if (_servicesService.AddService(newService))
            {
                LoadAllServices();
                PopulateComboBox(); // Cập nhật ComboBox
                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetFormState(FormState.Viewing);
            }
            else
            {
                MessageBox.Show("Thêm dịch vụ thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleEditService()
        {
            if (!(_bindingSource.Current is Service selectedService))
            {
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                quantity = selectedService.Quantity; // Giữ lại giá trị cũ nếu không hợp lệ
            }

            selectedService.ServiceName = txtServiceName.Text;
            selectedService.Price = price;
            selectedService.IsActive = chkIsActive.Checked;
            selectedService.Quantity = quantity; // Cập nhật Quantity

            if (_servicesService.UpdateService(selectedService))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _bindingSource.ResetBindings(false);
                SetFormState(FormState.Viewing);
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoadAllServices();
            SetFormState(FormState.Viewing);
            ClearFields();
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            if (!(_bindingSource.Current is Service selectedService))
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để xóa.",
                                "Thông báo",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc muốn xóa dịch vụ này?",
                                          "Xác nhận",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                bool isDeleted = _servicesService.DeleteService(selectedService.ServiceID);

                if (isDeleted)
                {
                    // ✅ Không RemoveCurrent nữa, thay bằng reload từ DB
                    LoadAllServices();
                    PopulateComboBox();

                    MessageBox.Show("Xóa thành công!",
                                    "Thông báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    ClearFields(); // reset form
                }
                else
                {
                    MessageBox.Show("Xóa thất bại. Vui lòng thử lại.",
                                    "Lỗi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }



        private void txtSearchService_TextChanged(object sender, EventArgs e)
        {
            // Lấy chuỗi tìm kiếm và chuyển về chữ thường để tìm kiếm không phân biệt chữ hoa, chữ thường
            string searchText = txtSearchService.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                // Nếu ô tìm kiếm trống, hiển thị lại toàn bộ danh sách dịch vụ
                _bindingSource.DataSource = _bindingListServices;
            }
            else
            {
                // Lọc danh sách dịch vụ bằng LINQ
                var filteredServices = _bindingListServices
                    .Where(s => s.ServiceName.ToLower().Contains(searchText) ||
                                s.Price.ToString().Contains(searchText) ||
                                s.Quantity.ToString().Contains(searchText))
                    .ToList();

                // Cập nhật DataSource của BindingSource bằng danh sách đã lọc
                _bindingSource.DataSource = new BindingList<Service>(filteredServices);
            }
        }

        private void ClearFields()
        {
            txtServiceName.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
            chkIsActive.Checked = false;
            datagridViewService.ClearSelection();
            txtServiceName.Focus();
        }

        private void btnClearService_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void chkIsActive_CheckedChanged(object sender, EventArgs e)
        {
            // Không có logic nào cần thêm ở đây
        }

        // Cập nhật số lượng tồn kho
        private void btnSaveInventory_Click_1(object sender, EventArgs e)
        {
            if (!ValidateInventoryInput())
            {
                return;
            }

            // Lấy ServiceID của dịch vụ được chọn từ ComboBox
            var selectedService = cboServiceName.SelectedItem as Service;
            if (selectedService == null)
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int serviceId = selectedService.ServiceID;

            // Lấy số lượng cần thêm
            if (!int.TryParse(txtQuantityAdd.Text, out int quantityToAdd) || quantityToAdd <= 0)
            {
                MessageBox.Show("Số lượng thêm phải là một số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantityAdd.Focus();
                return;
            }

            // Lấy số lượng hiện tại
            int currentQuantity = selectedService.Quantity;
            int newQuantity = currentQuantity + quantityToAdd;

            try
            {
                if (_servicesService.UpdateServiceQuantity(serviceId, newQuantity))
                {
                    MessageBox.Show($"Đã cập nhật số lượng tồn kho cho dịch vụ '{selectedService.ServiceName}' thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Cập nhật lại giao diện
                    LoadAllServices();
                    PopulateComboBox();
                    txtQuantityAdd.Clear();
                    cboServiceName.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Cập nhật số lượng tồn kho thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Khi chọn một dịch vụ trong ComboBox, xóa số lượng đã nhập
        private void cboServiceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtQuantityAdd.Clear();
        }
        private void cboServiceName_TextChanged_Filter(object sender, EventArgs e)
        {
            string searchText = cboServiceName.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                // Load lại toàn bộ danh sách khi ô trống
                cboServiceName.DataSource = _servicesService.GetAllServices();
            }
            else
            {
                var filteredServices = _servicesService.GetAllServices()
                                        .FindAll(s => s.ServiceName.ToLower().Contains(searchText));

                cboServiceName.DataSource = filteredServices;
                cboServiceName.DisplayMember = "ServiceName";
                cboServiceName.ValueMember = "ServiceID";

                // Giữ con trỏ nhập
                cboServiceName.DroppedDown = true;
                cboServiceName.IntegralHeight = true;
                cboServiceName.SelectedIndex = -1;
                cboServiceName.Text = searchText;
                cboServiceName.SelectionStart = cboServiceName.Text.Length;
            }
        }

    }
}