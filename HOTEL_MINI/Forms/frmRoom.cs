using HOTEL_MINI.BLL;
using HOTEL_MINI.Forms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public frmRoom(frmApplication form1)
        {
            InitializeComponent();
            _form1 = form1;
            _roomService = new RoomService();
            LoadRoom("All", "");
            LoadRoomByStatus();
            //pnlRight.Dock = DockStyle.Right;   // Gắn vào bên phải
            //pnlRight.Width = 200;              // Chiều rộng cố định

            //// Panel còn lại
            //pnlMain.Dock = DockStyle.Fill;
        }
        public void LoadRoom(string status, string searchText)
        {
            flpAllRooms.Controls.Clear();

            var listRoom = _roomService.getAllRoom();

            // lọc theo status
            if (status != "All")
            {
                listRoom = listRoom.Where(r => r.RoomStatus == status).ToList();
            }

            // lọc theo searchText (áp dụng cho RoomNumber và RoomName nếu có)
            if (!string.IsNullOrEmpty(searchText))
            {
                listRoom = listRoom.Where(r =>
                    r.RoomNumber.ToLower().Contains(searchText.ToLower()) // search theo tên phòng
                ).ToList();
            }

            foreach (var room in listRoom)
            {
                var card = new RoomCard(room);
                flpAllRooms.Controls.Add(card);
            }
            
            
        }


        public void LoadRoomByStatus()
        {            
            var listStatus = _roomService.getAllRoomStatus();
            cbxRoomStatus.Items.Add("All");
            foreach (var status in listStatus)
            {
                
                cbxRoomStatus.Items.Add(status);
            }
            
            cbxRoomStatus.SelectedIndex = 0;
        }

        private void cbxRoomStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatus = cbxRoomStatus.SelectedItem?.ToString() ?? "All";
            string searchText = txtSearchRoomNumber.Text.Trim();
            LoadRoom(selectedStatus, searchText);
        }


        private void txtSearchRoomNumber_TextChanged(object sender, EventArgs e)
        {
            string selectedStatus = cbxRoomStatus.SelectedItem?.ToString() ?? "All";
            string searchText = txtSearchRoomNumber.Text.Trim();
            LoadRoom(selectedStatus, searchText);
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            cbxRoomStatus.SelectedIndex = 0; // reset về "All"
            txtSearchRoomNumber.Clear();
            LoadRoom("All", ""); // load full list
        }

    }
}
