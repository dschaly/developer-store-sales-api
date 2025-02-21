using Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Branches;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class DeleteBranchHandlerTestData
{
    /// <summary>
    /// Generates a valid Branch entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static DeleteBranchCommand GenerateValidCommand()
    {
        return new DeleteBranchCommand(Guid.NewGuid());
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
    /// Generates a valid Branch entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static Branch GenerateValidBranch()
    {
        return BranchFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Branch entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static DeleteBranchCommand GenerateInvalidCommand()
    {
        return new DeleteBranchCommand(Guid.Empty);
    }

    /// <summary>
    /// Generates a valid Branch entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static DeleteBranchResult GenerateValidResult()
    {
        return new DeleteBranchResult { Success = true };
    }
}