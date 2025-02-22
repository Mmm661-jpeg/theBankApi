using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theBankApi.Domain.Models;

namespace theBankApi.Data.Interfaces
{
    public interface ICustomersRepo
    {
        HashSet<Customers> GetCustomers(int pageNumber, int amount = 100);
    }
}
