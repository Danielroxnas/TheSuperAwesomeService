using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheSuperAwesomeService.Models
{
    public class Customer : ICustomer
    {
        public Customer(Guid customerId)
        {
            CustomerId = customerId;
        }
        public Customer()
        {
            CustomerId = Guid.NewGuid();
        }
        public Guid CustomerId { get; }
        public int FreeDays { get; set; } = 0;
        public List<IService> Services { get; set; } = new List<IService>();
        public List<Discount> Discounts { get; set; } = new List<Discount>();
    }
}
