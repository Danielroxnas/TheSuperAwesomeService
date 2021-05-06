
using System;

namespace TheSuperAwesomeService.Models
{
    public class ServiceB : IService
    {
        public ServiceB(DateTime startDate)
        {
            StartDate = startDate;
        }
        public DateTime StartDate { get; }

        public decimal Price { get; set; } = 0.24M;
        public bool WorkDayOnly => true;

        public string ServiceId => "B";
    }
}
