using System;
using System.Collections.Generic;
using System.Linq;
using TheSuperAwesomeService.Models;
using TheSuperAwesomeService.Repository;

namespace TheSuperAwesomeService.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public void AddService(DtoCustomerService customerService)
        {
            var customer = _customerRepository.GetCustomer(customerService.CustomerId);
            var service = CreateServiceByServiceId(customerService.ServiceId);

            customer.Services.Add(service);
        }

        public Customer GetCustomer(Guid customerId) { 
            if(customerId == null || customerId == Guid.Empty)
            {
                return null;
            }
            return _customerRepository.GetCustomer(customerId);
        }

        public void AddCustomer(Customer customer) => _customerRepository.AddCustomer(customer);

        public void UpdateServicePrice(DtoAddCustomerService customerService)
        {
            var customer = _customerRepository.GetCustomer(customerService.CustomerId);
            var service = customer.Services.Single(x => x.ServiceId == customerService.ServiceId);
            service.Price = customerService.ServicePrice;
        }

        private IService CreateServiceByServiceId(string serviceId) => serviceId switch
        {

            "A" => new ServiceA(DateTime.Now),
            "B" => new ServiceB(DateTime.Now),
            "C" => new ServiceC(DateTime.Now),
            _ => throw new Exception("Invalid ServiceId")

        };
    }
}
