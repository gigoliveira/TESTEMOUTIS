using System;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.ValueObjects
{
    /// <summary>
    /// Unit tests for the <see cref="ProductSnapshot"/> value object.
    /// </summary>
    public class ProductSnapshotTests
    {
        /// <summary>
        /// Given valid parameters, when creating a ProductSnapshot, then should store correct data.
        /// </summary>
        [Fact]
        public void Given_ValidParameters_When_SnapshotCreated_Then_ShouldStoreCorrectData()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productName = "Product";
            var price = 100m;

            // Act
            var snapshot = ProductSnapshot.Create(productId, productName, price);

            // Assert
            Assert.Equal(productId, snapshot.ExternalProductId);
            Assert.Equal(productName, snapshot.ProductName);
            Assert.Equal(price, snapshot.ProductPrice);
        }

        /// <summary>
        /// Given invalid product id, when creating a ProductSnapshot, then should throw ValidationException.
        /// </summary>
        [Fact]
        public void Given_InvalidProductId_When_SnapshotCreated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var productId = Guid.Empty;
            var productName = "Product";
            var price = 100m;

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => ProductSnapshot.Create(productId, productName, price));
            Assert.Contains("Product Id cannot be empty", ex.Message);
        }

        /// <summary>
        /// Given invalid product name, when creating a ProductSnapshot, then should throw ValidationException.
        /// </summary>
        [Fact]
        public void Given_InvalidProductName_When_SnapshotCreated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productName = string.Empty;
            var price = 100m;

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => ProductSnapshot.Create(productId, productName, price));
            Assert.Contains("Product Name cannot be empty", ex.Message);
        }

        /// <summary>
        /// Given invalid price, when creating a ProductSnapshot, then should throw ValidationException.
        /// </summary>
        [Fact]
        public void Given_InvalidPrice_When_SnapshotCreated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productName = "Product";
            var price = 0m;

            // Act & Assert
            var ex = Assert.Throws<ValidationException>(() => ProductSnapshot.Create(productId, productName, price));
            Assert.Contains("Product Price must be greater than zero", ex.Message);
        }
    }
}