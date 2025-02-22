using FluentValidation;
using theBankApi.Domain.Requests;

namespace theBankApi.Middleware.Validators
{
    public class LoginRequestValidator:AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
                
        }
    }
}
