using HOTEL_MINI.BLL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmReport : Form
    {
        private readonly InvoiceService _invoiceService;
        private readonly CustomerService _customerService;
        public frmReport()
        {
            _invoiceService = new InvoiceService();
            _customerService = new CustomerService();
            InitializeComponent();
        }

        private void frmReport_Load(object sender, EventArgs e)
        {
            customerLoad();
            InvoiceLoad();
            RevenuRoomLoad();
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
        }
        private void customerLoad()
        {
            var customers = _customerService.getAllCustomers();
            rpvCustomer.LocalReport.DataSources.Clear();
            rpvCustomer.LocalReport.DataSources.Add(new ReportDataSource("CustomerDataSet", customers));
            rpvCustomer.RefreshReport();
        }
        private void InvoiceLoad()
        {
            var invoices = _invoiceService.GetAllInvoices();
            rpvInvoice.LocalReport.DataSources.Clear();
            rpvInvoice.LocalReport.DataSources.Add(new ReportDataSource("InvoiceDataSet", invoices));
            rpvInvoice.RefreshReport();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RevenuRoomLoad();
        }
        private void RevenuRoomLoad()
        {
            int month = dateTimePicker1.Value.Month;
            int year = dateTimePicker1.Value.Year;

            var revenu = _invoiceService.GetRevenueByRoom(month, year);

            rpvRevenuRoom.LocalReport.DataSources.Clear();
            rpvRevenuRoom.LocalReport.DataSources.Add(new ReportDataSource("RevenuDataSet", revenu));
            rpvRevenuRoom.RefreshReport();
        }
    }
}
