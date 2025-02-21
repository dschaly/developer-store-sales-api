using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Branches;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateBranchHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Branch entities.
    /// The generated users will have valid:
    /// - Name (using Company names)
    /// - Address (using full address)
    /// </summary>
    private static readonly Faker<CreateBranchCommand> createBranchHandlerFaker = new Faker<CreateBranchCommand>()
        .RuleFor(u => u.Name, f => f.Company.CompanyName())
        .RuleFor(u => u.Address, f => f.Address.FullAddress());

    /// <summary>
    /// Generates a valid Branch entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Branch entity with randomly generated data.</returns>
    public static CreateBranchCommand GenerateValidCommand()
    {
        return createBranchHandlerFaker.Generate();
    }
}