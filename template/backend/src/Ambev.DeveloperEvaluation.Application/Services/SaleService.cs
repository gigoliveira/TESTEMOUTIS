using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Policies;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Services
{
    /// <summary>
    /// Application service for handling sale operations, interacting with the repository.
    /// </summary>
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        /// <summary>
        /// Persists a new sale to the repository.
        /// </summary>
        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            if (sale == null)
                throw new ArgumentException("Sale cannot be null.");

            var policy = new MaxItemsDiscountSaleItemPolicy();

            foreach (var item in sale.Items.ToList())
                item.UpdateDiscount(policy.ApplyDiscount(item));

            await _saleRepository.AddAsync(sale);

            return sale;
        }

        /// <summary>
        /// Updates an existing sale in the repository.
        /// </summary>
        public async Task<Sale> UpdateSaleAsync(Sale sale)
        {
            if (sale == null)
                throw new ArgumentException("Sale cannot be null.");

            var policy = new MaxItemsDiscountSaleItemPolicy();

            foreach (var item in sale.Items.ToList())
                item.UpdateDiscount(policy.ApplyDiscount(item));

            await _saleRepository.UpdateAsync(sale);

            return sale;
        }

        /// <summary>
        /// Deletes a sale from the repository by its ID.
        /// </summary>
        public async Task<Sale> CancelSaleAsync(Guid saleId)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);

            if (sale == null)
                throw new InvalidOperationException("Sale not found.");

            sale.Cancel();

            await _saleRepository.CancelAsync(sale);
            return sale; // ✅ Make sure this is here
        }


        /// <inheritdoc/>
        public async Task<Sale> GetById(Guid saleId)
        {
            return await _saleRepository.GetByIdAsync(saleId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Sale>> GetAll()
        {
            return await _saleRepository.GetAllAsync();
        }
    }

}
