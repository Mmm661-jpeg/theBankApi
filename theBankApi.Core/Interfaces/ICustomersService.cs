using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theBankApi.Domain.DTOs;
using theBankApi.Domain.Models;

namespace theBankApi.Core.Interfaces
{
    public interface ICustomersService
    {
        HashSet<CustomersDTO> GetCustomers(int pageNumber);

        CustomersDTO GetCustomerById(int customerid);

        HashSet<CustomersDTO> SearchCustomers(string keyword, int pageNumber);

    }
}
