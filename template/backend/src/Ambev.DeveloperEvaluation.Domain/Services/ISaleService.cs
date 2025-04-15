using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    /// <summary>
    /// Defines the contract for the sale application service operations.
    /// </summary>
    public interface ISaleService
    {
        /// <summary>
        /// Persists a new sale to the repository.
        /// </summary>
        Task<Sale> CreateSaleAsync(Sale sale);

        /// <summary>
        /// Updates an existing sale in the repository.
        /// </summary>
        Task<Sale> UpdateSaleAsync(Sale sale);

        /// <summary>
        /// Deletes a sale from the repository by its ID.
        /// </summary>
        Task<Sale> CancelSaleAsync(Guid saleId);

        /// <summary>
        /// Retrieves a Sale by its unique identifier.
        /// </summary>
        /// <param name="saleId">The unique identifier of the Sale.</param>
        /// <returns>The Sale entity if found; otherwise, null.</returns>
        Task<Sale> GetById(Guid saleId);

        /// <summary>
        /// Retrieves all existing Sales.
        /// </summary>
        /// <returns>A collection of all Sales.</returns>
        Task<IEnumerable<Sale>> GetAll();
    }
}