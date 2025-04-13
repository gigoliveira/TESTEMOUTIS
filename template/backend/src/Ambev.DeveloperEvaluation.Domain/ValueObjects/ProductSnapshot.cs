using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class ProductSnapshot
    {
        public Guid ExternalProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal Price { get; private set; }

        public ProductSnapshot()
        {
            
        }
        public ProductSnapshot(Guid externalProductId, string productName, decimal price)
        {
            ExternalProductId = externalProductId;
            ProductName = productName;
            Price = price;
        }
    }
}
