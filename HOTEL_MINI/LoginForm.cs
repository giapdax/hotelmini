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

namespace HOTEL_MINI
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService;
        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text;
            string username = txtUsername.Text;
            LoginResult result = _authService.login(txtUsername.Text, txtPassword.Text);

            if (result.Success)
            {
                // Login thành công
                MessageBox.Show($"Chào {result.User.FullName}, đăng nhập thành công!", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.None);
                this.Hide();
                Form1 form1 = new Form1();
                form1.Show();
            }
            else
            {
                // Login thất bại, hiển thị message
                MessageBox.Show(result.Message);
            }
        }
    }
}
