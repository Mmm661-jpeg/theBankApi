using Azure;
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

        Customers GetCustomerById(int customerID);

        HashSet<Customers> SearchCustomers(string keyword, int pageNumber, int amount = 100);
    }
}
