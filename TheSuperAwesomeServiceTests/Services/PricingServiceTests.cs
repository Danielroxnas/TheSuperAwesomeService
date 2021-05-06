using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TheSuperAwesomeService.Models;
using TheSuperAwesomeService.Services;

namespace TheSuperAwesomeServiceTests.Services
{
    public class PricingServiceTests
    {
        private PricingService _sut;
        private Guid _customerId;
        private Customer _customer;
        private ICustomerService _customerService;

        [SetUp]
        public void Setup()
        {
            _customerId = Guid.NewGuid();
            List<IService> services = new List<IService> { new ServiceA(new DateTime(2020,10,01)) };
            _customer = new Customer(_customerId)
            {
                Services = services
            };

            _customerService = Mock.Of<ICustomerService>();
            Mock.Get(_customerService).Setup(x => x.GetCustomer(_customerId)).Returns(() => _customer);
            _sut = new PricingService(_customerService);
        }

        [Test]
        public void GetPrices_should_return_price()
        {
            var result = _sut.GetPrices(_customerId, new DateTime(2020, 10, 01), new DateTime(2020,10,07));
            Assert.That(result, Is.EqualTo(1.0));
        }

        [Test]
        public void GetPrices_should_return_price_with_freedays()
        {
            _customer.FreeDays = 2;
            var result = _sut.GetPrices(_customerId, new DateTime(2020, 10, 01), new DateTime(2020, 10, 07));
            Assert.That(result, Is.EqualTo(0.6));
        }

        [Test]
        public void GetPrices_should_call_CustomerService_GetCustomer_once()
        {
            var result = _sut.GetPrices(_customerId, new DateTime(2020, 10, 01), new DateTime(2020, 10, 07));
            Mock.Get(_customerService).Verify(x => x.GetCustomer(_customerId), Times.Once);
        }

        [Test]
        public void GetPrices_should_return_price_with_discount()
        {
            _customer.Discounts = new List<Discount> { new Discount { ServiceId = "A", Start = new DateTime(2020, 10, 01) , End = new DateTime(2020, 10, 07), Percent= 50 } };
            var result = _sut.GetPrices(_customerId, new DateTime(2020, 10, 01), new DateTime(2020, 10, 07));
            Assert.That(result, Is.EqualTo(0.5));
        }

        [Test]
        public void GetPrices_should_return_price_without_discount_if_date_is_out_of_scope()
        {
            _customer.Discounts = new List<Discount> { new Discount { ServiceId = "A", Start = new DateTime(2010, 10, 01), End = new DateTime(2010, 10, 07), Percent = 50 } };
            var result = _sut.GetPrices(_customerId, new DateTime(2020, 10, 01), new DateTime(2020, 10, 07));
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void GetPrices_should_return_price_with_discount_and_freedays()
        {
            _customer.FreeDays = 2;
            _customer.Discounts = new List<Discount> { new Discount { ServiceId = "A", Start = new DateTime(2020, 10, 01), End = new DateTime(2020, 10, 07), Percent = 50 } };
            var result = _sut.GetPrices(_customerId, new DateTime(2020, 10, 01), new DateTime(2020, 10, 07));
            Assert.That(result, Is.EqualTo(0.3));
        }

        [Test]
        public void GetPrices_should_throw_error_if_customerId_is_empty()
        {
            Assert.Throws<ArgumentException>(() => _sut.GetPrices(Guid.Empty, new DateTime(2020, 10, 01), new DateTime(2020, 10, 07)));
        }

        [Test]
        public void GetPrices_should_throw_error_if_customerId_not_match_customer()
        {
            Assert.Throws<ArgumentException>(() => _sut.GetPrices(Guid.NewGuid(), new DateTime(2020, 10, 01), new DateTime(2020, 10, 07)));
        }
    }
}