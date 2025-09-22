using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace HOTEL_MINI.BLL
{
    internal class ServicesService
    {
        private readonly ServicesRepository _serviceRepository;

        public ServicesService()
        {
            _serviceRepository = new ServicesRepository();
        }

        public List<Service> GetAllServices() { return _serviceRepository.GetAllServices(); }

        public bool AddService(Service service)
        {
            Validate(service);
            return _serviceRepository.AddService(service);
        }

        public bool UpdateService(Service service)
        {
            Validate(service);
            return _serviceRepository.UpdateService(service);
        }

        public bool UpdateServiceQuantity(int serviceId, int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Số lượng tồn kho không thể là số âm.");

            return _serviceRepository.UpdateServiceQuantity(serviceId, quantity);
        }

        public bool DeleteService(int serviceId)
        {
            if (serviceId <= 0)
                throw new ArgumentException("ServiceId không hợp lệ.");

            try
            {
                return _serviceRepository.DeleteService(serviceId);
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                throw new InvalidOperationException("Không thể xoá: Dịch vụ đang được sử dụng.", ex);
            }
        }

        public int GetQuantity(int serviceId) { return _serviceRepository.GetQuantity(serviceId); }
            

        private void Validate(Service s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));

            if (s.Price < 0) throw new ArgumentException("Giá dịch vụ không thể là số âm.");
            if (s.Quantity < 0) throw new ArgumentException("Số lượng tồn kho không thể là số âm.");

            var name = (s.ServiceName ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Tên dịch vụ không được để trống.");

            bool nameTaken = _serviceRepository
                .GetAllServices()
                .Any(x =>
                    x.ServiceID != s.ServiceID &&
                    string.Equals((x.ServiceName ?? string.Empty).Trim(), name, StringComparison.OrdinalIgnoreCase));

            if (nameTaken)
                throw new ArgumentException("Tên dịch vụ đã tồn tại. Vui lòng chọn tên khác.");
        }
    }
}
