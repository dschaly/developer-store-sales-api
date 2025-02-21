using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Customers;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateCustomerHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Customer entities.
    /// The generated users will have valid:
    /// - Name (using random names)
    /// - Email (valid format)
    /// </summary>
    private static readonly Faker<CreateCustomerCommand> createCustomerHandlerFaker = new Faker<CreateCustomerCommand>()
        .RuleFor(u => u.Name, f => f.Name.FirstName())
        .RuleFor(u => u.Email, f => f.Internet.Email());

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static CreateCustomerCommand GenerateValidCommand()
    {
        return createCustomerHandlerFaker.Generate();
    }
}