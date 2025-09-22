using HOTEL_MINI.BLL;
using HOTEL_MINI.Forms.Controls;
using HOTEL_MINI.Model.Entity;
using MiniHotel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HOTEL_MINI.Forms
{
    public partial class frmRoom : Form
    {
        private readonly frmApplication _form1;
        private readonly RoomService _roomService;
        private readonly RoomTypeService _roomTypeService;
        private RoomPricingService _roomPricingService;

        public frmRoom(frmApplication form1)
        {
            InitializeComponent();
            _form1 = form1;
            _roomService = new RoomService();
            _roomTypeService = new RoomTypeService();
            _roomPricingService = new RoomPricingService();
            LoadRoomStatus();
            LoadRoomType();
            LoadRoom("All", "", null);
            ClearPricingFields();// Không lọc theo room type khi khởi động
        }

        public void RefreshRoomList()
        {
            btnResetFilter.PerformClick();
        }
        //public List<RoomPricing> GetRoomPricingsByRoomType(int roomTypeId)
        //{
        //    return _roomPricingService.GetByRoomType(roomTypeId);
        //}
        public void LoadRoom(string status, string searchText, RoomTypes selectedRoomType)
        {
            flpAllRooms.Controls.Clear();

            var listRoom = _roomService.getAllRoom();

            // lọc theo status
            if (status != "All")
            {
                listRoom = listRoom.Where(r => r.RoomStatus == status).ToList();
            }

            // lọc theo searchText
            if (!string.IsNullOrEmpty(searchText))
            {
                listRoom = listRoom.Where(r =>
                    r.RoomNumber.ToLower().Contains(searchText.ToLower())
                ).ToList();
            }

            // lọc theo roomType
            if (selectedRoomType != null)
            {
                listRoom = listRoom.Where(r => r.RoomTypeID == selectedRoomType.RoomTypeID).ToList();
            }

            foreach (var room in listRoom)
            {
                var card = new RoomCard(_form1, room, this);
                flpAllRooms.Controls.Add(card);
            }
        }

        public void LoadRoomStatus()
        {
            var listStatus = _roomService.getAllRoomStatus();
            cbxRoomStatus.Items.Add("All");
            foreach (var status in listStatus)
            {
                cbxRoomStatus.Items.Add(status);
            }
            cbxRoomStatus.SelectedIndex = 0;
        }

        public void LoadRoomType()
        {
            var listRoomType = _roomTypeService.GetAllRoomTypes();
            foreach (var type in listRoomType)
            {
                cbxRoomType.Items.Add(type); // Chỉ thêm object RoomTypes
            }

            // Thêm một item rỗng hoặc không chọn gì mặc định
            if (cbxRoomType.Items.Count > 0)
            {
                cbxRoomType.SelectedIndex = -1; // Không chọn gì mặc định
            }
        }
        private void LoadRoomPricing(int roomTypeId)
        {
            var pricings = _roomPricingService.GetByRoomType(roomTypeId);

            // Xóa giá trị cũ
            ClearPricingFields();

            // Điền giá trị mới
            foreach (var pricing in pricings)
            {
                switch (pricing.PricingType)
                {
                    case "Hourly":
                        txtHourly.Text = $"{pricing.Price.ToString("N0")}đ/h";
                        break;
                    case "Nightly":
                        txtNightly.Text = $"{pricing.Price.ToString("N0")}đ/đêm";
                        break;
                    case "Daily":
                        txtDaily.Text = $"{pricing.Price.ToString("N0")}đ/ngày";
                        break;
                    case "Weekly":
                        txtWeekly.Text = $"{pricing.Price.ToString("N0")}đ/tuần";
                        break;
                }
            }
        }

        private void ClearPricingFields()
        {
            txtHourly.Text = "";
            txtNightly.Text = "";
            txtDaily.Text = "";
            txtWeekly.Text = "";
        }

        private void cbxRoomStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void txtSearchRoomNumber_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cbxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
            if (cbxRoomType.SelectedItem is RoomTypes selectedRoomType)
            {
                LoadRoomPricing(selectedRoomType.RoomTypeID);
            }
            else
            {
                ClearPricingFields(); // Xóa giá trị nếu không chọn gì
            }
        }

        private void ApplyFilters()
        {
            string selectedStatus = cbxRoomStatus.SelectedItem?.ToString() ?? "All";
            string searchText = txtSearchRoomNumber.Text.Trim();

            RoomTypes selectedRoomType = cbxRoomType.SelectedItem as RoomTypes;

            LoadRoom(selectedStatus, searchText, selectedRoomType);
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            cbxRoomStatus.SelectedIndex = 0;
            cbxRoomType.SelectedIndex = -1; // Bỏ chọn room type
            txtSearchRoomNumber.Clear();
            LoadRoom("All", "", null);
            ClearPricingFields();
        }
    }
}