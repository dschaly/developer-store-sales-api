using Ambev.DeveloperEvaluation.Application.Products.GetProductCategory;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class GetProductCategoryHandlerTestData
{
    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static GetProductCategoryCommand GenerateValidCommand()
    {
        return new GetProductCategoryCommand();
    }

    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// </summary>
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Category, f => f.Commerce.Categories(1)[0])
        .RuleFor(p => p.Price, f => f.Finance.Amount(1, 1000))
        .RuleFor(p => p.CreatedBy, f => f.Person.UserName)
        .RuleFor(p => p.CreatedAt, f => f.Date.Past(1))
        .RuleFor(p => p.UpdatedAt, f => f.Date.Past(1).OrNull(f))
        .RuleFor(p => p.UpdatedBy, f => f.Person.UserName.OrNull(f));

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static IEnumerable<Product> GenerateValidProducts()
    {
        return [ProductFaker.Generate()];
    }
}
