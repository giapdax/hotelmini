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
            this.components = new Container();
            this.lblType = new Label();
            this.cboType = new ComboBox();
            this.lblIn = new Label();
            this.lblOut = new Label();
            this.dtpIn = new DateTimePicker();
            this.dtpOut = new DateTimePicker();
            this.lblUnitPrice = new Label();
            this.lblCost = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.SuspendLayout();
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(71, 13);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "Hình thức thuê";
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboType.Location = new System.Drawing.Point(110, 12);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(210, 21);
            this.cboType.TabIndex = 1;
            // 
            // lblIn
            // 
            this.lblIn.AutoSize = true;
            this.lblIn.Location = new System.Drawing.Point(12, 48);
            this.lblIn.Name = "lblIn";
            this.lblIn.Size = new System.Drawing.Size(52, 13);
            this.lblIn.TabIndex = 2;
            this.lblIn.Text = "Check-in:";
            // 
            // lblOut
            // 
            this.lblOut.AutoSize = true;
            this.lblOut.Location = new System.Drawing.Point(12, 80);
            this.lblOut.Name = "lblOut";
            this.lblOut.Size = new System.Drawing.Size(59, 13);
            this.lblOut.TabIndex = 3;
            this.lblOut.Text = "Check-out:";
            // 
            // dtpIn
            // 
            this.dtpIn.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpIn.Format = DateTimePickerFormat.Custom;
            this.dtpIn.Location = new System.Drawing.Point(110, 44);
            this.dtpIn.Name = "dtpIn";
            this.dtpIn.Size = new System.Drawing.Size(210, 20);
            this.dtpIn.TabIndex = 4;
            // 
            // dtpOut
            // 
            this.dtpOut.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpOut.Format = DateTimePickerFormat.Custom;
            this.dtpOut.Location = new System.Drawing.Point(110, 76);
            this.dtpOut.Name = "dtpOut";
            this.dtpOut.Size = new System.Drawing.Size(210, 20);
            this.dtpOut.TabIndex = 5;
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.Location = new System.Drawing.Point(12, 110);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(65, 13);
            this.lblUnitPrice.TabIndex = 6;
            this.lblUnitPrice.Text = "Đơn giá: -";
            // 
            // lblCost
            // 
            this.lblCost.AutoSize = true;
            this.lblCost.Location = new System.Drawing.Point(12, 132);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(78, 13);
            this.lblCost.TabIndex = 7;
            this.lblCost.Text = "Tạm tính: 0";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(164, 162);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 27);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(245, 162);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 27);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Huỷ";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dlgRoomTime
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnCancel;
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 201);
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
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgRoomTime";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Chọn thời gian & hình thức";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
