using HOTEL_MINI.Forms;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI
{
    public partial class frmApplication : Form
    {
        private Form activeForm = null;
        private Panel panelIndicator;
        private Button currentButton = null;
        private User _currentUser;

        private const int ROLE_ADMIN = 1;
        private const int ROLE_RECEPTIONIST = 2;

        private readonly HashSet<string> _recAllowedForms = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "frmBooking",
            "frmRoom",
            "frmService",
            "frmRoomManager",
            "frmCustomer",
            "frmBookingDetail",
            "frmInvoiceManage",
            "frmReport"
        };

        public frmApplication(User user)
        {
            InitializeComponent();
            this.MinimumSize = new Size(1200, 800);
            _currentUser = user ?? new User();

            panelIndicator = new Panel
            {
                Size = new Size(3, 0),
                BackColor = Color.White,
                Visible = false
            };
            panelMenu.Controls.Add(panelIndicator);

            label1.Text = $"Xin chào, {_currentUser.FullName}";
            ApplyRolePermissions();

            if (IsAdmin())
            {
                OpenChildForm(new frmStatistical(), btnDashboardManage);
            }
            else
            {
                OpenChildForm(new frmBooking(_currentUser), btnRoom);
            }
        }

        public User GetCurrentUser() => _currentUser;

        private bool IsAdmin() => _currentUser != null && _currentUser.Role == ROLE_ADMIN;
        private bool IsReceptionist() => _currentUser != null && _currentUser.Role == ROLE_RECEPTIONIST;

        private void ApplyRolePermissions()
        {
            if (IsAdmin())
            {
                foreach (Control c in panelMenu.Controls)
                    if (c is Button b) b.Visible = true;
            }
            else
            {
                foreach (Control c in panelMenu.Controls)
                    if (c is Button b) b.Visible = false;

                SafeShow(btnRoom, true);
                SafeShow(btnService, true);
                SafeShow(btnRoomManager, true);
                SafeShow(btnCustomerManage, true);
                SafeShow(btnInvoicesManage, true);
                SafeShow(btnReport, true);

                SafeShow(btnDashboardManage, false);
                SafeShow(btnUserManage, false);

                panelIndicator.Visible = false;
            }

            if (currentButton != null && !currentButton.Visible)
            {
                currentButton = null;
                panelIndicator.Visible = false;
            }
        }

        private void SafeShow(Button btn, bool visible)
        {
            if (btn == null) return;
            btn.Visible = visible;
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
                if (!btn.Visible) return;

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

        public void OpenChildForm(Form childForm, object btnSender)
        {
            if (!IsAdmin())
            {
                var formName = childForm.GetType().Name;
                if (!_recAllowedForms.Contains(formName)) return;
            }

            if (activeForm != null) activeForm.Close();

            if (btnSender != null) HighlightButton(btnSender);

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

        // === Nút menu ===
        private void btnRoom_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmBooking(_currentUser), sender);
        }

        private void btnService_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmService(), sender);
        }

        private void btnRoomManager_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmRoomManager(), sender);
        }

        private void btnCustomerManage_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmCustomer(), sender);
        }

        private void btnUserManage_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmUser(), sender);
        }

        private void btnDashboardManage_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmStatistical(), sender);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            contextMenuProfile.Show(pictureBox1, new Point(0, pictureBox1.Height));
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

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
                MessageBox.Show("Không tìm thấy thông tin người dùng.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (frmProfile profileForm = new frmProfile(_currentUser))
            {
                var result = profileForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    label1.Text = $"Xin chào, {_currentUser.FullName}";
                }
            }
        }

        private void btnChangepass_Click(object sender, EventArgs e)
        {
            using (FrmChangePass changePassForm = new FrmChangePass(_currentUser))
            {
                changePassForm.ShowDialog();
            }
        }

        private void btnInvoicesManage_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmInvoiceManage(this), sender);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmReport(), sender);
        }
        public void ActivateBooking(bool goToUcBookRoom = true)
        {
            // mở frmBooking nếu chưa mở
            var booking = panelDesktop.Controls.OfType<frmBooking>().FirstOrDefault();
            if (booking == null || booking.IsDisposed)
            {
                OpenChildForm(new frmBooking(_currentUser), btnRoom);
                booking = panelDesktop.Controls.OfType<frmBooking>().FirstOrDefault();
            }

            if (booking != null)
            {
                if (goToUcBookRoom) booking.ShowUcBookRoom();
                else booking.ShowUcBookingRoom(); // <- vào thẳng UcBookingRoom
            }
        }

        public void NavigateToBookingUc(int? customerId = null, string idNumber = null)
        {
            // Nếu frmBooking đang mở → chỉ cần yêu cầu nó show UcBookRoom
            if (activeForm is frmBooking fbExisting)
            {
                try { fbExisting.ShowUcBookRoom(customerId, idNumber); } catch { }
                HighlightButton(btnRoom);
                lblTitle.Text = "Chọn phòng để đặt";
                return;
            }

            // Chưa có → mở mới rồi show UC
            var fb = new frmBooking(_currentUser);
            OpenChildForm(fb, btnRoom);
            try { fb.ShowUcBookRoom(customerId, idNumber); } catch { }
            lblTitle.Text = "Chọn phòng để đặt";
        }

    }
}
