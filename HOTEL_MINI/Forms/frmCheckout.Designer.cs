// frmCheckout1.Designer.cs
using System.Windows.Forms;
using System.Drawing;

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
            this.lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.lblIssuedBy = new System.Windows.Forms.Label();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.lblSurcharge = new System.Windows.Forms.Label();
            this.txtSurcharge = new System.Windows.Forms.TextBox();
            this.cbxPaymentMethod = new System.Windows.Forms.ComboBox();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
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
            this.mainLayout.Padding = new System.Windows.Forms.Padding(12);
            this.mainLayout.RowCount = 3;
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 237F));
            this.mainLayout.Size = new System.Drawing.Size(1120, 689);
            this.mainLayout.TabIndex = 0;
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.headerLayout);
            this.grpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHeader.Location = new System.Drawing.Point(15, 15);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1090, 59);
            this.grpHeader.TabIndex = 0;
            this.grpHeader.TabStop = false;
            this.grpHeader.Text = "Thông tin khách / thời gian";
            // 
            // headerLayout
            // 
            this.headerLayout.ColumnCount = 6;
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.headerLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.headerLayout.Controls.Add(this.lblCusName, 0, 0);
            this.headerLayout.Controls.Add(this.txtCusName, 1, 0);
            this.headerLayout.Controls.Add(this.lblIdNumber, 2, 0);
            this.headerLayout.Controls.Add(this.txtCusId, 3, 0);
            this.headerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerLayout.Location = new System.Drawing.Point(3, 27);
            this.headerLayout.Name = "headerLayout";
            this.headerLayout.RowCount = 1;
            this.headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.headerLayout.Size = new System.Drawing.Size(1084, 29);
            this.headerLayout.TabIndex = 0;
            // 
            // lblCusName
            // 
            this.lblCusName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCusName.AutoSize = true;
            this.lblCusName.Location = new System.Drawing.Point(43, 0);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(64, 29);
            this.lblCusName.TabIndex = 0;
            this.lblCusName.Text = "Khách hàng:";
            // 
            // txtCusName
            // 
            this.txtCusName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCusName.Location = new System.Drawing.Point(113, 3);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(315, 31);
            this.txtCusName.TabIndex = 1;
            // 
            // lblIdNumber
            // 
            this.lblIdNumber.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblIdNumber.AutoSize = true;
            this.lblIdNumber.Location = new System.Drawing.Point(446, 2);
            this.lblIdNumber.Name = "lblIdNumber";
            this.lblIdNumber.Size = new System.Drawing.Size(62, 25);
            this.lblIdNumber.TabIndex = 2;
            this.lblIdNumber.Text = "CCCD:";
            // 
            // txtCusId
            // 
            this.txtCusId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCusId.Location = new System.Drawing.Point(514, 3);
            this.txtCusId.Name = "txtCusId";
            this.txtCusId.Size = new System.Drawing.Size(154, 31);
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
            this.middleLayout.Location = new System.Drawing.Point(15, 80);
            this.middleLayout.Name = "middleLayout";
            this.middleLayout.RowCount = 1;
            this.middleLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.middleLayout.Size = new System.Drawing.Size(1090, 357);
            this.middleLayout.TabIndex = 1;
            // 
            // grpServices
            // 
            this.grpServices.Controls.Add(this.serviceLayout);
            this.grpServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpServices.Location = new System.Drawing.Point(3, 3);
            this.grpServices.Name = "grpServices";
            this.grpServices.Size = new System.Drawing.Size(539, 351);
            this.grpServices.TabIndex = 0;
            this.grpServices.TabStop = false;
            this.grpServices.Text = "Dịch vụ đã dùng";
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
            this.serviceLayout.Location = new System.Drawing.Point(3, 27);
            this.serviceLayout.Name = "serviceLayout";
            this.serviceLayout.RowCount = 2;
            this.serviceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.serviceLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.serviceLayout.Size = new System.Drawing.Size(533, 321);
            this.serviceLayout.TabIndex = 0;
            // 
            // dgvUsedService
            // 
            this.dgvUsedService.AllowUserToAddRows = false;
            this.dgvUsedService.AllowUserToDeleteRows = false;
            this.dgvUsedService.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsedService.ColumnHeadersHeight = 34;
            this.serviceLayout.SetColumnSpan(this.dgvUsedService, 2);
            this.dgvUsedService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUsedService.Location = new System.Drawing.Point(3, 3);
            this.dgvUsedService.Name = "dgvUsedService";
            this.dgvUsedService.ReadOnly = true;
            this.dgvUsedService.RowHeadersVisible = false;
            this.dgvUsedService.RowHeadersWidth = 62;
            this.dgvUsedService.Size = new System.Drawing.Size(527, 283);
            this.dgvUsedService.TabIndex = 0;
            // 
            // lblServiceTotal
            // 
            this.lblServiceTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblServiceTotal.AutoSize = true;
            this.lblServiceTotal.Location = new System.Drawing.Point(284, 292);
            this.lblServiceTotal.Name = "lblServiceTotal";
            this.lblServiceTotal.Size = new System.Drawing.Size(86, 25);
            this.lblServiceTotal.TabIndex = 1;
            this.lblServiceTotal.Text = "Tổng DV:";
            // 
            // txtServiceCharge
            // 
            this.txtServiceCharge.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtServiceCharge.Location = new System.Drawing.Point(376, 292);
            this.txtServiceCharge.Name = "txtServiceCharge";
            this.txtServiceCharge.ReadOnly = true;
            this.txtServiceCharge.Size = new System.Drawing.Size(100, 31);
            this.txtServiceCharge.TabIndex = 2;
            this.txtServiceCharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpRooms
            // 
            this.grpRooms.Controls.Add(this.roomLayout);
            this.grpRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRooms.Location = new System.Drawing.Point(548, 3);
            this.grpRooms.Name = "grpRooms";
            this.grpRooms.Size = new System.Drawing.Size(539, 351);
            this.grpRooms.TabIndex = 1;
            this.grpRooms.TabStop = false;
            this.grpRooms.Text = "Phòng muốn thanh toán";
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
            this.roomLayout.Location = new System.Drawing.Point(3, 27);
            this.roomLayout.Name = "roomLayout";
            this.roomLayout.RowCount = 2;
            this.roomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.roomLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.roomLayout.Size = new System.Drawing.Size(533, 321);
            this.roomLayout.TabIndex = 0;
            // 
            // dgvRoom
            // 
            this.dgvRoom.AllowUserToAddRows = false;
            this.dgvRoom.AllowUserToDeleteRows = false;
            this.dgvRoom.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoom.ColumnHeadersHeight = 34;
            this.roomLayout.SetColumnSpan(this.dgvRoom, 2);
            this.dgvRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoom.Location = new System.Drawing.Point(3, 3);
            this.dgvRoom.Name = "dgvRoom";
            this.dgvRoom.ReadOnly = true;
            this.dgvRoom.RowHeadersVisible = false;
            this.dgvRoom.RowHeadersWidth = 62;
            this.dgvRoom.Size = new System.Drawing.Size(527, 283);
            this.dgvRoom.TabIndex = 0;
            // 
            // lblRoomTotal
            // 
            this.lblRoomTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRoomTotal.AutoSize = true;
            this.lblRoomTotal.Location = new System.Drawing.Point(264, 292);
            this.lblRoomTotal.Name = "lblRoomTotal";
            this.lblRoomTotal.Size = new System.Drawing.Size(106, 25);
            this.lblRoomTotal.TabIndex = 1;
            this.lblRoomTotal.Text = "Tiền phòng:";
            // 
            // txtTongtien
            // 
            this.txtTongtien.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTongtien.Location = new System.Drawing.Point(376, 292);
            this.txtTongtien.Name = "txtTongtien";
            this.txtTongtien.ReadOnly = true;
            this.txtTongtien.Size = new System.Drawing.Size(100, 31);
            this.txtTongtien.TabIndex = 2;
            this.txtTongtien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpSummary
            // 
            this.grpSummary.Controls.Add(this.summaryLayout);
            this.grpSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSummary.Location = new System.Drawing.Point(15, 443);
            this.grpSummary.Name = "grpSummary";
            this.grpSummary.Size = new System.Drawing.Size(1090, 231);
            this.grpSummary.TabIndex = 2;
            this.grpSummary.TabStop = false;
            this.grpSummary.Text = "Tổng kết thanh toán";
            // 
            // summaryLayout
            // 
            this.summaryLayout.ColumnCount = 1;
            this.summaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.summaryLayout.Controls.Add(this.leftFormLayout, 0, 0);
            this.summaryLayout.Controls.Add(this.buttonsPanel, 0, 1);
            this.summaryLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryLayout.Location = new System.Drawing.Point(3, 27);
            this.summaryLayout.Name = "summaryLayout";
            this.summaryLayout.RowCount = 2;
            this.summaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.summaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.summaryLayout.Size = new System.Drawing.Size(1084, 201);
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
            this.leftFormLayout.Controls.Add(this.lblDiscount, 2, 1);
            this.leftFormLayout.Controls.Add(this.txtDiscount, 3, 1);
            this.leftFormLayout.Controls.Add(this.txtNote, 3, 3);
            this.leftFormLayout.Controls.Add(this.lblIssuedBy, 0, 3);
            this.leftFormLayout.Controls.Add(this.txtEmployeeName, 1, 3);
            this.leftFormLayout.Controls.Add(this.lblSurcharge, 0, 1);
            this.leftFormLayout.Controls.Add(this.txtSurcharge, 1, 1);
            this.leftFormLayout.Controls.Add(this.cbxPaymentMethod, 1, 2);
            this.leftFormLayout.Controls.Add(this.txtSubtotal, 3, 2);
            this.leftFormLayout.Controls.Add(this.lblPaymentMethod, 0, 2);
            this.leftFormLayout.Controls.Add(this.lblSubtotal, 2, 2);
            this.leftFormLayout.Controls.Add(this.lblNote, 2, 3);
            this.leftFormLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftFormLayout.Location = new System.Drawing.Point(3, 3);
            this.leftFormLayout.Name = "leftFormLayout";
            this.leftFormLayout.RowCount = 4;
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.leftFormLayout.Size = new System.Drawing.Size(1078, 151);
            this.leftFormLayout.TabIndex = 0;
            // 
            // lblRoomTotal2
            // 
            this.lblRoomTotal2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRoomTotal2.AutoSize = true;
            this.lblRoomTotal2.Location = new System.Drawing.Point(160, 5);
            this.lblRoomTotal2.Name = "lblRoomTotal2";
            this.lblRoomTotal2.Size = new System.Drawing.Size(106, 25);
            this.lblRoomTotal2.TabIndex = 0;
            this.lblRoomTotal2.Text = "Tiền phòng:";
            // 
            // txtRoomTotal2
            // 
            this.txtRoomTotal2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtRoomTotal2.Location = new System.Drawing.Point(272, 3);
            this.txtRoomTotal2.Name = "txtRoomTotal2";
            this.txtRoomTotal2.ReadOnly = true;
            this.txtRoomTotal2.Size = new System.Drawing.Size(100, 31);
            this.txtRoomTotal2.TabIndex = 1;
            this.txtRoomTotal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblServiceTotal2
            // 
            this.lblServiceTotal2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblServiceTotal2.AutoSize = true;
            this.lblServiceTotal2.Location = new System.Drawing.Point(694, 5);
            this.lblServiceTotal2.Name = "lblServiceTotal2";
            this.lblServiceTotal2.Size = new System.Drawing.Size(110, 25);
            this.lblServiceTotal2.TabIndex = 2;
            this.lblServiceTotal2.Text = "Tiền dịch vụ:";
            // 
            // txtServiceTotal2
            // 
            this.txtServiceTotal2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtServiceTotal2.Location = new System.Drawing.Point(810, 3);
            this.txtServiceTotal2.Name = "txtServiceTotal2";
            this.txtServiceTotal2.ReadOnly = true;
            this.txtServiceTotal2.Size = new System.Drawing.Size(100, 31);
            this.txtServiceTotal2.TabIndex = 3;
            this.txtServiceTotal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDiscount
            // 
            this.lblDiscount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(718, 41);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(86, 25);
            this.lblDiscount.TabIndex = 6;
            this.lblDiscount.Text = "Giảm giá:";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDiscount.Location = new System.Drawing.Point(810, 39);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(100, 31);
            this.txtDiscount.TabIndex = 7;
            this.txtDiscount.Text = "0";
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtNote
            // 
            this.txtNote.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNote.Location = new System.Drawing.Point(810, 114);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(100, 31);
            this.txtNote.TabIndex = 23;
            // 
            // lblIssuedBy
            // 
            this.lblIssuedBy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblIssuedBy.AutoSize = true;
            this.lblIssuedBy.Location = new System.Drawing.Point(171, 117);
            this.lblIssuedBy.Name = "lblIssuedBy";
            this.lblIssuedBy.Size = new System.Drawing.Size(95, 25);
            this.lblIssuedBy.TabIndex = 20;
            this.lblIssuedBy.Text = "Nhân viên:";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtEmployeeName.Location = new System.Drawing.Point(272, 114);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.ReadOnly = true;
            this.txtEmployeeName.Size = new System.Drawing.Size(100, 31);
            this.txtEmployeeName.TabIndex = 21;
            // 
            // lblSurcharge
            // 
            this.lblSurcharge.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSurcharge.AutoSize = true;
            this.lblSurcharge.Location = new System.Drawing.Point(190, 41);
            this.lblSurcharge.Name = "lblSurcharge";
            this.lblSurcharge.Size = new System.Drawing.Size(76, 25);
            this.lblSurcharge.TabIndex = 4;
            this.lblSurcharge.Text = "Phụ phí:";
            // 
            // txtSurcharge
            // 
            this.txtSurcharge.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSurcharge.Location = new System.Drawing.Point(272, 39);
            this.txtSurcharge.Name = "txtSurcharge";
            this.txtSurcharge.Size = new System.Drawing.Size(100, 31);
            this.txtSurcharge.TabIndex = 5;
            this.txtSurcharge.Text = "0";
            this.txtSurcharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbxPaymentMethod
            // 
            this.cbxPaymentMethod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPaymentMethod.Location = new System.Drawing.Point(272, 76);
            this.cbxPaymentMethod.Name = "cbxPaymentMethod";
            this.cbxPaymentMethod.Size = new System.Drawing.Size(121, 33);
            this.cbxPaymentMethod.TabIndex = 19;
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSubtotal.Location = new System.Drawing.Point(810, 75);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(100, 31);
            this.txtSubtotal.TabIndex = 9;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(147, 77);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(119, 25);
            this.lblPaymentMethod.TabIndex = 18;
            this.lblPaymentMethod.Text = "Phương thức:";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(719, 77);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(85, 25);
            this.lblSubtotal.TabIndex = 8;
            this.lblSubtotal.Text = "Tạm tính:";
            // 
            // lblNote
            // 
            this.lblNote.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(729, 117);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(75, 25);
            this.lblNote.TabIndex = 22;
            this.lblNote.Text = "Ghi chú:";
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.Controls.Add(this.btnConfirm);
            this.buttonsPanel.Controls.Add(this.btnCancel);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonsPanel.Location = new System.Drawing.Point(3, 160);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(1078, 38);
            this.buttonsPanel.TabIndex = 25;
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnConfirm.AutoSize = true;
            this.btnConfirm.Location = new System.Drawing.Point(964, 3);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(111, 35);
            this.btnConfirm.TabIndex = 0;
            this.btnConfirm.Text = "Thanh toán";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(883, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 35);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Đóng";
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(0, 0);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.ReadOnly = true;
            this.txtTotalAmount.Size = new System.Drawing.Size(100, 26);
            this.txtTotalAmount.TabIndex = 0;
            this.txtTotalAmount.Visible = false;
            // 
            // frmCheckout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 689);
            this.Controls.Add(this.mainLayout);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
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

        // NEW
        private TableLayoutPanel leftFormLayout;

        private Label lblRoomTotal2, lblServiceTotal2, lblSurcharge, lblDiscount, lblSubtotal, lblPaymentMethod, lblIssuedBy, lblNote;
        private TextBox txtRoomTotal2, txtServiceTotal2, txtSurcharge, txtDiscount, txtSubtotal, txtEmployeeName, txtNote, txtTotalAmount;
        private ComboBox cbxPaymentMethod;

        private FlowLayoutPanel buttonsPanel;
        private Button btnConfirm, btnCancel;
    }
}
