using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.CustomerSnapshotValidations
{
    /// <summary>
    /// Validator for CustomerSnapshot. Defines validation rules for the CustomerSnapshot value object.
    /// </summary>
    public class CustomerSnapshotValidator : AbstractValidator<CustomerSnapshot>
    {
        public CustomerSnapshotValidator()
        {
            // Rule to ensure ExternalCustomerId is not empty.
            RuleFor(x => x.ExternalCustomerId)
                .NotEmpty().WithMessage("Customer Id cannot be empty");

            // Rule to ensure CustomerName is not empty or just whitespace.
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Customer Name cannot be empty")
                .Matches(@"^\S.*$").WithMessage("Customer Name cannot be empty");
        }
    }
}
