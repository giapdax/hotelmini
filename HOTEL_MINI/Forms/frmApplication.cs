using HOTEL_MINI.Forms;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace HOTEL_MINI
{
    public partial class frmApplication : Form
    {
        private Form activeForm = null;
        private Size originalSize;
        private Dictionary<Control, Rectangle> controlsOriginalBounds;
        private Panel panelIndicator;
        private Button currentButton = null;
        private User _currentUser;

        public frmApplication(User user)
        {
            InitializeComponent();
            _currentUser = user;

            this.MaximizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            originalSize = this.Size;
            controlsOriginalBounds = new Dictionary<Control, Rectangle>();
            SaveControlBounds(this);

            this.Resize += Form1_Resize;

            panelIndicator = new Panel();
            panelIndicator.Size = new Size(3, 0);
            panelIndicator.BackColor = Color.White;
            panelIndicator.Visible = false;
            panelMenu.Controls.Add(panelIndicator);
            label1.Text = $"Xin chào, {_currentUser.FullName}";

            // Mở form frmDashboard ngay khi khởi động
            OpenChildForm(new Forms.frmStatistical(), btnDashboard);
        }
        public User GetCurrentUser()
        {
            return _currentUser;
        }

        private void SaveControlBounds(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl == panelIndicator) continue;

                controlsOriginalBounds[ctrl] = ctrl.Bounds;
                if (ctrl.Controls.Count > 0)
                {
                    SaveControlBounds(ctrl);
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            float scaleX = (float)this.Width / originalSize.Width;
            float scaleY = (float)this.Height / originalSize.Height;

            ResizeControls(scaleX, scaleY, this);

            if (currentButton != null)
            {
                HighlightButton(currentButton);
            }
        }

        private void ResizeControls(float scaleX, float scaleY, Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (controlsOriginalBounds.ContainsKey(ctrl))
                {
                    Rectangle rect = controlsOriginalBounds[ctrl];
                    ctrl.Bounds = new Rectangle(
                        (int)(rect.X * scaleX),
                        (int)(rect.Y * scaleY),
                        (int)(rect.Width * scaleX),
                        (int)(rect.Height * scaleY)
                    );
                }

                if (ctrl.Controls.Count > 0)
                {
                    ResizeControls(scaleX, scaleY, ctrl);
                }
            }
        }

        private void ResetButtonColors()
        {
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(43, 47, 55);
                    btn.ForeColor = Color.Gainsboro;
                }
            }
        }

        private void HighlightButton(object sender)
        {
            if (sender is Button btn)
            {
                ResetButtonColors();
                currentButton = btn;

                btn.BackColor = Color.FromArgb(14, 26, 28);
                btn.ForeColor = Color.Gainsboro;

                panelIndicator.Dock = DockStyle.None;
                panelIndicator.Width = 4;
                panelIndicator.Height = btn.Height;
                panelIndicator.Top = btn.Top;
                panelIndicator.Left = btn.Left + btn.Width - panelIndicator.Width;

                panelIndicator.Parent = btn.Parent;
                panelIndicator.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                panelIndicator.Visible = true;
                panelIndicator.BringToFront();
            }
        }
        // Hàm mở form con trong panelDesktop   
        public void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();

            HighlightButton(btnSender);

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelDesktop.Controls.Clear();
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;

            childForm.BringToFront();
            childForm.Show();

            lblTitle.Text = childForm.Text;
        }
        //public void BookingDetailRoom(int roomId)
        //{
        //    OpenChildForm(new Forms.frmBookingDetail(roomId, this), btnDashboard);
        //}
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmStatistical(), sender);
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmRoom(this), sender);
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmService(), sender);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmUser(), sender);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            contextMenuProfile.Show(pictureBox1, new Point(0, pictureBox1.Height));
        }


        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
                frmLogin loginForm = new frmLogin();
                loginForm.Show();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (frmProfile profileForm = new frmProfile(_currentUser))
            {
                DialogResult result = profileForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                }
            }
        }

        private void btnRoomManager_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmRoomManager(), sender);
        }

        private void btnCustomerManage_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmCustomer(), sender);
        }
    }
}