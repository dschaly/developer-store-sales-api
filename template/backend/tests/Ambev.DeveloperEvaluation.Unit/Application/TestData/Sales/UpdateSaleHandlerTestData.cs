using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class UpdateSaleHandlerTestData
{
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
    /// </summary>
    private static readonly Faker<UpdateSaleCommand> UpdateSaleHandlerFaker = new Faker<UpdateSaleCommand>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
        .RuleFor(s => s.CustomerId, Guid.NewGuid())
        .RuleFor(s => s.BranchId, Guid.NewGuid())
        .RuleFor(s => s.SaleItems, SaleTestData.GenerateValidSaleItems());

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static UpdateSaleCommand GenerateValidCommand()
    {
        return UpdateSaleHandlerFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Entity with randomized data.
    /// The generated customer will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Entity with randomly generated data.</returns>
    public static List<UpdateSaleItemResult> GenerateValidSaleItems()
    {
        var saleItem = new Faker<UpdateSaleItemResult>()
            .RuleFor(s => s.ProductId, Guid.NewGuid()) // Generate a random GUID for ProductId
            .RuleFor(s => s.Quantity, f => f.Random.Int(1, 100)); // Generate a random quantity between 1 and 100

        var validSaleItems = new List<UpdateSaleItemResult> { saleItem.Generate(), saleItem.Generate() };

        return validSaleItems;
    }

    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// </summary>
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(b => b.Id, f => f.Random.Guid())
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
    private static readonly Faker<UpdateSaleResult> UpdateSaleResultFaker = new Faker<UpdateSaleResult>()
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
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Product GenerateExistingProduct()
    {
        return ProductTestData.GenerateValidProduct();
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static UpdateSaleResult GenerateValidResult()
    {
        return UpdateSaleResultFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateExistingSale()
    {
        return SaleFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated branch will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateUpdatedSale()
    {
        return SaleFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Guid Id name using Faker.
    /// The generated name will:
    /// - Follow the Guid format
    /// </summary>
    /// <returns>A valid Guid.</returns>
    public static UpdateSaleCommand GenerateInvalidCommand()
    {
        return new UpdateSaleCommand();
    }
}