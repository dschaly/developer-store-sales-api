using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CancelSaleHandlerTestData
{
    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static CancelSaleCommand GenerateValidCommand()
    {
        return new CancelSaleCommand(Guid.NewGuid());
    }

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated branch will have valid:
    /// - Id (using random GUID)
    /// - Title (using random product names)
    /// - Price (random decimal within a reasonable range)
    /// - Description (random description)
    /// - Category (random category name)
    /// - Image (random image URL)
    /// - Rating (random rating with valid values)
    /// - CreatedBy (random username)
    /// - CreatedAt (current date and time)
    /// - UpdatedBy (optional)
    /// - UpdatedAt (optional)
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(s => s.SaleNumber, Guid.NewGuid().ToString())
        .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(1, 1000))
        .RuleFor(s => s.IsCancelled, false)
        .RuleFor(s => s.CustomerId, Guid.NewGuid())
        .RuleFor(s => s.BranchId, Guid.NewGuid())
        .RuleFor(s => s.SaleItems, GenerateValidSaleItems())
        .RuleFor(s => s.CreatedBy, f => f.Person.UserName)
        .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
        .RuleFor(s => s.UpdatedAt, f => f.Date.Past(1).OrNull(f))
        .RuleFor(s => s.UpdatedBy, f => f.Person.UserName.OrNull(f));

    public static List<SaleItem> GenerateValidSaleItems()
    {
        var saleItem = new Faker<SaleItem>()
                .RuleFor(s => s.ProductId, Guid.NewGuid())
                .RuleFor(s => s.Product, ProductTestData.GenerateValidProduct())
                .RuleFor(s => s.Quantity, f => f.Random.Int(1, 100))
                .RuleFor(s => s.Discount, f => f.Random.Decimal(0, 50))
                .RuleFor(s => s.TotalAmount, (f, s) => Math.Round((f.Random.Decimal(10, 100) * s.Quantity) - s.Discount, 2))
                .RuleFor(s => s.SaleId, Guid.NewGuid())
                .RuleFor(s => s.CreatedBy, f => f.Internet.UserName())
                .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
                .RuleFor(s => s.UpdatedAt, f => f.Date.Recent(1))
                .RuleFor(s => s.UpdatedBy, f => f.Internet.UserName().OrNull(f));

        var validSaleItems = new List<SaleItem> { saleItem, saleItem };

        return validSaleItems;
    }

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
    public static CancelSaleCommand GenerateInvalidCommand()
    {
        return new CancelSaleCommand(Guid.Empty);
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static CancelSaleResponse GenerateValidResult()
    {
        return new CancelSaleResponse { Success = true };
    }
}