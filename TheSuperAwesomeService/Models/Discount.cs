using System;

namespace TheSuperAwesomeService.Models
{
    public class Discount 
    {
        public string ServiceId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal Percent { get; set; }

    }
}