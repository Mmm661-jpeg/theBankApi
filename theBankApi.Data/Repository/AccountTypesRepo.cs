using Inlämningsuppgift3.Data.DataModels;
using Inlämningsuppgift3.Data.Interfaces;
using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Data.Repository
{
    public class AccountTypesRepo:IAccountTypesRepo
    {
        private readonly Inlämningsuppgift3DBcontext db;

        public AccountTypesRepo(Inlämningsuppgift3DBcontext db)
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
