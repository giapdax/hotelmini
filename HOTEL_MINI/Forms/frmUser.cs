using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmUser : Form
    {
        private readonly UserService _userService;

        public frmUser()
        {
            InitializeComponent();
            _userService = new UserService();
            this.Load += new System.EventHandler(this.frmUser_Load);
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
        }

        // 🌟 Phương thức tải tất cả người dùng
        private void LoadAllUsers()
        {
            List<User> users = _userService.GetAllUsers();
            dataGridView1.DataSource = users;
            dataGridView1.Refresh();
        }

        // 🌟 Xử lý sự kiện khi click vào một ô trong DataGridView
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtFullName.Text = row.Cells["FullName"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                cmbRole.Text = GetRoleName(Convert.ToInt32(row.Cells["Role"].Value));
                cmbStatus.Text = row.Cells["Status"].Value.ToString();
            }
        }

        // 🌟 Ánh xạ RoleID sang tên Role
        private string GetRoleName(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "Admin";
                case 2:
                    return "Receptionist";
                default:
                    return "Unknown";
            }
        }

        // 🌟 Xử lý sự kiện khi form được tải
        private void frmUser_Load(object sender, EventArgs e)
        {
            LoadAllUsers();
        }

        // 🌟 Các hàm trống bạn đã cung cấp (được giữ lại nguyên vẹn)
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void lblThongTinChiTiet_Click(object sender, EventArgs e)
        {

        }

        private void panelUserManager_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var newUser = new User()
            {
                Username = txtUsername.Text,
                FullName = txtFullName.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Role = cmbRole.SelectedItem.ToString() == "Admin" ? 1 : 2,
                Status = (UserStatus)Enum.Parse(typeof(UserStatus), cmbStatus.SelectedItem.ToString())
            };
            string password = txtPassword.Text;
            bool isAdded = _userService.AddUser(newUser, password);
            if (isAdded)
            {
                MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadAllUsers();
            }
            else
            {
                MessageBox.Show("Thêm người dùng thất bại. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearFields()
        {
            // Xóa trắng các TextBox
            txtUsername.Text = string.Empty;
            txtFullName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtPhone.Text = string.Empty;

            // Bạn cũng nên đặt lại giá trị mặc định cho các ComboBox nếu cần
            cmbRole.SelectedIndex = -1; // Chọn không có mục nào
            cmbStatus.SelectedIndex = -1; // Chọn không có mục nào

            // Đặt con trỏ chuột về ô nhập liệu đầu tiên để người dùng tiện nhập tiếp
            txtUsername.Focus();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int selectedRowIndex = dataGridView1.SelectedRows[0].Index;
            int userId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["UserID"].Value);

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
            var selectedRows = dataGridView1.SelectedRows;
            if (selectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int selectedRowIndex = selectedRows[0].Index;
            int userId = Convert.ToInt32(dataGridView1.Rows[selectedRowIndex].Cells["UserID"].Value);
            var updatedUser = new User()
            {
                UserID = userId,
                Username = txtUsername.Text,
                FullName = txtFullName.Text,
                Phone = txtPhone.Text,
                Email = txtEmail.Text,
                Role = cmbRole.SelectedItem.ToString() == "Admin" ? 1 : 2,
                Status = (UserStatus)Enum.Parse(typeof(UserStatus), cmbStatus.SelectedItem.ToString())
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
    }
}