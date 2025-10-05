namespace HOTEL_MINI.Forms
{
    partial class frmBooking
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
            this.tabBooking = new System.Windows.Forms.TabControl();
            this.btnBookRoom = new System.Windows.Forms.TabPage();
            this.btnBookingRoom = new System.Windows.Forms.TabPage();
            this.tabBooking.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabBooking
            // 
            this.tabBooking.Controls.Add(this.btnBookRoom);
            this.tabBooking.Controls.Add(this.btnBookingRoom);
            this.tabBooking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabBooking.Location = new System.Drawing.Point(0, 0);
            this.tabBooking.Name = "tabBooking";
            this.tabBooking.SelectedIndex = 0;
            this.tabBooking.Size = new System.Drawing.Size(1048, 546);
            this.tabBooking.TabIndex = 0;
            // 
            // btnBookRoom
            // 
            this.btnBookRoom.Location = new System.Drawing.Point(4, 29);
            this.btnBookRoom.Name = "btnBookRoom";
            this.btnBookRoom.Padding = new System.Windows.Forms.Padding(3);
            this.btnBookRoom.Size = new System.Drawing.Size(1040, 513);
            this.btnBookRoom.TabIndex = 0;
            this.btnBookRoom.Text = "Đặt phòng";
            this.btnBookRoom.UseVisualStyleBackColor = true;
            // 
            // btnBookingRoom
            // 
            this.btnBookingRoom.Location = new System.Drawing.Point(4, 29);
            this.btnBookingRoom.Name = "btnBookingRoom";
            this.btnBookingRoom.Padding = new System.Windows.Forms.Padding(3);
            this.btnBookingRoom.Size = new System.Drawing.Size(1040, 513);
            this.btnBookingRoom.TabIndex = 1;
            this.btnBookingRoom.Text = "Quản lý đơn đặt phòng";
            this.btnBookingRoom.UseVisualStyleBackColor = true;
            // 
            // frmBooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 546);
            this.Controls.Add(this.tabBooking);
            this.Name = "frmBooking";
            this.Text = "Quản lý đặt phòng và đơn đặt phòng";
            this.tabBooking.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabBooking;
        private System.Windows.Forms.TabPage btnBookRoom;
        private System.Windows.Forms.TabPage btnBookingRoom;
    }
}