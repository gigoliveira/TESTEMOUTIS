using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
    public class Sale
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public Guid BranchId { get; }
        public List<SaleItem> Items { get; private set; }
        public SaleStatus Status { get; private set; }

        public Sale(Guid customerId, Guid branchId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Items = new List<SaleItem>();
            Status = SaleStatus.Pending;
            BranchId = branchId;
        }

        public void AddItem(SaleItem item)
        {
            if (Status == SaleStatus.Completed || Status == SaleStatus.Cancelled)
            {
                throw new InvalidOperationException("Cannot add items to a completed sale.");
            }

            Items.Add(item);
        }

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

    }
}

