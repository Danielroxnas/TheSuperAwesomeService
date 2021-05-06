using System;
using System.Collections;
using System.Collections.Generic;
using TheSuperAwesomeService.Controllers;
using TheSuperAwesomeService.Models;

namespace TheSuperAwesomeService.Services
{
    public interface ICustomerService
    {
        Customer GetCustomer(Guid customerId);
        void UpdateServicePrice(DtoAddCustomerService service);
        void AddService(DtoCustomerService customerService);
        void AddCustomer(Customer customer);
    }
}