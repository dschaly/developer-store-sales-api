using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Users;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ListUserHandlerTestData
{
    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static ListUserCommand GenerateValidCommand()
    {
        return ListUserCommandFaker.Generate();
    }

    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// </summary>
    private static readonly Faker<ListUserCommand> ListUserCommandFaker = new Faker<ListUserCommand>()
        .RuleFor(b => b.Page, f => f.Random.Int(1, 10))
        .RuleFor(b => b.Size, f => f.Random.Int(1, 10))
        .RuleFor(c => c.Order, "");

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
    /// Configures the Faker to generate valid User entities.
    /// </summary>
    private static readonly Faker<UserResult> UserResultFaker = new Faker<UserResult>()
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

    /// <summary>
    /// Configures the Faker to generate valid User entities.
    /// </summary>
    private static readonly Faker<ListUserResult> ListUserResultFaker = new Faker<ListUserResult>()
        .RuleFor(b => b.PageSize, f => f.Random.Int(1, 10))
        .RuleFor(b => b.CurrentPage, f => f.Random.Int(1, 10))
        .RuleFor(b => b.TotalCount, f => f.Random.Int(1, 10))
        .RuleFor(b => b.TotalItems, f => f.Random.Int(1, 10))
        .RuleFor(b => b.TotalPages, f => f.Random.Int(1, 10))
        .RuleFor(c => c.Data, [UserResultFaker.Generate()]);

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static ListUserResult GenerateValidResult()
    {
        return ListUserResultFaker.Generate();
    }

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static List<UserResult> GenerateMappedUserList()
    {
        return [UserResultFaker.Generate()];
    }

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static ListUserCommand GenerateInvalidCommand()
    {
        return new ListUserCommand();
    }

    /// <summary>
    /// Generates a valid User entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid User entity with randomly generated data.</returns>
    public static List<User> GenerateUserList()
    {
        return [GenerateValidUser()];
    }
}