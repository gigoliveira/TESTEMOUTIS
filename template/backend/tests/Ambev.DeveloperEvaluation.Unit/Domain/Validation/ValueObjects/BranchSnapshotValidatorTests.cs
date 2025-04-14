using System;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.ValueObjects
{
    /// <summary>
    /// Unit tests for validating the <see cref="BranchSnapshot"/> value object.
    /// </summary>
    public class BranchSnapshotValidatorTests
    {
        /// <summary>
        /// Test to validate if a valid BranchSnapshot passes validation.
        /// </summary>
        [Fact]
        public void Given_ValidBranchSnapshot_When_Create_Then_ShouldNotThrow()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var branchName = "Valid Branch";

            // Act and Assert
            var snapshot = BranchSnapshot.Create(branchId, branchName);
            snapshot.Validate();
        }
        
        /// <summary>
        /// Test to validate that an empty ExternalBranchId throws a validation error.
        /// </summary>
        [Fact]
        public void Given_EmptyBranchId_When_Create_Then_ShouldThrowValidationError()
        {
            // Arrange
            var branchId = Guid.Empty;
            var branchName = "Valid Branch";

            // Act and Assert
            var ex = Assert.Throws<ValidationException>(() => BranchSnapshot.Create(branchId, branchName));
            Assert.Contains("Branch Id cannot be empty", ex.Message);
        }

        /// <summary>
        /// Test to validate that an empty BranchName throws a validation error.
        /// </summary>
        [Fact]
        public void Given_EmptyBranchName_When_Create_Then_ShouldThrowValidationError()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var branchName = "";

            // Act and Assert
            var ex = Assert.Throws<ValidationException>(() => BranchSnapshot.Create(branchId, branchName));
            Assert.Contains("Branch Name cannot be empty", ex.Message);
        }

        /// <summary>
        /// Test to validate that a whitespace-only BranchName throws a validation error.
        /// </summary>
        [Fact]
        public void Given_WhitespaceBranchName_When_Create_Then_ShouldThrowValidationError()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var branchName = "   ";

            // Act and Assert
            var ex = Assert.Throws<ValidationException>(() => BranchSnapshot.Create(branchId, branchName));
            Assert.Contains("Branch Name cannot be just whitespace", ex.Message);
        }
    }
}
