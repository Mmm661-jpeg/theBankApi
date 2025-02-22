using Microsoft.Extensions.Logging;
using theBankApi.Core.Interfaces;
using theBankApi.Data.Interfaces;
using theBankApi.Domain.Models;
using theBankApi.Domain.Requests;


namespace theBankApi.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepo repo;
        private readonly ITokenGenerator tokenGenerator;
        private readonly ILogger<UsersService> logger;

        public UsersService(IUsersRepo repo, ITokenGenerator tokenGenerator, ILogger<UsersService> logger)
        {
            this.repo = repo;
            this.tokenGenerator = tokenGenerator;
            this.logger = logger;
        }

        public string Login(LoginRequest loginRequest)
        {
            HashTheExistingPasswords(); // Running the script before going and login in

            try
            {
                var user = repo.Login(loginRequest.Username);
                if (user != null)
                {
                    bool corretPass = verifyPass(loginRequest.Password, user.Password);
                    if (corretPass)
                    {
                        var token = tokenGenerator.GenerateToken(user);
                        return token;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    logger.LogDebug("User was null when attempting to login");
                    return null;
                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public bool Register(RegisterRequest request)
        {
            try
            {
                string hashedPassword = HashPass(request.Password);

                var user = new Users()
                {
                    Username = request.Username,
                    Password = hashedPassword
                };

                var customer = new Customers()
                {
                    Gender = request.Gender,
                    Givenname = request.Givenname,
                    Surname = request.Surname,
                    Streetaddress = request.Streetaddress,
                    City = request.City,
                    Zipcode = request.Zipcode,
                    Country = request.Country,
                    CountryCode = request.CountryCode,
                    Birthday = request.Birthday,
                    Telephonecountrycode = request.Telephonecountrycode,
                    Telephonenumber = request.Telephonenumber,
                    Emailaddress = request.Emailaddress

                };

                var result = repo.Register(user, customer);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return false;
            }


        }



        private bool verifyPass(string password, string dbPass)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, dbPass);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return false;
            }
        }

        private string HashPass(string password)
        {
            try
            {
                var result = BCrypt.Net.BCrypt.HashPassword(password);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return null;
            }
        }

        private void HashTheExistingPasswords() //script only runs once
        {
            var theExistingUsers = repo.GetUsers();
            var usersToUpdate = theExistingUsers.Where(user => !user.Password.StartsWith("$")).ToHashSet();

            if (usersToUpdate.Count > 0)
            {
                foreach (var user in usersToUpdate)
                {
                    user.Password = HashPass(user.Password);
                }

                repo.UpdatePasses(theExistingUsers);

            }
            else
            {
                logger.LogDebug("No Passwords needed to be hashed!");
            }


        }
    }
}
