using System;
using System.Collections;
using System.Collections.Generic;

namespace TheSuperAwesomeService.Models
{
    public interface IService
    {
        public string ServiceId { get; }
        public DateTime StartDate { get; }
        public decimal Price { get; set; }
        public bool WorkDayOnly { get; }
        
    }
}