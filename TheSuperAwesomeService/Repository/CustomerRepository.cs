using System;
using System.Collections.Generic;
using System.Linq;
using TheSuperAwesomeService.Models;
using TheSuperAwesomeService.Initcustomers;

namespace TheSuperAwesomeService.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private List<Customer> _customers;

        public CustomerRepository()
        {
            _customers = InitCustomers.Init();
        }
        public void AddCustomer(Customer customer) => _customers.Add(customer);
        public Customer GetCustomer(Guid customerId)
        {
            return _customers.FirstOrDefault(x => x.CustomerId == customerId);
        }
    }
}
