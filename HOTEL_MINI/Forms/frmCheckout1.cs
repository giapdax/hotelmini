using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmCheckout1 : Form
    {
        private readonly int _bookingId;
        private readonly int _currentUserId;

        private readonly BookingService _bookingSvc = new BookingService();
        private readonly RoomService _roomSvc = new RoomService();

        private Booking _booking;
        private List<UsedServiceDto> _services = new List<UsedServiceDto>();

        private decimal _roomCharge = 0m;
        private decimal _serviceTotal = 0m;

        public frmCheckout1(int bookingId, int currentUserId)
        {
            InitializeComponent();
            _bookingId = bookingId;
            _currentUserId = currentUserId;

            this.Load += frmCheckout1_Load;
            btnConfirm.Click += btnConfirm_Click;
            btnCancel.Click += (s, e) => this.Close();
            txtDiscount.TextChanged += RecalcTotal;
            txtSurcharge.TextChanged += RecalcTotal;

            cbxPaymentMethod.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void frmCheckout1_Load(object sender, EventArgs e)
        {
            // 1) Load core booking
            _booking = _bookingSvc.GetBookingById(_bookingId);
            if (_booking == null)
            {
                MessageBox.Show("Không tìm thấy booking.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            if (!_booking.CheckOutDate.HasValue) _booking.CheckOutDate = DateTime.Now;

            // 2) Load thêm thông tin hiển thị
            var repo = new BookingRepository();
            var cust = repo.GetCustomerBasicById(_booking.CustomerID); // CustomerBasic (đã hướng dẫn đặt trong BookingRepository)
            var roomNo = repo.GetRoomNumberById(_booking.RoomID);
            var pricingType = _roomSvc.getPricingTypeByID(_booking.PricingID);

            // 3) Dịch vụ đã dùng
            _services = _bookingSvc.GetUsedServicesByBookingID(_bookingId) ?? new List<UsedServiceDto>();
            _serviceTotal = _services.Sum(s => s.Price * s.Quantity);

            // 4) Tiền phòng
            _roomCharge = _bookingSvc.GetRoomCharge(_booking);

            // 5) Bind UI
            txtCusName.Text = cust?.FullName ?? "";
            txtCusId.Text = cust?.IDNumber ?? "";
            txtCheckin.Text = _booking.CheckInDate?.ToString("dd/MM/yyyy HH:mm");
            txtCheckout.Text = _booking.CheckOutDate?.ToString("dd/MM/yyyy HH:mm");
            txtRoomCharge.Text = _roomCharge.ToString("N0");

            // Hình thức thanh toán
            cbxPaymentMethod.DataSource = _bookingSvc.getPaymentMethods();

            // Nhân viên: nếu bạn có tên user hiện tại, set ở đây
            // txtEmployeeName.Text = currentUserFullName;
            txtEmployeeName.Text = txtEmployeeName.Text; // giữ nguyên nếu để trống

            // Grid dịch vụ
            var srvRows = _services.Select(s => new
            {
                s.ServiceName,
                Price = s.Price,
                s.Quantity,
                Total = s.Price * s.Quantity
            }).ToList();

            dgvUsedService.AutoGenerateColumns = false;
            dgvUsedService.Columns.Clear();
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServiceName", HeaderText = "Dịch vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Đơn giá", Width = 90, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "SL", Width = 50 });
            dgvUsedService.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Total", HeaderText = "Thành tiền", Width = 110, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            dgvUsedService.DataSource = srvRows;
            txtServiceCharge.Text = _serviceTotal.ToString("N0");

            // Grid phòng (ở tableLayoutPanel1 / dataGridView1)
            dgvRoom.AutoGenerateColumns = false;
            dgvRoom.Columns.Clear();
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomNumber", HeaderText = "Phòng", Width = 80 });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PricingType", HeaderText = "Giá theo", Width = 90 });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CheckIn", HeaderText = "Check-in", Width = 130 });
            dgvRoom.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CheckOut", HeaderText = "Check-out", Width = 130 });

            dgvRoom.DataSource = new[]
            {
                new {
                    RoomNumber = roomNo,
                    PricingType = pricingType,
                    CheckIn = _booking.CheckInDate?.ToString("dd/MM/yyyy HH:mm"),
                    CheckOut = _booking.CheckOutDate?.ToString("dd/MM/yyyy HH:mm")
                }
            };

            // Tổng
            txtSurcharge.Text = "0";
            txtDiscount.Text = "0";
            RecalcTotal(null, null);
        }

        private void RecalcTotal(object sender, EventArgs e)
        {
            decimal.TryParse((txtSurcharge.Text ?? "0").Replace(",", ""), out var surcharge);
            decimal.TryParse((txtDiscount.Text ?? "0").Replace(",", ""), out var discount);
            var total = _roomCharge + _serviceTotal + surcharge - discount;
            if (total < 0) total = 0;
            txtTotalAmount.Text = total.ToString("N0");
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cbxPaymentMethod.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn hình thức thanh toán.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal.TryParse((txtSurcharge.Text ?? "0").Replace(",", ""), out var surcharge);
            decimal.TryParse((txtDiscount.Text ?? "0").Replace(",", ""), out var discount);
            var paymentMethod = cbxPaymentMethod.SelectedItem.ToString();

            try
            {
                // lấy checkout từ textbox nếu người dùng chỉnh
                if (DateTime.TryParse(txtCheckout.Text, out var co))
                    _booking.CheckOutDate = co;

                // Gọi checkout -> tạo Invoice + Payment, cập nhật trạng thái
                _bookingSvc.Checkout(_booking, _roomCharge, _serviceTotal, discount, surcharge, paymentMethod, _currentUserId);

                // ======= Hiện hóa đơn (UcInvoice1) trên form riêng =======
                using (var frm = new Form
                {
                    Text = "Hóa đơn",
                    StartPosition = FormStartPosition.CenterParent,
                    Width = 660,
                    Height = 780
                })
                {
                    var uc = new HOTEL_MINI.Forms.Controls.UcInvoice1 { Dock = DockStyle.Fill };
                    frm.Controls.Add(uc);

                    // Build viewmodel cho invoice
                    var roomRow = new HOTEL_MINI.Forms.Controls.UcInvoice1.RoomRow
                    {
                        RoomNumber = Convert.ToString(((dynamic)dgvRoom.DataSource)[0].RoomNumber),
                        PricingType = Convert.ToString(((dynamic)dgvRoom.DataSource)[0].PricingType),
                        CheckIn = Convert.ToString(((dynamic)dgvRoom.DataSource)[0].CheckIn),
                        CheckOut = Convert.ToString(((dynamic)dgvRoom.DataSource)[0].CheckOut)
                    };

                    var serviceRows = (_services ?? new List<UsedServiceDto>())
                        .Select(s => new HOTEL_MINI.Forms.Controls.UcInvoice1.InvoiceServiceRow
                        {
                            ServiceName = s.ServiceName,
                            Price = s.Price,
                            Quantity = s.Quantity
                        })
                        .ToList();

                    var vm = new HOTEL_MINI.Forms.Controls.UcInvoice1.InvoiceVm
                    {
                        CustomerName = txtCusName.Text,
                        CustomerIdNumber = txtCusId.Text,
                        CheckIn = _booking.CheckInDate ?? DateTime.MinValue,
                        CheckOut = _booking.CheckOutDate ?? DateTime.Now,
                        RoomCharge = _roomCharge,
                        ServiceCharge = _serviceTotal,
                        Discount = discount,
                        Surcharge = surcharge,
                        Total = (_roomCharge + _serviceTotal + surcharge - discount) < 0 ? 0 : (_roomCharge + _serviceTotal + surcharge - discount),
                        EmployeeName = txtEmployeeName.Text,
                        PaymentMethod = paymentMethod,
                        Note = txtNote.Text
                    };

                    uc.BindFrom(vm, serviceRows, new[] { roomRow });
                    frm.ShowDialog(this);
                }

                MessageBox.Show("Thanh toán thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thanh toán thất bại: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
