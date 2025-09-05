using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.ComponentModel;
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

            // Gán sự kiện
            this.Load += frmService_Load;
            this.datagridViewService.CellClick += datagridViewService_CellClick;
            this.txtSearchService.TextChanged += txtSearchService_TextChanged;
            this.datagridViewService.CellFormatting += datagridViewService_CellFormatting;

            // Gán sự kiện cho các nút chức năng mới
            this.btnAddService.Click += btnAddService_Click;
            this.btnEditService.Click += btnEditService_Click;
            this.btnSave.Click += btnSave_Click; // Xử lý Lưu
            this.btnCancel.Click += btnCancel_Click; // Xử lý Hủy
            this.btnDeleteService.Click += btnDeleteService_Click; // Giữ nguyên

            // Thiết lập trạng thái ban đầu
            SetFormState(FormState.Viewing);
        }

        private void frmService_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadAllServices();
        }

        // Hàm này thiết lập trạng thái của form và điều chỉnh hiển thị/enable của controls
        private void SetFormState(FormState newState)
        {
            _currentState = newState;

            // Mặc định: Tắt chỉnh sửa trên các control nhập liệu
            bool enableInput = false;
            bool enableGridAndSearch = false;

            // Hiển thị/Ẩn các nút
            btnAddService.Visible = _currentState == FormState.Viewing;
            btnEditService.Visible = _currentState == FormState.Viewing;
            btnDeleteService.Visible = _currentState == FormState.Viewing;

            btnSave.Visible = _currentState == FormState.Adding || _currentState == FormState.Editing;
            btnCancel.Visible = _currentState == FormState.Adding || _currentState == FormState.Editing;

            // Bật/Tắt Grid và Search
            datagridViewService.Enabled = _currentState == FormState.Viewing;
            txtSearchService.Enabled = _currentState == FormState.Viewing;


            switch (newState)
            {
                case FormState.Viewing:
                    // Ở chế độ xem, các TextBox bị tắt, Grid và Search được bật
                    enableInput = false;
                    enableGridAndSearch = true;
                    // Đảm bảo không có dòng nào được chọn khi chuyển về Viewing
                    datagridViewService.ClearSelection();
                    break;
                case FormState.Adding:
                    // Ở chế độ thêm, các TextBox được bật, xóa dữ liệu cũ
                    enableInput = true;
                    enableGridAndSearch = false;
                    ClearFields();
                    break;
                case FormState.Editing:
                    // Ở chế độ sửa, các TextBox được bật
                    enableInput = true;
                    enableGridAndSearch = false;
                    break;
            }

            // Áp dụng thuộc tính Enabled cho các control nhập liệu
            txtServiceName.Enabled = enableInput;
            txtPrice.Enabled = enableInput;
            chkIsActive.Enabled = enableInput;
        }

        // Giữ nguyên các hàm Load, SetupDataGridView, CellFormatting
        private void SetupDataGridView()
        {
            datagridViewService.AutoGenerateColumns = false;
            datagridViewService.DataSource = _bindingSource;
            datagridViewService.Columns.Clear();
            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServiceID", Visible = false });
            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServiceName", HeaderText = "Tên Dịch Vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            datagridViewService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Giá", DefaultCellStyle = { Format = "N0" } });
            datagridViewService.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "IsActive", HeaderText = "Hoạt Động" });
        }

        private void LoadAllServices()
        {
            try
            {
                var allServices = _servicesService.GetAllServices();
                _bindingListServices = new BindingList<Service>(allServices);
                _bindingSource.DataSource = _bindingListServices;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        // Khi click vào Cell, chỉ cho phép hiển thị dữ liệu nếu đang ở chế độ Xem (Viewing)
        private void datagridViewService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_currentState != FormState.Viewing)
            {
                return; // Không làm gì nếu không ở chế độ Xem
            }

            if (e.RowIndex < 0 || datagridViewService.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }

            if (datagridViewService.Rows[e.RowIndex].DataBoundItem is Service selectedService)
            {
                txtServiceName.Text = selectedService.ServiceName;
                // Hiển thị giá tiền gốc (chưa định dạng) để dễ sửa chữa
                txtPrice.Text = selectedService.Price.ToString();
                chkIsActive.Checked = selectedService.IsActive;
            }
        }

        // Hàm kiểm tra và parse dữ liệu đầu vào (Cần thiết cho cả Thêm và Sửa)
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

            return true;
        }

        // Xử lý khi ấn nút THÊM
        private void btnAddService_Click(object sender, EventArgs e)
        {
            SetFormState(FormState.Adding);
            txtServiceName.Focus();
        }

        // Xử lý khi ấn nút SỬA
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

        // Xử lý khi ấn nút LƯU (Chung cho Thêm và Sửa)
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

        // Xử lý logic Thêm
        private void HandleAddService()
        {
            var newService = new Service
            {
                ServiceName = txtServiceName.Text,
                Price = price,
                IsActive = chkIsActive.Checked
            };

            if (_servicesService.AddService(newService))
            {
                // Tải lại để lấy ServiceID mới nhất
                LoadAllServices();
                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SetFormState(FormState.Viewing); // Trở về chế độ Xem
            }
            else
            {
                MessageBox.Show("Thêm dịch vụ thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý logic Sửa
        private void HandleEditService()
        {
            if (!(_bindingSource.Current is Service selectedService))
            {
                return;
            }

            // Cập nhật đối tượng đã chọn
            selectedService.ServiceName = txtServiceName.Text;
            selectedService.Price = price;
            selectedService.IsActive = chkIsActive.Checked;

            if (_servicesService.UpdateService(selectedService))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // BindingSource.ResetBindings(false) giúp DataGridView cập nhật ngay lập tức
                _bindingSource.ResetBindings(false);
                SetFormState(FormState.Viewing); // Trở về chế độ Xem
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Xử lý khi ấn nút HỦY
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Tải lại dữ liệu (tương đương với việc hủy bỏ mọi thay đổi chưa lưu)
            LoadAllServices();
            SetFormState(FormState.Viewing); // Trở về chế độ Xem
            ClearFields(); // Xóa dữ liệu trên các control nhập liệu
        }

        // Xử lý khi ấn nút XÓA (Giữ nguyên)
        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            if (!(_bindingSource.Current is Service selectedService))
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa dịch vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_servicesService.DeleteService(selectedService.ServiceID))
                {
                    _bindingSource.RemoveCurrent();
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Xử lý tìm kiếm (Giữ nguyên)
        private void txtSearchService_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearchService.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                _bindingSource.RemoveFilter();
            }
            else
            {
                string escapedSearchText = searchText.Replace("'", "''");
                // Lọc theo Tên Dịch Vụ hoặc Giá
                _bindingSource.Filter = $"ServiceName LIKE '%{escapedSearchText}%' OR CONVERT(Price, 'System.String') LIKE '%{escapedSearchText}%'";
            }
        }

        // Hàm xóa dữ liệu trên các control nhập liệu (Giữ nguyên)
        private void ClearFields()
        {
            txtServiceName.Clear();
            txtPrice.Clear();
            chkIsActive.Checked = false;
            datagridViewService.ClearSelection();
            txtServiceName.Focus();
        }

        // Xử lý khi ấn nút Xóa dữ liệu (Giữ nguyên)
        private void btnClearService_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

    }
}