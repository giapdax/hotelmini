// File: frmLogin.cs
using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Response;
using System;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmLogin : Form
    {
        private AuthService _authService;
        private bool isPasswordVisible = false;

        public frmLogin()
        {
            InitializeComponent();
            _authService = new AuthService();

            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.btnTogglePassword.Click += new EventHandler(this.btnTogglePassword_Click);

            // *** Quan trọng ***:
            // Mặc định, hãy sử dụng UseSystemPasswordChar để mật khẩu bị ẩn.
            // KHÔNG thiết lập PasswordChar ở đây để tránh xung đột.
            txtPassword.UseSystemPasswordChar = true;
            btnTogglePassword.Image = Properties.Resources.eye_slash; // icon mắt đóng
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            LoginResult result = _authService.Login(username, password);

            if (result.Success)
            {
                MessageBox.Show(result.Message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();

                frmApplication mainForm = new frmApplication(result.User);
                mainForm.Show();
            }
            else
            {
                MessageBox.Show(result.Message, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtUsername.Focus();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            // Đảo ngược trạng thái hiển thị mật khẩu
            isPasswordVisible = !isPasswordVisible;

            // Điều chỉnh thuộc tính UseSystemPasswordChar dựa trên trạng thái mới
            txtPassword.UseSystemPasswordChar = !isPasswordVisible; // Nếu isPasswordVisible là true (hiện), thì UseSystemPasswordChar sẽ là false

            // Cập nhật icon cho nút
            btnTogglePassword.Image = isPasswordVisible
                ? Properties.Resources.eye       // icon mắt mở
                : Properties.Resources.eye_slash; // icon mắt đóng
        }

        // Các sự kiện không dùng đến, giữ lại để code biên dịch
        private void frmLogin_Load_1(object sender, EventArgs e) { }
        private void pnlRight_Paint(object sender, PaintEventArgs e) { }
    }
}