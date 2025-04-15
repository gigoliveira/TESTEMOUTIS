using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    /// <summary>
    /// Defines a contract for managing <see cref="Sale"/> aggregates in a repository.
    /// </summary>
    public interface ISaleRepository
    {
        /// <summary>
        /// Retrieves a sale by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the sale.</param>
        /// <returns>The sale matching the provided id.</returns>
        Task<Sale> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all sales.
        /// </summary>
        /// <returns>A collection of all sales.</returns>
        Task<IEnumerable<Sale>> GetAllAsync();

        /// <summary>
        /// Adds a new sale to the repository.
        /// </summary>
        /// <param name="sale">The sale to be added.</param>
        Task<Sale> AddAsync(Sale sale);

        /// <summary>
        /// Updates an existing sale in the repository.
        /// </summary>
        /// <param name="sale">The sale to be updated.</param>
        Task<Sale> UpdateAsync(Sale sale);

        /// <summary>
        /// Deletes an existing sale from the repository.
        /// </summary>
        /// <param name="sale">The sale to be deleted.</param>
        Task<Sale> CancelAsync(Sale sale);
    }
}
