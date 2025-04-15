using System;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
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
            var customerSnapshot = new CustomerSnapshot(Guid.NewGuid(), "Customer Name");
            var branchSnapshot = new BranchSnapshot(Guid.NewGuid(), "Branch Name");
            var sale = Sale.CreateSale(Guid.NewGuid(), customerSnapshot, branchSnapshot);

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
            var customerSnapshot = new CustomerSnapshot(Guid.NewGuid(), "Customer Name");
            var branchSnapshot = new BranchSnapshot(Guid.NewGuid(), "Branch Name");
            var sale = Sale.CreateSale(Guid.NewGuid(), customerSnapshot, branchSnapshot);
            sale.Cancel();

            // Act
            var specification = new ValidStatusSpecification();
            var result = specification.IsSatisfiedBy(sale);

            // Assert
            Assert.False(result);
        }
    }
}
