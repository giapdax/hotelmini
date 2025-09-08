namespace HOTEL_MINI.Forms
{
    partial class frmRoomManager
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
            this.tabRoom = new System.Windows.Forms.TabPage();
            this.tplRoom = new System.Windows.Forms.TableLayoutPanel();
            this.tblRoom1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvRoom = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grbSearch = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.cbmRoomStatusSearch = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboRoomTypeNameSearch = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tblRoom2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grbIPriceInformation = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRoomNumber = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lblWeeklyPrice = new System.Windows.Forms.Label();
            this.txtWeeklyPrice = new System.Windows.Forms.TextBox();
            this.lblDayPrice = new System.Windows.Forms.Label();
            this.txtDayPrice = new System.Windows.Forms.TextBox();
            this.lblNightlyPrice = new System.Windows.Forms.Label();
            this.txtNightlyPrice = new System.Windows.Forms.TextBox();
            this.lblHourlyPrice = new System.Windows.Forms.Label();
            this.txtHourlyPrice = new System.Windows.Forms.TextBox();
            this.grbRoomInformation = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRoomNumber1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboRoomStatus = new System.Windows.Forms.ComboBox();
            this.lblRoomTypeName = new System.Windows.Forms.Label();
            this.cboRoomTypeName = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControlRoom = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.lblRoomPrice = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.grpRoomType = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSaveRoomType = new System.Windows.Forms.Button();
            this.btnCancelRoomType = new System.Windows.Forms.Button();
            this.btnEditRoomType = new System.Windows.Forms.Button();
            this.btnAddRoomType = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tblTenLoaiPhong = new System.Windows.Forms.Label();
            this.txtRoomTypeName = new System.Windows.Forms.TextBox();
            this.cboRoomType = new System.Windows.Forms.ComboBox();
            this.cboPricingType = new System.Windows.Forms.ComboBox();
            this.tabRoom.SuspendLayout();
            this.tplRoom.SuspendLayout();
            this.tblRoom1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoom)).BeginInit();
            this.panel1.SuspendLayout();
            this.grbSearch.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tblRoom2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.grbIPriceInformation.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.grbRoomInformation.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabControlRoom.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.grpRoomType.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabRoom
            // 
            this.tabRoom.BackColor = System.Drawing.Color.Thistle;
            this.tabRoom.Controls.Add(this.tplRoom);
            this.tabRoom.Location = new System.Drawing.Point(4, 29);
            this.tabRoom.Name = "tabRoom";
            this.tabRoom.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoom.Size = new System.Drawing.Size(1040, 513);
            this.tabRoom.TabIndex = 0;
            this.tabRoom.Text = "Quản Lý Phòng";
            // 
            // tplRoom
            // 
            this.tplRoom.ColumnCount = 2;
            this.tplRoom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.37717F));
            this.tplRoom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.62283F));
            this.tplRoom.Controls.Add(this.tblRoom1, 0, 0);
            this.tplRoom.Controls.Add(this.tblRoom2, 1, 0);
            this.tplRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tplRoom.Location = new System.Drawing.Point(3, 3);
            this.tplRoom.Name = "tplRoom";
            this.tplRoom.RowCount = 1;
            this.tplRoom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tplRoom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tplRoom.Size = new System.Drawing.Size(1034, 507);
            this.tplRoom.TabIndex = 0;
            // 
            // tblRoom1
            // 
            this.tblRoom1.ColumnCount = 1;
            this.tblRoom1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRoom1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRoom1.Controls.Add(this.dgvRoom, 0, 1);
            this.tblRoom1.Controls.Add(this.panel1, 0, 0);
            this.tblRoom1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRoom1.Location = new System.Drawing.Point(3, 3);
            this.tblRoom1.Name = "tblRoom1";
            this.tblRoom1.RowCount = 2;
            this.tblRoom1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.37525F));
            this.tblRoom1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.62475F));
            this.tblRoom1.Size = new System.Drawing.Size(669, 501);
            this.tblRoom1.TabIndex = 0;
            // 
            // dgvRoom
            // 
            this.dgvRoom.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRoom.Location = new System.Drawing.Point(3, 65);
            this.dgvRoom.Name = "dgvRoom";
            this.dgvRoom.RowHeadersWidth = 62;
            this.dgvRoom.RowTemplate.Height = 28;
            this.dgvRoom.Size = new System.Drawing.Size(663, 433);
            this.dgvRoom.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.grbSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(663, 56);
            this.panel1.TabIndex = 1;
            // 
            // grbSearch
            // 
            this.grbSearch.Controls.Add(this.tableLayoutPanel5);
            this.grbSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbSearch.Location = new System.Drawing.Point(0, 0);
            this.grbSearch.Name = "grbSearch";
            this.grbSearch.Size = new System.Drawing.Size(663, 56);
            this.grbSearch.TabIndex = 0;
            this.grbSearch.TabStop = false;
            this.grbSearch.Text = "Tìm kiếm";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 5;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tableLayoutPanel5.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbmRoomStatusSearch, 4, 0);
            this.tableLayoutPanel5.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.cboRoomTypeNameSearch, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtSearch, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(657, 31);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Firebrick;
            this.label3.Location = new System.Drawing.Point(428, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 31);
            this.label3.TabIndex = 6;
            this.label3.Text = "Trạng Thái:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbmRoomStatusSearch
            // 
            this.cbmRoomStatusSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbmRoomStatusSearch.FormattingEnabled = true;
            this.cbmRoomStatusSearch.Location = new System.Drawing.Point(542, 3);
            this.cbmRoomStatusSearch.Name = "cbmRoomStatusSearch";
            this.cbmRoomStatusSearch.Size = new System.Drawing.Size(112, 28);
            this.cbmRoomStatusSearch.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Firebrick;
            this.label2.Location = new System.Drawing.Point(200, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "Loại Phòng:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboRoomTypeNameSearch
            // 
            this.cboRoomTypeNameSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboRoomTypeNameSearch.FormattingEnabled = true;
            this.cboRoomTypeNameSearch.Location = new System.Drawing.Point(314, 3);
            this.cboRoomTypeNameSearch.Name = "cboRoomTypeNameSearch";
            this.cboRoomTypeNameSearch.Size = new System.Drawing.Size(108, 28);
            this.cboRoomTypeNameSearch.TabIndex = 5;
            // 
            // txtSearch
            // 
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Location = new System.Drawing.Point(3, 3);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(191, 26);
            this.txtSearch.TabIndex = 0;
            // 
            // tblRoom2
            // 
            this.tblRoom2.ColumnCount = 1;
            this.tblRoom2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRoom2.Controls.Add(this.tableLayoutPanel6, 0, 2);
            this.tblRoom2.Controls.Add(this.grbIPriceInformation, 0, 0);
            this.tblRoom2.Controls.Add(this.grbRoomInformation, 0, 1);
            this.tblRoom2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblRoom2.Location = new System.Drawing.Point(678, 3);
            this.tblRoom2.Name = "tblRoom2";
            this.tblRoom2.RowCount = 3;
            this.tblRoom2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 42.11577F));
            this.tblRoom2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.70459F));
            this.tblRoom2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblRoom2.Size = new System.Drawing.Size(353, 501);
            this.tblRoom2.TabIndex = 1;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.btnEdit, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnAdd, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 453);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(347, 45);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(176, 3);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(168, 39);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(3, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(167, 39);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // grbIPriceInformation
            // 
            this.grbIPriceInformation.Controls.Add(this.tableLayoutPanel9);
            this.grbIPriceInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbIPriceInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbIPriceInformation.Location = new System.Drawing.Point(3, 3);
            this.grbIPriceInformation.Name = "grbIPriceInformation";
            this.grbIPriceInformation.Size = new System.Drawing.Size(347, 205);
            this.grbIPriceInformation.TabIndex = 1;
            this.grbIPriceInformation.TabStop = false;
            this.grbIPriceInformation.Text = "Thông tin giá";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 1;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.lblRoomNumber, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel7, 0, 1);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 2;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(341, 180);
            this.tableLayoutPanel9.TabIndex = 2;
            // 
            // lblRoomNumber
            // 
            this.lblRoomNumber.AutoSize = true;
            this.lblRoomNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNumber.ForeColor = System.Drawing.Color.White;
            this.lblRoomNumber.Location = new System.Drawing.Point(3, 0);
            this.lblRoomNumber.Name = "lblRoomNumber";
            this.lblRoomNumber.Size = new System.Drawing.Size(335, 36);
            this.lblRoomNumber.TabIndex = 0;
            this.lblRoomNumber.Text = "100";
            this.lblRoomNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.lblWeeklyPrice, 0, 3);
            this.tableLayoutPanel7.Controls.Add(this.txtWeeklyPrice, 1, 3);
            this.tableLayoutPanel7.Controls.Add(this.lblDayPrice, 0, 2);
            this.tableLayoutPanel7.Controls.Add(this.txtDayPrice, 1, 2);
            this.tableLayoutPanel7.Controls.Add(this.lblNightlyPrice, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.txtNightlyPrice, 1, 1);
            this.tableLayoutPanel7.Controls.Add(this.lblHourlyPrice, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.txtHourlyPrice, 1, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 39);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 4;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(335, 138);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // lblWeeklyPrice
            // 
            this.lblWeeklyPrice.AutoSize = true;
            this.lblWeeklyPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWeeklyPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeeklyPrice.ForeColor = System.Drawing.Color.Firebrick;
            this.lblWeeklyPrice.Location = new System.Drawing.Point(3, 102);
            this.lblWeeklyPrice.Name = "lblWeeklyPrice";
            this.lblWeeklyPrice.Size = new System.Drawing.Size(161, 36);
            this.lblWeeklyPrice.TabIndex = 6;
            this.lblWeeklyPrice.Text = "Thuê theo tuần:";
            this.lblWeeklyPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtWeeklyPrice
            // 
            this.txtWeeklyPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWeeklyPrice.Location = new System.Drawing.Point(170, 105);
            this.txtWeeklyPrice.Name = "txtWeeklyPrice";
            this.txtWeeklyPrice.ReadOnly = true;
            this.txtWeeklyPrice.Size = new System.Drawing.Size(162, 26);
            this.txtWeeklyPrice.TabIndex = 7;
            // 
            // lblDayPrice
            // 
            this.lblDayPrice.AutoSize = true;
            this.lblDayPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDayPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDayPrice.ForeColor = System.Drawing.Color.Firebrick;
            this.lblDayPrice.Location = new System.Drawing.Point(3, 68);
            this.lblDayPrice.Name = "lblDayPrice";
            this.lblDayPrice.Size = new System.Drawing.Size(161, 34);
            this.lblDayPrice.TabIndex = 4;
            this.lblDayPrice.Text = "Thuê theo ngày";
            this.lblDayPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDayPrice
            // 
            this.txtDayPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDayPrice.Location = new System.Drawing.Point(170, 71);
            this.txtDayPrice.Name = "txtDayPrice";
            this.txtDayPrice.ReadOnly = true;
            this.txtDayPrice.Size = new System.Drawing.Size(162, 26);
            this.txtDayPrice.TabIndex = 5;
            // 
            // lblNightlyPrice
            // 
            this.lblNightlyPrice.AutoSize = true;
            this.lblNightlyPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNightlyPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNightlyPrice.ForeColor = System.Drawing.Color.Firebrick;
            this.lblNightlyPrice.Location = new System.Drawing.Point(3, 34);
            this.lblNightlyPrice.Name = "lblNightlyPrice";
            this.lblNightlyPrice.Size = new System.Drawing.Size(161, 34);
            this.lblNightlyPrice.TabIndex = 2;
            this.lblNightlyPrice.Text = "Thuê theo đêm";
            this.lblNightlyPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNightlyPrice
            // 
            this.txtNightlyPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNightlyPrice.Location = new System.Drawing.Point(170, 37);
            this.txtNightlyPrice.Name = "txtNightlyPrice";
            this.txtNightlyPrice.ReadOnly = true;
            this.txtNightlyPrice.Size = new System.Drawing.Size(162, 26);
            this.txtNightlyPrice.TabIndex = 3;
            // 
            // lblHourlyPrice
            // 
            this.lblHourlyPrice.AutoSize = true;
            this.lblHourlyPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHourlyPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHourlyPrice.ForeColor = System.Drawing.Color.Firebrick;
            this.lblHourlyPrice.Location = new System.Drawing.Point(3, 0);
            this.lblHourlyPrice.Name = "lblHourlyPrice";
            this.lblHourlyPrice.Size = new System.Drawing.Size(161, 34);
            this.lblHourlyPrice.TabIndex = 0;
            this.lblHourlyPrice.Text = "Thuê theo giờ:";
            this.lblHourlyPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtHourlyPrice
            // 
            this.txtHourlyPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHourlyPrice.Location = new System.Drawing.Point(170, 3);
            this.txtHourlyPrice.Name = "txtHourlyPrice";
            this.txtHourlyPrice.Size = new System.Drawing.Size(162, 26);
            this.txtHourlyPrice.TabIndex = 1;
            // 
            // grbRoomInformation
            // 
            this.grbRoomInformation.Controls.Add(this.tableLayoutPanel3);
            this.grbRoomInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grbRoomInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbRoomInformation.Location = new System.Drawing.Point(3, 214);
            this.grbRoomInformation.Name = "grbRoomInformation";
            this.grbRoomInformation.Size = new System.Drawing.Size(347, 233);
            this.grbRoomInformation.TabIndex = 2;
            this.grbRoomInformation.TabStop = false;
            this.grbRoomInformation.Text = "Thông tin phòng";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblRoomNumber1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel8, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(341, 208);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // lblRoomNumber1
            // 
            this.lblRoomNumber1.AutoSize = true;
            this.lblRoomNumber1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoomNumber1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomNumber1.ForeColor = System.Drawing.Color.White;
            this.lblRoomNumber1.Location = new System.Drawing.Point(3, 0);
            this.lblRoomNumber1.Name = "lblRoomNumber1";
            this.lblRoomNumber1.Size = new System.Drawing.Size(335, 31);
            this.lblRoomNumber1.TabIndex = 1;
            this.lblRoomNumber1.Text = "100";
            this.lblRoomNumber1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 2;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.Controls.Add(this.lblNote, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.txtNote, 1, 2);
            this.tableLayoutPanel8.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.cboRoomStatus, 1, 1);
            this.tableLayoutPanel8.Controls.Add(this.lblRoomTypeName, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.cboRoomTypeName, 1, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 34);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(335, 129);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.ForeColor = System.Drawing.Color.Firebrick;
            this.lblNote.Location = new System.Drawing.Point(3, 84);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(161, 45);
            this.lblNote.TabIndex = 8;
            this.lblNote.Text = "Ghi chú";
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNote
            // 
            this.txtNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNote.Location = new System.Drawing.Point(170, 87);
            this.txtNote.Name = "txtNote";
            this.txtNote.ReadOnly = true;
            this.txtNote.Size = new System.Drawing.Size(162, 26);
            this.txtNote.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(3, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 42);
            this.label1.TabIndex = 4;
            this.label1.Text = "Trạng Thái:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboRoomStatus
            // 
            this.cboRoomStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboRoomStatus.FormattingEnabled = true;
            this.cboRoomStatus.Location = new System.Drawing.Point(170, 45);
            this.cboRoomStatus.Name = "cboRoomStatus";
            this.cboRoomStatus.Size = new System.Drawing.Size(162, 28);
            this.cboRoomStatus.TabIndex = 5;
            // 
            // lblRoomTypeName
            // 
            this.lblRoomTypeName.AutoSize = true;
            this.lblRoomTypeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoomTypeName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomTypeName.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRoomTypeName.Location = new System.Drawing.Point(3, 0);
            this.lblRoomTypeName.Name = "lblRoomTypeName";
            this.lblRoomTypeName.Size = new System.Drawing.Size(161, 42);
            this.lblRoomTypeName.TabIndex = 2;
            this.lblRoomTypeName.Text = "Loại Phòng:";
            this.lblRoomTypeName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboRoomTypeName
            // 
            this.cboRoomTypeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboRoomTypeName.FormattingEnabled = true;
            this.cboRoomTypeName.Location = new System.Drawing.Point(170, 3);
            this.cboRoomTypeName.Name = "cboRoomTypeName";
            this.cboRoomTypeName.Size = new System.Drawing.Size(162, 28);
            this.cboRoomTypeName.TabIndex = 3;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnCancel, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 169);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(335, 36);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(170, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(162, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(161, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // tabControlRoom
            // 
            this.tabControlRoom.Controls.Add(this.tabRoom);
            this.tabControlRoom.Controls.Add(this.tabPage1);
            this.tabControlRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlRoom.Location = new System.Drawing.Point(0, 0);
            this.tabControlRoom.Name = "tabControlRoom";
            this.tabControlRoom.SelectedIndex = 0;
            this.tabControlRoom.Size = new System.Drawing.Size(1048, 546);
            this.tabControlRoom.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1040, 513);
            this.tabPage1.TabIndex = 1;
            this.tabPage1.Text = "Quản lý loại phòng và giá";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.NavajoWhite;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.54352F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.45648F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel10, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1034, 507);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.98004F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.01996F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(589, 501);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 1;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.grpRoomType, 0, 1);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(598, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 2;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(433, 501);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel11);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(427, 294);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin loại phòng";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.cboPricingType, 1, 1);
            this.tableLayoutPanel11.Controls.Add(this.button5, 0, 4);
            this.tableLayoutPanel11.Controls.Add(this.button6, 1, 4);
            this.tableLayoutPanel11.Controls.Add(this.button7, 0, 5);
            this.tableLayoutPanel11.Controls.Add(this.button8, 1, 5);
            this.tableLayoutPanel11.Controls.Add(this.label7, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.textBox6, 1, 2);
            this.tableLayoutPanel11.Controls.Add(this.checkBox1, 1, 3);
            this.tableLayoutPanel11.Controls.Add(this.lblRoomPrice, 0, 2);
            this.tableLayoutPanel11.Controls.Add(this.label8, 0, 3);
            this.tableLayoutPanel11.Controls.Add(this.cboRoomType, 1, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 6;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(421, 269);
            this.tableLayoutPanel11.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.button5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(3, 179);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(204, 38);
            this.button5.TabIndex = 13;
            this.button5.Text = "Lưu";
            this.button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.button6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(213, 179);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(205, 38);
            this.button6.TabIndex = 11;
            this.button6.Text = "Hủy";
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.button7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(3, 223);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(204, 43);
            this.button7.TabIndex = 12;
            this.button7.Text = "Sửa";
            this.button7.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.button8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(213, 223);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(205, 43);
            this.button8.TabIndex = 10;
            this.button8.Text = "Thêm";
            this.button8.UseVisualStyleBackColor = false;
            // 
            // lblRoomPrice
            // 
            this.lblRoomPrice.AutoSize = true;
            this.lblRoomPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRoomPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoomPrice.ForeColor = System.Drawing.Color.Firebrick;
            this.lblRoomPrice.Location = new System.Drawing.Point(3, 88);
            this.lblRoomPrice.Name = "lblRoomPrice";
            this.lblRoomPrice.Size = new System.Drawing.Size(204, 44);
            this.lblRoomPrice.TabIndex = 8;
            this.lblRoomPrice.Text = "Giá cả:";
            this.lblRoomPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox6
            // 
            this.textBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox6.Location = new System.Drawing.Point(213, 91);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(205, 26);
            this.textBox6.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Firebrick;
            this.label8.Location = new System.Drawing.Point(3, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(204, 44);
            this.label8.TabIndex = 6;
            this.label8.Text = "Trạng thái";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Firebrick;
            this.label7.Location = new System.Drawing.Point(3, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(204, 44);
            this.label7.TabIndex = 4;
            this.label7.Text = "Loại giá:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Firebrick;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 44);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tên loại phòng:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBox1.Location = new System.Drawing.Point(213, 135);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(205, 38);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Đang hoạt động";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // grpRoomType
            // 
            this.grpRoomType.Controls.Add(this.tableLayoutPanel12);
            this.grpRoomType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpRoomType.Location = new System.Drawing.Point(3, 303);
            this.grpRoomType.Name = "grpRoomType";
            this.grpRoomType.Size = new System.Drawing.Size(427, 195);
            this.grpRoomType.TabIndex = 1;
            this.grpRoomType.TabStop = false;
            this.grpRoomType.Text = "Thông tin loại phòng";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 2;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel12.Controls.Add(this.btnSaveRoomType, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.btnCancelRoomType, 1, 2);
            this.tableLayoutPanel12.Controls.Add(this.btnEditRoomType, 0, 3);
            this.tableLayoutPanel12.Controls.Add(this.btnAddRoomType, 1, 3);
            this.tableLayoutPanel12.Controls.Add(this.lblDescription, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.txtDescription, 1, 1);
            this.tableLayoutPanel12.Controls.Add(this.tblTenLoaiPhong, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.txtRoomTypeName, 1, 0);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 4;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(421, 170);
            this.tableLayoutPanel12.TabIndex = 0;
            // 
            // btnSaveRoomType
            // 
            this.btnSaveRoomType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnSaveRoomType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSaveRoomType.ForeColor = System.Drawing.Color.White;
            this.btnSaveRoomType.Location = new System.Drawing.Point(3, 87);
            this.btnSaveRoomType.Name = "btnSaveRoomType";
            this.btnSaveRoomType.Size = new System.Drawing.Size(204, 36);
            this.btnSaveRoomType.TabIndex = 11;
            this.btnSaveRoomType.Text = "Lưu";
            this.btnSaveRoomType.UseVisualStyleBackColor = false;
            // 
            // btnCancelRoomType
            // 
            this.btnCancelRoomType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnCancelRoomType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancelRoomType.ForeColor = System.Drawing.Color.White;
            this.btnCancelRoomType.Location = new System.Drawing.Point(213, 87);
            this.btnCancelRoomType.Name = "btnCancelRoomType";
            this.btnCancelRoomType.Size = new System.Drawing.Size(205, 36);
            this.btnCancelRoomType.TabIndex = 9;
            this.btnCancelRoomType.Text = "Hủy";
            this.btnCancelRoomType.UseVisualStyleBackColor = false;
            // 
            // btnEditRoomType
            // 
            this.btnEditRoomType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnEditRoomType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEditRoomType.ForeColor = System.Drawing.Color.White;
            this.btnEditRoomType.Location = new System.Drawing.Point(3, 129);
            this.btnEditRoomType.Name = "btnEditRoomType";
            this.btnEditRoomType.Size = new System.Drawing.Size(204, 38);
            this.btnEditRoomType.TabIndex = 10;
            this.btnEditRoomType.Text = "Sửa";
            this.btnEditRoomType.UseVisualStyleBackColor = false;
            // 
            // btnAddRoomType
            // 
            this.btnAddRoomType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnAddRoomType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddRoomType.ForeColor = System.Drawing.Color.White;
            this.btnAddRoomType.Location = new System.Drawing.Point(213, 129);
            this.btnAddRoomType.Name = "btnAddRoomType";
            this.btnAddRoomType.Size = new System.Drawing.Size(205, 38);
            this.btnAddRoomType.TabIndex = 8;
            this.btnAddRoomType.Text = "Thêm";
            this.btnAddRoomType.UseVisualStyleBackColor = false;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.Firebrick;
            this.lblDescription.Location = new System.Drawing.Point(3, 42);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(204, 42);
            this.lblDescription.TabIndex = 6;
            this.lblDescription.Text = "Mô tả:";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(213, 45);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(205, 26);
            this.txtDescription.TabIndex = 7;
            // 
            // tblTenLoaiPhong
            // 
            this.tblTenLoaiPhong.AutoSize = true;
            this.tblTenLoaiPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblTenLoaiPhong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblTenLoaiPhong.ForeColor = System.Drawing.Color.Firebrick;
            this.tblTenLoaiPhong.Location = new System.Drawing.Point(3, 0);
            this.tblTenLoaiPhong.Name = "tblTenLoaiPhong";
            this.tblTenLoaiPhong.Size = new System.Drawing.Size(204, 42);
            this.tblTenLoaiPhong.TabIndex = 4;
            this.tblTenLoaiPhong.Text = "Tên loại phòng:";
            this.tblTenLoaiPhong.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRoomTypeName
            // 
            this.txtRoomTypeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRoomTypeName.Location = new System.Drawing.Point(213, 3);
            this.txtRoomTypeName.Name = "txtRoomTypeName";
            this.txtRoomTypeName.Size = new System.Drawing.Size(205, 26);
            this.txtRoomTypeName.TabIndex = 5;
            // 
            // cboRoomType
            // 
            this.cboRoomType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboRoomType.FormattingEnabled = true;
            this.cboRoomType.Location = new System.Drawing.Point(213, 3);
            this.cboRoomType.Name = "cboRoomType";
            this.cboRoomType.Size = new System.Drawing.Size(205, 28);
            this.cboRoomType.TabIndex = 15;
            // 
            // cboPricingType
            // 
            this.cboPricingType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPricingType.FormattingEnabled = true;
            this.cboPricingType.Location = new System.Drawing.Point(213, 47);
            this.cboPricingType.Name = "cboPricingType";
            this.cboPricingType.Size = new System.Drawing.Size(205, 28);
            this.cboPricingType.TabIndex = 16;
            // 
            // frmRoomManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1048, 546);
            this.Controls.Add(this.tabControlRoom);
            this.Name = "frmRoomManager";
            this.Text = "Quản Lý Phòng";
            this.tabRoom.ResumeLayout(false);
            this.tplRoom.ResumeLayout(false);
            this.tblRoom1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoom)).EndInit();
            this.panel1.ResumeLayout(false);
            this.grbSearch.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tblRoom2.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.grbIPriceInformation.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.grbRoomInformation.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tabControlRoom.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.grpRoomType.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabRoom;
        private System.Windows.Forms.TableLayoutPanel tplRoom;
        private System.Windows.Forms.TableLayoutPanel tblRoom1;
        private System.Windows.Forms.DataGridView dgvRoom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox grbSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbmRoomStatusSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboRoomTypeNameSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TableLayoutPanel tblRoom2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grbIPriceInformation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label lblRoomNumber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Label lblWeeklyPrice;
        private System.Windows.Forms.TextBox txtWeeklyPrice;
        private System.Windows.Forms.Label lblDayPrice;
        private System.Windows.Forms.TextBox txtDayPrice;
        private System.Windows.Forms.Label lblNightlyPrice;
        private System.Windows.Forms.TextBox txtNightlyPrice;
        private System.Windows.Forms.Label lblHourlyPrice;
        private System.Windows.Forms.TextBox txtHourlyPrice;
        private System.Windows.Forms.GroupBox grbRoomInformation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblRoomNumber1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboRoomStatus;
        private System.Windows.Forms.Label lblRoomTypeName;
        private System.Windows.Forms.ComboBox cboRoomTypeName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControlRoom;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox grpRoomType;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.Label lblRoomPrice;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label tblTenLoaiPhong;
        private System.Windows.Forms.TextBox txtRoomTypeName;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button btnSaveRoomType;
        private System.Windows.Forms.Button btnCancelRoomType;
        private System.Windows.Forms.Button btnEditRoomType;
        private System.Windows.Forms.Button btnAddRoomType;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox cboPricingType;
        private System.Windows.Forms.ComboBox cboRoomType;
    }
}