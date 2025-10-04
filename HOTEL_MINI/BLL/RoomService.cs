using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using HOTEL_MINI.Model.Response;
using MiniHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HOTEL_MINI.BLL
{
    public class RoomService
    {
        private readonly RoomRepository roomRepository;

        public RoomService()
        {
            roomRepository = new RoomRepository();
        }

        public RoomStatistics GetRoomStatistics()
        {
            return roomRepository.GetRoomStatistics();
        }

        public List<Room> getAllRoom()
        {
            return roomRepository.getRoomList();
        }

        public List<string> getAllRoomStatus()
        {
            return roomRepository.getRoomStatus();
        }

        public bool UpdateRoomStatus(int roomID, string status)
        {
            return roomRepository.UpdateRoomStatus(roomID, status);
        }

        public List<string> getAllPricingType()
        {
            return roomRepository.getAllPricingType();
        }

        public RoomPricing getPricingID(string pricingType, int roomType)
        {
            return roomRepository.getPricingID(pricingType, roomType);
        }

        public string getPricingTypeByID(int pricingID)
        {
            return roomRepository.getPricingTypeByID(pricingID);
        }


        public bool AddRoom(Room room)
        {
            ValidateRoom(room, isUpdate: false);
            return roomRepository.AddRoom(room);
        }

        public bool UpdateRoom(Room room)
        {
            ValidateRoom(room, isUpdate: true);
            return roomRepository.UpdateRoom(room);
        }
        private static void NormalizeRoom(Room room)
        {
            room.RoomNumber = room.RoomNumber?.Trim();
            room.RoomStatus = room.RoomStatus?.Trim();
        }

        private void ValidateRoom(Room room, bool isUpdate)
        {
            if (room == null) throw new ArgumentNullException(nameof(room), "Đối tượng phòng không hợp lệ.");

            NormalizeRoom(room);

            if (isUpdate && room.RoomID <= 0)
                throw new ArgumentException("ID phòng không hợp lệ.");

            if (string.IsNullOrWhiteSpace(room.RoomNumber))
                throw new ArgumentException("Tên phòng không được để trống.");

            if (room.RoomTypeID <= 0)
                throw new ArgumentException("Loại phòng không hợp lệ.");

            if (string.IsNullOrWhiteSpace(room.RoomStatus))
                throw new ArgumentException("Trạng thái phòng không được để trống.");

            var validStatuses = roomRepository.getRoomStatus();
            if (validStatuses != null && validStatuses.Count > 0)
            {
                var ok = validStatuses.Any(s =>
                    string.Equals(s?.Trim(), room.RoomStatus, StringComparison.OrdinalIgnoreCase));
                if (!ok)
                    throw new ArgumentException("Trạng thái phòng không hợp lệ.");
            }
            var allRooms = roomRepository.getRoomList() ?? new List<Room>();
            var targetNumber = room.RoomNumber?.Trim();

            bool duplicated = allRooms.Any(r =>
                !string.IsNullOrWhiteSpace(r.RoomNumber) &&
                string.Equals(r.RoomNumber.Trim(), targetNumber, StringComparison.OrdinalIgnoreCase) &&
                (!isUpdate || r.RoomID != room.RoomID)
            );

            if (duplicated)
                throw new ArgumentException("Phòng đã tồn tại (trùng số phòng).");
        }

    }
}
