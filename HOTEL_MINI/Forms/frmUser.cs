using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmUser : Form
    {
        private readonly UserService _userSvc = new UserService();
        private readonly RoleService _roleSvc = new RoleService();

        private bool isAdd = false; 
        private BindingList<User> _binding;

        public frmUser()
        {
            InitializeComponent();
        }

        private void frmUser_Load(object sender, EventArgs e)
        {
            LoadRoles();
            LoadData();
            SetButtons(viewMode: true);
        }

        private void LoadRoles()
        {
            var roles = _roleSvc.GetAllRoles() ?? new List<Role>();
            cmbRole.DataSource = roles;
            cmbRole.DisplayMember = "RoleName";
            cmbRole.ValueMember = "RoleID";
            cmbRole.SelectedIndex = -1;

            cmbStatus.DataSource = Enum.GetNames(typeof(UserStatus));
            cmbStatus.SelectedIndex = -1;
        }

        private void LoadData()
        {
            var list = _userSvc.GetAllUsers() ?? new List<User>();
            _binding = new BindingList<User>(list);
            dataGridView1.AutoGenerateColumns = true; 
            dataGridView1.DataSource = _binding;
            FillFromGrid();
        }

        private void SetButtons(bool viewMode)
        {
            btnAdd.Enabled = viewMode;
            btnEdit.Enabled = viewMode && dataGridView1.CurrentRow != null;
            btnDelete.Enabled = viewMode && dataGridView1.CurrentRow != null;
            btnSave.Enabled = !viewMode;
            btnCancel.Enabled = !viewMode;
            txtUsername.Enabled = !viewMode && isAdd;
        }

        private void ClearInputs()
        {
            txtUsername.Clear();
            txtFullName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
        }

        private void FillFromGrid()
        {
            var u = dataGridView1.CurrentRow?.DataBoundItem as User;
            if (u == null) { ClearInputs(); return; }

            txtUsername.Text = u.Username;
            txtFullName.Text = u.FullName ?? "";
            txtPhone.Text = u.Phone ?? "";
            txtEmail.Text = u.Email ?? "";
            txtPassword.Text = "********";
            cmbRole.SelectedValue = u.Role;
            cmbStatus.SelectedItem = u.Status.ToString();
        }

        private User FillToModel()
        {
            int id = 0;
            if (!isAdd && dataGridView1.CurrentRow?.DataBoundItem is User cur)
                id = cur.UserID;

            var statusName = cmbStatus.SelectedItem?.ToString();
            Enum.TryParse(statusName, out UserStatus status);

            int roleId = cmbRole.SelectedValue is int r ? r : 0;

            return new User
            {
                UserID = id,
                Username = txtUsername.Text?.Trim(),
                FullName = txtFullName.Text?.Trim(),
                Phone = txtPhone.Text?.Trim(),
                Email = txtEmail.Text?.Trim(),
                Role = roleId,
                Status = status
            };
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (btnSave.Enabled) return;
            FillFromGrid();
            SetButtons(viewMode: true);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var q = (txtSearch.Text ?? "").Trim().ToLower();
            var all = _userSvc.GetAllUsers() ?? new List<User>();
            if (!string.IsNullOrEmpty(q))
            {
                all = all.Where(u =>
                        (u.Username ?? "").ToLower().Contains(q) ||
                        (u.FullName ?? "").ToLower().Contains(q) ||
                        (u.Email ?? "").ToLower().Contains(q) ||
                        (u.Phone ?? "").ToLower().Contains(q) ||
                        u.Status.ToString().ToLower().Contains(q) ||
                        u.Role.ToString().Contains(q))
                    .ToList();
            }
            _binding = new BindingList<User>(all);
            dataGridView1.DataSource = _binding;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            isAdd = true;
            ClearInputs();
            SetButtons(viewMode: false);
            txtUsername.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            isAdd = false;
            FillFromGrid();
            txtPassword.Text = "********";
            SetButtons(viewMode: false);
            txtFullName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            isAdd = false;
            LoadData();
            SetButtons(viewMode: true);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var model = FillToModel();

            var plainPassword = txtPassword.Text;
            if (!isAdd && plainPassword == "********")
                plainPassword = null;

            try
            {
                // Validate ở BLL và HIỂN THỊ LỖI
                var vr = _userSvc.TryValidateUser(model, isUpdate: !isAdd, plainPasswordIfProvided: plainPassword);
                if (!vr.IsValid)
                {
                    MessageBox.Show(vr.Message, "Dữ liệu chưa hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool ok = isAdd
                    ? _userSvc.AddUser(model, plainPassword)                 
                    : _userSvc.UpdateUser(model, plainPassword);  

                if (!ok)
                {
                    MessageBox.Show("Thao tác không thành công.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Đã lưu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isAdd = false;
                LoadData();
                SetButtons(viewMode: true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var u = dataGridView1.CurrentRow?.DataBoundItem as User;
            if (u == null) return;

            var cf = MessageBox.Show($"Xóa user '{u.Username}' ?", "Xác nhận",
                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cf != DialogResult.Yes) return;

            try
            {
                if (_userSvc.DeleteUser(u.UserID))
                {
                    MessageBox.Show("Đã xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
