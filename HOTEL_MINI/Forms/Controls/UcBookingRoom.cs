using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms.Controls
{
    public partial class UcBookingRoom : UserControl
    {
        private readonly BookingService _svc = new BookingService();

        private BindingList<BookingFlatDisplay> _binding = new BindingList<BookingFlatDisplay>();
        private List<BookingFlatDisplay> _all = new List<BookingFlatDisplay>();

        private CheckBox _chkHeader;
        private const string COL_SEL = "colSelect";
        private bool _suppressHeaderEvent;

        private static readonly string ST_ALL = "Tất cả";
        private static readonly string ST_BOOKED_VN = "Chưa nhận";
        private static readonly string ST_OCCUPIED_VN = "Nhận rồi";
        private static readonly string ST_CANCELLED_VN = "Đã hủy";
        private static readonly string ST_COMPLETED_VN = "Đã trả phòng";

        public int CurrentUserId { get; set; }
        public event Action<int> RequestCheckout;
        public event Action<List<int>> RequestCheckoutMany;

        public UcBookingRoom()
        {
            InitializeComponent();
            InitGrid();
            InitUi();
            LoadData();
        }

        public void ReloadData() => LoadData();

        private void InitUi()
        {
            cboStatusBooking.Items.AddRange(new object[] { ST_ALL, ST_BOOKED_VN, ST_OCCUPIED_VN, ST_CANCELLED_VN, ST_COMPLETED_VN });
            cboStatusBooking.SelectedIndex = 0;
            cboStatusBooking.SelectedIndexChanged += (s, e) => ApplyFilter();
            txtSearch.TextChanged += (s, e) => ApplyFilter();
        }

        private void InitGrid()
        {
            var gv = dataGridView1;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.MultiSelect = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.RowHeadersVisible = false;
            gv.ReadOnly = false;
            gv.Columns.Clear();

            var chkCol = new DataGridViewCheckBoxColumn { Name = COL_SEL, HeaderText = "", Width = 35 };
            gv.Columns.Add(chkCol);
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Trạng thái", Width = 120, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HeaderBookingID", HeaderText = "BookingID", Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerIDNumber", HeaderText = "CCCD", Width = 140, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomNumber", HeaderText = "Phòng", Width = 80, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EmployeeName", HeaderText = "NV tạo", Width = 140, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BookingDate", HeaderText = "Ngày đặt", Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CheckInDate", HeaderText = "Check-in", Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CheckOutDate", HeaderText = "Check-out", Width = 130, DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Notes", HeaderText = "Ghi chú", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });

            _chkHeader = new CheckBox { Size = new Size(15, 15) };
            _chkHeader.CheckedChanged += HeaderCheckedChangedProxy;
            gv.Controls.Add(_chkHeader);

            gv.Scroll += (s, e) => PlaceHeaderChk();
            gv.ColumnWidthChanged += (s, e) => PlaceHeaderChk();
            gv.SizeChanged += (s, e) => PlaceHeaderChk();
            gv.CurrentCellDirtyStateChanged += (s, e) => { if (gv.IsCurrentCellDirty) gv.CommitEdit(DataGridViewDataErrorContexts.Commit); };
            gv.CellValueChanged += (s, e) => { if (e.RowIndex >= 0 && gv.Columns[e.ColumnIndex].Name == COL_SEL) UpdateButtons(); };
            gv.CellFormatting += GridView_CellFormatting;

            gv.CellDoubleClick += (s, e) =>
            {
                if (e.RowIndex < 0) return;
                var rowData = gv.Rows[e.RowIndex].DataBoundItem;
                if (!(rowData is BookingFlatDisplay item)) return;
                var lineIds = new List<int> { item.BookingRoomID };
                var app = TopLevelControl as frmApplication;
                if (app != null) app.OpenChildForm(new frmBookingDetail(lineIds, CurrentUserId), null);
            };
        }

        private void GridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var gv = sender as DataGridView;
            if (gv == null) return;

            if (gv.Columns[e.ColumnIndex].DataPropertyName == "Status" && gv.Rows[e.RowIndex].DataBoundItem is BookingFlatDisplay booking)
            {
                var raw = (booking.Status ?? "").Trim().ToLowerInvariant();
                var font = new Font(gv.Font, FontStyle.Bold);

                if (raw == "booked")
                {
                    e.Value = ST_BOOKED_VN;
                    e.CellStyle.ForeColor = Color.Black;
                    e.CellStyle.BackColor = Color.FromArgb(255, 193, 7);
                    e.CellStyle.Font = font;
                    e.FormattingApplied = true;
                }
                else if (raw == "checkedin" || raw == "occupied")
                {
                    e.Value = ST_OCCUPIED_VN;
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.FromArgb(40, 167, 69);
                    e.CellStyle.Font = font;
                    e.FormattingApplied = true;
                }
                else if (raw == "checkedout")
                {
                    e.Value = ST_COMPLETED_VN;
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.FromArgb(52, 58, 64);
                    e.CellStyle.Font = font;
                    e.FormattingApplied = true;
                }
                else if (raw == "cancelled")
                {
                    e.Value = ST_CANCELLED_VN;
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.FromArgb(220, 53, 69);
                    e.CellStyle.Font = font;
                    e.FormattingApplied = true;
                }
            }
        }

        private void PlaceHeaderChk()
        {
            if (dataGridView1.Columns.Count == 0) return;
            var rect = dataGridView1.GetCellDisplayRectangle(0, -1, true);
            _chkHeader.Location = new Point(rect.X + 10, rect.Y + (rect.Height - _chkHeader.Height) / 2);
        }

        private void LoadData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                _all = _svc.GetAllBookingFlatDisplays() ?? new List<BookingFlatDisplay>();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load booking: " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private static bool IsBooked(string s) =>
            !string.IsNullOrEmpty(s) && s.Equals("Booked", StringComparison.OrdinalIgnoreCase);

        private static bool IsOccupied(string s) =>
            !string.IsNullOrEmpty(s) && (s.Equals("CheckedIn", StringComparison.OrdinalIgnoreCase) || s.Equals("Occupied", StringComparison.OrdinalIgnoreCase));

        private void ApplyFilter()
        {
            string q = (txtSearch.Text ?? "").Trim().ToLower();
            string st = cboStatusBooking.SelectedItem?.ToString();

            IEnumerable<BookingFlatDisplay> list = _all;

            if (!string.IsNullOrEmpty(q))
            {
                list = list.Where(x =>
                    (x.RoomNumber ?? "").ToLower().Contains(q) ||
                    (x.CustomerIDNumber ?? "").ToLower().Contains(q) ||
                    (x.EmployeeName ?? "").ToLower().Contains(q) ||
                    (x.Status ?? "").ToLower().Contains(q) ||
                    x.HeaderBookingID.ToString().Contains(q) ||
                    x.BookingRoomID.ToString().Contains(q));
            }

            if (st == ST_BOOKED_VN) list = list.Where(x => IsBooked(x.Status));
            else if (st == ST_OCCUPIED_VN) list = list.Where(x => IsOccupied(x.Status));
            else if (st == ST_CANCELLED_VN) list = list.Where(x => (x.Status ?? "").Equals("Cancelled", StringComparison.OrdinalIgnoreCase));
            else if (st == ST_COMPLETED_VN) list = list.Where(x => (x.Status ?? "").Equals("CheckedOut", StringComparison.OrdinalIgnoreCase));

            var sorted = list.OrderByDescending(x => x.BookingDate).ToList();
            _binding = new BindingList<BookingFlatDisplay>(sorted);
            dataGridView1.DataSource = _binding;

            _suppressHeaderEvent = true;
            try
            {
                foreach (DataGridViewRow r in dataGridView1.Rows) r.Cells[COL_SEL].Value = false;
                _chkHeader.Checked = false;
            }
            finally { _suppressHeaderEvent = false; }

            UpdateButtons();
        }

        private void HeaderCheckedChangedProxy(object sender, EventArgs e)
        {
            if (_suppressHeaderEvent) return;
            try
            {
                _suppressHeaderEvent = true;
                bool check = _chkHeader.Checked;
                foreach (DataGridViewRow r in dataGridView1.Rows) r.Cells[COL_SEL].Value = check;
            }
            finally { _suppressHeaderEvent = false; }
            UpdateButtons();
        }

        private List<BookingFlatDisplay> GetChecked()
        {
            return dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[COL_SEL].Value is bool b && b)
                .Select(r => r.DataBoundItem as BookingFlatDisplay)
                .Where(x => x != null)
                .ToList();
        }

        private void UpdateButtons()
        {
            var sel = GetChecked();
            btnNhanphong.Enabled = btnTraphong.Enabled = btnHuydat.Enabled = false;
            if (sel.Count == 0) return;

            bool allBooked = sel.All(x => IsBooked(x.Status));
            bool allOcc = sel.All(x => IsOccupied(x.Status));

            if (allBooked) { btnNhanphong.Enabled = true; btnHuydat.Enabled = true; }
            else if (allOcc) { btnTraphong.Enabled = true; }
        }

        private void EnsureCommitEdits()
        {
            if (dataGridView1.IsCurrentCellDirty) dataGridView1.EndEdit();
            dataGridView1.EndEdit();
        }

        private void btnHuydat_Click(object sender, EventArgs e)
        {
            EnsureCommitEdits();

            var sel = GetChecked();
            if (sel.Count == 0) return;

            if (!sel.All(x => IsBooked(x.Status)))
            {
                MessageBox.Show("Chỉ hủy được đơn đang 'Booked'.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn hủy {sel.Count} booking này?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            int ok = 0, fail = 0;
            foreach (var item in sel)
            {
                try
                {
                    if (_svc.CancelBooking(item.BookingRoomID)) ok++;
                    else fail++;
                }
                catch { fail++; }
            }

            MessageBox.Show($"Hủy đặt phòng: {ok} thành công, {fail} thất bại.");
            LoadData();
        }

        private void btnNhanphong_Click(object sender, EventArgs e)
        {
            EnsureCommitEdits();

            var sel = GetChecked();
            if (sel.Count == 0) return;

            if (!sel.All(x => IsBooked(x.Status)))
            {
                MessageBox.Show("Chỉ nhận phòng cho đơn đang 'Booked'.");
                return;
            }

            int ok = 0, fail = 0;
            foreach (var item in sel)
            {
                try
                {
                    if (_svc.CheckInBookingRoom(item.BookingRoomID)) ok++;
                    else fail++;
                }
                catch { fail++; }
            }

            MessageBox.Show($"Nhận phòng: {ok} thành công, {fail} thất bại.");
            LoadData();
        }

        private void btnTraphong_Click(object sender, EventArgs e)
        {
            EnsureCommitEdits();

            var sel = GetChecked();
            if (sel.Count == 0)
            {
                MessageBox.Show("Hãy chọn ít nhất 1 phòng.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!sel.All(x => IsOccupied(x.Status)))
            {
                MessageBox.Show("Chỉ trả phòng cho đơn đang 'CheckedIn'.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int firstBookingId = sel.First().HeaderBookingID;
            bool allSameBooking = sel.All(x => x.HeaderBookingID == firstBookingId);
            if (!allSameBooking)
            {
                MessageBox.Show("Các phòng bạn chọn không cùng đơn đặt phòng. Vui lòng chọn lại.", "Không hợp lệ",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var lineIds = sel.Select(x => x.BookingRoomID).Distinct().ToList();
            var app = TopLevelControl as frmApplication;
            if (app != null)
            {
                app.OpenChildForm(new frmBookingDetail(lineIds, CurrentUserId), null);
            }
            else
            {
                MessageBox.Show("Không tìm thấy frmApplication để mở màn hình con.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
