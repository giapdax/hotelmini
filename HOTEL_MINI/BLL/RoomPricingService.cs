using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;

namespace HOTEL_MINI.BLL
{
    public class RoomPricingService
    {
        private readonly RoomPricingRepository _repo = new RoomPricingRepository();

        public List<string> GetPricingTypes() => _repo.GetPricingTypes();
        public RoomPricing GetPricingTypeById(int pricingId) => _repo.GetPricingTypeById(pricingId);
        public RoomPricing GetByRoomTypeAndType(int roomTypeId, string pricingType) => _repo.GetByRoomTypeAndType(roomTypeId, pricingType);
        public List<RoomPricing> GetByRoomType(int roomTypeId) => _repo.GetByRoomType(roomTypeId);

        public bool Add(RoomPricing p) { Validate(p, requireId: false); return _repo.Add(p); }
        public bool Update(RoomPricing p) { Validate(p, requireId: true); return _repo.Update(p); }
        public RoomPricing GetById(int pricingId)
        {
            return _repo.GetPricingTypeById(pricingId);
        }
        private void Validate(RoomPricing p, bool requireId)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            if (requireId && p.PricingID <= 0) throw new ArgumentException("PricingID không hợp lệ.");
            if (p.RoomTypeID <= 0) throw new ArgumentException("Chưa chọn loại phòng.");
            if (string.IsNullOrWhiteSpace(p.PricingType)) throw new ArgumentException("Chưa chọn loại giá.");
            if (p.Price < 0) throw new ArgumentException("Giá không thể âm.");

            var existed = _repo.GetByRoomTypeAndType(p.RoomTypeID, p.PricingType);
            if (existed != null && (!requireId || existed.PricingID != p.PricingID))
                throw new ArgumentException("Tổ hợp Loại phòng và Loại giá đã tồn tại.");
        }
    }
}
