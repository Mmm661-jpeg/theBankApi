using AutoMapper;
using Inlämningsuppgift3.Core.Interfaces;
using Inlämningsuppgift3.Data.Interfaces;
using theBankApi.Domain.DTOs;
using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Core.Services
{
    public class BankService:IBankService
    {
        private readonly IAccountsRepo accountsRepo;
        private readonly IAccountTypesRepo accountTypesRepo;
        private readonly IDispositionsRepo dispositionsRepo;
        private readonly ILoansRepo loansRepo;
        private readonly ITransactionsRepo transactionsRepo;

        private readonly IMapper mapper;
        
        public BankService(IAccountsRepo accountsRepo, IAccountTypesRepo accountTypesRepo,
            IDispositionsRepo dispositionsRepo, ILoansRepo loansRepo,
            ITransactionsRepo transactionsRepo,IMapper mapper)
        {
            this.accountsRepo = accountsRepo;
            this.accountTypesRepo = accountTypesRepo;
            this.dispositionsRepo = dispositionsRepo;
            this.loansRepo = loansRepo;
            this.transactionsRepo = transactionsRepo;

            this.mapper = mapper;

        }

        public bool CreateAccount(int accounttype, int customerid)//User
        {
            int newacID = accountsRepo.AddAccount(accounttype);
            if(newacID >0)
            {
                var result = dispositionsRepo.CreateDisposition(customerid, newacID, "OWNER");
                return result;
            }
            else
            {
                return false;
            }
  
        }

        public bool CreateLoan(int accountid, decimal amount, int duration, string status)//admin
        {
            //check for duplicate 
            var duplicateCheck = loansRepo.GetLoan(accountid);
            if (duplicateCheck != null) { return false; }
            var theAccount = accountsRepo.GetanAccount(accountid);
            if (theAccount == null) {return false;}

            var increasedBalance = accountsRepo.IncreaseBalance(accountid, amount);
            if(increasedBalance)
            {
                var result = loansRepo.CreateLoan(accountid, amount, duration, status);

                var writingTsucces = transactionsRepo.CreateTransaction(accountid, +amount,
                    theAccount.Balance, "Loan");
                if (writingTsucces == false) { return false; }


                return result;
            }
            else
            {
                return false;
            }

            
        }

        public bool Deposit(int accountid, int customerid, decimal amount)//User
        {
            var verifyAccount = dispositionsRepo.GetCustomerID(accountid);
            if(verifyAccount == customerid)
            {
                var theAccount = accountsRepo.GetanAccount(accountid);
                if (theAccount == null) { return false; }
     
                var result = accountsRepo.IncreaseBalance(theAccount.AccountId, amount);
                if(result)
                {
                    var writingTsucces = transactionsRepo.CreateTransaction(theAccount.AccountId
                        , +amount, theAccount.Balance, "Deposit");
                    return writingTsucces;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
           
        }

        public HashSet<AccountsDTO> GetAccounts(int customerid)//User //MAP
        {
           
            var theDispos = dispositionsRepo.GetMyaccountDispositions(customerid);

            var theaccountIDs = theDispos.Select(dispo => dispo.AccountId).ToList();

            var result = accountsRepo.GetAllAccounts(theaccountIDs);
            var finalresult = mapper.Map<IEnumerable<AccountsDTO>>(result);

            return new HashSet<AccountsDTO>(finalresult);
        }

        public List<AccountTypesDTO> GetAccountTypes()//User //MAP
        {
            var result = accountTypesRepo.GetAccountTypes();
            var finalresult = mapper.Map<IEnumerable<AccountTypesDTO>>(result);
            return new List<AccountTypesDTO>(finalresult);
        }

        public HashSet<TransactionsDTO> GetAllAccountTransactions(int customerid)//User //MAP
        {
            var ouraccounts = dispositionsRepo.GetMyaccountDispositions(customerid);
            List<int> Ids = new List<int>();
            foreach(var account in ouraccounts)
            {
                Ids.Add(account.AccountId);
            }

            var result = transactionsRepo.GetAllAccountTransactions(Ids);
            var finalresult = mapper.Map<IEnumerable<TransactionsDTO>>(result);
            return new HashSet<TransactionsDTO>(finalresult);
        }

        public List<LoansDTO> GetLoans() //admin //MAP
        {
            var result = loansRepo.GetAllLoans();
            var finalresult = mapper.Map<IEnumerable<LoansDTO>>(result);
            return new List<LoansDTO>(finalresult);
        }

        public LoansDTO GetmyLoan(int accountid, int customerid)//user //MAP
        {
            var verifyowner = dispositionsRepo.GetCustomerID(accountid);
            if(verifyowner == customerid)
            {
                var result = loansRepo.GetLoan(accountid);
                var finalresult = mapper.Map<LoansDTO>(result);
                return finalresult;
            }
            else
            {
                return null;
            }
        }

        public AccountsDTO GetOneAccount(int accountid, int customerid)//User //MAP
        {
            var forverify = dispositionsRepo.GetCustomerID(accountid);
            if(forverify == customerid)
            {
                var result = accountsRepo.GetanAccount(accountid);
                var finalresult = mapper.Map<AccountsDTO>(result);
                return finalresult;
            }
            else
            {
                return null;
            }
        }

        public HashSet<TransactionsDTO> GetTransactions(int accountid)//Admin //MAP
        {
            var result = transactionsRepo.GetTransactions(accountid);
            var finalresult = mapper.Map<IEnumerable<TransactionsDTO>>(result);
            return new HashSet<TransactionsDTO>(finalresult);
        }

        public bool SendMoney(int toaccountid, int fromaccount, decimal amount, int customerid)//User
        {
            Console.WriteLine($"Sender Account ID: {fromaccount}, Receiver Account ID: {toaccountid}, Amount: {amount}");
            var verifyAccount = dispositionsRepo.GetCustomerID(fromaccount);
            if( verifyAccount == customerid)
            {
                var sender = accountsRepo.GetanAccount(fromaccount);
                var reciever = accountsRepo.GetanAccount(toaccountid);

                if(sender==null || reciever == null) { Console.WriteLine("Sender or Receiver account not found!"); return false; }

                Console.WriteLine($"Sender balance before: {sender.Balance}, Receiver balance before: {reciever.Balance}");


                var takeResult = accountsRepo.DecreaseBalance(sender.AccountId, amount);
                var giveResult = accountsRepo.IncreaseBalance(reciever.AccountId, amount);
                if(takeResult && giveResult)
                {
                    Console.WriteLine($"Sender222 balance before: {sender.Balance}, Receiver balance before: {reciever.Balance}");

                    var writingTsucces1 = transactionsRepo.CreateTransaction(sender.AccountId,
                        -amount, sender.Balance, "Sending money");

                    Console.WriteLine("Attempting to create transaction for sender.");

                    var writingTsucces2 = transactionsRepo.CreateTransaction(reciever.AccountId, +amount
                        , reciever.Balance, "Recieving money");

                    Console.WriteLine("Attempting to create transaction for sender.");

                    if (writingTsucces1 && writingTsucces2)
                    {
                        Console.WriteLine($"Sender222 balance before: {sender.Balance}, Receiver balance before: {reciever.Balance}");
                        /////
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Sender222 balance before: {sender.Balance}, Receiver balance before: {reciever.Balance}");
                        //fails

                        return false;
                    }

                }
                else
                {
                    Console.WriteLine("Balance operations failed!");
                    return false;
                }
                
            }

            Console.WriteLine("Customer ID mismatch!");
            return false;
          
        }

        public bool UpdateLoan(int accountid, decimal? amount = null, int? duration = null, string? status = null)//Admin
        {
            var result = loansRepo.UpdateLoanStatus(accountid, amount, duration, status);
            return result;
        }

        public bool Withdraw(int accontid, int customerid, decimal amount)//User
        {
            Console.WriteLine($"Withdraw Method Called: AccountId = {accontid}, CustomerId = {customerid}, Amount = {amount}");
            var verifyAccount = dispositionsRepo.GetCustomerID(accontid);
            if(verifyAccount == customerid)
            {
                var theAccount = accountsRepo.GetanAccount(accontid);
                if (theAccount == null) { Console.WriteLine("Account not found."); return false; }
                Console.WriteLine($"Account found: AccountId = {theAccount.AccountId}, Balance = {theAccount.Balance}");


                var result = accountsRepo.DecreaseBalance(theAccount.AccountId, amount);

                if(result)
                {
                    Console.WriteLine($"Balance decreased successfully. New Balance: {theAccount.Balance}");
                    var writeTsucces = transactionsRepo.CreateTransaction(theAccount.AccountId,
                        -amount, theAccount.Balance, "Withdrawl");

                    return writeTsucces;
                }
                else
                {
                    Console.WriteLine("Balance decrease failed.");
                    return false;
                }
               
            }
            else
            {
                Console.WriteLine("Customer ID does not match.");
                return false;
            }
        }
    }
}
