namespace HOTEL_MINI
{
    partial class frmApplication
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApplication));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.contextMenuProfile = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnUserManage = new System.Windows.Forms.Button();
            this.btnDashboardManage = new System.Windows.Forms.Button();
            this.btnInvoicesManage = new System.Windows.Forms.Button();
            this.btnCustomerManage = new System.Windows.Forms.Button();
            this.btnRoomManager = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnService = new System.Windows.Forms.Button();
            this.btnRoom = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.btnChangepass = new System.Windows.Forms.ToolStripMenuItem();
            this.panelMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.contextMenuProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.panelMenu.Controls.Add(this.btnUserManage);
            this.panelMenu.Controls.Add(this.btnDashboardManage);
            this.panelMenu.Controls.Add(this.btnInvoicesManage);
            this.panelMenu.Controls.Add(this.btnCustomerManage);
            this.panelMenu.Controls.Add(this.btnRoomManager);
            this.panelMenu.Controls.Add(this.panel1);
            this.panelMenu.Controls.Add(this.btnService);
            this.panelMenu.Controls.Add(this.btnRoom);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.MinimumSize = new System.Drawing.Size(220, 583);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 666);
            this.panelMenu.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 598);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 68);
            this.panel1.TabIndex = 6;
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(41)))), ((int)(((byte)(47)))));
            this.panelLogo.Controls.Add(this.label1);
            this.panelLogo.Controls.Add(this.picLogo);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.MinimumSize = new System.Drawing.Size(220, 100);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(220, 142);
            this.panelLogo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.label1.Location = new System.Drawing.Point(28, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Xin Chào, Đình Giáp!";
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.panelTitleBar.Controls.Add(this.pictureBox1);
            this.panelTitleBar.Controls.Add(this.lblTitle);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(220, 0);
            this.panelTitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1070, 64);
            this.panelTitleBar.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblTitle.Location = new System.Drawing.Point(492, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(85, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TITLE";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelDesktop
            // 
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(220, 64);
            this.panelDesktop.MinimumSize = new System.Drawing.Size(590, 375);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(1070, 602);
            this.panelDesktop.TabIndex = 2;
            // 
            // contextMenuProfile
            // 
            this.contextMenuProfile.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuProfile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLogout,
            this.btnProfile,
            this.btnChangepass});
            this.contextMenuProfile.Name = "contextMenuStrip1";
            this.contextMenuProfile.Size = new System.Drawing.Size(240, 100);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = global::HOTEL_MINI.Properties.Resources.menus;
            this.pictureBox1.Location = new System.Drawing.Point(1003, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(67, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnUserManage
            // 
            this.btnUserManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnUserManage.FlatAppearance.BorderSize = 0;
            this.btnUserManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUserManage.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUserManage.ForeColor = System.Drawing.Color.Transparent;
            this.btnUserManage.Image = global::HOTEL_MINI.Properties.Resources.about__1_;
            this.btnUserManage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserManage.Location = new System.Drawing.Point(0, 502);
            this.btnUserManage.Name = "btnUserManage";
            this.btnUserManage.Size = new System.Drawing.Size(220, 60);
            this.btnUserManage.TabIndex = 14;
            this.btnUserManage.Text = " Quản Lý User";
            this.btnUserManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUserManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUserManage.UseVisualStyleBackColor = true;
            this.btnUserManage.Click += new System.EventHandler(this.btnUserManage_Click);
            // 
            // btnDashboardManage
            // 
            this.btnDashboardManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboardManage.FlatAppearance.BorderSize = 0;
            this.btnDashboardManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboardManage.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboardManage.ForeColor = System.Drawing.Color.Transparent;
            this.btnDashboardManage.Image = global::HOTEL_MINI.Properties.Resources.report;
            this.btnDashboardManage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboardManage.Location = new System.Drawing.Point(0, 442);
            this.btnDashboardManage.Name = "btnDashboardManage";
            this.btnDashboardManage.Size = new System.Drawing.Size(220, 60);
            this.btnDashboardManage.TabIndex = 13;
            this.btnDashboardManage.Text = " Thống Kê";
            this.btnDashboardManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboardManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDashboardManage.UseVisualStyleBackColor = true;
            this.btnDashboardManage.Click += new System.EventHandler(this.btnDashboardManage_Click);
            // 
            // btnInvoicesManage
            // 
            this.btnInvoicesManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInvoicesManage.FlatAppearance.BorderSize = 0;
            this.btnInvoicesManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvoicesManage.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoicesManage.ForeColor = System.Drawing.Color.Transparent;
            this.btnInvoicesManage.Image = global::HOTEL_MINI.Properties.Resources.icons8_invoice_32;
            this.btnInvoicesManage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInvoicesManage.Location = new System.Drawing.Point(0, 382);
            this.btnInvoicesManage.Name = "btnInvoicesManage";
            this.btnInvoicesManage.Size = new System.Drawing.Size(220, 60);
            this.btnInvoicesManage.TabIndex = 12;
            this.btnInvoicesManage.Text = "Quản Lý Hóa Đơn";
            this.btnInvoicesManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInvoicesManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInvoicesManage.UseVisualStyleBackColor = true;
            this.btnInvoicesManage.Click += new System.EventHandler(this.btnInvoicesManage_Click);
            // 
            // btnCustomerManage
            // 
            this.btnCustomerManage.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomerManage.FlatAppearance.BorderSize = 0;
            this.btnCustomerManage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomerManage.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomerManage.ForeColor = System.Drawing.Color.Transparent;
            this.btnCustomerManage.Image = global::HOTEL_MINI.Properties.Resources.icons8_customer_32;
            this.btnCustomerManage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomerManage.Location = new System.Drawing.Point(0, 322);
            this.btnCustomerManage.Name = "btnCustomerManage";
            this.btnCustomerManage.Size = new System.Drawing.Size(220, 60);
            this.btnCustomerManage.TabIndex = 9;
            this.btnCustomerManage.Text = " Quản Lý KH";
            this.btnCustomerManage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomerManage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCustomerManage.UseVisualStyleBackColor = true;
            this.btnCustomerManage.Click += new System.EventHandler(this.btnCustomerManage_Click);
            // 
            // btnRoomManager
            // 
            this.btnRoomManager.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRoomManager.FlatAppearance.BorderSize = 0;
            this.btnRoomManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomManager.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomManager.ForeColor = System.Drawing.Color.Transparent;
            this.btnRoomManager.Image = global::HOTEL_MINI.Properties.Resources.icons8_double_bed_32__2_;
            this.btnRoomManager.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoomManager.Location = new System.Drawing.Point(0, 262);
            this.btnRoomManager.Name = "btnRoomManager";
            this.btnRoomManager.Size = new System.Drawing.Size(220, 60);
            this.btnRoomManager.TabIndex = 8;
            this.btnRoomManager.Text = " Quản Lý Phòng";
            this.btnRoomManager.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoomManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRoomManager.UseVisualStyleBackColor = true;
            this.btnRoomManager.Click += new System.EventHandler(this.btnRoomManager_Click);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(8)))), ((int)(((byte)(20)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = global::HOTEL_MINI.Properties.Resources.exit;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(220, 80);
            this.btnExit.TabIndex = 5;
            this.btnExit.Text = "   Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnService
            // 
            this.btnService.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnService.FlatAppearance.BorderSize = 0;
            this.btnService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnService.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnService.ForeColor = System.Drawing.Color.Transparent;
            this.btnService.Image = global::HOTEL_MINI.Properties.Resources.cleaning__1_;
            this.btnService.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnService.Location = new System.Drawing.Point(0, 202);
            this.btnService.Name = "btnService";
            this.btnService.Size = new System.Drawing.Size(220, 60);
            this.btnService.TabIndex = 3;
            this.btnService.Text = " Quản Lý Dịch Vụ";
            this.btnService.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnService.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnService.UseVisualStyleBackColor = true;
            this.btnService.Click += new System.EventHandler(this.btnService_Click);
            // 
            // btnRoom
            // 
            this.btnRoom.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRoom.FlatAppearance.BorderSize = 0;
            this.btnRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoom.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoom.ForeColor = System.Drawing.Color.Transparent;
            this.btnRoom.Image = global::HOTEL_MINI.Properties.Resources.hotel__1_;
            this.btnRoom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoom.Location = new System.Drawing.Point(0, 142);
            this.btnRoom.Name = "btnRoom";
            this.btnRoom.Size = new System.Drawing.Size(220, 60);
            this.btnRoom.TabIndex = 2;
            this.btnRoom.Text = " Quản Lý Đặt Phòng";
            this.btnRoom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRoom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRoom.UseVisualStyleBackColor = true;
            this.btnRoom.Click += new System.EventHandler(this.btnRoom_Click);
            // 
            // picLogo
            // 
            this.picLogo.Image = global::HOTEL_MINI.Properties.Resources.user;
            this.picLogo.Location = new System.Drawing.Point(32, -10);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(129, 117);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Image = global::HOTEL_MINI.Properties.Resources.power_off__1_;
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(239, 32);
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnProfile.ForeColor = System.Drawing.Color.White;
            this.btnProfile.Image = global::HOTEL_MINI.Properties.Resources.user__2_;
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(239, 32);
            this.btnProfile.Text = "Thông tin cá nhân";
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // btnChangepass
            // 
            this.btnChangepass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnChangepass.ForeColor = System.Drawing.Color.White;
            this.btnChangepass.Image = global::HOTEL_MINI.Properties.Resources.icons8_password_32;
            this.btnChangepass.Name = "btnChangepass";
            this.btnChangepass.Size = new System.Drawing.Size(239, 32);
            this.btnChangepass.Text = "Thay đổi mật khẩu";
            this.btnChangepass.Click += new System.EventHandler(this.btnChangepass_Click);
            // 
            // frmApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1290, 666);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.MinimumSize = new System.Drawing.Size(1312, 722);
            this.Name = "frmApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ỨNG DỤNG QUẢN LÝ HOTEL MINI";
            this.panelMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.contextMenuProfile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnService;
        private System.Windows.Forms.Button btnRoom;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuProfile;
        private System.Windows.Forms.ToolStripMenuItem btnLogout;
        private System.Windows.Forms.ToolStripMenuItem btnProfile;
        private System.Windows.Forms.Button btnRoomManager;
        private System.Windows.Forms.Button btnCustomerManage;
        private System.Windows.Forms.ToolStripMenuItem btnChangepass;
        private System.Windows.Forms.Button btnUserManage;
        private System.Windows.Forms.Button btnDashboardManage;
        private System.Windows.Forms.Button btnInvoicesManage;
    }
}

