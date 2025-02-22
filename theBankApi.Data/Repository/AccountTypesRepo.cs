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
    public class AccountTypesRepo:IAccountTypesRepo
    {
        private readonly theBankApiDBcontext db;

        public AccountTypesRepo(theBankApiDBcontext db)
        {
            this.db = db;
        }

        public List<AccountTypes> GetAccountTypes()
        {
            var result = db.AccountTypes.ToList();
            return result;
        }
    }
}
