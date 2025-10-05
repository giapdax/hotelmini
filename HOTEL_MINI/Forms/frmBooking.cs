using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HOTEL_MINI.Forms.Controls;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Response;

namespace HOTEL_MINI.Forms
{
    public partial class frmBooking : Form
    {
        private int _currentUserId;
        private User _currentUser;

        private UcBookRoom _ucBookRoom;
        private UcBookingRoom _ucBookingRoom;
        
        public frmBooking()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Load += (s, e) => EnsureTabContent();
            this.tabBooking.SelectedIndexChanged += (s, e) => EnsureTabContent();
        }

        public frmBooking(int currentUserId) : this() { _currentUserId = currentUserId; }
        public frmBooking(User user) : this() { _currentUser = user; _currentUserId = user?.UserID ?? 0; }
        public frmBooking(global::HOTEL_MINI.frmApplication app) : this()
        {
            try { var u = app?.GetCurrentUser(); _currentUser = u; _currentUserId = u?.UserID ?? 0; }
            catch { _currentUserId = 0; }
        }

        public int CurrentUserId => _currentUserId;
        public User CurrentUser => _currentUser;
        
        private static bool IsOccupiedLike(string status)
            => string.Equals(status, "Occupied", StringComparison.OrdinalIgnoreCase)
            || string.Equals(status, "CheckedIn", StringComparison.OrdinalIgnoreCase)
            || string.Equals(status, "Đang ở", StringComparison.OrdinalIgnoreCase);

