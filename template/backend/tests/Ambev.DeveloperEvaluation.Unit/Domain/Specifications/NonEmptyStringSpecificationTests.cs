using Xunit;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class NonEmptyStringSpecificationTests
    {
        // Given a valid string
        // When validated
        // Then should pass specification
        [Fact]
        public void Given_ValidString_When_Validated_Then_ShouldPassSpecification()
        {
            // Arrange
            var specification = new NonEmptyStringSpecification();
            var validString = "Valid String";

            // Act
            var result = specification.IsSatisfiedBy(validString);

            // Assert
            Assert.True(result);
        }

        // Given an empty string
        // When validated
        // Then should fail specification
        [Fact]
        public void Given_EmptyString_When_Validated_Then_ShouldFailSpecification()
        {
            // Arrange
            var specification = new NonEmptyStringSpecification();
            var emptyString = string.Empty;

            // Act
            var result = specification.IsSatisfiedBy(emptyString);

            // Assert
            Assert.False(result);
        }

        // Given a string with only whitespace
        // When validated
        // Then should fail specification
        [Fact]
        public void Given_WhiteSpaceString_When_Validated_Then_ShouldFailSpecification()
        {
            // Arrange
            var specification = new NonEmptyStringSpecification();
            var whitespaceString = "   "; // string with only spaces

            // Act
            var result = specification.IsSatisfiedBy(whitespaceString);

            // Assert
            Assert.False(result);
        }
    }
}
