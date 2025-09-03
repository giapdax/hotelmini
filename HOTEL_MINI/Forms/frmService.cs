// using ...
using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.ComponentModel;
using System.Windows.Forms; // Cần cho BindingList

namespace HOTEL_MINI.Forms
{
    public partial class frmService : Form
    {
        private readonly ServicesService _servicesService;
        // Sử dụng BindingList để DataGridView tự động cập nhật
        private BindingList<Service> _bindingListServices;
        private BindingSource _bindingSource;
        private decimal price;

        public frmService()
        {
            InitializeComponent();
            _servicesService = new ServicesService();
            _bindingSource = new BindingSource();

            // Gán sự kiện một lần trong constructor
            this.Load += frmService_Load;
            this.datagridViewService.CellClick += datagridViewService_CellClick;
            this.txtSearchService.TextChanged += txtSearchService_TextChanged;


            // Sự kiện để định dạng giá tiền
            this.datagridViewService.CellFormatting += datagridViewService_CellFormatting;
        }

        private void frmService_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            LoadAllServices();
        }

        private void SetupDataGridView()
        {
            datagridViewService.AutoGenerateColumns = false; // Tắt tự động tạo cột để kiểm soát hoàn toàn
            datagridViewService.DataSource = _bindingSource;

            // Định nghĩa các cột thủ công
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

        private void datagridViewService_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu người dùng click vào header của bảng (e.RowIndex == -1)
            // Hoặc click vào dòng trống dùng để thêm mới (IsNewRow)
            if (e.RowIndex < 0 || datagridViewService.Rows[e.RowIndex].IsNewRow)
            {
                return;
            }

            // Lấy đối tượng Service trực tiếp từ dòng đã được click.
            // Đây là cách làm chính xác và ổn định nhất.
            if (datagridViewService.Rows[e.RowIndex].DataBoundItem is Service selectedService)
            {
                txtServiceName.Text = selectedService.ServiceName;
                txtPrice.Text = selectedService.Price.ToString();
                chkIsActive.Checked = selectedService.IsActive;
            }
        }

        // Sự kiện định dạng lại cách hiển thị của cột giá tiền
        private void datagridViewService_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Kiểm tra nếu đây là cột "Price"
            if (datagridViewService.Columns[e.ColumnIndex].DataPropertyName == "Price")
            {
                if (e.Value is decimal price)
                {
                    e.Value = $"{price:N0} VNĐ";
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnAddService_Click(object sender, EventArgs e)
        {
            // ... (Phần kiểm tra dữ liệu đầu vào giữ nguyên)

            var newService = new Service
            {
                ServiceName = txtServiceName.Text,
                Price = price, // Biến price đã được parse ở trên
                IsActive = chkIsActive.Checked
            };

            if (_servicesService.AddService(newService))
            {
                // Không cần tải lại toàn bộ! Chỉ cần thêm vào BindingList.
                // Tuy nhiên, ServiceID được tạo bởi database, nên cách tốt nhất vẫn là tải lại.
                // Đây là trường hợp ngoại lệ mà việc tải lại là hợp lý để lấy ID mới.
                LoadAllServices();
                ClearFields();
                MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm dịch vụ thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditService_Click(object sender, EventArgs e)
        {
            if (!(_bindingSource.Current is Service selectedService))
            {
                MessageBox.Show("Vui lòng chọn một dịch vụ để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ... (Phần kiểm tra dữ liệu đầu vào giữ nguyên)

            // Cập nhật đối tượng đã chọn
            selectedService.ServiceName = txtServiceName.Text;
            selectedService.Price = price; // Biến price đã được parse
            selectedService.IsActive = chkIsActive.Checked;

            if (_servicesService.UpdateService(selectedService))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // DataGridView tự động cập nhật! Chỉ cần reset lại binding.
                _bindingSource.ResetBindings(false);
                ClearFields();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                    // Xóa khỏi BindingSource, DataGridView tự động cập nhật
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
                _bindingSource.Filter = $"ServiceName LIKE '%{escapedSearchText}%' OR CONVERT(Price, 'System.String') LIKE '%{escapedSearchText}%'";
            }
        }

        private void ClearFields()
        {
            txtServiceName.Clear();
            txtPrice.Clear();
            chkIsActive.Checked = false;
            datagridViewService.ClearSelection();
            txtServiceName.Focus();
        }

        private void btnClearService_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }
}