namespace HOTEL_MINI.Forms.Controls
{
    partial class RoomCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRoomNumber = new System.Windows.Forms.Label();
            this.btnBook = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.lblRoomStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRoomNumber.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNumber.Location = new System.Drawing.Point(0, 0);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(90, 31);
            this.lblRoomNumber.TabIndex = 0;
            this.lblRoomNumber.Text = "Phòng";
            // 
            // btnBook
            // 
            this.btnBook.Location = new System.Drawing.Point(27, 189);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(88, 52);
            this.btnBook.TabIndex = 1;
            this.btnBook.Text = "Đặt";
            this.btnBook.UseVisualStyleBackColor = true;
            // 
            // btnDetail
            // 
            this.btnDetail.Location = new System.Drawing.Point(174, 189);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(88, 52);
            this.btnDetail.TabIndex = 2;
            this.btnDetail.Text = "Chi tiết";
            this.btnDetail.UseVisualStyleBackColor = true;
            // 
            // lblRoomStatus
            // 
            this.lblRoomStatus.AutoSize = true;
            this.lblRoomStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRoomStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomStatus.Location = new System.Drawing.Point(27, 95);
            this.lblRoomStatus.Name = "lblRoomStatus";
            this.lblRoomStatus.Size = new System.Drawing.Size(90, 31);
            this.lblRoomStatus.TabIndex = 3;
            this.lblRoomStatus.Text = "Phòng";
            // 
            // RoomCard
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.lblRoomStatus);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.btnBook);
            this.Controls.Add(this.lblRoomNumber);
            this.Name = "RoomCard";
            this.Size = new System.Drawing.Size(296, 265);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.Button btnBook;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.Label lblRoomStatus;
    }
}
