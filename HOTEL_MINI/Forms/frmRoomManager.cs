using System;
using System.Windows.Forms;
using HOTEL_MINI.BLL;

namespace HOTEL_MINI.Forms
{
    public partial class frmRoomManager : Form
    {
        private readonly RoomService _roomService = new RoomService();
        private readonly RoomTypeService _roomTypeSvc = new RoomTypeService();
        private readonly RoomPricingService _pricingSvc = new RoomPricingService();

        private UcRoom _ucRoom;
        private UcRoomType_Pricing _ucRoomTypePricing;

        public frmRoomManager()
        {
            InitializeComponent();

            _ucRoom = new UcRoom(_roomService, _roomTypeSvc, _pricingSvc) { Dock = DockStyle.Fill };
            tabRooms.Controls.Add(_ucRoom);

            _ucRoomTypePricing = new UcRoomType_Pricing(_roomService, _roomTypeSvc, _pricingSvc) { Dock = DockStyle.Fill };
            _ucRoomTypePricing.RoomTypeChanged += id => _ucRoom?.SelectRoomType(id);
            tabRoomTypePricing.Controls.Add(_ucRoomTypePricing);
        }
    }
}
