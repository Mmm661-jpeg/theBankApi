using Azure.Core;
using theBankApi.Domain.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using theBankApi.Core.Interfaces;

namespace theBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService service;

        public UsersController(IUsersService service)
        {
            this.service = service;
        }

        [HttpPost("Login")] 
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = service.Login(loginRequest);
            if (result != null)
            {
                return Ok(new { success = true, token = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Failed to login", timestamp = DateTime.UtcNow });
            }
        }

      
        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = service.Register(request);
            if(result)
            {
                return Ok(new { success = true, message = "Registration successful!", timestamp = DateTime.UtcNow });
            }
            else
            {
                return BadRequest(new { success = false, message = "Registration failed!", timestamp = DateTime.UtcNow });
            }
        }
    }
}
