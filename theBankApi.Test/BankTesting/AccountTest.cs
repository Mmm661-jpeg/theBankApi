using theBankApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using theBankApi.Controllers;
using theBankApi.Domain.Models;
using theBankApi.Domain.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace Inlamningsuppgift3.Test.BankTesting
{
    public class AccountTest
    {
        
        private readonly Mock<IBankService> _mockBankService;
        private readonly BankController bankController;
        
        public AccountTest() //Register
        {
            _mockBankService = new Mock<IBankService>();
            bankController = new BankController(_mockBankService.Object);  
        }

        [Fact]

        public void GetAccountTypes()
        {
            var mockAccountTypes = new List<AccountTypesDTO>()
            { new AccountTypesDTO()
                {
                AccountTypeId = 1,
                TypeName = "Savings",
                Description = "For saving"

                },
            

              new AccountTypesDTO()
              {
                  AccountTypeId= 2,
                  TypeName = "Spending",
                  Description = "For spending"
              }

            };

            _mockBankService.Setup(service => service.GetAccountTypes()).Returns(mockAccountTypes);

            var result = bankController.GetAccountTypes();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = okResult.Value as dynamic;

            // Assert
            Assert.NotNull(response);
            Assert.True((bool)response.GetType().GetProperty("success")?.GetValue(response));
            Assert.NotNull(response.GetType().GetProperty("result")?.GetValue(response));

            //  Ok(new { success = true, timestamp = DateTime.UtcNow, result = result});


        }



    }
}
