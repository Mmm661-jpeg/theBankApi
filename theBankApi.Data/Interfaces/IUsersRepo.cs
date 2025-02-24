using theBankApi.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;

namespace theBankApi.Data.Interfaces
{
    public interface IUsersRepo
    {
        bool Register(Users users,Customers customers);

        Users Login(string username);

        HashSet<Users> GetUsers(); //For the hash script

        void UpdatePasses(HashSet<Users> users);

       

    }
}
