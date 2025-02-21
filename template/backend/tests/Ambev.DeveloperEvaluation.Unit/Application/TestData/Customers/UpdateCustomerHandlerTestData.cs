using Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Customers;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UpdateCustomerHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Customer entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using random names)
    /// - Email (valid format)
    /// - CreatedBy (random username)
    /// - CreatedAt (current date and time)
    /// - UpdatedBy (optional)
    /// - UpdatedAt (optional)
    /// </summary>
    private static readonly Faker<UpdateCustomerCommand> UpdateCustomerHandlerFaker = new Faker<UpdateCustomerCommand>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Email, f => f.Internet.Email());

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static UpdateCustomerCommand GenerateValidCommand()
    {
        return UpdateCustomerHandlerFaker.Generate();
    }

    /// <summary>
    /// Configures the Faker to generate valid Customer entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using random names)
    /// - Email (valid format)
    /// - CreatedBy (random username)
    /// - CreatedAt (current date and time)
    /// - UpdatedBy (optional)
    /// - UpdatedAt (optional)
    /// </summary>
    private static readonly Faker<Customer> CustomerFaker = new Faker<Customer>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Email, f => f.Internet.Email())
        .RuleFor(b => b.CreatedAt, f => f.Date.Past())
        .RuleFor(b => b.CreatedBy, f => f.Person.FullName)
        .RuleFor(b => b.UpdatedAt, f => f.Date.Past())
        .RuleFor(b => b.UpdatedBy, f => f.Person.FullName);

    /// <summary>
    /// Configures the Faker to generate valid Customer entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using random names)
    /// - Email (valid format)
    /// - CreatedBy (random username)
    /// - CreatedAt (current date and time)
    /// - UpdatedBy (optional)
    /// - UpdatedAt (optional)
    /// </summary>
    private static readonly Faker<UpdateCustomerResult> UpdateCustomerResultFaker = new Faker<UpdateCustomerResult>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Email, f => f.Internet.Email())
        .RuleFor(b => b.CreatedAt, f => f.Date.Past())
        .RuleFor(b => b.CreatedBy, f => f.Person.FullName)
        .RuleFor(b => b.UpdatedAt, f => f.Date.Past())
        .RuleFor(b => b.UpdatedBy, f => f.Person.FullName);

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static UpdateCustomerResult GenerateValidResult()
    {
        return UpdateCustomerResultFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static Customer GenerateExistingCustomer()
    {
        return CustomerFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Customer entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Customer entity with randomly generated data.</returns>
    public static Customer GenerateUpdatedCustomer()
    {
        return CustomerFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Guid Id name using Faker.
    /// The generated name will:
    /// - Follow the Guid format
    /// </summary>
    /// <returns>A valid Guid.</returns>
    public static UpdateCustomerCommand GenerateInvalidCommand()
    {
        return new UpdateCustomerCommand();
    }

    /// <summary>
    /// Generates an invalid branch with missing or incorrect data.
    /// </summary>
    /// <returns>An invalid Customer entity.</returns>
    public static UpdateCustomerCommand GenerateInvalidCustomer()
    {
        return new UpdateCustomerCommand
        {
            Id = Guid.Empty, // Invalid ID
        };
    }
}
