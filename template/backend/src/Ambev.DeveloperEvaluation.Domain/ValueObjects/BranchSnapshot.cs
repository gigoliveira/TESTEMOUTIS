using System;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Validation.BranchSnapshotValidations;
using Ambev.DeveloperEvaluation.Domain.Validation.CustomerSnapshotValidations;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    /// <summary>
    /// Represents a snapshot of a branch.
    /// </summary>
    public class BranchSnapshot
    {
        /// <summary>
        /// Gets the external branch identifier.
        /// </summary>
        public Guid ExternalBranchId { get; private set; }

        /// <summary>
        /// Gets the name of the branch.
        /// </summary>
        public string BranchName { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BranchSnapshot"/> class.
        /// </summary>
        /// <param name="externalBranchId">The external branch identifier.</param>
        /// <param name="branchName">The name of the branch.</param>
        public BranchSnapshot(Guid externalBranchId, string branchName)
        {
            ExternalBranchId = externalBranchId;
            BranchName = branchName;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BranchSnapshot"/> and validates it.
        /// </summary>
        /// <param name="externalBranchId">The external branch identifier.</param>
        /// <param name="branchName">The name of the branch.</param>
        /// <returns>A new <see cref="BranchSnapshot"/> instance.</returns>
        public static BranchSnapshot Create(Guid externalBranchId, string branchName)
        {
            var branchSnapshot = new BranchSnapshot(externalBranchId, branchName);
            branchSnapshot.Validate();

            return branchSnapshot;
        }

        /// <summary>
        /// Validates the current instance of <see cref="BranchSnapshot"/>.
        /// </summary>
        /// <returns>A validation result indicating whether the object is valid or not.</returns>
        public ValidationResultDetail Validate()
        {
            var validator = new BranchSnapshotValidator();

            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(e => new ValidationErrorDetail
                {
                    Detail = e.ErrorMessage
                })
            };
        }

    }
}
