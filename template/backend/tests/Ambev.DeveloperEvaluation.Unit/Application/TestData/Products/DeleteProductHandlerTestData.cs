using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteProductHandlerTestData
{
    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static DeleteProductCommand GenerateValidCommand()
    {
        return new DeleteProductCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Title (using random product names)
    /// - Price (random decimal within a reasonable range)
    /// - Description (random description)
    /// - Category (random category name)
    /// - Image (random image URL)
    /// - Rating (random rating with valid values)
    /// - CreatedBy (random username)
    /// - CreatedAt (current date and time)
    /// - UpdatedBy (optional)
    /// - UpdatedAt (optional)
    /// </summary>
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(min: 1.00m, max: 1000.00m))) // Price between 1 and 1000
        .RuleFor(p => p.Description, f => f.Lorem.Sentence(10))
        .RuleFor(p => p.Category, f => f.Commerce.Department())
        .RuleFor(p => p.Image, f => f.Internet.Url())
        .RuleFor(p => p.Rating, f => new Rating(f.Random.Int(1, 5), f.Random.Int(0, 100))) // Random rating between 1 and 5, with a random number of reviews
        .RuleFor(p => p.CreatedBy, f => f.Internet.UserName())
        .RuleFor(p => p.CreatedAt, f => f.Date.Past(1))
        .RuleFor(p => p.UpdatedAt, f => f.Date.Between(f.Date.Past(1), DateTime.Now).OrNull(f))
        .RuleFor(p => p.UpdatedBy, f => f.Internet.UserName().OrNull(f));

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static DeleteProductCommand GenerateInvalidCommand()
    {
        return new DeleteProductCommand(Guid.Empty);
    }

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static DeleteProductResponse GenerateValidResult()
    {
        return new DeleteProductResponse { Success = true };
    }
}