using System;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.ValueObjects
{
    /// <summary>
    /// Unit tests for the <see cref="BranchSnapshot"/> value object.
    /// </summary>
    public class BranchSnapshotTests
    {
        /// <summary>
        /// Given external branch id, when snapshot created, then should store id and name.
        /// </summary>
        [Fact]
        public void Given_ExternalBranchId_When_SnapshotCreated_Then_ShouldStoreIdAndName()
        {
            var externalBranchId = Guid.NewGuid();
            var branchName = "Branch ABC";

            var snapshot = BranchSnapshot.Create(externalBranchId, branchName);

            Assert.Equal(externalBranchId, snapshot.ExternalBranchId);
            Assert.Equal(branchName, snapshot.BranchName);
        }

        /// <summary>
        /// Given invalid branch id, when snapshot created, then should throw validation exception.
        /// </summary>
        [Fact]
        public void Given_InvalidBranchId_When_SnapshotCreated_Then_ShouldThrowValidationException()
        {
            var branchName = "Branch ABC";
            var ex = Assert.Throws<ValidationException>(() => BranchSnapshot.Create(Guid.Empty, branchName));
            Assert.Contains("Branch Id cannot be empty", ex.Message);
        }

        /// <summary>
        /// Given invalid branch name, when snapshot created, then should throw validation exception.
        /// </summary>
        [Fact]
        public void Given_InvalidBranchName_When_SnapshotCreated_Then_ShouldThrowValidationException()
        {
            var externalBranchId = Guid.NewGuid();
            var ex = Assert.Throws<ValidationException>(() => BranchSnapshot.Create(externalBranchId, ""));
            Assert.Contains("Branch Name cannot be empty", ex.Message);
        }
    }
}