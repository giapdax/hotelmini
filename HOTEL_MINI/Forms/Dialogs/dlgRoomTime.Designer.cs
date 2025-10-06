// HOTEL_MINI.Forms.Dialogs/dlgRoomTime.Designer.cs
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms.Dialogs
{
    partial class dlgRoomTime
    {
        private IContainer components = null;

        private Label lblType;
        private ComboBox cboType;
        private Label lblIn;
        private Label lblOut;
        private DateTimePicker dtpIn;
        private DateTimePicker dtpOut;
        private Label lblUnitPrice;
        private Label lblCost;
        private Label lblNotes;            // <— NEW
        private Button btnOK;
        private Button btnCancel;
        private RadioButton rdoReceiveNow;
        private RadioButton rdoReceiveLater;
        private ToolTip toolTip1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.lblType = new Label();
            this.cboType = new ComboBox();
            this.lblIn = new Label();
            this.lblOut = new Label();
            this.dtpIn = new DateTimePicker();
            this.dtpOut = new DateTimePicker();
            this.lblUnitPrice = new Label();
            this.lblCost = new Label();
            this.lblNotes = new Label();       // <— NEW
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.rdoReceiveNow = new RadioButton();
            this.rdoReceiveLater = new RadioButton();
            this.toolTip1 = new ToolTip(this.components);
            this.SuspendLayout();

            // === Form ===
            this.AutoScaleDimensions = new SizeF(96F, 96F);      // DPI-aware
            this.AutoScaleMode = AutoScaleMode.Dpi;
            this.ClientSize = new Size(420, 380);                // ↑ tăng chiều cao để chứa “Lưu ý”
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgRoomTime";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Chọn thời gian & hình thức";
            this.BackColor = Color.White;

            // === lblType ===
            this.lblType.AutoSize = true;
            this.lblType.Location = new Point(16, 20);
            this.lblType.Name = "lblType";
            this.lblType.Size = new Size(94, 19);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Hình thức thuê";

            // === cboType ===
            this.cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboType.Location = new Point(140, 16);
            this.cboType.Name = "cboType";
            this.cboType.Size = new Size(230, 25);
            this.cboType.TabIndex = 1;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            this.toolTip1.SetToolTip(this.cboType, "Chọn hình thức tính giá: Theo giờ / Đêm / Ngày / Tuần");

            // === rdoReceiveNow ===
            this.rdoReceiveNow.AutoSize = true;
            this.rdoReceiveNow.Location = new Point(20, 52);
            this.rdoReceiveNow.Name = "rdoReceiveNow";
            this.rdoReceiveNow.Size = new Size(94, 23);
            this.rdoReceiveNow.TabIndex = 2;
            this.rdoReceiveNow.TabStop = true;
            this.rdoReceiveNow.Text = "Nhận ngay";
            this.rdoReceiveNow.Checked = true;
            this.rdoReceiveNow.UseVisualStyleBackColor = true;
            this.toolTip1.SetToolTip(this.rdoReceiveNow, "Chọn nếu khách nhận phòng ngay");

            // === rdoReceiveLater ===
            this.rdoReceiveLater.AutoSize = true;
            this.rdoReceiveLater.Location = new Point(160, 52);
            this.rdoReceiveLater.Name = "rdoReceiveLater";
            this.rdoReceiveLater.Size = new Size(86, 23);
            this.rdoReceiveLater.TabIndex = 3;
            this.rdoReceiveLater.TabStop = true;
            this.rdoReceiveLater.Text = "Nhận sau";
            this.rdoReceiveLater.UseVisualStyleBackColor = true;
            this.toolTip1.SetToolTip(this.rdoReceiveLater, "Chọn nếu khách sẽ nhận phòng sau");

            // === lblIn ===
            this.lblIn.AutoSize = true;
            this.lblIn.Location = new Point(16, 96);
            this.lblIn.Name = "lblIn";
            this.lblIn.Size = new Size(64, 19);
            this.lblIn.TabIndex = 4;
            this.lblIn.Text = "Check-in:";

            // === dtpIn ===
            this.dtpIn.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpIn.Format = DateTimePickerFormat.Custom;
            this.dtpIn.Location = new Point(140, 92);
            this.dtpIn.Name = "dtpIn";
            this.dtpIn.Size = new Size(230, 25);
            this.dtpIn.TabIndex = 5;
            this.toolTip1.SetToolTip(this.dtpIn, "Chọn thời điểm nhận phòng");
            // (Không gắn ValueChanged ở Designer để tránh trùng với đăng ký trong constructor)

            // === lblOut ===
            this.lblOut.AutoSize = true;
            this.lblOut.Location = new Point(16, 128);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new Size(75, 19);
            this.lblOut.TabIndex = 6;
            this.lblOut.Text = "Check-out:";

            // === dtpOut ===
            this.dtpOut.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpOut.Format = DateTimePickerFormat.Custom;
            this.dtpOut.Location = new Point(140, 124);
            this.dtpOut.Name = "dtpOut";
            this.dtpOut.Size = new Size(230, 25);
            this.dtpOut.TabIndex = 7;
            this.toolTip1.SetToolTip(this.dtpOut, "Chọn thời điểm trả phòng");
            // (Không gắn ValueChanged ở Designer)

            // === lblUnitPrice ===
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Location = new Point(16, 162);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new Size(71, 19);
            this.lblUnitPrice.TabIndex = 8;
            this.lblUnitPrice.Text = "Đơn giá: -";

            // === lblCost (nhấn màu, đậm) ===
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new Point(16, 186);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new Size(87, 19);
            this.lblCost.TabIndex = 9;
            this.lblCost.Text = "Tạm tính: 0";
            this.lblCost.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblCost.ForeColor = Color.FromArgb(0, 120, 215);

            // === lblNotes (Lưu ý, in nghiêng, phía dưới) ===
            this.lblNotes.AutoSize = false;
            this.lblNotes.Location = new Point(16, 218);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new Size(390, 90);  // đủ cho vài dòng
            this.lblNotes.TabIndex = 10;
            this.lblNotes.Font = new Font("Segoe UI", 9.5F, FontStyle.Italic);
            this.lblNotes.ForeColor = Color.FromArgb(94, 94, 94);
            this.lblNotes.Padding = new Padding(0);
            this.lblNotes.Text =
                "Lưu ý:\r\n" +
                "• Theo giờ: tối thiểu 1 giờ, làm tròn lên theo giờ.\r\n" +
                "• Đêm: khung cố định 21:00 → 07:00 (1 đêm).\r\n" +
                "• Ngày: 14:00 hôm nay → 12:00 ngày hôm sau.\r\n" +
                "• Tuần: đúng 7 ngày (Check-out = Check-in + 7 ngày)."+
                "• sẽ thu thêm phụ phí nếu trả phòng muộn."+
                "• Giảm giá linh hoạt và hiện tại chỉ áp dụng thanh toán 1 lần ";

            // === btnOK ===
            this.btnOK.Location = new Point(214, 322);            // ↓ đẩy xuống dưới “Lưu ý”
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(95, 32);
            this.btnOK.TabIndex = 11;
            this.btnOK.Text = "Xác nhận";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            this.toolTip1.SetToolTip(this.btnOK, "Lưu thời gian và hình thức đã chọn");

            // === btnCancel ===
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(315, 322);        // ↓ theo nút OK
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(75, 32);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Huỷ";
            this.btnCancel.UseVisualStyleBackColor = true;

            // === Accept/Cancel buttons ===
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;

            // === Add controls ===
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.rdoReceiveNow);
            this.Controls.Add(this.rdoReceiveLater);
            this.Controls.Add(this.lblIn);
            this.Controls.Add(this.dtpIn);
            this.Controls.Add(this.lblOut);
            this.Controls.Add(this.dtpOut);
            this.Controls.Add(this.lblUnitPrice);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.lblNotes);     // <— NEW
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);

            // === Tab order tổng ===
            this.lblType.TabIndex = 0;
            this.cboType.TabIndex = 1;
            this.rdoReceiveNow.TabIndex = 2;
            this.rdoReceiveLater.TabIndex = 3;
            this.lblIn.TabIndex = 4;
            this.dtpIn.TabIndex = 5;
            this.lblOut.TabIndex = 6;
            this.dtpOut.TabIndex = 7;
            this.lblUnitPrice.TabIndex = 8;
            this.lblCost.TabIndex = 9;
            this.lblNotes.TabIndex = 10;   // <— NEW
            this.btnOK.TabIndex = 11;
            this.btnCancel.TabIndex = 12;

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
