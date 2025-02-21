using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Auth;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class AuthenticateHandlerTestData
{
    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static AuthenticateUserCommand GenerateValidCommand()
    {
        return AuthenticateUserCommandFaker.Generate();
    }

    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// </summary>
    private static readonly Faker<AuthenticateUserCommand> AuthenticateUserCommandFaker = new Faker<AuthenticateUserCommand>()
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}");

    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// </summary>
    private static readonly Faker<User> UserFaker = new Faker<User>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
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
}
