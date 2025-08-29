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
    public partial class Form1 : Form
    {
        private Form activeForm = null;   // Form con đang hiển thị
        private Size originalSize;        // Kích thước gốc của form
        private Dictionary<Control, Rectangle> controlsOriginalBounds; // Lưu kích thước gốc của controls
        private Panel panelIndicator;
        private Button currentButton = null;

        public Form1()
        {
            InitializeComponent();

            this.MaximizeBox = true;
            this.FormBorderStyle = FormBorderStyle.Sizable;


            // Lưu size gốc
            originalSize = this.Size;
            controlsOriginalBounds = new Dictionary<Control, Rectangle>();
            SaveControlBounds(this);

            this.Resize += Form1_Resize;

            // Tạo thanh indicator
            panelIndicator = new Panel();
            panelIndicator.Size = new Size(3, 0); // rộng 3px, cao tùy theo button
            panelIndicator.BackColor = Color.White; // màu nổi bật
            panelIndicator.Visible = false;
            panelMenu.Controls.Add(panelIndicator);
        }

        // Lưu bounds gốc của control và con của nó
        private void SaveControlBounds(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                // Bỏ qua panelIndicator để nó không bị scale
                if (ctrl == panelIndicator) continue;

                controlsOriginalBounds[ctrl] = ctrl.Bounds;
                if (ctrl.Controls.Count > 0)
                {
                    SaveControlBounds(ctrl);
                }
            }
        }


        // Khi resize form → scale toàn bộ controls
        private void Form1_Resize(object sender, EventArgs e)
        {
            float scaleX = (float)this.Width / originalSize.Width;
            float scaleY = (float)this.Height / originalSize.Height;

            ResizeControls(scaleX, scaleY, this);

            // cập nhật lại indicator nếu đang có button được chọn
            if (currentButton != null)
            {
                HighlightButton(currentButton);
            }
        }

        // Hàm scale tất cả control dựa trên tỉ lệ
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

        // Reset tất cả button về màu mặc định
        private void ResetButtonColors()
        {
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.BackColor = Color.FromArgb(43, 47, 55); // màu gốc
                    btn.ForeColor = Color.Gainsboro;
                }
            }
        }

        // Đổi màu button đang được chọn
        private void HighlightButton(object sender)
        {
            if (sender is Button btn)
            {
                ResetButtonColors(); // reset lại toàn bộ trước
                currentButton = btn; // lưu lại button hiện tại

                // đổi màu cho button được chọn
                btn.BackColor = Color.FromArgb(14, 26, 28);
                btn.ForeColor = Color.Gainsboro;

                // panelIndicator bám sát bên phải
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


        // Hàm mở form con vào panelDesktop
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();   // đóng form cũ nếu có

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



        private void btnDashboard_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmDashboard(), sender);
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

        private void btnSetting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.frmSetting(), sender);  
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            contextMenuProfile.Show(pictureBox1, new Point(0, pictureBox1.Height));
        }
    }
}
