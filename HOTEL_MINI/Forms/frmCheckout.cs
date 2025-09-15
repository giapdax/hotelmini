using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmCheckout : Form
    {
        private readonly Room _room;
        private readonly Booking _booking;
        private frmApplication _frmApplication;
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;

        public frmCheckout(Room room, Booking booking, frmApplication frmApplication)
        {
            InitializeComponent();
            _bookingService = new BookingService();
            _roomService = new RoomService();
            _room = room;
            _booking = booking;
            _frmApplication = frmApplication;
            txtSurcharge.KeyPress += txtSurcharge_KeyPress;
            txtDiscount.KeyPress += txtDiscount_KeyPress; // Sửa từ txtSurcharge_KeyPress thành txtDiscount_KeyPress

            LoadInfor();
        }

        private Decimal roomCharge()
        {
            var roomCharge = _bookingService.GetRoomCharge(_booking);
            return roomCharge;
        }

        private Decimal serviceCharge()
        {
            var listUsedServices = _bookingService.GetUsedServicesByBookingID(_booking.BookingID);
            decimal totalServiceCharge = 0;
            foreach (var service in listUsedServices)
            {
                totalServiceCharge += service.Total;
            }
            return totalServiceCharge;
        }

        private void txtSurcharge_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalAmount();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            decimal room = roomCharge();
            decimal service = serviceCharge();

            decimal surcharge = 0;
            decimal discount = 0;

            // Xử lý surcharge - chỉ parse nếu không rỗng
            if (!string.IsNullOrWhiteSpace(txtSurcharge.Text) &&
                !decimal.TryParse(txtSurcharge.Text, out surcharge))
            {
                MessageBox.Show("Phụ phí phải là số hợp lệ!");
                txtSurcharge.Text = "";
                surcharge = 0;
            }

            // Xử lý discount - chỉ parse nếu không rỗng
            if (!string.IsNullOrWhiteSpace(txtDiscount.Text) &&
                !decimal.TryParse(txtDiscount.Text, out discount))
            {
                MessageBox.Show("Giảm giá phải là số hợp lệ!");
                txtDiscount.Text = "";
                discount = 0;
            }

            decimal total = room + service + surcharge - discount;
            if (total < 0) total = 0;

            txtTotalAmount.Text = total.ToString("N0") + " đ";
        }

        private void txtSurcharge_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép số, phím Backspace và dấu chấm thập phân
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Chỉ được nhập số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Nếu đã có dấu chấm rồi thì không cho nhập thêm
            TextBox txt = sender as TextBox;
            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép số, phím Backspace và dấu chấm thập phân
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
                MessageBox.Show("Chỉ được nhập số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Nếu đã có dấu chấm rồi thì không cho nhập thêm
            TextBox txt = sender as TextBox;
            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void txtSurcharge_Leave(object sender, EventArgs e)
        {
            // Nếu không nhập gì hoặc nhập chuỗi rỗng thì cho phép
            if (string.IsNullOrWhiteSpace(txtSurcharge.Text))
            {
                txtSurcharge.Text = ""; // Giữ nguyên là rỗng
                return;
            }

            // Nếu có nhập thì phải là số hợp lệ
            if (!decimal.TryParse(txtSurcharge.Text, out decimal result))
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSurcharge.Focus();
                txtSurcharge.Text = ""; // Xóa nếu nhập không hợp lệ
            }
            else if (result < 0)
            {
                MessageBox.Show("Phụ phí không được âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSurcharge.Focus();
                txtSurcharge.Text = "";
            }
        }

        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            // Nếu không nhập gì hoặc nhập chuỗi rỗng thì cho phép
            if (string.IsNullOrWhiteSpace(txtDiscount.Text))
            {
                txtDiscount.Text = ""; // Giữ nguyên là rỗng
                return;
            }

            // Nếu có nhập thì phải là số hợp lệ
            if (!decimal.TryParse(txtDiscount.Text, out decimal result))
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDiscount.Focus();
                txtDiscount.Text = ""; // Xóa nếu nhập không hợp lệ
            }
            else if (result < 0)
            {
                MessageBox.Show("Giảm giá không được âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDiscount.Focus();
                txtDiscount.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                decimal surcharge = string.IsNullOrWhiteSpace(txtSurcharge.Text) ? 0 : Convert.ToDecimal(txtSurcharge.Text);
                decimal discount = string.IsNullOrWhiteSpace(txtDiscount.Text) ? 0 : Convert.ToDecimal(txtDiscount.Text);

                // Kiểm tra nếu số âm (mặc dù đã check ở Leave nhưng check lại cho chắc)
                if (surcharge < 0 || discount < 0)
                {
                    MessageBox.Show("Phụ phí và giảm giá không được âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal room = roomCharge();
                decimal service = serviceCharge();
                decimal totalAmount = room + service + surcharge - discount;

                // Gọi service để checkout
                _bookingService.Checkout(
                    _booking.BookingID,
                    _booking.RoomID,
                    room,
                    service,
                    discount,
                    surcharge,
                    cbxPaymentMethod.SelectedItem?.ToString() ?? "Cash",
                    _frmApplication.GetCurrentUser().UserID
                );

                MessageBox.Show("Checkout thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi checkout: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUsedServicesToGrid()
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

        private void LoadInfor()
        {
            lblRoomNumber.Text = _room.RoomNumber;
            txtCheckin.Text = _booking.CheckInDate.Value.ToString("dd/MM/yyyy HH:mm");
            txtCheckout.Text = _booking.CheckOutDate.Value.ToString("dd/MM/yyyy HH:mm") ?? DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            txtRoomCharge.Text = roomCharge().ToString("N0") + " đ";
            LoadUsedServicesToGrid();
            txtServiceCharge.Text = serviceCharge().ToString("N0") + " đ";
            UpdateTotalAmount();
            txtEmployeeName.Text = _frmApplication.GetCurrentUser().FullName;

            var paymentMethods = _bookingService.getPaymentMethods();
            foreach (var method in paymentMethods)
            {
                cbxPaymentMethod.Items.Add(method);
            }

            if (cbxPaymentMethod.Items.Count > 0)
            {
                cbxPaymentMethod.SelectedIndex = 0;
            }
        }

        // Các phương thức event handler rỗng (nếu cần)
        private void label1_Click(object sender, EventArgs e) { }
        private void frmCheckout_Load(object sender, EventArgs e) { }
    }
}