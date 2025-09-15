namespace HOTEL_MINI.Forms
{
    partial class frmCheckout
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
            this.lblRoomCharge = new System.Windows.Forms.Label();
            this.lblServiceChange = new System.Windows.Forms.Label();
            this.lblSurcharge = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblIssuedBy = new System.Windows.Forms.Label();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtRoomCharge = new System.Windows.Forms.TextBox();
            this.txtSurcharge = new System.Windows.Forms.TextBox();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.cbxPaymentMethod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCheckin = new System.Windows.Forms.TextBox();
            this.txtCheckout = new System.Windows.Forms.TextBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvUsedService = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServiceCharge = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedService)).BeginInit();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblRoomNumber.ForeColor = System.Drawing.Color.White;
            this.lblRoomNumber.Location = new System.Drawing.Point(280, 9);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(112, 37);
            this.lblRoomNumber.TabIndex = 0;
            this.lblRoomNumber.Text = "phong";
            this.lblRoomNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRoomCharge
            // 
            this.lblRoomCharge.AutoSize = true;
            this.lblRoomCharge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRoomCharge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblRoomCharge.Location = new System.Drawing.Point(3, 0);
            this.lblRoomCharge.Name = "lblRoomCharge";
            this.lblRoomCharge.Size = new System.Drawing.Size(77, 38);
            this.lblRoomCharge.TabIndex = 1;
            this.lblRoomCharge.Text = "Tiền phòng: ";
            // 
            // lblServiceChange
            // 
            this.lblServiceChange.AutoSize = true;
            this.lblServiceChange.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblServiceChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblServiceChange.Location = new System.Drawing.Point(3, 114);
            this.lblServiceChange.Name = "lblServiceChange";
            this.lblServiceChange.Size = new System.Drawing.Size(81, 25);
            this.lblServiceChange.TabIndex = 2;
            this.lblServiceChange.Text = "Dịch vụ:";
            // 
            // lblSurcharge
            // 
            this.lblSurcharge.AutoSize = true;
            this.lblSurcharge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblSurcharge.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSurcharge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblSurcharge.Location = new System.Drawing.Point(3, 420);
            this.lblSurcharge.Name = "lblSurcharge";
            this.lblSurcharge.Size = new System.Drawing.Size(82, 25);
            this.lblSurcharge.TabIndex = 3;
            this.lblSurcharge.Text = "Phụ phí:";
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblDiscount.Location = new System.Drawing.Point(3, 458);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(92, 25);
            this.lblDiscount.TabIndex = 4;
            this.lblDiscount.Text = "Giảm giá:";
            this.lblDiscount.Click += new System.EventHandler(this.lblDiscount_Click);
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(36, 595);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(99, 25);
            this.lblTotalAmount.TabIndex = 5;
            this.lblTotalAmount.Text = "Tổng tiền:";
            // 
            // lblIssuedBy
            // 
            this.lblIssuedBy.AutoSize = true;
            this.lblIssuedBy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblIssuedBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblIssuedBy.Location = new System.Drawing.Point(3, 496);
            this.lblIssuedBy.Name = "lblIssuedBy";
            this.lblIssuedBy.Size = new System.Drawing.Size(104, 25);
            this.lblIssuedBy.TabIndex = 6;
            this.lblIssuedBy.Text = "Nhân viên:";
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblPaymentMethod.Location = new System.Drawing.Point(3, 534);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(102, 25);
            this.lblPaymentMethod.TabIndex = 7;
            this.lblPaymentMethod.Text = "Hình thức:";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.lblNote.Location = new System.Drawing.Point(3, 572);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(82, 25);
            this.lblNote.TabIndex = 8;
            this.lblNote.Text = "Ghi chú:";
            this.lblNote.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtRoomCharge
            // 
            this.txtRoomCharge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtRoomCharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtRoomCharge.ForeColor = System.Drawing.Color.White;
            this.txtRoomCharge.Location = new System.Drawing.Point(121, 3);
            this.txtRoomCharge.Name = "txtRoomCharge";
            this.txtRoomCharge.Size = new System.Drawing.Size(128, 26);
            this.txtRoomCharge.TabIndex = 9;
            // 
            // txtSurcharge
            // 
            this.txtSurcharge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSurcharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtSurcharge.ForeColor = System.Drawing.Color.White;
            this.txtSurcharge.Location = new System.Drawing.Point(121, 423);
            this.txtSurcharge.Name = "txtSurcharge";
            this.txtSurcharge.Size = new System.Drawing.Size(128, 26);
            this.txtSurcharge.TabIndex = 11;
            this.txtSurcharge.TextChanged += new System.EventHandler(this.txtSurcharge_TextChanged);
            this.txtSurcharge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSurcharge_KeyPress);
            this.txtSurcharge.Leave += new System.EventHandler(this.txtSurcharge_Leave);
            // 
            // txtDiscount
            // 
            this.txtDiscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtDiscount.ForeColor = System.Drawing.Color.White;
            this.txtDiscount.Location = new System.Drawing.Point(121, 461);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(128, 26);
            this.txtDiscount.TabIndex = 12;
            this.txtDiscount.TextChanged += new System.EventHandler(this.txtDiscount_TextChanged);
            this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscount_KeyPress);
            this.txtDiscount.Leave += new System.EventHandler(this.txtDiscount_Leave);
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtTotalAmount.ForeColor = System.Drawing.Color.White;
            this.txtTotalAmount.Location = new System.Drawing.Point(353, 594);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(128, 26);
            this.txtTotalAmount.TabIndex = 13;
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtEmployeeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtEmployeeName.ForeColor = System.Drawing.Color.White;
            this.txtEmployeeName.Location = new System.Drawing.Point(121, 499);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(206, 26);
            this.txtEmployeeName.TabIndex = 14;
            // 
            // txtNote
            // 
            this.txtNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtNote.ForeColor = System.Drawing.Color.White;
            this.txtNote.Location = new System.Drawing.Point(121, 575);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(510, 150);
            this.txtNote.TabIndex = 16;
            // 
            // cbxPaymentMethod
            // 
            this.cbxPaymentMethod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cbxPaymentMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.cbxPaymentMethod.ForeColor = System.Drawing.Color.White;
            this.cbxPaymentMethod.FormattingEnabled = true;
            this.cbxPaymentMethod.Location = new System.Drawing.Point(121, 537);
            this.cbxPaymentMethod.Name = "cbxPaymentMethod";
            this.cbxPaymentMethod.Size = new System.Drawing.Size(128, 28);
            this.cbxPaymentMethod.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 25);
            this.label2.TabIndex = 19;
            this.label2.Text = "Checkin:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.label3.Location = new System.Drawing.Point(3, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 25);
            this.label3.TabIndex = 20;
            this.label3.Text = "Checkout:";
            // 
            // txtCheckin
            // 
            this.txtCheckin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCheckin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtCheckin.ForeColor = System.Drawing.Color.White;
            this.txtCheckin.Location = new System.Drawing.Point(121, 41);
            this.txtCheckin.Name = "txtCheckin";
            this.txtCheckin.Size = new System.Drawing.Size(128, 26);
            this.txtCheckin.TabIndex = 21;
            // 
            // txtCheckout
            // 
            this.txtCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCheckout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtCheckout.ForeColor = System.Drawing.Color.White;
            this.txtCheckout.Location = new System.Drawing.Point(121, 79);
            this.txtCheckout.Name = "txtCheckout";
            this.txtCheckout.Size = new System.Drawing.Size(128, 26);
            this.txtCheckout.TabIndex = 22;
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.btnConfirm.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnConfirm.Location = new System.Drawing.Point(517, 798);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(112, 34);
            this.btnConfirm.TabIndex = 23;
            this.btnConfirm.Text = "Thanh toán";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancel.Location = new System.Drawing.Point(301, 798);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 34);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.61199F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 81.38801F));
            this.tableLayoutPanel1.Controls.Add(this.txtRoomCharge, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtCheckout, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtNote, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.txtCheckin, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblServiceChange, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblRoomCharge, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblSurcharge, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtSurcharge, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.lblDiscount, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.txtDiscount, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lblIssuedBy, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.txtEmployeeName, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.lblPaymentMethod, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.cbxPaymentMethod, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.lblNote, 0, 8);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.10526F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.263158F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.05263F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(634, 728);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.dgvUsedService, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(121, 117);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.31579F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.68421F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(510, 300);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // dgvUsedService
            // 
            this.dgvUsedService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsedService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsedService.Location = new System.Drawing.Point(3, 3);
            this.dgvUsedService.Name = "dgvUsedService";
            this.dgvUsedService.RowHeadersWidth = 62;
            this.dgvUsedService.RowTemplate.Height = 28;
            this.dgvUsedService.Size = new System.Drawing.Size(504, 252);
            this.dgvUsedService.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 261);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(504, 36);
            this.panel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtServiceCharge, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(504, 36);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(48)))), ((int)(((byte)(86)))));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = " Total:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtServiceCharge
            // 
            this.txtServiceCharge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtServiceCharge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txtServiceCharge.Location = new System.Drawing.Point(255, 3);
            this.txtServiceCharge.Name = "txtServiceCharge";
            this.txtServiceCharge.Size = new System.Drawing.Size(231, 26);
            this.txtServiceCharge.TabIndex = 1;
            this.txtServiceCharge.Tag = "";
            this.txtServiceCharge.Text = "14560000";
            // 
            // frmCheckout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(52)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(658, 904);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblRoomNumber);
            this.Controls.Add(this.txtTotalAmount);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MaximumSize = new System.Drawing.Size(680, 960);
            this.MinimumSize = new System.Drawing.Size(680, 960);
            this.Name = "frmCheckout";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin thanh toán";
            this.Load += new System.EventHandler(this.frmCheckout_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedService)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.Label lblRoomCharge;
        private System.Windows.Forms.Label lblServiceChange;
        private System.Windows.Forms.Label lblSurcharge;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblIssuedBy;
        private System.Windows.Forms.Label lblPaymentMethod;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtRoomCharge;
        private System.Windows.Forms.TextBox txtSurcharge;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.ComboBox cbxPaymentMethod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCheckin;
        private System.Windows.Forms.TextBox txtCheckout;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridView dgvUsedService;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServiceCharge;
    }
}