using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ListSaleHandlerTestData
{
    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static ListSaleCommand GenerateValidCommand()
    {
        return new ListSaleCommand();
    }

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated branch will have valid:
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
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

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// </summary>
    private static readonly Faker<SaleResult> SaleResultFaker = new Faker<SaleResult>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(s => s.SaleNumber, Guid.NewGuid().ToString())
        .RuleFor(s => s.TotalAmount, f => f.Finance.Amount(1, 1000))
        .RuleFor(s => s.IsCancelled, f => f.Random.Bool())
        .RuleFor(s => s.CustomerId, Guid.NewGuid())
        .RuleFor(s => s.BranchId, Guid.NewGuid())
        .RuleFor(s => s.SaleItems, GenerateValidSaleItemResult())
        .RuleFor(s => s.CreatedBy, f => f.Person.UserName)
        .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
        .RuleFor(s => s.UpdatedAt, f => f.Date.Past(1).OrNull(f))
        .RuleFor(s => s.UpdatedBy, f => f.Person.UserName.OrNull(f));

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
    /// Generates a valid Entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Entity with randomly generated data.</returns>
    public static List<SaleItem> GenerateValidSaleItems()
    {
        var saleItem = new Faker<SaleItem>()
            .RuleFor(s => s.ProductId, Guid.NewGuid()) // Generate a random GUID for ProductId
            .RuleFor(s => s.Product, ProductTestData.GenerateValidProduct()) // Generate a valid Product
            .RuleFor(s => s.Quantity, f => f.Random.Int(1, 100)) // Generate a random quantity between 1 and 100
            .RuleFor(s => s.Discount, f => f.Random.Decimal(0, 50)) // Generate a discount between 0 and 50
            .RuleFor(s => s.TotalAmount, (f, s) => Math.Round((f.Random.Decimal(10, 100) * s.Quantity) - s.Discount, 2)) // Calculate total amount based on quantity and discount
            .RuleFor(s => s.SaleId, Guid.NewGuid()) // Generate a random GUID for SaleId
            .RuleFor(s => s.CreatedBy, f => f.Internet.UserName()) // Generate a random username for CreatedBy
            .RuleFor(s => s.CreatedAt, f => f.Date.Past(1)) // Generate a random date within the past year
            .RuleFor(s => s.UpdatedAt, f => f.Date.Past(1)) // Generate a recent date for UpdatedAt
            .RuleFor(s => s.UpdatedBy, f => f.Internet.UserName().OrNull(f)); // Generate a random username for UpdatedBy or null

        var validSaleItems = new List<SaleItem> { saleItem, saleItem };

        return validSaleItems;
    }

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// </summary>
    private static readonly Faker<ListSaleResult> ListSaleResultFaker = new Faker<ListSaleResult>()
        .RuleFor(b => b.PageSize, f => f.Random.Int(1, 10))
        .RuleFor(b => b.CurrentPage, f => f.Random.Int(1, 10))
        .RuleFor(b => b.TotalCount, f => f.Random.Int(1, 10))
        .RuleFor(b => b.TotalItems, f => f.Random.Int(1, 10))
        .RuleFor(b => b.TotalPages, f => f.Random.Int(1, 10))
        .RuleFor(c => c.Data, [SaleResultFaker.Generate()]);

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// </summary>
    private static readonly ListSaleItemResult ListSaleItemResultFaker = new Faker<ListSaleItemResult>()
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
    public static ListSaleResult GenerateValidResult()
    {
        return ListSaleResultFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static List<ListSaleItemResult> GenerateValidSaleItemResult()
    {
        return [ListSaleItemResultFaker];
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static List<SaleResult> GenerateMappedSaleList()
    {
        return [SaleResultFaker.Generate()];
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static ListSaleCommand GenerateInvalidCommand()
    {
        return new ListSaleCommand();
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static List<Sale> GenerateSaleList()
    {
        return [GenerateValidSale()];
    }
}