using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    partial class frmSelectCheckoutTime
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
            this.lblCheckin = new System.Windows.Forms.Label();
            this.lblCheckout = new System.Windows.Forms.Label();
            this.dtpCheckinTime = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckoutTime = new System.Windows.Forms.DateTimePicker();
            this.lblRoomNumber = new System.Windows.Forms.Label();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCheckin
            // 
            this.lblCheckin.AutoSize = true;
            this.lblCheckin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckin.Location = new System.Drawing.Point(12, 132);
            this.lblCheckin.Name = "lblCheckin";
            this.lblCheckin.Size = new System.Drawing.Size(106, 29);
            this.lblCheckin.TabIndex = 0;
            this.lblCheckin.Text = "Checkin:";
            // 
            // lblCheckout
            // 
            this.lblCheckout.AutoSize = true;
            this.lblCheckout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckout.Location = new System.Drawing.Point(12, 180);
            this.lblCheckout.Name = "lblCheckout";
            this.lblCheckout.Size = new System.Drawing.Size(120, 29);
            this.lblCheckout.TabIndex = 1;
            this.lblCheckout.Text = "Checkout:";
            // 
            // dtpCheckinTime
            // 
            this.dtpCheckinTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpCheckinTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckinTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckinTime.Location = new System.Drawing.Point(151, 132);
            this.dtpCheckinTime.Name = "dtpCheckinTime";
            this.dtpCheckinTime.ShowUpDown = true;
            this.dtpCheckinTime.Size = new System.Drawing.Size(275, 35);
            this.dtpCheckinTime.TabIndex = 2;
            // 
            // dtpCheckoutTime
            // 
            this.dtpCheckoutTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpCheckoutTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckoutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckoutTime.Location = new System.Drawing.Point(151, 180);
            this.dtpCheckoutTime.Name = "dtpCheckoutTime";
            this.dtpCheckoutTime.ShowUpDown = true;
            this.dtpCheckoutTime.Size = new System.Drawing.Size(275, 35);
            this.dtpCheckoutTime.TabIndex = 3;
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.BackColor = System.Drawing.Color.White;
            this.lblRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNumber.ForeColor = System.Drawing.Color.Blue;
            this.lblRoomNumber.Location = new System.Drawing.Point(186, 32);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(97, 52);
            this.lblRoomNumber.TabIndex = 4;
            this.lblRoomNumber.Text = "104";
            this.lblRoomNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Location = new System.Drawing.Point(274, 256);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(129, 51);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Xác nhận";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(70, 256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 51);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Trở lại";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmSelectCheckoutTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(462, 358);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lblRoomNumber);
            this.Controls.Add(this.dtpCheckoutTime);
            this.Controls.Add(this.dtpCheckinTime);
            this.Controls.Add(this.lblCheckout);
            this.Controls.Add(this.lblCheckin);
            this.Name = "frmSelectCheckoutTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn giờ trả phòng";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCheckin;
        private System.Windows.Forms.Label lblCheckout;
        private System.Windows.Forms.DateTimePicker dtpCheckinTime;
        private System.Windows.Forms.DateTimePicker dtpCheckoutTime;
        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
    }
}