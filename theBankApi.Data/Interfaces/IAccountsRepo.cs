using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Data.Interfaces
{
    public interface IAccountsRepo
    {
        Accounts GetanAccount(int accountid);

        HashSet<Accounts> GetAllAccounts(List<int> accountids);

        int AddAccount(int accounttypes); //freq,created,balans: deafulvärden sätts
                                                             //inne i funk (decimal ej int)
        bool DecreaseBalance(int accountid,decimal amount);
        bool IncreaseBalance(int accountid,decimal amount);

    }
}
