using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Customers;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteCustomerHandlerTestData
{
    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static DeleteCustomerCommand GenerateValidCommand()
    {
        return new DeleteCustomerCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Configures the Faker to generate valid Customer entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using Company names)
    /// - Address (using full address)
    /// - CreatedAt (using past dates)
    /// - CreatedBy (using internet usernames)
    /// </summary>
    private static readonly Faker<Customer> CustomerFaker = new Faker<Customer>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(c => c.Name, f => f.Name.FullName())
        .RuleFor(c => c.Email, f => f.Internet.Email())
        .RuleFor(c => c.CreatedBy, f => f.Internet.UserName())
        .RuleFor(c => c.CreatedAt, f => f.Date.Past(1))
        .RuleFor(c => c.UpdatedAt, f => f.Date.Between(f.Date.Past(1), DateTime.Now).OrNull(f))
        .RuleFor(c => c.UpdatedBy, f => f.Internet.UserName().OrNull(f));

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static Customer GenerateValidCustomer()
    {
        return CustomerFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static DeleteCustomerCommand GenerateInvalidCommand()
    {
        return new DeleteCustomerCommand(Guid.Empty);
    }

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static DeleteCustomerResponse GenerateValidResult()
    {
        return new DeleteCustomerResponse { Success = true };
    }
}