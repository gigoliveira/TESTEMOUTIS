using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Repositories
{
    /// <summary>
    /// Unit tests for the <see cref="ISaleRepository"/> interface.
    /// </summary>
    public class ISaleRepositoryTests
    {
        private readonly ISaleRepository _repositoryMock;
        private readonly Sale _sale;

        /// <summary>
        /// Initializes a new instance of the <see cref="ISaleRepositoryTests"/> class.
        /// </summary>
        public ISaleRepositoryTests()
        {
            _repositoryMock = Substitute.For<ISaleRepository>();
            var customerSnapshot = new CustomerSnapshot(Guid.NewGuid(), "Customer Name");
            var branchSnapshot = new BranchSnapshot(Guid.NewGuid(), "Branch Name");
            _sale = new Sale(customerSnapshot, branchSnapshot);
        }

        /// <summary>
        /// Given a valid id, when calling GetByIdAsync, then should return the correct sale.
        /// </summary>
        [Fact]
        public async Task Given_ValidId_When_GetByIdAsyncCalled_Then_ShouldReturnCorrectSale()
        {
            // Arrange
            var saleId = _sale.Id;
            _repositoryMock.GetByIdAsync(saleId).Returns(_sale);

            // Act
            var result = await _repositoryMock.GetByIdAsync(saleId);

            // Assert
            Assert.Equal(_sale, result);
        }

        /// <summary>
        /// When calling GetAllAsync, then should return all sales.
        /// </summary>
        [Fact]
        public async Task When_GetAllAsyncCalled_Then_ShouldReturnAllSales()
        {
            // Arrange
            var sales = new List<Sale>
            {
                _sale,
                _sale
            };

            _repositoryMock.GetAllAsync().Returns(sales);

            // Act
            var result = await _repositoryMock.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(_sale, result);
        }

        /// <summary>
        /// Given a sale, when calling AddAsync, then should call the repository's AddAsync method.
        /// </summary>
        [Fact]
        public async Task Given_Sale_When_AddAsyncCalled_Then_ShouldCallRepositoryAdd()
        {
            // Act
            await _repositoryMock.AddAsync(_sale);

            // Assert
            await _repositoryMock.Received(1).AddAsync(_sale);
        }

        /// <summary>
        /// Given a sale, when calling UpdateAsync, then should call the repository's UpdateAsync method.
        /// </summary>
        [Fact]
        public async Task Given_Sale_When_UpdateAsyncCalled_Then_ShouldCallRepositoryUpdate()
        {
            // Act
            await _repositoryMock.UpdateAsync(_sale);

            // Assert
            await _repositoryMock.Received(1).UpdateAsync(_sale);
        }

        /// <summary>
        /// Given a sale, when calling DeleteAsync, then should call the repository's DeleteAsync method.
        /// </summary>
        [Fact]
        public async Task Given_Sale_When_DeleteAsyncCalled_Then_ShouldCallRepositoryDelete()
        {
            // Act
            await _repositoryMock.DeleteAsync(_sale);

            // Assert
            await _repositoryMock.Received(1).DeleteAsync(_sale);
        }
    }
}

