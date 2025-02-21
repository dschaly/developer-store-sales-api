using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateProductHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated users will have valid:
    /// - Title (using random product names)
    /// - Price (random decimal within a reasonable range)
    /// - Description (random description)
    /// - Category (random category name)
    /// - Image (random image URL)
    /// - Rating (random rating with valid values)
    /// </summary>
    private static readonly Faker<CreateProductCommand> createProductHandlerFaker = new Faker<CreateProductCommand>()
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(min: 1.00m, max: 1000.00m))) // Price between 1 and 1000
        .RuleFor(p => p.Description, f => f.Lorem.Sentence(10))
        .RuleFor(p => p.Category, f => f.Commerce.Department())
        .RuleFor(p => p.Image, f => f.Internet.Url())
        .RuleFor(p => p.Rating, f => new Rating(f.Random.Int(1, 5), f.Random.Int(0, 100))); // Random rating between 1 and 5, with a random number of reviews

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static CreateProductCommand GenerateValidCommand()
    {
        return createProductHandlerFaker.Generate();
    }
}