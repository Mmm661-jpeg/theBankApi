using FluentValidation;
using theBankApi.Domain.Requests;

namespace theBankApi.Middleware.Validators
{
    public class WithdrawOrDepositRequestValidator:AbstractValidator<WithdrawOrDepositRequest>
    {
        public WithdrawOrDepositRequestValidator()
        {
            RuleFor(x => x.AccountID).NotEmpty()
                .WithMessage("AccountID required");

            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount required!")
                .GreaterThan(0).WithMessage("Amount must be greater than 0");
        }
    }
}
