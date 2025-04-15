using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Policies;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
    /// <summary>
    /// Represents a sale with customer, branch, and a list of sale items.
    /// </summary>
    public class Sale : BaseEntity
    {
        public CustomerSnapshot Customer { get; set; }
        public BranchSnapshot Branch { get; set; }
        public List<SaleItem> Items { get; private set; }
        public SaleStatus Status { get; set; }
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Calculates the total value of the sale based on its items.
        /// </summary>
        public decimal TotalSaleAmount => Items.Sum(i => i.TotalAmount);
        protected Sale() { }
        public Sale(Guid id, CustomerSnapshot customer, BranchSnapshot branch)
        {
            Id = id;
            Customer = customer;
            Items = new List<SaleItem>();
            Status = SaleStatus.Pending;
            Branch = branch;
        }

        /// <summary>
        /// Adds an item to the sale.
        /// Throws an exception if the sale is completed or cancelled.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <exception cref="InvalidOperationException">Thrown when attempting to add an item to a completed or cancelled sale.</exception>
        public void AddItem(SaleItem item)
        {
            var policy = new MaxItemsDiscountSaleItemPolicy();
            item.UpdateDiscount(policy.ApplyDiscount(item));

            Items.Add(item);
        }

        public void UpdateDiscountItem(SaleItem item)
        {
            if (Status == SaleStatus.Completed || Status == SaleStatus.Cancelled)
            {
                throw new InvalidOperationException("Cannot add items to a completed sale.");
            }
            var policy = new MaxItemsDiscountSaleItemPolicy();
            item.UpdateDiscount(policy.ApplyDiscount(item));
        }


        /// <summary>
        /// Cancels the sale, setting its status to <see cref="SaleStatus.Cancelled"/>.
        /// Throws an exception if the sale is already completed.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when attempting to cancel a completed sale.</exception>
        public void Cancel()
        {
            if (Status == SaleStatus.Completed)
            {
                throw new InvalidOperationException("A completed sale cannot be cancelled.");
            }

            Status = SaleStatus.Cancelled;
        }

        /// <summary>
        /// Finalizes the sale, setting its status to <see cref="SaleStatus.Completed"/>.
        /// Throws an exception if the sale has no items.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when attempting to finalize a sale without items.</exception>
        public void Finalize()
        {
            if (Items == null || Items.Count == 0)
            {
                throw new InvalidOperationException("Cannot finalize a sale with no items.");
            }

            Status = SaleStatus.Completed;
        }
        /// <summary>
        /// Updates the customer and branch information of the sale.
        /// </summary>
        public void Update(CustomerSnapshot customer, BranchSnapshot branch)
        {
            Customer = customer;
            Branch = branch;
        }
        /// <summary>
        /// Creates a new sale instance.
        /// </summary>
        /// <param name="customerId">The external customer ID.</param>
        /// <param name="customerName">The customer name.</param>
        /// <param name="branchId">The external branch ID.</param>
        /// <param name="branchName">The branch name.</param>
        /// <returns>A new instance of the <see cref="Sale"/> class.</returns>
        public static Sale CreateSale(Guid id, CustomerSnapshot customer, BranchSnapshot branch)
        {
            return new Sale(
                id,
                customer,
                branch
            );
        }
        /// <summary>
        /// Checks if the sale is cancelled.
        /// </summary>
        /// <returns>True if the sale status is Cancelled; otherwise, false.</returns>
        public bool IsCancelled()
        {
            return Status == SaleStatus.Cancelled;
        }

    }
}

