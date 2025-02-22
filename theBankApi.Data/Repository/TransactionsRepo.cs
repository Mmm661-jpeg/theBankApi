using theBankApi.Data.DataModels;
using theBankApi.Data.Interfaces;
using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Data.Repository
{
    public class TransactionsRepo:ITransactionsRepo
    {
        private readonly theBankApiDBcontext db;
        public TransactionsRepo(theBankApiDBcontext db)
        {
            this.db = db;
        }

        public bool CreateTransaction(int accountid, decimal amount, decimal balance, string operation)
        {
            Console.WriteLine("Entering CreateTransaction method.");
            Console.WriteLine($"Parameters - AccountId: {accountid}, Amount: {amount}, Balance: {balance}, Operation: {operation}");

            var transaction = new Transactions()
            {
                AccountId = accountid,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Amount = amount,
                Balance = balance,
                Type = "Credit",
                Operation = operation
            };

            Console.WriteLine("Transaction object created:");
            Console.WriteLine($"AccountId: {transaction.AccountId}, Date: {transaction.Date}, Amount: {transaction.Amount}, Balance: {transaction.Balance}, Type: {transaction.Type}, Operation: {transaction.Operation}");

            using (var t = db.Database.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("Adding transaction to the database.");
                    db.Transactions.Add(transaction);
                    Console.WriteLine("Saving changes to the database.");
                    db.SaveChanges();
                    Console.WriteLine("Committing transaction.");
                    t.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception occurred:");
                    t.Rollback();
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }

        public HashSet<Transactions> GetAllAccountTransactions(List<int> accountids)
        {
            var transactions = new HashSet<Transactions>();

            var result = db.Transactions.Where(c=>accountids.Contains(c.AccountId)).ToList();
            foreach (var transaction in result)
            {
                transactions.Add(transaction);
            }
            return transactions;
        }

        public HashSet<Transactions> GetTransactions(int accountid)
        {
            var transactions = new HashSet<Transactions>();

            var result = db.Transactions.Where(c=>c.AccountId == accountid ).ToList();
            if(result == null)
            {
                return null;
            }
            else
            {
                foreach (var transaction in result)
                {
                    transactions.Add(transaction);
                }
                return transactions;
            }
            
        }
    }
}
