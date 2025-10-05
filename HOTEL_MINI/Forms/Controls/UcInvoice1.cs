using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms.Controls
{
    public partial class UcInvoice1 : UserControl
    {
        // ====== View models ======
        public class InvoiceServiceRow
        {
            public string ServiceName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal Total => Price * Quantity;
        }

        public class RoomRow
        {
            public string RoomNumber { get; set; }
            public string PricingType { get; set; }
            public string CheckIn { get; set; }
            public string CheckOut { get; set; }
        }

        public class InvoiceVm
        {
            public string CustomerName { get; set; }
            public string CustomerIdNumber { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            public decimal RoomCharge { get; set; }
            public decimal ServiceCharge { get; set; }
            public decimal Discount { get; set; }
            public decimal Surcharge { get; set; }
            public decimal Total { get; set; }
            public string EmployeeName { get; set; }
            public string PaymentMethod { get; set; }
            public string Note { get; set; }
        }

        public UcInvoice1()
        {
            InitializeComponent();
            WireUp();
            SetupGrids();
        }

        private void WireUp()
        {
            btnBack.Click += (s, e) => this.FindForm()?.Close();
            btnExportInvoice.Click += btnExportInvoice_Click;
        }

        private void SetupGrids()
        {
            // Grid dịch vụ
            dgvUsedService.AutoGenerateColumns = false;
            dgvUsedService.Columns.Clear();
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(InvoiceServiceRow.ServiceName), HeaderText = "Dịch vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(InvoiceServiceRow.Price), HeaderText = "Đơn giá", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(InvoiceServiceRow.Quantity), HeaderText = "SL", Width = 50 });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(InvoiceServiceRow.Total), HeaderText = "Thành tiền", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });

            // Grid phòng (dataGridView1) – đã có sẵn trong designer
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomRow.RoomNumber), HeaderText = "Phòng", Width = 80 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomRow.PricingType), HeaderText = "Giá theo", Width = 90 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomRow.CheckIn), HeaderText = "Check-in", Width = 130 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomRow.CheckOut), HeaderText = "Check-out", Width = 130 });
        }

        public void BindFrom(InvoiceVm vm, IEnumerable<InvoiceServiceRow> services, IEnumerable<RoomRow> rooms = null)
        {
            // Header
            txtCusName.Text = vm.CustomerName ?? "";
            txtCusId.Text = vm.CustomerIdNumber ?? "";
            txtCheckin.Text = vm.CheckIn.ToString("dd/MM/yyyy HH:mm");
            txtCheckout.Text = vm.CheckOut.ToString("dd/MM/yyyy HH:mm");

            // Tiền
            txtRoomCharge.Text = vm.RoomCharge.ToString("N0");
            txtServiceCharge.Text = vm.ServiceCharge.ToString("N0");
            txtSurcharge.Text = vm.Surcharge.ToString("N0");
            txtDiscount.Text = vm.Discount.ToString("N0");
            txtTotalAmount.Text = vm.Total.ToString("N0");

            // Khác
            txtEmployeeName.Text = vm.EmployeeName ?? "";
            txtPaymentMethod.Text = vm.PaymentMethod ?? "";
            txtNote.Text = vm.Note ?? "";

            // Bind grids
            var serviceList = services?.ToList() ?? new List<InvoiceServiceRow>();
            dgvUsedService.DataSource = new BindingList<InvoiceServiceRow>(serviceList);

            if (rooms != null)
            {
                dataGridView1.DataSource = new BindingList<RoomRow>(rooms.ToList());
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog
            {
                Filter = "PNG Image|*.png",
                FileName = $"Invoice_{DateTime.Now:yyyyMMdd_HHmm}.png"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var bmp = new Bitmap(this.Width, this.Height))
                    {
                        this.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                        bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    MessageBox.Show("Đã xuất hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
