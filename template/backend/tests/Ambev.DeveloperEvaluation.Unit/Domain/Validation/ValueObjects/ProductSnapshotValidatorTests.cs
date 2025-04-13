using System;
using Xunit;
using FluentAssertions;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Validation.ProductSnapshotValidations;
using System.ComponentModel.DataAnnotations;
using Ambev.DeveloperEvaluation.Unit.Domain.Validation.ValueObjects.TestData;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.ValueObjects
{
    public class ProductSnapshotValidatorTests
    {
        private readonly ProductSnapshotValidator _validator;

        public ProductSnapshotValidatorTests()
        {
            _validator = new ProductSnapshotValidator();
        }

        [Fact]
        public void Given_ValidProduct_When_Validated_Then_ShouldPassValidation()
        {
            // Arrange
            var validProductSnapshot = ProductSnapshotValidatorTestData.GenerateValidProduct();

            // Act
            var validationResult = _validator.Validate(validProductSnapshot);

            // Assert
            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Given_InvalidProductPrice_When_Validated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var invalidProductSnapshot = ProductSnapshotValidatorTestData.GenerateInvalidPriceProduct();

            // Act
            var validationResult = _validator.Validate(invalidProductSnapshot);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "Product Price must be greater than zero");
        }

        [Fact]
        public void Given_InvalidProductName_When_Validated_Then_ShouldThrowValidationException()
        {
            // Arrange
            var invalidProductSnapshot = new ProductSnapshot(Guid.NewGuid(), "", 100);

            // Act
            var validationResult = _validator.Validate(invalidProductSnapshot);

            // Assert
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle(error => error.ErrorMessage == "Product Name cannot be empty");
        }
    }
}

