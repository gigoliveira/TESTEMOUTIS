using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.CustomerSnapshotValidations
{
    public class CustomerSnapshotValidator : AbstractValidator<CustomerSnapshot>
    {
        public CustomerSnapshotValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Customer name cannot be empty");

        }
    }
}
