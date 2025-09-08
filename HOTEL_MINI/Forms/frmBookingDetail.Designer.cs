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
            this.btnClose = new System.Windows.Forms.Button();
            this.gbxUsedServices = new System.Windows.Forms.GroupBox();
            this.dgvUsedServices = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblReduceQuantity = new System.Windows.Forms.Label();
            this.nbrReduce = new System.Windows.Forms.NumericUpDown();
            this.btnReduce = new System.Windows.Forms.Button();
            this.gbxRoomInfor = new System.Windows.Forms.GroupBox();
            this.txtPricingType = new System.Windows.Forms.TextBox();
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
            this.lblSex = new System.Windows.Forms.Label();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblDiachi = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxServicesMenu = new System.Windows.Forms.GroupBox();
            this.dgvHotelServices = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblAddQuantity = new System.Windows.Forms.Label();
            this.nbrIncrease = new System.Windows.Forms.NumericUpDown();
            this.btnIncrease = new System.Windows.Forms.Button();
            this.btnTraphong = new System.Windows.Forms.Button();
            this.gbxUsedServices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedServices)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrReduce)).BeginInit();
            this.gbxRoomInfor.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbxServicesMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHotelServices)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrIncrease)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNumber.ForeColor = System.Drawing.Color.Red;
            this.lblRoomNumber.Location = new System.Drawing.Point(684, 3);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(273, 67);
            this.lblRoomNumber.TabIndex = 9;
            this.lblRoomNumber.Text = "Phòng 101";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(0, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(143, 47);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Trở lại";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gbxUsedServices
            // 
            this.gbxUsedServices.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gbxUsedServices.Controls.Add(this.dgvUsedServices);
            this.gbxUsedServices.Controls.Add(this.panel4);
            this.gbxUsedServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxUsedServices.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxUsedServices.Location = new System.Drawing.Point(388, 3);
            this.gbxUsedServices.Name = "gbxUsedServices";
            this.gbxUsedServices.Size = new System.Drawing.Size(531, 745);
            this.gbxUsedServices.TabIndex = 6;
            this.gbxUsedServices.TabStop = false;
            this.gbxUsedServices.Text = "Dịch vụ đang dùng";
            // 
            // dgvUsedServices
            // 
            this.dgvUsedServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsedServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsedServices.Location = new System.Drawing.Point(3, 40);
            this.dgvUsedServices.Name = "dgvUsedServices";
            this.dgvUsedServices.RowHeadersWidth = 62;
            this.dgvUsedServices.RowTemplate.Height = 28;
            this.dgvUsedServices.Size = new System.Drawing.Size(525, 605);
            this.dgvUsedServices.TabIndex = 22;
            this.dgvUsedServices.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsedServices_CellClick);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lblReduceQuantity);
            this.panel4.Controls.Add(this.nbrReduce);
            this.panel4.Controls.Add(this.btnReduce);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(3, 645);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(525, 97);
            this.panel4.TabIndex = 21;
            // 
            // lblReduceQuantity
            // 
            this.lblReduceQuantity.AutoSize = true;
            this.lblReduceQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReduceQuantity.Location = new System.Drawing.Point(72, 42);
            this.lblReduceQuantity.Name = "lblReduceQuantity";
            this.lblReduceQuantity.Size = new System.Drawing.Size(80, 20);
            this.lblReduceQuantity.TabIndex = 17;
            this.lblReduceQuantity.Text = "Số lượng";
            // 
            // nbrReduce
            // 
            this.nbrReduce.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbrReduce.Location = new System.Drawing.Point(167, 37);
            this.nbrReduce.Name = "nbrReduce";
            this.nbrReduce.Size = new System.Drawing.Size(78, 35);
            this.nbrReduce.TabIndex = 18;
            this.nbrReduce.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnReduce
            // 
            this.btnReduce.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReduce.Location = new System.Drawing.Point(309, 37);
            this.btnReduce.Name = "btnReduce";
            this.btnReduce.Size = new System.Drawing.Size(91, 39);
            this.btnReduce.TabIndex = 19;
            this.btnReduce.Text = "Bớt";
            this.btnReduce.UseVisualStyleBackColor = true;
            this.btnReduce.Click += new System.EventHandler(this.btnReduce_Click);
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
            this.gbxRoomInfor.Controls.Add(this.lblSex);
            this.gbxRoomInfor.Controls.Add(this.lblCCCD);
            this.gbxRoomInfor.Controls.Add(this.lblEmail);
            this.gbxRoomInfor.Controls.Add(this.lblSDT);
            this.gbxRoomInfor.Controls.Add(this.lblDiachi);
            this.gbxRoomInfor.Controls.Add(this.lblTen);
            this.gbxRoomInfor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxRoomInfor.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxRoomInfor.Location = new System.Drawing.Point(3, 3);
            this.gbxRoomInfor.Name = "gbxRoomInfor";
            this.gbxRoomInfor.Size = new System.Drawing.Size(379, 745);
            this.gbxRoomInfor.TabIndex = 5;
            this.gbxRoomInfor.TabStop = false;
            this.gbxRoomInfor.Text = "Thông tin ";
            this.gbxRoomInfor.Enter += new System.EventHandler(this.gbxRoomInfor_Enter);
            // 
            // txtPricingType
            // 
            this.txtPricingType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPricingType.Location = new System.Drawing.Point(107, 352);
            this.txtPricingType.Name = "txtPricingType";
            this.txtPricingType.Size = new System.Drawing.Size(128, 35);
            this.txtPricingType.TabIndex = 20;
            // 
            // txtGender
            // 
            this.txtGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGender.Location = new System.Drawing.Point(96, 295);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(358, 35);
            this.txtGender.TabIndex = 19;
            // 
            // txtCCCD
            // 
            this.txtCCCD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCCD.Location = new System.Drawing.Point(96, 241);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(358, 35);
            this.txtCCCD.TabIndex = 18;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(96, 193);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(358, 35);
            this.txtEmail.TabIndex = 17;
            // 
            // txtSDT
            // 
            this.txtSDT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSDT.Location = new System.Drawing.Point(96, 146);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(358, 35);
            this.txtSDT.TabIndex = 16;
            // 
            // txtDiachi
            // 
            this.txtDiachi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiachi.Location = new System.Drawing.Point(96, 99);
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.Size = new System.Drawing.Size(358, 35);
            this.txtDiachi.TabIndex = 15;
            // 
            // txtTen
            // 
            this.txtTen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTen.Location = new System.Drawing.Point(96, 51);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(358, 35);
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
            this.txtNote.Size = new System.Drawing.Size(358, 133);
            this.txtNote.TabIndex = 12;
            // 
            // dtpCheckoutTime
            // 
            this.dtpCheckoutTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpCheckoutTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckoutTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckoutTime.Location = new System.Drawing.Point(176, 469);
            this.dtpCheckoutTime.Name = "dtpCheckoutTime";
            this.dtpCheckoutTime.Size = new System.Drawing.Size(278, 30);
            this.dtpCheckoutTime.TabIndex = 11;
            // 
            // dtpCheckinTime
            // 
            this.dtpCheckinTime.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpCheckinTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpCheckinTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCheckinTime.Location = new System.Drawing.Point(176, 421);
            this.dtpCheckinTime.Name = "dtpCheckinTime";
            this.dtpCheckinTime.Size = new System.Drawing.Size(278, 30);
            this.dtpCheckinTime.TabIndex = 10;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.lblRoomNumber);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1504, 105);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.btnTraphong);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 856);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1504, 113);
            this.panel2.TabIndex = 11;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.80952F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.19048F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 581F));
            this.tableLayoutPanel1.Controls.Add(this.gbxRoomInfor, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbxUsedServices, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.gbxServicesMenu, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 105);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1504, 751);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // gbxServicesMenu
            // 
            this.gbxServicesMenu.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.gbxServicesMenu.Controls.Add(this.dgvHotelServices);
            this.gbxServicesMenu.Controls.Add(this.panel3);
            this.gbxServicesMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxServicesMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxServicesMenu.Location = new System.Drawing.Point(925, 3);
            this.gbxServicesMenu.Name = "gbxServicesMenu";
            this.gbxServicesMenu.Size = new System.Drawing.Size(576, 745);
            this.gbxServicesMenu.TabIndex = 9;
            this.gbxServicesMenu.TabStop = false;
            this.gbxServicesMenu.Text = "Menu dịch vụ";
            // 
            // dgvHotelServices
            // 
            this.dgvHotelServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHotelServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHotelServices.Location = new System.Drawing.Point(3, 40);
            this.dgvHotelServices.Name = "dgvHotelServices";
            this.dgvHotelServices.RowHeadersWidth = 62;
            this.dgvHotelServices.RowTemplate.Height = 28;
            this.dgvHotelServices.Size = new System.Drawing.Size(570, 607);
            this.dgvHotelServices.TabIndex = 21;
            this.dgvHotelServices.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHotelServices_CellClick);
            this.dgvHotelServices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHotelServices_CellContentClick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblAddQuantity);
            this.panel3.Controls.Add(this.nbrIncrease);
            this.panel3.Controls.Add(this.btnIncrease);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(3, 647);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(570, 95);
            this.panel3.TabIndex = 20;
            // 
            // lblAddQuantity
            // 
            this.lblAddQuantity.AutoSize = true;
            this.lblAddQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddQuantity.Location = new System.Drawing.Point(72, 42);
            this.lblAddQuantity.Name = "lblAddQuantity";
            this.lblAddQuantity.Size = new System.Drawing.Size(80, 20);
            this.lblAddQuantity.TabIndex = 17;
            this.lblAddQuantity.Text = "Số lượng";
            // 
            // nbrIncrease
            // 
            this.nbrIncrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nbrIncrease.Location = new System.Drawing.Point(167, 37);
            this.nbrIncrease.Name = "nbrIncrease";
            this.nbrIncrease.Size = new System.Drawing.Size(78, 35);
            this.nbrIncrease.TabIndex = 18;
            this.nbrIncrease.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnIncrease
            // 
            this.btnIncrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIncrease.Location = new System.Drawing.Point(309, 37);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(91, 39);
            this.btnIncrease.TabIndex = 19;
            this.btnIncrease.Text = "Thêm";
            this.btnIncrease.UseVisualStyleBackColor = true;
            this.btnIncrease.Click += new System.EventHandler(this.btnIncrease_Click);
            // 
            // btnTraphong
            // 
            this.btnTraphong.Location = new System.Drawing.Point(685, 42);
            this.btnTraphong.Name = "btnTraphong";
            this.btnTraphong.Size = new System.Drawing.Size(123, 59);
            this.btnTraphong.TabIndex = 0;
            this.btnTraphong.Text = "Trả Phòng";
            this.btnTraphong.UseVisualStyleBackColor = true;
            // 
            // frmBookingDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1504, 969);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmBookingDetail";
            this.Text = "frmBookingDetail";
            this.gbxUsedServices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedServices)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrReduce)).EndInit();
            this.gbxRoomInfor.ResumeLayout(false);
            this.gbxRoomInfor.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.gbxServicesMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHotelServices)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrIncrease)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox gbxUsedServices;
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
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.Label lblDiachi;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.TextBox txtPricingType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbxServicesMenu;
        private System.Windows.Forms.Button btnIncrease;
        private System.Windows.Forms.NumericUpDown nbrIncrease;
        private System.Windows.Forms.Label lblAddQuantity;
        private System.Windows.Forms.DataGridView dgvHotelServices;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvUsedServices;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblReduceQuantity;
        private System.Windows.Forms.NumericUpDown nbrReduce;
        private System.Windows.Forms.Button btnReduce;
        private System.Windows.Forms.Button btnTraphong;
    }
}