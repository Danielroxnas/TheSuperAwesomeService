using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TheSuperAwesomeService.Models;
using TheSuperAwesomeService.Repository;
using TheSuperAwesomeService.Services;

namespace TheSuperAwesomeServiceTests.Services
{
    public class CustomerServiceTests
    {
        private CustomerService _sut;
        private ICustomerRepository _customerRepository;
        private Guid _customerId;
        private Customer _customer;

        [SetUp]
        public void Setup()
        {
            _customerRepository = Mock.Of<ICustomerRepository>();
            _customerId = Guid.NewGuid();
            _customer = new Customer(_customerId);
            var customers = new List<Customer> { new Customer(Guid.NewGuid()), new Customer(Guid.NewGuid()) };
            Mock.Get(_customerRepository).Setup(x => x.GetCustomer(_customerId)).Returns(() => _customer);
            _sut = new CustomerService(_customerRepository);
        }

        [Test]
        public void GetCustomer_should_return_a_customer()
        {
            var result = _sut.GetCustomer(_customerId);
            Assert.That(result.CustomerId, Is.EqualTo(_customerId));
        }
        [Test]
        public void GetCustomer_Should_call_CustomerRepository_GetCustomer_once()
        {
            var result = _sut.GetCustomer(_customerId);
            Mock.Get(_customerRepository).Verify(x => x.GetCustomer(_customerId), Times.Once);
        }

        [Test]
        public void GetCustomer_should_return_null_if_guid_is_empty()
        {
            var result = _sut.GetCustomer(Guid.Empty);
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetCustomer_should_return_null_if_no_match()
        {
            var result = _sut.GetCustomer(Guid.NewGuid());
            Assert.That(result, Is.Null);
        }

        [Test]
        public void AddService_should_call_CustomerRepository_GetCustomer_once()
        {
            var c = new DtoCustomerService() { CustomerId = _customerId, ServiceId = "A" };
            _sut.AddService(c);
            Mock.Get(_customerRepository).Verify(x => x.GetCustomer(c.CustomerId), Times.Once);

        }

        [Test]
        public void AddService_should_add_new_service_to_customer()
        {
            var c = new DtoCustomerService() { CustomerId = _customerId, ServiceId = "C" };
            _sut.AddService(c);

            Assert.That(_customer.Services.First(x => x.StartDate.Date == DateTime.Today).ServiceId, Is.EqualTo(c.ServiceId));

        }

        [Test]
        public void UpdateServicePrice_should_update_service_price_to_customer()
        {
            var c = new DtoAddCustomerService() { CustomerId = _customerId, ServiceId = "C", ServicePrice=0.8M };
            _customer.Services.Add(new ServiceC(DateTime.Now));
            _sut.UpdateServicePrice(c);

            Assert.That(_customer.Services.First(x => x.StartDate.Date == DateTime.Today).Price, Is.EqualTo(c.ServicePrice));
        }
    }
}
