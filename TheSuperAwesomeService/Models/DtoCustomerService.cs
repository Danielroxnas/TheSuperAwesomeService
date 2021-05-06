using System;

namespace TheSuperAwesomeService.Models
{
    public class DtoCustomerService
    {
        public string ServiceId { get; set; }
        public Guid CustomerId { get; set; }
    }
    public class DtoAddCustomerService
    {
        public decimal ServicePrice { get; set; }
        public string ServiceId { get; set; }
        public Guid CustomerId { get; set; }
    }
    public class DtoGetCustomerPrice
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Guid CustomerId { get; set; }
    }
}
