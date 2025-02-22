using AutoMapper;
using Inlämningsuppgift3.Core.Services;
using Inlämningsuppgift3.Data.Interfaces;
using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlamningsuppgift3.Test.BankTesting
{
    public class LoanTest
    {
        private readonly Mock<ILoansRepo> mockLoansRepo;
        private readonly BankService bankService;

        //För att bygga upp bankservice
        private readonly Mock<IAccountsRepo> mockAccountsRepo;
        private readonly Mock<IAccountTypesRepo> mockAccountTypesRepo;
        private readonly Mock<IDispositionsRepo> mockDispositionsRepo;
        private readonly Mock<ITransactionsRepo> mockTransactionsRepo;
        private readonly Mock<IMapper> mockMapper;
        //

        public LoanTest()
        {
            mockLoansRepo = new Mock<ILoansRepo>();

            //
            mockAccountsRepo = new Mock<IAccountsRepo>();
            mockAccountTypesRepo = new Mock<IAccountTypesRepo>();
            mockDispositionsRepo = new Mock<IDispositionsRepo>();
            mockTransactionsRepo = new Mock<ITransactionsRepo>();
            mockMapper = new Mock<IMapper>();
            //

         
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
        public void CreateLoanSucces()
        {
            int accountid = 1;
            decimal amount = 200m;
            int duration = 24;
            string status = "Active";

            var account = new Accounts { AccountId=accountid, Balance = 0, Frequency = "Monthly", Created = DateOnly.FromDateTime(DateTime.Now) };
        
            mockLoansRepo.Setup(repo=>repo.GetLoan(accountid)).Returns<Loans>(null);
            mockAccountsRepo.Setup(repo=>repo.GetanAccount(accountid)).Returns(account);

            mockAccountsRepo.Setup(repo=>repo.IncreaseBalance(accountid,amount)).Returns(true);

            mockLoansRepo.Setup(repo => repo.CreateLoan(accountid, amount, duration, status)).Returns(true);

            mockTransactionsRepo.Setup(repo => repo.CreateTransaction(accountid, amount, account.Balance, "Loan")).Returns(true);

            var result = bankService.CreateLoan(accountid,amount,duration, status);

            // Assert
            Assert.True(result);

            mockLoansRepo.Verify(repo => repo.GetLoan(accountid), Times.Once);
            mockAccountsRepo.Verify(repo => repo.GetanAccount(accountid),Times.Once);
            mockAccountsRepo.Verify(repo => repo.IncreaseBalance(accountid, amount), Times.Once);
            mockLoansRepo.Verify(repo => repo.CreateLoan(accountid, amount, duration, status), Times.Once);
            mockTransactionsRepo.Verify(repo => repo.CreateTransaction(accountid, amount, account.Balance, "Loan"), Times.Once);

        }


    }
}
