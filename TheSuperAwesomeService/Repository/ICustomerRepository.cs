using System;
using System.Collections.Generic;
using TheSuperAwesomeService.Models;

namespace TheSuperAwesomeService.Repository
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(Guid customerId);
        void AddCustomer(Customer customer);
    }
}