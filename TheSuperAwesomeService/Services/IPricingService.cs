using System;

namespace TheSuperAwesomeService.Services
{
    public interface IPricingService
    {
        decimal GetPrices(Guid customerId, DateTime start, DateTime end);
    }
}