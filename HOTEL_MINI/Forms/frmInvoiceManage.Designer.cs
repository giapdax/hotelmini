// frmInvoiceManage.Designer.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    partial class frmInvoiceManage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelSearchCard = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumberID = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panelCustomerCard = new System.Windows.Forms.Panel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblTen = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblGioitinh = new System.Windows.Forms.Label();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.lblDiachi = new System.Windows.Forms.Label();
            this.txtDiachi = new System.Windows.Forms.TextBox();
            this.dgvBookings = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnXuatHoaDon = new System.Windows.Forms.Button();
            this.lblCountBookingByNumberID = new System.Windows.Forms.Label();
            this.txtCountBookingByNumberID = new System.Windows.Forms.TextBox();
            this.btnt = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelSearchCard.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelCustomerCard.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgvBookings, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1100, 640);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tableLayoutPanel2.Controls.Add(this.panelSearchCard, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.panelCustomerCard, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.Padding = new System.Windows.Forms.Padding(16, 16, 16, 8);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1094, 174);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // panelSearchCard
            // 
            this.panelSearchCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.panelSearchCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSearchCard.Controls.Add(this.panel1);
            this.panelSearchCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSearchCard.Location = new System.Drawing.Point(19, 19);
            this.panelSearchCard.Name = "panelSearchCard";
            this.panelSearchCard.Padding = new System.Windows.Forms.Padding(16);
            this.panelSearchCard.Size = new System.Drawing.Size(503, 144);
            this.panelSearchCard.TabIndex = 0;
            this.panelSearchCard.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorder);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblNumberID);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.btnReset);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(16, 16);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8);
            this.panel1.Size = new System.Drawing.Size(469, 110);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nhập CCCD để tìm hóa đơn";
            // 
            // lblNumberID
            // 
            this.lblNumberID.AutoSize = true;
            this.lblNumberID.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNumberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            this.lblNumberID.Location = new System.Drawing.Point(8, 48);
            this.lblNumberID.Name = "lblNumberID";
            this.lblNumberID.Size = new System.Drawing.Size(64, 28);
            this.lblNumberID.TabIndex = 1;
            this.lblNumberID.Text = "CCCD:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtSearch.Location = new System.Drawing.Point(72, 42);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(280, 39);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.White;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.btnReset.Location = new System.Drawing.Point(364, 42);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(98, 34);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(468, 42);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 34);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Kiểm tra";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // panelCustomerCard
            // 
            this.panelCustomerCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.panelCustomerCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCustomerCard.Controls.Add(this.tableLayoutPanel3);
            this.panelCustomerCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCustomerCard.Location = new System.Drawing.Point(528, 19);
            this.panelCustomerCard.Name = "panelCustomerCard";
            this.panelCustomerCard.Padding = new System.Windows.Forms.Padding(16);
            this.panelCustomerCard.Size = new System.Drawing.Size(547, 144);
            this.panelCustomerCard.TabIndex = 1;
            this.panelCustomerCard.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorder);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblTen, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblSDT, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtPhone, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.lblEmail, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtEmail, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblGioitinh, 2, 1);
            this.tableLayoutPanel3.Controls.Add(this.txtGender, 3, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblDiachi, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.txtDiachi, 1, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(16, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(513, 110);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblTen
            // 
            this.lblTen.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblTen.Location = new System.Drawing.Point(11, 7);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(100, 28);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Họ và tên:";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtName.Location = new System.Drawing.Point(120, 6);
            this.txtName.Margin = new System.Windows.Forms.Padding(6, 2, 12, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(124, 39);
            this.txtName.TabIndex = 1;
            // 
            // lblSDT
            // 
            this.lblSDT.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSDT.AutoSize = true;
            this.lblSDT.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSDT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblSDT.Location = new System.Drawing.Point(312, 7);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(51, 28);
            this.lblSDT.TabIndex = 2;
            this.lblSDT.Text = "SĐT:";
            // 
            // txtPhone
            // 
            this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtPhone.Location = new System.Drawing.Point(372, 6);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(6, 2, 12, 2);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(125, 39);
            this.txtPhone.TabIndex = 3;
            // 
            // lblEmail
            // 
            this.lblEmail.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblEmail.Location = new System.Drawing.Point(48, 41);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(63, 28);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtEmail.Location = new System.Drawing.Point(120, 40);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(6, 2, 12, 2);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(124, 39);
            this.txtEmail.TabIndex = 5;
            // 
            // lblGioitinh
            // 
            this.lblGioitinh.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblGioitinh.AutoSize = true;
            this.lblGioitinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGioitinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblGioitinh.Location = new System.Drawing.Point(272, 41);
            this.lblGioitinh.Name = "lblGioitinh";
            this.lblGioitinh.Size = new System.Drawing.Size(91, 28);
            this.lblGioitinh.TabIndex = 6;
            this.lblGioitinh.Text = "Giới tính:";
            // 
            // txtGender
            // 
            this.txtGender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGender.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtGender.Location = new System.Drawing.Point(372, 40);
            this.txtGender.Margin = new System.Windows.Forms.Padding(6, 2, 12, 2);
            this.txtGender.Name = "txtGender";
            this.txtGender.Size = new System.Drawing.Size(125, 39);
            this.txtGender.TabIndex = 7;
            // 
            // lblDiachi
            // 
            this.lblDiachi.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDiachi.AutoSize = true;
            this.lblDiachi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiachi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblDiachi.Location = new System.Drawing.Point(36, 75);
            this.lblDiachi.Name = "lblDiachi";
            this.lblDiachi.Size = new System.Drawing.Size(75, 28);
            this.lblDiachi.TabIndex = 8;
            this.lblDiachi.Text = "Địa chỉ:";
            // 
            // txtDiachi
            // 
            this.txtDiachi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.SetColumnSpan(this.txtDiachi, 3);
            this.txtDiachi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtDiachi.Location = new System.Drawing.Point(120, 74);
            this.txtDiachi.Margin = new System.Windows.Forms.Padding(6, 2, 12, 2);
            this.txtDiachi.Multiline = true;
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.Size = new System.Drawing.Size(377, 30);
            this.txtDiachi.TabIndex = 9;
            // 
            // dgvBookings
            // 
            this.dgvBookings.AllowUserToAddRows = false;
            this.dgvBookings.AllowUserToDeleteRows = false;
            this.dgvBookings.AllowUserToOrderColumns = true;
            this.dgvBookings.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.dgvBookings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvBookings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBookings.BackgroundColor = System.Drawing.Color.White;
            this.dgvBookings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dgvBookings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvBookings.ColumnHeadersHeight = 40;
            this.dgvBookings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBookings.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvBookings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvBookings.EnableHeadersVisualStyles = false;
            this.dgvBookings.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.dgvBookings.Location = new System.Drawing.Point(16, 188);
            this.dgvBookings.Margin = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.dgvBookings.MultiSelect = false;
            this.dgvBookings.Name = "dgvBookings";
            this.dgvBookings.ReadOnly = true;
            this.dgvBookings.RowHeadersVisible = false;
            this.dgvBookings.RowHeadersWidth = 62;
            this.dgvBookings.RowTemplate.Height = 32;
            this.dgvBookings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBookings.Size = new System.Drawing.Size(1068, 372);
            this.dgvBookings.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnXuatHoaDon);
            this.panel2.Controls.Add(this.lblCountBookingByNumberID);
            this.panel2.Controls.Add(this.txtCountBookingByNumberID);
            this.panel2.Controls.Add(this.btnt);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 571);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.panel2.Size = new System.Drawing.Size(1094, 66);
            this.panel2.TabIndex = 3;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorder);
            // 
            // btnXuatHoaDon
            // 
            this.btnXuatHoaDon.Location = new System.Drawing.Point(706, 15);
            this.btnXuatHoaDon.Name = "btnXuatHoaDon";
            this.btnXuatHoaDon.Size = new System.Drawing.Size(201, 41);
            this.btnXuatHoaDon.TabIndex = 3;
            this.btnXuatHoaDon.Text = "Xuất hóa đơn";
            this.btnXuatHoaDon.UseVisualStyleBackColor = true;
            this.btnXuatHoaDon.Click += new System.EventHandler(this.btnXuatHoaDon_Click);
            // 
            // lblCountBookingByNumberID
            // 
            this.lblCountBookingByNumberID.AutoSize = true;
            this.lblCountBookingByNumberID.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountBookingByNumberID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCountBookingByNumberID.Location = new System.Drawing.Point(12, 22);
            this.lblCountBookingByNumberID.Name = "lblCountBookingByNumberID";
            this.lblCountBookingByNumberID.Size = new System.Drawing.Size(199, 28);
            this.lblCountBookingByNumberID.TabIndex = 0;
            this.lblCountBookingByNumberID.Text = "Số lần book phòng";
            this.lblCountBookingByNumberID.Visible = false;
            // 
            // txtCountBookingByNumberID
            // 
            this.txtCountBookingByNumberID.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtCountBookingByNumberID.Location = new System.Drawing.Point(234, 17);
            this.txtCountBookingByNumberID.Name = "txtCountBookingByNumberID";
            this.txtCountBookingByNumberID.Size = new System.Drawing.Size(120, 39);
            this.txtCountBookingByNumberID.TabIndex = 1;
            this.txtCountBookingByNumberID.Visible = false;
            // 
            // btnt
            // 
            this.btnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.btnt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnt.FlatAppearance.BorderSize = 0;
            this.btnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnt.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnt.ForeColor = System.Drawing.Color.White;
            this.btnt.Location = new System.Drawing.Point(1794, 14);
            this.btnt.Name = "btnt";
            this.btnt.Size = new System.Drawing.Size(170, 40);
            this.btnt.TabIndex = 2;
            this.btnt.Text = "Xuất hóa đơn";
            this.btnt.UseVisualStyleBackColor = false;
            this.btnt.Click += new System.EventHandler(this.btnXuatHoaDon_Click);
            // 
            // frmInvoiceManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 640);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            this.Name = "frmInvoiceManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hóa đơn";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelSearchCard.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelCustomerCard.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBookings)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Vẽ border mảnh cho các "card" theo màu đã chọn (an toàn với Designer).
        /// </summary>
        private void PaintBorder(object sender, PaintEventArgs e)
        {
            Control c = sender as Control;
            if (c == null) return;
            using (Pen p = new Pen(Color.FromArgb(230, 235, 239)))
            {
                e.Graphics.DrawRectangle(p, 0, 0, c.Width - 1, c.Height - 1);
            }
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panelSearchCard;
        private Panel panelCustomerCard;
        private Panel panel1;
        private Button btnReset;
        private Label label1;
        private Button btnSearch;
        private TextBox txtSearch;
        private Label lblNumberID;
        private TableLayoutPanel tableLayoutPanel3;
        private TextBox txtName;
        private Label lblTen;
        private Label lblSDT;
        private TextBox txtPhone;
        private Label lblGioitinh;
        private TextBox txtGender;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblDiachi;
        private TextBox txtDiachi;
        private DataGridView dgvBookings;
        private Panel panel2;
        private Button btnt;
        private TextBox txtCountBookingByNumberID;
        private Label lblCountBookingByNumberID;
        private Button btnXuatHoaDon;
    }
}
