using FluentValidation;

namespace theBankApi.Middleware.Validators
{
    public class PageNumberValidator:AbstractValidator<int?>
    {
        public PageNumberValidator()
        {
            RuleFor(pageNumber => pageNumber)
             .NotNull().WithMessage("Page number is required.")
            .GreaterThan(0).WithMessage("Page number must be 1 or greater.");
        }
    }
}
