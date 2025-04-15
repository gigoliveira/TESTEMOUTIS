using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Aggregates
{
    /// <summary>
    /// Represents an individual item within a sale.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; set; }
        public ProductSnapshot Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal DiscountPrice { get; private set; }

        /// <summary>
        /// Calculates the total value for the sale item based on quantity and product price.
        /// </summary>
        public decimal TotalAmount => (Quantity * DiscountPrice);
        protected SaleItem() { }

        public SaleItem(Guid saleId, ProductSnapshot product, int quantity)
        {
            SaleId = saleId;
            Product = product;
            Quantity = quantity;
        }

        /// <summary>
        /// Updates the quantity of the sale item.
        /// </summary>
        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }

        /// <summary>
        /// Updates the discount of the sale item.
        /// </summary>
        public void UpdateDiscount(decimal discount)
        {
            DiscountPrice = discount;
        }

        public static SaleItem CreateSaleItem(Guid saleId, ProductSnapshot product, int quantity)
        {
            return new SaleItem(
                saleId, 
                product,
                quantity
            );
        }

        public static SaleItem CreateSaleItem(SaleItem saleItem)
        {
            return new SaleItem(
                saleItem.SaleId,
                saleItem.Product,
                saleItem.Quantity
            );
        }
    }
}
