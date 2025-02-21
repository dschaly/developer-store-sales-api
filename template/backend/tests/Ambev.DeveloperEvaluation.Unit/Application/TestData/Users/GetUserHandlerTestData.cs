using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Users;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class GetUserHandlerTestData
{
    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static GetUserCommand GenerateValidCommand()
    {
        return new GetUserCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using Company names)
    /// - Address (using full address)
    /// - CreatedAt (using past dates)
    /// - CreatedBy (using internet usernames)
    /// </summary>
    private static readonly Faker<User> UserFaker = new Faker<User>()
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin));

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static User GenerateValidUser()
    {
        return UserFaker.Generate();
    }

    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Name (using Company names)
    /// - Address (using full address)
    /// - CreatedAt (using past dates)
    /// - CreatedBy (using internet usernames)
    /// </summary>
    private static readonly Faker<GetUserResult> UserResultFaker = new Faker<GetUserResult>()
        .RuleFor(u => u.Name, f => f.Internet.UserName())
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin));

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static GetUserResult GenerateValidResult()
    {
        return UserResultFaker.Generate();
    }
}