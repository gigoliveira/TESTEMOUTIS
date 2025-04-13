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
    public class CustomerSnapshotValidatorTests
    {
        // Given a valid customer
        // When validated
        // Then should pass validation
        [Fact]
        public void Given_ValidCustomer_When_Validated_Then_ShouldPassValidation()
        {
            // Arrange
            var snapshot = new CustomerSnapshot(Guid.NewGuid(), "Valid Customer");

            // Act
            var validator = new CustomerSnapshotValidator();

            // Assert
            validator.ValidateAndThrow(snapshot);
        }

        // Given an invalid customer name (empty)
        // When validated
        // Then should throw ValidationException
        [Fact]
        public void Given_InvalidCustomerName_When_Validated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var snapshot = new CustomerSnapshot(Guid.NewGuid(), string.Empty);

            // Act
            var validator = new CustomerSnapshotValidator();

            // Assert
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(snapshot));
        }
    }
}
