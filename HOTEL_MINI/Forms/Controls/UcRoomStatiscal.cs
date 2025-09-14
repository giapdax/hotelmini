using HOTEL_MINI.BLL;
using HOTEL_MINI.Model.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HOTEL_MINI.Forms.Controls
{
    public partial class UcRoomStatiscal : UserControl
    {
        private readonly RoomService _roomService;

        public UcRoomStatiscal()
        {
            InitializeComponent();
            _roomService = new RoomService();
            LoadRoomStatistics();
        }

        private void LoadRoomStatistics()
        {
            try
            {
                var stats = _roomService.GetRoomStatistics();

                if (stats != null)
                {
                    txtNumberOfTotalRoom.Text = $"{stats.TotalRooms} phòng";
                    txtNumberOfAvailableRoom.Text = $"{stats.AvailableRooms} phòng ({(stats.TotalRooms > 0 ? (stats.AvailableRooms * 100 / stats.TotalRooms) : 0)}%)";
                    txtNumberOfBookedRoom.Text = $"{stats.BookedRooms} phòng ({(stats.TotalRooms > 0 ? (stats.BookedRooms * 100 / stats.TotalRooms) : 0)}%)";
                    txtNumberOfOccupiedRoom.Text = $"{stats.OccupiedRooms} phòng ({(stats.TotalRooms > 0 ? (stats.OccupiedRooms * 100 / stats.TotalRooms) : 0)}%)";
                    txtNumberOfMaintenanceRoom.Text = $"{stats.MaintenanceRooms} phòng ({(stats.TotalRooms > 0 ? (stats.MaintenanceRooms * 100 / stats.TotalRooms) : 0)}%)";

                    FormatTextBoxColors(stats);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê phòng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatTextBoxColors(RoomStatistics stats)
        {
            // Màu xanh cho phòng trống
            txtNumberOfAvailableRoom.BackColor = Color.LightGreen;
            txtNumberOfAvailableRoom.ForeColor = Color.DarkGreen;

            // Màu vàng cho phòng đã book
            txtNumberOfBookedRoom.BackColor = Color.LightYellow;
            txtNumberOfBookedRoom.ForeColor = Color.Orange;

            // Màu đỏ cho phòng đang có người ở
            txtNumberOfOccupiedRoom.BackColor = Color.LightCoral;
            txtNumberOfOccupiedRoom.ForeColor = Color.DarkRed;

            // Màu xám cho phòng bảo trì
            txtNumberOfMaintenanceRoom.BackColor = Color.LightGray;
            txtNumberOfMaintenanceRoom.ForeColor = Color.DarkSlateGray;

            // Màu mặc định cho tổng số phòng
            txtNumberOfTotalRoom.BackColor = Color.LightBlue;
            txtNumberOfTotalRoom.ForeColor = Color.DarkBlue;

            // Làm cho các textbox readonly và căn giữa
            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox textBox)
                {
                    textBox.ReadOnly = true;
                    textBox.TextAlign = HorizontalAlignment.Center;
                    textBox.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRoomStatistics();
        }
    }
}
