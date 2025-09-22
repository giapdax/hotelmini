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
            this.pnlRight.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpAllRooms
            // 
            this.flpAllRooms.AutoScroll = true;
            this.flpAllRooms.AutoSize = true;
            this.flpAllRooms.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.flpAllRooms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpAllRooms.Location = new System.Drawing.Point(0, 0);
            this.flpAllRooms.Name = "flpAllRooms";
            this.flpAllRooms.Size = new System.Drawing.Size(676, 546);
            this.flpAllRooms.TabIndex = 0;
            // 
            // cbxRoomStatus
            // 
            this.cbxRoomStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRoomStatus.FormattingEnabled = true;
            this.cbxRoomStatus.Location = new System.Drawing.Point(65, 26);
            this.cbxRoomStatus.Name = "cbxRoomStatus";
            this.cbxRoomStatus.Size = new System.Drawing.Size(236, 37);
            this.cbxRoomStatus.TabIndex = 1;
            this.cbxRoomStatus.SelectedIndexChanged += new System.EventHandler(this.cbxRoomStatus_SelectedIndexChanged);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnlRight.Controls.Add(this.panel1);
            this.pnlRight.Controls.Add(this.cbxRoomType);
            this.pnlRight.Controls.Add(this.btnResetFilter);
            this.pnlRight.Controls.Add(this.txtSearchRoomNumber);
            this.pnlRight.Controls.Add(this.cbxRoomStatus);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(676, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(372, 546);
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
            this.panel1.Location = new System.Drawing.Point(6, 319);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 315);
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
            this.lblHourly.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHourly.Location = new System.Drawing.Point(3, 48);
            this.lblHourly.Name = "lblHourly";
            this.lblHourly.Size = new System.Drawing.Size(110, 29);
            this.lblHourly.TabIndex = 9;
            this.lblHourly.Text = "Theo giờ";
            // 
            // lblNightly
            // 
            this.lblNightly.AutoSize = true;
            this.lblNightly.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNightly.Location = new System.Drawing.Point(3, 111);
            this.lblNightly.Name = "lblNightly";
            this.lblNightly.Size = new System.Drawing.Size(124, 29);
            this.lblNightly.TabIndex = 6;
            this.lblNightly.Text = "Theo đêm";
            // 
            // lblDaily
            // 
            this.lblDaily.AutoSize = true;
            this.lblDaily.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDaily.Location = new System.Drawing.Point(3, 181);
            this.lblDaily.Name = "lblDaily";
            this.lblDaily.Size = new System.Drawing.Size(127, 29);
            this.lblDaily.TabIndex = 7;
            this.lblDaily.Text = "Theo ngày";
            // 
            // lblWeekly
            // 
            this.lblWeekly.AutoSize = true;
            this.lblWeekly.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeekly.Location = new System.Drawing.Point(3, 249);
            this.lblWeekly.Name = "lblWeekly";
            this.lblWeekly.Size = new System.Drawing.Size(121, 29);
            this.lblWeekly.TabIndex = 8;
            this.lblWeekly.Text = "Theo tuần";
            // 
            // cbxRoomType
            // 
            this.cbxRoomType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxRoomType.FormattingEnabled = true;
            this.cbxRoomType.Location = new System.Drawing.Point(65, 81);
            this.cbxRoomType.Name = "cbxRoomType";
            this.cbxRoomType.Size = new System.Drawing.Size(236, 37);
            this.cbxRoomType.TabIndex = 4;
            this.cbxRoomType.SelectedIndexChanged += new System.EventHandler(this.cbxRoomType_SelectedIndexChanged);
            // 
            // btnResetFilter
            // 
            this.btnResetFilter.Location = new System.Drawing.Point(152, 179);
            this.btnResetFilter.Name = "btnResetFilter";
            this.btnResetFilter.Size = new System.Drawing.Size(75, 45);
            this.btnResetFilter.TabIndex = 3;
            this.btnResetFilter.Text = "reset";
            this.btnResetFilter.UseVisualStyleBackColor = true;
            this.btnResetFilter.Click += new System.EventHandler(this.btnResetFilter_Click);
            // 
            // txtSearchRoomNumber
            // 
            this.txtSearchRoomNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchRoomNumber.Location = new System.Drawing.Point(65, 138);
            this.txtSearchRoomNumber.Name = "txtSearchRoomNumber";
            this.txtSearchRoomNumber.Size = new System.Drawing.Size(236, 35);
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
            this.pnlMain.Size = new System.Drawing.Size(676, 546);
            this.pnlMain.TabIndex = 3;
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
            this.pnlRight.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
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
    }
}