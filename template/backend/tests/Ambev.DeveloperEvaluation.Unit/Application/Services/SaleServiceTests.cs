using System;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Services;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Services
{
    /// <summary>
    /// Unit tests for the SaleService class, validating business rules and repository interactions.
    /// </summary>
    public class SaleServiceTests
    {
        private const decimal UnitPrice = 100m;
        private readonly ISaleRepository _repositoryMock;
        private readonly ISaleService _service;

        /// <summary>
        /// Initializes the test class with mocked dependencies.
        /// </summary>
        public SaleServiceTests()
        {
            _repositoryMock = Substitute.For<ISaleRepository>();
            _service = new SaleService(_repositoryMock);
        }

        /// <summary>
        /// Creates a valid sale instance with optional sale items.
        /// </summary>
        /// <param name="itemQuantity">Quantity of items to add to the sale.</param>
        /// <returns>A valid Sale object.</returns>
        private Sale CreateValidSale(int itemQuantity = 0)
        {
            var sale = Sale.CreateSale(
                Guid.NewGuid(),
                new CustomerSnapshot(Guid.NewGuid(), "John Doe"),
                new BranchSnapshot(Guid.NewGuid(), "Main Branch")
            );

            if (itemQuantity > 0)
            {
                var productSnapshot = ProductSnapshot.Create(Guid.NewGuid(), "Product A", UnitPrice);
                var item = SaleItem.CreateSaleItem(sale.Id, productSnapshot, itemQuantity);
                sale.AddItem(item);
            }

            return sale;
        }

        /// <summary>
        /// Verifies that a valid sale is persisted to the repository when created.
        /// </summary>
        [Fact]
        public async Task Given_ValidSale_When_CreateSale_Then_ShouldPersist()
        {
            var sale = CreateValidSale();
            await _service.CreateSaleAsync(sale);
            await _repositoryMock.Received(1).AddAsync(sale);
        }

        /// <summary>
        /// Ensures an ArgumentException is thrown when attempting to create a null sale.
        /// </summary>
        [Fact]
        public async Task Given_InvalidSale_When_CreateSale_Then_ShouldThrowValidationException()
        {
            Sale invalidSale = null;
            Func<Task> act = async () => await _service.CreateSaleAsync(invalidSale);
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Sale cannot be null.");
        }

        /// <summary>
        /// Validates that no discount is applied for a sale with 3 items.
        /// </summary>
        [Fact]
        public async Task Given_ValidSaleWith3Items_When_CreateSale_Then_NoDiscountShouldBeApplied()
        {
            var sale = CreateValidSale(3);
            var result = await _service.CreateSaleAsync(sale);

            await _repositoryMock.Received(1).AddAsync(sale);
            var item = result.Items.First();
            item.DiscountPrice.Should().Be(UnitPrice);
            item.TotalAmount.Should().Be(UnitPrice * 3);
        }

        /// <summary>
        /// Validates that a 10% discount is applied when creating a sale with 4 items.
        /// </summary>
        [Fact]
        public async Task Given_ValidSaleWith4Items_When_CreateSale_Then_10PercentDiscountShouldBeApplied()
        {
            var sale = CreateValidSale(4);
            var result = await _service.CreateSaleAsync(sale);

            await _repositoryMock.Received(1).AddAsync(sale);
            var item = result.Items.First();
            item.DiscountPrice.Should().Be(UnitPrice * 0.10m);
            item.TotalAmount.Should().Be(item.DiscountPrice * 4);
        }

        /// <summary>
        /// Validates that a 20% discount is applied when creating a sale with 15 items.
        /// </summary>
        [Fact]
        public async Task Given_ValidSaleWith15Items_When_CreateSale_Then_20PercentDiscountShouldBeApplied()
        {
            var sale = CreateValidSale(15);
            var result = await _service.CreateSaleAsync(sale);

            await _repositoryMock.Received(1).AddAsync(sale);
            var item = result.Items.First();
            item.DiscountPrice.Should().Be(UnitPrice * 0.20m);
            item.TotalAmount.Should().Be(item.DiscountPrice * 15);
        }

        /// <summary>
        /// Ensures an exception is thrown when attempting to purchase more than 20 of the same item.
        /// </summary>
        [Fact]
        public async Task Given_ValidSaleWith21Items_When_CreateSale_Then_ShouldThrowException()
        {
            var sale = CreateValidSale(19);
            sale.Items.FirstOrDefault().UpdateQuantity(22);

            Func<Task> act = async () => await _service.CreateSaleAsync(sale);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Cannot purchase more than 20 identical items for 'Product A'.");
        }

        /// <summary>
        /// Verifies that an existing sale is updated correctly in the repository.
        /// </summary>
        [Fact]
        public async Task Given_ExistingSale_When_UpdateSale_Then_ShouldApplyChanges()
        {
            var sale = CreateValidSale();
            await _service.UpdateSaleAsync(sale);
            await _repositoryMock.Received(1).UpdateAsync(sale);
        }

        /// <summary>
        /// Validates no discount is applied when updating a sale with 3 items.
        /// </summary>
        [Fact]
        public async Task Given_ValidSaleWith3Items_When_UpdateSale_Then_NoDiscountShouldBeApplied()
        {
            var sale = CreateValidSale(3);
            await _service.UpdateSaleAsync(sale);
            var item = sale.Items.First();
            item.DiscountPrice.Should().Be(UnitPrice);
            item.TotalAmount.Should().Be(UnitPrice * 3);
        }

        /// <summary>
        /// Validates a 10% discount is applied when updating a sale with 4 items.
        /// </summary>
        [Fact]
        public async Task Given_ValidSaleWith4Items_When_UpdateSale_Then_10PercentDiscountShouldBeApplied()
        {
            var sale = CreateValidSale(4);
            await _service.UpdateSaleAsync(sale);
            var item = sale.Items.First();
            item.DiscountPrice.Should().Be(UnitPrice * 0.10m);
            item.TotalAmount.Should().Be(item.DiscountPrice * 4);
        }

        /// <summary>
        /// Validates a 20% discount is applied when updating a sale with 15 items.
        /// </summary>
        [Fact]
        public async Task Given_ValidSaleWith15Items_When_UpdateSale_Then_20PercentDiscountShouldBeApplied()
        {
            var sale = CreateValidSale(15);
            await _service.UpdateSaleAsync(sale);
            var item = sale.Items.First();
            item.DiscountPrice.Should().Be(UnitPrice * 0.20m);
            item.TotalAmount.Should().Be(item.DiscountPrice * 15);
        }

        /// <summary>
        /// Ensures an exception is thrown when updating a sale with more than 20 identical items.
        /// </summary>
        [Fact]
        public async Task Given_ValidSaleWith21Items_When_UpdateSale_Then_ShouldThrowException()
        {
            var sale = CreateValidSale(19);
            sale.Items.FirstOrDefault().UpdateQuantity(22);

            Func<Task> act = async () => await _service.UpdateSaleAsync(sale);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Cannot purchase more than 20 identical items for 'Product A'.");
        }

        /// <summary>
        /// Verifies that a sale can be canceled and its status updated.
        /// </summary>
        [Fact]
        public async Task Given_ExistingSale_When_CancelSale_Then_ShouldUpdateStatus()
        {
            var sale = CreateValidSale();
            await _service.CreateSaleAsync(sale);
            _repositoryMock.GetByIdAsync(sale.Id).Returns(sale);

            var canceledSale = await _service.CancelSaleAsync(sale.Id);

            canceledSale.IsCancelled().Should().BeTrue();
            await _repositoryMock.Received(1).CancelAsync(sale);
        }

        /// <summary>
        /// Verifies that the CancelAsync method is called when canceling a sale.
        /// </summary>
        [Fact]
        public async Task Given_ExistingSale_When_CancelSale_Then_ShouldCancelIt()
        {
            var sale = CreateValidSale();
            await _service.CreateSaleAsync(sale);
            _repositoryMock.GetByIdAsync(sale.Id).Returns(sale);

            await _service.CancelSaleAsync(sale.Id);
            await _repositoryMock.Received(1).CancelAsync(sale);
        }
    }
}
