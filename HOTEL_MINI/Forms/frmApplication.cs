// File: frmApplication.cs  (CODE-BEHIND)
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

        // Role constants khớp DB
        private const int ROLE_ADMIN = 1;
        private const int ROLE_RECEPTIONIST = 2;

        // Danh sách form mà non-admin được phép mở
        private readonly HashSet<string> _recAllowedForms = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "frmRoom",          // Quản lý đặt phòng (tạm map frmRoom)
            "frmService",       // Quản lý dịch vụ
            "frmRoomManager",   // Quản lý phòng
            "frmCustomer"       // Quản lý khách hàng
            // Nếu bạn có frmBooking riêng: "frmBooking", "frmBookingManager"
        };

        public frmApplication(User user)
        {
            InitializeComponent();
            _currentUser = user ?? new User();

            // BẮT BUỘC: _currentUser.RoleID phải được AuthService fill ra từ Users.RoleID
            // Gợi ý kiểm tra nhanh khi debug:
            // MessageBox.Show($"RoleID={_currentUser.RoleID}");

            this.MaximizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            originalSize = this.Size;
            controlsOriginalBounds = new Dictionary<Control, Rectangle>();
            SaveControlBounds(this);
            this.Resize += Form1_Resize;

            panelIndicator = new Panel
            {
                Size = new Size(3, 0),
                BackColor = Color.White,
                Visible = false
            };
            panelMenu.Controls.Add(panelIndicator);

            label1.Text = $"Xin chào, {_currentUser.FullName}";

            // Áp quyền, ẩn hẳn nút không có quyền
            ApplyRolePermissions();

            // Mở form mặc định theo quyền
            if (IsAdmin())
            {
                //OpenChildForm(new frmDashboard(), btnDashboardManage);
                OpenChildForm(new frmStatistical(), btnDashboardManage);
            }
            else
            {
                OpenChildForm(new frmRoom(this), btnRoom);
            }
        }

        public User GetCurrentUser() => _currentUser;

        // ==========================
        // Role check: chỉ dùng RoleID
        // ==========================
        private bool IsAdmin() => _currentUser != null && _currentUser.Role == ROLE_ADMIN;
        private bool IsReceptionist() => _currentUser != null && _currentUser.Role == ROLE_RECEPTIONIST;

        // ==========================
        // Resize layout helpers
        // ==========================
        private void SaveControlBounds(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl == panelIndicator) continue;
                controlsOriginalBounds[ctrl] = ctrl.Bounds;
                if (ctrl.Controls.Count > 0) SaveControlBounds(ctrl);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            float scaleX = (float)this.Width / originalSize.Width;
            float scaleY = (float)this.Height / originalSize.Height;

            ResizeControls(scaleX, scaleY, this);

            if (currentButton != null) HighlightButton(currentButton);
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

                if (ctrl.Controls.Count > 0) ResizeControls(scaleX, scaleY, ctrl);
            }
        }

        // ==========================
        // RBAC hiển thị menu
        // ==========================
        private void ApplyRolePermissions()
        {
            if (IsAdmin())
            {
                // Admin: thấy tất cả, gồm Dashboard & User
                foreach (Control c in panelMenu.Controls)
                    if (c is Button b) b.Visible = true;
            }
            else
            {
                // Non-admin: ẩn hết trước
                foreach (Control c in panelMenu.Controls)
                    if (c is Button b) b.Visible = false;

                // Chỉ để đúng 4 nút
                SafeShow(btnRoom, true);
                SafeShow(btnService, true);
                SafeShow(btnRoomManager, true);
                SafeShow(btnCustomerManage, true);

                // Ẩn hẳn 2 nút này cho non-admin
                SafeShow(btnDashboardManage, false);
                SafeShow(btnUserManage, false);

                panelIndicator.Visible = false;
            }

            // Với DockStyle.Top, Visible=false tự co layout; chỉ cần đảm bảo không highlight nút ẩn
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

        // ==========================
        // Mở form con (kèm kiểm tra quyền)
        // ==========================
        public void OpenChildForm(Form childForm, object btnSender)
        {
            // Chặn non-admin mở form ngoài danh sách cho phép
            if (!IsAdmin())
            {
                var formName = childForm.GetType().Name;
                if (!_recAllowedForms.Contains(formName))
                    return; // im lặng bỏ qua
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

        // ==========================
        // Menu click handlers
        // ==========================
        private void btnRoom_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmRoom(this), sender);
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
            OpenChildForm(new Forms.frmStatistical(), sender);
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
    }
}
