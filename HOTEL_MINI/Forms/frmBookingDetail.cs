using HOTEL_MINI.BLL;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms
{
    public partial class frmBookingDetail : Form
    {
        // ====== Services / BLL / Repo ======
        private readonly ServicesService _svc = new ServicesService();                        // Danh mục + tồn kho DV
        private readonly BookingRoomServiceService _brsSvc = new BookingRoomServiceService(); // Nghiệp vụ DV theo line/phòng
        private readonly BookingService _bookingSvc = new BookingService();                   // Header + đọc line
        private readonly RoomPricingRepository _pricingRepo = new RoomPricingRepository();    // Đọc pricing
        private readonly CustomerService _customerSvc = new CustomerService();
        private readonly RoomService _roomSvc = new RoomService();

        // ====== UI state ======
        private BindingList<Service> _servicesList;
        private BindingList<CheckoutRow> _checkoutRows;
        private DataTable _usedServicesTable;

        // input
        private readonly List<int> _bookingRoomIds; // danh sách line-id (trong entity hiện tại là BookingID)
        private readonly int _currentUserId;

        // cache map
        private readonly Dictionary<int, string> _roomNumberByRoomId = new Dictionary<int, string>();
        // map: line-id(=BookingID hiện tại) -> RoomID
        private readonly Dictionary<int, int> _roomIdByLineId = new Dictionary<int, int>();

        // ====== Ctor ======
        public frmBookingDetail(List<int> bookingRoomIds, int currentUserId)
        {
            InitializeComponent();

            _bookingRoomIds = bookingRoomIds ?? new List<int>();
            _currentUserId = currentUserId;

            // events
            Load += FrmBookingDetail_Load;
            btnClose.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            btnIncrease.Click += BtnIncrease_Click;
            btnReduce.Click += BtnReduce_Click;

            btnTraPhong.Text = "Trả phòng";
            btnTraPhong.Click += BtnCheckout_Click;

            // numeric defaults
            nbrIncrease.Minimum = 1; nbrIncrease.Maximum = 999; nbrIncrease.Value = 1;
            nbrReduce.Minimum = 1; nbrReduce.Maximum = 999; nbrReduce.Value = 1;

            // input chỉ hiển thị
            txtCCCD.ReadOnly = true;
            txtTen.ReadOnly = true;
            txtGender.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtDiachi.ReadOnly = true;
            txtPricingType.ReadOnly = true;
            btnCheck.Enabled = false;
        }

        public frmBookingDetail() : this(new List<int>(), 0) { }

        // ====== VM hiển thị ======
        private class CheckoutRow
        {
            public bool Pick { get; set; } = true;
            public int BookingRoomID { get; set; } // line-id (trong entity hiện tại là BookingID)
            public int RoomID { get; set; }
            public string RoomNumber { get; set; }
            public DateTime? CheckIn { get; set; }
            public DateTime? CheckOut { get; set; }
            public string PricingType { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal RoomCharge { get; set; }
            public decimal ServiceTotal { get; set; }
            public decimal GrandTotal { get { return RoomCharge + ServiceTotal; } }
        }

        // ====== Load ======
        private void FrmBookingDetail_Load(object sender, EventArgs e)
        {
            try
            {
                if (_bookingRoomIds == null || _bookingRoomIds.Count == 0)
                    throw new InvalidOperationException("Không có BookingRoomID nào.");

                EnsureSameHeader(_bookingRoomIds); // tất cả line phải thuộc cùng 1 header Booking

                SetupRoomsGrid();
                SetupServicesMenu();
                SetupUsedServicesTable();
                BindUsedServicesGrid();

                LoadInitialData(_bookingRoomIds);

                txtPricingType.Text = string.Join(", ",
                    _checkoutRows.Select(r => r.PricingType)
                                 .Distinct()
                                 .Where(x => !string.IsNullOrWhiteSpace(x)));

                lblBookingID.Text = "Phòng " + _checkoutRows.Count + " phòng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        /// <summary> Đảm bảo các line-id cùng 1 BookingID (header). </summary>
        private void EnsureSameHeader(List<int> bookingRoomIds)
        {
            var headerIds = bookingRoomIds
                .Distinct()
                .Select(id => _bookingSvc.GetHeaderIdByBookingRoomId(id)) // id đang là line-id (BookingID hiện tại)
                .ToList();

            if (headerIds.Any(h => !h.HasValue))
                throw new InvalidOperationException("Có phòng không thuộc đơn đặt phòng hợp lệ.");

            var firstHeader = headerIds.First().Value;
            if (headerIds.Any(h => h.Value != firstHeader))
                throw new InvalidOperationException("Các phòng không cùng một Booking.");
        }

        // ====== UI setup ======
        private void SetupRoomsGrid()
        {
            var gv = dataGridView1;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.MultiSelect = false;
            gv.ReadOnly = false;
            gv.RowHeadersVisible = false;
            gv.Columns.Clear();

            var money = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight, Format = "N0" };
            var dt = new DataGridViewCellStyle { Format = "dd/MM HH:mm" };

            gv.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = nameof(CheckoutRow.Pick), HeaderText = "Chọn", Width = 50, ReadOnly = false });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.RoomNumber), HeaderText = "Phòng", Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.CheckIn), HeaderText = "Check-in", DefaultCellStyle = dt, Width = 110, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.CheckOut), HeaderText = "Check-out", DefaultCellStyle = dt, Width = 110, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.PricingType), HeaderText = "Giá theo", Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.UnitPrice), HeaderText = "Đơn giá", DefaultCellStyle = money, Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.RoomCharge), HeaderText = "Tiền phòng", DefaultCellStyle = money, Width = 110, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.ServiceTotal), HeaderText = "Tổng DV", DefaultCellStyle = money, Width = 110, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = nameof(CheckoutRow.GrandTotal), HeaderText = "Tổng cộng", DefaultCellStyle = money, Width = 120, ReadOnly = true });

            _checkoutRows = new BindingList<CheckoutRow>();
            gv.DataSource = _checkoutRows;
        }

        private void SetupServicesMenu()
        {
            var gv = dgvHotelServices;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.MultiSelect = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServiceID", HeaderText = "ID", Width = 50, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServiceName", HeaderText = "Dịch vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Price", HeaderText = "Đơn giá", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Tồn kho", Width = 80, ReadOnly = true });

            _servicesList = new BindingList<Service>(_svc.GetAllServices().Where(s => s.IsActive).ToList());
            gv.DataSource = _servicesList;
        }

        private void SetupUsedServicesTable()
        {
            _usedServicesTable = new DataTable();
            _usedServicesTable.Columns.Add("BookingRoomID", typeof(int)); // line-id
            _usedServicesTable.Columns.Add("RoomID", typeof(int));
            _usedServicesTable.Columns.Add("RoomNumber", typeof(string));
            _usedServicesTable.Columns.Add("ServiceID", typeof(int));
            _usedServicesTable.Columns.Add("ServiceName", typeof(string));
            _usedServicesTable.Columns.Add("UnitPrice", typeof(decimal));
            _usedServicesTable.Columns.Add("Quantity", typeof(int));
            _usedServicesTable.Columns.Add("Total", typeof(decimal), "UnitPrice * Quantity");
        }

        private void BindUsedServicesGrid()
        {
            var gv = dgvUsedServices;
            gv.AutoGenerateColumns = false;
            gv.AllowUserToAddRows = false;
            gv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gv.MultiSelect = false;
            gv.Columns.Clear();

            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RoomNumber", HeaderText = "Phòng", Width = 90, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ServiceName", HeaderText = "Dịch vụ", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UnitPrice", HeaderText = "Đơn giá", Width = 90, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "SL", Width = 60, ReadOnly = true });
            gv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Total", HeaderText = "Thành tiền", Width = 110, ReadOnly = true, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" } });

            dgvUsedServices.DataSource = _usedServicesTable;
        }

        // ====== Load data ======
        private void LoadInitialData(List<int> bookingRoomIds)
        {
            // Lấy line/phòng đầu tiên để suy ra header rồi lấy khách
            var firstLine = _bookingSvc.GetBookingById(bookingRoomIds[0]); // NOTE: trả về đối tượng line; line-id hiện tại là BookingID
            if (firstLine != null)
            {
                // Lấy header từ line-id (ở entity hiện tại: BookingID)
                var headerIdOpt = _bookingSvc.GetHeaderIdByBookingRoomId(firstLine.BookingID);
                if (headerIdOpt.HasValue)
                {
                    var custFull = _bookingSvc.GetCustomerByHeaderId(headerIdOpt.Value);
                    if (custFull != null)
                    {
                        txtCCCD.Text = custFull.IDNumber ?? "";
                        txtTen.Text = custFull.FullName ?? "";
                        txtDiachi.Text = custFull.Address ?? "";
                        txtGender.Text = custFull.Gender ?? "";
                        txtEmail.Text = custFull.Email ?? "";
                        txtSDT.Text = custFull.Phone ?? "";
                    }
                }
            }

            foreach (var lineId in bookingRoomIds.Distinct())
            {
                var b = _bookingSvc.GetBookingById(lineId); // đọc theo line-id
                if (b == null) continue;

                // Bổ sung CheckOut mặc định nếu thiếu/không hợp lệ
                if (!b.CheckOutDate.HasValue || (b.CheckInDate.HasValue && b.CheckOutDate <= b.CheckInDate))
                    b.CheckOutDate = DateTime.Now;

                // Cache roomNo & map line-id -> RoomID
                var roomNo = _roomSvc.GetRoomNumberById(b.RoomID);
                _roomNumberByRoomId[b.RoomID] = roomNo;
                _roomIdByLineId[b.BookingID] = b.RoomID;

                // Pricing
                var pricing = _pricingRepo.GetPricingTypeById(b.PricingID);
                var unit = pricing != null ? pricing.Price : 0m;
                var ptype = pricing != null ? pricing.PricingType : string.Empty;

                // Tiền phòng (tính theo line b)
                decimal roomCharge = 0m;
                try { roomCharge = _bookingSvc.GetRoomCharge(b); } catch { }

                // Dịch vụ đã dùng theo line/phòng (note: line-id = BookingID hiện tại)
                var svcs = _brsSvc.GetUsedServicesByBookingRoomId(b.BookingID);
                decimal svcTotal = svcs.Sum(x => x.Price * x.Quantity);

                // Add row grid phòng
                _checkoutRows.Add(new CheckoutRow
                {
                    Pick = true,
                    BookingRoomID = b.BookingID, // line-id
                    RoomID = b.RoomID,
                    RoomNumber = roomNo,
                    CheckIn = b.CheckInDate,
                    CheckOut = b.CheckOutDate,
                    PricingType = ptype,
                    UnitPrice = unit,
                    RoomCharge = roomCharge,
                    ServiceTotal = svcTotal
                });

                // Add rows grid dịch vụ đã dùng
                foreach (var s in svcs)
                {
                    var row = _usedServicesTable.NewRow();
                    row["BookingRoomID"] = b.BookingID; // line-id
                    row["RoomID"] = b.RoomID;
                    row["RoomNumber"] = roomNo;
                    row["ServiceID"] = s.ServiceID;
                    row["ServiceName"] = s.ServiceName;
                    row["UnitPrice"] = s.Price;
                    row["Quantity"] = s.Quantity;
                    _usedServicesTable.Rows.Add(row);
                }
            }

            dataGridView1.Refresh();
            RecalcServiceTotalsForRows();
        }

        // ====== Helpers ======
        private List<int> GetPickedBookingRoomIds()
        {
            return (_checkoutRows ?? new BindingList<CheckoutRow>())
                   .Where(x => x.Pick)
                   .Select(x => x.BookingRoomID) // đang map từ b.BookingID
                   .Distinct()
                   .ToList();
        }

        private void RecalcServiceTotalsForRows()
        {
            foreach (var r in _checkoutRows)
            {
                var svcTotal = _usedServicesTable.AsEnumerable()
                    .Where(x => x.Field<int>("BookingRoomID") == r.BookingRoomID)
                    .Sum(x => x.Field<decimal>("UnitPrice") * x.Field<int>("Quantity"));

                r.ServiceTotal = svcTotal;
            }
            dataGridView1.Refresh();
        }

        // ====== Button events ======
        private void BtnIncrease_Click(object sender, EventArgs e)
        {
            var svc = dgvHotelServices.CurrentRow != null
                ? dgvHotelServices.CurrentRow.DataBoundItem as Service
                : null;

            if (svc == null)
            {
                MessageBox.Show("Chọn 1 dịch vụ trong menu bên phải.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int qty = (int)nbrIncrease.Value;
            if (qty <= 0) qty = 1;

            var pickedLineIds = GetPickedBookingRoomIds();
            if (pickedLineIds.Count == 0)
            {
                MessageBox.Show("Hãy tick phòng (cột 'Chọn') trước khi thêm.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // BLL: reserve tồn + upsert line theo các line-id (atomic)
                var result = _brsSvc.AddServiceToRooms(pickedLineIds, svc.ServiceID, qty);
                int remaining = result.remainingAfter;

                // Update bảng dịch vụ đã dùng theo từng phòng
                foreach (var lineId in pickedLineIds)
                {
                    int roomId;
                    if (!_roomIdByLineId.TryGetValue(lineId, out roomId)) continue;

                    string roomNo = _roomNumberByRoomId.ContainsKey(roomId) ? _roomNumberByRoomId[roomId] : "";

                    var existing = _usedServicesTable.AsEnumerable().FirstOrDefault(r =>
                        r.Field<int>("BookingRoomID") == lineId &&
                        r.Field<int>("ServiceID") == svc.ServiceID);

                    if (existing != null)
                    {
                        existing["Quantity"] = existing.Field<int>("Quantity") + qty;
                    }
                    else
                    {
                        var row = _usedServicesTable.NewRow();
                        row["BookingRoomID"] = lineId;
                        row["RoomID"] = roomId;
                        row["RoomNumber"] = roomNo;
                        row["ServiceID"] = svc.ServiceID;
                        row["ServiceName"] = svc.ServiceName;
                        row["UnitPrice"] = svc.Price;
                        row["Quantity"] = qty;
                        _usedServicesTable.Rows.Add(row);
                    }
                }

                // Sync tồn kho trong menu dịch vụ
                if (_servicesList != null)
                {
                    var item = _servicesList.FirstOrDefault(x => x.ServiceID == svc.ServiceID);
                    if (item != null)
                    {
                        item.Quantity = remaining; // lấy số còn lại từ BLL
                        dgvHotelServices.Refresh();
                    }
                }

                RecalcServiceTotalsForRows();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể thêm dịch vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReduce_Click(object sender, EventArgs e)
        {
            if (dgvUsedServices.CurrentRow == null)
            {
                MessageBox.Show("Chọn dịch vụ muốn bớt.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var drv = dgvUsedServices.CurrentRow.DataBoundItem as DataRowView;
            if (drv == null) return;

            int reduceBy = (int)nbrReduce.Value;
            if (reduceBy <= 0) reduceBy = 1;

            int bookingRoomId = Convert.ToInt32(drv.Row["BookingRoomID"]); // line-id
            int serviceId = Convert.ToInt32(drv.Row["ServiceID"]);
            int currentQty = Convert.ToInt32(drv.Row["Quantity"]);

            try
            {
                int actuallyRemoved = _brsSvc.ReduceServiceQuantity(bookingRoomId, serviceId, reduceBy);
                if (actuallyRemoved > 0)
                {
                    int left = currentQty - actuallyRemoved;
                    if (left <= 0) _usedServicesTable.Rows.Remove(drv.Row);
                    else drv.Row["Quantity"] = left;

                    // hoàn kho trên menu
                    if (_servicesList != null)
                    {
                        var menuItem = _servicesList.FirstOrDefault(x => x.ServiceID == serviceId);
                        if (menuItem != null)
                        {
                            menuItem.Quantity = menuItem.Quantity + actuallyRemoved;
                            dgvHotelServices.Refresh();
                        }
                    }

                    RecalcServiceTotalsForRows();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể bớt dịch vụ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            // Commit mọi thay đổi checkbox trước khi đọc
            if (dataGridView1.IsCurrentCellDirty) dataGridView1.EndEdit();
            dataGridView1.EndEdit();

            var picked = (_checkoutRows ?? new BindingList<CheckoutRow>())
                         .Where(x => x.Pick)
                         .ToList();

            if (picked.Count == 0)
            {
                MessageBox.Show("Chọn ít nhất 1 phòng để trả.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy list line-id (BookingRoomID hiện tại = BookingID của line)
            var lineIds = picked.Select(x => x.BookingRoomID)
                                .Distinct()
                                .ToList();

            try
            {
                // Resolve header từ line đầu tiên
                int? headerIdOpt = _bookingSvc.GetHeaderIdByBookingRoomId(lineIds[0]);
                if (!headerIdOpt.HasValue)
                {
                    MessageBox.Show("Không xác định được đơn đặt phòng (header).", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int headerId = headerIdOpt.Value;

                // Đảm bảo tất cả line đều cùng 1 header
                foreach (var id in lineIds)
                {
                    var h = _bookingSvc.GetHeaderIdByBookingRoomId(id);
                    if (!h.HasValue || h.Value != headerId)
                    {
                        MessageBox.Show("Các phòng được chọn không thuộc cùng một Booking.", "Cảnh báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Mở form Checkout: (headerId, roomLineIds, currentUserId)
                using (var f = new frmCheckout(headerId, lineIds, _currentUserId))
                {
                    f.StartPosition = FormStartPosition.CenterParent;
                    var rs = f.ShowDialog(this);
                    if (rs == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể mở màn hình trả phòng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }
}
