using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.BLL
{
    public class RoomService
    {
        private readonly RoomRepository roomRepository;
        public RoomService()
        {
            roomRepository = new RoomRepository();
        }
        public List<Room> getAllRoom()
        {
            return roomRepository.getRoomList();
        }
        public List<string> getAllRoomStatus()
        {
            return roomRepository.getRoomStatus();
        }
        public bool updateRoomStatus(int roomID, string status)
        {
            return roomRepository.UpdateRoomStatus(roomID, status);
        }
        public List<string> getAllPricingType()
        {
            return roomRepository.getAllPricingType();
        }
    }
}
