using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using theBankApi.Core.Interfaces;

namespace theBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService customersService;

        public CustomersController(ICustomersService customersService)
        {
            this.customersService = customersService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers([FromQuery] int pageNumber)
        {
            var result = customersService.GetCustomers(pageNumber);

            if(result.Count > 0)
            {
                return Ok(new { result = result});
            }
            else
            {
                return BadRequest(); //BadRequest?? something ele?
            }
        }
    }
}
