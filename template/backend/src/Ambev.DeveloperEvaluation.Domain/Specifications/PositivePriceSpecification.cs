using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public class PositivePriceSpecification : ISpecification<SaleItem>
    {
        public bool IsSatisfiedBy(SaleItem saleItem)
        {
            return saleItem.Product.ProductPrice > 0;
        }
    }
}
