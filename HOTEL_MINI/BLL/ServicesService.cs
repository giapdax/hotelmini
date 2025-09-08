// using ...

using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System.Collections.Generic;

namespace HOTEL_MINI.BLL
{
    internal class ServicesService
    {
        // Nên để là readonly để đảm bảo nó không bị thay đổi sau khi khởi tạo
        private readonly ServicesRepository _serviceRepository;

        public ServicesService()
        {
            _serviceRepository = new ServicesRepository();
        }

        public List<Service> GetAllServices()
        {
            // Trong một ứng dụng phức tạp hơn, bạn có thể thêm logic nghiệp vụ ở đây
            // Ví dụ: kiểm tra quyền, xác thực dữ liệu,...
            return _serviceRepository.GetAllServices();
        }

        public bool AddService(Service service)
        {
            // Ví dụ về logic nghiệp vụ:
            if (service.Price < 0)
            {
                // throw new ArgumentException("Giá dịch vụ không thể là số âm.");
                return false;
            }
            return _serviceRepository.AddService(service);
        }

        public bool UpdateService(Service service)
        {
            return _serviceRepository.UpdateService(service);
        }

        public bool DeleteService(int serviceId)
        {
            return _serviceRepository.DeleteService(serviceId);
        }
        public void UpdateServiceQuantity(int serviceId, int quantity)
        {
            _serviceRepository.UpdateQuantity(serviceId, quantity);
        }
        public int GetQuantity(int serviceId)
        {
            return _serviceRepository.GetQuantity(serviceId);
        }
    }
}