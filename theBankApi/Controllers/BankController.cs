using theBankApi.Core.Interfaces;
using theBankApi.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Security.Claims;

namespace theBankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankController : ControllerBase
    {
        
        private readonly IBankService bankService;
        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        private int GetCid()
        {
            var idstring = User.FindFirstValue("CustomerID");
            if (idstring == null) { return -1; }
            
            int theID = int.Parse(idstring);
            
            return theID;
        }



        //GET:






        [Authorize]
        [HttpGet("GetAccountTypes")]
        public IActionResult GetAccountTypes()
        {
            var result = bankService.GetAccountTypes();
            if(result != null)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow, result = result});
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow, result = result });
            }
        }

        [Authorize(Roles ="User")]
        [HttpGet("GetMyTransactions")]
        public IActionResult GetMyTransactions()
        {
            var customerID = GetCid();
            if(customerID ==-1 ) { return BadRequest("CustomerID not found"); }

            var result = bankService.GetAllAccountTransactions(customerID);
            if(result != null)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow, result = result });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow, result = result });
            }
            
        }

        [Authorize(Roles ="Admin")]
        [HttpGet("GetTransactions")] //changename
        public IActionResult GetTransactions([FromQuery] int accountID)
        {
            var result = bankService.GetTransactions(accountID);
            if(result != null)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow, result = result });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow, result = result });
            }
        }

        [Authorize(Roles ="User")]
        [HttpGet("GetAccounts")]
        public IActionResult GetAccounts()
        {
            var customerID = GetCid();
            if (customerID == -1) { return BadRequest("CustomerID not found"); }

            var result = bankService.GetAccounts(customerID);
            if (result != null)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow, result = result });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow, result = result });
            }

        }

        [Authorize(Roles ="User")]
        [HttpGet("GetOneAccount")]

        public IActionResult GetOneAccount([FromQuery] int accountID)
        {
            var customerID = GetCid();
            if (customerID == -1) { return BadRequest("CustomerID not found"); }

            var result = bankService.GetOneAccount(accountID, customerID);
            if (result != null)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow, result = result });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow, result = result });
            }

        }

        [Authorize(Roles ="Admin")]
        [HttpGet("GetLoans")]
        public IActionResult GetLoans()
        {
            var result = bankService.GetLoans();
            if (result != null)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow, result = result });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow, result = result });
            }
        }

        [Authorize(Roles ="User")]
        [HttpGet("GetmyLoan")]
        public IActionResult GetmyLoan([FromQuery] int accountID)
        {
            var customerID = GetCid();
            if (customerID == -1) { return BadRequest("CustomerID not found"); }

            var result = bankService.GetmyLoan(accountID, customerID);
            if (result != null)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow, result = result });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow, result = result });
            }

        }





        //PUT:






        [Authorize(Roles ="User")]
        [HttpPut("Deposit")]
        public IActionResult Deposit([FromBody] WithdrawOrDepositRequest withdrawOrDeposit) 
        {
            var customerID = GetCid();
            if (customerID == -1) { return BadRequest("CustomerID not found"); }

            var result = bankService.Deposit(withdrawOrDeposit.AccountID,customerID,withdrawOrDeposit.Amount);
            if (result)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow });
            }

        }

        [Authorize(Roles ="User")]
        [HttpPut("Withdraw")]
        public IActionResult Withdraw([FromBody] WithdrawOrDepositRequest withdrawOrDeposit) 
        {
            var customerID = GetCid();
            if (customerID == -1) { return BadRequest("CustomerID not found"); }

            var result = bankService.Withdraw(withdrawOrDeposit.AccountID,customerID,withdrawOrDeposit.Amount);
            if (result)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow });
            }

        }

        [Authorize(Roles ="User")]
        [HttpPut("SendMoney")] 
        public IActionResult SendMoney([FromBody] SendMoneyRequest moneyRequest)//FromRequest
        {
            var customerID = GetCid();
            if (customerID == -1) { return BadRequest("CustomerID not found"); }

            var result = bankService.SendMoney(moneyRequest.Toaccountid,moneyRequest.Fromaccountid,moneyRequest.Amount,customerID);
            if (result)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow });
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateLoan")] 
        public IActionResult UpdateLoan([FromQuery] int accountid, decimal? amount = null, int? duration = null, string? status = null)
        {
            var result = bankService.UpdateLoan(accountid, amount, duration, status);
            if (result)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow });
            }
        }






        //POST:




        [Authorize(Roles = "Admin")]
        [HttpPost("CreateLoan")] 
        public IActionResult CreateLoan([FromQuery] int accountid, decimal amount, int duration, string status)//FromRequest
        {
            var result = bankService.CreateLoan(accountid,amount,duration,status);
            if (result)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow });
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount([FromQuery]int accounttype)
        {
            var customerID = GetCid();
            if (customerID == -1) { return BadRequest("CustomerID not found"); }

            var result = bankService.CreateAccount(accounttype, customerID);
            if (result)
            {
                return Ok(new { success = true, timestamp = DateTime.UtcNow });
            }
            else
            {
                return BadRequest(new { success = false, timestamp = DateTime.UtcNow });
            }

        }

       


    }
}
