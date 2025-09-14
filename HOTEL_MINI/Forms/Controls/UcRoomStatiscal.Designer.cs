using System;

namespace HOTEL_MINI.Forms.Controls
{
    partial class UcRoomStatiscal
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button(); // CHỈ KHAI BÁO 1 LẦN
            this.txtNumberOfMaintenanceRoom = new System.Windows.Forms.TextBox();
            this.txtNumberOfOccupiedRoom = new System.Windows.Forms.TextBox();
            this.txtNumberOfAvailableRoom = new System.Windows.Forms.TextBox();
            this.txtNumberOfBookedRoom = new System.Windows.Forms.TextBox();
            this.txtNumberOfTotalRoom = new System.Windows.Forms.TextBox();
            this.lblNumberOfMaintenanceRoom = new System.Windows.Forms.Label();
            this.lblnumberOfOccupiedRoom = new System.Windows.Forms.Label();
            this.lblNumberOfBookedRoom = new System.Windows.Forms.Label();
            this.lblNumberOfAvailableRoom = new System.Windows.Forms.Label();
            this.lblNumberOfTotalRoom = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.txtNumberOfMaintenanceRoom);
            this.panel1.Controls.Add(this.txtNumberOfOccupiedRoom);
            this.panel1.Controls.Add(this.txtNumberOfAvailableRoom);
            this.panel1.Controls.Add(this.txtNumberOfBookedRoom);
            this.panel1.Controls.Add(this.txtNumberOfTotalRoom);
            this.panel1.Controls.Add(this.lblNumberOfMaintenanceRoom);
            this.panel1.Controls.Add(this.lblnumberOfOccupiedRoom);
            this.panel1.Controls.Add(this.lblNumberOfBookedRoom);
            this.panel1.Controls.Add(this.lblNumberOfAvailableRoom);
            this.panel1.Controls.Add(this.lblNumberOfTotalRoom);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1311, 874);
            this.panel1.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(732, 744);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(200, 50);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtNumberOfMaintenanceRoom
            // 
            this.txtNumberOfMaintenanceRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfMaintenanceRoom.Location = new System.Drawing.Point(586, 613);
            this.txtNumberOfMaintenanceRoom.Name = "txtNumberOfMaintenanceRoom";
            this.txtNumberOfMaintenanceRoom.Size = new System.Drawing.Size(456, 44);
            this.txtNumberOfMaintenanceRoom.TabIndex = 9;
            // 
            // txtNumberOfOccupiedRoom
            // 
            this.txtNumberOfOccupiedRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfOccupiedRoom.Location = new System.Drawing.Point(586, 492);
            this.txtNumberOfOccupiedRoom.Name = "txtNumberOfOccupiedRoom";
            this.txtNumberOfOccupiedRoom.Size = new System.Drawing.Size(456, 44);
            this.txtNumberOfOccupiedRoom.TabIndex = 8;
            // 
            // txtNumberOfAvailableRoom
            // 
            this.txtNumberOfAvailableRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfAvailableRoom.Location = new System.Drawing.Point(586, 229);
            this.txtNumberOfAvailableRoom.Name = "txtNumberOfAvailableRoom";
            this.txtNumberOfAvailableRoom.Size = new System.Drawing.Size(456, 44);
            this.txtNumberOfAvailableRoom.TabIndex = 7;
            // 
            // txtNumberOfBookedRoom
            // 
            this.txtNumberOfBookedRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfBookedRoom.Location = new System.Drawing.Point(586, 363);
            this.txtNumberOfBookedRoom.Name = "txtNumberOfBookedRoom";
            this.txtNumberOfBookedRoom.Size = new System.Drawing.Size(456, 44);
            this.txtNumberOfBookedRoom.TabIndex = 6;
            // 
            // txtNumberOfTotalRoom
            // 
            this.txtNumberOfTotalRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumberOfTotalRoom.Location = new System.Drawing.Point(586, 113);
            this.txtNumberOfTotalRoom.Name = "txtNumberOfTotalRoom";
            this.txtNumberOfTotalRoom.Size = new System.Drawing.Size(456, 44);
            this.txtNumberOfTotalRoom.TabIndex = 5;
            // 
            // lblNumberOfMaintenanceRoom
            // 
            this.lblNumberOfMaintenanceRoom.AutoSize = true;
            this.lblNumberOfMaintenanceRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfMaintenanceRoom.Location = new System.Drawing.Point(53, 616);
            this.lblNumberOfMaintenanceRoom.Name = "lblNumberOfMaintenanceRoom";
            this.lblNumberOfMaintenanceRoom.Size = new System.Drawing.Size(536, 56);
            this.lblNumberOfMaintenanceRoom.TabIndex = 4;
            this.lblNumberOfMaintenanceRoom.Text = "Số phòng đang bảo trì";
            // 
            // lblnumberOfOccupiedRoom
            // 
            this.lblnumberOfOccupiedRoom.AutoSize = true;
            this.lblnumberOfOccupiedRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnumberOfOccupiedRoom.Location = new System.Drawing.Point(53, 492);
            this.lblnumberOfOccupiedRoom.Name = "lblnumberOfOccupiedRoom";
            this.lblnumberOfOccupiedRoom.Size = new System.Drawing.Size(629, 56);
            this.lblnumberOfOccupiedRoom.TabIndex = 3;
            this.lblnumberOfOccupiedRoom.Text = "Số phòng đang có người ở";
            // 
            // lblNumberOfBookedRoom
            // 
            this.lblNumberOfBookedRoom.AutoSize = true;
            this.lblNumberOfBookedRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfBookedRoom.Location = new System.Drawing.Point(53, 366);
            this.lblNumberOfBookedRoom.Name = "lblNumberOfBookedRoom";
            this.lblNumberOfBookedRoom.Size = new System.Drawing.Size(629, 56);
            this.lblNumberOfBookedRoom.TabIndex = 2;
            this.lblNumberOfBookedRoom.Text = "Số phòng đang được book";
            // 
            // lblNumberOfAvailableRoom
            // 
            this.lblNumberOfAvailableRoom.AutoSize = true;
            this.lblNumberOfAvailableRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfAvailableRoom.Location = new System.Drawing.Point(53, 232);
            this.lblNumberOfAvailableRoom.Name = "lblNumberOfAvailableRoom";
            this.lblNumberOfAvailableRoom.Size = new System.Drawing.Size(509, 56);
            this.lblNumberOfAvailableRoom.TabIndex = 1;
            this.lblNumberOfAvailableRoom.Text = "Số phòng đang trống";
            // 
            // lblNumberOfTotalRoom
            // 
            this.lblNumberOfTotalRoom.AutoSize = true;
            this.lblNumberOfTotalRoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfTotalRoom.Location = new System.Drawing.Point(53, 116);
            this.lblNumberOfTotalRoom.Name = "lblNumberOfTotalRoom";
            this.lblNumberOfTotalRoom.Size = new System.Drawing.Size(256, 37);
            this.lblNumberOfTotalRoom.TabIndex = 0;
            this.lblNumberOfTotalRoom.Text = "Tổng số phòng:";
            // 
            // UcRoomStatiscal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "UcRoomStatiscal";
            this.Size = new System.Drawing.Size(1311, 874);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNumberOfMaintenanceRoom;
        private System.Windows.Forms.Label lblnumberOfOccupiedRoom;
        private System.Windows.Forms.Label lblNumberOfBookedRoom;
        private System.Windows.Forms.Label lblNumberOfAvailableRoom;
        private System.Windows.Forms.Label lblNumberOfTotalRoom;
        private System.Windows.Forms.TextBox txtNumberOfMaintenanceRoom;
        private System.Windows.Forms.TextBox txtNumberOfOccupiedRoom;
        private System.Windows.Forms.TextBox txtNumberOfAvailableRoom;
        private System.Windows.Forms.TextBox txtNumberOfBookedRoom;
        private System.Windows.Forms.TextBox txtNumberOfTotalRoom;
        private System.Windows.Forms.Button btnRefresh;
    }
}
