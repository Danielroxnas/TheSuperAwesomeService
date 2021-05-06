using System;
using System.Collections.Generic;
using System.Linq;
using TheSuperAwesomeService.Models;

namespace TheSuperAwesomeService.Services
{
    public class PricingService : IPricingService
    {
        private readonly ICustomerService _customerService;

        public PricingService(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public decimal GetPrices(Guid customerId, DateTime start, DateTime end)
        {
            var customer = _customerService.GetCustomer(customerId);
            if (customer is null)
            {
                throw new ArgumentException($"Could not found customer with id: {customerId}");
            }
            var totalPrice = 0M;
            foreach (DateTime day in EachDay(start, end))
            {

                foreach (var service in customer.Services)
                {

                    if (service.WorkDayOnly && !IsWorkDay(day))
                    {
                        continue;
                    }
                    if (customer.FreeDays > 0)
                    {
                        customer.FreeDays--;
                        break;
                    }
                    var percent = GetPercent(customer, day, service);
                    totalPrice += percent != 0 ? service.Price - ((percent / 100) * service.Price) : service.Price;
                }
            }
            return totalPrice;
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }
        private bool IsWorkDay(DateTime day) => day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday;

        private decimal GetPercent(Customer customer, DateTime day, IService service)
        {
            var discounts = customer.Discounts.Where(x => x.ServiceId.ToLower() == service.ServiceId.ToLower());

            if (!discounts.Any())
            {
                return 0;
            }
            var discount = discounts.FirstOrDefault(x => x.Start <= day && (x.End >= day || x.End == DateTime.MinValue));
            return discount != null ? discount.Percent : 0;
        }

    }
}