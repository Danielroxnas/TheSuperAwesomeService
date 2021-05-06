using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TheSuperAwesomeService.Models;
using TheSuperAwesomeService.Repository;
using TheSuperAwesomeService.Services;

namespace TheSuperAwesomeServiceTests
{
    public class Scenarios
    {
        [Test]
        public void scenarios1()
        {
            var customerService = new CustomerService(new CustomerRepository());
            var customer = new Customer()
            {
                Discounts = new List<Discount> { new Discount {
                    ServiceId = "c",
                    Start=new DateTime(2019,09,22),
                    End = new DateTime(2019,09,24),
                    Percent=20} },
                Services = new List<IService> {
                    new ServiceA(new DateTime(2019,09,20)),
                    new ServiceC(new DateTime(2019,09,20)) }
            };
            customerService.AddCustomer(customer);

            var sut = new PricingService(customerService);
            var result = sut.GetPrices(customer.CustomerId, new DateTime(2019, 09, 20), new DateTime(2019, 10, 01));

            Assert.That(result, Is.EqualTo(6.16M));
        }

        [Test]
        public void scenarios2()
        {
            var customerService = new CustomerService(new CustomerRepository());
            var customer = new Customer()
            {
                Discounts = new List<Discount> { 
                    new Discount {
                        ServiceId = "B",
                        Start = new DateTime(2018, 01, 01),
                        Percent = 30 },
                    new Discount {
                        ServiceId = "C",
                        Start = new DateTime(2018, 01, 01),
                        Percent = 30 } },
                Services = new List<IService> {
                    new ServiceB(new DateTime(2018, 01, 01)),
                    new ServiceC(new DateTime(2018, 01, 01)) },
                FreeDays = 200
            };
            customerService.AddCustomer(customer);

            var sut = new PricingService(customerService);
            var result = sut.GetPrices(customer.CustomerId, customer.Services.First().StartDate, new DateTime(2019, 10, 01));
            Assert.That(result, Is.EqualTo(175.504M));
        }
    }
}
