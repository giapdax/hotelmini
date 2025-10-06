using System.Windows.Forms;

namespace HOTEL_MINI.Forms.Controls
{
    partial class UcBookingRoom
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.cboStatusBooking = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnNhanphong = new System.Windows.Forms.Button();
            this.btnTraphong = new System.Windows.Forms.Button();
            this.btnHuydat = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // UcBookingRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UcBookingRoom";
            this.Size = new System.Drawing.Size(1040, 513);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1040, 513);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1034, 66);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(431, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tìm kiếm đơn đặt phòng";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 243F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 565F));
            this.tableLayoutPanel5.Controls.Add(this.cboStatusBooking, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.txtSearch, 2, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 32);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(1028, 31);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // cboStatusBooking
            // 
            this.cboStatusBooking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStatusBooking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatusBooking.Location = new System.Drawing.Point(3, 1);
            this.cboStatusBooking.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.cboStatusBooking.Name = "cboStatusBooking";
            this.cboStatusBooking.Size = new System.Drawing.Size(237, 28);
            this.cboStatusBooking.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nhập thông tin tìm kiếm: ";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(466, 2);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(559, 26);
            this.txtSearch.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dataGridView1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 75);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(1034, 389);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeight = 34;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1028, 383);
            this.dataGridView1.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 5;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.54545F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.54545F));
            this.tableLayoutPanel4.Controls.Add(this.btnNhanphong, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnTraphong, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnHuydat, 3, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 470);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1034, 40);
            this.tableLayoutPanel4.TabIndex = 2;
            // 
            // btnNhanphong
            // 
            this.btnNhanphong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnNhanphong.Location = new System.Drawing.Point(323, 3);
            this.btnNhanphong.Name = "btnNhanphong";
            this.btnNhanphong.Size = new System.Drawing.Size(105, 32);
            this.btnNhanphong.TabIndex = 0;
            this.btnNhanphong.Text = "Nhận phòng";
            this.btnNhanphong.Click += new System.EventHandler(this.btnNhanphong_Click);
            // 
            // btnTraphong
            // 
            this.btnTraphong.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTraphong.Location = new System.Drawing.Point(464, 3);
            this.btnTraphong.Name = "btnTraphong";
            this.btnTraphong.Size = new System.Drawing.Size(105, 32);
            this.btnTraphong.TabIndex = 1;
            this.btnTraphong.Text = "Trả phòng";
            this.btnTraphong.Click += new System.EventHandler(this.btnTraphong_Click);
            // 
            // btnHuydat
            // 
            this.btnHuydat.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnHuydat.Location = new System.Drawing.Point(605, 3);
            this.btnHuydat.Name = "btnHuydat";
            this.btnHuydat.Size = new System.Drawing.Size(105, 32);
            this.btnHuydat.TabIndex = 2;
            this.btnHuydat.Text = "Hủy đặt phòng";
            this.btnHuydat.Click += new System.EventHandler(this.btnHuydat_Click);
            // 
            // finalize
            // 
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel5;
        private Label label3;
        private TextBox txtSearch;
        private ComboBox cboStatusBooking;
        private TableLayoutPanel tableLayoutPanel3;
        private DataGridView dataGridView1;
        private TableLayoutPanel tableLayoutPanel4;
        private Button btnNhanphong;
        private Button btnTraphong;
        private Button btnHuydat;
    }
}
