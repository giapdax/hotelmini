namespace HOTEL_MINI.Forms.Controls
{
    partial class UcInvoice
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.lblCusName = new System.Windows.Forms.Label();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.lblIdNumber = new System.Windows.Forms.Label();
            this.txtCusId = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCheckin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCheckout = new System.Windows.Forms.TextBox();
            this.lblServiceChange = new System.Windows.Forms.Label();
            this.dgvUsedService = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvRooms = new System.Windows.Forms.DataGridView();
            this.groupTotals = new System.Windows.Forms.GroupBox();
            this.lblRoomCharge = new System.Windows.Forms.Label();
            this.txtRoomCharge = new System.Windows.Forms.TextBox();
            this.lblSurcharge = new System.Windows.Forms.Label();
            this.txtSurcharge = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.lblIssuedBy = new System.Windows.Forms.Label();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.txtPaymentMethod = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnExportInvoice = new System.Windows.Forms.Button();
            this.labelPayments = new System.Windows.Forms.Label();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
            this.lblServiceTotal = new System.Windows.Forms.Label();
            this.txtServiceCharge = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).BeginInit();
            this.groupTotals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCusName
            // 
            this.lblCusName.AutoSize = true;
            this.lblCusName.Location = new System.Drawing.Point(16, 15);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(90, 20);
            this.lblCusName.TabIndex = 0;
            this.lblCusName.Text = "Khách hàng";
            // 
            // txtCusName
            // 
            this.txtCusName.Location = new System.Drawing.Point(120, 12);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(340, 26);
            this.txtCusName.TabIndex = 1;
            // 
            // lblIdNumber
            // 
            this.lblIdNumber.AutoSize = true;
            this.lblIdNumber.Location = new System.Drawing.Point(480, 15);
            this.lblIdNumber.Name = "lblIdNumber";
            this.lblIdNumber.Size = new System.Drawing.Size(56, 20);
            this.lblIdNumber.TabIndex = 2;
            this.lblIdNumber.Text = "CCCD";
            // 
            // txtCusId
            // 
            this.txtCusId.Location = new System.Drawing.Point(540, 12);
            this.txtCusId.Name = "txtCusId";
            this.txtCusId.Size = new System.Drawing.Size(170, 26);
            this.txtCusId.TabIndex = 3;
            // 
            // label2 (Check-in)
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Check-in";
            // 
            // txtCheckin
            // 
            this.txtCheckin.Location = new System.Drawing.Point(120, 47);
            this.txtCheckin.Name = "txtCheckin";
            this.txtCheckin.Size = new System.Drawing.Size(200, 26);
            this.txtCheckin.TabIndex = 5;
            // 
            // label3 (Checkout)
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(340, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Check-out";
            // 
            // txtCheckout
            // 
            this.txtCheckout.Location = new System.Drawing.Point(420, 47);
            this.txtCheckout.Name = "txtCheckout";
            this.txtCheckout.Size = new System.Drawing.Size(170, 26);
            this.txtCheckout.TabIndex = 7;
            // 
            // lblServiceChange
            // 
            this.lblServiceChange.AutoSize = true;
            this.lblServiceChange.Location = new System.Drawing.Point(16, 90);
            this.lblServiceChange.Name = "lblServiceChange";
            this.lblServiceChange.Size = new System.Drawing.Size(63, 20);
            this.lblServiceChange.TabIndex = 8;
            this.lblServiceChange.Text = "Dịch vụ";
            // 
            // dgvUsedService
            // 
            this.dgvUsedService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvUsedService.Location = new System.Drawing.Point(20, 115);
            this.dgvUsedService.Name = "dgvUsedService";
            this.dgvUsedService.RowHeadersWidth = 62;
            this.dgvUsedService.RowTemplate.Height = 28;
            this.dgvUsedService.Size = new System.Drawing.Size(690, 160);
            this.dgvUsedService.TabIndex = 9;
            // 
            // label4 (Rooms)
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 285);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Phòng";
            // 
            // dgvRooms
            // 
            this.dgvRooms.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRooms.Location = new System.Drawing.Point(20, 310);
            this.dgvRooms.Name = "dgvRooms";
            this.dgvRooms.RowHeadersWidth = 62;
            this.dgvRooms.RowTemplate.Height = 28;
            this.dgvRooms.Size = new System.Drawing.Size(690, 150);
            this.dgvRooms.TabIndex = 11;
            // 
            // groupTotals
            // 
            this.groupTotals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.groupTotals.Controls.Add(this.lblRoomCharge);
            this.groupTotals.Controls.Add(this.txtRoomCharge);
            this.groupTotals.Controls.Add(this.lblServiceTotal);
            this.groupTotals.Controls.Add(this.txtServiceCharge);
            this.groupTotals.Controls.Add(this.lblSurcharge);
            this.groupTotals.Controls.Add(this.txtSurcharge);
            this.groupTotals.Controls.Add(this.lblDiscount);
            this.groupTotals.Controls.Add(this.txtDiscount);
            this.groupTotals.Controls.Add(this.lblTotalAmount);
            this.groupTotals.Controls.Add(this.txtTotalAmount);
            this.groupTotals.Location = new System.Drawing.Point(20, 640);
            this.groupTotals.Name = "groupTotals";
            this.groupTotals.Size = new System.Drawing.Size(410, 140);
            this.groupTotals.TabIndex = 12;
            this.groupTotals.TabStop = false;
            this.groupTotals.Text = "Tổng tiền";
            // 
            // lblRoomCharge
            // 
            this.lblRoomCharge.AutoSize = true;
            this.lblRoomCharge.Location = new System.Drawing.Point(12, 28);
            this.lblRoomCharge.Name = "lblRoomCharge";
            this.lblRoomCharge.Size = new System.Drawing.Size(85, 20);
            this.lblRoomCharge.TabIndex = 0;
            this.lblRoomCharge.Text = "Tiền phòng";
            // 
            // txtRoomCharge
            // 
            this.txtRoomCharge.Location = new System.Drawing.Point(110, 25);
            this.txtRoomCharge.Name = "txtRoomCharge";
            this.txtRoomCharge.Size = new System.Drawing.Size(120, 26);
            this.txtRoomCharge.TabIndex = 1;
            // 
            // lblServiceTotal
            // 
            this.lblServiceTotal.AutoSize = true;
            this.lblServiceTotal.Location = new System.Drawing.Point(245, 28);
            this.lblServiceTotal.Name = "lblServiceTotal";
            this.lblServiceTotal.Size = new System.Drawing.Size(66, 20);
            this.lblServiceTotal.TabIndex = 2;
            this.lblServiceTotal.Text = "Dịch vụ";
            // 
            // txtServiceCharge
            // 
            this.txtServiceCharge.Location = new System.Drawing.Point(315, 25);
            this.txtServiceCharge.Name = "txtServiceCharge";
            this.txtServiceCharge.Size = new System.Drawing.Size(80, 26);
            this.txtServiceCharge.TabIndex = 3;
            // 
            // lblSurcharge
            // 
            this.lblSurcharge.AutoSize = true;
            this.lblSurcharge.Location = new System.Drawing.Point(12, 65);
            this.lblSurcharge.Name = "lblSurcharge";
            this.lblSurcharge.Size = new System.Drawing.Size(58, 20);
            this.lblSurcharge.TabIndex = 4;
            this.lblSurcharge.Text = "Phụ phí";
            // 
            // txtSurcharge
            // 
            this.txtSurcharge.Location = new System.Drawing.Point(110, 62);
            this.txtSurcharge.Name = "txtSurcharge";
            this.txtSurcharge.Size = new System.Drawing.Size(120, 26);
            this.txtSurcharge.TabIndex = 5;
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(245, 65);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(68, 20);
            this.lblDiscount.TabIndex = 6;
            this.lblDiscount.Text = "Giảm giá";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(315, 62);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(80, 26);
            this.txtDiscount.TabIndex = 7;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(12, 102);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(73, 20);
            this.lblTotalAmount.TabIndex = 8;
            this.lblTotalAmount.Text = "Tổng tiền";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(110, 99);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(285, 26);
            this.txtTotalAmount.TabIndex = 9;
            // 
            // lblIssuedBy
            // 
            this.lblIssuedBy.AutoSize = true;
            this.lblIssuedBy.Location = new System.Drawing.Point(450, 645);
            this.lblIssuedBy.Name = "lblIssuedBy";
            this.lblIssuedBy.Size = new System.Drawing.Size(76, 20);
            this.lblIssuedBy.TabIndex = 13;
            this.lblIssuedBy.Text = "Nhân viên";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(540, 642);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(170, 26);
            this.txtEmployeeName.TabIndex = 14;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(450, 680);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(77, 20);
            this.lblPaymentMethod.TabIndex = 15;
            this.lblPaymentMethod.Text = "Hình thức";
            // 
            // txtPaymentMethod
            // 
            this.txtPaymentMethod.Location = new System.Drawing.Point(540, 677);
            this.txtPaymentMethod.Name = "txtPaymentMethod";
            this.txtPaymentMethod.Size = new System.Drawing.Size(170, 26);
            this.txtPaymentMethod.TabIndex = 16;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(450, 715);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(58, 20);
            this.lblNote.TabIndex = 17;
            this.lblNote.Text = "Ghi chú";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(540, 712);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(170, 68);
            this.txtNote.TabIndex = 18;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(450, 790);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 32);
            this.btnBack.TabIndex = 19;
            this.btnBack.Text = "Đóng";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // btnExportInvoice
            // 
            this.btnExportInvoice.Location = new System.Drawing.Point(610, 790);
            this.btnExportInvoice.Name = "btnExportInvoice";
            this.btnExportInvoice.Size = new System.Drawing.Size(100, 32);
            this.btnExportInvoice.TabIndex = 20;
            this.btnExportInvoice.Text = "Export";
            this.btnExportInvoice.UseVisualStyleBackColor = true;
            // 
            // labelPayments
            // 
            this.labelPayments.AutoSize = true;
            this.labelPayments.Location = new System.Drawing.Point(16, 470);
            this.labelPayments.Name = "labelPayments";
            this.labelPayments.Size = new System.Drawing.Size(122, 20);
            this.labelPayments.TabIndex = 21;
            this.labelPayments.Text = "Lịch sử thanh toán";
            // 
            // dgvPayments
            // 
            this.dgvPayments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPayments.Location = new System.Drawing.Point(20, 495);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.RowHeadersWidth = 62;
            this.dgvPayments.RowTemplate.Height = 28;
            this.dgvPayments.Size = new System.Drawing.Size(690, 130);
            this.dgvPayments.TabIndex = 22;
            // 
            // UcInvoice1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvPayments);
            this.Controls.Add(this.labelPayments);
            this.Controls.Add(this.btnExportInvoice);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.txtPaymentMethod);
            this.Controls.Add(this.lblPaymentMethod);
            this.Controls.Add(this.txtEmployeeName);
            this.Controls.Add(this.lblIssuedBy);
            this.Controls.Add(this.groupTotals);
            this.Controls.Add(this.dgvRooms);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvUsedService);
            this.Controls.Add(this.lblServiceChange);
            this.Controls.Add(this.txtCheckout);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCheckin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCusId);
            this.Controls.Add(this.lblIdNumber);
            this.Controls.Add(this.txtCusName);
            this.Controls.Add(this.lblCusName);
            this.Name = "UcInvoice1";
            this.Size = new System.Drawing.Size(730, 840);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRooms)).EndInit();
            this.groupTotals.ResumeLayout(false);
            this.groupTotals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private System.Windows.Forms.Label lblCusName;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.Label lblIdNumber;
        private System.Windows.Forms.TextBox txtCusId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCheckin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCheckout;

        private System.Windows.Forms.Label lblServiceChange;
        private System.Windows.Forms.DataGridView dgvUsedService;

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvRooms;

        private System.Windows.Forms.GroupBox groupTotals;
        private System.Windows.Forms.Label lblRoomCharge;
        private System.Windows.Forms.TextBox txtRoomCharge;
        private System.Windows.Forms.Label lblServiceTotal;
        private System.Windows.Forms.TextBox txtServiceCharge;
        private System.Windows.Forms.Label lblSurcharge;
        private System.Windows.Forms.TextBox txtSurcharge;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.TextBox txtTotalAmount;

        private System.Windows.Forms.Label lblIssuedBy;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.TextBox txtPaymentMethod;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnExportInvoice;

        private System.Windows.Forms.Label labelPayments;
        private System.Windows.Forms.DataGridView dgvPayments;
    }
}
