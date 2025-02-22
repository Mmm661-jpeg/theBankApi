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
    public class DispositionsRepo:IDispositionsRepo
    {
        private readonly theBankApiDBcontext db;

        public DispositionsRepo(theBankApiDBcontext db)
        {
            this.db = db;
        }

        public bool CreateDisposition(int customerid, int accountid, string type) //för skapa account
        {
            var disposition = new Dispositions()
            {
                CustomerId = customerid,
                AccountId = accountid,
                Type = type
            };

            using(var transactions = db.Database.BeginTransaction())
            {
                try
                {
                    db.Dispositions.Add(disposition);
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

        public int GetCustomerID(int accountid) //kan användas för verifiering
        {
            var result = db.Dispositions.FirstOrDefault(c=>c.AccountId == accountid);
            if(result != null)
            {
                return result.CustomerId;
            }
            else
            {
                return -1;
            }
        }

        public HashSet<Dispositions> GetMyaccountDispositions(int customerid) 
        {

            var result = db.Dispositions.Where(c => c.CustomerId == customerid).ToHashSet();
            return result ?? new HashSet<Dispositions>();


        }

    }
}
