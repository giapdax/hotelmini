using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HOTEL_MINI.Forms.Controls;
using HOTEL_MINI.Model.Entity;

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
            DoubleBuffered = true;

            Load += FrmBooking_Load;
            if (tabBooking != null)
                tabBooking.SelectedIndexChanged += (s, e) => EnsureTabContent();
        }

        public frmBooking(User user) : this()
        {
            _currentUser = user;
            _currentUserId = user?.UserID ?? 0;
        }

        private void FrmBooking_Load(object sender, EventArgs e)
        {
            EnsureTabContent();
        }

        private void EnsureTabContent()
        {
            if (tabBooking == null || tabBooking.SelectedTab == null) return;

            if (tabBooking.SelectedTab == btnBookRoom)
            {
                EnsureBookRoomTab();
                _ucBookRoom?.ReloadRoomsNow();
            }
            else if (tabBooking.SelectedTab == btnBookingRoom)
            {
                EnsureBookingRoomTab();
            }
        }

        private void EnsureBookRoomTab()
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

        private void EnsureBookingRoomTab()
        {
            if (_ucBookingRoom == null)
            {
                _ucBookingRoom = new UcBookingRoom
                {
                    Dock = DockStyle.Fill,
                    CurrentUserId = _currentUserId
                };
                _ucBookingRoom.RequestCheckout += OnRequestCheckout;
                _ucBookingRoom.RequestCheckoutMany += OnRequestCheckoutMany;

                btnBookingRoom.Controls.Clear();
                btnBookingRoom.Controls.Add(_ucBookingRoom);
            }
            else
            {
                _ucBookingRoom.CurrentUserId = _currentUserId;
                _ucBookingRoom.ReloadData();
            }
        }

        private void OnRequestCheckout(int bookingRoomId)
        {
            OpenCheckout(new List<int> { bookingRoomId });
        }

        private void OnRequestCheckoutMany(List<int> bookingRoomIds)
        {
            if (bookingRoomIds == null || bookingRoomIds.Count == 0) return;
            OpenCheckout(bookingRoomIds.Distinct().ToList());
        }

      
        public void ShowUcBookRoom(int? customerId = null, string idNumber = null)
        {
            if (tabBooking != null && btnBookRoom != null)
                tabBooking.SelectedTab = btnBookRoom;

            EnsureBookRoomTab();

            _ucBookRoom.CurrentUserId = _currentUserId;
            if (customerId.HasValue)
                _ucBookRoom.SetCustomer(customerId.Value, idNumber ?? "");

            _ucBookRoom.BringToFront();
            _ucBookRoom.ReloadRoomsNow();
        }
        public void ShowUcBookingRoom()
        {
            if (tabBooking != null && btnBookingRoom != null)
                tabBooking.SelectedTab = btnBookingRoom;

            EnsureBookingRoomTab();                
            _ucBookingRoom.CurrentUserId = _currentUserId;
            _ucBookingRoom.BringToFront();
            _ucBookingRoom.ReloadData();        
        }

        private void OpenCheckout(List<int> bookingRoomIds)
        {
            try
            {
                using (var f = new frmBookingDetail(bookingRoomIds, _currentUserId))
                {
                    f.StartPosition = FormStartPosition.CenterParent;
                    var rs = f.ShowDialog(this);
                    if (rs == DialogResult.OK)
                    {
                        _ucBookingRoom?.ReloadData();
                        _ucBookRoom?.ReloadRoomsNow();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được màn hình trả phòng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
