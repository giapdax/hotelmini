namespace HOTEL_MINI.Forms
{
    partial class frmBookingDetail
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
            this.lblRoomNumber = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbxServicesMenu = new System.Windows.Forms.GroupBox();
            this.btnIncrease = new System.Windows.Forms.Button();
            this.numIncrease = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.lbxHotelServices = new System.Windows.Forms.ListBox();
            this.gbxUsedServices = new System.Windows.Forms.GroupBox();
            this.btnReduce = new System.Windows.Forms.Button();
            this.numReduce = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.lbxUsedServices = new System.Windows.Forms.ListBox();
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
            this.txtPricingType = new System.Windows.Forms.TextBox();
            this.gbxServicesMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIncrease)).BeginInit();
            this.gbxUsedServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReduce)).BeginInit();
            this.gbxRoomInfor.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNumber.ForeColor = System.Drawing.Color.Red;
            this.lblRoomNumber.Location = new System.Drawing.Point(792, 51);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(273, 67);
            this.lblRoomNumber.TabIndex = 9;
            this.lblRoomNumber.Text = "Phòng 101";
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(80, 70);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(95, 31);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Trở lại";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // gbxServicesMenu
            // 
            this.gbxServicesMenu.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gbxServicesMenu.Controls.Add(this.btnIncrease);
            this.gbxServicesMenu.Controls.Add(this.numIncrease);
            this.gbxServicesMenu.Controls.Add(this.label12);
            this.gbxServicesMenu.Controls.Add(this.lbxHotelServices);
            this.gbxServicesMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxServicesMenu.Location = new System.Drawing.Point(1168, 134);
            this.gbxServicesMenu.Name = "gbxServicesMenu";
            this.gbxServicesMenu.Size = new System.Drawing.Size(392, 689);
            this.gbxServicesMenu.TabIndex = 7;
            this.gbxServicesMenu.TabStop = false;
            this.gbxServicesMenu.Text = "Menu dịch vụ";
            // 
            // btnIncrease
            // 
            this.btnIncrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncrease.Location = new System.Drawing.Point(273, 607);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(91, 39);
            this.btnIncrease.TabIndex = 19;
            this.btnIncrease.Text = "Thêm";
            this.btnIncrease.UseVisualStyleBackColor = true;
            // 
            // numIncrease
            // 
            this.numIncrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numIncrease.Location = new System.Drawing.Point(125, 611);
            this.numIncrease.Name = "numIncrease";
            this.numIncrease.Size = new System.Drawing.Size(78, 35);
            this.numIncrease.TabIndex = 18;
            this.numIncrease.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(39, 620);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 20);
            this.label12.TabIndex = 17;
            this.label12.Text = "Số lượng";
            // 
            // lbxHotelServices
            // 
            this.lbxHotelServices.FormattingEnabled = true;
            this.lbxHotelServices.ItemHeight = 37;
            this.lbxHotelServices.Location = new System.Drawing.Point(19, 43);
            this.lbxHotelServices.Name = "lbxHotelServices";
            this.lbxHotelServices.Size = new System.Drawing.Size(355, 522);
            this.lbxHotelServices.TabIndex = 16;
            // 
            // gbxUsedServices
            // 
            this.gbxUsedServices.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gbxUsedServices.Controls.Add(this.btnReduce);
            this.gbxUsedServices.Controls.Add(this.numReduce);
            this.gbxUsedServices.Controls.Add(this.label11);
            this.gbxUsedServices.Controls.Add(this.lbxUsedServices);
            this.gbxUsedServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxUsedServices.Location = new System.Drawing.Point(719, 134);
            this.gbxUsedServices.Name = "gbxUsedServices";
            this.gbxUsedServices.Size = new System.Drawing.Size(395, 689);
            this.gbxUsedServices.TabIndex = 6;
            this.gbxUsedServices.TabStop = false;
            this.gbxUsedServices.Text = "Dịch vụ đang dùng";
            // 
            // btnReduce
            // 
            this.btnReduce.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReduce.Location = new System.Drawing.Point(266, 611);
            this.btnReduce.Name = "btnReduce";
            this.btnReduce.Size = new System.Drawing.Size(91, 39);
            this.btnReduce.TabIndex = 15;
            this.btnReduce.Text = "Bớt";
            this.btnReduce.UseVisualStyleBackColor = true;
            // 
            // numReduce
            // 
            this.numReduce.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numReduce.Location = new System.Drawing.Point(118, 615);
            this.numReduce.Name = "numReduce";
            this.numReduce.Size = new System.Drawing.Size(78, 35);
            this.numReduce.TabIndex = 14;
            this.numReduce.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(32, 624);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 20);
            this.label11.TabIndex = 1;
            this.label11.Text = "Số lượng";
            // 
            // lbxUsedServices
            // 
            this.lbxUsedServices.FormattingEnabled = true;
            this.lbxUsedServices.ItemHeight = 37;
            this.lbxUsedServices.Location = new System.Drawing.Point(21, 43);
            this.lbxUsedServices.Name = "lbxUsedServices";
            this.lbxUsedServices.Size = new System.Drawing.Size(357, 522);
            this.lbxUsedServices.TabIndex = 0;
            // 
            // gbxRoomInfor
            // 
            this.gbxRoomInfor.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gbxRoomInfor.Controls.Add(this.txtPricingType);
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
            this.gbxRoomInfor.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxRoomInfor.Location = new System.Drawing.Point(71, 134);
            this.gbxRoomInfor.Name = "gbxRoomInfor";
            this.gbxRoomInfor.Size = new System.Drawing.Size(597, 689);
            this.gbxRoomInfor.TabIndex = 5;
            this.gbxRoomInfor.TabStop = false;
            this.gbxRoomInfor.Text = "Thông tin ";
            // 
            // txtGender
            // 
            this.txtGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGender.Location = new System.Drawing.Point(96, 295);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(479, 35);
            this.txtGender.TabIndex = 19;
            // 
            // txtCCCD
            // 
            this.txtCCCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCCD.Location = new System.Drawing.Point(96, 241);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(479, 35);
            this.txtCCCD.TabIndex = 18;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(96, 193);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(479, 35);
            this.txtEmail.TabIndex = 17;
            // 
            // txtSDT
            // 
            this.txtSDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSDT.Location = new System.Drawing.Point(96, 146);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(479, 35);
            this.txtSDT.TabIndex = 16;
            // 
            // txtDiachi
            // 
            this.txtDiachi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiachi.Location = new System.Drawing.Point(96, 99);
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.Size = new System.Drawing.Size(479, 35);
            this.txtDiachi.TabIndex = 15;
            // 
            // txtTen
            // 
            this.txtTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTen.Location = new System.Drawing.Point(96, 51);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(479, 35);
            this.txtTen.TabIndex = 14;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(15, 534);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(61, 22);
            this.lblNote.TabIndex = 13;
            this.lblNote.Text = "NOTE";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(96, 534);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(479, 133);
            this.txtNote.TabIndex = 12;
            // 
            // dtpCheckoutTime
            // 
            this.dtpCheckoutTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckoutTime.Location = new System.Drawing.Point(176, 469);
            this.dtpCheckoutTime.Name = "dtpCheckoutTime";
            this.dtpCheckoutTime.Size = new System.Drawing.Size(399, 30);
            this.dtpCheckoutTime.TabIndex = 11;
            this.dtpCheckoutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckoutTime.CustomFormat = "dd/MM/yyyy HH:mm";

            // 
            // dtpCheckinTime
            // 
            this.dtpCheckinTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckinTime.Location = new System.Drawing.Point(176, 421);
            this.dtpCheckinTime.Name = "dtpCheckinTime";
            this.dtpCheckinTime.Size = new System.Drawing.Size(399, 30);
            this.dtpCheckinTime.TabIndex = 10;
            this.dtpCheckinTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckinTime.CustomFormat = "dd/MM/yyyy HH:mm";
            // 
            // lblCHECKOUT
            // 
            this.lblCHECKOUT.AutoSize = true;
            this.lblCHECKOUT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCHECKOUT.Location = new System.Drawing.Point(15, 477);
            this.lblCHECKOUT.Name = "lblCHECKOUT";
            this.lblCHECKOUT.Size = new System.Drawing.Size(137, 22);
            this.lblCHECKOUT.TabIndex = 9;
            this.lblCHECKOUT.Text = "Check-out Time";
            // 
            // lblCHECKIN
            // 
            this.lblCHECKIN.AutoSize = true;
            this.lblCHECKIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCHECKIN.Location = new System.Drawing.Point(15, 429);
            this.lblCHECKIN.Name = "lblCHECKIN";
            this.lblCHECKIN.Size = new System.Drawing.Size(126, 22);
            this.lblCHECKIN.TabIndex = 8;
            this.lblCHECKIN.Text = "Check-in Time";
            // 
            // lblKieuThue
            // 
            this.lblKieuThue.AutoSize = true;
            this.lblKieuThue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKieuThue.Location = new System.Drawing.Point(15, 360);
            this.lblKieuThue.Name = "lblKieuThue";
            this.lblKieuThue.Size = new System.Drawing.Size(86, 22);
            this.lblKieuThue.TabIndex = 7;
            this.lblKieuThue.Text = "Kiểu thuê";
            // 
            // cbxPricingType
            // 
            this.cbxPricingType.FormattingEnabled = true;
            this.cbxPricingType.Location = new System.Drawing.Point(404, 336);
            this.cbxPricingType.Name = "cbxPricingType";
            this.cbxPricingType.Size = new System.Drawing.Size(121, 45);
            this.cbxPricingType.TabIndex = 6;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSex.Location = new System.Drawing.Point(15, 303);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(81, 22);
            this.lblSex.TabIndex = 5;
            this.lblSex.Text = "Giới tính:";
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCCCD.Location = new System.Drawing.Point(15, 254);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(67, 22);
            this.lblCCCD.TabIndex = 4;
            this.lblCCCD.Text = "CCCD:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(17, 201);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(59, 22);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email:";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSDT.Location = new System.Drawing.Point(19, 154);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(57, 22);
            this.lblSDT.TabIndex = 2;
            this.lblSDT.Text = "SĐT: ";
            // 
            // lblDiachi
            // 
            this.lblDiachi.AutoSize = true;
            this.lblDiachi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiachi.Location = new System.Drawing.Point(12, 107);
            this.lblDiachi.Name = "lblDiachi";
            this.lblDiachi.Size = new System.Drawing.Size(70, 22);
            this.lblDiachi.TabIndex = 1;
            this.lblDiachi.Text = "Địa chỉ:";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTen.Location = new System.Drawing.Point(12, 61);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(47, 22);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Tên:";
            // 
            // txtPricingType
            // 
            this.txtPricingType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPricingType.Location = new System.Drawing.Point(107, 352);
            this.txtPricingType.Name = "txtPricingType";
            this.txtPricingType.Size = new System.Drawing.Size(128, 35);
            this.txtPricingType.TabIndex = 20;
            // 
            // frmBookingDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1660, 982);
            this.Controls.Add(this.lblRoomNumber);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbxServicesMenu);
            this.Controls.Add(this.gbxUsedServices);
            this.Controls.Add(this.gbxRoomInfor);
            this.Name = "frmBookingDetail";
            this.Text = "frmBookingDetail";
            this.gbxServicesMenu.ResumeLayout(false);
            this.gbxServicesMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numIncrease)).EndInit();
            this.gbxUsedServices.ResumeLayout(false);
            this.gbxUsedServices.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReduce)).EndInit();
            this.gbxRoomInfor.ResumeLayout(false);
            this.gbxRoomInfor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox gbxServicesMenu;
        private System.Windows.Forms.Button btnIncrease;
        private System.Windows.Forms.NumericUpDown numIncrease;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox lbxHotelServices;
        private System.Windows.Forms.GroupBox gbxUsedServices;
        private System.Windows.Forms.Button btnReduce;
        private System.Windows.Forms.NumericUpDown numReduce;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ListBox lbxUsedServices;
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
        private System.Windows.Forms.TextBox txtPricingType;
    }
}