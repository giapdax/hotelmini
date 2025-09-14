namespace HOTEL_MINI.Forms
{
    partial class frmStatistical
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabRevenue = new System.Windows.Forms.TabPage();
            this.tabRoom = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabRevenue);
            this.tabControl1.Controls.Add(this.tabRoom);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(2693, 1556);
            this.tabControl1.TabIndex = 0;
            // 
            // tabRevenue
            // 
            this.tabRevenue.Location = new System.Drawing.Point(4, 29);
            this.tabRevenue.Name = "tabRevenue";
            this.tabRevenue.Padding = new System.Windows.Forms.Padding(3);
            this.tabRevenue.Size = new System.Drawing.Size(2685, 1523);
            this.tabRevenue.TabIndex = 0;
            this.tabRevenue.Text = "Doanh thu";
            this.tabRevenue.UseVisualStyleBackColor = true;
            // 
            // tabRoom
            // 
            this.tabRoom.Location = new System.Drawing.Point(4, 29);
            this.tabRoom.Name = "tabRoom";
            this.tabRoom.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoom.Size = new System.Drawing.Size(2685, 1523);
            this.tabRoom.TabIndex = 1;
            this.tabRoom.Text = "Thống kê phòng";
            this.tabRoom.UseVisualStyleBackColor = true;
            // 
            // frmStatistical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1795, 1037);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmStatistical";
            this.Text = "Thống Kê";
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabRevenue;
        private System.Windows.Forms.TabPage tabRoom;
    }
}