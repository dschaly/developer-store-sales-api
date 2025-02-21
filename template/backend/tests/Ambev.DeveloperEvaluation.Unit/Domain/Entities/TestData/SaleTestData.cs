using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data for the Customer entity using the Bogus library.
/// This class centralizes all test data generation to ensure consistency across test cases
/// and provide both valid and invalid data scenarios.
/// </summary>
public static class SaleTestData
{
    private static readonly Guid _saleId = Guid.NewGuid();

    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, _saleId)
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
    /// Generates a valid Entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Entity with randomly generated data.</returns>
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
    public static Sale GenerateInvvalidTotalAmountSale()
    {
        return SaleFaker
            .RuleFor(s => s.TotalAmount, 0)
            .Generate();
    }

    /// <summary>
    /// Generates a valid Entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Entity with randomly generated data.</returns>
    public static Sale GenerateValidCanceledSale()
    {
        return SaleFaker
            .RuleFor(s => s.IsCancelled, true)
            .Generate();
    }

    /// <summary>
    /// Generates a valid Entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Entity with randomly generated data.</returns>
    public static string GenerateValidSaleNumber()
    {
        return Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Generates a valid Entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Entity with randomly generated data.</returns>
    public static decimal GenerateValidTotalAmount()
    {
        return new Faker().Finance.Amount(1, 1000);
    }

    /// <summary>
    /// Generates a valid Entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Entity with randomly generated data.</returns>
    public static bool GenerateValidIsCancelled()
    {
        return new Faker().Random.Bool();
    }

    /// <summary>
    /// Generates a valid Entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Entity with randomly generated data.</returns>
    public static Guid GenerateValidCustomerId()
    {
        return Guid.NewGuid();
    }

    /// <summary>
    /// Generates a valid Entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Entity with randomly generated data.</returns>
    public static Guid GenerateValidBranchId()
    {
        return Guid.NewGuid();
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
                .RuleFor(s => s.Quantity, f => f.Random.Int(1, 10)) // Generate a random quantity between 1 and 100
                .RuleFor(s => s.Discount, f => f.Random.Decimal(0, 50)) // Generate a discount between 0 and 50
                .RuleFor(s => s.TotalAmount, (f, s) => Math.Round((f.Random.Decimal(10, 100) * s.Quantity) - s.Discount, 2)) // Calculate total amount based on quantity and discount
                .RuleFor(s => s.SaleId, _saleId) // Generate a random GUID for SaleId
                .RuleFor(s => s.CreatedBy, f => f.Internet.UserName()) // Generate a random username for CreatedBy
                .RuleFor(s => s.CreatedAt, f => f.Date.Past(1)) // Generate a random date within the past year
                .RuleFor(s => s.UpdatedAt, f => f.Date.Past(1)) // Generate a recent date for UpdatedAt
                .RuleFor(s => s.UpdatedBy, f => f.Internet.UserName().OrNull(f)); // Generate a random username for UpdatedBy or null

        var validSaleItems = new List<SaleItem> { saleItem, saleItem };

        return validSaleItems;
    }
}