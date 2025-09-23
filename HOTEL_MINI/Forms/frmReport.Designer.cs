using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    partial class frmReport
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
            this.tabCustomerReport = new System.Windows.Forms.TabControl();
            this.tabCustomer = new System.Windows.Forms.TabPage();
            this.rpvCustomer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabRevenuRoom = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.rpvRevenuRoom = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabInvoice = new System.Windows.Forms.TabPage();
            this.rpvInvoice = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabCustomerReport.SuspendLayout();
            this.tabCustomer.SuspendLayout();
            this.tabRevenuRoom.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabInvoice.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCustomerReport
            // 
            this.tabCustomerReport.Controls.Add(this.tabCustomer);
            this.tabCustomerReport.Controls.Add(this.tabRevenuRoom);
            this.tabCustomerReport.Controls.Add(this.tabInvoice);
            this.tabCustomerReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCustomerReport.Location = new System.Drawing.Point(0, 0);
            this.tabCustomerReport.Name = "tabCustomerReport";
            this.tabCustomerReport.SelectedIndex = 0;
            this.tabCustomerReport.Size = new System.Drawing.Size(1263, 760);
            this.tabCustomerReport.TabIndex = 0;
            // 
            // tabCustomer
            // 
            this.tabCustomer.Controls.Add(this.rpvCustomer);
            this.tabCustomer.Location = new System.Drawing.Point(4, 29);
            this.tabCustomer.Name = "tabCustomer";
            this.tabCustomer.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustomer.Size = new System.Drawing.Size(1255, 727);
            this.tabCustomer.TabIndex = 0;
            this.tabCustomer.Text = "Customer Report";
            this.tabCustomer.UseVisualStyleBackColor = true;
            // 
            // rpvCustomer
            // 
            this.rpvCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvCustomer.LocalReport.ReportEmbeddedResource = "HOTEL_MINI.Report.ReportCustomer.rdlc";
            this.rpvCustomer.Location = new System.Drawing.Point(3, 3);
            this.rpvCustomer.Name = "rpvCustomer";
            this.rpvCustomer.ServerReport.BearerToken = null;
            this.rpvCustomer.Size = new System.Drawing.Size(1249, 721);
            this.rpvCustomer.TabIndex = 0;
            // 
            // tabRevenuRoom
            // 
            this.tabRevenuRoom.Controls.Add(this.panel1);
            this.tabRevenuRoom.Controls.Add(this.rpvRevenuRoom);
            this.tabRevenuRoom.Location = new System.Drawing.Point(4, 29);
            this.tabRevenuRoom.Name = "tabRevenuRoom";
            this.tabRevenuRoom.Padding = new System.Windows.Forms.Padding(3);
            this.tabRevenuRoom.Size = new System.Drawing.Size(1255, 727);
            this.tabRevenuRoom.TabIndex = 2;
            this.tabRevenuRoom.Text = "Revenu Room";
            this.tabRevenuRoom.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(949, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(303, 721);
            this.panel1.TabIndex = 1;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.CustomFormat = "MM/yyyy";
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(0, 85);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(298, 53);
            this.dateTimePicker1.TabIndex = 0;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // rpvRevenuRoom
            // 
            this.rpvRevenuRoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvRevenuRoom.LocalReport.ReportEmbeddedResource = "HOTEL_MINI.Report.ReportRevenuRoom.rdlc";
            this.rpvRevenuRoom.Location = new System.Drawing.Point(3, 3);
            this.rpvRevenuRoom.Name = "rpvRevenuRoom";
            this.rpvRevenuRoom.ServerReport.BearerToken = null;
            this.rpvRevenuRoom.Size = new System.Drawing.Size(1249, 721);
            this.rpvRevenuRoom.TabIndex = 0;
            // 
            // tabInvoice
            // 
            this.tabInvoice.Controls.Add(this.rpvInvoice);
            this.tabInvoice.Location = new System.Drawing.Point(4, 29);
            this.tabInvoice.Name = "tabInvoice";
            this.tabInvoice.Padding = new System.Windows.Forms.Padding(3);
            this.tabInvoice.Size = new System.Drawing.Size(1255, 727);
            this.tabInvoice.TabIndex = 1;
            this.tabInvoice.Text = "Invoice Report";
            this.tabInvoice.UseVisualStyleBackColor = true;
            // 
            // rpvInvoice
            // 
            this.rpvInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rpvInvoice.LocalReport.ReportEmbeddedResource = "HOTEL_MINI.Report.ReportInvoice.rdlc";
            this.rpvInvoice.Location = new System.Drawing.Point(3, 3);
            this.rpvInvoice.Name = "rpvInvoice";
            this.rpvInvoice.ServerReport.BearerToken = null;
            this.rpvInvoice.Size = new System.Drawing.Size(1249, 721);
            this.rpvInvoice.TabIndex = 0;
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 760);
            this.Controls.Add(this.tabCustomerReport);
            this.Name = "frmReport";
            this.Text = "frmReport";
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.tabCustomerReport.ResumeLayout(false);
            this.tabCustomer.ResumeLayout(false);
            this.tabRevenuRoom.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabInvoice.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCustomerReport;
        private System.Windows.Forms.TabPage tabCustomer;
        private System.Windows.Forms.TabPage tabInvoice;
        private Microsoft.Reporting.WinForms.ReportViewer rpvCustomer;
        private Microsoft.Reporting.WinForms.ReportViewer rpvInvoice;
        private System.Windows.Forms.TabPage tabRevenuRoom;
        private Microsoft.Reporting.WinForms.ReportViewer rpvRevenuRoom;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}