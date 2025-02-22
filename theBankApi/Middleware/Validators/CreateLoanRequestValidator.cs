using FluentValidation;
using theBankApi.Domain.Requests;

namespace theBankApi.Middleware.Validators
{
    public class CreateLoanRequestValidator:AbstractValidator<CreateLoanRequest>
    {
        public CreateLoanRequestValidator()
        {
            RuleFor(x => x.Accountid).NotEmpty()
                .WithMessage("AccountID required!");

            RuleFor(x => x.Amount).NotEmpty()
                .GreaterThan(0).WithMessage("Amount must be greater than 0");


            RuleFor(x => x.Duration).NotEmpty().WithMessage("Duration required!")
                .GreaterThan(0).WithMessage("Duration must be greater than 0");

            RuleFor(x => x.Status).NotEmpty().WithMessage("Status required!");
        }
    }
}
