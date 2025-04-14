using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Infrastructure
{
    /// <summary>
    /// Unit tests for the <see cref="SaleRepository"/> class.
    /// </summary>
    public class SaleRepositoryTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DefaultContext _dbContext;

        public SaleRepositoryTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _dbContext = new DefaultContext(dbContextOptions);
            _saleRepository = new SaleRepository(_dbContext);
        }

        /// <summary>
        /// Given an existing sale, when GetById is called, then it should return the sale.
        /// </summary>
        [Fact]
        public async Task Given_ExistingSale_When_GetById_Then_ShouldReturnSale()
        {
            // Arrange
            var sale = new Sale(new CustomerSnapshot(Guid.NewGuid(), "Customer"), new BranchSnapshot(Guid.NewGuid(), "Branch"));
            await _dbContext.Sales.AddAsync(sale);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _saleRepository.GetByIdAsync(sale.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sale.Id, result.Id);
        }

        /// <summary>
        /// Given a new sale, when AddAsync is called, then it should persist the sale.
        /// </summary>
        [Fact]
        public async Task Given_NewSale_When_Added_Then_ShouldPersist()
        {
            // Arrange
            var sale = new Sale(new CustomerSnapshot(Guid.NewGuid(), "Customer"), new BranchSnapshot(Guid.NewGuid(), "Branch"));

            // Act
            await _saleRepository.AddAsync(sale);
            await _dbContext.SaveChangesAsync();

            // Assert
            var addedSale = await _dbContext.Sales.FindAsync(sale.Id);
            Assert.NotNull(addedSale);
        }

        /// <summary>
        /// Given an existing sale, when UpdateAsync is called, then it should persist the changes.
        /// </summary>
        [Fact]
        public async Task Given_ExistingSale_When_Updated_Then_ShouldPersistChanges()
        {
            // Arrange
            var sale = new Sale(new CustomerSnapshot(Guid.NewGuid(), "Customer"), new BranchSnapshot(Guid.NewGuid(), "Branch"));
            await _dbContext.Sales.AddAsync(sale);
            await _dbContext.SaveChangesAsync();

            // Act
            sale.Status = SaleStatus.Completed;
            await _saleRepository.UpdateAsync(sale);
            await _dbContext.SaveChangesAsync();

            // Assert
            var updatedSale = await _dbContext.Sales.FindAsync(sale.Id);
            Assert.Equal(SaleStatus.Completed, updatedSale.Status);
        }

        /// <summary>
        /// Given an existing sale, when DeleteAsync is called, then it should remove the sale from the database.
        /// </summary>
        [Fact]
        public async Task Given_ExistingSale_When_Deleted_Then_ShouldRemoveFromDatabase()
        {
            // Arrange
            var sale = new Sale(new CustomerSnapshot(Guid.NewGuid(), "Customer"), new BranchSnapshot(Guid.NewGuid(), "Branch"));
            await _dbContext.Sales.AddAsync(sale);
            await _dbContext.SaveChangesAsync();

            // Act
            await _saleRepository.DeleteAsync(sale);
            await _dbContext.SaveChangesAsync();

            // Assert
            var deletedSale = await _dbContext.Sales.FindAsync(sale.Id);
            Assert.Null(deletedSale);
        }

        /// <summary>
        /// Given no sales, when GetAll is called, then it should return an empty list.
        /// </summary>
        [Fact]
        public async Task Given_NoSales_When_GetAll_Then_ShouldReturnEmptyList()
        {
            // Act
            var result = await _saleRepository.GetAllAsync();

            // Assert
            Assert.Empty(result);
        }
    }
}
