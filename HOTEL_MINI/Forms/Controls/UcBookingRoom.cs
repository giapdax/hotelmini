using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms.Controls
{
    public partial class UcBookingRoom : UserControl
    {
        // ====== Delegates để tích hợp BLL/DAL ======
        // Trả về danh sách booking theo CCCD + trạng thái
        public Func<string, string, List<BookingRow>> FetchBookingsByCustomer { get; set; }

        // Trả về danh sách phòng + booking hiện tại theo số phòng + trạng thái phòng
        public Func<string, string, List<RoomBookingRow>> FetchBookingsByRoom { get; set; }

        // Thực hiện check-in (Nhận phòng) theo bookingId, return true nếu OK
        public Func<int, bool> CheckInBooking { get; set; }

        // Thực hiện hủy booking theo bookingId, return true nếu OK
        public Func<int, bool> CancelBooking { get; set; }

        // Thực hiện trả phòng (checkout) theo bookingId, return true nếu OK
        public Func<int, bool> CheckoutBooking { get; set; }

        // Nếu bạn muốn mở form chi tiết (ví dụ frmBookingDetail1) khi đang ở
        public Action<int> OpenBookingDetail { get; set; }

        // ====== Binding nguồn dữ liệu ======
        private BindingList<BookingRow> _leftData = new BindingList<BookingRow>();
        private BindingList<RoomBookingRow> _rightData = new BindingList<RoomBookingRow>();

        // Theo dõi lựa chọn hiện tại
        private int _selectedBookingId = 0;
        private string _selectedStatus = null;

        public UcBookingRoom()
        {
            InitializeComponent();
            HookEvents();
            SetupUI();
            LoadCombos();
            RefreshLeft();   // mặc định tải bảng trái
            RefreshRight();  // và bảng phải
        }

        #region DTO cho lưới
        public class BookingRow
        {
            public int BookingID { get; set; }
            public string CustomerName { get; set; }
            public string IDNumber { get; set; }
            public string RoomNumber { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            /// <summary> "Booked" | "Occupied" | "CheckedIn" | "Cancelled" | "Completed" </summary>
            public string Status { get; set; }
            public decimal? TotalEstimate { get; set; }
        }

        public class RoomBookingRow
        {
            public string RoomNumber { get; set; }
            public int BookingID { get; set; }
            /// <summary> "Empty" | "Booked" | "Occupied" </summary>
            public string RoomStatus { get; set; }
            public string CustomerName { get; set; }
            public DateTime? CheckIn { get; set; }
            public DateTime? CheckOut { get; set; }
        }
        #endregion

        #region UI init
        private void SetupUI()
        {
            // Left grid (dataGridView1) : Booking theo khách
            var gvL = dataGridView1;
            gvL.AutoGenerateColumns = false;
            gvL.AllowUserToAddRows = false;
            gvL.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvL.MultiSelect = false;
            gvL.RowHeadersVisible = false;
            gvL.Columns.Clear();

            gvL.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookingRow.BookingID), HeaderText = "BookingID", Width = 90 });
            gvL.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookingRow.CustomerName), HeaderText = "Khách hàng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            gvL.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookingRow.IDNumber), HeaderText = "CCCD", Width = 120 });
            gvL.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookingRow.RoomNumber), HeaderText = "Phòng", Width = 80 });
            gvL.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookingRow.CheckIn), HeaderText = "Check-in", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
            gvL.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookingRow.CheckOut), HeaderText = "Check-out", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" } });
            gvL.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(BookingRow.Status), HeaderText = "Trạng thái", Width = 110 });

            gvL.DataSource = _leftData;

            // Right grid (dataGridView2) : Phòng theo trạng thái
            var gvR = dataGridView2;
            gvR.AutoGenerateColumns = false;
            gvR.AllowUserToAddRows = false;
            gvR.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvR.MultiSelect = false;
            gvR.RowHeadersVisible = false;
            gvR.Columns.Clear();

            gvR.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBookingRow.RoomNumber), HeaderText = "Phòng", Width = 80 });
            gvR.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBookingRow.RoomStatus), HeaderText = "TT Phòng", Width = 110 });
            gvR.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBookingRow.BookingID), HeaderText = "BookingID", Width = 90 });
            gvR.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBookingRow.CustomerName), HeaderText = "Khách hàng", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            gvR.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBookingRow.CheckIn), HeaderText = "Check-in", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM HH:mm" } });
            gvR.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(RoomBookingRow.CheckOut), HeaderText = "Check-out", Width = 120, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM HH:mm" } });

            gvR.DataSource = _rightData;

            // Đặt text các nút (theo designer là button1/2/3)
            button1.Text = "Nhận phòng";
            button2.Text = "Trả phòng";
            button3.Text = "Hủy đặt phòng";

            UpdateButtons(null);
        }

        private void LoadCombos()
        {
            // Trạng thái booking (lọc trái)
            // "(Tất cả)", "Booked", "Đang ở", "Booked/Đang ở", ...
            cboStatusBooking.Items.Clear();
            cboStatusBooking.Items.Add("(Tất cả)");
            cboStatusBooking.Items.Add("Booked");
            cboStatusBooking.Items.Add("Đang ở"); // Occupied/CheckedIn
            cboStatusBooking.Items.Add("Booked/Đang ở");
            cboStatusBooking.SelectedIndex = 3; // mặc định: Booked/Đang ở

            // Trạng thái phòng (lọc phải)
            cboStatusRoom.Items.Clear();
            cboStatusRoom.Items.Add("(Tất cả)");
            cboStatusRoom.Items.Add("Booked");
            cboStatusRoom.Items.Add("Đang ở");
            cboStatusRoom.Items.Add("Trống");
            cboStatusRoom.SelectedIndex = 0;
        }

        private void HookEvents()
        {
            // Sự kiện tìm kiếm nhanh
            txtCCCD.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) RefreshLeft(); };
            txtRoomNumber.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) RefreshRight(); };
            cboStatusBooking.SelectedIndexChanged += (s, e) => RefreshLeft();
            cboStatusRoom.SelectedIndexChanged += (s, e) => RefreshRight();

            // Chọn dòng → cập nhật nút
            dataGridView1.SelectionChanged += (s, e) => SelectFromLeft();
            dataGridView2.SelectionChanged += (s, e) => SelectFromRight();

            // Nút actions
            button1.Click += (s, e) => DoCheckIn();
            button2.Click += (s, e) => DoCheckout();
            button3.Click += (s, e) => DoCancel();
        }
        #endregion

        #region Load dữ liệu 2 bảng
        private string NormalizeBookingFilter(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw) || raw == "(Tất cả)") return null;
            if (raw.Equals("Đang ở", StringComparison.OrdinalIgnoreCase)) return "Occupied"; // hoặc "CheckedIn"
            if (raw.Equals("Booked/Đang ở", StringComparison.OrdinalIgnoreCase)) return "BookedOrOccupied";
            return raw; // "Booked", "Cancelled", ...
        }

        private string NormalizeRoomFilter(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw) || raw == "(Tất cả)") return null;
            if (raw.Equals("Đang ở", StringComparison.OrdinalIgnoreCase)) return "Occupied";
            if (raw.Equals("Trống", StringComparison.OrdinalIgnoreCase)) return "Empty";
            return raw; // "Booked"
        }

        private void RefreshLeft()
        {
            var cccd = (txtCCCD.Text ?? "").Trim();
            var st = NormalizeBookingFilter(cboStatusBooking.SelectedItem?.ToString());

            List<BookingRow> data;
            if (FetchBookingsByCustomer != null)
            {
                try { data = FetchBookingsByCustomer(cccd, st) ?? new List<BookingRow>(); }
                catch { data = new List<BookingRow>(); }
            }
            else
            {
                // Chưa gắn nguồn → để rỗng
                data = new List<BookingRow>();
            }

            _leftData = new BindingList<BookingRow>(data.OrderByDescending(x => x.CheckIn).ToList());
            dataGridView1.DataSource = _leftData;

            // reset chọn
            _selectedBookingId = 0;
            _selectedStatus = null;
            UpdateButtons(null);
        }

        private void RefreshRight()
        {
            var roomNo = (txtRoomNumber.Text ?? "").Trim();
            var st = NormalizeRoomFilter(cboStatusRoom.SelectedItem?.ToString());

            List<RoomBookingRow> data;
            if (FetchBookingsByRoom != null)
            {
                try { data = FetchBookingsByRoom(roomNo, st) ?? new List<RoomBookingRow>(); }
                catch { data = new List<RoomBookingRow>(); }
            }
            else
            {
                data = new List<RoomBookingRow>();
            }

            _rightData = new BindingList<RoomBookingRow>(data.OrderBy(x => x.RoomNumber).ToList());
            dataGridView2.DataSource = _rightData;

            _selectedBookingId = 0;
            _selectedStatus = null;
            UpdateButtons(null);
        }
        #endregion

        #region Chọn dòng & bật/tắt nút
        private static bool IsBooked(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return false;
            return status.Equals("Booked", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsOccupied(string status)
        {
            if (string.IsNullOrWhiteSpace(status)) return false;
            // Tuỳ DB đặt tên "Occupied" hay "CheckedIn"
            return status.Equals("Occupied", StringComparison.OrdinalIgnoreCase)
                || status.Equals("CheckedIn", StringComparison.OrdinalIgnoreCase)
                || status.Equals("Đang ở", StringComparison.OrdinalIgnoreCase);
        }

        private void UpdateButtons(string status)
        {
            // Không có gì được chọn
            if (string.IsNullOrWhiteSpace(status))
            {
                button1.Enabled = false; // Nhận phòng
                button2.Enabled = false; // Trả phòng
                button3.Enabled = false; // Hủy
                return;
            }

            // Quy ước:
            // - Booked  => chỉ cho Nhận phòng/Hủy
            // - Occupied => chỉ cho Trả phòng
            button1.Enabled = IsBooked(status);
            button3.Enabled = IsBooked(status);
            button2.Enabled = IsOccupied(status);
        }

        private void SelectFromLeft()
        {
            if (dataGridView1.CurrentRow?.DataBoundItem is BookingRow row)
            {
                _selectedBookingId = row.BookingID;
                _selectedStatus = row.Status;
                UpdateButtons(_selectedStatus);
            }
        }

        private void SelectFromRight()
        {
            if (dataGridView2.CurrentRow?.DataBoundItem is RoomBookingRow row)
            {
                _selectedBookingId = row.BookingID;
                _selectedStatus = row.RoomStatus; // với bảng phải dùng trạng thái phòng
                UpdateButtons(_selectedStatus);
            }
        }
        #endregion

        #region Actions: Nhận phòng / Trả phòng / Hủy đặt
        private void DoCheckIn()
        {
            if (_selectedBookingId <= 0 || !IsBooked(_selectedStatus))
            {
                MessageBox.Show("Hãy chọn 1 booking đang ở trạng thái 'Booked'.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var cf = MessageBox.Show($"Xác nhận nhận phòng cho booking #{_selectedBookingId}?",
                                     "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cf != DialogResult.Yes) return;

            if (CheckInBooking != null)
            {
                try
                {
                    var ok = CheckInBooking(_selectedBookingId);
                    if (ok)
                    {
                        MessageBox.Show("Đã nhận phòng. Trạng thái chuyển sang 'Đang ở'.", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshLeft();
                        RefreshRight();
                    }
                    else
                    {
                        MessageBox.Show("Nhận phòng thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi nhận phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Chưa gắn xử lý CheckInBooking. Hãy truyền delegate gọi BookingService.CheckIn(...).",
                    "Chưa cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DoCancel()
        {
            if (_selectedBookingId <= 0 || !IsBooked(_selectedStatus))
            {
                MessageBox.Show("Chỉ hủy được booking đang 'Booked'.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var cf = MessageBox.Show($"Bạn chắc muốn hủy booking #{_selectedBookingId}?",
                                     "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (cf != DialogResult.Yes) return;

            if (CancelBooking != null)
            {
                try
                {
                    var ok = CancelBooking(_selectedBookingId);
                    if (ok)
                    {
                        MessageBox.Show("Đã hủy booking.", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshLeft();
                        RefreshRight();
                    }
                    else
                    {
                        MessageBox.Show("Hủy booking thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi hủy booking: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Chưa gắn xử lý CancelBooking. Hãy truyền delegate gọi BookingService.Cancel(...).",
                    "Chưa cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DoCheckout()
        {
            if (_selectedBookingId <= 0 || !IsOccupied(_selectedStatus))
            {
                MessageBox.Show("Hãy chọn booking/phòng đang 'Đang ở' để trả phòng.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Option A: mở form chi tiết trước khi trả (gợi ý theo yêu cầu)
            if (OpenBookingDetail != null)
            {
                try
                {
                    OpenBookingDetail(_selectedBookingId);
                    // Sau khi đóng form chi tiết, bạn có thể refresh
                    RefreshLeft();
                    RefreshRight();
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Không mở được chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            // Option B: gọi thẳng CheckoutBooking
            if (CheckoutBooking != null)
            {
                var cf = MessageBox.Show($"Xác nhận trả phòng cho booking #{_selectedBookingId}?",
                                         "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cf != DialogResult.Yes) return;

                try
                {
                    var ok = CheckoutBooking(_selectedBookingId);
                    if (ok)
                    {
                        MessageBox.Show("Đã trả phòng thành công.", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshLeft();
                        RefreshRight();
                    }
                    else
                    {
                        MessageBox.Show("Trả phòng thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi trả phòng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Hướng dẫn tích hợp theo yêu cầu bạn mô tả
                MessageBox.Show(
                    "Chưa gắn xử lý trả phòng.\n" +
                    "- Nếu phòng đang có người ở, bạn có thể mở frmBookingDetail1 để xem chi tiết, " +
                    "đổi nút Đặt phòng thành Trả phòng và xuất thông tin hoá đơn.\n" +
                    "- Hoặc truyền delegate CheckoutBooking để mình gọi thẳng BLL.",
                    "Chưa cấu hình", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
