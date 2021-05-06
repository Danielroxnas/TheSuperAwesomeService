using Microsoft.AspNetCore.Mvc;
using TheSuperAwesomeService.Models;
using TheSuperAwesomeService.Services;

namespace TheSuperAwesomeService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        public ServiceController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPatch]
        public ActionResult Patch(DtoAddCustomerService service)
        {
            _customerService.UpdateServicePrice(service);
            return Ok();
        }

        [HttpPost]
        public ActionResult Post(DtoCustomerService customerService)
        {
             _customerService.AddService(customerService);
            return Ok();
            
        }



    }
}
