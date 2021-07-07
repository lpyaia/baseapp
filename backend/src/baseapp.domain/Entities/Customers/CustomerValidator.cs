using FluentValidation;

namespace BaseApp.Domain.Entities.Customers
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.FirstName)
                .NotEmpty()
                .MaximumLength(25)
                .WithMessage("First name is invalid.");

            RuleFor(c => c.LastName)
                .NotEmpty()
                .MaximumLength(25)
                .WithMessage("Last name is invalid.");
        }
    }
}