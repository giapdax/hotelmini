// File: ServicesService.cs
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;

namespace HOTEL_MINI.BLL
{
    internal class ServicesService
    {
        private readonly ServicesRepository _serviceRepository;

        public ServicesService()
        {
            _serviceRepository = new ServicesRepository();
        }

        public List<Service> GetAllServices()
            => _serviceRepository.GetAllServices();

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

        // Giữ 1 hàm DUY NHẤT để cập nhật số lượng
        public bool UpdateServiceQuantity(int serviceId, int quantity)
        {
            if (quantity < 0)
                throw new ArgumentException("Số lượng tồn kho không thể là số âm.");

            return _serviceRepository.UpdateServiceQuantity(serviceId, quantity);
        }

        public bool DeleteService(int serviceId)
            => _serviceRepository.DeleteService(serviceId);

        public int GetQuantity(int serviceId)
            => _serviceRepository.GetQuantity(serviceId);

        private static void Validate(Service s)
        {
            if (s.Price < 0) throw new ArgumentException("Giá dịch vụ không thể là số âm.");
            if (s.Quantity < 0) throw new ArgumentException("Số lượng tồn kho không thể là số âm.");
        }
    }
}
