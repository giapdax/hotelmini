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
        private readonly ServicesService _svc = new ServicesService();
        private readonly BookingRoomServiceService _brsSvc = new BookingRoomServiceService(); // BLL dịch vụ cho line (đúng tên)
        private readonly BookingService _bookingSvc = new BookingService();                     // BLL header + read line
        private readonly RoomPricingRepository _pricingRepo = new RoomPricingRepository();
        private readonly CustomerService _customerSvc = new CustomerService();
        private readonly RoomService _roomSvc = new RoomService();

        private BindingList<Service> _servicesList;
        private BindingList<CheckoutRow> _checkoutRows;
        private DataTable _usedServicesTable;

        private readonly List<int> _bookingRoomIds;
        private readonly int _currentUserId;

        private readonly Dictionary<int, string> _roomNumberByRoomId = new Dictionary<int, string>();
        private readonly Dictionary<int, int> _roomIdByBookingRoomId = new Dictionary<int, int>();

        public frmBookingDetail(List<int> bookingRoomIds, int currentUserId)
        {
            InitializeComponent();
            _bookingRoomIds = bookingRoomIds ?? new List<int>();
            _currentUserId = currentUserId;

            Load += FrmBookingDetail_Load;
            btnClose.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            btnIncrease.Click += BtnIncrease_Click;
            btnReduce.Click += BtnReduce_Click;

            btnDatphong.Text = "Trả phòng";
            btnDatphong.Click += BtnCheckout_Click;

            nbrIncrease.Minimum = 1; nbrIncrease.Maximum = 999; nbrIncrease.Value = 1;
            nbrReduce.Minimum = 1; nbrReduce.Maximum = 999; nbrReduce.Value = 1;

            // Các input khách hàng chỉ để hiển thị
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

        private class CheckoutRow
        {
            public bool Pick { get; set; } = true;
            public int BookingRoomID { get; set; }
            public int RoomID { get; set; }
            public string RoomNumber { get; set; }
            public DateTime? CheckIn { get; set; }
            public DateTime? CheckOut { get; set; }
            public string PricingType { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal RoomCharge { get; set; }
            public decimal ServiceTotal { get; set; }
            public decimal GrandTotal => RoomCharge + ServiceTotal;
        }

        private void FrmBookingDetail_Load(object sender, EventArgs e)
        {
            try
            {
                if (_bookingRoomIds == null || _bookingRoomIds.Count == 0)
                    throw new InvalidOperationException("Không có BookingRoomID nào.");

                EnsureSameHeader(_bookingRoomIds);

                SetupRoomsGrid();
                SetupServicesMenu();
                SetupUsedServicesTable();
                BindUsedServicesGrid();

                LoadInitialData(_bookingRoomIds);

                txtPricingType.Text = string.Join(", ",
                    _checkoutRows.Select(r => r.PricingType).Distinct().Where(x => !string.IsNullOrWhiteSpace(x)));
                lblBookingID.Text = "Trả phòng - " + _checkoutRows.Count + " phòng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void EnsureSameHeader(List<int> bookingRoomIds)
        {
            var headerIds = bookingRoomIds.Distinct()
                                          .Select(id => _bookingSvc.GetHeaderIdByBookingRoomId(id))
                                          .ToList();
            if (headerIds.Any(h => !h.HasValue))
                throw new InvalidOperationException("Có phòng không thuộc đơn đặt phòng hợp lệ.");

            var firstHeader = headerIds.First().Value;
            if (headerIds.Any(h => h.Value != firstHeader))
                throw new InvalidOperationException("Các phòng không cùng một Booking.");
        }

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
            _usedServicesTable.Columns.Add("BookingRoomID", typeof(int));
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

        private void LoadInitialData(List<int> bookingRoomIds)
        {
            // Lấy booking đầu tiên và thông tin khách qua BookingService
            var first = _bookingSvc.GetBookingById(bookingRoomIds[0]);
            if (first != null)
            {
                var headerIdOpt = _bookingSvc.GetHeaderIdByBookingRoomId(first.BookingID);
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

            foreach (var bid in bookingRoomIds.Distinct())
            {
                var b = _bookingSvc.GetBookingById(bid);
                if (b == null) continue;

                if (!b.CheckOutDate.HasValue || (b.CheckInDate.HasValue && b.CheckOutDate <= b.CheckInDate))
                    b.CheckOutDate = DateTime.Now;

                var roomNo = _roomSvc.GetRoomNumberById(b.RoomID);
                _roomNumberByRoomId[b.RoomID] = roomNo;
                _roomIdByBookingRoomId[b.BookingID] = b.RoomID;

                var pricing = _pricingRepo.GetPricingTypeById(b.PricingID);
                var unit = pricing != null ? pricing.Price : 0m;
                var ptype = pricing != null ? pricing.PricingType : "";
                decimal roomCharge = 0m;
                try { roomCharge = _bookingSvc.GetRoomCharge(b); } catch { }

                var svcs = _brsSvc.GetUsedServicesByBookingRoomId(b.BookingID);
                decimal svcTotal = svcs.Sum(x => x.Price * x.Quantity);

                _checkoutRows.Add(new CheckoutRow
                {
                    Pick = true,
                    BookingRoomID = b.BookingID,
                    RoomID = b.RoomID,
                    RoomNumber = roomNo,
                    CheckIn = b.CheckInDate,
                    CheckOut = b.CheckOutDate,
                    PricingType = ptype,
                    UnitPrice = unit,
                    RoomCharge = roomCharge,
                    ServiceTotal = svcTotal
                });

                foreach (var s in svcs)
                {
                    var row = _usedServicesTable.NewRow();
                    row["BookingRoomID"] = b.BookingID;
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

        private List<int> GetPickedBookingRoomIds()
        {
            return (_checkoutRows ?? new BindingList<CheckoutRow>())
                   .Where(x => x.Pick)
                   .Select(x => x.BookingRoomID)
                   .Distinct()
                   .ToList();
        }

        private void BtnIncrease_Click(object sender, EventArgs e)
        {
            var svc = dgvHotelServices.CurrentRow?.DataBoundItem as Service;
            if (svc == null)
            {
                MessageBox.Show("Chọn 1 dịch vụ trong menu bên phải.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int qty = (int)nbrIncrease.Value;
            if (qty <= 0) qty = 1;

            var bookingIds = GetPickedBookingRoomIds();
            if (bookingIds.Count == 0)
            {
                MessageBox.Show("Hãy tick phòng (cột 'Chọn') trước khi thêm.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Gọi BLL: atomic reserve + upsert lines
                var result = _brsSvc.AddServiceToRooms(bookingIds, svc.ServiceID, qty);
                int totalAdded = result.totalAdded;      // tổng qty đã trừ kho
                int remaining = result.remainingAfter;   // tồn còn lại sau khi thêm

                // Cập nhật bảng _usedServicesTable theo từng phòng (giữ code cũ)
                foreach (var bid in bookingIds)
                {
                    int roomId;
                    if (!_roomIdByBookingRoomId.TryGetValue(bid, out roomId)) continue;
                    string roomNo = _roomNumberByRoomId.ContainsKey(roomId) ? _roomNumberByRoomId[roomId] : "";

                    var existing = _usedServicesTable.AsEnumerable().FirstOrDefault(r =>
                        r.Field<int>("BookingRoomID") == bid && r.Field<int>("ServiceID") == svc.ServiceID);

                    if (existing != null)
                        existing["Quantity"] = existing.Field<int>("Quantity") + qty;
                    else
                    {
                        var row = _usedServicesTable.NewRow();
                        row["BookingRoomID"] = bid;
                        row["RoomID"] = roomId;
                        row["RoomNumber"] = roomNo;
                        row["ServiceID"] = svc.ServiceID;
                        row["ServiceName"] = svc.ServiceName;
                        row["UnitPrice"] = svc.Price;
                        row["Quantity"] = qty;
                        _usedServicesTable.Rows.Add(row);
                    }
                }

                // ✅ Sync cột “Tồn kho” trên menu dịch vụ (nếu entity Service có Quantity)
                var item = _servicesList != null
                    ? _servicesList.FirstOrDefault(x => x.ServiceID == svc.ServiceID)
                    : null;
                if (item != null)
                {
                    // Lấy remaining làm nguồn đúng nhất
                    item.Quantity = remaining;
                    dgvHotelServices.Refresh();
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

            int reduceBy = (int)nbrReduce.Value; if (reduceBy <= 0) reduceBy = 1;

            int bookingRoomId = Convert.ToInt32(drv.Row["BookingRoomID"]);
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

                    // ✅ Sync tồn kho menu: cộng lại số đã bớt
                    var item = _servicesList != null
                        ? _servicesList.FirstOrDefault(x => x.ServiceID == serviceId)
                        : null;
                    if (item != null)
                    {
                        item.Quantity = item.Quantity + actuallyRemoved;
                        dgvHotelServices.Refresh();
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

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            var picks = _checkoutRows.Where(x => x.Pick).ToList();
            if (picks.Count == 0)
            {
                MessageBox.Show("Chọn ít nhất 1 phòng để trả.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var ids = picks.Select(x => x.BookingRoomID).Distinct().ToList();

            using (var f = new frmCheckout(ids, _currentUserId))
            {
                f.StartPosition = FormStartPosition.CenterParent;
                var rs = f.ShowDialog(this);
                if (rs == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }
    }
}
