using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Response;

namespace HOTEL_MINI.Forms.Controls
{
    /// <summary>
    /// Danh sách các booking-ROOM đang hoạt động (Booked/CheckedIn) + thao tác nhận/hủy/trả.
    /// UI không truy vấn DB trực tiếp; tất cả thông qua BookingService.
    /// </summary>
    public partial class UcBookingRoom : UserControl
    {
        private readonly BookingService _svc = new BookingService();

        private BindingList<BookingDisplay> _view = new BindingList<BookingDisplay>();
        private List<BookingDisplay> _cache = new List<BookingDisplay>();

        private CheckBox _chkHeader;
        private const string COL_SEL = "colSelect";
        private bool _suppressHeaderEvent;

        // Lọc trạng thái
        private static readonly string ST_ALL_NO_CANCEL = "Tất cả (trừ hủy)";
        private static readonly string ST_BOOKED = "Booked";
        private static readonly string ST_OCCUPIED = "Check-in";
        private static readonly string ST_CANCELLED = "Đã hủy";
        private static readonly string ST_COMPLETED = "Hoàn tất";

        // Event để form cha dùng (nếu cần)
        public event Action<int> RequestCheckout;
        public event Action<List<int>> RequestCheckoutMany;

        public UcBookingRoom()
        {
            InitializeComponent();
            InitGridAndUi();
            LoadAll();
        }

        /// <summary>Form cha gán vào để chuyển tiếp cho màn chi tiết.</summary>
        public int CurrentUserId { get; set; }

        public void ReloadData() => LoadAll();

        private static bool IsBooked(string st) =>
            !string.IsNullOrEmpty(st) && st.Equals("Booked", StringComparison.OrdinalIgnoreCase);

        private static bool IsOccupied(string st) =>
            !string.IsNullOrEmpty(st) && (st.Equals("CheckedIn", StringComparison.OrdinalIgnoreCase)
                                          || st.Equals("Occupied", StringComparison.OrdinalIgnoreCase));

        private void InitGridAndUi()
        {
            // ===== Combobox trạng thái =====
            cboStatusBooking.Items.Clear();
            cboStatusBooking.Items.AddRange(new object[]
            {
                ST_ALL_NO_CANCEL, ST_BOOKED, ST_OCCUPIED, ST_CANCELLED, ST_COMPLETED
            });
            cboStatusBooking.SelectedIndex = 0;
            cboStatusBooking.SelectedIndexChanged += delegate { ApplyFilter(); };

            // ===== Grid =====
            var gv = dataGridView1;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.ReadOnly = false; // chỉ cột check cho phép sửa
            gv.MultiSelect = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.RowHeadersVisible = false;
            gv.Columns.Clear();

            // Cột chọn
            var chkCol = new DataGridViewCheckBoxColumn
            {
                HeaderText = "",
                Name = COL_SEL,
                Width = 36
            };
            gv.Columns.Add(chkCol);

            // Cột dữ liệu (đều ReadOnly)
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BookingID", HeaderText = "BookingID", Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerIDNumber", HeaderText = "CCCD", Width = 140, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomNumber", HeaderText = "Phòng", Width = 80, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EmployeeName", HeaderText = "NV tạo", Width = 140, ReadOnly = true });

