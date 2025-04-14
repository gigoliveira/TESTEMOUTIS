using Ambev.DeveloperEvaluation.Domain.ValueObjects;
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
        public void Given_ValidCustomerSnapshot_When_Validated_Then_ShouldBeValid()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var customerName = "Valid Customer";

            // Creating a valid CustomerSnapshot.
            var customerSnapshot = CustomerSnapshot.Create(customerId, customerName);

            // Act
            var validationResult = customerSnapshot.Validate();

            // Assert
            Assert.True(validationResult.IsValid); // Should be valid
            Assert.Empty(validationResult.Errors); // Should not have any validation errors
        }

        /// <summary>
        /// Given an empty CustomerId, it should result in a validation error.
        /// </summary>
        [Fact]
        public void Given_EmptyCustomerId_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var customerSnapshot = CustomerSnapshot.Create(Guid.Empty, "Valid Customer");

            // Act
            var validationResult = customerSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid); // Should be invalid
            Assert.Contains(validationResult.Errors, e => e.Detail == "Customer Id cannot be empty");
        }

        /// <summary>
        /// Given an empty CustomerName, it should result in a validation error.
        /// </summary>
        [Fact]
        public void Given_EmptyCustomerName_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var customerSnapshot = CustomerSnapshot.Create(Guid.NewGuid(), "");

            // Act
            var validationResult = customerSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid); // Should be invalid
            Assert.Contains(validationResult.Errors, e => e.Detail == "Customer Name cannot be empty");
        }

        /// <summary>
        /// Given a CustomerName with only whitespace, it should result in a validation error.
        /// </summary>
        [Fact]
        public void Given_WhitespaceCustomerName_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var customerSnapshot = CustomerSnapshot.Create(Guid.NewGuid(), "   ");

            // Act
            var validationResult = customerSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid); // Should be invalid
            Assert.Contains(validationResult.Errors, e => e.Detail == "Customer Name cannot be empty");
        }
    }
}
