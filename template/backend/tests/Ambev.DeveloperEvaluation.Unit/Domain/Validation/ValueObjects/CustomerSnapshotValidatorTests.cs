using System;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.ValueObjects
{
    /// <summary>
    /// Unit tests for validating the CustomerSnapshot value object.
    /// </summary>
    public class CustomerSnapshotValidatorTests
    {
        /// <summary>
        /// Given a valid CustomerSnapshot, it should pass validation without errors.
        /// </summary>
        [Fact]
        public void Given_ValidCustomerSnapshot_When_Validated_Then_ShouldNotThrow()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerName = "Valid Customer";

            // Act & Assert
            var ex = Record.Exception(() => CustomerSnapshot.Create(customerId, customerName));
            Assert.Null(ex);
        }

        /// <summary>
        /// Given an empty CustomerId, it should result in a validation exception.
        /// </summary>
        [Fact]
        public void Given_EmptyCustomerId_When_Validated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var customerId = Guid.Empty;
            var customerName = "Valid Customer";

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => CustomerSnapshot.Create(customerId, customerName));
            Assert.Contains("Customer Id cannot be empty", ex.Message);
        }

        /// <summary>
        /// Given an empty CustomerName, it should result in a validation exception.
        /// </summary>
        [Fact]
        public void Given_EmptyCustomerName_When_Validated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerName = string.Empty;

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => CustomerSnapshot.Create(customerId, customerName));
            Assert.Contains("Customer Name cannot be empty", ex.Message);
        }

        /// <summary>
        /// Given a CustomerName with only whitespace, it should result in a validation exception.
        /// </summary>
        [Fact]
        public void Given_WhitespaceCustomerName_When_Validated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerName = "   ";

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => CustomerSnapshot.Create(customerId, customerName));
            Assert.Contains("Customer Name cannot be empty", ex.Message);
        }
    }
}
