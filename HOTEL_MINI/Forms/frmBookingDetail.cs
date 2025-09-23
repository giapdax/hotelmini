using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmBookingDetail : Form
    {
        private readonly frmApplication _form1;
        private readonly frmRoom _frmRoom;
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
        private readonly CustomerService _customerService;
        private readonly Booking _booking;
        private readonly Room _room;
        private readonly ServicesService _servicesService;
        public frmBookingDetail(Booking booking, Room room, frmApplication frmApplication)
        {
            InitializeComponent();
            _booking = booking;
            _roomService = new RoomService();
            _customerService = new CustomerService();   
            _bookingService = new BookingService();
            _room = room;
            _servicesService = new ServicesService();
            _form1 = frmApplication;
            LoadInforBooking();
            LoadServicesToGrid();
            LoadUsedServicesToGrid();
        }

        private void LoadInforBooking()
        {
            lblRoomNumber.Text = _room.RoomNumber;
            var customer = _customerService.getCustomerByCustomerID(_booking.CustomerID);
            if (customer != null)
            {
                txtTen.Text = customer.FullName;
                txtCCCD.Text = customer.IDNumber;
                txtSDT.Text = customer.Phone;
                txtEmail.Text = customer.Email;
                txtDiachi.Text = customer.Address;
                txtGender.Text = customer.Gender;
            }
            else 
            {
                MessageBox.Show("Khách hàng không tồn tại trong hệ thống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            txtPricingType.Text = _roomService.getPricingTypeByID(_booking.PricingID);
            dtpCheckinTime.Value = _booking.CheckInDate.HasValue ? _booking.CheckInDate.Value : DateTime.Now;
            txtNote.Text = _booking.Notes;
            txtNote.ReadOnly = true;    
            if (_booking.CheckOutDate == null)
            {
                //dtpCheckoutTime.Value = DateTime.Now;
                //dtpCheckoutTime.Enabled = true;
                dtpCheckoutTime.Visible = false;
                lblCHECKOUT.Visible = false;
            }
            else
            {
                dtpCheckoutTime.Value = _booking.CheckOutDate.Value;
            }
            //dtpCheckoutTime.Value = _booking.CheckOutDate.HasValue ? _booking.CheckInDate.Value : DateTime.Now;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            var user = _form1.GetCurrentUser();
            _form1.OpenChildForm(new frmRoom(_form1), btnClose);
        }
        private void LoadServicesToGrid()
        {
            var services = _servicesService.GetAllServices();

            dgvHotelServices.DataSource = null;
            dgvHotelServices.AutoGenerateColumns = false; // không tự tạo tất cả cột

            dgvHotelServices.Columns.Clear();

            // Cột tên dịch vụ
            var colName = new DataGridViewTextBoxColumn();
            colName.DataPropertyName = "ServiceName"; // trỏ tới property của object
            colName.HeaderText = "Tên dịch vụ";
            dgvHotelServices.Columns.Add(colName);

            // Cột giá
            var colPrice = new DataGridViewTextBoxColumn();
            colPrice.DataPropertyName = "PriceFormatted";
            colPrice.HeaderText = "Giá";
            dgvHotelServices.Columns.Add(colPrice);
            // Cột số lượng
            var colQuantity = new DataGridViewTextBoxColumn();
            colQuantity.DataPropertyName = "Quantity";
            colQuantity.HeaderText = "Số lượng";
            dgvHotelServices.Columns.Add(colQuantity);

            // Gán data source
            dgvHotelServices.DataSource = services;
            lblAddQuantity.Enabled = false;
            nbrIncrease.Enabled = false;
            nbrIncrease.Value = 0;
            btnIncrease.Enabled = false;
        }
        private void LoadUsedServicesToGrid()
        {
            var usedServices = _bookingService.GetUsedServicesByBookingID(_booking.BookingID);

            dgvUsedServices.DataSource = null;
            dgvUsedServices.AutoGenerateColumns = false;
            dgvUsedServices.Columns.Clear();

            // Tên dịch vụ
            dgvUsedServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ServiceName",
                HeaderText = "Tên dịch vụ"
            });

            // Giá
            dgvUsedServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "PriceFormatted",
                HeaderText = "Giá"
            });

            // Số lượng
            dgvUsedServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Số lượng"
            });

            // Tổng tiền
            dgvUsedServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalFormatted",
                HeaderText = "Thành tiền"
            });

            dgvUsedServices.DataSource = usedServices;
            lblReduceQuantity.Enabled = false;
            nbrReduce.Enabled = false;
            nbrReduce.Value = 0;
            btnReduce.Enabled = false;
        }

        //private Service GetSelectedService()
        //{
        //    if (dgvHotelServices.CurrentRow != null)
        //    {
        //        return dgvHotelServices.CurrentRow.DataBoundItem as Service;
        //    }
        //    return null;
        //}

        private void dgvHotelServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gbxRoomInfor_Enter(object sender, EventArgs e)
        {

        }

        private void dgvHotelServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // tránh click header
            {
                var selectedService = (Service)dgvHotelServices.Rows[e.RowIndex].DataBoundItem;

                // Gán vào label, numericUpDown,...
                //lblAddQuantity.Text = $"Dịch vụ: {selectedService.ServiceName}";
                nbrIncrease.Value = 1;

                lblAddQuantity.Enabled = true;
                nbrIncrease.Enabled = true;
                btnIncrease.Enabled = true;
            }
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            if (dgvHotelServices.CurrentRow == null || (int)nbrIncrease.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ và số lượng hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedService = (Service)dgvHotelServices.CurrentRow.DataBoundItem;
            int addQuantity = (int)nbrIncrease.Value;

            // Kiểm tra tồn kho
            if (addQuantity > selectedService.Quantity)
            {
                MessageBox.Show(
                    $"Dịch vụ {selectedService.ServiceName} chỉ còn {selectedService.Quantity} cái, không thể đặt {addQuantity}!",
                    "Không đủ số lượng",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Thêm/cập nhật dịch vụ cho booking
            _bookingService.AddOrUpdateServiceForBooking(_booking.BookingID, selectedService.ServiceID, addQuantity);

            // Cập nhật lại tồn kho trong bảng Services
            _servicesService.UpdateServiceQuantity(selectedService.ServiceID,selectedService.Quantity - addQuantity);

            MessageBox.Show($"Đã thêm {addQuantity} {selectedService.ServiceName} cho booking {_booking.BookingID}");

            // Refresh lại grid hiển thị
            LoadServicesToGrid();      // danh sách dịch vụ khách sạn
            LoadUsedServicesToGrid();  // dịch vụ đã chọn
        }

        private void dgvUsedServices_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // tránh click header
            {
                var selectedService = (UsedServiceDto)dgvUsedServices.Rows[e.RowIndex].DataBoundItem;

                // Gán vào label, numericUpDown,...
                //lblAddQuantity.Text = $"Dịch vụ: {selectedService.ServiceName}";
                nbrReduce.Value = 1;

                lblReduceQuantity.Enabled = true;
                nbrReduce.Enabled = true;
                btnReduce.Enabled = true;
            }
        }

        private void btnReduce_Click(object sender, EventArgs e)
        {
            if (dgvUsedServices.CurrentRow == null || (int)nbrReduce.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ và nhập số lượng hợp lệ!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selected = (UsedServiceDto)dgvUsedServices.CurrentRow.DataBoundItem;
            int reduceQuantity = (int)nbrReduce.Value;

            if (reduceQuantity > selected.Quantity)
            {
                MessageBox.Show("Số lượng giảm lớn hơn số lượng đang dùng!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (reduceQuantity == selected.Quantity)
            {
                // Trả lại toàn bộ vào kho
                _servicesService.UpdateServiceQuantity(
                    selected.ServiceID,
                    _servicesService.GetQuantity(selected.ServiceID) + reduceQuantity
                );

                // Xóa khỏi bảng BookingServices
                _bookingService.RemoveServiceFromBooking(selected.BookingServiceID);

                MessageBox.Show($"Đã xóa dịch vụ {selected.ServiceName} khỏi booking");
            }
            else
            {
                // Giảm quantity trong BookingServices
                int newQuantity = selected.Quantity - reduceQuantity;
                _bookingService.UpdateServiceQuantity(selected.BookingServiceID, newQuantity);

                // Trả lại vào kho số lượng đã giảm
                _servicesService.UpdateServiceQuantity(
                    selected.ServiceID,
                    _servicesService.GetQuantity(selected.ServiceID) + reduceQuantity
                );

                MessageBox.Show($"Đã giảm {reduceQuantity} {selected.ServiceName}");
            }

            // Refresh lại grid
            LoadUsedServicesToGrid();
            LoadServicesToGrid();
        }

        private void btnTraphong_Click(object sender, EventArgs e)
        {
            try
            {
                if (_booking == null || _room == null)
                {
                    MessageBox.Show("Dữ liệu phòng hoặc booking bị thiếu!");
                    return;
                }

                if (_booking.CheckOutDate != null)
                {
                    // Đã có giờ checkout, mở thẳng form thanh toán
                    OpenCheckoutForm();
                }
                else
                {
                    //MessageBox.Show($"Mở phòng {_room.RoomNumber}!");
                    // Chưa có giờ checkout, mở form chọn giờ trước
                    OpenCheckoutTimeForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thực hiện trả phòng: {ex.Message}",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenCheckoutForm()
        {
            using (var frmCheckout = new frmCheckout(_room, _booking, _form1))
            {
                    var result=frmCheckout.ShowDialog();
                if (result == DialogResult.Cancel)
                {
                   // _booking.CheckOutDate = null;
                }
                else if (result == DialogResult.OK)
                {
                    // Checkout thành công - cập nhật UI
                    MessageBox.Show("Trả phòng thành công!", "Thành công",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Gọi method refresh lại danh sách phòng ở form chính
                    //_form1.RefreshRoomList();

                    _form1.OpenChildForm(new frmRoom(_form1), btnClose);
                    this.Close();
                }
            }
        }

        private void OpenCheckoutTimeForm()
        {
            using (var frmTime = new frmSelectCheckoutTime(_room, _booking, _form1))
            {
                if (frmTime.ShowDialog() == DialogResult.OK)
                {
                    using (var frmCheckout = new frmCheckout(_room, _booking, _form1))
                    {
                        var result = frmCheckout.ShowDialog();

                        if (result == DialogResult.Cancel)
                        {
                            _booking.CheckOutDate = null;
                        }
                        else if (result == DialogResult.OK)
                        {
                            // Checkout thành công - cập nhật UI
                            MessageBox.Show("Trả phòng thành công!", "Thành công",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Gọi method refresh lại danh sách phòng ở form chính
                            //_form1.RefreshRoomList();

                            _form1.OpenChildForm(new frmRoom(_form1), btnClose);
                            this.Close();
                        }
                    }
                }
            }
        }
    }
}
