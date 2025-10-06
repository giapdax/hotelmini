using HOTEL_MINI.Common;
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;

namespace HOTEL_MINI.BLL
{
    public class ServicesService
    {
        private readonly ServicesRepository _repo = new ServicesRepository();

        public System.Collections.Generic.List<Service> GetAllServices() => _repo.GetAllServices();

        // Thêm dịch vụ MỚI
        public bool AddService(Service s)
        {
            if (s == null) throw new ArgumentException("Dữ liệu dịch vụ không hợp lệ.");
            if (string.IsNullOrWhiteSpace(s.ServiceName)) throw new ArgumentException("Tên dịch vụ không được trống.");
            if (s.Price < 0) throw new ArgumentException("Giá không hợp lệ.");
            if (s.Quantity < 0) s.Quantity = 0;

            if (_repo.ExistsByName(s.ServiceName, null))
                throw new ArgumentException("Tên dịch vụ đã tồn tại.");

            return _repo.AddService(s);
        }

        // Cập nhật thông tin dịch vụ (không phải nhập kho)
        public bool UpdateService(Service s)
        {
            if (s == null || s.ServiceID <= 0) throw new ArgumentException("Dữ liệu dịch vụ không hợp lệ.");
            if (string.IsNullOrWhiteSpace(s.ServiceName)) throw new ArgumentException("Tên dịch vụ không được trống.");
            if (s.Price < 0) throw new ArgumentException("Giá không hợp lệ.");
            if (s.Quantity < 0) s.Quantity = 0;

            if (_repo.ExistsByName(s.ServiceName, s.ServiceID))
                throw new ArgumentException("Tên dịch vụ đã tồn tại.");

            return _repo.UpdateService(s);
        }

        // Đặt lại số lượng tồn tuyệt đối (ít dùng cho nhập kho)
        public bool UpdateServiceQuantity(int serviceId, int newQty)
        {
            if (serviceId <= 0) throw new ArgumentException("ID không hợp lệ.");
            if (newQty < 0) throw new ArgumentException("Số lượng không hợp lệ.");
            return _repo.UpdateQuantity(serviceId, newQty);
        }

        // TĂNG số lượng tồn theo delta (dùng cho nhập kho)
        public bool IncreaseServiceQuantity(int serviceId, int add)
        {
            if (serviceId <= 0) throw new ArgumentException("ID không hợp lệ.");
            if (add <= 0) throw new ArgumentException("Số lượng nhập phải > 0.");
            return _repo.IncreaseQuantity(serviceId, add);
        }

        public bool DeleteService(int id) => _repo.DeleteService(id);
        public int GetQuantity(int id) => _repo.GetQuantity(id);
    }
}