        private void EnsureTabContent()
        {
            if (tabBooking == null || tabBooking.SelectedTab == null) return;
            var current = tabBooking.SelectedTab;

            // Tab Đặt phòng
            if (current == btnBookRoom)
            {
                if (_ucBookRoom == null)
                {
                    _ucBookRoom = new UcBookRoom
                    {
                        Dock = DockStyle.Fill,
                        CurrentUserId = _currentUserId
                    };
                    btnBookRoom.Controls.Clear();
                    btnBookRoom.Controls.Add(_ucBookRoom);
                }
                else
                {
                    _ucBookRoom.CurrentUserId = _currentUserId;
                }
            }
            // Tab Quản lý booking/phòng
            else if (current == btnBookingRoom)
            {
                if (_ucBookingRoom == null)
                {
                    var bookingSvc = new BookingService();
                    var roomSvc = new RoomService();
                    var roomRepo = new RoomRepository();
                    var bookingRepo = new BookingRepository();

                    _ucBookingRoom = new UcBookingRoom { Dock = DockStyle.Fill };

                    // ===== BÊN TRÁI: tìm booking theo CCCD + trạng thái =====
                    _ucBookingRoom.FetchBookingsByCustomer = (cccd, statusFilter) =>
                    {
                        List<BookingDisplay> src =
                            string.IsNullOrWhiteSpace(cccd)
                                ? bookingSvc.GetTop20LatestBookingDisplays()
                                : bookingSvc.GetBookingDisplaysByCustomerNumber(cccd.Trim());

                        IEnumerable<BookingDisplay> q = src;

                        if (string.Equals(statusFilter, "BookedOrOccupied", StringComparison.OrdinalIgnoreCase))
                        {
                            q = q.Where(x =>
                                string.Equals(x.Status, "Booked", StringComparison.OrdinalIgnoreCase) ||
                                IsOccupiedLike(x.Status));
                        }
                        else if (!string.IsNullOrWhiteSpace(statusFilter))
                        {
                            if (string.Equals(statusFilter, "Đang ở", StringComparison.OrdinalIgnoreCase))
                                q = q.Where(x => IsOccupiedLike(x.Status));
                            else
                                q = q.Where(x => string.Equals(x.Status, statusFilter, StringComparison.OrdinalIgnoreCase));
                        }

                        return q
                            .OrderByDescending(x => x.CheckInDate ?? x.BookingDate)
                            .Select(x => new UcBookingRoom.BookingRow
                            {
                                BookingID = x.BookingID,
                                CustomerName = "",                    // cột này repo chưa trả, để trống
                                IDNumber = string.IsNullOrWhiteSpace(cccd) ? "" : cccd,
                                RoomNumber = x.RoomNumber,
                                CheckIn = x.CheckInDate ?? x.BookingDate,
                                CheckOut = x.CheckOutDate ?? x.BookingDate,
                                Status = x.Status              // giữ nguyên tiếng Anh để logic nút nhận diện
                            })
                            .ToList();
                    };

                    // ===== BÊN PHẢI: theo số phòng + trạng thái phòng =====
                    _ucBookingRoom.FetchBookingsByRoom = (roomNo, roomStatusFilter) =>
                    {
                        var rooms = roomRepo.getRoomList() ?? new List<Room>();

                        // lọc theo số phòng gõ (contains)
                        if (!string.IsNullOrWhiteSpace(roomNo))
                            rooms = rooms.Where(r => (r.RoomNumber ?? "").IndexOf(roomNo.Trim(), StringComparison.OrdinalIgnoreCase) >= 0).ToList();

                        var rows = new List<UcBookingRoom.RoomBookingRow>();

                        foreach (var r in rooms.OrderBy(x => x.RoomNumber))
                        {
                            // booking đang hoạt động (Booked/CheckedIn) nếu có
                            var active = bookingSvc.GetActiveBookingByRoomId(r.RoomID);
                            string roomStatus = r.RoomStatus; // Available / Booked / Occupied ...

                            // Nếu muốn dựa trên booking: override
                            if (active != null)
                            {
                                if (string.Equals(active.Status, "Booked", StringComparison.OrdinalIgnoreCase))
                                    roomStatus = "Booked";
                                else if (IsOccupiedLike(active.Status))
                                    roomStatus = "Occupied";
                            }
                            else
                            {
                                // Không có booking active -> nếu DB chưa cập nhật thì vẫn ưu tiên 'Available'
                                if (!string.Equals(roomStatus, "Available", StringComparison.OrdinalIgnoreCase))
                                    roomStatus = "Available";
                            }

                            // Lọc theo combobox trạng thái phòng
                            string normFilter = null;
                            if (!string.IsNullOrWhiteSpace(roomStatusFilter) && roomStatusFilter != "(Tất cả)")
                            {
                                if (string.Equals(roomStatusFilter, "Đang ở", StringComparison.OrdinalIgnoreCase))
                                    normFilter = "Occupied";
                                else if (string.Equals(roomStatusFilter, "Trống", StringComparison.OrdinalIgnoreCase))
                                    normFilter = "Available";
                                else
                                    normFilter = roomStatusFilter; // Booked
                            }

                            if (normFilter == null || string.Equals(roomStatus, normFilter, StringComparison.OrdinalIgnoreCase))
                            {
                                rows.Add(new UcBookingRoom.RoomBookingRow
                                {
                                    RoomNumber = r.RoomNumber,
                                    BookingID = active?.BookingID ?? 0,
                                    RoomStatus = roomStatus,
                                    CustomerName = "", // nếu cần thì JOIN thêm để có tên khách
                                    CheckIn = active?.CheckInDate,
                                    CheckOut = active?.CheckOutDate
                                });
                            }
                        }

                        return rows;
                    };

                    // ===== Nhận phòng =====
                    _ucBookingRoom.CheckInBooking = (bookingId) =>
                    {
                        try
                        {
                            return bookingSvc.CheckInBooking(bookingId);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Nhận phòng lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    };

                    // ===== Hủy booking =====
                    _ucBookingRoom.CancelBooking = (bookingId) =>
                    {
                        try
                        {
                            return bookingSvc.CancelBooking(bookingId);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hủy booking lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    };

                    // ===== Trả phòng (mặc định mở form chi tiết để tính hoá đơn) =====
                    _ucBookingRoom.OpenBookingDetail = (bookingId) =>
                    {
                        // Lấy dữ liệu cần thiết để mở form chi tiết. Ở đây demo mở form rỗng
                        // (bạn có thể nạp SelectedRoomWithTime, plans, used services tuỳ ý).
                        using (var f = new frmBookingDetail1(
                            new List<SelectedRoomWithTime>(),
                            new Dictionary<int, frmBookingDetail1.RoomPlan>()))
                        {
                            f.StartPosition = FormStartPosition.CenterParent;
                            f.ShowDialog(this);
                        }
                    };

                    // (Tuỳ chọn) Nếu muốn cho phép “Trả phòng thẳng” (không mở form):
                    _ucBookingRoom.CheckoutBooking = (bookingId) =>
                    {
                        // Ở đây mình để false để UI ưu tiên đường OpenBookingDetail.
                        return false;
                    };

                    btnBookingRoom.Controls.Clear();
                    btnBookingRoom.Controls.Add(_ucBookingRoom);
                }
            }
        }
    }
}
