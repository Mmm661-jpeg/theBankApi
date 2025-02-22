using Inlämningsuppgift3.Core.Interfaces;
using Inlämningsuppgift3.Data.Interfaces;
using theBankApi.Domain.Models;
using theBankApi.Domain.Requests;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Inlämningsuppgift3.Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepo repo;

        public UsersService(IUsersRepo repo)
        {
            this.repo = repo;
        }

        public string Login(string username, string password)
        {
            var user = repo.Login(username, password);
            if (user != null)
            {
                var token = GenerateToken(user);
                return token;
            }
            else
            {
                return null;
            }
        }

        public bool Register(RegisterRequest request)
        {
            var user = new Users()
            {
                Username = request.Username,
                Password = request.Password,
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

        private string GenerateToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASuperSecureKeyWithAtLeast32CharactersOrMore"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("Username",user.Username),
                new Claim("UserID",user.UserID.ToString()),
                new Claim("CustomerID",user.CustomerId.ToString())
            };

            if (user.Username == "Mike")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }


            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
            

                var token = new JwtSecurityToken(
                   issuer: "http://localhost:5226",
                   audience: "http://localhost:5226",
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(15),
                   signingCredentials: credentials);
            

                return new JwtSecurityTokenHandler().WriteToken(token);


        }   
    }
}
