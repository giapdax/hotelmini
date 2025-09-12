using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using MiniHotel.Models;
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
        public List<RoomTypes> GetRoomTypes() 
        { 
            return roomRepository.getRoomTypeList();
        }
        public RoomPricing getPricingID(string pricingType, int roomType)
        {
            return roomRepository.getPricingID(pricingType, roomType);
        }

        public bool UpdateRoomStatus(int roomID, string v)
        {
            return roomRepository.UpdateRoomStatus(roomID, v);
        }
        public string getPricingTypeByID(int pricingID)
        {
            return roomRepository.getPricingTypeByID(pricingID);
        }
        public bool AddRoom(Room room)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));
            if (string.IsNullOrWhiteSpace(room.RoomNumber))
                throw new ArgumentException("Tên phòng không được để trống.");
            if (room.RoomTypeID <= 0)
                throw new ArgumentException("Loại phòng không hợp lệ.");
            if (string.IsNullOrWhiteSpace(room.RoomStatus))
                throw new ArgumentException("Trạng thái phòng không được để trống.");
            return roomRepository.AddRoom(room);
        }
        public bool  UpdateRoom(Room room)
        {
            if (room == null) throw new ArgumentNullException(nameof(room));
            if (room.RoomID <= 0)
                throw new ArgumentException("ID phòng không hợp lệ.");
            if (string.IsNullOrWhiteSpace(room.RoomNumber))
                throw new ArgumentException("Tên phòng không được để trống.");
            if (room.RoomTypeID <= 0)
                throw new ArgumentException("Loại phòng không hợp lệ.");
            if (string.IsNullOrWhiteSpace(room.RoomStatus))
                throw new ArgumentException("Trạng thái phòng không được để trống.");
            return roomRepository.UpdateRoom(room);
        }
    }
}
