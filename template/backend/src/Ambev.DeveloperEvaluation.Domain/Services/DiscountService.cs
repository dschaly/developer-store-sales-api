using Ambev.DeveloperEvaluation.Domain.Specifications;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public class DiscountService : IDiscountService
{
    private readonly ISpecification<int> _discountAvailableSpecification;
    private readonly ISpecification<int> _maxQuantitySpecification;

    public DiscountService()
    {
        _discountAvailableSpecification = new DiscountAvailableSpecification();
        _maxQuantitySpecification = new MaxQuantitySpecification();
    }

    public decimal CalculateDiscount(int quantity, decimal unitPrice)
    {
        if (!_maxQuantitySpecification.IsSatisfiedBy(quantity))
        {
            throw new InvalidOperationException("It's not possible to buy more than 20 of the same item.");
        }

        if (!_discountAvailableSpecification.IsSatisfiedBy(quantity))
        {
            return 0;
        }

        if (quantity >= 4 && quantity <= 9)
        {
            return unitPrice * quantity * 0.10m;
        }
        else if (quantity >= 10 && quantity <= 20)
        {
            return unitPrice * quantity * 0.20m;
        }

        return 0;
    }
}