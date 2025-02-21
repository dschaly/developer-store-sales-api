using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class GetSaleHandlerTestData
{
    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static GetSaleCommand GenerateValidCommand()
    {
        return new GetSaleCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated branch will have valid:
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, Guid.NewGuid())
        .RuleFor(s => s.SaleNumber, Guid.NewGuid().ToString())
        .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(1, 1000))
        .RuleFor(s => s.IsCancelled, f => f.Random.Bool())
        .RuleFor(s => s.CustomerId, Guid.NewGuid())
        .RuleFor(s => s.BranchId, Guid.NewGuid())
        .RuleFor(s => s.SaleItems, SaleTestData.GenerateValidSaleItems())
        .RuleFor(s => s.CreatedBy, f => f.Person.UserName)
        .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
        .RuleFor(s => s.UpdatedAt, f => f.Date.Past(1).OrNull(f))
        .RuleFor(s => s.UpdatedBy, f => f.Person.UserName.OrNull(f));

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// </summary>
    private static readonly GetSaleItemResult GetSaleItemResultFaker = new Faker<GetSaleItemResult>()
        .RuleFor(s => s.Id, Guid.NewGuid())
        .RuleFor(s => s.ProductId, Guid.NewGuid())
        .RuleFor(s => s.Product, ProductTestData.GenerateValidProduct())
        .RuleFor(s => s.Quantity, f => f.Random.Int(1, 100))
        .RuleFor(s => s.Discount, f => f.Random.Decimal(0, 50))
        .RuleFor(s => s.TotalAmount, (f) => f.Random.Decimal(10m, 1000m))
        .RuleFor(s => s.SaleId, Guid.NewGuid())
        .RuleFor(s => s.CreatedBy, f => f.Internet.UserName())
        .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
        .RuleFor(s => s.UpdatedAt, f => f.Date.Recent(1))
        .RuleFor(s => s.UpdatedBy, f => f.Internet.UserName().OrNull(f))
        .Generate();

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static GetSaleResult GenerateValidResult()
    {
        return new Faker<GetSaleResult>()
            .RuleFor(s => s.Id, Guid.NewGuid())
            .RuleFor(s => s.SaleNumber, Guid.NewGuid().ToString())
            .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(1, 1000))
            .RuleFor(s => s.IsCancelled, f => f.Random.Bool())
            .RuleFor(s => s.CustomerId, Guid.NewGuid())
            .RuleFor(s => s.BranchId, Guid.NewGuid())
            .RuleFor(s => s.SaleItems, [GetSaleItemResultFaker])
            .RuleFor(s => s.CreatedBy, f => f.Person.UserName)
            .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
            .RuleFor(s => s.UpdatedAt, f => f.Date.Past(1).OrNull(f))
            .RuleFor(s => s.UpdatedBy, f => f.Person.UserName.OrNull(f))
            .Generate();
    }
}