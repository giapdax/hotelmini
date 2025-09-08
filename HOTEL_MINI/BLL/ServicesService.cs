// File: ServicesService.cs
using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System.Collections.Generic;
using System; // Thêm using System; để dùng ArgumentException

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
        {
            return _serviceRepository.GetAllServices();
        }

        public bool AddService(Service service)
        {
            if (service.Price < 0)
            {
                throw new ArgumentException("Giá dịch vụ không thể là số âm.");
            }
            // Đảm bảo Quantity cũng hợp lệ
            if (service.Quantity < 0)
            {
                throw new ArgumentException("Số lượng tồn kho không thể là số âm.");
            }
            return _serviceRepository.AddService(service);
        }

        public bool UpdateService(Service service)
        {
            if (service.Price < 0)
            {
                throw new ArgumentException("Giá dịch vụ không thể là số âm.");
            }
            if (service.Quantity < 0)
            {
                throw new ArgumentException("Số lượng tồn kho không thể là số âm.");
            }
            return _serviceRepository.UpdateService(service);
        }

        // Phương thức mới để cập nhật số lượng
        public bool UpdateServiceQuantity(int serviceId, int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException("Số lượng tồn kho không thể là số âm.");
            }
            return _serviceRepository.UpdateServiceQuantity(serviceId, quantity);
        }

        public bool DeleteService(int serviceId)
        {
            return _serviceRepository.DeleteService(serviceId);
        }
    }
}