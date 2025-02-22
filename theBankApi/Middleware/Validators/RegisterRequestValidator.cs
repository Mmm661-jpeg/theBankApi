using FluentValidation;
using theBankApi.Domain.Requests;

namespace Inlämningsuppgift3.Middleware.Validators
{
    public class RegisterRequestValidator:AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Username)
         .NotEmpty().WithMessage("Username is required.")
         .MinimumLength(2).WithMessage("Username must be at least 2 characters long.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(3).WithMessage("Password must be at least 3 characters long.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.");
  

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required");
         ;

            RuleFor(x => x.Givenname)
                .NotEmpty().WithMessage("Givenname is required");
          

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required");
         

            RuleFor(x => x.Streetaddress)
                .NotEmpty().WithMessage("Streetaddress is required");
            

            RuleFor(x => x.City).NotEmpty()
                .WithMessage("City is required");
         

            RuleFor(x => x.Zipcode)
                .NotEmpty()
                .WithMessage("Zipcode is required");
              

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Country is required");
          

            RuleFor(x => x.CountryCode)
                .NotEmpty()
                .WithMessage("CountryCode is required");

            RuleFor(x => x.Emailaddress) 
                .EmailAddress().WithMessage("Please enter valid Email");
                
        }
    }
}
