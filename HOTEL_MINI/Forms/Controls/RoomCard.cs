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
    public partial class RoomCard : UserControl
    {
        private readonly Room _room;
        private readonly frmApplication _form1;
        private readonly frmRoom _frmRoom;
        private readonly BookingService _bookingService;
        private readonly RoomService _roomService;
        private readonly Booking _booking;
        public RoomCard(frmApplication frmApplication, Room room, frmRoom frmRoom)//au tạo constructor thì thêm Form1 form1,
        {
            InitializeComponent();
            _form1 = frmApplication;
            _room = room;
            _frmRoom = frmRoom;
            _bookingService = new BookingService();
            _roomService = new RoomService();
            _booking = new Booking();
            LoadUiRoomCard();

        }
        public void LoadUiRoomCard()
        {
            lblRoomNumber.Text = $"{_room.RoomNumber}";
            var roomStatus = _room.RoomStatus;
            lblRoomStatus.Text = roomStatus;

            // reset màu mặc định
            this.BackColor = Color.Gray;

            if (roomStatus == "Available")
            {
                this.BackColor = Color.LightGreen;
                btnBook.Text = "Đặt Phòng";
                btnDetails.Visible = false;
                btnBook.Visible = true;
            }
            else if (roomStatus == "Booked")
            {
                this.BackColor = Color.Yellow;
                btnBook.Text = "Nhận phòng";
                btnDetails.Visible = true;
                btnBook.Visible = true;

                // Lấy booking gần nhất cho phòng này
                var booking = _bookingService.GetLatestBookingByRoomId(_room.RoomID);

                if (booking != null && booking.CheckInDate.HasValue)
                {
                    var timeToCheckin = booking.CheckInDate.Value - DateTime.Now;
                    if (timeToCheckin.TotalHours <= 1 && timeToCheckin.TotalHours >= 0)
                    {
                        this.BackColor = Color.Red; // sắp tới giờ checkin
                    }
                }
            }
            else if (roomStatus == "Occupied")
            {
                this.BackColor = Color.Blue;
                btnBook.Visible = false;
                btnDetails.Visible = true;

                var booking = _bookingService.GetLatestBookingByRoomId(_room.RoomID);

                if (booking != null && booking.CheckOutDate.HasValue)
                {
                    var timeToCheckout = booking.CheckOutDate.Value - DateTime.Now;
                    if (timeToCheckout.TotalHours <= 1 && timeToCheckout.TotalHours >= 0)
                    {
                        this.BackColor = Color.Orange; // sắp đến giờ checkout
                    }
                }
            }
            else
            {
                this.BackColor = Color.Gray;
                btnBook.Visible = false;
                btnDetails.Visible = false;
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (_room.RoomStatus == "Available")
            {
                frmBookingPopup bookingPopup = new frmBookingPopup(_form1, _room, _frmRoom);
                bookingPopup.ShowDialog();
            }
            else
            {   
                
                updateStatus();
            }
        }
        private void updateStatus()  // khi Roomstatus == Booked thì btnBook tương đương với nhận phòng, nhấn vào sẽ cập nhất Roomtstua và Booking status`
        {
            // Khi Room đang "Booked" -> người dùng nhấn "Nhận phòng"
            if (_room.RoomStatus == "Booked")
            {
                // 1. Update booking status thành CheckedIn
                var latestBooking = _bookingService.GetLatestBookingByRoomId(_room.RoomID);
                if (latestBooking != null)
                {
                    // 2. Update room status thành Occupied                                
                    _room.RoomStatus = "Occupied";
                    
                    var resultRoomStatus = _roomService.UpdateRoomStatus(_room.RoomID, "Occupied");
                    if (!resultRoomStatus)
                    {
                        MessageBox.Show("Cập nhật trạng thái phòng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    latestBooking.Status = "CheckedIn";
                    _bookingService.UpdateBooking(latestBooking);
                    // 3. Refresh lại form chính
                    _frmRoom.RefreshRoomList();

                    MessageBox.Show("Nhận phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy booking nào cho phòng này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if(_room.RoomStatus == "Occupied")
            {
                var latestBooking = _bookingService.GetLatestBookingByRoomId(_room.RoomID);
                if (latestBooking != null)
                {
                    _form1.OpenChildForm(new frmBookingDetail(latestBooking, _room, _form1), btnDetails); //_form1, latestBooking, _frmRoom
                    //frmBookingDetail bookingDetails = new frmBookingDetail(latestBooking, _room); //_form1, latestBooking, _frmRoom
                    
                    //bookingDetails.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy booking nào cho phòng này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {

            }
        }
    }
}
