using theBankApi.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Core.Interfaces
{
    public interface IUsersService
    {
        bool Register(RegisterRequest request);

        string Login(string username, string password);
    }
}
