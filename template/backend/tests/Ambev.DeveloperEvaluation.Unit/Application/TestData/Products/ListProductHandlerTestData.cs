using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Products;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ListProductHandlerTestData
{
    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static ListProductCommand GenerateValidCommand()
    {
        return ListProductCommandFaker.Generate();
    }

    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using random names)
    /// - Email (valid format)
    /// - CreatedBy (random username)
    /// - CreatedAt (current date and time)
    /// - UpdatedBy (optional)
    /// - UpdatedAt (optional)
    /// </summary>
    private static readonly Faker<ListProductCommand> ListProductCommandFaker = new Faker<ListProductCommand>()
        .RuleFor(b => b.Page, f => f.Random.Int(1, 10))
        .RuleFor(b => b.Size, f => f.Random.Int(1, 10))
        .RuleFor(c => c.Order, "")
        .RuleFor(c => c.Title, "")
        .RuleFor(c => c.Category, "");

    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using random names)
    /// - Email (valid format)
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
    /// Configures the Faker to generate valid Product entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using random names)
    /// - Email (valid format)
    /// - CreatedBy (random username)
    /// - CreatedAt (current date and time)
    /// - UpdatedBy (optional)
    /// - UpdatedAt (optional)
    /// </summary>
    private static readonly Faker<ProductResult> ProductResultFaker = new Faker<ProductResult>()
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
    /// Configures the Faker to generate valid Product entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using random names)
    /// - Email (valid format)
    /// - CreatedBy (random username)
    /// - CreatedAt (current date and time)
    /// - UpdatedBy (optional)
    /// - UpdatedAt (optional)
    /// </summary>
    private static readonly Faker<ListProductResult> ListProductResultFaker = new Faker<ListProductResult>()
        .RuleFor(b => b.PageSize, f => f.Random.Int(1, 10))
        .RuleFor(b => b.CurrentPage, f => f.Random.Int(1, 10))
        .RuleFor(b => b.TotalCount, f => f.Random.Int(1, 10))
        .RuleFor(b => b.TotalItems, f => f.Random.Int(1, 10))
        .RuleFor(b => b.TotalPages, f => f.Random.Int(1, 10))
        .RuleFor(c => c.Data, [ProductResultFaker.Generate()]);

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static ListProductResult GenerateValidResult()
    {
        return ListProductResultFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static List<ProductResult> GenerateMappedProductList()
    {
        return [ProductResultFaker.Generate()];
    }

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static ListProductCommand GenerateInvalidCommand()
    {
        return new ListProductCommand();
    }

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static List<Product> GenerateProductList()
    {
        return [GenerateValidProduct()];
    }
}