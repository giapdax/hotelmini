using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Response;
using System;
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

            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
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
        private void frmLogin_Load_1(object sender, EventArgs e) { }
        private void pnlRight_Paint(object sender, PaintEventArgs e) { }
    }
}