using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HOTEL_MINI.BLL
{
    /// <summary>
    /// Thin service cho bảng Services (CRUD, get stock). KHÔNG orchestration.
    /// </summary>
    public class ServicesService
    {
        private readonly ServicesRepository _repo = new ServicesRepository();

        public List<Service> GetAllServices() => _repo.GetAllServices();
        public int GetQuantity(int serviceId) => _repo.GetQuantity(serviceId);

        public bool AddService(Service s) => _repo.AddService(s);

        public bool UpdateService(Service s) => _repo.UpdateService(s);

        public bool UpdateServiceQuantity(int id, int qty)
        {
            if (qty < 0) throw new ArgumentException("Số lượng tồn kho không thể âm.");
            // dùng UpdateService với entity nếu bạn muốn, còn đây tách riêng cũng ok
            var fake = new Service { ServiceID = id, ServiceName = "", Price = 0, IsActive = true, Quantity = qty };
            return _repo.UpdateService(fake);
        }

        public bool DeleteService(int id)
        {
            if (id <= 0) throw new ArgumentException("ServiceId không hợp lệ.");
            try { return _repo.DeleteService(id); }
            catch (SqlException ex) when (ex.Number == 547)
            { throw new InvalidOperationException("Không thể xoá: Dịch vụ đang được sử dụng.", ex); }
        }
    }
}
