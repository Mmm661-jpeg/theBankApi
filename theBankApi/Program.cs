using FluentValidation.AspNetCore;
using FluentValidation;
using Inlämningsuppgift3.Core.Interfaces;
using Inlämningsuppgift3.Core.Services;
using Inlämningsuppgift3.Data.DataModels;
using Inlämningsuppgift3.Data.Interfaces;
using Inlämningsuppgift3.Data.Repository;
using Inlämningsuppgift3.Middleware.Extensions;
using Inlämningsuppgift3.Middleware.Validators;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthenticationExtentsion(
    issuer: "http://localhost:5226"
    ,audience: "http://localhost:5226"
    ,signingKey: "ThisIsASuperSecureKeyWithAtLeast32CharactersOrMore");

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

builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IBankService, BankService>();


builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
builder.Services.AddFluentValidationAutoValidation();


builder.Services.AddDbContext<Inlämningsuppgift3DBcontext>(options =>
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
