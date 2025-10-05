// HOTEL_MINI.Forms.Dialogs/dlgRoomTime.cs
using HOTEL_MINI.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HOTEL_MINI.Forms.Dialogs
{
    public partial class dlgRoomTime : Form
    {
        private readonly int _roomTypeId;
        private readonly Dictionary<string, decimal> _rates =
            new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

        public DateTime CheckIn { get; private set; }
        public DateTime CheckOut { get; private set; }
        public string PricingType { get; private set; }
        public decimal? UnitPrice { get; private set; }
        public decimal CalculatedCost { get; private set; }
        public bool IsReceiveNow { get; private set; } = true; // mặc định Nhận ngay


        public dlgRoomTime(int roomTypeId, DateTime baseIn, DateTime baseOut, string title = null)
        {
            InitializeComponent();

            _roomTypeId = roomTypeId;

            // format time
            dtpIn.Format = DateTimePickerFormat.Custom;
            dtpIn.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpOut.Format = DateTimePickerFormat.Custom;
            dtpOut.CustomFormat = "dd/MM/yyyy HH:mm";

            dtpIn.Value = baseIn;
            dtpOut.Value = baseOut;

            if (!string.IsNullOrWhiteSpace(title))
                this.Text = title;

            LoadActiveRates();

            // nguồn dữ liệu combobox
            cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboType.DataSource = _rates.Keys.ToList();
            if (cboType.Items.Count > 0) cboType.SelectedIndex = 0;

            // events
            cboType.SelectedIndexChanged += (s, e) => ApplyTypePresetAndRecalc();
            dtpIn.ValueChanged += (s, e) => Recalc();
            dtpOut.ValueChanged += (s, e) => Recalc();

            btnOK.Click += BtnOK_Click;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            ApplyTypePresetAndRecalc();
        }

        private void LoadActiveRates()
        {
            var repo = new RoomPricingRepository();
            var list = repo.GetByRoomType(_roomTypeId).Where(p => p.IsActive).ToList();

            foreach (var p in list)
            {
                if (p.PricingType == null) continue;
                var key = p.PricingType.Trim();
                if (!_rates.ContainsKey(key))
                    _rates[key] = p.Price;
            }
        }

        //private void ApplyTypePresetAndRecalc()
        //{
        //    var type = cboType.SelectedItem != null ? cboType.SelectedItem.ToString() : null;

        //    if (string.Equals(type, "Nightly", StringComparison.OrdinalIgnoreCase))
        //    {
        //        // Preset 1 đêm: 21:00 -> 07:00 hôm sau
        //        var dIn = dtpIn.Value.Date;
        //        var inTime = new DateTime(dIn.Year, dIn.Month, dIn.Day, 21, 0, 0);
        //        var outTime = inTime.AddDays(1).Date.AddHours(7);

        //        dtpIn.Value = inTime;
        //        dtpOut.Value = outTime;

        //        dtpIn.ShowUpDown = true;
        //        dtpOut.ShowUpDown = true;
        //    }
        //    else
        //    {
        //        dtpIn.ShowUpDown = false;
        //        dtpOut.ShowUpDown = false;
        //    }

        //    Recalc();
        //}
        private void ApplyTypePresetAndRecalc()
        {
            dtpIn.ValueChanged -= DtpIn_Weekly_ValueChanged;
            var type = cboType.SelectedItem != null ? cboType.SelectedItem.ToString() : null;

            dtpIn.ShowUpDown = false;
            dtpOut.ShowUpDown = false;
            dtpIn.Enabled = true;
            dtpOut.Enabled = true;

            if (string.Equals(type, "Daily", StringComparison.OrdinalIgnoreCase))
            {
                // Cố định 14:00 -> 12:00 hôm sau
                var date = DateTime.Now.Date;
                var inTime = new DateTime(date.Year, date.Month, date.Day, 14, 0, 0);
                var outTime = inTime.AddDays(1).Date.AddHours(12);

                dtpIn.Value = inTime;
                dtpOut.Value = outTime;

                // Chỉ cho chỉnh ngày, không cho chỉnh giờ
                dtpIn.ShowUpDown = false;
                dtpOut.ShowUpDown = false;
                dtpIn.CustomFormat = "dd/MM/yyyy";
                dtpOut.CustomFormat = "dd/MM/yyyy";
            }
            else if (string.Equals(type, "Nightly", StringComparison.OrdinalIgnoreCase))
            {
                // Cố định 21:00 -> 07:00 hôm sau
                var dIn = DateTime.Now.Date;
                var inTime = new DateTime(dIn.Year, dIn.Month, dIn.Day, 21, 0, 0);
                var outTime = inTime.AddDays(1).Date.AddHours(7);

                dtpIn.Value = inTime;
                dtpOut.Value = outTime;

                // ❌ Không cho chỉnh giờ phút, chỉ hiển thị ngày
                dtpIn.ShowUpDown = false;
                dtpOut.ShowUpDown = false;
                dtpIn.CustomFormat = "dd/MM/yyyy";
                dtpOut.CustomFormat = "dd/MM/yyyy";

                // Có thể cho chỉnh ngày nếu muốn đặt cho hôm khác
                dtpIn.Enabled = true;
                dtpOut.Enabled = true;
            }

            else if (string.Equals(type, "Weekly", StringComparison.OrdinalIgnoreCase))
            {
                // Check-in chỉnh được ngày + giờ
                dtpIn.Enabled = true;
                dtpIn.ShowUpDown = false;
                dtpIn.CustomFormat = "dd/MM/yyyy HH:mm";

                // Check-out luôn = Check-in + 7 ngày, không chỉnh được
                dtpOut.Enabled = false;
                dtpOut.ShowUpDown = false;
                dtpOut.CustomFormat = "dd/MM/yyyy HH:mm";

                var ci = DateTime.Now;
                dtpIn.Value = ci;
                dtpOut.Value = ci.AddDays(7);

                // khi người dùng đổi check-in => cập nhật check-out
                dtpIn.ValueChanged -= DtpIn_Weekly_ValueChanged;
                dtpIn.ValueChanged += DtpIn_Weekly_ValueChanged;
            }


            else if (string.Equals(type, "Hourly", StringComparison.OrdinalIgnoreCase))
            {
                var now = DateTime.Now;
                dtpIn.Value = now;
                dtpOut.Value = now.AddHours(1);

                // Cho chỉnh cả ngày + giờ
                dtpIn.ShowUpDown = false;    // show lịch chọn ngày
                dtpOut.ShowUpDown = false;

                dtpIn.CustomFormat = "dd/MM/yyyy HH:mm";
                dtpOut.CustomFormat = "dd/MM/yyyy HH:mm";

                dtpIn.Enabled = true;
                dtpOut.Enabled = true;
            }


            Recalc();
        }
        private void DtpIn_Weekly_ValueChanged(object sender, EventArgs e)
        {
            // cập nhật checkout = checkin + 7 ngày
            dtpOut.Value = dtpIn.Value.AddDays(7);
        }


        

        private void DtpDaily_ValueChanged(object sender, EventArgs e)
        {
            var picker = (DateTimePicker)sender;
            if (string.Equals(cboType.SelectedItem?.ToString(), "Daily", StringComparison.OrdinalIgnoreCase))
            {
                if (picker == dtpIn)
                {
                    // Giữ nguyên ngày, reset giờ về 14:00
                    var d = dtpIn.Value.Date;
                    if (dtpIn.Value.Hour != 14 || dtpIn.Value.Minute != 0)
                        dtpIn.Value = new DateTime(d.Year, d.Month, d.Day, 14, 0, 0);

                    // Cập nhật checkout = hôm sau 12:00
                    dtpOut.Value = dtpIn.Value.Date.AddDays(1).AddHours(12);
                }
                else if (picker == dtpOut)
                {
                    // Giữ nguyên ngày, reset giờ về 12:00
                    var d = dtpOut.Value.Date;
                    if (dtpOut.Value.Hour != 12 || dtpOut.Value.Minute != 0)
                        dtpOut.Value = new DateTime(d.Year, d.Month, d.Day, 12, 0, 0);
                }
            }
        }



        private static int CeilToInt(double v)
        {
            var c = Math.Ceiling(v);
            if (c > int.MaxValue) return int.MaxValue;
            return (int)c;
        }

        private void Recalc()
        {
            PricingType = cboType.SelectedItem != null ? cboType.SelectedItem.ToString() : null;

            var ci = dtpIn.Value;
            var co = dtpOut.Value;

            if (co < ci)
            {
                lblUnitPrice.Text = "Đơn giá: -";
                lblCost.Text = "Tạm tính: 0";
                CalculatedCost = 0m;
                return;
            }

            decimal unit = 0m;
            if (PricingType != null && _rates.ContainsKey(PricingType))
                unit = _rates[PricingType];

            UnitPrice = unit;
            lblUnitPrice.Text = $"Đơn giá ({PricingType ?? "-"}) : {unit:N0}";

            decimal cost = 0m;

            if (string.Equals(PricingType, "Hourly", StringComparison.OrdinalIgnoreCase))
            {
                var hours = CeilToInt((co - ci).TotalHours);
                cost = unit * hours;
            }
            else if (string.Equals(PricingType, "Daily", StringComparison.OrdinalIgnoreCase))
            {
                var days = CeilToInt((co - ci).TotalDays);
                if (days < 1) days = 1;
                cost = unit * days;
            }
            else if (string.Equals(PricingType, "Weekly", StringComparison.OrdinalIgnoreCase))
            {
                var weeks = CeilToInt((co - ci).TotalDays / 7.0);
                if (weeks < 1) weeks = 1;
                cost = unit * weeks;
            }
            else if (string.Equals(PricingType, "Nightly", StringComparison.OrdinalIgnoreCase))
            {
                // đúng 1 đêm
                cost = unit;
            }

            CalculatedCost = cost;
            lblCost.Text = $"Tạm tính: {cost:N0}";
        }

        //private void BtnOK_Click(object sender, EventArgs e)
        //{
        //    var ci = dtpIn.Value;
        //    var co = dtpOut.Value;

        //    if (co <= ci)
        //    {
        //        MessageBox.Show("Thời gian trả phòng phải lớn hơn nhận phòng.", "Lỗi",
        //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    if (string.Equals(PricingType, "Nightly", StringComparison.OrdinalIgnoreCase))
        //    {
        //        var okIn = ci.Hour == 21 && ci.Minute == 0;
        //        var okOut = co.Hour == 7 && co.Minute == 0 && co.Date == ci.Date.AddDays(1);
        //        if (!okIn || !okOut)
        //        {
        //            MessageBox.Show("Thuê theo ĐÊM chỉ cho phép 21:00 → 07:00 (1 đêm).", "Lỗi",
        //                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            return;
        //        }
        //    }

        //    CheckIn = ci;
        //    CheckOut = co;
        //    IsReceiveNow = rdoReceiveNow.Checked;
        //    this.DialogResult = DialogResult.OK;
        //}
        private void BtnOK_Click(object sender, EventArgs e)
        {
            var ci = dtpIn.Value;
            var co = dtpOut.Value;

            // ❌ Không cho nhận phòng ở quá khứ
            //if (ci < DateTime.Now.AddMinutes(-1))
            //{
            //    MessageBox.Show("Không thể chọn thời gian nhận phòng trong quá khứ.", "Lỗi",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            // ❌ Check-out phải sau check-in
            if (co <= ci)
            {
                MessageBox.Show("Thời gian trả phòng phải lớn hơn thời gian nhận phòng.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.Equals(PricingType, "Nightly", StringComparison.OrdinalIgnoreCase))
            {
                // Chỉ cho phép đúng 1 đêm: 21:00 -> 07:00 hôm sau
                var okIn = ci.Hour == 21 && ci.Minute == 0;
                var okOut = co.Hour == 7 && co.Minute == 0 && co.Date == ci.Date.AddDays(1);
                if (!okIn || !okOut)
                {
                    MessageBox.Show("Thuê theo ĐÊM chỉ cho phép 21:00 → 07:00 (1 đêm).", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (string.Equals(PricingType, "Weekly", StringComparison.OrdinalIgnoreCase))
            {
                // Phải đúng 7 ngày
                if ((co - ci).TotalDays < 7 - 0.01 || (co - ci).TotalDays > 7.01)
                {
                    MessageBox.Show("Thuê theo TUẦN phải đúng 7 ngày.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CheckIn = ci;
            CheckOut = co;
            IsReceiveNow = rdoReceiveNow.Checked;
            this.DialogResult = DialogResult.OK;
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOK_Click_1(object sender, EventArgs e)
        {

        }
    }
}
