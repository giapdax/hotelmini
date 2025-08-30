using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Response;
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
    public partial class frmLogin : Form
    {
        private AuthService _authService;

        public frmLogin()
        {
            InitializeComponent();
            _authService = new AuthService();
            // Gán các sự kiện cho nút
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Có thể thêm code khởi tạo khi form load ở đây
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

                // 🌟 MỞ FORM CHÍNH CỦA ỨNG DỤNG
                frmApplication mainForm = new frmApplication();
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

        // Phương thức này không cần thiết, bạn có thể xóa nếu không sử dụng
        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load_1(object sender, EventArgs e)
        {

        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void pnlRight_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
