using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Validation.CustomerSnapshotValidations;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.ValueObjects
{
    public class BranchSnapshotValidatorTests
    {
        // Given a valid branch
        // When validated
        // Then should pass validation
        [Fact]
        public void Given_ValidBranch_When_Validated_Then_ShouldPassValidation()
        {
            // Arrange
            var snapshot = new BranchSnapshot(Guid.NewGuid(), "Valid Branch");

            // Act
            var validator = new BranchSnapshotValidator();

            // Assert
            validator.ValidateAndThrow(snapshot);
        }

        // Given an invalid branch name (empty)
        // When validated
        // Then should throw ValidationException
        [Fact]
        public void Given_InvalidBranchName_When_Validated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var snapshot = new BranchSnapshot(Guid.NewGuid(), string.Empty);

            // Act
            var validator = new BranchSnapshotValidator();

            // Assert
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(snapshot));
        }
    }
}
