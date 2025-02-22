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
    }
}
