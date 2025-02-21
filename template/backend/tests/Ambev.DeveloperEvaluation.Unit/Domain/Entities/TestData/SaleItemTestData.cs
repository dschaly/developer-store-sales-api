using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleItemTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(s => s.ProductId, f => f.Random.Guid()) // Generate a random GUID for ProductId
        .RuleFor(s => s.Product, GenerateValidProduct()) // Generate a valid Product
        .RuleFor(s => s.Quantity, f => f.Random.Int(1, 20)) // Generate a random quantity between 1 and 100
        .RuleFor(s => s.Discount, f => f.Random.Decimal(0, 40)) // Generate a discount between 0 and 50
        .RuleFor(s => s.TotalAmount, 155.20m) // Calculate total amount based on quantity and discount
        .RuleFor(s => s.SaleId, Guid.NewGuid()) // Generate a random GUID for SaleId
        .RuleFor(s => s.CreatedBy, f => f.Internet.UserName()) // Generate a random username for CreatedBy
        .RuleFor(s => s.CreatedAt, f => f.Date.Past(1)) // Generate a random date within the past year
        .RuleFor(s => s.UpdatedAt, f => f.Date.Recent(1)) // Generate a recent date for UpdatedAt
        .RuleFor(s => s.UpdatedBy, f => f.Internet.UserName().OrNull(f)); // Generate a random username for UpdatedBy or null

    public static Product GenerateValidProduct()
    {
        return new Faker<Product>()
        .RuleFor(p => p.Id, f => f.Random.Guid())
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(min: 1.00m, max: 1000.00m))) // Price between 1 and 1000
        .RuleFor(p => p.Description, f => f.Lorem.Sentence(10))
        .RuleFor(p => p.Category, f => f.Commerce.Department())
        .RuleFor(p => p.Image, f => f.Internet.Url())
        .RuleFor(p => p.Rating, f => new Rating(f.Random.Int(1, 5), f.Random.Int(0, 100))) // Random rating between 1 and 5, with a random number of reviews
        .RuleFor(p => p.CreatedBy, f => f.Internet.UserName())
        .RuleFor(p => p.CreatedAt, f => f.Date.Past(1))
        .RuleFor(s => s.UpdatedAt, f => f.Date.Recent(1)) // Generate a recent date for UpdatedAt
        .RuleFor(p => p.UpdatedBy, f => f.Internet.UserName().OrNull(f))
        .Generate();
    }

    public static SaleItem GenerateValidSaleItem()
    {
        return SaleItemFaker.Generate();
    }

    public static SaleItem GenerateInvalidDiscountSaleItem()
    {
        return SaleItemFaker
            .RuleFor(s => s.Discount, -10) // Invalid discount, should be greater than or equal to 0
            .Generate();
    }

    public static SaleItem GenerateInvalidTotalAmountSaleItem()
    {
        return SaleItemFaker
            .RuleFor(s => s.TotalAmount, -10) // Invalid discount, should be greater than or equal to 0
            .Generate();
    }
}
