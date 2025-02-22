using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using theBankApi.Core.Interfaces;
using theBankApi.Middleware.Validators;

namespace theBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService customersService;
        private readonly IValidator<int?> pagenumberValidator;

        public CustomersController(ICustomersService customersService,IValidator<int?> pagenumberValidator)
        {
            this.customersService = customersService;
            this.pagenumberValidator = pagenumberValidator;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers([FromQuery] int pageNumber)
        {
            var validationResult = pagenumberValidator.Validate(pageNumber);

            if(!validationResult.IsValid)
            {
                BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

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
