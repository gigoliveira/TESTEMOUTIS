using System;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
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
        public void Given_ValidBranchSnapshot_When_Validated_Then_ShouldBeValid()
        {
            // Arrange
            var branchId = Guid.NewGuid();
            var branchName = "Valid Branch";

            var branchSnapshot = BranchSnapshot.Create(branchId, branchName);

            // Act
            var validationResult = branchSnapshot.Validate();

            // Assert
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        /// <summary>
        /// Test to validate that an empty ExternalBranchId throws a validation error.
        /// </summary>
        [Fact]
        public void Given_EmptyBranchId_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var branchSnapshot = BranchSnapshot.Create(Guid.Empty, "Valid Branch");

            // Act
            var validationResult = branchSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.Detail == "Branch Id cannot be empty");
        }

        /// <summary>
        /// Test to validate that an empty BranchName throws a validation error.
        /// </summary>
        [Fact]
        public void Given_EmptyBranchName_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var branchSnapshot = BranchSnapshot.Create(Guid.NewGuid(), "");

            // Act
            var validationResult = branchSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.Detail == "Branch Name cannot be empty");
        }

        /// <summary>
        /// Test to validate that a whitespace-only BranchName throws a validation error.
        /// </summary>
        [Fact]
        public void Given_WhitespaceBranchName_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var branchSnapshot = BranchSnapshot.Create(Guid.NewGuid(), "   ");

            // Act
            var validationResult = branchSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.Detail == "Branch Name cannot be just whitespace");
        }
    }
}
