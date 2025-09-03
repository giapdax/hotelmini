// File: frmUser.cs
using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmUser : Form
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private List<Role> _roles;

        // Thêm một biến để lưu trữ danh sách người dùng gốc
        private List<User> _allUsers;

        public frmUser()
        {
            InitializeComponent();
            _userService = new UserService();
            _roleService = new RoleService();
            this.Load += frmUser_Load;
            this.dataGridView1.CellClick += dataGridView1_CellClick;
            this.dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            this.txtSearch.TextChanged += txtSearch_TextChanged;
        }

        private void LoadAllUsers()
        {
            _allUsers = _userService.GetAllUsers(); // Lưu danh sách gốc
            dataGridView1.DataSource = _allUsers;
            SetupDataGridView();
        }

        private void LoadRolesAndStatusToComboBox()
        {
            _roles = _roleService.GetAllRoles();
            cmbRole.DataSource = _roles;
            cmbRole.DisplayMember = "RoleName";
            cmbRole.ValueMember = "RoleID";

            cmbStatus.DataSource = Enum.GetValues(typeof(UserStatus));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? string.Empty;
                txtFullName.Text = row.Cells["FullName"].Value?.ToString() ?? string.Empty;
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? string.Empty;
                txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? string.Empty;
                txtPassword.Text = string.Empty;

                if (row.Cells["Role"].Value != null)
                {
                    int roleId = (int)row.Cells["Role"].Value;
                    cmbRole.SelectedValue = roleId;
                }

                if (row.Cells["Status"].Value != null)
                {
                    cmbStatus.SelectedItem = row.Cells["Status"].Value;
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Handle Status column formatting
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Status" && e.Value is UserStatus)
            {
                e.Value = e.Value.ToString();
                e.FormattingApplied = true;
            }

            // Handle Role column formatting
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Role" && e.Value is int roleId)
            {
                var role = _roles.FirstOrDefault(r => r.RoleID == roleId);
                if (role != null)
                {
                    e.Value = role.RoleName;
                }
                e.FormattingApplied = true;
            }
        }

        private void SetupDataGridView()
        {
            if (dataGridView1.Columns.Contains("PasswordHash"))
            {
                dataGridView1.Columns["PasswordHash"].Visible = false;
            }

            if (dataGridView1.Columns.Contains("FullName")) dataGridView1.Columns["FullName"].HeaderText = "Họ và Tên";
            if (dataGridView1.Columns.Contains("Username")) dataGridView1.Columns["Username"].HeaderText = "Tên đăng nhập";
            if (dataGridView1.Columns.Contains("Email")) dataGridView1.Columns["Email"].HeaderText = "Email";
            if (dataGridView1.Columns.Contains("Phone")) dataGridView1.Columns["Phone"].HeaderText = "Điện thoại";
            if (dataGridView1.Columns.Contains("Role")) dataGridView1.Columns["Role"].HeaderText = "Vai trò";
            if (dataGridView1.Columns.Contains("Status")) dataGridView1.Columns["Status"].HeaderText = "Trạng thái";
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            LoadRolesAndStatusToComboBox();
            LoadAllUsers();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || cmbRole.SelectedValue == null || cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newUser = new User()
            {
                Username = txtUsername.Text,
                FullName = txtFullName.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Role = (int)cmbRole.SelectedValue,
                Status = (UserStatus)cmbStatus.SelectedItem
            };
            string password = txtPassword.Text;
            bool isAdded = _userService.AddUser(newUser, password);
            if (isAdded)
            {
                MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllUsers();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Thêm người dùng thất bại. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserID"].Value);
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bool isDeleted = _userService.DeleteUser(userId);
                if (isDeleted)
                {
                    MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAllUsers();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Xóa người dùng thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbRole.SelectedValue == null || cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["UserID"].Value);
            var updatedUser = new User()
            {
                UserID = userId,
                Username = txtUsername.Text,
                FullName = txtFullName.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Role = (int)cmbRole.SelectedValue,
                Status = (UserStatus)cmbStatus.SelectedItem
            };
            string newPassword = string.IsNullOrWhiteSpace(txtPassword.Text) ? null : txtPassword.Text;
            bool isUpdated = _userService.UpdateUser(updatedUser, newPassword);
            if (isUpdated)
            {
                MessageBox.Show("Cập nhật người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllUsers();
            }
            else
            {
                MessageBox.Show("Cập nhật người dùng thất bại. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearFields()
        {
            txtUsername.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPhone.Text = string.Empty;
            cmbRole.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            txtUsername.Focus();
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearFields();

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtSearch.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                dataGridView1.DataSource = _allUsers;
            }
            else
            {
                List<User> filteredUsers = _allUsers.Where(user =>
                    user.Username.ToLower().Contains(searchText) ||
                    user.FullName.ToLower().Contains(searchText) ||
                    user.Email.ToLower().Contains(searchText) ||
                    (user.Phone != null && user.Phone.ToLower().Contains(searchText))
                ).ToList();

                dataGridView1.DataSource = filteredUsers;
            }

            SetupDataGridView();
        }

        private void button1_Click(object sender, EventArgs e) { }
        private void lblThongTinChiTiet_Click(object sender, EventArgs e) { }
        private void panelUserManager_Paint(object sender, EventArgs e) { }
        private void tableLayoutPanel1_Paint(object sender, EventArgs e) { }
        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void lblRole_Click(object sender, EventArgs e) { }
        private void btnSearch_Click(object sender, EventArgs e) { }

        private void cmbSearchStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}