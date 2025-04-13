using System;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class ValidStatusSpecificationTests
    {
        // Given a sale with a valid status
        // When validated
        // Then should pass specification
        [Fact]
        public void Given_SaleWithValidStatus_When_Validated_Then_ShouldPassSpecification()
        {
            // Arrange
            var sale = new Sale(Guid.NewGuid());

            // Act
            var specification = new ValidStatusSpecification();
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            Assert.True(result);
        }

        // Given a sale with an invalid status
        // When validated
        // Then should fail specification
        [Fact]
        public void Given_SaleWithInvalidStatus_When_Validated_Then_ShouldFailSpecification()
        {
            // Arrange
            var sale = new Sale(Guid.NewGuid()); // assuming Cancelled is invalid for the example
            sale.Cancel();

            // Act
            var specification = new ValidStatusSpecification();
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            Assert.False(result);
        }
    }
}
