namespace HOTEL_MINI.Forms
{
    partial class frmBookingPopup
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
            this.gbxRoomInfor = new System.Windows.Forms.GroupBox();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.txtDiachi = new System.Windows.Forms.TextBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.dtpCheckoutTime = new System.Windows.Forms.DateTimePicker();
            this.dtpCheckinTime = new System.Windows.Forms.DateTimePicker();
            this.lblCHECKOUT = new System.Windows.Forms.Label();
            this.lblCHECKIN = new System.Windows.Forms.Label();
            this.lblKieuThue = new System.Windows.Forms.Label();
            this.cbxPricingType = new System.Windows.Forms.ComboBox();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblDiachi = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBookConfirm = new System.Windows.Forms.Button();
            this.btnCheckExistCCCD = new System.Windows.Forms.Button();
            this.lblRoomNumber = new System.Windows.Forms.Label();
            this.rdbDattruoc = new System.Windows.Forms.RadioButton();
            this.rdbCheckinNow = new System.Windows.Forms.RadioButton();
            this.gbxRoomInfor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxRoomInfor
            // 
            this.gbxRoomInfor.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gbxRoomInfor.Controls.Add(this.rdbCheckinNow);
            this.gbxRoomInfor.Controls.Add(this.rdbDattruoc);
            this.gbxRoomInfor.Controls.Add(this.lblRoomNumber);
            this.gbxRoomInfor.Controls.Add(this.btnCheckExistCCCD);
            this.gbxRoomInfor.Controls.Add(this.btnBookConfirm);
            this.gbxRoomInfor.Controls.Add(this.btnCancel);
            this.gbxRoomInfor.Controls.Add(this.txtGender);
            this.gbxRoomInfor.Controls.Add(this.txtCCCD);
            this.gbxRoomInfor.Controls.Add(this.txtEmail);
            this.gbxRoomInfor.Controls.Add(this.txtSDT);
            this.gbxRoomInfor.Controls.Add(this.txtDiachi);
            this.gbxRoomInfor.Controls.Add(this.txtTen);
            this.gbxRoomInfor.Controls.Add(this.lblNote);
            this.gbxRoomInfor.Controls.Add(this.txtNote);
            this.gbxRoomInfor.Controls.Add(this.dtpCheckoutTime);
            this.gbxRoomInfor.Controls.Add(this.dtpCheckinTime);
            this.gbxRoomInfor.Controls.Add(this.lblCHECKOUT);
            this.gbxRoomInfor.Controls.Add(this.lblCHECKIN);
            this.gbxRoomInfor.Controls.Add(this.lblKieuThue);
            this.gbxRoomInfor.Controls.Add(this.cbxPricingType);
            this.gbxRoomInfor.Controls.Add(this.lblSex);
            this.gbxRoomInfor.Controls.Add(this.lblCCCD);
            this.gbxRoomInfor.Controls.Add(this.lblEmail);
            this.gbxRoomInfor.Controls.Add(this.lblSDT);
            this.gbxRoomInfor.Controls.Add(this.lblDiachi);
            this.gbxRoomInfor.Controls.Add(this.lblTen);
            this.gbxRoomInfor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxRoomInfor.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxRoomInfor.Location = new System.Drawing.Point(0, 0);
            this.gbxRoomInfor.Name = "gbxRoomInfor";
            this.gbxRoomInfor.Size = new System.Drawing.Size(1190, 1608);
            this.gbxRoomInfor.TabIndex = 6;
            this.gbxRoomInfor.TabStop = false;
            this.gbxRoomInfor.Text = "Thông tin ";
            // 
            // txtGender
            // 
            this.txtGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGender.Location = new System.Drawing.Point(128, 420);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(479, 35);
            this.txtGender.TabIndex = 19;
            // 
            // txtCCCD
            // 
            this.txtCCCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCCD.Location = new System.Drawing.Point(146, 168);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(276, 35);
            this.txtCCCD.TabIndex = 18;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(128, 379);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(278, 35);
            this.txtEmail.TabIndex = 17;
            // 
            // txtSDT
            // 
            this.txtSDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSDT.Location = new System.Drawing.Point(130, 328);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(479, 35);
            this.txtSDT.TabIndex = 16;
            // 
            // txtDiachi
            // 
            this.txtDiachi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiachi.Location = new System.Drawing.Point(130, 281);
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.Size = new System.Drawing.Size(479, 35);
            this.txtDiachi.TabIndex = 15;
            // 
            // txtTen
            // 
            this.txtTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTen.Location = new System.Drawing.Point(130, 233);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(479, 35);
            this.txtTen.TabIndex = 14;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(49, 716);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(61, 22);
            this.lblNote.TabIndex = 13;
            this.lblNote.Text = "NOTE";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(130, 716);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(479, 133);
            this.txtNote.TabIndex = 12;
            // 
            // dtpCheckoutTime
            // 
            this.dtpCheckoutTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckoutTime.Location = new System.Drawing.Point(210, 651);
            this.dtpCheckoutTime.Name = "dtpCheckoutTime";
            this.dtpCheckoutTime.Size = new System.Drawing.Size(399, 30);
            this.dtpCheckoutTime.TabIndex = 11;
            // 
            // dtpCheckinTime
            // 
            this.dtpCheckinTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckinTime.Location = new System.Drawing.Point(210, 603);
            this.dtpCheckinTime.Name = "dtpCheckinTime";
            this.dtpCheckinTime.Size = new System.Drawing.Size(399, 30);
            this.dtpCheckinTime.TabIndex = 10;
            // 
            // lblCHECKOUT
            // 
            this.lblCHECKOUT.AutoSize = true;
            this.lblCHECKOUT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCHECKOUT.Location = new System.Drawing.Point(49, 659);
            this.lblCHECKOUT.Name = "lblCHECKOUT";
            this.lblCHECKOUT.Size = new System.Drawing.Size(137, 22);
            this.lblCHECKOUT.TabIndex = 9;
            this.lblCHECKOUT.Text = "Check-out Time";
            // 
            // lblCHECKIN
            // 
            this.lblCHECKIN.AutoSize = true;
            this.lblCHECKIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCHECKIN.Location = new System.Drawing.Point(49, 611);
            this.lblCHECKIN.Name = "lblCHECKIN";
            this.lblCHECKIN.Size = new System.Drawing.Size(126, 22);
            this.lblCHECKIN.TabIndex = 8;
            this.lblCHECKIN.Text = "Check-in Time";
            // 
            // lblKieuThue
            // 
            this.lblKieuThue.AutoSize = true;
            this.lblKieuThue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKieuThue.Location = new System.Drawing.Point(41, 509);
            this.lblKieuThue.Name = "lblKieuThue";
            this.lblKieuThue.Size = new System.Drawing.Size(86, 22);
            this.lblKieuThue.TabIndex = 7;
            this.lblKieuThue.Text = "Kiểu thuê";
            // 
            // cbxPricingType
            // 
            this.cbxPricingType.FormattingEnabled = true;
            this.cbxPricingType.Location = new System.Drawing.Point(146, 509);
            this.cbxPricingType.Name = "cbxPricingType";
            this.cbxPricingType.Size = new System.Drawing.Size(121, 45);
            this.cbxPricingType.TabIndex = 6;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSex.Location = new System.Drawing.Point(41, 433);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(81, 22);
            this.lblSex.TabIndex = 5;
            this.lblSex.Text = "Giới tính:";
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCCCD.Location = new System.Drawing.Point(65, 181);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(67, 22);
            this.lblCCCD.TabIndex = 4;
            this.lblCCCD.Text = "CCCD:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(49, 387);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(59, 22);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email:";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDT.Location = new System.Drawing.Point(53, 336);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(57, 22);
            this.lblSDT.TabIndex = 2;
            this.lblSDT.Text = "SĐT: ";
            // 
            // lblDiachi
            // 
            this.lblDiachi.AutoSize = true;
            this.lblDiachi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiachi.Location = new System.Drawing.Point(46, 289);
            this.lblDiachi.Name = "lblDiachi";
            this.lblDiachi.Size = new System.Drawing.Size(70, 22);
            this.lblDiachi.TabIndex = 1;
            this.lblDiachi.Text = "Địa chỉ:";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.Location = new System.Drawing.Point(46, 243);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(47, 22);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Tên:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(134, 904);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 88);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnBookConfirm
            // 
            this.btnBookConfirm.Location = new System.Drawing.Point(517, 876);
            this.btnBookConfirm.Name = "btnBookConfirm";
            this.btnBookConfirm.Size = new System.Drawing.Size(155, 116);
            this.btnBookConfirm.TabIndex = 21;
            this.btnBookConfirm.Text = " Confirm";
            this.btnBookConfirm.UseVisualStyleBackColor = true;
            this.btnBookConfirm.Click += new System.EventHandler(this.btnBookConfirm_Click);
            // 
            // btnCheckExistCCCD
            // 
            this.btnCheckExistCCCD.Location = new System.Drawing.Point(483, 139);
            this.btnCheckExistCCCD.Name = "btnCheckExistCCCD";
            this.btnCheckExistCCCD.Size = new System.Drawing.Size(179, 88);
            this.btnCheckExistCCCD.TabIndex = 22;
            this.btnCheckExistCCCD.Text = "Check Exist";
            this.btnCheckExistCCCD.UseVisualStyleBackColor = true;
            this.btnCheckExistCCCD.Click += new System.EventHandler(this.btnCheckExistCCCD_Click);
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNumber.ForeColor = System.Drawing.Color.Magenta;
            this.lblRoomNumber.Location = new System.Drawing.Point(316, 49);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(93, 40);
            this.lblRoomNumber.TabIndex = 23;
            this.lblRoomNumber.Text = "Tên:";
            // 
            // rdbDattruoc
            // 
            this.rdbDattruoc.AutoSize = true;
            this.rdbDattruoc.Location = new System.Drawing.Point(437, 461);
            this.rdbDattruoc.Name = "rdbDattruoc";
            this.rdbDattruoc.Size = new System.Drawing.Size(235, 41);
            this.rdbDattruoc.TabIndex = 24;
            this.rdbDattruoc.TabStop = true;
            this.rdbDattruoc.Text = "radioButton1";
            this.rdbDattruoc.UseVisualStyleBackColor = true;
            this.rdbDattruoc.CheckedChanged += new System.EventHandler(this.rdbDattruoc_CheckedChanged);
            // 
            // rdbCheckinNow
            // 
            this.rdbCheckinNow.AutoSize = true;
            this.rdbCheckinNow.Location = new System.Drawing.Point(437, 530);
            this.rdbCheckinNow.Name = "rdbCheckinNow";
            this.rdbCheckinNow.Size = new System.Drawing.Size(237, 41);
            this.rdbCheckinNow.TabIndex = 25;
            this.rdbCheckinNow.TabStop = true;
            this.rdbCheckinNow.Text = "radioButton2";
            this.rdbCheckinNow.UseVisualStyleBackColor = true;
            this.rdbCheckinNow.CheckedChanged += new System.EventHandler(this.rdbCheckinNow_CheckedChanged);
            // 
            // frmBookingPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 1072);
            this.Controls.Add(this.gbxRoomInfor);
            this.Name = "frmBookingPopup";
            this.Text = "frmBookingPopup";
            this.gbxRoomInfor.ResumeLayout(false);
            this.gbxRoomInfor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxRoomInfor;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtDiachi;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.DateTimePicker dtpCheckoutTime;
        private System.Windows.Forms.DateTimePicker dtpCheckinTime;
        private System.Windows.Forms.Label lblCHECKOUT;
        private System.Windows.Forms.Label lblCHECKIN;
        private System.Windows.Forms.Label lblKieuThue;
        private System.Windows.Forms.ComboBox cbxPricingType;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblDiachi;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Button btnCheckExistCCCD;
        private System.Windows.Forms.Button btnBookConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.RadioButton rdbCheckinNow;
        private System.Windows.Forms.RadioButton rdbDattruoc;
    }
}