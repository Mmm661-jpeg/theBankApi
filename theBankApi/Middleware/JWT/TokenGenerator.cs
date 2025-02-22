using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using theBankApi.Core.Interfaces;
using theBankApi.Domain.Models;

namespace theBankApi.Middleware.JWT
{
    public class TokenGenerator:ITokenGenerator
    {
        private readonly IConfiguration configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(Users users)
        {
            var secretKey = configuration["JwtSettings:SecretKey"];
            var issuer = configuration["JwtSettings:Issuer"];
            var audience = configuration["JwtSettings:Audience"];
            var expirationHours = int.Parse(configuration["JwtSettings:TokenExpirationInHours"]);

            List<Claim> claims = new List<Claim>()
            {
                new Claim("Username",users.Username),
                new Claim("UserID",users.UserID.ToString()),
                new Claim("CustomerID",users.CustomerId.ToString())

            };

            if(users.Username == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

           else
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expirationHours),
                signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
