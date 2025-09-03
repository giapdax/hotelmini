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
            this.btnClearService = new System.Windows.Forms.Button();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblNameService = new System.Windows.Forms.Label();
            this.txtServiceName = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelUser = new System.Windows.Forms.Panel();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.datagridViewService = new System.Windows.Forms.DataGridView();
            this.tableLayoutRight = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSearch = new System.Windows.Forms.GroupBox();
            this.txtSearchService = new System.Windows.Forms.TextBox();
            this.groupBoxDetailService = new System.Windows.Forms.GroupBox();
            this.tableLayoutServiceInfo = new System.Windows.Forms.TableLayoutPanel();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.panelButtons.SuspendLayout();
            this.tableLayoutButtons.SuspendLayout();
            this.panelUser.SuspendLayout();
            this.tableLayoutPanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.datagridViewService)).BeginInit();
            this.tableLayoutRight.SuspendLayout();
            this.groupBoxSearch.SuspendLayout();
            this.groupBoxDetailService.SuspendLayout();
            this.tableLayoutServiceInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.tableLayoutButtons);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(3, 317);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(255, 124);
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
            this.tableLayoutButtons.Controls.Add(this.btnClearService, 1, 1);
            this.tableLayoutButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutButtons.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutButtons.Name = "tableLayoutButtons";
            this.tableLayoutButtons.RowCount = 2;
            this.tableLayoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutButtons.Size = new System.Drawing.Size(255, 124);
            this.tableLayoutButtons.TabIndex = 1;
            // 
            // btnAddService
            // 
            this.btnAddService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnAddService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddService.ForeColor = System.Drawing.Color.White;
            this.btnAddService.Location = new System.Drawing.Point(3, 3);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(121, 56);
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
            this.btnEditService.Location = new System.Drawing.Point(130, 3);
            this.btnEditService.Name = "btnEditService";
            this.btnEditService.Size = new System.Drawing.Size(122, 56);
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
            this.btnDeleteService.Location = new System.Drawing.Point(3, 65);
            this.btnDeleteService.Name = "btnDeleteService";
            this.btnDeleteService.Size = new System.Drawing.Size(121, 56);
            this.btnDeleteService.TabIndex = 2;
            this.btnDeleteService.Text = "Xóa";
            this.btnDeleteService.UseVisualStyleBackColor = false;
            this.btnDeleteService.Click += new System.EventHandler(this.btnDeleteService_Click);
            // 
            // btnClearService
            // 
            this.btnClearService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.btnClearService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClearService.ForeColor = System.Drawing.Color.White;
            this.btnClearService.Location = new System.Drawing.Point(130, 65);
            this.btnClearService.Name = "btnClearService";
            this.btnClearService.Size = new System.Drawing.Size(122, 56);
            this.btnClearService.TabIndex = 3;
            this.btnClearService.Text = "Làm Mới";
            this.btnClearService.UseVisualStyleBackColor = false;
            this.btnClearService.Click += new System.EventHandler(this.btnClearService_Click);
            // 
            // lblPrice
            // 
            this.lblPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrice.ForeColor = System.Drawing.Color.White;
            this.lblPrice.Location = new System.Drawing.Point(3, 58);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(81, 40);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "Price: ";
            this.lblPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPrice
            // 
            this.txtPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPrice.Location = new System.Drawing.Point(90, 61);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(156, 26);
            this.txtPrice.TabIndex = 3;
            // 
            // lblNameService
            // 
            this.lblNameService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNameService.ForeColor = System.Drawing.Color.White;
            this.lblNameService.Location = new System.Drawing.Point(3, 16);
            this.lblNameService.Name = "lblNameService";
            this.lblNameService.Size = new System.Drawing.Size(81, 42);
            this.lblNameService.TabIndex = 11;
            this.lblNameService.Text = "Service Name:";
            this.lblNameService.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServiceName
            // 
            this.txtServiceName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServiceName.Location = new System.Drawing.Point(90, 19);
            this.txtServiceName.Name = "txtServiceName";
            this.txtServiceName.Size = new System.Drawing.Size(156, 26);
            this.txtServiceName.TabIndex = 10;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(3, 98);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(81, 35);
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
            this.panelUser.Size = new System.Drawing.Size(800, 450);
            this.panelUser.TabIndex = 1;
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.66666F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanelMain.Controls.Add(this.datagridViewService, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutRight, 1, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 1;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // datagridViewService
            // 
            this.datagridViewService.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.datagridViewService.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.datagridViewService.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagridViewService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datagridViewService.Location = new System.Drawing.Point(3, 3);
            this.datagridViewService.Name = "datagridViewService";
            this.datagridViewService.ReadOnly = true;
            this.datagridViewService.RowHeadersWidth = 62;
            this.datagridViewService.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagridViewService.Size = new System.Drawing.Size(527, 444);
            this.datagridViewService.TabIndex = 0;
            this.datagridViewService.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridViewService_CellContentClick);
            // 
            // tableLayoutRight
            // 
            this.tableLayoutRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.tableLayoutRight.ColumnCount = 1;
            this.tableLayoutRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRight.Controls.Add(this.groupBoxSearch, 0, 0);
            this.tableLayoutRight.Controls.Add(this.panelButtons, 0, 2);
            this.tableLayoutRight.Controls.Add(this.groupBoxDetailService, 0, 1);
            this.tableLayoutRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRight.Location = new System.Drawing.Point(536, 3);
            this.tableLayoutRight.Name = "tableLayoutRight";
            this.tableLayoutRight.RowCount = 3;
            this.tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.53061F));
            this.tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.46939F));
            this.tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.tableLayoutRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutRight.Size = new System.Drawing.Size(261, 444);
            this.tableLayoutRight.TabIndex = 1;
            // 
            // groupBoxSearch
            // 
            this.groupBoxSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBoxSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.groupBoxSearch.Controls.Add(this.txtSearchService);
            this.groupBoxSearch.ForeColor = System.Drawing.Color.White;
            this.groupBoxSearch.Location = new System.Drawing.Point(3, 3);
            this.groupBoxSearch.Name = "groupBoxSearch";
            this.groupBoxSearch.Padding = new System.Windows.Forms.Padding(10);
            this.groupBoxSearch.Size = new System.Drawing.Size(255, 77);
            this.groupBoxSearch.TabIndex = 2;
            this.groupBoxSearch.TabStop = false;
            this.groupBoxSearch.Text = "Tìm Kiếm";
            // 
            // txtSearchService
            // 
            this.txtSearchService.Location = new System.Drawing.Point(10, 25);
            this.txtSearchService.Name = "txtSearchService";
            this.txtSearchService.Size = new System.Drawing.Size(235, 26);
            this.txtSearchService.TabIndex = 0;
            this.txtSearchService.TextChanged += new System.EventHandler(this.txtSearchService_TextChanged);
            // 
            // groupBoxDetailService
            // 
            this.groupBoxDetailService.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDetailService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(55)))));
            this.groupBoxDetailService.Controls.Add(this.tableLayoutServiceInfo);
            this.groupBoxDetailService.ForeColor = System.Drawing.Color.White;
            this.groupBoxDetailService.Location = new System.Drawing.Point(3, 86);
            this.groupBoxDetailService.Name = "groupBoxDetailService";
            this.groupBoxDetailService.Size = new System.Drawing.Size(255, 225);
            this.groupBoxDetailService.TabIndex = 0;
            this.groupBoxDetailService.TabStop = false;
            this.groupBoxDetailService.Text = "Thông tin dịch vụ";
            // 
            // tableLayoutServiceInfo
            // 
            this.tableLayoutServiceInfo.ColumnCount = 2;
            this.tableLayoutServiceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutServiceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutServiceInfo.Controls.Add(this.lblStatus, 0, 3);
            this.tableLayoutServiceInfo.Controls.Add(this.lblPrice, 0, 2);
            this.tableLayoutServiceInfo.Controls.Add(this.lblNameService, 0, 1);
            this.tableLayoutServiceInfo.Controls.Add(this.txtPrice, 1, 2);
            this.tableLayoutServiceInfo.Controls.Add(this.txtServiceName, 1, 1);
            this.tableLayoutServiceInfo.Controls.Add(this.chkIsActive, 1, 3);
            this.tableLayoutServiceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutServiceInfo.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutServiceInfo.Name = "tableLayoutServiceInfo";
            this.tableLayoutServiceInfo.RowCount = 5;
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutServiceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutServiceInfo.Size = new System.Drawing.Size(249, 200);
            this.tableLayoutServiceInfo.TabIndex = 0;
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(90, 101);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(91, 24);
            this.chkIsActive.TabIndex = 12;
            this.chkIsActive.Text = "IsActive";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // frmService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(26)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelUser);
            this.Name = "frmService";
            this.Text = "Quản Lý Dịch Vụ";
            this.Load += new System.EventHandler(this.frmService_Load);
            this.panelButtons.ResumeLayout(false);
            this.tableLayoutButtons.ResumeLayout(false);
            this.panelUser.ResumeLayout(false);
            this.tableLayoutPanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.datagridViewService)).EndInit();
            this.tableLayoutRight.ResumeLayout(false);
            this.groupBoxSearch.ResumeLayout(false);
            this.groupBoxSearch.PerformLayout();
            this.groupBoxDetailService.ResumeLayout(false);
            this.tableLayoutServiceInfo.ResumeLayout(false);
            this.tableLayoutServiceInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        private void datagridViewService_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            throw new NotImplementedException();
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
        private System.Windows.Forms.Button btnClearService;
        private System.Windows.Forms.CheckBox chkIsActive;
    }
}