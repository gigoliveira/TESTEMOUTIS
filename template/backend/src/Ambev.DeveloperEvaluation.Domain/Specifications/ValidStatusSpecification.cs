using Ambev.DeveloperEvaluation.Domain.Aggregates;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public class ValidStatusSpecification : ISpecification<Sale>
    {
        public bool IsSatisfiedBy(Sale sale)
        {
            // Assuming SaleStatus is an enum, valid statuses are only certain ones
            return sale.Status == SaleStatus.Pending || sale.Status == SaleStatus.Completed;
        }
    }
}
