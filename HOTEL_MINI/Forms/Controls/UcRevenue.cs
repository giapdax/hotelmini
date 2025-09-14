using HOTEL_MINI.BLL;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HOTEL_MINI.Forms.Controls
{
    public partial class UcRevenue : UserControl
    {
        private readonly InvoiceService _invoiceService;

        public UcRevenue()
        {
            InitializeComponent();
            _invoiceService = new InvoiceService();
            InitializeComboBox();
            LoadChart();
            LoadSummaryData();
        }

        private void InitializeComboBox()
        {
            comboBoxTimeRange.Items.AddRange(new object[] {
                "6 Tháng gần nhất",
                "Theo tháng",
                "Theo tuần"
            });
            comboBoxTimeRange.SelectedIndex = 0;
            comboBoxTimeRange.SelectedIndexChanged += ComboBoxTimeRange_SelectedIndexChanged;
        }

        private void ComboBoxTimeRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
            LoadSummaryData();
        }

        private void LoadChart()
        {
            try
            {
                SetupChart();

                DataTable revenueData = null;
                string selectedRange = comboBoxTimeRange.SelectedItem?.ToString();

                switch (selectedRange)
                {
                    case "6 Tháng gần nhất":
                        revenueData = _invoiceService.GetRevenueLast6Months();
                        break;
                    case "Theo tháng":
                        revenueData = _invoiceService.GetRevenueByMonth(DateTime.Now.Year);
                        break;
                    case "Theo tuần":
                        revenueData = _invoiceService.GetRevenueByWeek();
                        break;
                    default:
                        revenueData = _invoiceService.GetRevenueLast6Months();
                        break;
                }

                if (revenueData != null && revenueData.Rows.Count > 0)
                {
                    DisplayRevenueData(revenueData, selectedRange);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu doanh thu để hiển thị.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu doanh thu: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSummaryData()
        {
            try
            {
                var summary = _invoiceService.GetRevenueSummary();

                if (summary != null)
                {
                    txtRoomCharge.Text = summary.RoomCharge.ToString("#,##0");
                    txtServiceCharge.Text = summary.ServiceCharge.ToString("#,##0");
                    txtDiscount.Text = summary.Discount.ToString("#,##0");
                    txtSurcharge.Text = summary.Surcharge.ToString("#,##0");
                    txtTotal.Text = summary.TotalAmount.ToString("#,##0");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu tổng quan: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupChart()
        {
            chartRevenue.Series.Clear();
            chartRevenue.Titles.Clear();
            chartRevenue.ChartAreas.Clear();
            chartRevenue.Legends.Clear();

            // Create and configure chart area
            ChartArea chartArea = new ChartArea("RevenueArea");
            chartArea.AxisX.Title = "Thời gian";
            chartArea.AxisY.Title = "Doanh thu (VND)";
            chartArea.AxisY.LabelStyle.Format = "#,##0";
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.AxisX.Interval = 1;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.BackColor = Color.White;
            chartRevenue.ChartAreas.Add(chartArea);

            // Create series
            Series series = new Series("Doanh thu");
            series.ChartType = SeriesChartType.Column;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "#,##0";
            series.Color = Color.SteelBlue;
            series.Font = new Font("Arial", 9, FontStyle.Bold);
            chartRevenue.Series.Add(series);

            // Configure chart appearance
            chartRevenue.BackColor = Color.White;
            chartRevenue.BorderlineColor = Color.LightGray;
            chartRevenue.BorderlineWidth = 1;
            chartRevenue.BorderlineDashStyle = ChartDashStyle.Solid;
        }

        private void DisplayRevenueData(DataTable revenueData, string timeRange)
        {
            chartRevenue.Series["Doanh thu"].Points.Clear();

            decimal totalRevenue = 0;
            decimal maxRevenue = 0;

            foreach (DataRow row in revenueData.Rows)
            {
                string periodName = row["DisplayPeriod"].ToString();
                decimal revenue = Convert.ToDecimal(row["TotalRevenue"]);

                DataPoint point = new DataPoint();
                point.SetValueXY(periodName, revenue);
                point.LabelFormat = "#,##0";
                point.Font = new Font("Arial", 8);
                point.Color = GetColorForRevenue(revenue);
                point.ToolTip = $"{periodName}: {revenue.ToString("#,##0")} VND";

                chartRevenue.Series["Doanh thu"].Points.Add(point);

                totalRevenue += revenue;
                if (revenue > maxRevenue) maxRevenue = revenue;
            }

            // Set chart title based on time range
            string titleText = GetChartTitle(timeRange);
            Title title = new Title(titleText);
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.ForeColor = Color.SteelBlue;
            chartRevenue.Titles.Add(title);

            // Set Y-axis interval
            if (maxRevenue > 0)
            {
                decimal interval = Math.Ceiling(maxRevenue / 5 / 1000000) * 1000000;
                chartRevenue.ChartAreas["RevenueArea"].AxisY.Interval = (double)interval;
            }

            // Add average line if there's data
            if (revenueData.Rows.Count > 0 && maxRevenue > 0)
            {
                decimal averageRevenue = totalRevenue / revenueData.Rows.Count;

                StripLine averageLine = new StripLine();
                averageLine.Interval = 0;
                averageLine.IntervalOffset = (double)averageRevenue;
                averageLine.StripWidth = 0;
                averageLine.BorderColor = Color.Red;
                averageLine.BorderWidth = 2;
                averageLine.BorderDashStyle = ChartDashStyle.Dash;
                averageLine.Text = $"Trung bình: {averageRevenue.ToString("#,##0")}";
                averageLine.TextLineAlignment = StringAlignment.Far;

                chartRevenue.ChartAreas["RevenueArea"].AxisY.StripLines.Add(averageLine);
            }
        }

        private string GetChartTitle(string timeRange)
        {
            switch (timeRange)
            {
                case "6 Tháng gần nhất":
                    return "DOANH THU 6 THÁNG GẦN NHẤT";
                case "Theo tháng":
                    return $"DOANH THU THEO THÁNG - NĂM {DateTime.Now.Year}";
                case "Theo tuần":
                    return $"DOANH THU THEO TUẦN - NĂM {DateTime.Now.Year}";
                default:
                    return "DOANH THU";
            }
        }

        private Color GetColorForRevenue(decimal revenue)
        {
            if (revenue == 0) return Color.LightGray;
            if (revenue < 10000000) return Color.SteelBlue;
            if (revenue < 20000000) return Color.DodgerBlue;
            if (revenue < 30000000) return Color.RoyalBlue;
            return Color.MediumBlue;
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadChart();
            LoadSummaryData();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|BMP Image|*.bmp";
                saveDialog.Title = "Lưu biểu đồ";
                saveDialog.FileName = $"DoanhThu_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ChartImageFormat format;

                    // Sử dụng switch statement thay vì switch expression
                    switch (saveDialog.FilterIndex)
                    {
                        case 2:
                            format = ChartImageFormat.Jpeg;
                            break;
                        case 3:
                            format = ChartImageFormat.Bmp;
                            break;
                        default:
                            format = ChartImageFormat.Png;
                            break;
                    }

                    chartRevenue.SaveImage(saveDialog.FileName, format);
                    MessageBox.Show("Đã lưu biểu đồ thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất biểu đồ: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}