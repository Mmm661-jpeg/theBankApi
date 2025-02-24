using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theBankApi.Data.DataModels;
using theBankApi.Data.Interfaces;
using theBankApi.Domain.Models;

namespace theBankApi.Data.Repository
{
    public class CustomersRepo:ICustomersRepo
    {
        private readonly theBankApiDBcontext db;
        private readonly ILogger<CustomersRepo> logger;

        public CustomersRepo(theBankApiDBcontext db, ILogger<CustomersRepo> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public HashSet<Customers> GetCustomers(int pageNumber,int amount = 100)
        {
            try
            {
                return db.Customers.OrderBy(c => c.CustomerId)
                    .Skip((pageNumber - 1) * amount)
                    .Take(amount)
                    .ToHashSet();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new HashSet<Customers>();
            }
        }

        public Customers GetCustomerById(int customerid)
        {
           try
            {
                var result = db.Customers.FirstOrDefault(dbCustomer => dbCustomer.CustomerId.Equals(customerid));
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public HashSet<Customers> SearchCustomers(string keyword, int pageNumber, int amount = 100) //Filter to search by other collums?
        {
            var searchResult = db.Customers.Where(dbCustomer => EF.Functions.Like(dbCustomer.Givenname, keyword));
            var finalResult = searchResult.Skip((pageNumber - 1) * amount).Take(amount);

            return finalResult.ToHashSet();
            
        }
    }
}
