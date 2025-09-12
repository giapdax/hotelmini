// File: RoomTypeService.cs
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using MiniHotel.Models;
using System;
using System.Collections.Generic;

namespace HOTEL_MINI.BLL
{
    public class RoomTypeService
    {
        private readonly RoomTypeRepository _roomTypeRepository;

        public RoomTypeService()
        {
            _roomTypeRepository = new RoomTypeRepository();
        }

        public List<RoomTypes> GetAllRoomTypes()
            => _roomTypeRepository.GetAllRoomTypes();

        public RoomTypes GetById(int roomTypeId)
            => _roomTypeRepository.GetById(roomTypeId);

        public bool AddRoomType(RoomTypes roomType)
        {
            Validate(roomType, requireId: false);
            return _roomTypeRepository.AddRoomType(roomType);
        }

        public bool UpdateRoomType(RoomTypes roomType)
        {
            Validate(roomType, requireId: true);
            return _roomTypeRepository.UpdateRoomType(roomType);
        }

        public bool DeleteRoomType(int roomTypeId)
            => _roomTypeRepository.DeleteRoomType(roomTypeId);

        // ===== Helpers =====
        private static void Validate(RoomTypes rt, bool requireId)
        {
            if (rt == null) throw new ArgumentNullException(nameof(rt));
            if (requireId && rt.RoomTypesID <= 0)
                throw new ArgumentException("ID loại phòng không hợp lệ.");
            if (string.IsNullOrWhiteSpace(rt.TypeName))
                throw new ArgumentException("Tên loại phòng không được để trống.");
            if (string.IsNullOrWhiteSpace(rt.Description))
                throw new ArgumentException("Mô tả không được để trống.");
        }
    }
}
