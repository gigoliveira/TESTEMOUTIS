using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation.ValueObjects.TestData
{
    public static class ProductSnapshotValidatorTestData
    {
        private static readonly Faker<ProductSnapshot> ProductFaker = new Faker<ProductSnapshot>()
            .RuleFor(u => u.ProductId, f => f.Commerce.Product())
            .RuleFor(u => u.ProductName, f => f.Commerce.ProductName())
            .RuleFor(u => u.Price, f => f.Finance.Amount(0.01m, 10000.00m));

        public static ProductSnapshot GenerateValidProduct()
        {
            return ProductFaker.Generate();
        }

        public static ProductSnapshot GenerateInvalidPriceProduct()
        {
            return ProductFaker.Clone()
                .RuleFor(u => u.Price, -1)
                .Generate();
        }

        public static ProductSnapshot GenerateInvalidNameProduct()
        {
            return ProductFaker.Clone()
                .RuleFor(u => u.ProductName, "")
                .Generate();
        }
    }
}