            var colBook = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingDate",
                HeaderText = "Ngày đặt",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            };
            gv.Columns.Add(colBook);

            var colIn = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckInDate",
                HeaderText = "Check-in",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            };
            gv.Columns.Add(colIn);

            var colOut = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckOutDate",
                HeaderText = "Check-out",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            };
            gv.Columns.Add(colOut);

            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Trạng thái", Width = 100, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Notes", HeaderText = "Ghi chú", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });

            gv.DataSource = _view;

            // Header checkbox
            _chkHeader = new CheckBox { Size = new Size(15, 15) };
            _chkHeader.CheckedChanged += HeaderCheckedChangedProxy;
            gv.Controls.Add(_chkHeader);
            gv.Scroll += delegate { PlaceHeaderChk(); };
            gv.ColumnWidthChanged += delegate { PlaceHeaderChk(); };
            gv.SizeChanged += delegate { PlaceHeaderChk(); };

            gv.CurrentCellDirtyStateChanged += delegate
            {
                if (gv.IsCurrentCellDirty) gv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            gv.CellValueChanged += delegate (object s, DataGridViewCellEventArgs e)
            {
                if (e.RowIndex >= 0 && gv.Columns[e.ColumnIndex].Name == COL_SEL) UpdateButtons();
            };

            // Search CCCD
            txtCCCD.TextChanged += delegate { ApplyFilter(); };

            // Buttons
            button1.Text = "Nhận phòng";
            button2.Text = "Trả phòng";
            button3.Text = "Hủy đặt phòng";
            button1.Click += btnNhan_Click;
            button2.Click += btnTra_Click;
            button3.Click += btnHuy_Click;

            UpdateButtons();
        }

        private void PlaceHeaderChk()
        {
            if (dataGridView1.Columns.Count == 0) return;
            var rect = dataGridView1.GetCellDisplayRectangle(0, -1, true);
            _chkHeader.Location = new Point(rect.X + 10, rect.Y + (rect.Height - _chkHeader.Height) / 2);
        }

        // ================== DATA ==================
        private void LoadAll()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                _cache = _svc.GetActiveBookingDisplays() ?? new List<BookingDisplay>();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        // ================== FILTER ==================
        private void ApplyFilter()
        {
            string cccd = (txtCCCD.Text ?? string.Empty).Trim();
            string st = cboStatusBooking.SelectedItem == null ? null : cboStatusBooking.SelectedItem.ToString();

            IEnumerable<BookingDisplay> q = _cache ?? Enumerable.Empty<BookingDisplay>();

            // Nếu có CCCD: query bổ sung theo CCCD và gộp (tránh trùng)
            if (!string.IsNullOrEmpty(cccd))
            {
                try
                {
                    var more = _svc.GetBookingDisplaysByCustomerNumber(cccd) ?? new List<BookingDisplay>();
                    q = more.Concat(_cache).GroupBy(x => x.BookingID).Select(g => g.First());
                }
                catch
                {
                    // ignore soft error
                }
            }

            if (st == null || st == ST_ALL_NO_CANCEL)
                q = q.Where(x => IsBooked(x.Status) || IsOccupied(x.Status));
            else if (st == ST_BOOKED)
                q = q.Where(x => IsBooked(x.Status));
            else if (st == ST_OCCUPIED)
                q = q.Where(x => IsOccupied(x.Status));
            else if (st == ST_CANCELLED)
                q = q.Where(x => x.Status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase));
            else if (st == ST_COMPLETED)
                q = q.Where(x => x.Status.Equals("CheckedOut", StringComparison.OrdinalIgnoreCase));

            var list = q.OrderByDescending(x => x.BookingDate)
                        .ThenByDescending(x => x.CheckInDate)
                        .ToList();

            _view = new BindingList<BookingDisplay>(list);
            dataGridView1.DataSource = _view;

            _suppressHeaderEvent = true;
            try
            {
                foreach (DataGridViewRow r in dataGridView1.Rows)
                    r.Cells[COL_SEL].Value = false;
                _chkHeader.Checked = false;
            }
            finally
            {
                _suppressHeaderEvent = false;
            }

            UpdateButtons();
        }

        private void HeaderCheckedChangedProxy(object sender, EventArgs e)
        {
            if (_suppressHeaderEvent) return;
            try
            {
                _suppressHeaderEvent = true;
                bool check = _chkHeader.Checked;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                    r.Cells[COL_SEL].Value = check;
            }
            finally
            {
                _suppressHeaderEvent = false;
            }
            UpdateButtons();
        }

        private List<BookingDisplay> GetChecked()
        {
            return dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[COL_SEL].Value is bool && (bool)r.Cells[COL_SEL].Value)
                .Select(r => r.DataBoundItem as BookingDisplay)
                .Where(x => x != null)
                .ToList();
        }

        private void UpdateButtons()
        {
            var sel = GetChecked();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            if (sel.Count == 0) return;

            bool allBooked = sel.All(x => IsBooked(x.Status));
            bool allOcc = sel.All(x => IsOccupied(x.Status));

            if (allBooked)
            {
                button1.Enabled = true; // Nhận phòng
                button3.Enabled = true; // Huỷ
            }
            else if (allOcc)
            {
                button2.Enabled = true; // Trả phòng
            }
        }

        // ================== ACTIONS ==================
        private void btnNhan_Click(object sender, EventArgs e)
        {
            var sel = GetChecked();
            if (sel.Count == 0) return;

            if (!sel.All(x => IsBooked(x.Status)))
            {
                MessageBox.Show("Chỉ nhận những đơn đang 'Booked'.");
                return;
            }

            int ok = 0, fail = 0;
            foreach (var r in sel)
            {
                try
                {
                    // Nếu BookingService của bạn là CheckInBookingRoom (đã chuẩn hoá), dùng dòng dưới.
                    // Nếu vẫn còn hàm CheckInBooking cũ, giữ nguyên theo project của bạn.
                    if (_svc.CheckInBookingRoom(r.BookingID)) ok++;
                    else fail++;
                }
                catch
                {
                    fail++;
                }
            }

            MessageBox.Show("Nhận phòng: thành công " + ok + ", thất bại " + fail + ".");
            LoadAll();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            var sel = GetChecked();
            if (sel.Count == 0) return;

            if (!sel.All(x => IsBooked(x.Status)))
            {
                MessageBox.Show("Chỉ hủy những đơn đang 'Booked'.");
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn chắc muốn hủy " + sel.Count + " booking đã chọn?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            int ok = 0, fail = 0;
            foreach (var r in sel)
            {
                try
                {
                    // Giữ theo service của bạn (CancelBooking hoặc tương đương)
                    if (_svc.CancelBooking(r.BookingID)) ok++;
                    else fail++;
                }
                catch
                {
                    fail++;
                }
            }

            MessageBox.Show("Hủy đặt: thành công " + ok + ", thất bại " + fail + ".");
            LoadAll();
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            var sel = GetChecked();
            if (sel.Count == 0) return;

            if (!sel.All(x => IsOccupied(x.Status)))
            {
                MessageBox.Show("Chỉ trả phòng cho những đơn đang 'CheckedIn'.");
                return;
            }

            var ids = sel.Select(x => x.BookingID).Distinct().ToList();

            try
            {
                // SỬA: mở đúng frmBookingDetail
                using (var dlg = new frmBookingDetail(ids, CurrentUserId))
                {
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    var rs = dlg.ShowDialog(FindForm());
                    if (rs == DialogResult.OK)
                    {
                        LoadAll();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được màn hình trả phòng (frmBookingDetail): " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
