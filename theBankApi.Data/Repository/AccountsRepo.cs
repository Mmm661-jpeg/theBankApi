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
    public class AccountsRepo:IAccountsRepo
    {
        private readonly theBankApiDBcontext db;
        public AccountsRepo(theBankApiDBcontext db)
        {
            this.db = db;
        }

        public int AddAccount(int accounttypes)
        {
            var account = new Accounts()
            {
                Frequency = "Monthly",
                Created = DateOnly.FromDateTime(DateTime.Today),
                Balance = 0,
                AccountTypesId = accounttypes
            };

            using(var transactions = db.Database.BeginTransaction())
            {
                try
                {
                    db.Accounts.Add(account);
                    db.SaveChanges();
                    transactions.Commit();
                    return account.AccountId;    

                }
                catch (Exception ex)
                {
                    transactions.Rollback();
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
        }

        public bool DecreaseBalance(int accountid, decimal amount)
        {
            var account = db.Accounts.FirstOrDefault(c=>c.AccountId == accountid);
            if(account == null)
            {
                Console.WriteLine($"Account with AccountId {accountid} not found.");
                return false;
            }

            Console.WriteLine($"Account found: AccountId = {account.AccountId}, Current Balance = {account.Balance}");


            if (account.Balance < amount)
            {
                Console.WriteLine($"Insufficient funds. Available Balance = {account.Balance}, Withdrawal Amount = {amount}");
                return false;
            }


            using (var transactions = db.Database.BeginTransaction())
            {
                try
                {
                    account.Balance -= amount;
                    db.SaveChanges();
                    transactions.Commit();
                    Console.WriteLine($"Balance updated successfully. New Balance = {account.Balance}");
                    return true;

                }
                catch (Exception ex)
                {
                    transactions.Rollback();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine($"Error while updating balance: {ex.Message}");
                    return false;
                }
            }
                
            
            
        }

        public Accounts GetanAccount(int accountid)
        {
            var account = db.Accounts.FirstOrDefault(c => c.AccountId == accountid);
            return account;
        }



        public HashSet<Accounts> GetAllAccounts(List<int> accountids)

        {
            
            return db.Accounts.Where(a => accountids.Contains(a.AccountId)).ToHashSet(); 

        }

        public bool IncreaseBalance(int accountid, decimal amount)
        {
            var account = db.Accounts.FirstOrDefault(c => c.AccountId == accountid);
            if (account == null)
            {
                return false;
            }
            else
            {
                using (var transactions = db.Database.BeginTransaction())
                {
                    try
                    {
                        account.Balance += amount;
                        db.SaveChanges();
                        transactions.Commit();
                        return true;

                    }
                    catch (Exception ex)
                    {
                        transactions.Rollback();
                        Console.WriteLine(ex.Message);
                        return false;
                    }
                }
            }
        }
    }
}
