using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmCustomer : Form
    {
        private readonly CustomerService _svc = new CustomerService();
        private List<Customer> _all = new List<Customer>();
        private int _currentCustomerId = 0;

        private enum FormMode { View, Edit }
        private FormMode _mode = FormMode.View;

        public frmCustomer()
        {
            InitializeComponent();

            // wire events ngoài các event đã có sẵn trong Designer
            this.Load += FrmCustomer_Load;
            dgvCustomer.SelectionChanged += dgvCustomer_SelectionChanged;
            textBox2.TextChanged += textBox2_TextChanged; // ô search trong group Tìm kiếm
        }

        #region Load + Binding

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            // cột ngày tạo chỉ xem
            txtCreatedAt.ReadOnly = true;

            LoadCustomers();
            SetupGrid();
            SetMode(FormMode.View);
        }

        private void LoadCustomers()
        {
            _all = _svc.getAllCustomers() ?? new List<Customer>();
            dgvCustomer.AutoGenerateColumns = true;
            dgvCustomer.DataSource = _all;

            if (_all.Count > 0)
            {
                dgvCustomer.ClearSelection();
                dgvCustomer.Rows[0].Selected = true;
                ShowSelectedToForm();
            }
            else
            {
                ClearFields();
            }
        }

        private void SetupGrid()
        {
            dgvCustomer.ReadOnly = true;
            dgvCustomer.AllowUserToAddRows = false;
            dgvCustomer.AllowUserToDeleteRows = false;
            dgvCustomer.MultiSelect = false;
            dgvCustomer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvCustomer.DataBindingComplete += (s, e) =>
            {
                if (dgvCustomer.Columns.Contains("CustomerID"))
                    dgvCustomer.Columns["CustomerID"].HeaderText = "ID";

                if (dgvCustomer.Columns.Contains("FullName"))
                    dgvCustomer.Columns["FullName"].HeaderText = "Họ và tên";

                if (dgvCustomer.Columns.Contains("Gender"))
                    dgvCustomer.Columns["Gender"].HeaderText = "Giới tính";

                if (dgvCustomer.Columns.Contains("Phone"))
                    dgvCustomer.Columns["Phone"].HeaderText = "SĐT";

                if (dgvCustomer.Columns.Contains("Email"))
                    dgvCustomer.Columns["Email"].HeaderText = "Email";

                if (dgvCustomer.Columns.Contains("Address"))
                    dgvCustomer.Columns["Address"].HeaderText = "Địa chỉ";

                if (dgvCustomer.Columns.Contains("IDNumber"))
                    dgvCustomer.Columns["IDNumber"].HeaderText = "CCCD";

                if (dgvCustomer.Columns.Contains("CreatedAt"))
                    dgvCustomer.Columns["CreatedAt"].HeaderText = "Ngày tạo";
            };
        }

        #endregion

        #region Mode + Form state

        private void SetMode(FormMode mode)
        {
            _mode = mode;

            bool editable = (_mode == FormMode.Edit);

            // chỉ cho sửa khi Edit; CreatedAt luôn readonly
            txtFullname.ReadOnly = !editable;
            txtGender.ReadOnly = !editable;
            txtPhone.ReadOnly = !editable;
            textBox4.ReadOnly = !editable; // Email textbox
            txtAddress.ReadOnly = !editable;
            txtIDNumber.ReadOnly = !editable;
            txtCreatedAt.ReadOnly = true;

            btnSave.Text = editable ? "Save" : "Edit";
            btnCancel.Text = editable ? "Hủy" : "Hủy";

            dgvCustomer.Enabled = !editable;

            if (!editable)
            {
                // thoát edit -> hiển thị lại dữ liệu đang chọn
                ShowSelectedToForm();
            }
        }

        #endregion

        #region Helpers

        private void ClearFields()
        {
            _currentCustomerId = 0;
            txtFullname.Text = "";
            txtGender.Text = "";
            txtPhone.Text = "";
            textBox4.Text = ""; // email
            txtAddress.Text = "";
            txtIDNumber.Text = "";
            txtCreatedAt.Text = "";
        }

        private void ShowSelectedToForm()
        {
            if (dgvCustomer.SelectedRows.Count == 0)
            {
                ClearFields();
                return;
            }

            var c = dgvCustomer.SelectedRows[0].DataBoundItem as Customer;
            if (c == null) { ClearFields(); return; }

            _currentCustomerId = c.CustomerID;
            txtFullname.Text = c.FullName ?? "";
            txtGender.Text = c.Gender ?? "";
            txtPhone.Text = c.Phone ?? "";
            textBox4.Text = c.Email ?? "";
            txtAddress.Text = c.Address ?? "";
            txtIDNumber.Text = c.IDNumber ?? "";
            txtCreatedAt.Text = c.CreatedAt.ToString("yyyy-MM-dd HH:mm");
        }

        private bool ValidateInputs()
        {
            var name = txtFullname.Text.Trim();
            var phone = txtPhone.Text.Trim();
            var email = textBox4.Text.Trim();
            var idn = txtIDNumber.Text.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Họ và tên không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullname.Focus(); return false;
            }

            if (!string.IsNullOrWhiteSpace(phone) && !Regex.IsMatch(phone, @"^\d{9,11}$"))
            {
                MessageBox.Show("SĐT chỉ gồm 9–11 chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhone.Focus(); return false;
            }

            if (!string.IsNullOrWhiteSpace(email) &&
                !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox4.Focus(); return false;
            }

            // CCCD unique (nếu thay đổi)
            if (!string.IsNullOrWhiteSpace(idn))
            {
                var exist = _all.Any(x => x.IDNumber != null &&
                                          x.IDNumber.Equals(idn, StringComparison.OrdinalIgnoreCase) &&
                                          x.CustomerID != _currentCustomerId);
                if (exist)
                {
                    MessageBox.Show("CCCD đã tồn tại cho khách khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtIDNumber.Focus(); return false;
                }
            }

            return true;
        }

        private Customer CollectForm()
        {
            return new Customer
            {
                CustomerID = _currentCustomerId,
                FullName = txtFullname.Text.Trim(),
                Gender = txtGender.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = textBox4.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                IDNumber = txtIDNumber.Text.Trim(),
                // CreatedAt không update
            };
        }

        #endregion

        #region Events

        private void dgvCustomer_SelectionChanged(object sender, EventArgs e)
        {
            if (_mode == FormMode.View)
                ShowSelectedToForm();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string q = textBox2.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(q))
            {
                dgvCustomer.DataSource = _all;
                return;
            }

            var filtered = _all.Where(c =>
                    (!string.IsNullOrEmpty(c.FullName) && c.FullName.ToLower().Contains(q)) ||
                    (!string.IsNullOrEmpty(c.Phone) && c.Phone.ToLower().Contains(q)) ||
                    (!string.IsNullOrEmpty(c.Email) && c.Email.ToLower().Contains(q)) ||
                    (!string.IsNullOrEmpty(c.Address) && c.Address.ToLower().Contains(q)) ||
                    (!string.IsNullOrEmpty(c.IDNumber) && c.IDNumber.ToLower().Contains(q))
                ).ToList();

            dgvCustomer.DataSource = filtered;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_mode == FormMode.View)
            {
                // Chỉ cho Edit khi có dòng chọn
                if (dgvCustomer.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                SetMode(FormMode.Edit);
                return;
            }

            // _mode == Edit -> Save
            if (!ValidateInputs()) return;

            var dto = CollectForm();
            bool ok = _svc.updateCustomer(dto);

            if (ok)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // reload + giữ lại selection
                int keepId = dto.CustomerID;
                LoadCustomers();
                // chọn lại dòng cũ nếu còn
                var row = dgvCustomer.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(r => (r.DataBoundItem as Customer)?.CustomerID == keepId);
                if (row != null)
                {
                    dgvCustomer.ClearSelection();
                    row.Selected = true;
                    dgvCustomer.FirstDisplayedScrollingRowIndex = row.Index;
                }

                SetMode(FormMode.View);
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (_mode == FormMode.Edit)
            {
                // hủy chỉnh sửa, revert lại
                SetMode(FormMode.View);
            }
            else
            {
                // đang View: đóng form (hoặc để nguyên nếu cậu muốn)
                // this.Close();
                // tạm thời chỉ revert hiển thị
                ShowSelectedToForm();
            }
        }

        #endregion
    }
}
