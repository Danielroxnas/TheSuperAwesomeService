using System;
using System.Collections.Generic;

namespace TheSuperAwesomeService.Models
{
    public interface ICustomer
    {
        Guid CustomerId { get; }
        List<Discount> Discounts { get; set; } 
        int FreeDays { get; set; }
        List<IService> Services { get; set; }
    }
}