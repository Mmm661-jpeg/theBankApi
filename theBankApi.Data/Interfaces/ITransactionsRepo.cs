using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Data.Interfaces
{
    public interface ITransactionsRepo
    {
        HashSet<Transactions> GetTransactions(int accountid);

        HashSet<Transactions> GetAllAccountTransactions(List<int> accountids);

        //add transaction(balans tas från accounts i service)
        bool CreateTransaction(int accountid, decimal amount, decimal balance,string operation);
        //Date,Type,Operation sätts inne resten null
    }
}
