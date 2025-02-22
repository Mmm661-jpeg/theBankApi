using FluentValidation.AspNetCore;
using FluentValidation;
using theBankApi.Core.Interfaces;
using theBankApi.Core.Services;
using theBankApi.Data.DataModels;
using theBankApi.Data.Interfaces;
using theBankApi.Data.Repository;


using theBankApi.Middleware.Validators;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

using theBankApi.Middleware.Extensions;
using theBankApi.Middleware.JWT;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("Jwtsettings");

builder.Services.AddAuthenticationExtentsion(
    issuer: jwtSettings["Issuer"]
    ,audience: jwtSettings["Audience"]
    ,signingKey: jwtSettings["SecretKey"]);

builder.Services.AddAuthorization();

builder.Services.AddControllers();//test

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

/*builder.Services.AddControllers().AddJsonOptions(options => //Ersätt genom att fixa DTO
                                                            //Automapper
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});*/

builder.Services.AddTransient<IUsersRepo,UsersRepo>();

builder.Services.AddTransient<ITransactionsRepo, TransactionsRepo>();
builder.Services.AddTransient<ILoansRepo,LoansRepo>();
builder.Services.AddTransient<IDispositionsRepo,DispositionsRepo>();
builder.Services.AddTransient<IAccountTypesRepo,AccountTypesRepo>();
builder.Services.AddTransient<IAccountsRepo,AccountsRepo>();

builder.Services.AddScoped<ICustomersRepo,CustomersRepo>();
builder.Services.AddScoped<ICustomersService,CustomersService>();

builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IBankService, BankService>();

builder.Services.AddScoped<ITokenGenerator,TokenGenerator>();


builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddDbContext<theBankApiDBcontext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});


builder.Services.AddSwaggerExtended();






var app = builder.Build();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerExtended();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });



app.Run();
