using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for the Customer entity using the Bogus library.
/// This class centralizes all test data generation to ensure consistency across test cases
/// and provide both valid and invalid data scenarios.
/// </summary>
public static class CustomerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Customer entities.
    /// The generated customers will have valid:
    /// - Name (using random names)
    /// - Email (valid format)
    /// - CreatedBy (random username)
    /// - CreatedAt (current date and time)
    /// - UpdatedBy (optional)
    /// - UpdatedAt (optional)
    /// </summary>
    private static readonly Faker<Customer> CustomerFaker = new Faker<Customer>()
        .RuleFor(c => c.Name, f => f.Name.FullName())
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.CreatedBy, f => f.Internet.UserName())
        .RuleFor(c => c.CreatedAt, f => f.Date.Past(1))
        .RuleFor(c => c.UpdatedAt, f => f.Date.Between(f.Date.Past(1), DateTime.Now).OrNull(f))
        .RuleFor(c => c.UpdatedBy, f => f.Internet.UserName().OrNull(f));

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static Customer GenerateValidCustomer()
    {
        return CustomerFaker.Generate();
    }

    /// <summary>
    /// Generates a valid email address using Faker.
    /// The generated email will:
    /// - Follow the standard email format (user@domain.com)
    /// - Have valid characters in both local and domain parts
    /// - Have a valid TLD
    /// </summary>
    /// <returns>A valid email address.</returns>
    public static string GenerateValidEmail()
    {
        return new Faker().Internet.Email();
    }

    /// <summary>
    /// Generates a valid name using Faker.
    /// The generated name will:
    /// - Be a random full name
    /// </summary>
    /// <returns>A valid name.</returns>
    public static string GenerateValidName()
    {
        return new Faker().Name.FullName();
    }

    /// <summary>
    /// Generates a valid created by username using Faker.
    /// The generated username will:
    /// - Follow internet username conventions
    /// </summary>
    /// <returns>A valid created by username.</returns>
    public static string GenerateValidCreatedBy()
    {
        return new Faker().Internet.UserName();
    }

    /// <summary>
    /// Generates a valid created at date using Faker.
    /// The generated date will:
    /// - Be a random date from the past 1 year
    /// </summary>
    /// <returns>A valid created at date.</returns>
    public static DateTime GenerateValidCreatedAt()
    {
        return new Faker().Date.Past(1);
    }

    /// <summary>
    /// Generates an invalid email address for testing negative scenarios.
    /// The generated email will:
    /// - Not follow the standard email format
    /// - Not contain the @ symbol
    /// - Be a simple word or string
    /// This is useful for testing email validation error cases.
    /// </summary>
    /// <returns>An invalid email address.</returns>
    public static string GenerateInvalidEmail()
    {
        return new Faker().Lorem.Word();
    }

    /// <summary>
    /// Generates an invalid name for testing negative scenarios.
    /// The generated name will:
    /// - Be empty or a very short string
    /// </summary>
    /// <returns>An invalid name.</returns>
    public static string GenerateInvalidName()
    {
        return string.Empty;
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