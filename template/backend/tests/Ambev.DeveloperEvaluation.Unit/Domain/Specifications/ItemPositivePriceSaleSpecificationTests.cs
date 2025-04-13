using System;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class ItemPositivePriceSaleSpecificationTests
    {
        // Given a valid price
        // When validated
        // Then should pass specification
        [Fact]
        public void Given_ValidSaleItem_When_Validated_Then_ShouldPassSpecification()
        {
            // Arrange
            var product = new ProductSnapshot(Guid.NewGuid(), "Product Name", 10.00m);
            var saleItem = new SaleItem(product, 1);
            var specification = new PositivePriceSpecification();

            // Act
            var result = specification.IsSatisfiedBy(saleItem);

            // Assert
            Assert.True(result);
        }

        // Given a negative price
        // When validated
        // Then should fail specification
        [Fact]
        public void Given_NegativePriceSaleItem_When_Validated_Then_ShouldFailSpecification()
        {
            // Arrange
            var product = new ProductSnapshot(Guid.NewGuid(), "Product Name", -5.00m);
            var saleItem = new SaleItem(product, 1);
            var specification = new PositivePriceSpecification();

            // Act
            var result = specification.IsSatisfiedBy(saleItem);

            // Assert
            Assert.False(result);
        }
    }
}
