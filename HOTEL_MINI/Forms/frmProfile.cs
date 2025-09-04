using System;
using System.Windows.Forms;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.BLL;
using System.Text.RegularExpressions;

namespace HOTEL_MINI.Forms
{
    public partial class frmProfile : Form
    {
        private User _currentUser;
        private readonly UserService _userService;

        public frmProfile(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _userService = new UserService();
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void LoadUserData()
        {
            txtUsername.Text = _currentUser.Username;
            txtFullName.Text = _currentUser.FullName;
            txtEmail.Text = _currentUser.Email;
            txtPhone.Text = _currentUser.Phone;
            txtPassword.Text = "";
            txtUsername.ReadOnly = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            _currentUser.FullName = txtFullName.Text.Trim();
            _currentUser.Email = txtEmail.Text.Trim();
            _currentUser.Phone = txtPhone.Text.Trim();

            string newPassword = null;
            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                newPassword = txtPassword.Text.Trim();
            }

            bool isUpdated = _userService.UpdateUser(_currentUser, newPassword);
            if (isUpdated)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Tên đầy đủ không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Email không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtPhone.Text) && !Regex.IsMatch(txtPhone.Text, @"^[0-9]+$"))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa chữ số.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
}