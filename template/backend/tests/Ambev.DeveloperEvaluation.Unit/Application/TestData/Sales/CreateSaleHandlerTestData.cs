using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateSaleHandlerTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated users will have valid:
    /// </summary>
    private static readonly Faker<CreateSaleCommand> createSaleHandlerFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.CustomerId, Guid.NewGuid())
        .RuleFor(s => s.BranchId, Guid.NewGuid())
        .RuleFor(s => s.SaleItems, GenerateValidSaleItems());

    public static Sale GenerateValidSale()
    {
        return new Faker<Sale>()
        .RuleFor(s => s.Id, Guid.NewGuid())
        .RuleFor(s => s.SaleNumber, Guid.NewGuid().ToString())
        .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(1, 1000))
        .RuleFor(s => s.IsCancelled, f => f.Random.Bool())
        .RuleFor(s => s.CustomerId, Guid.NewGuid())
        .RuleFor(s => s.BranchId, Guid.NewGuid())
        .RuleFor(s => s.SaleItems, GenerateValidSaleItems())
        .RuleFor(s => s.CreatedBy, f => f.Person.UserName)
        .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
        .RuleFor(s => s.UpdatedAt, f => f.Date.Past(1).OrNull(f))
        .RuleFor(s => s.UpdatedBy, f => f.Person.UserName.OrNull(f));
    }

    /// <summary>
    /// Generates a valid Entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Entity with randomly generated data.</returns>
    public static List<SaleItem> GenerateValidSaleItems()
    {
        var saleItem = new Faker<SaleItem>()
            .RuleFor(s => s.ProductId, Guid.NewGuid()) // Generate a random GUID for ProductId
            .RuleFor(s => s.Quantity, f => f.Random.Int(1, 100)); // Generate a random quantity between 1 and 100

        var validSaleItems = new List<SaleItem> { saleItem.Generate(), saleItem.Generate() };

        return validSaleItems;
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return createSaleHandlerFaker.Generate();
    }
}