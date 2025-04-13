using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class HasItemsSaleSpecificationTests
    {
        // Given a sale with items
        // When validated
        // Then should pass specification
        [Fact]
        public void Given_SaleWithItems_When_Validated_Then_ShouldPassSpecification()
        {
            // Arrange
            var sale = new Sale(Guid.NewGuid());
            var productSnapshot = new ProductSnapshot(Guid.NewGuid(), "Product Name", 10.00m);
            var saleItem = new SaleItem(productSnapshot, 1);
            sale.AddItem(saleItem);
            var specification = new HasItemsSaleSpecification();

            // Act
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            Assert.True(result);
        }

        // Given a sale without items
        // When validated
        // Then should fail specification
        [Fact]
        public void Given_SaleWithoutItems_When_Validated_Then_ShouldFailSpecification()
        {
            // Arrange
            var sale = new Sale(Guid.NewGuid());
            var specification = new HasItemsSaleSpecification();

            // Act
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            Assert.False(result);
        }
    }
}
