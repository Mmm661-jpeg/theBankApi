using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace theBankApi.Middleware.Extensions
{
    public static class JWTExtenstion
    {
        public static IServiceCollection AddAuthenticationExtentsion(this IServiceCollection services, string issuer, string audience, string signingKey)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = issuer,
                   ValidAudience = audience,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                   RoleClaimType = ClaimTypes.Role
               };
           });

            return services;
        }
   
    }
}
