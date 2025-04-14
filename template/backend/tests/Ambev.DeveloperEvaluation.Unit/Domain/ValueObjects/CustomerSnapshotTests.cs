using System;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.ValueObjects
{
    /// <summary>
    /// Unit tests for the <see cref="CustomerSnapshot"/> value object.
    /// </summary>
    public class CustomerSnapshotTests
    {
        /// <summary>
        /// Given external customer id, when snapshot created, then should store id and name.
        /// </summary>
        [Fact]
        public void Given_ExternalCustomerId_When_SnapshotCreated_Then_ShouldStoreIdAndName()
        {
            var externalCustomerId = Guid.NewGuid();
            var customerName = "John Doe";

            var snapshot = CustomerSnapshot.Create(externalCustomerId, customerName);

            Assert.Equal(externalCustomerId, snapshot.ExternalCustomerId);
            Assert.Equal(customerName, snapshot.CustomerName);
        }

        /// <summary>
        /// Given invalid customer id, when snapshot created, then should throw validation exception.
        /// </summary>
        [Fact]
        public void Given_InvalidCustomerId_When_SnapshotCreated_Then_ShouldThrowValidationException()
        {
            var externalCustomerId = Guid.Empty;
            var customerName = "John Doe";

            var ex = Assert.Throws<ValidationException>(() => CustomerSnapshot.Create(externalCustomerId, customerName));
            Assert.Contains("Customer Id cannot be empty", ex.Message);
        }

        /// <summary>
        /// Given invalid customer name, when snapshot created, then should throw validation exception.
        /// </summary>
        [Fact]
        public void Given_InvalidCustomerName_When_SnapshotCreated_Then_ShouldThrowValidationException()
        {
            var externalCustomerId = Guid.NewGuid();
            var customerName = "";

            var ex = Assert.Throws<ValidationException>(() => CustomerSnapshot.Create(externalCustomerId, customerName));
            Assert.Contains("Customer Name cannot be empty", ex.Message);
        }
    }
}