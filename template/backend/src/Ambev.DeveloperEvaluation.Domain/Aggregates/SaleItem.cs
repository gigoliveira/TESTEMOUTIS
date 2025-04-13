using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
    public class SaleItem
    {
        public ProductSnapshot Product { get; }
        public int Quantity { get; }

        public SaleItem(ProductSnapshot product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
