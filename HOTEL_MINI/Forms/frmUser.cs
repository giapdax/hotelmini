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
        // 🌟 Khai báo biến để sử dụng UserRepository
        private UserRepository _userRepository;

        public frmUser()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
            this.Load += new System.EventHandler(this.frmUser_Load);
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
        }

        // 🌟 Phương thức tải tất cả người dùng
        private void LoadAllUsers()
        {
            List<User> users = _userRepository.GetAllUsers();
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
    }
}