using System;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.ValueObjects
{
    public class ProductSnapshotValidatorTests
    {
        [Fact]
        public void Given_ValidProductSnapshot_When_Validated_Then_ShouldNotThrow()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productName = "Valid Product";
            var price = 10m;

            var productSnapshot = ProductSnapshot.Create(productId, productName, price);

            // Act and Assert
            productSnapshot.Validate();
        }

        [Fact]
        public void Given_EmptyProductId_When_Validated_Then_ShouldThrowValidationError()
        {
            // Arrange
            var productId = Guid.Empty;
            var productName = "Valid Product";
            var price = 10m;

            // Act and Assert
            var ex = Assert.Throws<ValidationException>(() => ProductSnapshot.Create(productId, productName, price));
            Assert.Contains("Product Id cannot be empty", ex.Message);
        }

        [Fact]
        public void Given_EmptyProductName_When_Validated_Then_ShouldThrowValidationError()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productName = "";
            var price = 10m;

            // Act and Assert
            var ex = Assert.Throws<ValidationException>(() => ProductSnapshot.Create(productId, productName, price));
            Assert.Contains("Product Name cannot be empty", ex.Message);
        }

        [Fact]
        public void Given_WhitespaceProductName_When_Validated_Then_ShouldThrowValidationError()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productName = "   ";
            var price = 10m;

            // Act and Assert
            var ex = Assert.Throws<ValidationException>(() => ProductSnapshot.Create(productId, productName, price));
            Assert.Contains("Product Name cannot be empty", ex.Message);
        }

        [Fact]
        public void Given_ZeroPrice_When_Validated_Then_ShouldThrowValidationError()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productName = "Valid Product";
            var price = 0m;

            // Act and Assert
            var ex = Assert.Throws<ValidationException>(() => ProductSnapshot.Create(productId, productName, price));
            Assert.Contains("Product Price must be greater than zero", ex.Message);
        }

        [Fact]
        public void Given_NegativePrice_When_Validated_Then_ShouldThrowValidationError()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productName = "Valid Product";
            var price = -10m;

            // Act and Assert
            var ex = Assert.Throws<ValidationException>(() => ProductSnapshot.Create(productId, productName, price));
            Assert.Contains("Product Price must be greater than zero", ex.Message);
        }
    }
}
