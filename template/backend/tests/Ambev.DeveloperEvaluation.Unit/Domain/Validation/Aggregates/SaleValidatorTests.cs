using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation.SaleValidations;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.Aggregates
{
    public class SaleValidatorTests
    {
        // Given a valid sale
        // When validated
        // Then should pass validation
        [Fact]
        public void Given_ValidSale_When_Validated_Then_ShouldPassValidation()
        {
            // Arrange
            var items = new List<SaleItem>
            {
                new SaleItem(new ProductSnapshot(Guid.NewGuid().ToString(), "Product A", 100), 2)
            };
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), items, SaleStatus.Pending);
            var validator = new SaleValidator();

            // Act & Assert
            validator.Validate(sale);
        }

        // Given a sale without items
        // When validated
        // Then should throw ValidationException
        [Fact]
        public void Given_SaleWithoutItems_When_Validated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var items = new List<SaleItem>(); 
            var sale = new Sale(Guid.NewGuid(), Guid.NewGuid(), items, SaleStatus.Pending);
            var validator = new SaleValidator();

            // Act & Assert
            Assert.Throws<ValidationException>(() => validator.Validate(sale));
        }
    }
}
