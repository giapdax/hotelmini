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

        private void ApplyTypePresetAndRecalc()
        {
            var type = cboType.SelectedItem != null ? cboType.SelectedItem.ToString() : null;

            if (string.Equals(type, "Nightly", StringComparison.OrdinalIgnoreCase))
            {
                // Preset 1 đêm: 21:00 -> 07:00 hôm sau
                var dIn = dtpIn.Value.Date;
                var inTime = new DateTime(dIn.Year, dIn.Month, dIn.Day, 21, 0, 0);
                var outTime = inTime.AddDays(1).Date.AddHours(7);

                dtpIn.Value = inTime;
                dtpOut.Value = outTime;

                dtpIn.ShowUpDown = true;
                dtpOut.ShowUpDown = true;
            }
            else
            {
                dtpIn.ShowUpDown = false;
                dtpOut.ShowUpDown = false;
            }

            Recalc();
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

        private void BtnOK_Click(object sender, EventArgs e)
        {
            var ci = dtpIn.Value;
            var co = dtpOut.Value;

            if (co <= ci)
            {
                MessageBox.Show("Thời gian trả phòng phải lớn hơn nhận phòng.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.Equals(PricingType, "Nightly", StringComparison.OrdinalIgnoreCase))
            {
                var okIn = ci.Hour == 21 && ci.Minute == 0;
                var okOut = co.Hour == 7 && co.Minute == 0 && co.Date == ci.Date.AddDays(1);
                if (!okIn || !okOut)
                {
                    MessageBox.Show("Thuê theo ĐÊM chỉ cho phép 21:00 → 07:00 (1 đêm).", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            CheckIn = ci;
            CheckOut = co;
            this.DialogResult = DialogResult.OK;
        }
    }
}
