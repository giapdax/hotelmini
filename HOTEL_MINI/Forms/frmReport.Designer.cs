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
            this.tabInvoice = new System.Windows.Forms.TabPage();
            this.rpvInvoice = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabCustomer = new System.Windows.Forms.TabPage();
            this.rpvCustomer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabCustomerReport = new System.Windows.Forms.TabControl();
            this.tabInvoice.SuspendLayout();
            this.tabCustomer.SuspendLayout();
            this.tabCustomerReport.SuspendLayout();
            this.SuspendLayout();
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
            // tabCustomerReport
            // 
            this.tabCustomerReport.Controls.Add(this.tabCustomer);
            this.tabCustomerReport.Controls.Add(this.tabInvoice);
            this.tabCustomerReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCustomerReport.Location = new System.Drawing.Point(0, 0);
            this.tabCustomerReport.Name = "tabCustomerReport";
            this.tabCustomerReport.SelectedIndex = 0;
            this.tabCustomerReport.Size = new System.Drawing.Size(1263, 760);
            this.tabCustomerReport.TabIndex = 0;
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 760);
            this.Controls.Add(this.tabCustomerReport);
            this.Name = "frmReport";
            this.Text = "Báo Cáo";
            this.Load += new System.EventHandler(this.frmReport_Load);
            this.tabInvoice.ResumeLayout(false);
            this.tabCustomer.ResumeLayout(false);
            this.tabCustomerReport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabPage tabInvoice;
        private Microsoft.Reporting.WinForms.ReportViewer rpvInvoice;
        private TabPage tabCustomer;
        private Microsoft.Reporting.WinForms.ReportViewer rpvCustomer;
        private TabControl tabCustomerReport;
    }
}