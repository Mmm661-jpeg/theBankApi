using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Data.Interfaces
{
    public interface IDispositionsRepo
    {
        bool CreateDisposition(int customerid, int accountid, string type);
        HashSet<Dispositions> GetMyaccountDispositions(int customerid);

        int GetCustomerID(int accountid);

        
        
    }
}
