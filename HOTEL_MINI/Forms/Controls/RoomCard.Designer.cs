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
            this.btnDetails = new System.Windows.Forms.Button();
            this.lblRoomStatus = new System.Windows.Forms.Label();
            this.lblRoomType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNumber.Location = new System.Drawing.Point(3, 0);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(143, 48);
            this.lblRoomNumber.TabIndex = 0;
            this.lblRoomNumber.Text = "Phòng";
            // 
            // btnBook
            // 
            this.btnBook.Location = new System.Drawing.Point(34, 189);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(75, 40);
            this.btnBook.TabIndex = 1;
            this.btnBook.Text = "Đặt";
            this.btnBook.UseVisualStyleBackColor = true;
            this.btnBook.Click += new System.EventHandler(this.btnBook_Click);
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(183, 189);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(75, 40);
            this.btnDetails.TabIndex = 2;
            this.btnDetails.Text = "Chi tiết";
            this.btnDetails.UseVisualStyleBackColor = true;
            this.btnDetails.Click += new System.EventHandler(this.btnDetails_Click);
            // 
            // lblRoomStatus
            // 
            this.lblRoomStatus.AutoSize = true;
            this.lblRoomStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRoomStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomStatus.Location = new System.Drawing.Point(0, 115);
            this.lblRoomStatus.Name = "lblRoomStatus";
            this.lblRoomStatus.Size = new System.Drawing.Size(90, 31);
            this.lblRoomStatus.TabIndex = 3;
            this.lblRoomStatus.Text = "Phòng";
            // 
            // lblRoomType
            // 
            this.lblRoomType.AutoSize = true;
            this.lblRoomType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRoomType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomType.Location = new System.Drawing.Point(0, 72);
            this.lblRoomType.Name = "lblRoomType";
            this.lblRoomType.Size = new System.Drawing.Size(90, 31);
            this.lblRoomType.TabIndex = 4;
            this.lblRoomType.Text = "Phòng";
            // 
            // RoomCard
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.lblRoomType);
            this.Controls.Add(this.lblRoomStatus);
            this.Controls.Add(this.btnDetails);
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
        private System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.Label lblRoomStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRoomType;
    }
}
