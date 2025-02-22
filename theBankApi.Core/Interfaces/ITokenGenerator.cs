using theBankApi.Domain.Models;

namespace theBankApi.Core.Interfaces
{
    public interface ITokenGenerator
    {
        public string GenerateToken(Users users);
    }
}
