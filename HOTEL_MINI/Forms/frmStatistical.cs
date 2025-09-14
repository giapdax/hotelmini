using HOTEL_MINI.BLL;
using HOTEL_MINI.Forms;
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

namespace HOTEL_MINI.Forms
{
    public partial class frmStatistical : Form
    {
        private readonly UcRevenue _ucRevenue;
        private readonly UcRoomStatiscal _ucRoomStatistical;
        public frmStatistical()
        {
            InitializeComponent();
            _ucRevenue = new UcRevenue() { Dock = DockStyle.Fill };
            tabRevenue.Controls.Add(_ucRevenue);

            _ucRoomStatistical = new UcRoomStatiscal() { Dock = DockStyle.Fill };
            tabRoom.Controls.Add(_ucRoomStatistical);

        }
    }
}
//private readonly RoomService _roomService = new RoomService();
//private readonly RoomTypeService _roomTypeSvc = new RoomTypeService();
//private readonly RoomPricingService _pricingSvc = new RoomPricingService();

//private UcRoom _ucRoom;
//private UcRoomType_Pricing _ucRoomTypePricing;

//public frmRoomManager()
//{
//    InitializeComponent();

//    _ucRoom = new UcRoom(_roomService, _roomTypeSvc, _pricingSvc) { Dock = DockStyle.Fill };
//    tabRooms.Controls.Add(_ucRoom);

//    _ucRoomTypePricing = new UcRoomType_Pricing(_roomService, _roomTypeSvc, _pricingSvc) { Dock = DockStyle.Fill };
//    _ucRoomTypePricing.RoomTypeChanged += id => _ucRoom?.SelectRoomType(id);
//    tabRoomTypePricing.Controls.Add(_ucRoomTypePricing);
//}