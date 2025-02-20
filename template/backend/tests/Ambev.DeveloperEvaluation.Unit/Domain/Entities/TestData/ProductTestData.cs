using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for the Product entity using the Bogus library.
/// This class centralizes all test data generation to ensure consistency across test cases
/// and provide both valid and invalid data scenarios.
/// </summary>
public static class ProductTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated products will have valid:
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
    /// The generated product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    /// <summary>
    /// Generates a valid product title using Faker.
    /// The generated title will:
    /// - Be a random product name
    /// </summary>
    /// <returns>A valid product title.</returns>
    public static string GenerateValidTitle()
    {
        return new Faker().Commerce.ProductName();
    }

    /// <summary>
    /// Generates a valid product price using Faker.
    /// The generated price will:
    /// - Be a random decimal between 1 and 1000
    /// </summary>
    /// <returns>A valid product price.</returns>
    public static decimal GenerateValidPrice()
    {
        return decimal.Parse(new Faker().Commerce.Price(1.00m, 1000.00m));
    }

    /// <summary>
    /// Generates a valid product description using Faker.
    /// The generated description will:
    /// - Be a random sentence with 10 words
    /// </summary>
    /// <returns>A valid product description.</returns>
    public static string GenerateValidDescription()
    {
        return new Faker().Lorem.Sentence(10);
    }

    /// <summary>
    /// Generates a valid product category using Faker.
    /// The generated category will:
    /// - Be a random department name
    /// </summary>
    /// <returns>A valid product category.</returns>
    public static string GenerateValidCategory()
    {
        return new Faker().Commerce.Department();
    }

    /// <summary>
    /// Generates a valid product image URL using Faker.
    /// The generated image will:
    /// - Be a random URL pointing to an image
    /// </summary>
    /// <returns>A valid product image URL.</returns>
    public static string GenerateValidImage()
    {
        return new Faker().Internet.Url();
    }

    /// <summary>
    /// Generates a valid product rating using Faker.
    /// The generated rating will:
    /// - Have a random value between 1 and 5
    /// - Have a random number of reviews (0 to 100)
    /// </summary>
    /// <returns>A valid product rating.</returns>
    public static Rating GenerateValidRating()
    {
        return new Rating(new Faker().Random.Int(1, 5), new Faker().Random.Int(0, 100));
    }

    /// <summary>
    /// Generates an invalid product title for testing negative scenarios.
    /// The generated title will:
    /// - Be an empty string
    /// </summary>
    /// <returns>An invalid product title.</returns>
    public static string GenerateInvalidTitle()
    {
        return string.Empty;
    }

    /// <summary>
    /// Generates an invalid product price for testing negative scenarios.
    /// The generated price will:
    /// - Be a negative value
    /// </summary>
    /// <returns>An invalid product price.</returns>
    public static decimal GenerateInvalidPrice()
    {
        return -1.00m;
    }

    /// <summary>
    /// Generates an invalid product description for testing negative scenarios.
    /// The generated description will:
    /// - Be an empty string
    /// </summary>
    /// <returns>An invalid product description.</returns>
    public static string GenerateInvalidDescription()
    {
        return string.Empty;
    }

    /// <summary>
    /// Generates an invalid product category for testing negative scenarios.
    /// The generated category will:
    /// - Be an empty string
    /// </summary>
    /// <returns>An invalid product category.</returns>
    public static string GenerateInvalidCategory()
    {
        return string.Empty;
    }

    /// <summary>
    /// Generates an invalid product image URL for testing negative scenarios.
    /// The generated image URL will:
    /// - Not be a valid URL (e.g., missing protocol)
    /// </summary>
    /// <returns>An invalid product image URL.</returns>
    public static string GenerateInvalidImage()
    {
        return "invalid-url";
    }

    /// <summary>
    /// Generates an invalid product rating for testing negative scenarios.
    /// The generated rating will:
    /// - Have an invalid value (e.g., less than 1 or greater than 5)
    /// </summary>
    /// <returns>An invalid product rating.</returns>
    public static Rating GenerateInvalidRating()
    {
        return new Rating(0, 0); // Invalid rating
    }

    /// <summary>
    /// Generates a long CreatedBy value for testing validation error cases.
    /// The generated username will:
    /// - Be longer than allowed
    /// </summary>
    /// <returns>A long CreatedBy value.</returns>
    public static string GenerateLongCreatedBy()
    {
        return new Faker().Random.String2(101); // Exceeds typical username length
    }

    /// <summary>
    /// Generates an invalid CreatedAt date for testing validation error cases.
    /// The generated date will:
    /// - Be in the future
    /// </summary>
    /// <returns>An invalid CreatedAt date.</returns>
    public static DateTime GenerateInvalidCreatedAt()
    {
        return new Faker().Date.Future();
    }

    /// <summary>
    /// Generates an invalid UpdatedBy value for testing validation error cases.
    /// The generated value will:
    /// - Be a random string
    /// </summary>
    /// <returns>An invalid UpdatedBy value.</returns>
    public static string GenerateInvalidUpdatedBy()
    {
        return new Faker().Random.String(5);
    }
}
