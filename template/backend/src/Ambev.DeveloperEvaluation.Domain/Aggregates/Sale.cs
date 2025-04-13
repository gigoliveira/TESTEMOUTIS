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
        public List<SaleItem> Items { get; }
        public SaleStatus Status { get; }

        public Sale(Guid id, Guid customerId, List<SaleItem> items, SaleStatus status)
        {
            Id = id;
            CustomerId = customerId;
            Items = items;
            Status = status;
        }
    }
}
