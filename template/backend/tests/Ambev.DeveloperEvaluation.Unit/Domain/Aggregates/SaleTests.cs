using System;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Aggregates
{
    /// <summary>
    /// Unit tests for the <see cref="Sale"/> aggregate.
    /// </summary>
    public class SaleTests
    {
        /// <summary>
        /// Given valid parameters, when creating a Sale, then should store correct data.
        /// </summary>
        [Fact]
        public void Given_ValidParameters_When_SaleCreated_Then_ShouldStoreCorrectData()
        {
            // Arrange
            var customerSnapshot = new CustomerSnapshot(Guid.NewGuid(), "Customer Name");
            var branchSnapshot = new BranchSnapshot(Guid.NewGuid(), "Branch Name");

            // Act
            var sale = new Sale(customerSnapshot, branchSnapshot);

            // Assert
            Assert.Equal(customerSnapshot.ExternalCustomerId, sale.Customer.ExternalCustomerId);
            Assert.Equal(branchSnapshot.ExternalBranchId, sale.Branch.ExternalBranchId);
            Assert.NotEqual(Guid.Empty, sale.Id);
            Assert.Empty(sale.Items);
            Assert.Equal(SaleStatus.Pending, sale.Status);
        }
        /// <summary>
        /// Given valid item, when adding item to sale, then should add to items list.
        /// </summary>
        [Fact]
        public void Given_ValidItem_When_AddItem_Then_ShouldAddToItemsList()
        {
            // Arrange
            var customerSnapshot = new CustomerSnapshot(Guid.NewGuid(), "Customer Name");
            var branchSnapshot = new BranchSnapshot(Guid.NewGuid(), "Branch Name");
            var sale = new Sale(customerSnapshot, branchSnapshot);
            var productSnapshot = new ProductSnapshot(Guid.NewGuid(), "Product Name", 10.0m);

            // Act
            var saleItem = new SaleItem(productSnapshot, 5);
            sale.AddItem(saleItem);

            // Assert
            Assert.Single(sale.Items);
            Assert.Equal(productSnapshot.ExternalProductId, sale.Items[0].Product.ExternalProductId);
            Assert.Equal(5, sale.Items[0].Quantity);
            Assert.Equal(10.0m, sale.Items[0].Product.ProductPrice);
        }

        /// <summary>
        /// Given no items, when finalizing sale, then should throw InvalidOperationException.
        /// </summary>
        [Fact]
        public void Given_NoItems_When_FinalizingSale_Then_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var customerSnapshot = new CustomerSnapshot(Guid.NewGuid(), "Customer Name");
            var branchSnapshot = new BranchSnapshot(Guid.NewGuid(), "Branch Name");
            var sale = new Sale(customerSnapshot, branchSnapshot);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sale.Finalize());
        }

        /// <summary>
        /// Given items, when finalizing sale, then should set status to Completed.
        /// </summary>
        [Fact]
        public void Given_Items_When_FinalizingSale_Then_ShouldSetStatusToCompleted()
        {
            // Arrange
            var customerSnapshot = new CustomerSnapshot(Guid.NewGuid(), "Customer Name");
            var branchSnapshot = new BranchSnapshot(Guid.NewGuid(), "Branch Name");
            var sale = new Sale(customerSnapshot, branchSnapshot);
            var productSnapshot = new ProductSnapshot(Guid.NewGuid(), "Product Name", 5.0m);
            var saleItem = new SaleItem(productSnapshot, 3);
            sale.AddItem(saleItem);

            // Act
            sale.Finalize();

            // Assert
            Assert.Equal(SaleStatus.Completed, sale.Status);
        }

        /// <summary>
        /// Given finalized sale, when canceling, then should throw InvalidOperationException.
        /// </summary>
        [Fact]
        public void Given_FinalizedSale_When_Canceling_Then_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var customerSnapshot = new CustomerSnapshot(Guid.NewGuid(), "Customer Name");
            var branchSnapshot = new BranchSnapshot(Guid.NewGuid(), "Branch Name");
            var sale = new Sale(customerSnapshot, branchSnapshot);
            var productSnapshot = new ProductSnapshot(Guid.NewGuid(), "Product Name", 15.0m);
            var saleItem = new SaleItem(productSnapshot, 2);
            sale.AddItem(saleItem);
            sale.Finalize();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => sale.Cancel());
        }

        /// <summary>
        /// Given pending sale, when canceling, then should set status to Canceled.
        /// </summary>
        [Fact]
        public void Given_PendingSale_When_Canceling_Then_ShouldSetStatusToCanceled()
        {
            // Arrange
            var customerSnapshot = new CustomerSnapshot(Guid.NewGuid(), "Customer Name");
            var branchSnapshot = new BranchSnapshot(Guid.NewGuid(), "Branch Name");
            var sale = new Sale(customerSnapshot, branchSnapshot);
            var productSnapshot = new ProductSnapshot(Guid.NewGuid(), "Product Name", 20.0m);
            var saleItem = new SaleItem(productSnapshot, 1);
            sale.AddItem(saleItem);

            // Act
            sale.Cancel();

            // Assert
            Assert.Equal(SaleStatus.Cancelled, sale.Status);
        }
    }
}

