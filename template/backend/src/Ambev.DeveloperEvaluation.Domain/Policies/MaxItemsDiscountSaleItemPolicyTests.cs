using System;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Policies
{
    public class MaxItemsDiscountSaleItemPolicy
    {
        public decimal ApplyDiscount(SaleItem item)
        {
            if (item.Quantity <= 0)
                throw new InvalidOperationException($"Cannot purchase more than 20 identical items for '{item.Product.ProductName}'.");
            else if (item.Quantity > 20)
                throw new InvalidOperationException($"Cannot purchase more than 20 identical items for '{item.Product.ProductName}'.");
            else if (item.Quantity >= 10 && item.Quantity <= 20)
                return item.Product.ProductPrice * 0.20m;
            else if (item.Quantity >= 4)
                return item.Product.ProductPrice * 0.10m;
            else
                return item.Product.ProductPrice;
        }
    }
}
