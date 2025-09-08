using System;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    partial class frmService
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
            this.panelButtons = new System.Windows.Forms.Panel();
            this.tableLayoutButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnAddService = new System.Windows.Forms.Button();
            this.btnEditService = new System.Windows.Forms.Button();
            this.btnDeleteService = new System.Windows.Forms.Button();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblNameService = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelUser = new System.Windows.Forms.Panel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutRight = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxDetailService = new System.Windows.Forms.GroupBox();
            this.tableLayoutServiceInfo = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxUpdateInvetory = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtQuantityAdd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveInventory = new System.Windows.Forms.Button();
            this.cboServiceName = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.txtSearchService = new System.Windows.Forms.TextBox();
            this.datagridViewService = new System.Windows.Forms.DataGridView();
            this.panelButtons.SuspendLayout();
            this.tableLayoutButtons.SuspendLayout();
            this.panelUser.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutRight.SuspendLayout();
            this.groupBoxDetailService.SuspendLayout();
            this.tableLayoutServiceInfo.SuspendLayout();
            this.groupBoxUpdateInvetory.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridViewService)).BeginInit();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.tableLayoutButtons);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(3, 431);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(338, 106);
            this.panelButtons.TabIndex = 1;
            // 
            // tableLayoutButtons
            // 
            this.tableLayoutButtons.ColumnCount = 2;
            this.tableLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtons.Controls.Add(this.btnAddService, 0, 0);
            this.tableLayoutButtons.Controls.Add(this.btnEditService, 1, 0);
            this.tableLayoutButtons.Controls.Add(this.btnDeleteService, 0, 1);
            this.tableLayoutButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutButtons.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutButtons.Name = "tableLayoutButtons";
            this.tableLayoutButtons.RowCount = 2;
            this.tableLayoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutButtons.Size = new System.Drawing.Size(338, 106);
            this.tableLayoutButtons.TabIndex = 1;
            // 
            // btnAddService
            // 
            this.btnAddService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnAddService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddService.ForeColor = System.Drawing.Color.White;
            this.btnAddService.Location = new System.Drawing.Point(3, 3);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(163, 47);
            this.btnAddService.TabIndex = 0;
            this.btnAddService.Text = "Thêm";
            this.btnAddService.UseVisualStyleBackColor = false;
            this.btnAddService.Click += new System.EventHandler(this.btnAddService_Click);
            // 
            // btnEditService
            // 
            this.btnEditService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnEditService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditService.ForeColor = System.Drawing.Color.White;
            this.btnEditService.Location = new System.Drawing.Point(172, 3);
            this.btnEditService.Name = "btnEditService";
            this.btnEditService.Size = new System.Drawing.Size(163, 47);
            this.btnEditService.TabIndex = 1;
            this.btnEditService.Text = "Sửa";
            this.btnEditService.UseVisualStyleBackColor = false;
            this.btnEditService.Click += new System.EventHandler(this.btnEditService_Click);
            // 
            // btnDeleteService
            // 
            this.btnDeleteService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnDeleteService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDeleteService.ForeColor = System.Drawing.Color.White;
            this.btnDeleteService.Location = new System.Drawing.Point(3, 56);
            this.btnDeleteService.Name = "btnDeleteService";
            this.btnDeleteService.Size = new System.Drawing.Size(163, 47);
            this.btnDeleteService.TabIndex = 2;
            this.btnDeleteService.Text = "Xóa";
            this.btnDeleteService.UseVisualStyleBackColor = false;
            this.btnDeleteService.Click += new System.EventHandler(this.btnDeleteService_Click);
            // 
            // lblPrice
            // 
            this.lblPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPrice.ForeColor = System.Drawing.Color.White;
            this.lblPrice.Location = new System.Drawing.Point(3, 63);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(108, 33);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Price: ";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrice
            // 
            this.txtPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrice.Location = new System.Drawing.Point(117, 66);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(212, 26);
            this.txtPrice.TabIndex = 3;
            // 
            // lblNameService
            // 
            this.lblNameService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNameService.ForeColor = System.Drawing.Color.White;
            this.lblNameService.Location = new System.Drawing.Point(3, 0);
            this.lblNameService.Name = "lblNameService";
            this.lblNameService.Size = new System.Drawing.Size(108, 29);
            this.lblNameService.TabIndex = 11;
            this.lblNameService.Text = "Service Name:";
            this.lblNameService.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServiceName
            // 
            this.txtServiceName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServiceName.Location = new System.Drawing.Point(117, 3);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(212, 26);
            this.txtServiceName.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(3, 96);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(108, 36);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status:";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panelUser
            // 
            this.panelUser.Controls.Add(this.tableLayoutPanelMain);
            this.panelUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUser.Location = new System.Drawing.Point(0, 0);
            this.panelUser.Name = "panelUser";
            this.panelUser.Size = new System.Drawing.Size(1048, 546);
            this.panelUser.TabIndex = 1;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutRight, 1, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1048, 546);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutRight
            // 
            this.tableLayoutRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.tableLayoutRight.ColumnCount = 1;
            this.tableLayoutRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRight.Controls.Add(this.panelButtons, 0, 2);
            this.tableLayoutRight.Controls.Add(this.groupBoxDetailService, 0, 1);
            this.tableLayoutRight.Controls.Add(this.groupBoxUpdateInvetory, 0, 0);
            this.tableLayoutRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRight.Location = new System.Drawing.Point(701, 3);
            this.tableLayoutRight.Name = "tableLayoutRight";
            this.tableLayoutRight.RowCount = 3;
            this.tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.85542F));
            this.tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.14458F));
            this.tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutRight.Size = new System.Drawing.Size(344, 540);
            this.tableLayoutRight.TabIndex = 1;
            // 
            // groupBoxDetailService
            // 
            this.groupBoxDetailService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.groupBoxDetailService.Controls.Add(this.tableLayoutServiceInfo);
            this.groupBoxDetailService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDetailService.ForeColor = System.Drawing.Color.White;
            this.groupBoxDetailService.Location = new System.Drawing.Point(3, 169);
            this.groupBoxDetailService.Name = "groupBoxDetailService";
            this.groupBoxDetailService.Size = new System.Drawing.Size(338, 256);
            this.groupBoxDetailService.TabIndex = 0;
            this.groupBoxDetailService.TabStop = false;
            this.groupBoxDetailService.Text = "Thông tin dịch vụ";
            // 
            // tableLayoutServiceInfo
            // 
            this.tableLayoutServiceInfo.ColumnCount = 2;
            this.tableLayoutServiceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.53815F));
            this.tableLayoutServiceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.46185F));
            this.tableLayoutServiceInfo.Controls.Add(this.btnSave, 1, 4);
            this.tableLayoutServiceInfo.Controls.Add(this.lblStatus, 0, 3);
            this.tableLayoutServiceInfo.Controls.Add(this.lblPrice, 0, 2);
            this.tableLayoutServiceInfo.Controls.Add(this.txtPrice, 1, 2);
            this.tableLayoutServiceInfo.Controls.Add(this.chkIsActive, 1, 3);
            this.tableLayoutServiceInfo.Controls.Add(this.txtQuantity, 1, 1);
            this.tableLayoutServiceInfo.Controls.Add(this.txtServiceName, 1, 0);
            this.tableLayoutServiceInfo.Controls.Add(this.lblNameService, 0, 0);
            this.tableLayoutServiceInfo.Controls.Add(this.lblQuantity, 0, 1);
            this.tableLayoutServiceInfo.Controls.Add(this.btnCancel, 0, 4);
            this.tableLayoutServiceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutServiceInfo.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutServiceInfo.Name = "tableLayoutServiceInfo";
            this.tableLayoutServiceInfo.RowCount = 5;
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutServiceInfo.Size = new System.Drawing.Size(332, 231);
            this.tableLayoutServiceInfo.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(186, 135);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 34);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkIsActive.Location = new System.Drawing.Point(117, 99);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(212, 30);
            this.chkIsActive.TabIndex = 12;
            this.chkIsActive.Text = "IsActive";
            this.chkIsActive.UseVisualStyleBackColor = true;
            this.chkIsActive.CheckedChanged += new System.EventHandler(this.chkIsActive_CheckedChanged);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuantity.Location = new System.Drawing.Point(117, 32);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(212, 26);
            this.txtQuantity.TabIndex = 16;
            // 
            // lblQuantity
            // 
            this.lblQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQuantity.ForeColor = System.Drawing.Color.White;
            this.lblQuantity.Location = new System.Drawing.Point(3, 29);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(108, 34);
            this.lblQuantity.TabIndex = 15;
            this.lblQuantity.Text = "Số Lượng";
            this.lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(27, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(59, 34);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBoxUpdateInvetory
            // 
            this.groupBoxUpdateInvetory.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxUpdateInvetory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxUpdateInvetory.ForeColor = System.Drawing.Color.White;
            this.groupBoxUpdateInvetory.Location = new System.Drawing.Point(3, 3);
            this.groupBoxUpdateInvetory.Name = "groupBoxUpdateInvetory";
            this.groupBoxUpdateInvetory.Size = new System.Drawing.Size(338, 160);
            this.groupBoxUpdateInvetory.TabIndex = 2;
            this.groupBoxUpdateInvetory.TabStop = false;
            this.groupBoxUpdateInvetory.Text = "Cập Nhật  Kho";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.38153F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.61847F));
            this.tableLayoutPanel2.Controls.Add(this.txtQuantityAdd, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSaveInventory, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.cboServiceName, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(45, 43);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(246, 98);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // txtQuantityAdd
            // 
            this.txtQuantityAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuantityAdd.Location = new System.Drawing.Point(114, 33);
            this.txtQuantityAdd.Name = "txtQuantityAdd";
            this.txtQuantityAdd.Size = new System.Drawing.Size(129, 26);
            this.txtQuantityAdd.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dịch vụ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "SL Nhập:";
            // 
            // btnSaveInventory
            // 
            this.btnSaveInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnSaveInventory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveInventory.ForeColor = System.Drawing.Color.White;
            this.btnSaveInventory.Location = new System.Drawing.Point(114, 63);
            this.btnSaveInventory.Name = "btnSaveInventory";
            this.btnSaveInventory.Size = new System.Drawing.Size(129, 32);
            this.btnSaveInventory.TabIndex = 15;
            this.btnSaveInventory.Text = "Lưu";
            this.btnSaveInventory.UseVisualStyleBackColor = false;
            this.btnSaveInventory.Click += new System.EventHandler(this.btnSaveInventory_Click_1);
            // 
            // cboServiceName
            // 
            this.cboServiceName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboServiceName.FormattingEnabled = true;
            this.cboServiceName.Location = new System.Drawing.Point(114, 3);
            this.cboServiceName.Name = "cboServiceName";
            this.cboServiceName.Size = new System.Drawing.Size(129, 28);
            this.cboServiceName.TabIndex = 16;
            this.cboServiceName.SelectedIndexChanged += new System.EventHandler(this.cboServiceName_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBoxSearch, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.datagridViewService, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.31532F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.68468F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(692, 540);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.groupBoxSearch.Controls.Add(this.txtSearchService);
            this.groupBoxSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSearch.ForeColor = System.Drawing.Color.White;
            this.groupBoxSearch.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxSearch.Size = new System.Drawing.Size(686, 76);
            this.groupBoxSearch.TabIndex = 2;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "Tìm Kiếm";
            // 
            // txtSearchService
            // 
            this.txtSearchService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchService.Location = new System.Drawing.Point(13, 22);
            this.txtSearchService.Name = "txtSearchService";
            this.txtSearchService.Size = new System.Drawing.Size(400, 26);
            this.txtSearchService.TabIndex = 0;
            this.txtSearchService.TextChanged += new System.EventHandler(this.txtSearchService_TextChanged);
            // 
            // datagridViewService
            // 
            this.datagridViewService.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.datagridViewService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridViewService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagridViewService.Location = new System.Drawing.Point(3, 85);
            this.datagridViewService.Name = "datagridViewService";
            this.datagridViewService.ReadOnly = true;
            this.datagridViewService.RowHeadersWidth = 62;
            this.datagridViewService.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridViewService.Size = new System.Drawing.Size(686, 452);
            this.datagridViewService.TabIndex = 0;
            this.datagridViewService.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridViewService_CellContentClick);
            // 
            // frmService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(26)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(1048, 546);
            this.Controls.Add(this.panelUser);
            this.Name = "frmService";
            this.Text = "Quản Lý Dịch Vụ";
            this.Load += new System.EventHandler(this.frmService_Load);
            this.panelButtons.ResumeLayout(false);
            this.tableLayoutButtons.ResumeLayout(false);
            this.panelUser.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutRight.ResumeLayout(false);
            this.groupBoxDetailService.ResumeLayout(false);
            this.tableLayoutServiceInfo.ResumeLayout(false);
            this.tableLayoutServiceInfo.PerformLayout();
            this.groupBoxUpdateInvetory.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridViewService)).EndInit();
            this.ResumeLayout(false);

        }

        private void datagridViewService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        #endregion
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblNameService;
        private System.Windows.Forms.TextBox txtServiceName;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.DataGridView datagridViewService;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRight;
        private System.Windows.Forms.GroupBox groupBoxDetailService;
        private System.Windows.Forms.TableLayoutPanel tableLayoutServiceInfo;
        private System.Windows.Forms.GroupBox groupBoxSearch;
        private System.Windows.Forms.TextBox txtSearchService;
        private System.Windows.Forms.TableLayoutPanel tableLayoutButtons;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Button btnEditService;
        private System.Windows.Forms.Button btnDeleteService;
        private System.Windows.Forms.CheckBox chkIsActive;
        private Button btnCancel;
        private Button btnSave;
        private GroupBox groupBoxUpdateInvetory;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Label label2;
        private TextBox txtQuantityAdd;
        private Button btnSaveInventory;
        private ComboBox cboServiceName;
        private TextBox txtQuantity;
        private Label lblQuantity;
    }
}