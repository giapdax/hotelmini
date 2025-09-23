﻿using System;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    partial class frmRoom
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
            this.flpAllRooms = new System.Windows.Forms.FlowLayoutPanel();
            this.cbxRoomStatus = new System.Windows.Forms.ComboBox();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtWeekly = new System.Windows.Forms.TextBox();
            this.txtDaily = new System.Windows.Forms.TextBox();
            this.txtNightly = new System.Windows.Forms.TextBox();
            this.txtHourly = new System.Windows.Forms.TextBox();
            this.lblHourly = new System.Windows.Forms.Label();
            this.lblNightly = new System.Windows.Forms.Label();
            this.lblDaily = new System.Windows.Forms.Label();
            this.lblWeekly = new System.Windows.Forms.Label();
            this.cbxRoomType = new System.Windows.Forms.ComboBox();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.txtSearchRoomNumber = new System.Windows.Forms.TextBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlRight.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpAllRooms
            // 
            this.flpAllRooms.AutoScroll = true;
            this.flpAllRooms.AutoSize = true;
            this.flpAllRooms.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.flpAllRooms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpAllRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAllRooms.Location = new System.Drawing.Point(0, 0);
            this.flpAllRooms.Name = "flpAllRooms";
            this.flpAllRooms.Size = new System.Drawing.Size(647, 546);
            this.flpAllRooms.TabIndex = 0;
            // 
            // cbxRoomStatus
            // 
            this.cbxRoomStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRoomStatus.FormattingEnabled = true;
            this.cbxRoomStatus.Location = new System.Drawing.Point(200, 93);
            this.cbxRoomStatus.Name = "cbxRoomStatus";
            this.cbxRoomStatus.Size = new System.Drawing.Size(192, 37);
            this.cbxRoomStatus.TabIndex = 1;
            this.cbxRoomStatus.SelectedIndexChanged += new System.EventHandler(this.cbxRoomStatus_SelectedIndexChanged);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnlRight.Controls.Add(this.tableLayoutPanel1);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(647, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(401, 546);
            this.pnlRight.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtWeekly);
            this.panel1.Controls.Add(this.txtDaily);
            this.panel1.Controls.Add(this.txtNightly);
            this.panel1.Controls.Add(this.txtHourly);
            this.panel1.Controls.Add(this.lblHourly);
            this.panel1.Controls.Add(this.lblNightly);
            this.panel1.Controls.Add(this.lblDaily);
            this.panel1.Controls.Add(this.lblWeekly);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(389, 326);
            this.panel1.TabIndex = 5;
            // 
            // txtWeekly
            // 
            this.txtWeekly.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWeekly.Location = new System.Drawing.Point(127, 246);
            this.txtWeekly.Name = "txtWeekly";
            this.txtWeekly.ReadOnly = true;
            this.txtWeekly.Size = new System.Drawing.Size(225, 35);
            this.txtWeekly.TabIndex = 13;
            // 
            // txtDaily
            // 
            this.txtDaily.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDaily.Location = new System.Drawing.Point(127, 178);
            this.txtDaily.Name = "txtDaily";
            this.txtDaily.ReadOnly = true;
            this.txtDaily.Size = new System.Drawing.Size(225, 35);
            this.txtDaily.TabIndex = 12;
            // 
            // txtNightly
            // 
            this.txtNightly.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNightly.Location = new System.Drawing.Point(127, 111);
            this.txtNightly.Name = "txtNightly";
            this.txtNightly.ReadOnly = true;
            this.txtNightly.Size = new System.Drawing.Size(225, 35);
            this.txtNightly.TabIndex = 11;
            // 
            // txtHourly
            // 
            this.txtHourly.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHourly.Location = new System.Drawing.Point(127, 48);
            this.txtHourly.Name = "txtHourly";
            this.txtHourly.ReadOnly = true;
            this.txtHourly.Size = new System.Drawing.Size(225, 35);
            this.txtHourly.TabIndex = 10;
            // 
            // lblHourly
            // 
            this.lblHourly.AutoSize = true;
            this.lblHourly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHourly.Location = new System.Drawing.Point(3, 48);
            this.lblHourly.Name = "lblHourly";
            this.lblHourly.Size = new System.Drawing.Size(99, 20);
            this.lblHourly.TabIndex = 9;
            this.lblHourly.Text = "Giá theo giơ:";
            // 
            // lblNightly
            // 
            this.lblNightly.AutoSize = true;
            this.lblNightly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNightly.Location = new System.Drawing.Point(3, 111);
            this.lblNightly.Name = "lblNightly";
            this.lblNightly.Size = new System.Drawing.Size(109, 20);
            this.lblNightly.TabIndex = 6;
            this.lblNightly.Text = "Giá theo đêm:";
            // 
            // lblDaily
            // 
            this.lblDaily.AutoSize = true;
            this.lblDaily.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaily.Location = new System.Drawing.Point(3, 181);
            this.lblDaily.Name = "lblDaily";
            this.lblDaily.Size = new System.Drawing.Size(112, 20);
            this.lblDaily.TabIndex = 7;
            this.lblDaily.Text = "Giá theo ngày:";
            // 
            // lblWeekly
            // 
            this.lblWeekly.AutoSize = true;
            this.lblWeekly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeekly.Location = new System.Drawing.Point(3, 249);
            this.lblWeekly.Name = "lblWeekly";
            this.lblWeekly.Size = new System.Drawing.Size(110, 20);
            this.lblWeekly.TabIndex = 8;
            this.lblWeekly.Text = "Giá theo tuần:";
            // 
            // cbxRoomType
            // 
            this.cbxRoomType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRoomType.FormattingEnabled = true;
            this.cbxRoomType.Location = new System.Drawing.Point(200, 48);
            this.cbxRoomType.Name = "cbxRoomType";
            this.cbxRoomType.Size = new System.Drawing.Size(192, 37);
            this.cbxRoomType.TabIndex = 4;
            this.cbxRoomType.SelectedIndexChanged += new System.EventHandler(this.cbxRoomType_SelectedIndexChanged);
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Location = new System.Drawing.Point(200, 138);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(75, 42);
            this.btnResetFilter.TabIndex = 3;
            this.btnResetFilter.Text = "reset";
            this.btnResetFilter.UseVisualStyleBackColor = true;
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);
            // 
            // txtSearchRoomNumber
            // 
            this.txtSearchRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchRoomNumber.Location = new System.Drawing.Point(200, 3);
            this.txtSearchRoomNumber.Name = "txtSearchRoomNumber";
            this.txtSearchRoomNumber.Size = new System.Drawing.Size(192, 35);
            this.txtSearchRoomNumber.TabIndex = 2;
            this.txtSearchRoomNumber.TextChanged += new System.EventHandler(this.txtSearchRoomNumber_TextChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlMain.Controls.Add(this.flpAllRooms);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(647, 546);
            this.pnlMain.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.78261F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.21739F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(401, 546);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 192);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(395, 351);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bảng Giá Loại Phòng";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnResetFilter, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.cbxRoomStatus, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbxRoomType, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtSearchRoomNumber, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(395, 183);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 45);
            this.label1.TabIndex = 5;
            this.label1.Text = "Tìm kiếm phòng:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 45);
            this.label2.TabIndex = 6;
            this.label2.Text = "Loại phòng:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 45);
            this.label3.TabIndex = 7;
            this.label3.Text = "Trạng thái:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(1048, 546);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlRight);
            this.Name = "frmRoom";
            this.Text = "Quản Lý Phòng";
            this.pnlRight.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpAllRooms;
        private System.Windows.Forms.ComboBox cbxRoomStatus;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.Panel pnlMain;


        private TextBox txtSearchRoomNumber;
        private Button btnResetFilter;
        private ComboBox cbxRoomType;
        private Panel panel1;
        private Label lblNightly;
        private Label lblDaily;
        private Label lblWeekly;
        private Label lblHourly;
        private TextBox txtWeekly;
        private TextBox txtDaily;
        private TextBox txtNightly;
        private TextBox txtHourly;
        private TableLayoutPanel tableLayoutPanel1;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}