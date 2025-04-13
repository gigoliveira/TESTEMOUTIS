using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
    public class Sale
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public List<SaleItem> Items { get; private set; }
        public SaleStatus Status { get; private set; }

        public Sale(Guid customerId)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Items = new List<SaleItem>();
            Status = SaleStatus.Pending;
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
    }
}

