using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Validation.ProductSnapshotValidations;
using FluentValidation;

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
            this.Validate();
        }
        public static ProductSnapshot Create(Guid externalProductId, string productName, decimal price)
        {
            var productSnapshot = new ProductSnapshot(externalProductId, productName, price);
            productSnapshot.Validate(); 

            return productSnapshot;
        }

        public void Validate()
        {
            var validator = new ProductSnapshotValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                var errorMessages = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errorMessages}");
            }
        }
    }
}
