using Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Branches;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UpdateBranchHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Branch entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using Company names)
    /// - Address (using full address)
    /// - CreatedAt (using past dates)
    /// - CreatedBy (using internet usernames)
    /// </summary>
    private static readonly Faker<UpdateBranchCommand> UpdateBranchHandlerFaker = new Faker<UpdateBranchCommand>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Address, f => f.Address.FullAddress());

    /// <summary>
    /// Generates a valid Branch entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static UpdateBranchCommand GenerateValidCommand()
    {
        return UpdateBranchHandlerFaker.Generate();
    }

    /// <summary>
    /// Configures the Faker to generate valid Branch entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using Company names)
    /// - Address (using full address)
    /// - CreatedAt (using past dates)
    /// - CreatedBy (using internet usernames)
    /// </summary>
    private static readonly Faker<Branch> BranchFaker = new Faker<Branch>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Address, f => f.Address.FullAddress())
        .RuleFor(b => b.CreatedAt, f => f.Date.Past())
        .RuleFor(b => b.CreatedBy, f => f.Person.FullName);

    /// <summary>
    /// Configures the Faker to generate valid Branch entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using Company names)
    /// - Address (using full address)
    /// - CreatedAt (using past dates)
    /// - CreatedBy (using internet usernames)
    /// </summary>
    private static readonly Faker<UpdateBranchResult> UpdateBranchResultFaker = new Faker<UpdateBranchResult>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(b => b.Name, f => f.Company.CompanyName())
        .RuleFor(b => b.Address, f => f.Address.FullAddress())
        .RuleFor(b => b.CreatedAt, f => f.Date.Past())
        .RuleFor(b => b.CreatedBy, f => f.Person.FullName);

    /// <summary>
    /// Generates a valid Branch entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static UpdateBranchResult GenerateValidResult()
    {
        return UpdateBranchResultFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Branch entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static Branch GenerateExistingBranch()
    {
        return BranchFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Branch entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static Branch GenerateUpdatedBranch()
    {
        return BranchFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Guid Id name using Faker.
    /// The generated name will:
    /// - Follow the Guid format
    /// </summary>
    /// <returns>A valid Guid.</returns>
    public static UpdateBranchCommand GenerateInvalidCommand()
    {
        return new UpdateBranchCommand();
    }

    /// <summary>
    /// Generates an invalid branch with missing or incorrect data.
    /// </summary>
    /// <returns>An invalid Branch entity.</returns>
    public static UpdateBranchCommand GenerateInvalidBranch()
    {
        return new UpdateBranchCommand
        {
            Id = Guid.Empty, // Invalid ID
        };
    }
}
