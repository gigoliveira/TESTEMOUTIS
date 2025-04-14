using System;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.ValueObjects
{
    public class ProductSnapshotValidatorTests
    {
        [Fact]
        public void Given_ValidProductSnapshot_When_Validated_Then_ShouldBeValid()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productName = "Valid Product";
            var price = 10m;

            var productSnapshot = ProductSnapshot.Create(productId, productName, price);

            // Act
            var validationResult = productSnapshot.Validate();

            // Assert
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Fact]
        public void Given_EmptyProductId_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var productSnapshot = ProductSnapshot.Create(Guid.Empty, "Valid Product", 10m);

            // Act
            var validationResult = productSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.Detail == "Product Id cannot be empty");
        }

        [Fact]
        public void Given_EmptyProductName_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var productSnapshot = ProductSnapshot.Create(Guid.NewGuid(), "", 10m);

            // Act
            var validationResult = productSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.Detail == "Product Name cannot be empty");
        }

        [Fact]
        public void Given_WhitespaceProductName_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var productSnapshot = ProductSnapshot.Create(Guid.NewGuid(), "   ", 10m);

            // Act
            var validationResult = productSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.Detail == "Product Name cannot be empty");
        }

        [Fact]
        public void Given_ZeroPrice_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var productSnapshot = ProductSnapshot.Create(Guid.NewGuid(), "Valid Product", 0m);

            // Act
            var validationResult = productSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.Detail == "Product Price must be greater than zero");
        }

        [Fact]
        public void Given_NegativePrice_When_Validated_Then_ShouldHaveValidationError()
        {
            // Arrange
            var productSnapshot = ProductSnapshot.Create(Guid.NewGuid(), "Valid Product", -10m);

            // Act
            var validationResult = productSnapshot.Validate();

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.Contains(validationResult.Errors, e => e.Detail == "Product Price must be greater than zero");
        }
    }
}
