using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.CustomerSnapshotValidations
{
    public class BranchSnapshotValidator
    {
        public void ValidateAndThrow(BranchSnapshot snapshot)
        {
            if (string.IsNullOrWhiteSpace(snapshot.Name))
            {
                throw new ValidationException("Branch name cannot be empty.");
            }
        }
    }
}
