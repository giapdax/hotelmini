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

        // Danh sách gốc (Customer thuần)
        private List<Customer> _all = new List<Customer>();

        // Danh sách bind lên grid (có thêm số lần booking)
        private List<CustomerRow> _rows = new List<CustomerRow>();

        private int _currentCustomerId = 0;

        private enum FormMode { View, Edit }
        private FormMode _mode = FormMode.View;

        // Lớp hiển thị cho grid (thêm BookingsCount)
        private class CustomerRow
        {
            public int CustomerID { get; set; }
            public string FullName { get; set; }
            public string Gender { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public string IDNumber { get; set; }
            public DateTime CreatedAt { get; set; }
            public int BookingsCount { get; set; } // Số lần đặt phòng
        }

        public frmCustomer()
        {
            InitializeComponent();

            // wire events ngoài các event đã có sẵn trong Designer
            this.Load += FrmCustomer_Load;
            dgvCustomer.SelectionChanged += dgvCustomer_SelectionChanged;
            textBox2.TextChanged += textBox2_TextChanged; // ô search
            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;
        }

        #region Load + Binding

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            txtCreatedAt.ReadOnly = true;

            LoadGendersToCombo();
            LoadCustomers();

            SetupGrid();
            SetMode(FormMode.View);
        }

        private void LoadGendersToCombo()
        {
            try
            {
                var genders = _svc.getAllGender() ?? new List<string>();
                if (cboGender != null)
                {
                    cboGender.DataSource = genders;
                    // Khuyên dùng DropDownList để đồng bộ enum
                    // cboGender.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            catch
            {
                // ignore
            }
        }

        private void LoadCustomers()
        {
            _all = _svc.getAllCustomers() ?? new List<Customer>();
            var counts = _svc.getBookingCounts() ?? new Dictionary<int, int>();

            _rows = _all.Select(c => new CustomerRow
            {
                CustomerID = c.CustomerID,
                FullName = c.FullName,
                Gender = c.Gender,
                Phone = c.Phone,
                Email = c.Email,
                Address = c.Address,
                IDNumber = c.IDNumber,
                CreatedAt = c.CreatedAt,
                BookingsCount = counts.TryGetValue(c.CustomerID, out var n) ? n : 0
            }).ToList();

            dgvCustomer.AutoGenerateColumns = true;
            dgvCustomer.DataSource = _rows;

            if (_rows.Count > 0)
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

                if (dgvCustomer.Columns.Contains("BookingsCount"))
                    dgvCustomer.Columns["BookingsCount"].HeaderText = "Số lần đặt";
            };
        }

        #endregion

        #region Mode + Form state

        private void SetMode(FormMode mode)
        {
            _mode = mode;
            bool editable = (_mode == FormMode.Edit);

            txtFullname.ReadOnly = !editable;
            txtPhone.ReadOnly = !editable;
            textBox4.ReadOnly = !editable; // Email
            txtAddress.ReadOnly = !editable;
            txtIDNumber.ReadOnly = !editable;
            txtCreatedAt.ReadOnly = true;

            if (cboGender != null) cboGender.Enabled = editable;

            btnSave.Text = editable ? "Save" : "Edit";
            btnCancel.Text = editable ? "Hủy" : "Hủy";

            dgvCustomer.Enabled = !editable;

            if (!editable)
            {
                ShowSelectedToForm();
            }
        }

        #endregion

        #region Helpers

        private void ClearFields()
        {
            _currentCustomerId = 0;
            txtFullname.Text = "";
            txtPhone.Text = "";
            textBox4.Text = ""; // email
            txtAddress.Text = "";
            txtIDNumber.Text = "";
            txtCreatedAt.Text = "";

            if (cboGender != null)
            {
                if (cboGender.Items.Count > 0) cboGender.SelectedIndex = 0;
                else cboGender.Text = "";
            }
        }

        private void ShowSelectedToForm()
        {
            if (dgvCustomer.SelectedRows.Count == 0)
            {
                ClearFields();
                return;
            }

            var rowObj = dgvCustomer.SelectedRows[0].DataBoundItem as CustomerRow;
            if (rowObj == null) { ClearFields(); return; }

            _currentCustomerId = rowObj.CustomerID;
            txtFullname.Text = rowObj.FullName ?? "";

            if (cboGender != null)
            {
                if (cboGender.Items.Count > 0 && rowObj.Gender != null)
                {
                    int idx = -1;
                    for (int i = 0; i < cboGender.Items.Count; i++)
                    {
                        if (string.Equals(cboGender.Items[i]?.ToString(), rowObj.Gender, StringComparison.OrdinalIgnoreCase))
                        {
                            idx = i; break;
                        }
                    }
                    if (idx >= 0) cboGender.SelectedIndex = idx;
                    else cboGender.Text = rowObj.Gender; // fallback nếu enum chưa có
                }
                else
                {
                    cboGender.Text = rowObj.Gender ?? "";
                }
            }

            txtPhone.Text = rowObj.Phone ?? "";
            textBox4.Text = rowObj.Email ?? "";
            txtAddress.Text = rowObj.Address ?? "";
            txtIDNumber.Text = rowObj.IDNumber ?? "";
            txtCreatedAt.Text = rowObj.CreatedAt.ToString("yyyy-MM-dd HH:mm");
        }

        private bool ValidateInputs()
        {
            var name = txtFullname.Text.Trim();
            var phone = txtPhone.Text.Trim();
            var email = textBox4.Text.Trim();
            var idn = txtIDNumber.Text.Trim();

            string gender = cboGender != null
                ? (cboGender.SelectedItem?.ToString() ?? cboGender.Text ?? "")
                : "";
            gender = gender.Trim();

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

            //Nếu bắt buộc chọn Gender từ enum thì bật đoạn dưới:
             if (cboGender != null && cboGender.DropDownStyle == ComboBoxStyle.DropDownList && string.IsNullOrWhiteSpace(gender))
             {
                 MessageBox.Show("Vui lòng chọn giới tính.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 cboGender.Focus(); return false;
             }

            return true;
        }

        private Customer CollectForm()
        {
            string gender = cboGender != null
                ? (cboGender.SelectedItem?.ToString() ?? cboGender.Text ?? "")
                : "";
            gender = gender.Trim();

            return new Customer
            {
                CustomerID = _currentCustomerId,
                FullName = txtFullname.Text.Trim(),
                Gender = string.IsNullOrWhiteSpace(gender) ? null : gender,
                Phone = txtPhone.Text.Trim(),
                Email = textBox4.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                IDNumber = txtIDNumber.Text.Trim(),
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
                dgvCustomer.DataSource = _rows;
                return;
            }

            var filtered = _rows.Where(c =>
                    (!string.IsNullOrEmpty(c.FullName) && c.FullName.ToLower().Contains(q)) ||
                    (!string.IsNullOrEmpty(c.Phone) && c.Phone.ToLower().Contains(q)) ||
                    (!string.IsNullOrEmpty(c.Email) && c.Email.ToLower().Contains(q)) ||
                    (!string.IsNullOrEmpty(c.Address) && c.Address.ToLower().Contains(q)) ||
                    (!string.IsNullOrEmpty(c.IDNumber) && c.IDNumber.ToLower().Contains(q)) ||
                    (!string.IsNullOrEmpty(c.Gender) && c.Gender.ToLower().Contains(q)) ||
                    (c.BookingsCount.ToString().Contains(q))
                ).ToList();

            dgvCustomer.DataSource = filtered;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_mode == FormMode.View)
            {
                if (dgvCustomer.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng để chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                SetMode(FormMode.Edit);
                return;
            }

            if (!ValidateInputs()) return;

            var dto = CollectForm();
            bool ok = _svc.updateCustomer(dto);

            if (ok)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int keepId = dto.CustomerID;
                LoadCustomers();

                var row = dgvCustomer.Rows.Cast<DataGridViewRow>()
                    .FirstOrDefault(r => (r.DataBoundItem as CustomerRow)?.CustomerID == keepId);
                if (row != null)
                {
                    dgvCustomer.ClearSelection();
                    row.Selected = true;
                    try { dgvCustomer.FirstDisplayedScrollingRowIndex = row.Index; } catch { }
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
                SetMode(FormMode.View);
            }
            else
            {
                ShowSelectedToForm();
            }
        }

        #endregion
    }
}
