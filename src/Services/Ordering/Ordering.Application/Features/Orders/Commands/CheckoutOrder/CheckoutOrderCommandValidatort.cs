using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandValidatort : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidatort()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters");

            RuleFor(p => p.EmailAddress)
                .EmailAddress().WithMessage("{Email} is not valid")
                .NotEmpty().WithMessage("{EmailAddress} is required");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required")
                .GreaterThan(0).WithMessage("{TotalPrice} must be greater than 0");
        }
    }
}
