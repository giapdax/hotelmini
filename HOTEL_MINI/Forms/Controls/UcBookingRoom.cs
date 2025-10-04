using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms.Controls
{
    public partial class UcBookingRoom : UserControl
    {
        // ====== Delegates mức Booking ======
        // Trả về danh sách Booking theo CCCD + trạng thái (null => tất cả)
        public Func<string, string, List<BookingRow>> FetchBookingsByCustomer { get; set; }

        // Trả về danh sách Room + Booking theo số phòng + trạng thái (null => tất cả)
        public Func<string, string, List<RoomBookingRow>> FetchBookingsByRoom { get; set; }

        // Single booking
        public Func<int, bool> CheckInBooking { get; set; }
        public Func<int, bool> CancelBooking { get; set; }
        public Func<int, bool> CheckoutBooking { get; set; }

        // Batch booking (tuỳ chọn)
        public Func<List<int>, bool> CheckInBookingsBatch { get; set; }
        public Func<List<int>, bool> CancelBookingsBatch { get; set; }
        public Func<List<int>, bool> CheckoutBookingsBatch { get; set; }

        // ====== Delegates mức Room ======
        public class RoomKey
        {
            public int BookingID { get; set; }
            public string RoomNumber { get; set; }
        }

        // Single room (tuỳ chọn)
        public Func<RoomKey, bool> CheckInRoom { get; set; }
        public Func<RoomKey, bool> CheckoutRoom { get; set; }
        public Func<RoomKey, bool> CancelRoom { get; set; }

        // Batch rooms (tuỳ chọn)
        public Func<List<RoomKey>, bool> CheckInRoomsBatch { get; set; }
        public Func<List<RoomKey>, bool> CheckoutRoomsBatch { get; set; }
        public Func<List<RoomKey>, bool> CancelRoomsBatch { get; set; }

        // Mở form chi tiết (ví dụ trước khi checkout)
        public Action<int> OpenBookingDetail { get; set; }

        // ====== Binding ======
        private BindingList<BookingRow> _leftData = new BindingList<BookingRow>();
        private BindingList<RoomBookingRow> _rightData = new BindingList<RoomBookingRow>();

        // Chọn hiện tại (cho thao tác single)
        private int _selectedBookingId = 0;
        private string _selectedStatus = null;

        // Tên cột checkbox
        private const string ColSelectBooking = "SelBooking";
        private const string ColSelectRoom = "SelRoom";

        // Debounce timers
        private Timer _debounceLeft;
        private Timer _debounceRight;

        // Header checkbox: chọn tất cả
        private CheckBox _chkHeaderLeft;
        private CheckBox _chkHeaderRight;

        public UcBookingRoom()
        {
            InitializeComponent();
            SetupUI();
            HookEvents();
            LoadCombos();

            // Debounce tìm kiếm
            _debounceLeft = new Timer { Interval = 300 };
            _debounceLeft.Tick += (s, e) => { _debounceLeft.Stop(); RefreshLeft(); };

            _debounceRight = new Timer { Interval = 300 };
            _debounceRight.Tick += (s, e) => { _debounceRight.Stop(); RefreshRight(); };

            // Lần đầu: hiển thị (mặc định loại Hủy)
            RefreshLeft();
            RefreshRight();
        }

        #region DTO
        public class BookingRow
        {
            public int BookingID { get; set; }
            public string CustomerName { get; set; }
            public string IDNumber { get; set; }
            public string RoomNumber { get; set; }
            public DateTime CheckIn { get; set; }
            public DateTime CheckOut { get; set; }
            /// <summary>"Booked" | "Occupied" | "CheckedIn" | "Cancelled" | "Completed"</summary>
            public string Status { get; set; }
            public decimal? TotalEstimate { get; set; }

            // Checkbox nhiều booking
            public bool Sel { get; set; }
        }

        public class RoomBookingRow
        {
            public string RoomNumber { get; set; }
            public int BookingID { get; set; }
            /// <summary>"Empty" | "Booked" | "Occupied"</summary>
            public string RoomStatus { get; set; }
            public string CustomerName { get; set; }
            public DateTime? CheckIn { get; set; }
            public DateTime? CheckOut { get; set; }

            // Checkbox nhiều phòng
            public bool Sel { get; set; }
        }
        #endregion

        #region UI / Grid setup
        private void SetupUI()
        {
            // ==== LEFT: Booking ====
            var gvL = dataGridView1;
            gvL.AutoGenerateColumns = false;
            gvL.AllowUserToAddRows = false;
            gvL.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvL.MultiSelect = false;
            gvL.RowHeadersVisible = false;
            gvL.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gvL.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gvL.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            gvL.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            gvL.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            SetDoubleBuffered(gvL);
            gvL.Columns.Clear();

            // Checkbox header
            _chkHeaderLeft = new CheckBox { Size = new Size(15, 15) };
            _chkHeaderLeft.CheckedChanged += (s, e) =>
            {
                bool on = _chkHeaderLeft.Checked;
                foreach (var r in _leftData) r.Sel = on;
                gvL.Refresh();
                UpdateButtons(_selectedStatus);
            };
            gvL.Controls.Add(_chkHeaderLeft);

            var colSelL = new DataGridViewCheckBoxColumn
            {
                Name = ColSelectBooking,
                DataPropertyName = nameof(BookingRow.Sel),
                HeaderText = "",
                FillWeight = 20,
                MinimumWidth = 30
            };
            gvL.Columns.Add(colSelL);

            gvL.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(BookingRow.BookingID),
                HeaderText = "BookingID",
                FillWeight = 55,
                MinimumWidth = 70
            });
            gvL.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(BookingRow.CustomerName),
                HeaderText = "Khách hàng",
                FillWeight = 160,
                MinimumWidth = 140
            });
            gvL.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(BookingRow.IDNumber),
                HeaderText = "CCCD",
                FillWeight = 85,
                MinimumWidth = 100
            });
            gvL.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(BookingRow.RoomNumber),
                HeaderText = "Phòng",
                FillWeight = 60,
                MinimumWidth = 70
            });
            gvL.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(BookingRow.CheckIn),
                HeaderText = "Check-in",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" },
                FillWeight = 110,
                MinimumWidth = 120
            });
            gvL.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(BookingRow.CheckOut),
                HeaderText = "Check-out",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" },
                FillWeight = 110,
                MinimumWidth = 120
            });
            gvL.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(BookingRow.Status),
                HeaderText = "Trạng thái",
                FillWeight = 90,
                MinimumWidth = 100
            });

            gvL.DataSource = _leftData;
            gvL.Scroll += (s, e) => PositionHeaderCheckBox(gvL, _chkHeaderLeft);
            gvL.ColumnWidthChanged += (s, e) => PositionHeaderCheckBox(gvL, _chkHeaderLeft);
            gvL.SizeChanged += (s, e) => PositionHeaderCheckBox(gvL, _chkHeaderLeft);

            // ==== RIGHT: Room ====
            var gvR = dataGridView2;
            gvR.AutoGenerateColumns = false;
            gvR.AllowUserToAddRows = false;
            gvR.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gvR.MultiSelect = false; // dùng checkbox để chọn nhiều
            gvR.RowHeadersVisible = false;
            gvR.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gvR.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gvR.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            gvR.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            gvR.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 248, 248);
            SetDoubleBuffered(gvR);
            gvR.Columns.Clear();

            _chkHeaderRight = new CheckBox { Size = new Size(15, 15) };
            _chkHeaderRight.CheckedChanged += (s, e) =>
            {
                bool on = _chkHeaderRight.Checked;
                foreach (var r in _rightData) r.Sel = on;
                gvR.Refresh();
                UpdateButtons(_selectedStatus);
            };
            gvR.Controls.Add(_chkHeaderRight);

            var colSelR = new DataGridViewCheckBoxColumn
            {
                Name = ColSelectRoom,
                DataPropertyName = nameof(RoomBookingRow.Sel),
                HeaderText = "",
                FillWeight = 20,
                MinimumWidth = 30
            };
            gvR.Columns.Add(colSelR);

            gvR.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(RoomBookingRow.RoomNumber),
                HeaderText = "Phòng",
                FillWeight = 60,
                MinimumWidth = 70
            });
            gvR.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(RoomBookingRow.RoomStatus),
                HeaderText = "TT Phòng",
                FillWeight = 90,
                MinimumWidth = 100
            });
            gvR.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(RoomBookingRow.BookingID),
                HeaderText = "BookingID",
                FillWeight = 55,
                MinimumWidth = 70
            });
            gvR.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(RoomBookingRow.CustomerName),
                HeaderText = "Khách hàng",
                FillWeight = 160,
                MinimumWidth = 140
            });
            gvR.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(RoomBookingRow.CheckIn),
                HeaderText = "Check-in",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM HH:mm" },
                FillWeight = 90,
                MinimumWidth = 100
            });
            gvR.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = nameof(RoomBookingRow.CheckOut),
                HeaderText = "Check-out",
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM HH:mm" },
                FillWeight = 90,
                MinimumWidth = 100
            });

            gvR.DataSource = _rightData;
            gvR.Scroll += (s, e) => PositionHeaderCheckBox(gvR, _chkHeaderRight);
            gvR.ColumnWidthChanged += (s, e) => PositionHeaderCheckBox(gvR, _chkHeaderRight);
            gvR.SizeChanged += (s, e) => PositionHeaderCheckBox(gvR, _chkHeaderRight);

            // Nút
            button1.Text = "Nhận phòng";
            button2.Text = "Trả phòng";
            button3.Text = "Hủy đặt phòng";

            UpdateButtons(null);
        }

        // Đặt vị trí checkbox header ở cột đầu tiên
        private void PositionHeaderCheckBox(DataGridView gv, CheckBox chk)
        {
            if (gv.Columns.Count == 0) return;
            var rect = gv.GetCellDisplayRectangle(0, -1, true);
            chk.Location = new Point(rect.X + 6, rect.Y + (rect.Height - chk.Height) / 2);
        }

        // Giảm giật DataGridView
        private static void SetDoubleBuffered(DataGridView gv)
        {
            try
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null, gv, new object[] { true });
            }
            catch { /* ignore */ }
        }
        #endregion

        #region Hooks
        private void HookEvents()
        {
            // Tự load khi gõ (debounce)
            txtCCCD.TextChanged += (s, e) => { _debounceLeft.Stop(); _debounceLeft.Start(); };
            txtRoomNumber.TextChanged += (s, e) => { _debounceRight.Stop(); _debounceRight.Start(); };

            // Enter để load ngay
            txtCCCD.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { _debounceLeft.Stop(); RefreshLeft(); } };
            txtRoomNumber.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { _debounceRight.Stop(); RefreshRight(); } };

            cboStatusBooking.SelectedIndexChanged += (s, e) => RefreshLeft();
            cboStatusRoom.SelectedIndexChanged += (s, e) => RefreshRight();

            // Chọn dòng (single)
            dataGridView1.SelectionChanged += (s, e) => SelectFromLeft();
            dataGridView2.SelectionChanged += (s, e) => SelectFromRight();

            // Checkbox booking thay đổi → cập nhật nút
            dataGridView1.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dataGridView1.IsCurrentCellDirty) dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            dataGridView1.CellValueChanged += (s, e) =>
            {
                if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == ColSelectBooking)
                    UpdateButtons(_selectedStatus);
            };

            // Checkbox room thay đổi → cập nhật nút
            dataGridView2.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (dataGridView2.IsCurrentCellDirty) dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            dataGridView2.CellValueChanged += (s, e) =>
            {
                if (e.RowIndex >= 0 && dataGridView2.Columns[e.ColumnIndex].Name == ColSelectRoom)
                    UpdateButtons(_selectedStatus);
            };

            // Actions
            button1.Click += (s, e) => DoCheckIn();
            button2.Click += (s, e) => DoCheckout();
            button3.Click += (s, e) => DoCancel();
        }
        #endregion

        #region Load data & lọc
        private static string NullIfEmpty(string s)
            => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

        // Map hiển thị → token
        // "ALL_NO_CANCEL" = tất cả nhưng loại Cancelled theo yêu cầu
        private string NormalizeBookingFilter(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return "ALL_NO_CANCEL";
            switch (raw)
            {
                case "Tất cả (trừ hủy)": return "ALL_NO_CANCEL";
                case "Booked/Đang ở": return "BookedOrOccupied";
                case "Booked": return "Booked";
                case "Đang ở": return "Occupied";
                case "Đã hủy": return "Cancelled";
                case "Hoàn tất": return "Completed";
                default: return "ALL_NO_CANCEL";
            }
        }

        private string NormalizeRoomFilter(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw) || raw == "Tất cả") return null;
            if (raw.Equals("Đang ở", StringComparison.OrdinalIgnoreCase)) return "Occupied";
            if (raw.Equals("Trống", StringComparison.OrdinalIgnoreCase)) return "Empty";
            if (raw.Equals("Booked", StringComparison.OrdinalIgnoreCase)) return "Booked";
            return null; // default all
        }

        private static bool IsBooked(string status)
            => !string.IsNullOrWhiteSpace(status) && status.Equals("Booked", StringComparison.OrdinalIgnoreCase);

        private static bool IsOccupied(string status)
            => !string.IsNullOrWhiteSpace(status) && (
                   status.Equals("Occupied", StringComparison.OrdinalIgnoreCase)
                || status.Equals("CheckedIn", StringComparison.OrdinalIgnoreCase)
                || status.Equals("Đang ở", StringComparison.OrdinalIgnoreCase));

        private static bool IsCancelled(string status)
            => !string.IsNullOrWhiteSpace(status) && status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase);

        private static bool IsCompleted(string status)
            => !string.IsNullOrWhiteSpace(status) && status.Equals("Completed", StringComparison.OrdinalIgnoreCase);

        public void RefreshLeft()
        {
            var cccd = NullIfEmpty(txtCCCD.Text);
            var statusToken = NormalizeBookingFilter(cboStatusBooking.SelectedItem?.ToString());

            List<BookingRow> data;
            if (FetchBookingsByCustomer != null)
            {
                try
                {
                    // truyền null nếu "ALL_NO_CANCEL" hoặc Booked/Đang ở để backend ko bó hẹp
                    string backendStatus = statusToken == "ALL_NO_CANCEL" ? null : statusToken;
                    data = FetchBookingsByCustomer(cccd, backendStatus) ?? new List<BookingRow>();
                }
                catch
                {
                    data = new List<BookingRow>();
                }
            }
            else data = new List<BookingRow>();

            // Chuẩn hoá theo token hiển thị:
            if (statusToken == "ALL_NO_CANCEL")
            {
                data = data.Where(x => !IsCancelled(x.Status) && !IsCompleted(x.Status)).ToList();
            }
            else if (statusToken == "BookedOrOccupied")
            {
                data = data.Where(x => IsBooked(x.Status) || IsOccupied(x.Status)).ToList();
            }
            // Các token khác (Booked/Occupied/Cancelled/Completed) đã được backend xử lý nếu truyền status,
            // nhưng ta vẫn có thể lọc lại để chắc chắn:
            else if (statusToken == "Booked")
            {
                data = data.Where(x => IsBooked(x.Status)).ToList();
            }
            else if (statusToken == "Occupied")
            {
                data = data.Where(x => IsOccupied(x.Status)).ToList();
            }
            else if (statusToken == "Cancelled")
            {
                data = data.Where(x => IsCancelled(x.Status)).ToList();
            }
            else if (statusToken == "Completed")
            {
                data = data.Where(x => IsCompleted(x.Status)).ToList();
            }

            // Clear tick khi reload
            data.ForEach(x => x.Sel = false);

            _leftData = new BindingList<BookingRow>(data.OrderByDescending(x => x.CheckIn).ToList());
            dataGridView1.DataSource = _leftData;

            // reset header checkbox
            _chkHeaderLeft.CheckedChanged -= HeaderLeft_CheckedChanged_NoLoop;
            _chkHeaderLeft.Checked = false;
            _chkHeaderLeft.CheckedChanged += HeaderLeft_CheckedChanged_NoLoop;

            _selectedBookingId = 0;
            _selectedStatus = null;
            UpdateButtons(null);
        }

        private void HeaderLeft_CheckedChanged_NoLoop(object sender, EventArgs e)
        {
            // placeholder nếu muốn xử lý đặc biệt
        }

        public void RefreshRight()
        {
            var roomNo = NullIfEmpty(txtRoomNumber.Text);
            var status = NormalizeRoomFilter(cboStatusRoom.SelectedItem?.ToString());

            List<RoomBookingRow> data;
            if (FetchBookingsByRoom != null)
            {
                try { data = FetchBookingsByRoom(roomNo, status) ?? new List<RoomBookingRow>(); }
                catch { data = new List<RoomBookingRow>(); }
            }
            else data = new List<RoomBookingRow>();

            data.ForEach(x => x.Sel = false);

            _rightData = new BindingList<RoomBookingRow>(data.OrderBy(x => x.RoomNumber).ToList());
            dataGridView2.DataSource = _rightData;

            _chkHeaderRight.CheckedChanged -= HeaderRight_CheckedChanged_NoLoop;
            _chkHeaderRight.Checked = false;
            _chkHeaderRight.CheckedChanged += HeaderRight_CheckedChanged_NoLoop;

            _selectedBookingId = 0;
            _selectedStatus = null;
            UpdateButtons(null);
        }

        private void HeaderRight_CheckedChanged_NoLoop(object sender, EventArgs e)
        {
            // placeholder nếu muốn xử lý đặc biệt
        }
        #endregion

        #region Selection & Button state
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
                _selectedStatus = row.RoomStatus;
                UpdateButtons(_selectedStatus);
            }
        }

        private List<BookingRow> GetCheckedBookings() => _leftData.Where(r => r.Sel).ToList();
        private List<RoomBookingRow> GetCheckedRooms() => _rightData.Where(r => r.Sel).ToList();

        private List<int> GetCheckedBookingIds()
            => GetCheckedBookings().Select(b => b.BookingID).Distinct().ToList();

        private List<RoomKey> GetCheckedRoomKeys()
            => GetCheckedRooms().Select(r => new RoomKey { BookingID = r.BookingID, RoomNumber = r.RoomNumber }).ToList();

        private void UpdateButtons(string statusFromSingleSelection)
        {
            // Ưu tiên: tick booking
            var tickBookings = GetCheckedBookings();
            if (tickBookings.Count > 0)
            {
                bool anyBooked = tickBookings.Any(b => IsBooked(b.Status));
                bool anyOccupied = tickBookings.Any(b => IsOccupied(b.Status));
                bool mixed = anyBooked && anyOccupied;

                button1.Enabled = anyBooked && !mixed;   // Nhận phòng
                button3.Enabled = anyBooked && !mixed;   // Hủy
                button2.Enabled = anyOccupied && !mixed; // Trả phòng
                return;
            }

            // Tick room
            var tickRooms = GetCheckedRooms();
            if (tickRooms.Count > 0)
            {
                bool anyBooked = tickRooms.Any(r => IsBooked(r.RoomStatus));
                bool anyOccupied = tickRooms.Any(r => IsOccupied(r.RoomStatus));
                bool mixed = anyBooked && anyOccupied;

                button1.Enabled = anyBooked && !mixed;
                button3.Enabled = anyBooked && !mixed;
                button2.Enabled = anyOccupied && !mixed;
                return;
            }

            // Không tick → theo selection
            if (string.IsNullOrWhiteSpace(statusFromSingleSelection))
            {
                button1.Enabled = button2.Enabled = button3.Enabled = false;
                return;
            }

            button1.Enabled = IsBooked(statusFromSingleSelection);
            button3.Enabled = IsBooked(statusFromSingleSelection);
            button2.Enabled = IsOccupied(statusFromSingleSelection);
        }
        #endregion

        #region Actions
        private void DoCheckIn()
        {
            var tickBookings = GetCheckedBookings();
            var tickRooms = GetCheckedRooms();

            // Batch booking
            if (tickBookings.Count > 0)
            {
                if (!tickBookings.All(b => IsBooked(b.Status)))
                {
                    MessageBox.Show("Chỉ 'Nhận phòng' được các booking đang 'Booked'.");
                    return;
                }

                var bookingIds = GetCheckedBookingIds();
                if (MessageBox.Show($"Xác nhận nhận phòng cho {bookingIds.Count} booking đã chọn?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                bool ok = false;
                try
                {
                    if (CheckInBookingsBatch != null) ok = CheckInBookingsBatch(bookingIds);
                    else if (CheckInBooking != null) ok = bookingIds.Select(id => CheckInBooking(id)).All(x => x);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi nhận phòng: " + ex.Message); }

                if (ok) { MessageBox.Show("Đã nhận phòng.", "Thành công"); RefreshLeft(); RefreshRight(); }
                else MessageBox.Show("Nhận phòng thất bại.", "Lỗi");
                return;
            }

            // Batch room
            if (tickRooms.Count > 0)
            {
                if (!tickRooms.All(r => IsBooked(r.RoomStatus)))
                {
                    MessageBox.Show("Chỉ 'Nhận phòng' được các phòng đang 'Booked'.");
                    return;
                }

                var keys = GetCheckedRoomKeys();
                if (MessageBox.Show($"Xác nhận nhận {keys.Count} phòng đã chọn?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                bool ok = false;
                try
                {
                    if (CheckInRoomsBatch != null) ok = CheckInRoomsBatch(keys);
                    else if (CheckInRoom != null) ok = keys.Select(k => CheckInRoom(k)).All(x => x);
                    else if (CheckInBookingsBatch != null) ok = CheckInBookingsBatch(keys.Select(k => k.BookingID).Distinct().ToList());
                    else if (CheckInBooking != null) ok = keys.Select(k => k.BookingID).Distinct().Select(id => CheckInBooking(id)).All(x => x);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi nhận phòng: " + ex.Message); }

                if (ok) { MessageBox.Show("Đã nhận phòng.", "Thành công"); RefreshLeft(); RefreshRight(); }
                else MessageBox.Show("Nhận phòng thất bại.", "Lỗi");
                return;
            }

            // Single
            if (_selectedBookingId <= 0 || !IsBooked(_selectedStatus))
            {
                MessageBox.Show("Hãy chọn booking/phòng đang 'Booked'.");
                return;
            }

            if (MessageBox.Show($"Xác nhận nhận phòng cho booking #{_selectedBookingId}?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                var ok = CheckInBooking?.Invoke(_selectedBookingId) ?? false;
                if (ok) { MessageBox.Show("Đã nhận phòng.", "Thành công"); RefreshLeft(); RefreshRight(); }
                else MessageBox.Show("Nhận phòng thất bại.", "Lỗi");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi nhận phòng: " + ex.Message); }
        }

        private void DoCancel()
        {
            var tickBookings = GetCheckedBookings();
            var tickRooms = GetCheckedRooms();

            // Batch booking
            if (tickBookings.Count > 0)
            {
                if (!tickBookings.All(b => IsBooked(b.Status)))
                {
                    MessageBox.Show("Chỉ hủy được booking đang 'Booked'.");
                    return;
                }

                var bookingIds = GetCheckedBookingIds();
                if (MessageBox.Show($"Bạn chắc muốn hủy {bookingIds.Count} booking đã chọn?",
                        "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                bool ok = false;
                try
                {
                    if (CancelBookingsBatch != null) ok = CancelBookingsBatch(bookingIds);
                    else if (CancelBooking != null) ok = bookingIds.Select(id => CancelBooking(id)).All(x => x);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi hủy: " + ex.Message); }

                if (ok) { MessageBox.Show("Đã hủy.", "Thành công"); RefreshLeft(); RefreshRight(); }
                else MessageBox.Show("Hủy thất bại.", "Lỗi");
                return;
            }

            // Batch room
            if (tickRooms.Count > 0)
            {
                if (!tickRooms.All(r => IsBooked(r.RoomStatus)))
                {
                    MessageBox.Show("Chỉ hủy được các phòng đang 'Booked'.");
                    return;
                }

                var keys = GetCheckedRoomKeys();
                if (MessageBox.Show($"Bạn chắc muốn hủy {keys.Count} phòng đã chọn?",
                        "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

                bool ok = false;
                try
                {
                    if (CancelRoomsBatch != null) ok = CancelRoomsBatch(keys);
                    else if (CancelRoom != null) ok = keys.Select(k => CancelRoom(k)).All(x => x);
                    else if (CancelBookingsBatch != null) ok = CancelBookingsBatch(keys.Select(k => k.BookingID).Distinct().ToList());
                    else if (CancelBooking != null) ok = keys.Select(k => k.BookingID).Distinct().Select(id => CancelBooking(id)).All(x => x);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi hủy: " + ex.Message); }

                if (ok) { MessageBox.Show("Đã hủy.", "Thành công"); RefreshLeft(); RefreshRight(); }
                else MessageBox.Show("Hủy thất bại.", "Lỗi");
                return;
            }

            // Single
            if (_selectedBookingId <= 0 || !IsBooked(_selectedStatus))
            {
                MessageBox.Show("Chỉ hủy được booking đang 'Booked'.");
                return;
            }

            if (MessageBox.Show($"Bạn chắc muốn hủy booking #{_selectedBookingId}?",
                    "Xác nhận hủy", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            try
            {
                var ok = CancelBooking?.Invoke(_selectedBookingId) ?? false;
                if (ok) { MessageBox.Show("Đã hủy booking.", "Thành công"); RefreshLeft(); RefreshRight(); }
                else MessageBox.Show("Hủy booking thất bại.", "Lỗi");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi hủy: " + ex.Message); }
        }

        private void DoCheckout()
        {
            var tickBookings = GetCheckedBookings();
            var tickRooms = GetCheckedRooms();

            // Batch booking
            if (tickBookings.Count > 0)
            {
                if (!tickBookings.All(b => IsOccupied(b.Status)))
                {
                    MessageBox.Show("Chỉ 'Trả phòng' được các booking đang 'Đang ở'.");
                    return;
                }

                var bookingIds = GetCheckedBookingIds();

                // Nếu chỉ 1 booking → có thể mở form chi tiết hóa đơn
                if (bookingIds.Count == 1 && OpenBookingDetail != null)
                {
                    try { OpenBookingDetail(bookingIds[0]); }
                    catch (Exception ex) { MessageBox.Show("Không mở được chi tiết: " + ex.Message); }
                }

                if (MessageBox.Show($"Xác nhận trả phòng cho {bookingIds.Count} booking đã chọn?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                bool ok = false;
                try
                {
                    if (CheckoutBookingsBatch != null) ok = CheckoutBookingsBatch(bookingIds);
                    else if (CheckoutBooking != null) ok = bookingIds.Select(id => CheckoutBooking(id)).All(x => x);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi trả phòng: " + ex.Message); }

                if (ok) { MessageBox.Show("Đã trả phòng.", "Thành công"); RefreshLeft(); RefreshRight(); }
                else MessageBox.Show("Trả phòng thất bại.", "Lỗi");
                return;
            }

            // Batch room
            if (tickRooms.Count > 0)
            {
                if (!tickRooms.All(r => IsOccupied(r.RoomStatus)))
                {
                    MessageBox.Show("Chỉ 'Trả phòng' được các phòng đang 'Đang ở'.");
                    return;
                }

                var keys = GetCheckedRoomKeys();
                var bookingIds = keys.Select(k => k.BookingID).Distinct().ToList();

                if (bookingIds.Count == 1 && OpenBookingDetail != null)
                {
                    try { OpenBookingDetail(bookingIds[0]); }
                    catch (Exception ex) { MessageBox.Show("Không mở được chi tiết: " + ex.Message); }
                }

                if (MessageBox.Show($"Xác nhận trả {keys.Count} phòng đã chọn?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                bool ok = false;
                try
                {
                    if (CheckoutRoomsBatch != null) ok = CheckoutRoomsBatch(keys);
                    else if (CheckoutRoom != null) ok = keys.Select(k => CheckoutRoom(k)).All(x => x);
                    else if (CheckoutBookingsBatch != null) ok = CheckoutBookingsBatch(bookingIds);
                    else if (CheckoutBooking != null) ok = bookingIds.Select(id => CheckoutBooking(id)).All(x => x);
                }
                catch (Exception ex) { MessageBox.Show("Lỗi trả phòng: " + ex.Message); }

                if (ok) { MessageBox.Show("Đã trả phòng.", "Thành công"); RefreshLeft(); RefreshRight(); }
                else MessageBox.Show("Trả phòng thất bại.", "Lỗi");
                return;
            }

            // Single
            if (_selectedBookingId <= 0 || !IsOccupied(_selectedStatus))
            {
                MessageBox.Show("Hãy chọn booking/phòng đang 'Đang ở' để trả phòng.");
                return;
            }

            if (OpenBookingDetail != null)
            {
                try { OpenBookingDetail(_selectedBookingId); }
                catch (Exception ex) { MessageBox.Show("Không mở được chi tiết: " + ex.Message); }
            }

            if (MessageBox.Show($"Xác nhận trả phòng cho booking #{_selectedBookingId}?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                var ok = CheckoutBooking?.Invoke(_selectedBookingId) ?? false;
                if (ok) { MessageBox.Show("Đã trả phòng.", "Thành công"); RefreshLeft(); RefreshRight(); }
                else MessageBox.Show("Trả phòng thất bại.", "Lỗi");
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi trả phòng: " + ex.Message); }
        }
        #endregion

        #region Combo defaults
        private void LoadCombos()
        {
            // Trái (booking) — loại Hủy theo mặc định
            cboStatusBooking.Items.Clear();
            cboStatusBooking.Items.Add("Tất cả (trừ hủy)"); // <= mặc định để bạn không thấy Hủy
            cboStatusBooking.Items.Add("Booked");
            cboStatusBooking.Items.Add("Đang ở");           // Occupied/CheckedIn
            cboStatusBooking.Items.Add("Booked/Đang ở");
            cboStatusBooking.Items.Add("Đã hủy");           // nếu muốn xem hủy thì chọn mục này
            cboStatusBooking.Items.Add("Hoàn tất");
            cboStatusBooking.SelectedIndex = 0;

            // Phải (room)
            cboStatusRoom.Items.Clear();
            cboStatusRoom.Items.Add("Tất cả");
            cboStatusRoom.Items.Add("Booked");
            cboStatusRoom.Items.Add("Đang ở");
            cboStatusRoom.Items.Add("Trống");
            cboStatusRoom.SelectedIndex = 0;
        }
        #endregion
    }
}
