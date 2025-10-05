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
            this.lblCheckin = new System.Windows.Forms.Label();
            this.txtCheckin = new System.Windows.Forms.TextBox();
            this.lblCheckout = new System.Windows.Forms.Label();
            this.txtCheckout = new System.Windows.Forms.TextBox();
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
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblDaTra = new System.Windows.Forms.Label();
            this.txtDaTra = new System.Windows.Forms.TextBox();
            this.lblConLai = new System.Windows.Forms.Label();
            this.txtConLai = new System.Windows.Forms.TextBox();
            this.lblPayOption = new System.Windows.Forms.Label();
            this.cboPayOption = new System.Windows.Forms.ComboBox();
            this.lblPayNow = new System.Windows.Forms.Label();
            this.txtPayNow = new System.Windows.Forms.TextBox();
            this.lblPaymentMethod = new System.Windows.Forms.Label();
            this.cbxPaymentMethod = new System.Windows.Forms.ComboBox();
            this.lblIssuedBy = new System.Windows.Forms.Label();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.grpPayments = new System.Windows.Forms.GroupBox();
            this.dgvPayments = new System.Windows.Forms.DataGridView();
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
            this.grpPayments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).BeginInit();
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
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 107F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 302F));
            this.mainLayout.Size = new System.Drawing.Size(1120, 689);
            this.mainLayout.TabIndex = 0;
            // 
            // grpHeader
            // 
            this.grpHeader.Controls.Add(this.headerLayout);
            this.grpHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpHeader.Location = new System.Drawing.Point(15, 15);
            this.grpHeader.Name = "grpHeader";
            this.grpHeader.Size = new System.Drawing.Size(1090, 101);
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
            this.headerLayout.Controls.Add(this.lblCheckin, 4, 0);
            this.headerLayout.Controls.Add(this.txtCheckin, 5, 0);
            this.headerLayout.Controls.Add(this.lblCheckout, 4, 1);
            this.headerLayout.Controls.Add(this.txtCheckout, 5, 1);
            this.headerLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerLayout.Location = new System.Drawing.Point(3, 27);
            this.headerLayout.Name = "headerLayout";
            this.headerLayout.RowCount = 2;
            this.headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.headerLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.headerLayout.Size = new System.Drawing.Size(1084, 71);
            this.headerLayout.TabIndex = 0;
            // 
            // lblCusName
            // 
            this.lblCusName.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCusName.AutoSize = true;
            this.lblCusName.Location = new System.Drawing.Point(43, 0);
            this.lblCusName.Name = "lblCusName";
            this.lblCusName.Size = new System.Drawing.Size(64, 35);
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
            this.lblIdNumber.Location = new System.Drawing.Point(446, 5);
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
            // lblCheckin
            // 
            this.lblCheckin.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCheckin.AutoSize = true;
            this.lblCheckin.Location = new System.Drawing.Point(674, 5);
            this.lblCheckin.Name = "lblCheckin";
            this.lblCheckin.Size = new System.Drawing.Size(84, 25);
            this.lblCheckin.TabIndex = 4;
            this.lblCheckin.Text = "Check-in:";
            // 
            // txtCheckin
            // 
            this.txtCheckin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheckin.Location = new System.Drawing.Point(764, 3);
            this.txtCheckin.Name = "txtCheckin";
            this.txtCheckin.Size = new System.Drawing.Size(317, 31);
            this.txtCheckin.TabIndex = 5;
            // 
            // lblCheckout
            // 
            this.lblCheckout.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblCheckout.AutoSize = true;
            this.lblCheckout.Location = new System.Drawing.Point(681, 35);
            this.lblCheckout.Name = "lblCheckout";
            this.lblCheckout.Size = new System.Drawing.Size(77, 36);
            this.lblCheckout.TabIndex = 6;
            this.lblCheckout.Text = "Check-out:";
            // 
            // txtCheckout
            // 
            this.txtCheckout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCheckout.Location = new System.Drawing.Point(764, 38);
            this.txtCheckout.Name = "txtCheckout";
            this.txtCheckout.Size = new System.Drawing.Size(317, 31);
            this.txtCheckout.TabIndex = 7;
            // 
            // middleLayout
            // 
            this.middleLayout.ColumnCount = 2;
            this.middleLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.middleLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.middleLayout.Controls.Add(this.grpServices, 0, 0);
            this.middleLayout.Controls.Add(this.grpRooms, 1, 0);
            this.middleLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.middleLayout.Location = new System.Drawing.Point(15, 122);
            this.middleLayout.Name = "middleLayout";
            this.middleLayout.RowCount = 1;
            this.middleLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.middleLayout.Size = new System.Drawing.Size(1090, 250);
            this.middleLayout.TabIndex = 1;
            // 
            // grpServices
            // 
            this.grpServices.Controls.Add(this.serviceLayout);
            this.grpServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpServices.Location = new System.Drawing.Point(3, 3);
            this.grpServices.Name = "grpServices";
            this.grpServices.Size = new System.Drawing.Size(539, 244);
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
            this.serviceLayout.Size = new System.Drawing.Size(533, 214);
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
            this.dgvUsedService.Size = new System.Drawing.Size(527, 176);
            this.dgvUsedService.TabIndex = 0;
            // 
            // lblServiceTotal
            // 
            this.lblServiceTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblServiceTotal.AutoSize = true;
            this.lblServiceTotal.Location = new System.Drawing.Point(284, 185);
            this.lblServiceTotal.Name = "lblServiceTotal";
            this.lblServiceTotal.Size = new System.Drawing.Size(86, 25);
            this.lblServiceTotal.TabIndex = 1;
            this.lblServiceTotal.Text = "Tổng DV:";
            // 
            // txtServiceCharge
            // 
            this.txtServiceCharge.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtServiceCharge.Location = new System.Drawing.Point(376, 185);
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
            this.grpRooms.Size = new System.Drawing.Size(539, 244);
            this.grpRooms.TabIndex = 1;
            this.grpRooms.TabStop = false;
            this.grpRooms.Text = "Phòng trong booking";
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
            this.roomLayout.Size = new System.Drawing.Size(533, 214);
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
            this.dgvRoom.Size = new System.Drawing.Size(527, 176);
            this.dgvRoom.TabIndex = 0;
            // 
            // lblRoomTotal
            // 
            this.lblRoomTotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRoomTotal.AutoSize = true;
            this.lblRoomTotal.Location = new System.Drawing.Point(264, 185);
            this.lblRoomTotal.Name = "lblRoomTotal";
            this.lblRoomTotal.Size = new System.Drawing.Size(106, 25);
            this.lblRoomTotal.TabIndex = 1;
            this.lblRoomTotal.Text = "Tiền phòng:";
            // 
            // txtTongtien
            // 
            this.txtTongtien.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTongtien.Location = new System.Drawing.Point(376, 185);
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
            this.grpSummary.Location = new System.Drawing.Point(15, 378);
            this.grpSummary.Name = "grpSummary";
            this.grpSummary.Size = new System.Drawing.Size(1090, 296);
            this.grpSummary.TabIndex = 2;
            this.grpSummary.TabStop = false;
            this.grpSummary.Text = "Tổng kết thanh toán";
            // 
            // summaryLayout
            // 
            this.summaryLayout.ColumnCount = 2;
            this.summaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.summaryLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.summaryLayout.Controls.Add(this.leftFormLayout, 0, 0);
            this.summaryLayout.Controls.Add(this.grpPayments, 1, 0);
            this.summaryLayout.Controls.Add(this.buttonsPanel, 0, 1);
            this.summaryLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.summaryLayout.Location = new System.Drawing.Point(3, 27);
            this.summaryLayout.Name = "summaryLayout";
            this.summaryLayout.RowCount = 2;
            this.summaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.summaryLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.summaryLayout.Size = new System.Drawing.Size(1084, 266);
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
            this.leftFormLayout.Controls.Add(this.lblSubtotal, 0, 2);
            this.leftFormLayout.Controls.Add(this.txtSubtotal, 1, 2);
            this.leftFormLayout.Controls.Add(this.lblDaTra, 2, 2);
            this.leftFormLayout.Controls.Add(this.txtDaTra, 3, 2);
            this.leftFormLayout.Controls.Add(this.lblConLai, 0, 3);
            this.leftFormLayout.Controls.Add(this.txtConLai, 1, 3);
            this.leftFormLayout.Controls.Add(this.lblPayOption, 2, 3);
            this.leftFormLayout.Controls.Add(this.cboPayOption, 3, 3);
            this.leftFormLayout.Controls.Add(this.lblPayNow, 0, 4);
            this.leftFormLayout.Controls.Add(this.txtPayNow, 1, 4);
            this.leftFormLayout.Controls.Add(this.lblPaymentMethod, 2, 4);
            this.leftFormLayout.Controls.Add(this.cbxPaymentMethod, 3, 4);
            this.leftFormLayout.Controls.Add(this.lblIssuedBy, 0, 5);
            this.leftFormLayout.Controls.Add(this.txtEmployeeName, 1, 5);
            this.leftFormLayout.Controls.Add(this.lblNote, 2, 5);
            this.leftFormLayout.Controls.Add(this.txtNote, 3, 5);
            this.leftFormLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftFormLayout.Location = new System.Drawing.Point(3, 3);
            this.leftFormLayout.Name = "leftFormLayout";
            this.leftFormLayout.RowCount = 6;
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.leftFormLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.leftFormLayout.Size = new System.Drawing.Size(698, 216);
            this.leftFormLayout.TabIndex = 0;
            // 
            // lblRoomTotal2
            // 
            this.lblRoomTotal2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblRoomTotal2.AutoSize = true;
            this.lblRoomTotal2.Location = new System.Drawing.Point(65, 5);
            this.lblRoomTotal2.Name = "lblRoomTotal2";
            this.lblRoomTotal2.Size = new System.Drawing.Size(106, 25);
            this.lblRoomTotal2.TabIndex = 0;
            this.lblRoomTotal2.Text = "Tiền phòng:";
            // 
            // txtRoomTotal2
            // 
            this.txtRoomTotal2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtRoomTotal2.Location = new System.Drawing.Point(177, 3);
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
            this.lblServiceTotal2.Location = new System.Drawing.Point(409, 5);
            this.lblServiceTotal2.Name = "lblServiceTotal2";
            this.lblServiceTotal2.Size = new System.Drawing.Size(110, 25);
            this.lblServiceTotal2.TabIndex = 2;
            this.lblServiceTotal2.Text = "Tiền dịch vụ:";
            // 
            // txtServiceTotal2
            // 
            this.txtServiceTotal2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtServiceTotal2.Location = new System.Drawing.Point(525, 3);
            this.txtServiceTotal2.Name = "txtServiceTotal2";
            this.txtServiceTotal2.ReadOnly = true;
            this.txtServiceTotal2.Size = new System.Drawing.Size(100, 31);
            this.txtServiceTotal2.TabIndex = 3;
            this.txtServiceTotal2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSurcharge
            // 
            this.lblSurcharge.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSurcharge.AutoSize = true;
            this.lblSurcharge.Location = new System.Drawing.Point(95, 41);
            this.lblSurcharge.Name = "lblSurcharge";
            this.lblSurcharge.Size = new System.Drawing.Size(76, 25);
            this.lblSurcharge.TabIndex = 4;
            this.lblSurcharge.Text = "Phụ phí:";
            // 
            // txtSurcharge
            // 
            this.txtSurcharge.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSurcharge.Location = new System.Drawing.Point(177, 39);
            this.txtSurcharge.Name = "txtSurcharge";
            this.txtSurcharge.Size = new System.Drawing.Size(100, 31);
            this.txtSurcharge.TabIndex = 5;
            this.txtSurcharge.Text = "0";
            this.txtSurcharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDiscount
            // 
            this.lblDiscount.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(433, 41);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(86, 25);
            this.lblDiscount.TabIndex = 6;
            this.lblDiscount.Text = "Giảm giá:";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDiscount.Location = new System.Drawing.Point(525, 39);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(100, 31);
            this.txtDiscount.TabIndex = 7;
            this.txtDiscount.Text = "0";
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(86, 77);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(85, 25);
            this.lblSubtotal.TabIndex = 8;
            this.lblSubtotal.Text = "Tạm tính:";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtSubtotal.Location = new System.Drawing.Point(177, 75);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.ReadOnly = true;
            this.txtSubtotal.Size = new System.Drawing.Size(100, 31);
            this.txtSubtotal.TabIndex = 9;
            this.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDaTra
            // 
            this.lblDaTra.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDaTra.AutoSize = true;
            this.lblDaTra.Location = new System.Drawing.Point(455, 77);
            this.lblDaTra.Name = "lblDaTra";
            this.lblDaTra.Size = new System.Drawing.Size(64, 25);
            this.lblDaTra.TabIndex = 10;
            this.lblDaTra.Text = "Đã trả:";
            // 
            // txtDaTra
            // 
            this.txtDaTra.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtDaTra.Location = new System.Drawing.Point(525, 75);
            this.txtDaTra.Name = "txtDaTra";
            this.txtDaTra.ReadOnly = true;
            this.txtDaTra.Size = new System.Drawing.Size(100, 31);
            this.txtDaTra.TabIndex = 11;
            this.txtDaTra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblConLai
            // 
            this.lblConLai.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblConLai.AutoSize = true;
            this.lblConLai.Location = new System.Drawing.Point(101, 113);
            this.lblConLai.Name = "lblConLai";
            this.lblConLai.Size = new System.Drawing.Size(70, 25);
            this.lblConLai.TabIndex = 12;
            this.lblConLai.Text = "Còn lại:";
            // 
            // txtConLai
            // 
            this.txtConLai.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtConLai.Location = new System.Drawing.Point(177, 111);
            this.txtConLai.Name = "txtConLai";
            this.txtConLai.ReadOnly = true;
            this.txtConLai.Size = new System.Drawing.Size(100, 31);
            this.txtConLai.TabIndex = 13;
            this.txtConLai.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPayOption
            // 
            this.lblPayOption.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPayOption.AutoSize = true;
            this.lblPayOption.Location = new System.Drawing.Point(431, 113);
            this.lblPayOption.Name = "lblPayOption";
            this.lblPayOption.Size = new System.Drawing.Size(88, 25);
            this.lblPayOption.TabIndex = 14;
            this.lblPayOption.Text = "Lựa chọn:";
            // 
            // cboPayOption
            // 
            this.cboPayOption.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboPayOption.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPayOption.Location = new System.Drawing.Point(525, 112);
            this.cboPayOption.Name = "cboPayOption";
            this.cboPayOption.Size = new System.Drawing.Size(121, 33);
            this.cboPayOption.TabIndex = 15;
            // 
            // lblPayNow
            // 
            this.lblPayNow.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPayNow.AutoSize = true;
            this.lblPayNow.Location = new System.Drawing.Point(72, 149);
            this.lblPayNow.Name = "lblPayNow";
            this.lblPayNow.Size = new System.Drawing.Size(99, 25);
            this.lblPayNow.TabIndex = 16;
            this.lblPayNow.Text = "Trả lần này:";
            // 
            // txtPayNow
            // 
            this.txtPayNow.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtPayNow.Location = new System.Drawing.Point(177, 147);
            this.txtPayNow.Name = "txtPayNow";
            this.txtPayNow.Size = new System.Drawing.Size(100, 31);
            this.txtPayNow.TabIndex = 17;
            this.txtPayNow.Text = "0";
            this.txtPayNow.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPaymentMethod
            // 
            this.lblPaymentMethod.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblPaymentMethod.AutoSize = true;
            this.lblPaymentMethod.Location = new System.Drawing.Point(400, 149);
            this.lblPaymentMethod.Name = "lblPaymentMethod";
            this.lblPaymentMethod.Size = new System.Drawing.Size(119, 25);
            this.lblPaymentMethod.TabIndex = 18;
            this.lblPaymentMethod.Text = "Phương thức:";
            // 
            // cbxPaymentMethod
            // 
            this.cbxPaymentMethod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbxPaymentMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPaymentMethod.Location = new System.Drawing.Point(525, 148);
            this.cbxPaymentMethod.Name = "cbxPaymentMethod";
            this.cbxPaymentMethod.Size = new System.Drawing.Size(121, 33);
            this.cbxPaymentMethod.TabIndex = 19;
            // 
            // lblIssuedBy
            // 
            this.lblIssuedBy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblIssuedBy.AutoSize = true;
            this.lblIssuedBy.Location = new System.Drawing.Point(76, 188);
            this.lblIssuedBy.Name = "lblIssuedBy";
            this.lblIssuedBy.Size = new System.Drawing.Size(95, 25);
            this.lblIssuedBy.TabIndex = 20;
            this.lblIssuedBy.Text = "Nhân viên:";
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtEmployeeName.Location = new System.Drawing.Point(177, 185);
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.ReadOnly = true;
            this.txtEmployeeName.Size = new System.Drawing.Size(100, 31);
            this.txtEmployeeName.TabIndex = 21;
            // 
            // lblNote
            // 
            this.lblNote.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(444, 188);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(75, 25);
            this.lblNote.TabIndex = 22;
            this.lblNote.Text = "Ghi chú:";
            // 
            // txtNote
            // 
            this.txtNote.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtNote.Location = new System.Drawing.Point(525, 185);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(100, 31);
            this.txtNote.TabIndex = 23;
            // 
            // grpPayments
            // 
            this.grpPayments.Controls.Add(this.dgvPayments);
            this.grpPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPayments.Location = new System.Drawing.Point(707, 3);
            this.grpPayments.Name = "grpPayments";
            this.grpPayments.Size = new System.Drawing.Size(374, 216);
            this.grpPayments.TabIndex = 24;
            this.grpPayments.TabStop = false;
            this.grpPayments.Text = "Lịch sử thanh toán";
            // 
            // dgvPayments
            // 
            this.dgvPayments.AllowUserToAddRows = false;
            this.dgvPayments.AllowUserToDeleteRows = false;
            this.dgvPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPayments.ColumnHeadersHeight = 34;
            this.dgvPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPayments.Location = new System.Drawing.Point(3, 27);
            this.dgvPayments.Name = "dgvPayments";
            this.dgvPayments.ReadOnly = true;
            this.dgvPayments.RowHeadersVisible = false;
            this.dgvPayments.RowHeadersWidth = 62;
            this.dgvPayments.Size = new System.Drawing.Size(368, 186);
            this.dgvPayments.TabIndex = 0;
            // 
            // buttonsPanel
            // 
            this.summaryLayout.SetColumnSpan(this.buttonsPanel, 2);
            this.buttonsPanel.Controls.Add(this.btnConfirm);
            this.buttonsPanel.Controls.Add(this.btnCancel);
            this.buttonsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonsPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.buttonsPanel.Location = new System.Drawing.Point(3, 225);
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
            this.grpPayments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayments)).EndInit();
            this.buttonsPanel.ResumeLayout(false);
            this.buttonsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel mainLayout;

        private GroupBox grpHeader;
        private TableLayoutPanel headerLayout;
        private Label lblCusName, lblIdNumber, lblCheckin, lblCheckout;
        private TextBox txtCusName, txtCusId, txtCheckin, txtCheckout;

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

        private Label lblRoomTotal2, lblServiceTotal2, lblSurcharge, lblDiscount, lblSubtotal, lblDaTra, lblConLai, lblPayOption, lblPayNow, lblPaymentMethod, lblIssuedBy, lblNote;
        private TextBox txtRoomTotal2, txtServiceTotal2, txtSurcharge, txtDiscount, txtSubtotal, txtDaTra, txtConLai, txtPayNow, txtEmployeeName, txtNote, txtTotalAmount;
        private ComboBox cboPayOption, cbxPaymentMethod;

        private GroupBox grpPayments;
        private DataGridView dgvPayments;

        private FlowLayoutPanel buttonsPanel;
        private Button btnConfirm, btnCancel;
    }
}
