using HOTEL_MINI.Model.Entity; // Payment entity
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms.Controls
{
    /// <summary>
    /// UserControl hiển thị hoá đơn (read-only) + export PNG.
    /// Nhận dữ liệu ViewModel từ tầng BLL/Service/Forms.
    /// </summary>
    public partial class UcInvoice : UserControl
    {
        // ===== View models (UI only) =====
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

        // map từ Entity Payment để hiển thị lịch sử thanh toán
        public class PaymentRow
        {
            public DateTime PaymentDate { get; set; }
            public string Method { get; set; }
            public decimal Amount { get; set; }
            public string Status { get; set; }
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

        public UcInvoice()
        {
            InitializeComponent();
            WireUp();
            SetupGrids();
            MakeReadonlyFields();
        }

        private void WireUp()
        {
            btnBack.Click += delegate { var f = FindForm(); if (f != null) f.Close(); };
            btnExportInvoice.Click += btnExportInvoice_Click;
        }

        private void SetupGrids()
        {
            // ===== Grid dịch vụ
            dgvUsedService.AutoGenerateColumns = false;
            dgvUsedService.AllowUserToAddRows = false;
            dgvUsedService.ReadOnly = true;
            dgvUsedService.Columns.Clear();

            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ServiceName",
                HeaderText = "Dịch vụ",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Đơn giá",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "SL",
                Width = 70
            });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Total",
                HeaderText = "Thành tiền",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            // ===== Grid phòng
            dgvRooms.AutoGenerateColumns = false;
            dgvRooms.AllowUserToAddRows = false;
            dgvRooms.ReadOnly = true;
            dgvRooms.Columns.Clear();

            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RoomNumber",
                HeaderText = "Phòng",
                Width = 90
            });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PricingType",
                HeaderText = "Giá theo",
                Width = 120
            });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckIn",
                HeaderText = "Check-in",
                Width = 160
            });
            dgvRooms.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckOut",
                HeaderText = "Check-out",
                Width = 160
            });

            // ===== Grid lịch sử thanh toán
            dgvPayments.AutoGenerateColumns = false;
            dgvPayments.AllowUserToAddRows = false;
            dgvPayments.ReadOnly = true;
            dgvPayments.Columns.Clear();

            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PaymentDate",
                HeaderText = "Ngày",
                Width = 170,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Method",
                HeaderText = "Hình thức",
                Width = 140
            });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Amount",
                HeaderText = "Số tiền",
                Width = 140,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });
            dgvPayments.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Trạng thái",
                Width = 120
            });
        }

        private void MakeReadonlyFields()
        {
            foreach (var tb in new[] { txtRoomCharge, txtServiceCharge, txtSurcharge, txtDiscount, txtTotalAmount })
            { tb.ReadOnly = true; tb.TabStop = false; }

            foreach (var tb in new[] { txtCusName, txtCusId, txtCheckin, txtCheckout, txtEmployeeName, txtPaymentMethod, txtNote })
            { tb.ReadOnly = true; tb.TabStop = false; }
        }

        /// <summary>Bind tất cả dữ liệu cho control. `payments` dùng đúng entity Payment.</summary>
        public void BindFrom(
            InvoiceVm vm,
            IEnumerable<InvoiceServiceRow> services,
            IEnumerable<RoomRow> rooms,
            IEnumerable<Payment> payments)
        {
            if (vm == null) vm = new InvoiceVm();

            // Header
            txtCusName.Text = vm.CustomerName ?? string.Empty;
            txtCusId.Text = vm.CustomerIdNumber ?? string.Empty;
            txtCheckin.Text = vm.CheckIn == default(DateTime) ? "" : vm.CheckIn.ToString("dd/MM/yyyy HH:mm");
            txtCheckout.Text = vm.CheckOut == default(DateTime) ? "" : vm.CheckOut.ToString("dd/MM/yyyy HH:mm");

            // Tiền
            txtRoomCharge.Text = vm.RoomCharge.ToString("N0");
            txtServiceCharge.Text = vm.ServiceCharge.ToString("N0");
            txtSurcharge.Text = vm.Surcharge.ToString("N0");
            txtDiscount.Text = vm.Discount.ToString("N0");
            txtTotalAmount.Text = vm.Total.ToString("N0");

            // Khác
            txtEmployeeName.Text = vm.EmployeeName ?? string.Empty;
            txtPaymentMethod.Text = vm.PaymentMethod ?? string.Empty;
            txtNote.Text = vm.Note ?? string.Empty;

            // Grids
            dgvUsedService.DataSource =
                new BindingList<InvoiceServiceRow>((services ?? Enumerable.Empty<InvoiceServiceRow>()).ToList());

            dgvRooms.DataSource =
                new BindingList<RoomRow>((rooms ?? Enumerable.Empty<RoomRow>()).ToList());

            var payRows = (payments ?? Enumerable.Empty<Payment>())
                .Select(p => new PaymentRow
                {
                    PaymentDate = p.PaymentDate,
                    Method = p.Method,
                    Amount = p.Amount,
                    Status = p.Status
                }).ToList();

            dgvPayments.DataSource = new BindingList<PaymentRow>(payRows);
        }

        private void btnExportInvoice_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog
            {
                Filter = "PNG Image|*.png",
                FileName = "Invoice_" + DateTime.Now.ToString("yyyyMMdd_HHmm") + ".png"
            })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (var bmp = new Bitmap(Width, Height))
                    {
                        DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                        bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    MessageBox.Show("Đã xuất hóa đơn.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
