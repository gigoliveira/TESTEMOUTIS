using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; set; }
        public ProductSnapshot Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal TotalValue => Product.ProductPrice * Quantity;
        protected SaleItem() { }

        public SaleItem(ProductSnapshot product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }

}
