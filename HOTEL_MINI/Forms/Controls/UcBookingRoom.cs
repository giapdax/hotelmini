// UcBookingRoom.cs
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
    public partial class UcBookingRoom : UserControl
    {
        private readonly BookingService _svc = new BookingService();

        private BindingList<BookingDisplay> _view = new BindingList<BookingDisplay>();
        private List<BookingDisplay> _cache = new List<BookingDisplay>(); // đã load

        private CheckBox _chkHeader;
        private const string COL_SEL = "colSelect";
        private bool _suppressHeaderEvent = false;

        // Nhãn filter
        private static readonly string ST_ALL_NO_CANCEL = "Tất cả (trừ hủy)";
        private static readonly string ST_BOOKED = "Booked";
        private static readonly string ST_OCCUPIED = "Check-in";
        private static readonly string ST_CANCELLED = "Đã hủy";
        private static readonly string ST_COMPLETED = "Hoàn tất";

        // (Giữ lại, để form cha có thể dùng nếu muốn)
        public event Action<int> RequestCheckout;            // 1 booking
        public event Action<List<int>> RequestCheckoutMany;  // nhiều booking

        public UcBookingRoom()
        {
            InitializeComponent();
            InitGridAndUi();
            LoadAll();
        }

        // Form cha có thể set vào (để truyền tiếp sang frmBookingDetail1)
        public int CurrentUserId { get; set; }

        public void ReloadData() => LoadAll();

        private void InitGridAndUi()
        {
            // ====== Combobox trạng thái ======
            cboStatusBooking.Items.Clear();
            cboStatusBooking.Items.AddRange(new object[]
            {
                ST_ALL_NO_CANCEL, ST_BOOKED, ST_OCCUPIED, ST_CANCELLED, ST_COMPLETED
            });
            cboStatusBooking.SelectedIndex = 0;
            cboStatusBooking.SelectedIndexChanged += (s, e) => ApplyFilter();

            // ====== Grid ======
            var gv = dataGridView1;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.ReadOnly = false;
            gv.MultiSelect = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.RowHeadersVisible = false;
            gv.Columns.Clear();

            // Checkbox chọn
            gv.Columns.Add(new DataGridViewCheckBoxColumn
            {
                HeaderText = "",
                Name = COL_SEL,
                Width = 36
            });

            // Cột dữ liệu
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "BookingID", HeaderText = "BookingID", Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "CustomerIDNumber", HeaderText = "CCCD", Width = 140, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomNumber", HeaderText = "Phòng", Width = 80, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "EmployeeName", HeaderText = "NV tạo", Width = 140, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BookingDate",
                HeaderText = "Ngày đặt",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckInDate",
                HeaderText = "Check-in",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CheckOutDate",
                HeaderText = "Check-out",
                Width = 130,
                ReadOnly = true,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Trạng thái", Width = 100, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Notes", HeaderText = "Ghi chú", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });

            gv.DataSource = _view;

            // Header checkbox
            _chkHeader = new CheckBox { Size = new Size(15, 15) };
            _chkHeader.CheckedChanged += HeaderCheckedChangedProxy;
            gv.Controls.Add(_chkHeader);
            gv.Scroll += (s, e) => PlaceHeaderChk();
            gv.ColumnWidthChanged += (s, e) => PlaceHeaderChk();
            gv.SizeChanged += (s, e) => PlaceHeaderChk();
            gv.CurrentCellDirtyStateChanged += (s, e) =>
            {
                if (gv.IsCurrentCellDirty) gv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };
            gv.CellValueChanged += (s, e) =>
            {
                if (e.RowIndex >= 0 && gv.Columns[e.ColumnIndex].Name == COL_SEL) UpdateButtons();
            };

            // Search CCCD
            txtCCCD.TextChanged += (s, e) => ApplyFilter();

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
            finally { Cursor.Current = Cursors.Default; }
        }

        // ================== FILTER ==================
        private void ApplyFilter()
        {
            string cccd = (txtCCCD.Text ?? "").Trim();
            string st = cboStatusBooking.SelectedItem?.ToString();

            IEnumerable<BookingDisplay> q = _cache ?? Enumerable.Empty<BookingDisplay>();

            // Nếu nhập CCCD → query thêm rồi gộp (tránh trùng BookingID)
            if (!string.IsNullOrEmpty(cccd))
            {
                try
                {
                    var db = _svc.GetBookingDisplaysByCustomerNumber(cccd) ?? new List<BookingDisplay>();
                    q = db.Concat(_cache).GroupBy(x => x.BookingID).Select(g => g.First());
                }
                catch { /* ignore */ }
            }

            bool IsOcc(string s) => s != null &&
                                    (s.Equals("CheckedIn", StringComparison.OrdinalIgnoreCase)
                                     || s.Equals("Occupied", StringComparison.OrdinalIgnoreCase));

            // Mặc định: chỉ show Booked + CheckedIn
            if (st == null || st == ST_ALL_NO_CANCEL)
                q = q.Where(x => x.Status.Equals("Booked", StringComparison.OrdinalIgnoreCase) || IsOcc(x.Status));
            else if (st == ST_BOOKED)
                q = q.Where(x => x.Status.Equals("Booked", StringComparison.OrdinalIgnoreCase));
            else if (st == ST_OCCUPIED)
                q = q.Where(x => IsOcc(x.Status));
            else if (st == ST_CANCELLED)
                q = q.Where(x => x.Status.Equals("Cancelled", StringComparison.OrdinalIgnoreCase));
            else if (st == ST_COMPLETED)
                q = q.Where(x => x.Status.Equals("CheckedOut", StringComparison.OrdinalIgnoreCase));

            var list = q.OrderByDescending(x => x.BookingDate)
                        .ThenByDescending(x => x.CheckInDate)
                        .ToList();

            _view = new BindingList<BookingDisplay>(list);
            dataGridView1.DataSource = _view;

            // reset tick + header checkbox
            _suppressHeaderEvent = true;
            try
            {
                foreach (DataGridViewRow r in dataGridView1.Rows) r.Cells[COL_SEL].Value = false;
                _chkHeader.Checked = false;
            }
            finally { _suppressHeaderEvent = false; }

            UpdateButtons();
        }

        // ================== HEADER CHECKBOX ==================
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

        // ================== SELECTION ==================
        private List<BookingDisplay> GetChecked() =>
            dataGridView1.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells[COL_SEL].Value is bool b && b)
                .Select(r => r.DataBoundItem as BookingDisplay)
                .Where(x => x != null).ToList();

        private void UpdateButtons()
        {
            var sel = GetChecked();
            button1.Enabled = button2.Enabled = button3.Enabled = false;
            if (sel.Count == 0) return;

            // Chỉ khi toàn bộ là 'Booked' → cho nhận & hủy
            bool allBooked = sel.All(x => x.Status.Equals("Booked", StringComparison.OrdinalIgnoreCase));
            // Chỉ khi toàn bộ là 'CheckedIn/Occupied' → cho trả phòng
            bool allOcc = sel.All(x =>
                x.Status.Equals("CheckedIn", StringComparison.OrdinalIgnoreCase)
             || x.Status.Equals("Occupied", StringComparison.OrdinalIgnoreCase));

            if (allBooked) { button1.Enabled = true; button3.Enabled = true; }
            else if (allOcc) { button2.Enabled = true; }
        }

        // ================== ACTIONS ==================
        private void btnNhan_Click(object sender, EventArgs e)
        {
            var sel = GetChecked();
            if (sel.Count == 0) return;
            if (!sel.All(x => x.Status.Equals("Booked", StringComparison.OrdinalIgnoreCase)))
            { MessageBox.Show("Chỉ nhận những đơn đang 'Booked'."); return; }

            int ok = 0, fail = 0;
            foreach (var r in sel)
            {
                try { if (_svc.CheckInBooking(r.BookingID)) ok++; else fail++; }
                catch { fail++; }
            }
            MessageBox.Show($"Nhận phòng: thành công {ok}, thất bại {fail}.");
            LoadAll();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            var sel = GetChecked();
            if (sel.Count == 0) return;
            if (!sel.All(x => x.Status.Equals("Booked", StringComparison.OrdinalIgnoreCase)))
            { MessageBox.Show("Chỉ hủy những đơn đang 'Booked'."); return; }

            if (MessageBox.Show($"Bạn chắc muốn hủy {sel.Count} booking đã chọn?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            int ok = 0, fail = 0;
            foreach (var r in sel)
            {
                try { if (_svc.CancelBooking(r.BookingID)) ok++; else fail++; }
                catch { fail++; }
            }
            MessageBox.Show($"Hủy đặt: thành công {ok}, thất bại {fail}.");
            LoadAll();
        }

        private void btnTra_Click(object sender, EventArgs e)
        {
            var sel = GetChecked();
            if (sel.Count == 0) return;

            if (!sel.All(x => x.Status.Equals("CheckedIn", StringComparison.OrdinalIgnoreCase)
                           || x.Status.Equals("Occupied", StringComparison.OrdinalIgnoreCase)))
            {
                MessageBox.Show("Chỉ trả phòng cho những đơn đang 'CheckedIn'.");
                return;
            }

            var ids = sel.Select(x => x.BookingID).Distinct().ToList();

            // ĐÚNG FLOW: mở frmBookingDetail1 trước
            try
            {
                using (var dlg = new frmBookingDetail1(ids, CurrentUserId))
                {
                    dlg.StartPosition = FormStartPosition.CenterParent;
                    var rs = dlg.ShowDialog(this.FindForm()); // modal theo form cha
                    if (rs == DialogResult.OK)
                    {
                        // Sau khi hoàn tất ở BookingDetail1 (có thể đã checkout đủ)
                        LoadAll(); // reload list để cập nhật trạng thái
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được màn hình trả phòng (BookingDetail1): " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
