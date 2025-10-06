// frmBookingCreate.Designer.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    partial class frmBookingCreate
    {
        /// <summary> Required designer variable. </summary>
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grbRoom = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.gbxUsedServices = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvUsedServices = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblReduceQuantity = new System.Windows.Forms.Label();
            this.nbrReduce = new System.Windows.Forms.NumericUpDown();
            this.btnReduce = new System.Windows.Forms.Button();
            this.gbxServicesMenu = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvHotelServices = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblAddQuantity = new System.Windows.Forms.Label();
            this.nbrIncrease = new System.Windows.Forms.NumericUpDown();
            this.btnIncrease = new System.Windows.Forms.Button();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.lblDiachi = new System.Windows.Forms.Label();
            this.txtDiachi = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.txtCCCD = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cboGender = new System.Windows.Forms.ComboBox();
            this.gbxRoomInfor = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblBookingID = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.btnDatphong = new System.Windows.Forms.Button();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.grbRoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel11.SuspendLayout();
            this.gbxUsedServices.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedServices)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrReduce)).BeginInit();
            this.gbxServicesMenu.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHotelServices)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nbrIncrease)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.gbxRoomInfor.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbRoom
            // 
            this.grbRoom.BackColor = System.Drawing.Color.White;
            this.grbRoom.Controls.Add(this.dataGridView1);
            this.grbRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbRoom.Location = new System.Drawing.Point(15, 3);
            this.grbRoom.Name = "grbRoom";
            this.grbRoom.Padding = new System.Windows.Forms.Padding(10);
            this.grbRoom.Size = new System.Drawing.Size(799, 224);
            this.grbRoom.TabIndex = 0;
            this.grbRoom.TabStop = false;
            this.grbRoom.Text = "Phòng sử dụng";
            this.grbRoom.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorderSoft);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.dataGridView1.Location = new System.Drawing.Point(10, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 32;
            this.dataGridView1.Size = new System.Drawing.Size(779, 177);
            this.dataGridView1.TabIndex = 0;
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.gbxUsedServices, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.gbxServicesMenu, 1, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(15, 233);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(799, 244);
            this.tableLayoutPanel11.TabIndex = 1;
            // 
            // gbxUsedServices
            // 
            this.gbxUsedServices.BackColor = System.Drawing.Color.White;
            this.gbxUsedServices.Controls.Add(this.tableLayoutPanel4);
            this.gbxUsedServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxUsedServices.Location = new System.Drawing.Point(3, 15);
            this.gbxUsedServices.Name = "gbxUsedServices";
            this.gbxUsedServices.Padding = new System.Windows.Forms.Padding(10);
            this.gbxUsedServices.Size = new System.Drawing.Size(393, 226);
            this.gbxUsedServices.TabIndex = 0;
            this.gbxUsedServices.TabStop = false;
            this.gbxUsedServices.Text = "Dịch vụ đang dùng";
            this.gbxUsedServices.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorderSoft);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.dgvUsedServices, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(10, 37);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(373, 179);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // dgvUsedServices
            // 
            this.dgvUsedServices.AllowUserToAddRows = false;
            this.dgvUsedServices.AllowUserToDeleteRows = false;
            this.dgvUsedServices.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsedServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsedServices.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsedServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvUsedServices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsedServices.ColumnHeadersHeight = 40;
            this.dgvUsedServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsedServices.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvUsedServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsedServices.EnableHeadersVisualStyles = false;
            this.dgvUsedServices.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.dgvUsedServices.Location = new System.Drawing.Point(3, 3);
            this.dgvUsedServices.Name = "dgvUsedServices";
            this.dgvUsedServices.ReadOnly = true;
            this.dgvUsedServices.RowHeadersWidth = 62;
            this.dgvUsedServices.RowTemplate.Height = 32;
            this.dgvUsedServices.Size = new System.Drawing.Size(367, 133);
            this.dgvUsedServices.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblReduceQuantity, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.nbrReduce, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnReduce, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 142);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(367, 34);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // lblReduceQuantity
            // 
            this.lblReduceQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReduceQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblReduceQuantity.Location = new System.Drawing.Point(3, 0);
            this.lblReduceQuantity.Name = "lblReduceQuantity";
            this.lblReduceQuantity.Size = new System.Drawing.Size(84, 34);
            this.lblReduceQuantity.TabIndex = 0;
            this.lblReduceQuantity.Text = "Số lượng";
            this.lblReduceQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nbrReduce
            // 
            this.nbrReduce.Dock = System.Windows.Forms.DockStyle.Left;
            this.nbrReduce.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.nbrReduce.Location = new System.Drawing.Point(93, 3);
            this.nbrReduce.Name = "nbrReduce";
            this.nbrReduce.Size = new System.Drawing.Size(100, 37);
            this.nbrReduce.TabIndex = 1;
            this.nbrReduce.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnReduce
            // 
            this.btnReduce.BackColor = System.Drawing.Color.White;
            this.btnReduce.FlatAppearance.BorderSize = 0;
            this.btnReduce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReduce.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnReduce.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.btnReduce.Location = new System.Drawing.Point(231, 3);
            this.btnReduce.Name = "btnReduce";
            this.btnReduce.Size = new System.Drawing.Size(110, 23);
            this.btnReduce.TabIndex = 2;
            this.btnReduce.Text = "Bớt";
            this.btnReduce.UseVisualStyleBackColor = false;
            this.btnReduce.Click += new System.EventHandler(this.BtnReduce_Click);
            // 
            // gbxServicesMenu
            // 
            this.gbxServicesMenu.BackColor = System.Drawing.Color.White;
            this.gbxServicesMenu.Controls.Add(this.tableLayoutPanel6);
            this.gbxServicesMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxServicesMenu.Location = new System.Drawing.Point(402, 15);
            this.gbxServicesMenu.Name = "gbxServicesMenu";
            this.gbxServicesMenu.Padding = new System.Windows.Forms.Padding(10);
            this.gbxServicesMenu.Size = new System.Drawing.Size(394, 226);
            this.gbxServicesMenu.TabIndex = 1;
            this.gbxServicesMenu.TabStop = false;
            this.gbxServicesMenu.Text = "Menu dịch vụ";
            this.gbxServicesMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorderSoft);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.dgvHotelServices, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(10, 37);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(374, 179);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // dgvHotelServices
            // 
            this.dgvHotelServices.AllowUserToAddRows = false;
            this.dgvHotelServices.AllowUserToDeleteRows = false;
            this.dgvHotelServices.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHotelServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHotelServices.BackgroundColor = System.Drawing.Color.White;
            this.dgvHotelServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHotelServices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHotelServices.ColumnHeadersHeight = 40;
            this.dgvHotelServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHotelServices.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvHotelServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHotelServices.EnableHeadersVisualStyles = false;
            this.dgvHotelServices.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.dgvHotelServices.Location = new System.Drawing.Point(3, 3);
            this.dgvHotelServices.Name = "dgvHotelServices";
            this.dgvHotelServices.ReadOnly = true;
            this.dgvHotelServices.RowHeadersWidth = 62;
            this.dgvHotelServices.RowTemplate.Height = 32;
            this.dgvHotelServices.Size = new System.Drawing.Size(368, 133);
            this.dgvHotelServices.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblAddQuantity, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.nbrIncrease, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnIncrease, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 142);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(368, 34);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // lblAddQuantity
            // 
            this.lblAddQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAddQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblAddQuantity.Location = new System.Drawing.Point(3, 0);
            this.lblAddQuantity.Name = "lblAddQuantity";
            this.lblAddQuantity.Size = new System.Drawing.Size(84, 34);
            this.lblAddQuantity.TabIndex = 0;
            this.lblAddQuantity.Text = "Số lượng";
            this.lblAddQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nbrIncrease
            // 
            this.nbrIncrease.Dock = System.Windows.Forms.DockStyle.Left;
            this.nbrIncrease.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.nbrIncrease.Location = new System.Drawing.Point(93, 3);
            this.nbrIncrease.Name = "nbrIncrease";
            this.nbrIncrease.Size = new System.Drawing.Size(100, 37);
            this.nbrIncrease.TabIndex = 1;
            this.nbrIncrease.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnIncrease
            // 
            this.btnIncrease.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.btnIncrease.FlatAppearance.BorderSize = 0;
            this.btnIncrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIncrease.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnIncrease.ForeColor = System.Drawing.Color.White;
            this.btnIncrease.Location = new System.Drawing.Point(232, 3);
            this.btnIncrease.Name = "btnIncrease";
            this.btnIncrease.Size = new System.Drawing.Size(110, 23);
            this.btnIncrease.TabIndex = 2;
            this.btnIncrease.Text = "Thêm";
            this.btnIncrease.UseVisualStyleBackColor = false;
            this.btnIncrease.Click += new System.EventHandler(this.BtnIncrease_Click);
            // 
            // txtTen
            // 
            this.txtTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTen.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtTen.Location = new System.Drawing.Point(93, 3);
            this.txtTen.Name = "txtTen";
            this.txtTen.Size = new System.Drawing.Size(198, 35);
            this.txtTen.TabIndex = 1;
            // 
            // lblDiachi
            // 
            this.lblDiachi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDiachi.ForeColor = System.Drawing.Color.Black;
            this.lblDiachi.Location = new System.Drawing.Point(3, 36);
            this.lblDiachi.Name = "lblDiachi";
            this.lblDiachi.Size = new System.Drawing.Size(84, 36);
            this.lblDiachi.TabIndex = 2;
            this.lblDiachi.Text = "Địa chỉ:";
            this.lblDiachi.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiachi
            // 
            this.txtDiachi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiachi.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtDiachi.Location = new System.Drawing.Point(93, 39);
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.Size = new System.Drawing.Size(198, 35);
            this.txtDiachi.TabIndex = 3;
            // 
            // txtEmail
            // 
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtEmail.Location = new System.Drawing.Point(93, 111);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(198, 35);
            this.txtEmail.TabIndex = 7;
            // 
            // lblSDT
            // 
            this.lblSDT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSDT.ForeColor = System.Drawing.Color.Black;
            this.lblSDT.Location = new System.Drawing.Point(3, 72);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(84, 36);
            this.lblSDT.TabIndex = 4;
            this.lblSDT.Text = "SĐT:";
            this.lblSDT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSDT
            // 
            this.txtSDT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSDT.Location = new System.Drawing.Point(93, 75);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(198, 35);
            this.txtSDT.TabIndex = 5;
            // 
            // lblEmail
            // 
            this.lblEmail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblEmail.ForeColor = System.Drawing.Color.Black;
            this.lblEmail.Location = new System.Drawing.Point(3, 108);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(84, 36);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email:";
            this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTen
            // 
            this.lblTen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTen.ForeColor = System.Drawing.Color.Black;
            this.lblTen.Location = new System.Drawing.Point(3, 0);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(84, 36);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Tên:";
            this.lblTen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSex
            // 
            this.lblSex.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSex.ForeColor = System.Drawing.Color.Black;
            this.lblSex.Location = new System.Drawing.Point(3, 144);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(84, 36);
            this.lblSex.TabIndex = 8;
            this.lblSex.Text = "Giới tính:";
            this.lblSex.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCCCD
            // 
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCCCD.ForeColor = System.Drawing.Color.Black;
            this.lblCCCD.Location = new System.Drawing.Point(12, 28);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(64, 28);
            this.lblCCCD.TabIndex = 0;
            this.lblCCCD.Text = "CCCD:";
            // 
            // txtCCCD
            // 
            this.txtCCCD.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCCCD.Location = new System.Drawing.Point(76, 24);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new System.Drawing.Size(218, 34);
            this.txtCCCD.TabIndex = 1;
            // 
            // lblNote
            // 
            this.lblNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNote.ForeColor = System.Drawing.Color.Black;
            this.lblNote.Location = new System.Drawing.Point(3, 180);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(84, 72);
            this.lblNote.TabIndex = 10;
            this.lblNote.Text = "Ghi chú:";
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNote
            // 
            this.txtNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNote.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtNote.Location = new System.Drawing.Point(93, 183);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(198, 66);
            this.txtNote.TabIndex = 11;
            // 
            // btnCheck
            // 
            this.btnCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.btnCheck.FlatAppearance.BorderSize = 0;
            this.btnCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheck.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCheck.ForeColor = System.Drawing.Color.White;
            this.btnCheck.Location = new System.Drawing.Point(76, 60);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(100, 32);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "Kiểm tra";
            this.btnCheck.UseVisualStyleBackColor = false;
            this.btnCheck.Click += new System.EventHandler(this.BtnCheck_Click);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.lblTen, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtTen, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.lblDiachi, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.txtDiachi, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.lblSDT, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.txtSDT, 1, 2);
            this.tableLayoutPanel5.Controls.Add(this.lblEmail, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.txtEmail, 1, 3);
            this.tableLayoutPanel5.Controls.Add(this.lblSex, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.cboGender, 1, 4);
            this.tableLayoutPanel5.Controls.Add(this.lblNote, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.txtNote, 1, 5);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 98);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 6;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(294, 252);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // cboGender
            // 
            this.cboGender.Dock = System.Windows.Forms.DockStyle.Left;
            this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGender.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.cboGender.Location = new System.Drawing.Point(93, 147);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(140, 38);
            this.cboGender.TabIndex = 9;
            // 
            // gbxRoomInfor
            // 
            this.gbxRoomInfor.BackColor = System.Drawing.Color.White;
            this.gbxRoomInfor.Controls.Add(this.lblCCCD);
            this.gbxRoomInfor.Controls.Add(this.txtCCCD);
            this.gbxRoomInfor.Controls.Add(this.btnCheck);
            this.gbxRoomInfor.Controls.Add(this.tableLayoutPanel5);
            this.gbxRoomInfor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbxRoomInfor.Location = new System.Drawing.Point(3, 15);
            this.gbxRoomInfor.Name = "gbxRoomInfor";
            this.gbxRoomInfor.Padding = new System.Windows.Forms.Padding(12);
            this.gbxRoomInfor.Size = new System.Drawing.Size(294, 480);
            this.gbxRoomInfor.TabIndex = 0;
            this.gbxRoomInfor.TabStop = false;
            this.gbxRoomInfor.Text = "Thông tin khách";
            this.gbxRoomInfor.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorderSoft);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gbxRoomInfor, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel10, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 87);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 12, 0, 12);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1123, 510);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.grbRoom, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel11, 0, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(303, 15);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(817, 480);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.btnClose.Location = new System.Drawing.Point(8, 8);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 50);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Trở lại";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // lblBookingID
            // 
            this.tableLayoutPanel7.SetColumnSpan(this.lblBookingID, 2);
            this.lblBookingID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBookingID.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblBookingID.ForeColor = System.Drawing.Color.White;
            this.lblBookingID.Location = new System.Drawing.Point(131, 8);
            this.lblBookingID.Name = "lblBookingID";
            this.lblBookingID.Size = new System.Drawing.Size(981, 50);
            this.lblBookingID.TabIndex = 1;
            this.lblBookingID.Text = "Phòng 101";
            this.lblBookingID.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.tableLayoutPanel7.ColumnCount = 3;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.btnClose, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblBookingID, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(15, 15);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.Padding = new System.Windows.Forms.Padding(8);
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1123, 66);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // btnDatphong
            // 
            this.btnDatphong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.btnDatphong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDatphong.FlatAppearance.BorderSize = 0;
            this.btnDatphong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatphong.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnDatphong.ForeColor = System.Drawing.Color.White;
            this.btnDatphong.Location = new System.Drawing.Point(479, 8);
            this.btnDatphong.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btnDatphong.Name = "btnDatphong";
            this.btnDatphong.Size = new System.Drawing.Size(164, 50);
            this.btnDatphong.TabIndex = 0;
            this.btnDatphong.Text = "Đặt phòng";
            this.btnDatphong.UseVisualStyleBackColor = false;
            this.btnDatphong.Click += new System.EventHandler(this.BtnDatphong_Click);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 3;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.btnDatphong, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(15, 603);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1123, 58);
            this.tableLayoutPanel9.TabIndex = 2;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 0, 2);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.Padding = new System.Windows.Forms.Padding(12);
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1153, 676);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // frmBookingCreate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1153, 676);
            this.Controls.Add(this.tableLayoutPanel8);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            this.Name = "frmBookingCreate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đặt phòng";
            this.grbRoom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.gbxUsedServices.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedServices)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nbrReduce)).EndInit();
            this.gbxServicesMenu.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHotelServices)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nbrIncrease)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.gbxRoomInfor.ResumeLayout(false);
            this.gbxRoomInfor.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Vẽ viền nhẹ theo phong cách card – an toàn với Designer.
        /// </summary>
        private void PaintBorderSoft(object sender, PaintEventArgs e)
        {
            Control c = sender as Control;
            if (c == null) return;
            using (Pen p = new Pen(Color.FromArgb(230, 235, 239)))
            {
                e.Graphics.DrawRectangle(p, 0, 0, c.Width - 1, c.Height - 1);
            }
        }

        #endregion

        private System.Windows.Forms.GroupBox grbRoom;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.GroupBox gbxUsedServices;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btnReduce;
        private System.Windows.Forms.Label lblReduceQuantity;
        private System.Windows.Forms.NumericUpDown nbrReduce;
        private System.Windows.Forms.DataGridView dgvUsedServices;
        private System.Windows.Forms.GroupBox gbxServicesMenu;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnIncrease;
        private System.Windows.Forms.NumericUpDown nbrIncrease;
        private System.Windows.Forms.Label lblAddQuantity;
        private System.Windows.Forms.DataGridView dgvHotelServices;
        private System.Windows.Forms.TextBox txtTen;
        private System.Windows.Forms.Label lblDiachi;
        private System.Windows.Forms.TextBox txtDiachi;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblTen;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblCCCD;
        private System.Windows.Forms.TextBox txtCCCD;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.ComboBox cboGender;
        private System.Windows.Forms.GroupBox gbxRoomInfor;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblBookingID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Button btnDatphong;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
    }
}
