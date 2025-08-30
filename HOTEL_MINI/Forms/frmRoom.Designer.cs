using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    partial class frmRoom
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
            this.flpAllRooms = new System.Windows.Forms.FlowLayoutPanel();
            this.cbxRoomStatus = new System.Windows.Forms.ComboBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.txtSearchRoomNumber = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlRight.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpAllRooms
            // 
            this.flpAllRooms.AutoScroll = true;
            this.flpAllRooms.AutoSize = true;
            this.flpAllRooms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpAllRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAllRooms.Location = new System.Drawing.Point(0, 0);
            this.flpAllRooms.Name = "flpAllRooms";
            this.flpAllRooms.Size = new System.Drawing.Size(670, 450);
            this.flpAllRooms.TabIndex = 0;
            // 
            // cbxRoomStatus
            // 
            this.cbxRoomStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRoomStatus.FormattingEnabled = true;
            this.cbxRoomStatus.Location = new System.Drawing.Point(6, 12);
            this.cbxRoomStatus.Name = "cbxRoomStatus";
            this.cbxRoomStatus.Size = new System.Drawing.Size(113, 37);
            this.cbxRoomStatus.TabIndex = 1;
            this.cbxRoomStatus.SelectedIndexChanged += new System.EventHandler(this.cbxRoomStatus_SelectedIndexChanged);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnlRight.Controls.Add(this.btnReset);
            this.pnlRight.Controls.Add(this.txtSearchRoomNumber);
            this.pnlRight.Controls.Add(this.cbxRoomStatus);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(670, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(130, 450);
            this.pnlRight.TabIndex = 2;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlMain.Controls.Add(this.flpAllRooms);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(670, 450);
            this.pnlMain.TabIndex = 3;
            // 
            // txtSearchRoomNumber
            // 
            this.txtSearchRoomNumber.Location = new System.Drawing.Point(6, 75);
            this.txtSearchRoomNumber.Name = "txtSearchRoomNumber";
            this.txtSearchRoomNumber.Size = new System.Drawing.Size(100, 26);
            this.txtSearchRoomNumber.TabIndex = 2;
            this.txtSearchRoomNumber.TextChanged += new System.EventHandler(this.txtSearchRoomNumber_TextChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(37, 128);
            this.btnReset.Name = "btnResetFilter";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "button1";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnResetFilter_Click);
            // 
            // frmRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlRight);
            this.Name = "frmRoom";
            this.Text = "Quản Lý Phòng";
            this.pnlRight.ResumeLayout(false);
            this.pnlRight.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpAllRooms;
        private System.Windows.Forms.ComboBox cbxRoomStatus;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlMain;
        private Button btnReset;
        private TextBox txtSearchRoomNumber;
    }
}