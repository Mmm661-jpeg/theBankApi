using FluentValidation;
using theBankApi.Domain.Requests;

namespace Inlämningsuppgift3.Middleware.Validators
{
    public class SendMoneyRequestValidator:AbstractValidator<SendMoneyRequest>
    {
        public SendMoneyRequestValidator()
        {
            RuleFor(x => x.Toaccountid).NotEmpty().WithMessage("Please enter the recievers id");

            RuleFor(x => x.Fromaccountid).NotEmpty().WithMessage("Please enter the senders id");

            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount required")
                .GreaterThan(0).WithMessage("Amount must be more than 0");
            
        }
    }
}
