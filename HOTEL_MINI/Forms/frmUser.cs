// File: frmUser.cs
using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmUser : Form
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private List<Role> _roles;
        private List<User> _allUsers;

        private enum FormState { View, Add, Edit }
        private FormState _currentState;

        public frmUser()
        {
            InitializeComponent();
            _userService = new UserService();
            _roleService = new RoleService();

            // Đăng ký các sự kiện
            this.Load += frmUser_Load;
            this.dataGridView1.CellClick += dataGridView1_CellClick;
            this.dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            this.dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            this.txtSearch.TextChanged += txtSearch_TextChanged;

            // Sự kiện cho các nút
            this.btnAdd.Click += btnAdd_Click;
            this.btnEdit.Click += btnEdit_Click;
            this.btnDelete.Click += btnDelete_Click;
            this.btnCancel.Click += btnCancel_Click;
        }

        #region Load dữ liệu

        private void frmUser_Load(object sender, EventArgs e)
        {
            LoadRolesAndStatusToComboBox();
            LoadAllUsers();
            SetupDataGridView();
            SetFormState(FormState.View);
        }

        private void LoadAllUsers()
        {
            _allUsers = _userService.GetAllUsers();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _allUsers;
            ClearFields();
        }

        private void LoadRolesAndStatusToComboBox()
        {
            _roles = _roleService.GetAllRoles();
            cmbRole.DataSource = _roles;
            cmbRole.DisplayMember = "RoleName";
            cmbRole.ValueMember = "RoleID";
            cmbRole.SelectedIndex = -1;

            cmbStatus.DataSource = Enum.GetValues(typeof(UserStatus));
            cmbStatus.SelectedIndex = -1;
        }

        #endregion

        #region Form state

        private void SetFormState(FormState state)
        {
            _currentState = state;

            btnAdd.Enabled = (_currentState == FormState.View);
            btnEdit.Enabled = (_currentState == FormState.View && dataGridView1.SelectedRows.Count > 0);
            btnDelete.Enabled = (_currentState == FormState.View && dataGridView1.SelectedRows.Count > 0);

            btnSave.Visible = (_currentState == FormState.Add || _currentState == FormState.Edit);
            btnCancel.Visible = (_currentState == FormState.Add || _currentState == FormState.Edit);

            bool isViewing = _currentState == FormState.View;
            txtUsername.ReadOnly = isViewing || _currentState == FormState.Edit; // Username chỉ nhập khi thêm mới
            txtFullName.ReadOnly = isViewing;
            txtEmail.ReadOnly = isViewing;
            txtPhone.ReadOnly = isViewing;
            txtPassword.ReadOnly = isViewing;

            cmbRole.Enabled = !isViewing;
            cmbStatus.Enabled = !isViewing;
        }

        #endregion

        #region DataGridView Events

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && _currentState != FormState.Add)
            {
                DisplaySelectedRowInfo();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (_currentState == FormState.View)
            {
                btnEdit.Enabled = dataGridView1.SelectedRows.Count > 0;
                btnDelete.Enabled = dataGridView1.SelectedRows.Count > 0;

                if (dataGridView1.SelectedRows.Count > 0)
                    DisplaySelectedRowInfo();
                else
                    ClearFields();
            }
        }

        private void DisplaySelectedRowInfo()
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridView1.SelectedRows[0];

            txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";
            txtFullName.Text = row.Cells["FullName"].Value?.ToString() ?? "";
            txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
            txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";

            txtPassword.Text = "********";
            txtPassword.PasswordChar = '*';

            if (row.Cells["Role"].Value != null && int.TryParse(row.Cells["Role"].Value.ToString(), out int roleId))
            {
                if (_roles.Any(r => r.RoleID == roleId))
                    cmbRole.SelectedValue = roleId;
                else
                    cmbRole.SelectedIndex = -1;
            }

            if (row.Cells["Status"].Value is UserStatus status)
                cmbStatus.SelectedItem = status;
        }

        #endregion

        #region CRUD Button Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetFormState(FormState.Add);
            ClearFields();
            txtUsername.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                SetFormState(FormState.Edit);
                txtPassword.Text = "********";
                txtPassword.PasswordChar = '*';
                txtFullName.Focus();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_currentState == FormState.Add) HandleAddUser();
            else if (_currentState == FormState.Edit) HandleUpdateUser();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserID"].Value);
            string username = dataGridView1.SelectedRows[0].Cells["Username"].Value.ToString();
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng '{username}'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                bool isDeleted = _userService.DeleteUser(userId);
                if (isDeleted)
                {
                    MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllUsers();
                }
                else
                {
                    MessageBox.Show("Xóa người dùng thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
                DisplaySelectedRowInfo();
            else
                ClearFields();

            SetFormState(FormState.View);
        }

        #endregion

        #region Xử lý thêm/sửa

        private void HandleAddUser()
        {
            if (!ValidateInputs(isNew: true)) return;

            var newUser = new User()
            {
                Username = txtUsername.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Role = (int)cmbRole.SelectedValue,
                Status = (UserStatus)cmbStatus.SelectedItem
            };

            string password = txtPassword.Text.Trim();

            bool isAdded = _userService.AddUser(newUser, password);
            if (isAdded)
            {
                MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllUsers();
                SetFormState(FormState.View);
            }
            else
            {
                MessageBox.Show("Thêm người dùng thất bại. Username có thể đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleUpdateUser()
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            if (!ValidateInputs(isNew: false)) return;

            int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserID"].Value);
            var updatedUser = new User()
            {
                UserID = userId,
                Username = txtUsername.Text.Trim(),
                FullName = txtFullName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Role = (int)cmbRole.SelectedValue,
                Status = (UserStatus)cmbStatus.SelectedItem
            };

            string newPassword = null;
            if (!string.IsNullOrWhiteSpace(txtPassword.Text) && txtPassword.Text != "********")
                newPassword = txtPassword.Text.Trim();

            bool isUpdated = _userService.UpdateUser(updatedUser, newPassword);
            if (isUpdated)
            {
                MessageBox.Show("Cập nhật người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllUsers();
                SetFormState(FormState.View);
            }
            else
            {
                MessageBox.Show("Cập nhật người dùng thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Validate

        private bool ValidateInputs(bool isNew)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (isNew && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Password không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (cmbRole.SelectedValue == null || cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Vai trò và Trạng thái không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtPhone.Text) &&
                !Regex.IsMatch(txtPhone.Text, @"^[0-9]+$"))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        #endregion

        #region Tiện ích

        private void ClearFields()
        {
            txtUsername.Text = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtPassword.Text = "";
            txtPassword.PasswordChar = '*';
            cmbRole.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower().Trim();
            if (string.IsNullOrWhiteSpace(searchText))
                dataGridView1.DataSource = _allUsers;
            else
            {
                var filteredUsers = _allUsers.Where(user =>
                    user.Username.ToLower().Contains(searchText) ||
                    (user.FullName != null && user.FullName.ToLower().Contains(searchText)) ||
                    (user.Email != null && user.Email.ToLower().Contains(searchText)) ||
                    (user.Phone != null && user.Phone.Contains(searchText))
                ).ToList();

                dataGridView1.DataSource = filteredUsers;
            }
        }

        private void SetupDataGridView()
        {
            if (dataGridView1.Columns.Contains("UserID"))
                dataGridView1.Columns["UserID"].Visible = false;
            if (dataGridView1.Columns.Contains("PasswordHash"))
                dataGridView1.Columns["PasswordHash"].Visible = false;

            if (dataGridView1.Columns.Contains("FullName")) dataGridView1.Columns["FullName"].HeaderText = "Họ và Tên";
            if (dataGridView1.Columns.Contains("Username")) dataGridView1.Columns["Username"].HeaderText = "Tên đăng nhập";
            if (dataGridView1.Columns.Contains("Email")) dataGridView1.Columns["Email"].HeaderText = "Email";
            if (dataGridView1.Columns.Contains("Phone")) dataGridView1.Columns["Phone"].HeaderText = "Điện thoại";
            if (dataGridView1.Columns.Contains("Role")) dataGridView1.Columns["Role"].HeaderText = "Vai trò";
            if (dataGridView1.Columns.Contains("Status")) dataGridView1.Columns["Status"].HeaderText = "Trạng thái";
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Status" && e.Value is UserStatus)
            {
                e.Value = e.Value.ToString();
                e.FormattingApplied = true;
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Role" && e.Value is int roleId)
            {
                var role = _roles?.FirstOrDefault(r => r.RoleID == roleId);
                if (role != null) e.Value = role.RoleName;
                e.FormattingApplied = true;
            }
        }

        #endregion

        #region Sự kiện không dùng

        private void button1_Click(object sender, EventArgs e) { }
        private void lblThongTinChiTiet_Click(object sender, EventArgs e) { }
        private void panelUserManager_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e) { }
        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void lblRole_Click(object sender, EventArgs e) { }
        private void btnSearch_Click(object sender, EventArgs e) { }
        private void cmbSearchStatus_SelectedIndexChanged(object sender, EventArgs e) { }

        #endregion

    }
}
