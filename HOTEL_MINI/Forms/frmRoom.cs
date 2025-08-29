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
        private readonly Form1 _form1;
        private readonly RoomService _roomService;
        public frmRoom(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
            _roomService = new RoomService();
            LoadRoom("All");
            LoadRoomByStatus();
            //pnlRight.Dock = DockStyle.Right;   // Gắn vào bên phải
            //pnlRight.Width = 200;              // Chiều rộng cố định

            //// Panel còn lại
            //pnlMain.Dock = DockStyle.Fill;
        }
        public void LoadRoom(string status)
        {
            flpAllRooms.Controls.Clear();

            var listRoom = _roomService.getAllRoom();

            // Nếu chọn khác "All" thì lọc theo status
            if (status != "All")
            {
                listRoom = listRoom.Where(r => r.RoomStatus == status).ToList();
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
            string selectedStatus = cbxRoomStatus.SelectedItem.ToString();
            LoadRoom(selectedStatus);
        }
    }
}
