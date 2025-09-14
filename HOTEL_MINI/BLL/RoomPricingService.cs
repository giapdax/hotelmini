// File: HOTEL_MINI/BLL/RoomPricingService.cs
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;

namespace HOTEL_MINI.BLL
{
    public class RoomPricingService
    {
        private readonly RoomPricingRepository _repo = new RoomPricingRepository();

        public List<RoomPricing> GetAll() => _repo.GetAll();
        public RoomPricing GetById(int id) => _repo.GetPricingTypeById(id);
        public List<string> GetPricingTypes() => _repo.GetPricingTypes();
        public RoomPricing GetByRoomTypeAndType(int roomTypeId, string pricingType) => _repo.GetByRoomTypeAndType(roomTypeId, pricingType);

        public bool Add(RoomPricing p) { Validate(p, requireId: false); return _repo.Add(p); }
        public bool Update(RoomPricing p) { Validate(p, requireId: true); return _repo.Update(p); }
        public bool Delete(int id) => _repo.Delete(id);

        private static void Validate(RoomPricing p, bool requireId)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            if (requireId && p.PricingID <= 0) throw new ArgumentException("PricingID không hợp lệ.");
            if (p.RoomTypeID <= 0) throw new ArgumentException("Chưa chọn loại phòng.");
            if (string.IsNullOrWhiteSpace(p.PricingType)) throw new ArgumentException("Chưa chọn loại giá.");
            if (p.Price < 0) throw new ArgumentException("Giá không thể âm.");
        }
    }
}
