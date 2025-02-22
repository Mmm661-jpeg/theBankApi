using AutoMapper;
using theBankApi.Controllers;
using theBankApi.Core.Interfaces;
using theBankApi.Core.Services;
using theBankApi.Data.Interfaces;
using theBankApi.Data.Repository;
using theBankApi.Domain.Models;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamningsuppgift3.Test.BankTesting
{
    public class Deposit_Withdraw_Send_Test
    {
        private readonly Mock<IAccountsRepo> mockAccountsRepo;
        private readonly Mock<IAccountTypesRepo> mockAccountTypesRepo;
        private readonly Mock<IDispositionsRepo> mockDispositionsRepo;
        private readonly Mock<ILoansRepo> mockLoansRepo;
        private readonly Mock<ITransactionsRepo> mockTransactionsRepo;
        private readonly Mock<IMapper> mockMapper;

        private readonly BankService bankService;


        public Deposit_Withdraw_Send_Test()
        {
            mockAccountsRepo = new Mock<IAccountsRepo>();
            mockAccountTypesRepo = new Mock<IAccountTypesRepo>();
            mockDispositionsRepo = new Mock<IDispositionsRepo>();
            mockLoansRepo = new Mock<ILoansRepo>();
            mockTransactionsRepo = new Mock<ITransactionsRepo>();
            mockMapper = new Mock<IMapper>();

         
            bankService = new BankService(
                mockAccountsRepo.Object,
                mockAccountTypesRepo.Object,
                mockDispositionsRepo.Object,
                mockLoansRepo.Object,
                mockTransactionsRepo.Object,
                mockMapper.Object
            );

       
        }

        [Fact]
        public void DepositReturnOk()
        {

          
            var accountId = 1;
            var customerId = 123;
            var amount = 100m;


            var account = new Accounts { AccountId = accountId, Balance = 500, Frequency = "Monthly" };

       
            mockDispositionsRepo.Setup(repo => repo.GetCustomerID(accountId)).Returns(customerId);

            mockAccountsRepo.Setup(repo => repo.GetanAccount(accountId)).Returns(account);

            mockAccountsRepo.Setup(repo => repo.IncreaseBalance(accountId, amount)).Returns(true);

            mockTransactionsRepo.Setup(repo => repo.CreateTransaction(accountId, amount, account.Balance, "Deposit")).Returns(true);

         
            var result = bankService.Deposit(accountId, customerId, amount);

            // Assert
            Assert.True(result);
            mockAccountsRepo.Verify(repo => repo.IncreaseBalance(accountId, amount), Times.Once);
            mockTransactionsRepo.Verify(repo => repo.CreateTransaction(accountId, +amount, account.Balance, "Deposit"), Times.Once);

        }

        [Fact]
        public void WithdrawReturnOk()
        {
        
            var accountId = 1;
            var customerId = 123;
            var amount = 50m;

         
            var account = new Accounts { AccountId = accountId, Balance = 500m, Frequency = "Monthly", Created = DateOnly.FromDateTime(DateTime.Now) };

         
            mockDispositionsRepo.Setup(repo => repo.GetCustomerID(accountId)).Returns(customerId);

         
            mockAccountsRepo.Setup(repo => repo.GetanAccount(accountId)).Returns(account);

           
            mockAccountsRepo.Setup(repo => repo.DecreaseBalance(accountId, amount)).Returns(true);

            
            mockTransactionsRepo.Setup(repo => repo.CreateTransaction(accountId, -amount, account.Balance, "Withdrawl")).Returns(true);

        
            var result = bankService.Withdraw(accountId, customerId, amount);  

            //Debugg
            Console.WriteLine($"Withdraw Test Result: {result}");
            Console.WriteLine($"Expected Balance After Withdrawal: {account.Balance - amount}");

            // Assert
            Assert.True(result);  
            mockAccountsRepo.Verify(repo => repo.DecreaseBalance(accountId, amount), Times.Once);  
            mockTransactionsRepo.Verify(repo => repo.CreateTransaction(accountId, -amount, account.Balance, "Withdrawl"), Times.Once);  
        }


        [Fact]
        public void SendIsSuccessful()
        {

            var senderAccountId = 1;
            var receiverAccountId = 2;
            var senderCustomerId = 123;
            var amount = 100m;

            var senderAccount = new Accounts { AccountId = senderAccountId, Balance = 500, Frequency = "Monthly", Created = DateOnly.FromDateTime(DateTime.Now) };
            var receiverAccount = new Accounts { AccountId = receiverAccountId, Balance = 200, Frequency = "Monthly", Created = DateOnly.FromDateTime(DateTime.Now) };

  
            mockDispositionsRepo.Setup(repo => repo.GetCustomerID(senderAccountId)).Returns(senderCustomerId);

            mockAccountsRepo.Setup(repo => repo.GetanAccount(senderAccountId)).Returns(senderAccount);
            mockAccountsRepo.Setup(repo => repo.GetanAccount(receiverAccountId)).Returns(receiverAccount);

    
            mockAccountsRepo.Setup(repo => repo.DecreaseBalance(senderAccountId,amount)).Returns(true);

            mockAccountsRepo.Setup(repo => repo.IncreaseBalance(receiverAccountId, amount)).Returns(true);

  
            mockTransactionsRepo.Setup(repo => repo.CreateTransaction(senderAccount.AccountId, -amount, senderAccount.Balance, "Sending money")).Returns(true);
            mockTransactionsRepo.Setup(repo => repo.CreateTransaction(receiverAccount.AccountId, +amount, receiverAccount.Balance, "Recieving money")).Returns(true);

           
            var result = bankService.SendMoney(receiverAccountId, senderAccountId, amount, senderCustomerId);

            // Debugg 
            Console.WriteLine($"Test Result: {result}");
            Console.WriteLine($"Sender Final Balance: {senderAccount.Balance}, Receiver Final Balance: {receiverAccount.Balance}");

            // Assert
            Assert.True(result);
            mockAccountsRepo.Verify(repo => repo.DecreaseBalance(senderAccountId, amount), Times.Once);
            mockAccountsRepo.Verify(repo => repo.IncreaseBalance(receiverAccountId, amount), Times.Once);
            mockTransactionsRepo.Verify(repo => repo.CreateTransaction(senderAccountId,-amount, senderAccount.Balance, "Sending money"), Times.Once);
            mockTransactionsRepo.Verify(repo => repo.CreateTransaction(receiverAccountId, +amount, receiverAccount.Balance, "Recieving money"), Times.Once);
        }
    }
}
