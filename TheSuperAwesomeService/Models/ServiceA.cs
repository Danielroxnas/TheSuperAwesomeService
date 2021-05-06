
using System;

namespace TheSuperAwesomeService.Models
{
    public class ServiceA : IService
    {
        public ServiceA(DateTime startDate)
        {
            StartDate = startDate;
        }
        public DateTime StartDate { get; }
        public decimal Price { get; set; } = 0.2M;
        public bool WorkDayOnly => true;

        public string ServiceId => "A";
    }
}
