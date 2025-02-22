using Azure.Core;
using Inlämningsuppgift3.Core.Interfaces;
using theBankApi.Domain.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inlämningsuppgift3.Controllers
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

        [HttpGet("Login")]
        public IActionResult Login([FromQuery] string username, string password)
        {
            var result = service.Login(username, password);
            if (result != null)
            {
                return Ok(new { success = true, token = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Failed to login", timestamp = DateTime.UtcNow });
            }
        }

        //Auth senare
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
