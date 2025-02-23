using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        private int ReadCustomerID()
        {
            try
            {

                var idstring = User.FindFirstValue("CustomerID");
                if (idstring == null) { return -1; }

                int theID = int.Parse(idstring);

                return theID;
            }
            catch(Exception ex)
            {
                return -1; //Log??
            }
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

        //[Authorize]
        [HttpGet("GetACustomer")]
        public IActionResult GetACustomer([FromQuery] int customerid) //Validate no nulls or 0??
        {
            var result = customersService.GetCustomerById(customerid);
            if (result != null)
            {
                return Ok(new { result = result });
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpGet("GetMyCustomer")]
        public IActionResult GetMyCustomer()
        {
            int customerID = ReadCustomerID();
            if(customerID == -1) { return BadRequest("ID could not be read from token!"); }

            var result = customersService.GetCustomerById(customerID);

            if (result != null)
            {
                return Ok(new { result = result });
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
