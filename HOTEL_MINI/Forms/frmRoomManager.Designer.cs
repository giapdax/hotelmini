using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    partial class frmRoomManager
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabRooms = new System.Windows.Forms.TabPage();
            this.tabRoomTypePricing = new System.Windows.Forms.TabPage();
            this.tabControlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabRooms);
            this.tabControlMain.Controls.Add(this.tabRoomTypePricing);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1048, 546);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabRooms
            // 
            this.tabRooms.BackColor = System.Drawing.Color.Thistle;
            this.tabRooms.Location = new System.Drawing.Point(4, 29);
            this.tabRooms.Name = "tabRooms";
            this.tabRooms.Padding = new System.Windows.Forms.Padding(3);
            this.tabRooms.Size = new System.Drawing.Size(1040, 513);
            this.tabRooms.TabIndex = 0;
            this.tabRooms.Text = "Quản Lý Phòng";
            // 
            // tabRoomTypePricing
            // 
            this.tabRoomTypePricing.Location = new System.Drawing.Point(4, 29);
            this.tabRoomTypePricing.Name = "tabRoomTypePricing";
            this.tabRoomTypePricing.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoomTypePricing.Size = new System.Drawing.Size(1040, 513);
            this.tabRoomTypePricing.TabIndex = 1;
            this.tabRoomTypePricing.Text = "Quản lý loại phòng và giá";
            this.tabRoomTypePricing.UseVisualStyleBackColor = true;
            // 
            // frmRoomManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 546);
            this.Controls.Add(this.tabControlMain);
            this.Name = "frmRoomManager";
            this.Text = "Quản Lý Phòng";
            this.tabControlMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.TabControl tabControlMain;   // ĐÚNG: TabControl
        private System.Windows.Forms.TabPage tabRooms;            // ĐÚNG: TabPage
        private System.Windows.Forms.TabPage tabRoomTypePricing;  // ĐÚNG: TabPage
    }
}
