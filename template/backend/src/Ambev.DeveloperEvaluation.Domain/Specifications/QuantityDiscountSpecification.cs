namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class DiscountAvailableSpecification : ISpecification<int>
{
    public bool IsSatisfiedBy(int quantity)
    {
        return quantity >= 4;
    }

}

public class MaxQuantitySpecification : ISpecification<int>
{
    public bool IsSatisfiedBy(int quantity)
    {
        return quantity <= 20;
    }
}