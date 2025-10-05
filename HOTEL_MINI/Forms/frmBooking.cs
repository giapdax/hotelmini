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
        // ===== Current user =====
        private int _currentUserId;
        private User _currentUser;

        // ===== Child controls on tabs =====
        private UcBookRoom _ucBookRoom;
        private UcBookingRoom _ucBookingRoom;

        // ===== Ctors =====
        public frmBooking()
        {
            InitializeComponent();

            // smoother UI
            this.DoubleBuffered = true;

            // lazy-load content per tab
            this.Load += (s, e) => EnsureTabContent();
            if (this.tabBooking != null)
                this.tabBooking.SelectedIndexChanged += (s, e) => EnsureTabContent();
        }

        public frmBooking(int currentUserId) : this()
        {
            _currentUserId = currentUserId;
        }

        public frmBooking(User user) : this()
        {
            _currentUser = user;
            _currentUserId = user?.UserID ?? 0;
        }

        public frmBooking(global::HOTEL_MINI.frmApplication app) : this()
        {
            try
            {
                var u = app?.GetCurrentUser();
                _currentUser = u;
                _currentUserId = u?.UserID ?? 0;
            }
            catch
            {
                _currentUserId = 0;
            }
        }

        public int CurrentUserId => _currentUserId;
        public User CurrentUser => _currentUser;

        // =========================================================
        // Load nội dung cho từng Tab (lazy)
        //  - btnBookRoom: chứa UcBookRoom
        //  - btnBookingRoom: chứa UcBookingRoom (quản lý/ check-in/ check-out)
        // =========================================================
        private void EnsureTabContent()
        {
            if (tabBooking == null || tabBooking.SelectedTab == null) return;
            var current = tabBooking.SelectedTab;

            // --- Tab ĐẶT PHÒNG ---
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
            // --- Tab QUẢN LÝ / CHECK-IN / CHECK-OUT ---
            else if (current == btnBookingRoom)
            {
                if (_ucBookingRoom == null)
                {
                    _ucBookingRoom = new UcBookingRoom
                    {
                        Dock = DockStyle.Fill,
                        CurrentUserId = _currentUserId // để fallback mở frmCheckout1 khi cha không gắn handler
                    };

                    // GẮN 2 handler để bắt nút "Trả phòng" trong control con
                    _ucBookingRoom.RequestCheckout += OnRequestCheckout;           // một phòng
                    _ucBookingRoom.RequestCheckoutMany += OnRequestCheckoutMany;   // nhiều phòng

                    btnBookingRoom.Controls.Clear();
                    btnBookingRoom.Controls.Add(_ucBookingRoom);
                }
                else
                {
                    _ucBookingRoom.CurrentUserId = _currentUserId;
                }
            }
        }

        // =========================================================
        // HANDLERS từ UcBookingRoom
        // =========================================================

        // Người dùng chọn 1 BookingRoomID để trả phòng
        private void OnRequestCheckout(int bookingRoomId)
        {
            OpenCheckout(new List<int> { bookingRoomId });
        }

        // Người dùng chọn nhiều BookingRoomID để trả phòng
        private void OnRequestCheckoutMany(List<int> bookingRoomIds)
        {
            if (bookingRoomIds == null || bookingRoomIds.Count == 0) return;
            OpenCheckout(bookingRoomIds.Distinct().ToList());
        }

        // =========================================================
        // Mở flow "Chi tiết trả phòng" (frmBookingDetail1)
        //   - Form này sẽ load khách, phòng, dịch vụ
        //   - Nhấn "Trả phòng" trong đó => mở frmCheckout1
        //   - Khi thanh toán đủ, frmCheckout1 -> DialogResult.OK -> frmBookingDetail1.OK
        //   - Ở đây nhận OK và reload lại UC
        // =========================================================
        private void OpenCheckout(List<int> bookingRoomIds)
        {
            try
            {
                using (var f = new frmBookingDetail1(bookingRoomIds, _currentUserId))
                {
                    f.StartPosition = FormStartPosition.CenterParent;
                    var rs = f.ShowDialog(this);

                    if (rs == DialogResult.OK)
                    {
                        // Reload lại danh sách booking (đúng yêu cầu: thanh toán đủ thì load lại UC)
                        _ucBookingRoom?.ReloadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được màn hình trả phòng: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
