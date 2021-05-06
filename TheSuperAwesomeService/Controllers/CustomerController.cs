using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using TheSuperAwesomeService.Models;
using TheSuperAwesomeService.Services;

namespace TheSuperAwesomeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly IPricingService _pricingService;

        public CustomerController(ICustomerService customerService, IPricingService pricingService)
        {
            _pricingService = pricingService;
        }

        [HttpGet]
        public string Get([FromQuery]DtoGetCustomerPrice customerPrice)
        {
            var currancy = _pricingService.GetPrices(customerPrice.CustomerId, customerPrice.Start, customerPrice.End);
            return string.Format(new CultureInfo("de-DE"), "{0:c}", currancy);
        }
    }
}
