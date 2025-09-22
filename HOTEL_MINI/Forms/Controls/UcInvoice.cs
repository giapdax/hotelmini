using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms.Controls
{
    public partial class UcInvoice : UserControl
    {
        public readonly Booking _booking;
        public readonly string _roomNumber;
        public readonly Invoice _invoice;
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
        private readonly InvoiceService _invoiceService;
        private readonly PdfExportService _pdfExportService;

        public UcInvoice(Booking booking, string roomNumber, Invoice invoice)
        {
            InitializeComponent();

            _booking = booking;
            _roomNumber = roomNumber;
            _invoice = invoice;

            _bookingService = new BookingService();
            _roomService = new RoomService();
            _invoiceService = new InvoiceService();
            _pdfExportService = new PdfExportService();
            LoadInvoiceData();
            SetupEvents();
        }

        private void LoadInvoiceData()
        {
            try
            {
                // Hiển thị thông tin cơ bản
                label4.Text = $"Phòng {_roomNumber}";
                txtCheckin.Text = _booking.CheckInDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
                txtCheckout.Text = _booking.CheckOutDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";

                // Hiển thị chi phí
                txtRoomCharge.Text = _invoice.RoomCharge.ToString("N0") + " đ";
                txtServiceCharge.Text = _invoice.ServiceCharge.ToString("N0") + " đ";
                txtSurcharge.Text = _invoice.Surcharge.ToString("N0") + " đ";
                txtDiscount.Text = _invoice.Discount.ToString("N0") + " đ";
                txtTotalAmount.Text = _invoice.TotalAmount.ToString("N0") + " đ";

                // Hiển thị thông tin thanh toán
                txtPaymentMethod.Text = _invoiceService.GetPaymentByInvoiceID(_invoice.InvoiceID);
                txtEmployeeName.Text = _invoiceService.getFullNameByInvoiceID(_invoice.InvoiceID); // Có thể lấy tên từ UserService
                txtNote.Text = _invoice.Note;

                // Load dịch vụ đã sử dụng
                LoadUsedServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải hóa đơn: {ex.Message}");
            }
        }

        private void LoadUsedServices()
        {
            var usedServices = _bookingService.GetUsedServicesByBookingID(_booking.BookingID);
            dgvUsedService.DataSource = null;
            dgvUsedService.AutoGenerateColumns = false;
            dgvUsedService.Columns.Clear();

            // Tên dịch vụ
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ServiceName",
                HeaderText = "Tên dịch vụ"
            });

            // Số lượng
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Số lượng"
            });

            // Tổng tiền
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalFormatted",
                HeaderText = "Thành tiền"
            });

            dgvUsedService.DataSource = usedServices;
        }

        private void SetupEvents()
        {
            btnBack.Click += (s, e) =>
            {
                // Tìm form cha và đóng nó
                var parentForm = this.Parent as Form;
                parentForm?.Close();
            };

            btnExportInvoice.Click += (s, e) => ExportInvoice();
        }

        private void ExportInvoice()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    FileName = $"HoaDon_{_roomNumber}_{_invoice.InvoiceID}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Lấy danh sách dịch vụ đã sử dụng
                    var usedServices = _bookingService.GetUsedServicesByBookingID(_booking.BookingID);

                    // Export PDF
                    _pdfExportService.ExportInvoiceToPdf(_invoice, _booking, _roomNumber, usedServices, saveFileDialog.FileName);

                    MessageBox.Show("Xuất hóa đơn thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở file sau khi export (tuỳ chọn)
                    System.Diagnostics.Process.Start(saveFileDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}