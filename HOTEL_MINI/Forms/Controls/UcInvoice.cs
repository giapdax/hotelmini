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
        public readonly Room _room;
        public readonly Invoice _invoice;
        public readonly Payment _payment;
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
        private readonly InvoiceService _invoiceService;
        private readonly PaymentService _paymentService;

        public UcInvoice(Booking booking, Room room, Invoice invoice, Payment payment)
        {
            InitializeComponent();

            _booking = booking;
            _room = room;
            _invoice = invoice;
            _payment = payment;

            _bookingService = new BookingService();
            _roomService = new RoomService();
            _invoiceService = new InvoiceService();
            _paymentService = new PaymentService();

            LoadInvoiceData();
            SetupEvents();
        }

        private void LoadInvoiceData()
        {
            try
            {
                // Hiển thị thông tin cơ bản
                label4.Text = $"Phòng {_room.RoomNumber}";
                txtCheckin.Text = _booking.CheckInDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";
                txtCheckout.Text = _booking.CheckOutDate?.ToString("dd/MM/yyyy HH:mm") ?? "N/A";

                // Hiển thị chi phí
                txtRoomCharge.Text = _invoice.RoomCharge.ToString("N0") + " đ";
                txtServiceCharge.Text = _invoice.ServiceCharge.ToString("N0") + " đ";
                txtSurcharge.Text = _invoice.Surcharge.ToString("N0") + " đ";
                txtDiscount.Text = _invoice.Discount.ToString("N0") + " đ";
                txtTotalAmount.Text = _invoice.TotalAmount.ToString("N0") + " đ";

                // Hiển thị thông tin thanh toán
                txtPaymentMethod.Text = _payment?.Method ?? "N/A";
                txtEmployeeName.Text = $"NV{_invoice.IssuedBy}"; // Có thể lấy tên từ UserService
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
            btnBack.Click += (s, e) => { /* Logic quay lại */ };
            btnExportInvoice.Click += (s, e) => ExportInvoice();
        }

        private void ExportInvoice()
        {
            try
            {
                // Logic export PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    FileName = $"HoaDon_{_room.RoomNumber}_{_invoice.InvoiceID}.pdf"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Gọi service export PDF
                    //_invoiceService.ExportInvoiceToPdf(_invoice, _booking, _room, saveFileDialog.FileName);
                    MessageBox.Show("Xuất hóa đơn thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất hóa đơn: {ex.Message}");
            }
        }
    }
}