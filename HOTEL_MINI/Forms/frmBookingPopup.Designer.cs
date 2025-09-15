using System.Windows.Forms;

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
            this.cbxGender = new System.Windows.Forms.ComboBox();
            this.lblRoomNumber = new System.Windows.Forms.Label();
            this.btnCheckExistCCCD = new System.Windows.Forms.Button();
            this.btnBookConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbtnDattruoc = new System.Windows.Forms.RadioButton();
            this.rbtnNhanngay = new System.Windows.Forms.RadioButton();
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
            this.gbxRoomInfor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxRoomInfor
            // 
            this.gbxRoomInfor.AutoSize = true;
            this.gbxRoomInfor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(52)))), ((int)(((byte)(58)))));
            this.gbxRoomInfor.Controls.Add(this.cbxGender);
            this.gbxRoomInfor.Controls.Add(this.lblRoomNumber);
            this.gbxRoomInfor.Controls.Add(this.btnCheckExistCCCD);
            this.gbxRoomInfor.Controls.Add(this.btnBookConfirm);
            this.gbxRoomInfor.Controls.Add(this.btnCancel);
            this.gbxRoomInfor.Controls.Add(this.rbtnDattruoc);
            this.gbxRoomInfor.Controls.Add(this.rbtnNhanngay);
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
            this.gbxRoomInfor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxRoomInfor.ForeColor = System.Drawing.Color.White;
            this.gbxRoomInfor.Location = new System.Drawing.Point(0, 0);
            this.gbxRoomInfor.Name = "gbxRoomInfor";
            this.gbxRoomInfor.Size = new System.Drawing.Size(540, 564);
            this.gbxRoomInfor.TabIndex = 2;
            this.gbxRoomInfor.TabStop = false;
            this.gbxRoomInfor.Text = "Thông tin ";
            // 
            // cbxGender
            // 
            this.cbxGender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbxGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGender.ForeColor = System.Drawing.Color.White;
            this.cbxGender.FormattingEnabled = true;
            this.cbxGender.Location = new System.Drawing.Point(177, 257);
            this.cbxGender.Name = "cbxGender";
            this.cbxGender.Size = new System.Drawing.Size(102, 28);
            this.cbxGender.TabIndex = 26;
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNumber.ForeColor = System.Drawing.Color.White;
            this.lblRoomNumber.Location = new System.Drawing.Point(224, 34);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(85, 37);
            this.lblRoomNumber.TabIndex = 23;
            this.lblRoomNumber.Text = "Tên:";
            // 
            // btnCheckExistCCCD
            // 
            this.btnCheckExistCCCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.btnCheckExistCCCD.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckExistCCCD.ForeColor = System.Drawing.Color.White;
            this.btnCheckExistCCCD.Location = new System.Drawing.Point(356, 86);
            this.btnCheckExistCCCD.Name = "btnCheckExistCCCD";
            this.btnCheckExistCCCD.Size = new System.Drawing.Size(90, 31);
            this.btnCheckExistCCCD.TabIndex = 24;
            this.btnCheckExistCCCD.Text = "Check";
            this.btnCheckExistCCCD.UseVisualStyleBackColor = false;
            this.btnCheckExistCCCD.Click += new System.EventHandler(this.btnCheckExistCCCD_Click);
            // 
            // btnBookConfirm
            // 
            this.btnBookConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.btnBookConfirm.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBookConfirm.ForeColor = System.Drawing.Color.White;
            this.btnBookConfirm.Location = new System.Drawing.Point(345, 499);
            this.btnBookConfirm.Name = "btnBookConfirm";
            this.btnBookConfirm.Size = new System.Drawing.Size(101, 32);
            this.btnBookConfirm.TabIndex = 21;
            this.btnBookConfirm.Text = "Xác nhận";
            this.btnBookConfirm.UseVisualStyleBackColor = false;
            this.btnBookConfirm.Click += new System.EventHandler(this.btnBookConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(181, 499);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 32);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rbtnDattruoc
            // 
            this.rbtnDattruoc.AutoSize = true;
            this.rbtnDattruoc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rbtnDattruoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.rbtnDattruoc.Location = new System.Drawing.Point(298, 257);
            this.rbtnDattruoc.Name = "rbtnDattruoc";
            this.rbtnDattruoc.Size = new System.Drawing.Size(121, 29);
            this.rbtnDattruoc.TabIndex = 21;
            this.rbtnDattruoc.TabStop = true;
            this.rbtnDattruoc.Text = "Đặt Trước";
            this.rbtnDattruoc.UseVisualStyleBackColor = true;
            this.rbtnDattruoc.CheckedChanged += new System.EventHandler(this.rbtnDattruoc_CheckedChanged);
            // 
            // rbtnNhanngay
            // 
            this.rbtnNhanngay.AutoSize = true;
            this.rbtnNhanngay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.rbtnNhanngay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.rbtnNhanngay.Location = new System.Drawing.Point(298, 292);
            this.rbtnNhanngay.Name = "rbtnNhanngay";
            this.rbtnNhanngay.Size = new System.Drawing.Size(130, 29);
            this.rbtnNhanngay.TabIndex = 25;
            this.rbtnNhanngay.TabStop = true;
            this.rbtnNhanngay.Text = "Nhận ngay";
            this.rbtnNhanngay.UseVisualStyleBackColor = true;
            this.rbtnNhanngay.CheckedChanged += new System.EventHandler(this.rbtnNhanngay_CheckedChanged);
            // 
            // txtGender
            // 
            this.txtGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGender.Location = new System.Drawing.Point(177, 257);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(65, 26);
            this.txtGender.TabIndex = 19;
            // 
            // txtCCCD
            // 
            this.txtCCCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCCCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCCD.ForeColor = System.Drawing.Color.White;
            this.txtCCCD.Location = new System.Drawing.Point(181, 89);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(169, 26);
            this.txtCCCD.TabIndex = 18;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.ForeColor = System.Drawing.Color.White;
            this.txtEmail.Location = new System.Drawing.Point(177, 217);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(287, 26);
            this.txtEmail.TabIndex = 17;
            // 
            // txtSDT
            // 
            this.txtSDT.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSDT.ForeColor = System.Drawing.Color.White;
            this.txtSDT.Location = new System.Drawing.Point(179, 185);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(287, 26);
            this.txtSDT.TabIndex = 16;
            // 
            // txtDiachi
            // 
            this.txtDiachi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDiachi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiachi.ForeColor = System.Drawing.Color.White;
            this.txtDiachi.Location = new System.Drawing.Point(179, 153);
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.Size = new System.Drawing.Size(287, 26);
            this.txtDiachi.TabIndex = 15;
            // 
            // txtTen
            // 
            this.txtTen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTen.ForeColor = System.Drawing.Color.White;
            this.txtTen.Location = new System.Drawing.Point(179, 121);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(287, 26);
            this.txtTen.TabIndex = 14;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblNote.Location = new System.Drawing.Point(48, 395);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(82, 25);
            this.lblNote.TabIndex = 13;
            this.lblNote.Text = "Ghi chú:";
            // 
            // txtNote
            // 
            this.txtNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.ForeColor = System.Drawing.Color.White;
            this.txtNote.Location = new System.Drawing.Point(177, 395);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(284, 81);
            this.txtNote.TabIndex = 12;
            // 
            // dtpCheckoutTime
            // 
            this.dtpCheckoutTime.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpCheckoutTime.CustomFormat = "dd/MM/yyyy HH";
            this.dtpCheckoutTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckoutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckoutTime.Location = new System.Drawing.Point(177, 364);
            this.dtpCheckoutTime.Name = "dtpCheckoutTime";
            this.dtpCheckoutTime.ShowUpDown = true;
            this.dtpCheckoutTime.Size = new System.Drawing.Size(242, 26);
            this.dtpCheckoutTime.TabIndex = 11;
            // 
            // dtpCheckinTime
            // 
            this.dtpCheckinTime.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dtpCheckinTime.CustomFormat = "dd/MM/yyyy HH";
            this.dtpCheckinTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckinTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckinTime.Location = new System.Drawing.Point(177, 329);
            this.dtpCheckinTime.Name = "dtpCheckinTime";
            this.dtpCheckinTime.ShowUpDown = true;
            this.dtpCheckinTime.Size = new System.Drawing.Size(242, 26);
            this.dtpCheckinTime.TabIndex = 10;
            // 
            // lblCHECKOUT
            // 
            this.lblCHECKOUT.AutoSize = true;
            this.lblCHECKOUT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCHECKOUT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblCHECKOUT.Location = new System.Drawing.Point(48, 361);
            this.lblCHECKOUT.Name = "lblCHECKOUT";
            this.lblCHECKOUT.Size = new System.Drawing.Size(104, 25);
            this.lblCHECKOUT.TabIndex = 9;
            this.lblCHECKOUT.Text = "Trả phòng:";
            // 
            // lblCHECKIN
            // 
            this.lblCHECKIN.AutoSize = true;
            this.lblCHECKIN.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCHECKIN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblCHECKIN.Location = new System.Drawing.Point(48, 328);
            this.lblCHECKIN.Name = "lblCHECKIN";
            this.lblCHECKIN.Size = new System.Drawing.Size(123, 25);
            this.lblCHECKIN.TabIndex = 8;
            this.lblCHECKIN.Text = "Nhận phòng:";
            // 
            // lblKieuThue
            // 
            this.lblKieuThue.AutoSize = true;
            this.lblKieuThue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKieuThue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblKieuThue.Location = new System.Drawing.Point(48, 286);
            this.lblKieuThue.Name = "lblKieuThue";
            this.lblKieuThue.Size = new System.Drawing.Size(94, 25);
            this.lblKieuThue.TabIndex = 7;
            this.lblKieuThue.Text = "Kiểu thuê";
            // 
            // cbxPricingType
            // 
            this.cbxPricingType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbxPricingType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxPricingType.FormattingEnabled = true;
            this.cbxPricingType.Location = new System.Drawing.Point(177, 295);
            this.cbxPricingType.Name = "cbxPricingType";
            this.cbxPricingType.Size = new System.Drawing.Size(102, 28);
            this.cbxPricingType.TabIndex = 6;
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblSex.Location = new System.Drawing.Point(52, 256);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(90, 25);
            this.lblSex.TabIndex = 5;
            this.lblSex.Text = "Giới tính:";
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(52)))), ((int)(((byte)(58)))));
            this.lblCCCD.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCCCD.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblCCCD.Location = new System.Drawing.Point(55, 88);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(61, 25);
            this.lblCCCD.TabIndex = 4;
            this.lblCCCD.Text = "CCCD:";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblEmail.Location = new System.Drawing.Point(52, 216);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(63, 25);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email:";
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSDT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblSDT.Location = new System.Drawing.Point(55, 185);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(56, 25);
            this.lblSDT.TabIndex = 2;
            this.lblSDT.Text = "SĐT: ";
            // 
            // lblDiachi
            // 
            this.lblDiachi.AutoSize = true;
            this.lblDiachi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiachi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblDiachi.Location = new System.Drawing.Point(55, 152);
            this.lblDiachi.Name = "lblDiachi";
            this.lblDiachi.Size = new System.Drawing.Size(75, 25);
            this.lblDiachi.TabIndex = 1;
            this.lblDiachi.Text = "Địa chỉ:";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(52)))), ((int)(((byte)(58)))));
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblTen.Location = new System.Drawing.Point(55, 122);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(47, 25);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Tên:";
            // 
            // frmBookingPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 564);
            this.Controls.Add(this.gbxRoomInfor);
            this.Name = "frmBookingPopup";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin đặt phòng";
            this.gbxRoomInfor.ResumeLayout(false);
            this.gbxRoomInfor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxRoomInfor;
        private System.Windows.Forms.Button btnCheckExistCCCD;
        private System.Windows.Forms.Button btnBookConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbtnDattruoc;
        private System.Windows.Forms.RadioButton rbtnNhanngay;
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
        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.Label lblRoomType;
        private System.Windows.Forms.ComboBox cbxRoomType;
        private ComboBox cbxGender;
    }
}