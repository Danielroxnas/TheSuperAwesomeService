
using System;

namespace TheSuperAwesomeService.Models
{
    public class ServiceC : IService
    {
        public ServiceC(DateTime startDate)
        {
            StartDate = startDate;
        }
        public DateTime StartDate { get; }
        public decimal Price { get; set; } = 0.4M;
        public bool WorkDayOnly => false;

        public string ServiceId => "C";
    }
}
