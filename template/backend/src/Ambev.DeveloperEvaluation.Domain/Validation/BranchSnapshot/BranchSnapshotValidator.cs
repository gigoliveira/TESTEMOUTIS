using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Validation.BranchSnapshotValidations
{
    /// <summary>
    /// Validator for <see cref="BranchSnapshot"/>.
    /// </summary>
    public class BranchSnapshotValidator : AbstractValidator<BranchSnapshot>
    {
        public BranchSnapshotValidator()
        {
            // Ensure that the external branch ID is not empty.
            RuleFor(b => b.ExternalBranchId)
                .NotEqual(Guid.Empty).WithMessage("Branch Id cannot be empty");

            // Ensure that the branch name is not empty or whitespace.
            RuleFor(b => b.BranchName)
                .NotEmpty().WithMessage("Branch Name cannot be empty")
                .NotNull().WithMessage("Branch Name cannot be null")
                .Matches(@"^\S.*\S$").WithMessage("Branch Name cannot be just whitespace");
        }
    }
}
