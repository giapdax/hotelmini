namespace HOTEL_MINI.Forms.Controls
{
    partial class UcInvoice
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCusId = new System.Windows.Forms.TextBox();
            this.lblIdNumber = new System.Windows.Forms.Label();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.lblCusName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPaymentMethod = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnExportInvoice = new System.Windows.Forms.Button();
            this.txtCheckout = new System.Windows.Forms.TextBox();
            this.txtCheckin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvUsedService = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtServiceCharge = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtSurcharge = new System.Windows.Forms.TextBox();
            this.txtRoomCharge = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.lblRoomCharge = new System.Windows.Forms.Label();
            this.lblServiceChange = new System.Windows.Forms.Label();
            this.lblSurcharge = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblIssuedBy = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedService)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCusId);
            this.panel1.Controls.Add(this.lblIdNumber);
            this.panel1.Controls.Add(this.txtCusName);
            this.panel1.Controls.Add(this.lblCusName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtPaymentMethod);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Controls.Add(this.btnExportInvoice);
            this.panel1.Controls.Add(this.txtCheckout);
            this.panel1.Controls.Add(this.txtCheckin);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.txtNote);
            this.panel1.Controls.Add(this.txtEmployeeName);
            this.panel1.Controls.Add(this.txtTotalAmount);
            this.panel1.Controls.Add(this.txtDiscount);
            this.panel1.Controls.Add(this.txtSurcharge);
            this.panel1.Controls.Add(this.txtRoomCharge);
            this.panel1.Controls.Add(this.lblNote);
            this.panel1.Controls.Add(this.lblRoomCharge);
            this.panel1.Controls.Add(this.lblServiceChange);
            this.panel1.Controls.Add(this.lblSurcharge);
            this.panel1.Controls.Add(this.lblDiscount);
            this.panel1.Controls.Add(this.lblTotalAmount);
            this.panel1.Controls.Add(this.lblIssuedBy);
            this.panel1.Controls.Add(this.lblPaymentMethod);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(648, 1116);
            this.panel1.TabIndex = 0;
            // 
            // txtCusId
            // 
            this.txtCusId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCusId.Location = new System.Drawing.Point(148, 133);
            this.txtCusId.Name = "txtCusId";
            this.txtCusId.Size = new System.Drawing.Size(454, 35);
            this.txtCusId.TabIndex = 52;
            // 
            // lblIdNumber
            // 
            this.lblIdNumber.AutoSize = true;
            this.lblIdNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdNumber.Location = new System.Drawing.Point(20, 136);
            this.lblIdNumber.Name = "lblIdNumber";
            this.lblIdNumber.Size = new System.Drawing.Size(104, 25);
            this.lblIdNumber.TabIndex = 51;
            this.lblIdNumber.Text = "Căn cước";
            // 
            // txtCusName
            // 
            this.txtCusName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCusName.Location = new System.Drawing.Point(147, 87);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(455, 35);
            this.txtCusName.TabIndex = 50;
            // 
            // lblCusName
            // 
            this.lblCusName.AutoSize = true;
            this.lblCusName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCusName.Location = new System.Drawing.Point(13, 97);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(128, 25);
            this.lblCusName.TabIndex = 49;
            this.lblCusName.Text = "Khách hàng";
            this.lblCusName.Click += new System.EventHandler(this.lblCusName_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(257, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 37);
            this.label4.TabIndex = 48;
            this.label4.Text = "Phòng";
            // 
            // txtPaymentMethod
            // 
            this.txtPaymentMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaymentMethod.Location = new System.Drawing.Point(149, 836);
            this.txtPaymentMethod.Name = "txtPaymentMethod";
            this.txtPaymentMethod.Size = new System.Drawing.Size(191, 35);
            this.txtPaymentMethod.TabIndex = 47;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(237, 1012);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(84, 66);
            this.btnBack.TabIndex = 46;
            this.btnBack.Text = "Trở lại";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // btnExportInvoice
            // 
            this.btnExportInvoice.Location = new System.Drawing.Point(405, 1012);
            this.btnExportInvoice.Name = "btnExportInvoice";
            this.btnExportInvoice.Size = new System.Drawing.Size(84, 66);
            this.btnExportInvoice.TabIndex = 45;
            this.btnExportInvoice.Text = "Export";
            this.btnExportInvoice.UseVisualStyleBackColor = true;
            // 
            // txtCheckout
            // 
            this.txtCheckout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckout.Location = new System.Drawing.Point(149, 263);
            this.txtCheckout.Name = "txtCheckout";
            this.txtCheckout.Size = new System.Drawing.Size(453, 35);
            this.txtCheckout.TabIndex = 44;
            // 
            // txtCheckin
            // 
            this.txtCheckin.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckin.Location = new System.Drawing.Point(148, 215);
            this.txtCheckin.Name = "txtCheckin";
            this.txtCheckin.Size = new System.Drawing.Size(454, 35);
            this.txtCheckin.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 25);
            this.label3.TabIndex = 42;
            this.label3.Text = "Checkout";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 25);
            this.label2.TabIndex = 41;
            this.label2.Text = "Checkin";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.dgvUsedService);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(148, 310);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(455, 342);
            this.panel2.TabIndex = 39;
            // 
            // dgvUsedService
            // 
            this.dgvUsedService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsedService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsedService.Location = new System.Drawing.Point(0, 0);
            this.dgvUsedService.Name = "dgvUsedService";
            this.dgvUsedService.RowHeadersWidth = 62;
            this.dgvUsedService.RowTemplate.Height = 28;
            this.dgvUsedService.Size = new System.Drawing.Size(453, 295);
            this.dgvUsedService.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtServiceCharge);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 295);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(453, 45);
            this.panel3.TabIndex = 0;
            // 
            // txtServiceCharge
            // 
            this.txtServiceCharge.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtServiceCharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceCharge.Location = new System.Drawing.Point(237, 0);
            this.txtServiceCharge.Name = "txtServiceCharge";
            this.txtServiceCharge.Size = new System.Drawing.Size(216, 44);
            this.txtServiceCharge.TabIndex = 1;
            this.txtServiceCharge.Tag = "";
            this.txtServiceCharge.Text = "14560000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = " Total:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNote
            // 
            this.txtNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNote.Location = new System.Drawing.Point(150, 882);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(455, 111);
            this.txtNote.TabIndex = 38;
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeName.Location = new System.Drawing.Point(150, 795);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(452, 35);
            this.txtEmployeeName.TabIndex = 37;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmount.Location = new System.Drawing.Point(150, 754);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(452, 35);
            this.txtTotalAmount.TabIndex = 36;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(150, 700);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(452, 35);
            this.txtDiscount.TabIndex = 35;
            // 
            // txtSurcharge
            // 
            this.txtSurcharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSurcharge.Location = new System.Drawing.Point(149, 659);
            this.txtSurcharge.Name = "txtSurcharge";
            this.txtSurcharge.Size = new System.Drawing.Size(453, 35);
            this.txtSurcharge.TabIndex = 34;
            // 
            // txtRoomCharge
            // 
            this.txtRoomCharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoomCharge.Location = new System.Drawing.Point(148, 174);
            this.txtRoomCharge.Name = "txtRoomCharge";
            this.txtRoomCharge.Size = new System.Drawing.Size(455, 35);
            this.txtRoomCharge.TabIndex = 33;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(20, 882);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(86, 25);
            this.lblNote.TabIndex = 32;
            this.lblNote.Text = "Ghi chú";
            // 
            // lblRoomCharge
            // 
            this.lblRoomCharge.AutoSize = true;
            this.lblRoomCharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomCharge.Location = new System.Drawing.Point(20, 174);
            this.lblRoomCharge.Name = "lblRoomCharge";
            this.lblRoomCharge.Size = new System.Drawing.Size(121, 25);
            this.lblRoomCharge.TabIndex = 25;
            this.lblRoomCharge.Text = "Tiền phòng";
            // 
            // lblServiceChange
            // 
            this.lblServiceChange.AutoSize = true;
            this.lblServiceChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServiceChange.Location = new System.Drawing.Point(20, 310);
            this.lblServiceChange.Name = "lblServiceChange";
            this.lblServiceChange.Size = new System.Drawing.Size(91, 25);
            this.lblServiceChange.TabIndex = 26;
            this.lblServiceChange.Text = "Dịch vụ:";
            // 
            // lblSurcharge
            // 
            this.lblSurcharge.AutoSize = true;
            this.lblSurcharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurcharge.Location = new System.Drawing.Point(26, 659);
            this.lblSurcharge.Name = "lblSurcharge";
            this.lblSurcharge.Size = new System.Drawing.Size(85, 25);
            this.lblSurcharge.TabIndex = 27;
            this.lblSurcharge.Text = "Phụ phí";
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiscount.Location = new System.Drawing.Point(20, 706);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(97, 25);
            this.lblDiscount.TabIndex = 28;
            this.lblDiscount.Text = "Giảm giá";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.Location = new System.Drawing.Point(14, 760);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(103, 25);
            this.lblTotalAmount.TabIndex = 29;
            this.lblTotalAmount.Text = "Tổng tiên";
            // 
            // lblIssuedBy
            // 
            this.lblIssuedBy.AutoSize = true;
            this.lblIssuedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIssuedBy.Location = new System.Drawing.Point(14, 805);
            this.lblIssuedBy.Name = "lblIssuedBy";
            this.lblIssuedBy.Size = new System.Drawing.Size(109, 25);
            this.lblIssuedBy.TabIndex = 30;
            this.lblIssuedBy.Text = "Nhân viên";
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentMethod.Location = new System.Drawing.Point(20, 842);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(103, 25);
            this.lblPaymentMethod.TabIndex = 31;
            this.lblPaymentMethod.Text = "Hình thức";
            // 
            // UcInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(648, 1116);
            this.MinimumSize = new System.Drawing.Size(648, 1116);
            this.Name = "UcInvoice";
            this.Size = new System.Drawing.Size(648, 1116);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedService)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnExportInvoice;
        private System.Windows.Forms.TextBox txtCheckout;
        private System.Windows.Forms.TextBox txtCheckin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvUsedService;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtServiceCharge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TextBox txtSurcharge;
        private System.Windows.Forms.TextBox txtRoomCharge;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblRoomCharge;
        private System.Windows.Forms.Label lblServiceChange;
        private System.Windows.Forms.Label lblSurcharge;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblIssuedBy;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.TextBox txtPaymentMethod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCusId;
        private System.Windows.Forms.Label lblIdNumber;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.Label lblCusName;
    }
}
