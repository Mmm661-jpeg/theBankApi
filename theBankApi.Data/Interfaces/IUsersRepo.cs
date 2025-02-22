using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Data.Interfaces
{
    public interface IUsersRepo
    {
        bool Register(Users users,Customers customers);

        Users Login(string username,string password);

    }
}
