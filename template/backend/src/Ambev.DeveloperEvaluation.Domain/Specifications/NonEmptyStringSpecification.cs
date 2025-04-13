namespace Ambev.DeveloperEvaluation.Domain.Specifications
{
    public class NonEmptyStringSpecification
    {
        public bool IsSatisfiedBy(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
