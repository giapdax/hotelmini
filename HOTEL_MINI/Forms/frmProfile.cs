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
        private bool isPasswordVisible = false;   // trạng thái ẩn/hiện mật khẩu
        private bool isEditMode = false;          // trạng thái edit/view

        public frmProfile(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _userService = new UserService();
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            LoadUserData();
            SetEditMode(false); // mặc định chỉ xem
        }

        private void LoadUserData()
        {
            txtUsername.Text = _currentUser.Username;
            txtFullName.Text = _currentUser.FullName;
            txtEmail.Text = _currentUser.Email;
            txtPhone.Text = _currentUser.Phone;


            txtUsername.ReadOnly = true;
        }


        private void SetEditMode(bool editable)
        {
            isEditMode = editable;

            // Username luôn readonly
            txtUsername.ReadOnly = true;
            txtFullName.ReadOnly = !editable;
            txtEmail.ReadOnly = !editable;
            txtPhone.ReadOnly = !editable;

            btnEditSave.Text = editable ? "Lưu" : "Chỉnh sửa";
        }

        private void btnEditSave_Click(object sender, EventArgs e)
        {
            if (!isEditMode)
            {
                // bật chế độ chỉnh sửa
                SetEditMode(true);
            }
            else
            {
                // lưu dữ liệu
                if (!ValidateInputs()) return;

                _currentUser.FullName = txtFullName.Text.Trim();
                _currentUser.Email = txtEmail.Text.Trim();
                _currentUser.Phone = txtPhone.Text.Trim();

                bool isUpdated = _userService.UpdateUser(_currentUser);
                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetEditMode(false);
                    LoadUserData();
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (isEditMode)
            {
                // thoát chế độ edit, load lại dữ liệu cũ
                SetEditMode(false);
                LoadUserData();
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
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
