// frmCheckout.Designer.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    partial class frmCheckout
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainLayout = new System.Windows.Forms.TableLayoutPanel();
            this.grpHeader = new System.Windows.Forms.GroupBox();
            this.headerLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblCusName = new System.Windows.Forms.Label();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.lblIdNumber = new System.Windows.Forms.Label();
            this.txtCusId = new System.Windows.Forms.TextBox();
            this.middleLayout = new System.Windows.Forms.TableLayoutPanel();
            this.grpServices = new System.Windows.Forms.GroupBox();
            this.serviceLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dgvUsedService = new System.Windows.Forms.DataGridView();
            this.lblServiceTotal = new System.Windows.Forms.Label();
            this.txtServiceCharge = new System.Windows.Forms.TextBox();
            this.grpRooms = new System.Windows.Forms.GroupBox();
            this.roomLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dgvRoom = new System.Windows.Forms.DataGridView();
            this.lblRoomTotal = new System.Windows.Forms.Label();
            this.txtTongtien = new System.Windows.Forms.TextBox();
            this.grpSummary = new System.Windows.Forms.GroupBox();
            this.summaryLayout = new System.Windows.Forms.TableLayoutPanel();
            this.leftFormLayout = new System.Windows.Forms.TableLayoutPanel();
            this.lblRoomTotal2 = new System.Windows.Forms.Label();
            this.txtRoomTotal2 = new System.Windows.Forms.TextBox();
            this.lblServiceTotal2 = new System.Windows.Forms.Label();
            this.txtServiceTotal2 = new System.Windows.Forms.TextBox();
            this.lblSurcharge = new System.Windows.Forms.Label();
            this.txtSurcharge = new System.Windows.Forms.TextBox();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cbxPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblIssuedBy = new System.Windows.Forms.Label();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.buttonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.mainLayout.SuspendLayout();
            this.grpHeader.SuspendLayout();
            this.headerLayout.SuspendLayout();
            this.middleLayout.SuspendLayout();
            this.grpServices.SuspendLayout();
            this.serviceLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedService)).BeginInit();
            this.grpRooms.SuspendLayout();
            this.roomLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoom)).BeginInit();
            this.grpSummary.SuspendLayout();
            this.summaryLayout.SuspendLayout();
            this.leftFormLayout.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainLayout
            // 
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.Controls.Add(this.grpHeader, 0, 0);
            this.mainLayout.Controls.Add(this.middleLayout, 0, 1);
            this.mainLayout.Controls.Add(this.grpSummary, 0, 2);
            this.mainLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainLayout.Location = new System.Drawing.Point(0, 0);
            this.mainLayout.Name = "mainLayout";
            this.mainLayout.Padding = new System.Windows.Forms.Padding(14);
            this.mainLayout.RowCount = 3;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 250F));
            this.mainLayout.Size = new System.Drawing.Size(1120, 689);
            this.mainLayout.TabIndex = 0;
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.headerLayout);
            this.grpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            this.grpHeader.Location = new System.Drawing.Point(17, 17);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Padding = new System.Windows.Forms.Padding(10);
            this.grpHeader.Size = new System.Drawing.Size(1086, 76);
            this.grpHeader.TabIndex = 0;
            this.grpHeader.TabStop = false;
            this.grpHeader.Text = "Thông tin khách / thời gian";
            this.grpHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorderSoft);
            // 
            // headerLayout
            // 
            this.headerLayout.ColumnCount = 6;
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.headerLayout.Controls.Add(this.lblCusName, 0, 0);
            this.headerLayout.Controls.Add(this.txtCusName, 1, 0);
            this.headerLayout.Controls.Add(this.lblIdNumber, 2, 0);
            this.headerLayout.Controls.Add(this.txtCusId, 3, 0);
            this.headerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerLayout.Location = new System.Drawing.Point(10, 36);
            this.headerLayout.Name = "headerLayout";
            this.headerLayout.RowCount = 1;
            this.headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.headerLayout.Size = new System.Drawing.Size(1066, 30);
            this.headerLayout.TabIndex = 0;
            // 
            // lblCusName
            // 
            this.lblCusName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCusName.AutoSize = true;
            this.lblCusName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCusName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblCusName.Location = new System.Drawing.Point(47, 0);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(70, 30);
            this.lblCusName.TabIndex = 0;
            this.lblCusName.Text = "Khách hàng:";
            // 
            // txtCusName
            // 
            this.txtCusName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCusName.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.txtCusName.Location = new System.Drawing.Point(123, 3);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(288, 38);
            this.txtCusName.TabIndex = 1;
            // 
            // lblIdNumber
            // 
            this.lblIdNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblIdNumber.AutoSize = true;
            this.lblIdNumber.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIdNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblIdNumber.Location = new System.Drawing.Point(437, 1);
            this.lblIdNumber.Name = "lblIdNumber";
            this.lblIdNumber.Size = new System.Drawing.Size(64, 28);
            this.lblIdNumber.TabIndex = 2;
            this.lblIdNumber.Text = "CCCD:";
            // 
            // txtCusId
            // 
            this.txtCusId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCusId.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.txtCusId.Location = new System.Drawing.Point(507, 3);
            this.txtCusId.Name = "txtCusId";
            this.txtCusId.Size = new System.Drawing.Size(178, 38);
            this.txtCusId.TabIndex = 3;
            // 
            // middleLayout
            // 
            this.middleLayout.ColumnCount = 2;
            this.middleLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.middleLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.middleLayout.Controls.Add(this.grpServices, 0, 0);
            this.middleLayout.Controls.Add(this.grpRooms, 1, 0);
            this.middleLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.middleLayout.Location = new System.Drawing.Point(17, 99);
            this.middleLayout.Name = "middleLayout";
            this.middleLayout.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.middleLayout.RowCount = 1;
            this.middleLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.middleLayout.Size = new System.Drawing.Size(1086, 323);
            this.middleLayout.TabIndex = 1;
            // 
            // grpServices
            // 
            this.grpServices.BackColor = System.Drawing.Color.White;
            this.grpServices.Controls.Add(this.serviceLayout);
            this.grpServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpServices.Location = new System.Drawing.Point(3, 9);
            this.grpServices.Name = "grpServices";
            this.grpServices.Padding = new System.Windows.Forms.Padding(10);
            this.grpServices.Size = new System.Drawing.Size(537, 305);
            this.grpServices.TabIndex = 0;
            this.grpServices.TabStop = false;
            this.grpServices.Text = "Dịch vụ đã dùng";
            this.grpServices.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorderSoft);
            // 
            // serviceLayout
            // 
            this.serviceLayout.ColumnCount = 2;
            this.serviceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.serviceLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.serviceLayout.Controls.Add(this.dgvUsedService, 0, 0);
            this.serviceLayout.Controls.Add(this.lblServiceTotal, 0, 1);
            this.serviceLayout.Controls.Add(this.txtServiceCharge, 1, 1);
            this.serviceLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serviceLayout.Location = new System.Drawing.Point(10, 36);
            this.serviceLayout.Name = "serviceLayout";
            this.serviceLayout.RowCount = 2;
            this.serviceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.serviceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.serviceLayout.Size = new System.Drawing.Size(517, 259);
            this.serviceLayout.TabIndex = 0;
            // 
            // dgvUsedService
            // 
            this.dgvUsedService.AllowUserToAddRows = false;
            this.dgvUsedService.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.dgvUsedService.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsedService.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsedService.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsedService.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.dgvUsedService.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsedService.ColumnHeadersHeight = 40;
            this.dgvUsedService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.serviceLayout.SetColumnSpan(this.dgvUsedService, 2);
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsedService.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUsedService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsedService.EnableHeadersVisualStyles = false;
            this.dgvUsedService.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.dgvUsedService.Location = new System.Drawing.Point(3, 3);
            this.dgvUsedService.MultiSelect = false;
            this.dgvUsedService.Name = "dgvUsedService";
            this.dgvUsedService.ReadOnly = true;
            this.dgvUsedService.RowHeadersVisible = false;
            this.dgvUsedService.RowHeadersWidth = 62;
            this.dgvUsedService.RowTemplate.Height = 32;
            this.dgvUsedService.Size = new System.Drawing.Size(511, 217);
            this.dgvUsedService.TabIndex = 0;
            // 
            // lblServiceTotal
            // 
            this.lblServiceTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblServiceTotal.AutoSize = true;
            this.lblServiceTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblServiceTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblServiceTotal.Location = new System.Drawing.Point(266, 227);
            this.lblServiceTotal.Name = "lblServiceTotal";
            this.lblServiceTotal.Size = new System.Drawing.Size(92, 28);
            this.lblServiceTotal.TabIndex = 1;
            this.lblServiceTotal.Text = "Tổng DV:";
            // 
            // txtServiceCharge
            // 
            this.txtServiceCharge.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtServiceCharge.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.txtServiceCharge.Location = new System.Drawing.Point(364, 226);
            this.txtServiceCharge.Name = "txtServiceCharge";
            this.txtServiceCharge.ReadOnly = true;
            this.txtServiceCharge.Size = new System.Drawing.Size(150, 38);
            this.txtServiceCharge.TabIndex = 2;
            this.txtServiceCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpRooms
            // 
            this.grpRooms.BackColor = System.Drawing.Color.White;
            this.grpRooms.Controls.Add(this.roomLayout);
            this.grpRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRooms.Location = new System.Drawing.Point(546, 9);
            this.grpRooms.Name = "grpRooms";
            this.grpRooms.Padding = new System.Windows.Forms.Padding(10);
            this.grpRooms.Size = new System.Drawing.Size(537, 305);
            this.grpRooms.TabIndex = 1;
            this.grpRooms.TabStop = false;
            this.grpRooms.Text = "Phòng muốn thanh toán";
            this.grpRooms.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorderSoft);
            // 
            // roomLayout
            // 
            this.roomLayout.ColumnCount = 2;
            this.roomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.roomLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.roomLayout.Controls.Add(this.dgvRoom, 0, 0);
            this.roomLayout.Controls.Add(this.lblRoomTotal, 0, 1);
            this.roomLayout.Controls.Add(this.txtTongtien, 1, 1);
            this.roomLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roomLayout.Location = new System.Drawing.Point(10, 36);
            this.roomLayout.Name = "roomLayout";
            this.roomLayout.RowCount = 2;
            this.roomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.roomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.roomLayout.Size = new System.Drawing.Size(517, 259);
            this.roomLayout.TabIndex = 0;
            // 
            // dgvRoom
            // 
            this.dgvRoom.AllowUserToAddRows = false;
            this.dgvRoom.AllowUserToDeleteRows = false;
            this.dgvRoom.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRoom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoom.BackgroundColor = System.Drawing.Color.White;
            this.dgvRoom.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRoom.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRoom.ColumnHeadersHeight = 40;
            this.dgvRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.roomLayout.SetColumnSpan(this.dgvRoom, 2);
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(241)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRoom.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoom.EnableHeadersVisualStyles = false;
            this.dgvRoom.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(239)))));
            this.dgvRoom.Location = new System.Drawing.Point(3, 3);
            this.dgvRoom.MultiSelect = false;
            this.dgvRoom.Name = "dgvRoom";
            this.dgvRoom.ReadOnly = true;
            this.dgvRoom.RowHeadersVisible = false;
            this.dgvRoom.RowHeadersWidth = 62;
            this.dgvRoom.RowTemplate.Height = 32;
            this.dgvRoom.Size = new System.Drawing.Size(511, 217);
            this.dgvRoom.TabIndex = 0;
            // 
            // lblRoomTotal
            // 
            this.lblRoomTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRoomTotal.AutoSize = true;
            this.lblRoomTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRoomTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblRoomTotal.Location = new System.Drawing.Point(243, 227);
            this.lblRoomTotal.Name = "lblRoomTotal";
            this.lblRoomTotal.Size = new System.Drawing.Size(115, 28);
            this.lblRoomTotal.TabIndex = 1;
            this.lblRoomTotal.Text = "Tiền phòng:";
            // 
            // txtTongtien
            // 
            this.txtTongtien.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTongtien.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.txtTongtien.Location = new System.Drawing.Point(364, 226);
            this.txtTongtien.Name = "txtTongtien";
            this.txtTongtien.ReadOnly = true;
            this.txtTongtien.Size = new System.Drawing.Size(150, 38);
            this.txtTongtien.TabIndex = 2;
            this.txtTongtien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpSummary
            // 
            this.grpSummary.BackColor = System.Drawing.Color.White;
            this.grpSummary.Controls.Add(this.summaryLayout);
            this.grpSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSummary.Location = new System.Drawing.Point(17, 428);
            this.grpSummary.Name = "grpSummary";
            this.grpSummary.Padding = new System.Windows.Forms.Padding(10);
            this.grpSummary.Size = new System.Drawing.Size(1086, 244);
            this.grpSummary.TabIndex = 2;
            this.grpSummary.TabStop = false;
            this.grpSummary.Text = "Tổng kết thanh toán";
            this.grpSummary.Paint += new System.Windows.Forms.PaintEventHandler(this.PaintBorderSoft);
            // 
            // summaryLayout
            // 
            this.summaryLayout.ColumnCount = 1;
            this.summaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.summaryLayout.Controls.Add(this.leftFormLayout, 0, 0);
            this.summaryLayout.Controls.Add(this.buttonsPanel, 0, 1);
            this.summaryLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryLayout.Location = new System.Drawing.Point(10, 36);
            this.summaryLayout.Name = "summaryLayout";
            this.summaryLayout.RowCount = 2;
            this.summaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.summaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.summaryLayout.Size = new System.Drawing.Size(1066, 198);
            this.summaryLayout.TabIndex = 0;
            // 
            // leftFormLayout
            // 
            this.leftFormLayout.ColumnCount = 4;
            this.leftFormLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.leftFormLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.leftFormLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.leftFormLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.leftFormLayout.Controls.Add(this.lblRoomTotal2, 0, 0);
            this.leftFormLayout.Controls.Add(this.txtRoomTotal2, 1, 0);
            this.leftFormLayout.Controls.Add(this.lblServiceTotal2, 2, 0);
            this.leftFormLayout.Controls.Add(this.txtServiceTotal2, 3, 0);
            this.leftFormLayout.Controls.Add(this.lblSurcharge, 0, 1);
            this.leftFormLayout.Controls.Add(this.txtSurcharge, 1, 1);
            this.leftFormLayout.Controls.Add(this.lblDiscount, 2, 1);
            this.leftFormLayout.Controls.Add(this.txtDiscount, 3, 1);
            this.leftFormLayout.Controls.Add(this.lblPaymentMethod, 0, 2);
            this.leftFormLayout.Controls.Add(this.cbxPaymentMethod, 1, 2);
            this.leftFormLayout.Controls.Add(this.lblSubtotal, 2, 2);
            this.leftFormLayout.Controls.Add(this.txtSubtotal, 3, 2);
            this.leftFormLayout.Controls.Add(this.lblIssuedBy, 0, 3);
            this.leftFormLayout.Controls.Add(this.txtEmployeeName, 1, 3);
            this.leftFormLayout.Controls.Add(this.lblNote, 2, 3);
            this.leftFormLayout.Controls.Add(this.txtNote, 3, 3);
            this.leftFormLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftFormLayout.Location = new System.Drawing.Point(3, 3);
            this.leftFormLayout.Name = "leftFormLayout";
            this.leftFormLayout.Padding = new System.Windows.Forms.Padding(4);
            this.leftFormLayout.RowCount = 4;
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.leftFormLayout.Size = new System.Drawing.Size(1060, 140);
            this.leftFormLayout.TabIndex = 0;
            // 
            // lblRoomTotal2
            // 
            this.lblRoomTotal2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRoomTotal2.AutoSize = true;
            this.lblRoomTotal2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRoomTotal2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblRoomTotal2.Location = new System.Drawing.Point(149, 9);
            this.lblRoomTotal2.Name = "lblRoomTotal2";
            this.lblRoomTotal2.Size = new System.Drawing.Size(115, 28);
            this.lblRoomTotal2.TabIndex = 0;
            this.lblRoomTotal2.Text = "Tiền phòng:";
            // 
            // txtRoomTotal2
            // 
            this.txtRoomTotal2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtRoomTotal2.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.txtRoomTotal2.Location = new System.Drawing.Point(270, 7);
            this.txtRoomTotal2.Name = "txtRoomTotal2";
            this.txtRoomTotal2.ReadOnly = true;
            this.txtRoomTotal2.Size = new System.Drawing.Size(180, 38);
            this.txtRoomTotal2.TabIndex = 1;
            this.txtRoomTotal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblServiceTotal2
            // 
            this.lblServiceTotal2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblServiceTotal2.AutoSize = true;
            this.lblServiceTotal2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblServiceTotal2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblServiceTotal2.Location = new System.Drawing.Point(670, 9);
            this.lblServiceTotal2.Name = "lblServiceTotal2";
            this.lblServiceTotal2.Size = new System.Drawing.Size(120, 28);
            this.lblServiceTotal2.TabIndex = 2;
            this.lblServiceTotal2.Text = "Tiền dịch vụ:";
            // 
            // txtServiceTotal2
            // 
            this.txtServiceTotal2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtServiceTotal2.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.txtServiceTotal2.Location = new System.Drawing.Point(796, 7);
            this.txtServiceTotal2.Name = "txtServiceTotal2";
            this.txtServiceTotal2.ReadOnly = true;
            this.txtServiceTotal2.Size = new System.Drawing.Size(180, 38);
            this.txtServiceTotal2.TabIndex = 3;
            this.txtServiceTotal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSurcharge
            // 
            this.lblSurcharge.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSurcharge.AutoSize = true;
            this.lblSurcharge.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSurcharge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblSurcharge.Location = new System.Drawing.Point(182, 47);
            this.lblSurcharge.Name = "lblSurcharge";
            this.lblSurcharge.Size = new System.Drawing.Size(82, 28);
            this.lblSurcharge.TabIndex = 4;
            this.lblSurcharge.Text = "Phụ phí:";
            // 
            // txtSurcharge
            // 
            this.txtSurcharge.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSurcharge.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.txtSurcharge.Location = new System.Drawing.Point(270, 45);
            this.txtSurcharge.Name = "txtSurcharge";
            this.txtSurcharge.Size = new System.Drawing.Size(180, 38);
            this.txtSurcharge.TabIndex = 5;
            this.txtSurcharge.Text = "0";
            this.txtSurcharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDiscount
            // 
            this.lblDiscount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblDiscount.Location = new System.Drawing.Point(696, 47);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(94, 28);
            this.lblDiscount.TabIndex = 6;
            this.lblDiscount.Text = "Giảm giá:";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDiscount.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.txtDiscount.Location = new System.Drawing.Point(796, 45);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(180, 38);
            this.txtDiscount.TabIndex = 7;
            this.txtDiscount.Text = "0";
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPaymentMethod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblPaymentMethod.Location = new System.Drawing.Point(135, 85);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(129, 28);
            this.lblPaymentMethod.TabIndex = 8;
            this.lblPaymentMethod.Text = "Phương thức:";
            // 
            // cbxPaymentMethod
            // 
            this.cbxPaymentMethod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPaymentMethod.Location = new System.Drawing.Point(270, 83);
            this.cbxPaymentMethod.Name = "cbxPaymentMethod";
            this.cbxPaymentMethod.Size = new System.Drawing.Size(200, 33);
            this.cbxPaymentMethod.TabIndex = 9;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblSubtotal.Location = new System.Drawing.Point(698, 85);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(92, 28);
            this.lblSubtotal.TabIndex = 10;
            this.lblSubtotal.Text = "Tạm tính:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSubtotal.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            this.txtSubtotal.Location = new System.Drawing.Point(796, 83);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(180, 38);
            this.txtSubtotal.TabIndex = 11;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblIssuedBy
            // 
            this.lblIssuedBy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblIssuedBy.AutoSize = true;
            this.lblIssuedBy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblIssuedBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblIssuedBy.Location = new System.Drawing.Point(160, 123);
            this.lblIssuedBy.Name = "lblIssuedBy";
            this.lblIssuedBy.Size = new System.Drawing.Size(104, 28);
            this.lblIssuedBy.TabIndex = 12;
            this.lblIssuedBy.Text = "Nhân viên:";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmployeeName.Location = new System.Drawing.Point(270, 121);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.ReadOnly = true;
            this.txtEmployeeName.Size = new System.Drawing.Size(257, 33);
            this.txtEmployeeName.TabIndex = 13;
            // 
            // lblNote
            // 
            this.lblNote.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(111)))), ((int)(((byte)(123)))));
            this.lblNote.Location = new System.Drawing.Point(708, 123);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(82, 28);
            this.lblNote.TabIndex = 14;
            this.lblNote.Text = "Ghi chú:";
            // 
            // txtNote
            // 
            this.txtNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNote.Location = new System.Drawing.Point(796, 121);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(257, 33);
            this.txtNote.TabIndex = 15;
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.btnConfirm);
            this.buttonsPanel.Controls.Add(this.btnCancel);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonsPanel.Location = new System.Drawing.Point(3, 149);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.buttonsPanel.Size = new System.Drawing.Size(1060, 46);
            this.buttonsPanel.TabIndex = 1;
            // 
            // btnConfirm
            // 
            this.btnConfirm.AutoSize = true;
            this.btnConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.btnConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfirm.FlatAppearance.BorderSize = 0;
            this.btnConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirm.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnConfirm.ForeColor = System.Drawing.Color.White;
            this.btnConfirm.Location = new System.Drawing.Point(928, 6);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(132, 40);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "Thanh toán";
            this.btnConfirm.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(76)))), ((int)(((byte)(95)))));
            this.btnCancel.Location = new System.Drawing.Point(828, 6);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Đóng";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(0, 0);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(100, 26);
            this.txtTotalAmount.TabIndex = 0;
            this.txtTotalAmount.Visible = false;
            // 
            // frmCheckout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1120, 689);
            this.Controls.Add(this.mainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(27)))), ((int)(((byte)(34)))));
            this.MinimumSize = new System.Drawing.Size(980, 700);
            this.Name = "frmCheckout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thanh toán Booking";
            this.mainLayout.ResumeLayout(false);
            this.grpHeader.ResumeLayout(false);
            this.headerLayout.ResumeLayout(false);
            this.headerLayout.PerformLayout();
            this.middleLayout.ResumeLayout(false);
            this.grpServices.ResumeLayout(false);
            this.serviceLayout.ResumeLayout(false);
            this.serviceLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsedService)).EndInit();
            this.grpRooms.ResumeLayout(false);
            this.roomLayout.ResumeLayout(false);
            this.roomLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoom)).EndInit();
            this.grpSummary.ResumeLayout(false);
            this.summaryLayout.ResumeLayout(false);
            this.leftFormLayout.ResumeLayout(false);
            this.leftFormLayout.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.buttonsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Vẽ viền nhẹ cho GroupBox theo phong cách card – an toàn cho Designer.
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

        private TableLayoutPanel mainLayout;

        private GroupBox grpHeader;
        private TableLayoutPanel headerLayout;
        private Label lblCusName, lblIdNumber;
        private TextBox txtCusName, txtCusId;

        private TableLayoutPanel middleLayout;

        private GroupBox grpServices;
        private TableLayoutPanel serviceLayout;
        private DataGridView dgvUsedService;
        private Label lblServiceTotal;
        private TextBox txtServiceCharge;

        private GroupBox grpRooms;
        private TableLayoutPanel roomLayout;
        private DataGridView dgvRoom;
        private Label lblRoomTotal;
        private TextBox txtTongtien;

        private GroupBox grpSummary;
        private TableLayoutPanel summaryLayout;

        private TableLayoutPanel leftFormLayout;
        private Label lblRoomTotal2, lblServiceTotal2, lblSurcharge, lblDiscount, lblSubtotal, lblPaymentMethod, lblIssuedBy, lblNote;
        private TextBox txtRoomTotal2, txtServiceTotal2, txtSurcharge, txtDiscount, txtSubtotal, txtEmployeeName, txtNote, txtTotalAmount;
        private ComboBox cbxPaymentMethod;

        private FlowLayoutPanel buttonsPanel;
        private Button btnConfirm, btnCancel;
    }
}
