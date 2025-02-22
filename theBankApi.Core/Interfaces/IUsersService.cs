using theBankApi.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theBankApi.Core.Interfaces
{
    public interface IUsersService
    {
        bool Register(RegisterRequest request);

        string Login(LoginRequest loginRequest); //FromBody LoginRequestDTO
    }
}
