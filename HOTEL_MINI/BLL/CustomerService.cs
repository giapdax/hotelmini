using HOTEL_MINI.DAL;
using HOTEL_MINI.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HOTEL_MINI.BLL
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;
        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }
        public Customer getCustomerByIDNumber(string idNumber)
        {
            if(_customerRepository.checkExistNumberID(idNumber) == false)
            {
                return null;
            }
            return _customerRepository.GetCustomerByIDNumber(idNumber);
        }
        public Customer addNewCustomer(Customer customer)
        {
            return _customerRepository.AddNewCustomer(customer);
        }
        public bool checkExistNumberID(string idNumber)
        {
            return _customerRepository.checkExistNumberID(idNumber);
        }
    }
}
