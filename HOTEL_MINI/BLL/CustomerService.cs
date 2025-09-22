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
            //if(_customerRepository.checkExistNumberID(idNumber) == false)
            //{
            //    return null;
            //}
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
        public Customer getCustomerByCustomerID(int customerID)
        {
            return _customerRepository.GetCustomerByCustomerID(customerID);
        }
        public Customer GetCustomerByNumberID(string numberID)
        {
            return _customerRepository.GetCustomerByIDNumber(numberID);
        }
        public List<Customer> getAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public bool updateCustomer(Customer customer)
        {
            return _customerRepository.UpdateCustomer(customer);
        }

        public List<string> getAllGender()
        {
            return _customerRepository.getAllGender();
        }
        public Dictionary<int, int> getBookingCounts()
        {
            return _customerRepository.GetBookingCounts();
        }

    }
}
