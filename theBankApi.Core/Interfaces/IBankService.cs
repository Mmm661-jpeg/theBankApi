using theBankApi.Domain.DTOs;
using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Core.Interfaces
{
    public interface IBankService
    {
        bool CreateAccount(int accounttype,int customerid);
        AccountsDTO GetOneAccount(int accountid,int customerid);
        HashSet<AccountsDTO> GetAccounts(int customerid); 

        bool Deposit(int accountid,int customerid,decimal amount);
        bool Withdraw(int accontid,int customerid,decimal amount);
        bool SendMoney(int toaccountid,int fromaccount,decimal amount,int customerid);

        bool CreateLoan(int accountid, decimal amount, int duration, string status);
        List<LoansDTO> GetLoans();
        LoansDTO GetmyLoan(int accountid,int customerid);
        bool UpdateLoan(int accountid, decimal? amount = null, int? duration = null, string? status = null);

        HashSet<TransactionsDTO> GetTransactions(int accountid);
        HashSet<TransactionsDTO> GetAllAccountTransactions(int customerid);

        List<AccountTypesDTO> GetAccountTypes();


       


    }
}
