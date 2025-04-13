using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Validation.ProductSnapshotValidations
{
    public class ProductSnapshotValidator : AbstractValidator<ProductSnapshot>
    {
        public ProductSnapshotValidator()
        {
            RuleFor(x => x.ExternalProductId)
                .NotEmpty().WithMessage("Product Id cannot be empty");

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product Name cannot be empty");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product Price must be greater than zero");
        }
    }
}
