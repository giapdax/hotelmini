// HOTEL_MINI.Forms.Dialogs/dlgRoomTime.Designer.cs
using System.ComponentModel;
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
        private Button btnOK;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.lblIn = new System.Windows.Forms.Label();
            this.lblOut = new System.Windows.Forms.Label();
            this.dtpIn = new System.Windows.Forms.DateTimePicker();
            this.dtpOut = new System.Windows.Forms.DateTimePicker();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rdoReceiveNow = new System.Windows.Forms.RadioButton();
            this.rdoReceiveLater = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(17, 24);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(113, 20);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Hình thức thuê";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.Location = new System.Drawing.Point(141, 21);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(210, 28);
            this.cboType.TabIndex = 1;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // lblIn
            // 
            this.lblIn.AutoSize = true;
            this.lblIn.Location = new System.Drawing.Point(17, 108);
            this.lblIn.Name = "lblIn";
            this.lblIn.Size = new System.Drawing.Size(75, 20);
            this.lblIn.TabIndex = 2;
            this.lblIn.Text = "Check-in:";
            // 
            // lblOut
            // 
            this.lblOut.AutoSize = true;
            this.lblOut.Location = new System.Drawing.Point(17, 140);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new System.Drawing.Size(86, 20);
            this.lblOut.TabIndex = 3;
            this.lblOut.Text = "Check-out:";
            // 
            // dtpIn
            // 
            this.dtpIn.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpIn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIn.Location = new System.Drawing.Point(115, 104);
            this.dtpIn.Name = "dtpIn";
            this.dtpIn.Size = new System.Drawing.Size(210, 26);
            this.dtpIn.TabIndex = 4;
            // 
            // dtpOut
            // 
            this.dtpOut.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpOut.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOut.Location = new System.Drawing.Point(115, 136);
            this.dtpOut.Name = "dtpOut";
            this.dtpOut.Size = new System.Drawing.Size(210, 26);
            this.dtpOut.TabIndex = 5;
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Location = new System.Drawing.Point(17, 170);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(77, 20);
            this.lblUnitPrice.TabIndex = 6;
            this.lblUnitPrice.Text = "Đơn giá: -";
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(17, 192);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(87, 20);
            this.lblCost.TabIndex = 7;
            this.lblCost.Text = "Tạm tính: 0";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(169, 222);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click_1);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(250, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Huỷ";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // rdoReceiveNow
            // 
            this.rdoReceiveNow.AutoSize = true;
            this.rdoReceiveNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoReceiveNow.Location = new System.Drawing.Point(8, 55);
            this.rdoReceiveNow.Name = "rdoReceiveNow";
            this.rdoReceiveNow.Size = new System.Drawing.Size(152, 33);
            this.rdoReceiveNow.TabIndex = 10;
            this.rdoReceiveNow.TabStop = true;
            this.rdoReceiveNow.Text = "Nhận ngay";
            this.rdoReceiveNow.UseVisualStyleBackColor = true;
            // 
            // rdoReceiveLater
            // 
            this.rdoReceiveLater.AutoSize = true;
            this.rdoReceiveLater.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoReceiveLater.Location = new System.Drawing.Point(212, 55);
            this.rdoReceiveLater.Name = "rdoReceiveLater";
            this.rdoReceiveLater.Size = new System.Drawing.Size(139, 33);
            this.rdoReceiveLater.TabIndex = 11;
            this.rdoReceiveLater.TabStop = true;
            this.rdoReceiveLater.Text = "Nhận sau";
            this.rdoReceiveLater.UseVisualStyleBackColor = true;
            // 
            // dlgRoomTime
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(359, 272);
            this.Controls.Add(this.rdoReceiveLater);
            this.Controls.Add(this.rdoReceiveNow);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.lblUnitPrice);
            this.Controls.Add(this.dtpOut);
            this.Controls.Add(this.dtpIn);
            this.Controls.Add(this.lblOut);
            this.Controls.Add(this.lblIn);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.lblType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgRoomTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn thời gian & hình thức";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private RadioButton rdoReceiveNow;
        private RadioButton rdoReceiveLater;
    }
}
