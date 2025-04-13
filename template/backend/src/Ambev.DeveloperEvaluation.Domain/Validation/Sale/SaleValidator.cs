using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Aggregates;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.SaleValidations
{
    public class SaleValidator
    {
        // Validate the sale aggregate
        public void Validate(Sale sale)
        {
            if (sale.Items == null || sale.Items.Count == 0)
                throw new ValidationException("Sale must contain at least one item.");
        }
    }
}
