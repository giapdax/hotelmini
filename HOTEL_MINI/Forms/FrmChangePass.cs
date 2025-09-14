using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HOTEL_MINI.BLL;
using HOTEL_MINI.Common;
using HOTEL_MINI.Model.Entity;

namespace HOTEL_MINI.Forms
{
    public partial class FrmChangePass : Form
    {
        private readonly UserService _userService = new UserService();
        private readonly User _currentUser;

        public FrmChangePass(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));

            this.Text = "Đổi mật khẩu";
            txtUsername.PasswordChar = '*'; // current password
            txtFullName.PasswordChar = '*'; // new password
            txtEmail.PasswordChar = '*';    // confirm new password

            btnEditSave.Click -= btnEditSave_Click;
            btnEditSave.Click += btnEditSave_Click;

            btnCancel.Click -= btnCancel_Click;
            btnCancel.Click += btnCancel_Click;

            this.AcceptButton = btnEditSave;
            this.CancelButton = btnCancel;
        }

        private void btnEditSave_Click(object sender, EventArgs e)
        {
            string currentPw = txtUsername.Text;
            string newPw = txtFullName.Text;
            string confirmPw = txtEmail.Text;

            if (string.IsNullOrWhiteSpace(currentPw) ||
                string.IsNullOrWhiteSpace(newPw) ||
                string.IsNullOrWhiteSpace(confirmPw))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ 3 trường.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!PasswordHelper.Validate(newPw, out var msg))
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFullName.Focus();
                return;
            }

            if (newPw != confirmPw)
            {
                MessageBox.Show("Nhập lại mật khẩu mới không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }

            var result = _userService.ChangePassword(_currentUser.UserID, currentPw, newPw);

            switch (result)
            {
                case UserService.ChangePasswordResult.Success:
                    // đọc lại DB để sync object hiện tại
                    var fresh = _userService.GetById(_currentUser.UserID);
                    if (fresh != null)
                        _currentUser.PasswordHash = fresh.PasswordHash;

                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;

                case UserService.ChangePasswordResult.WrongCurrentPassword:
                    MessageBox.Show("Mật khẩu hiện tại không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.SelectAll();
                    txtUsername.Focus();
                    break;

                case UserService.ChangePasswordResult.NewPasswordTooWeak:
                    MessageBox.Show("Mật khẩu mới không đạt yêu cầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFullName.SelectAll();
                    txtFullName.Focus();
                    break;

                case UserService.ChangePasswordResult.SameAsOld:
                    MessageBox.Show("Mật khẩu mới trùng với mật khẩu hiện tại. Hãy dùng mật khẩu khác.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullName.SelectAll();
                    txtFullName.Focus();
                    break;

                default:
                    MessageBox.Show("Có lỗi xảy ra khi đổi mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
