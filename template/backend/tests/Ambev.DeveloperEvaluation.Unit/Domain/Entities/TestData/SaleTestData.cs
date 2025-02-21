using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

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

    public static Sale GenerateValidSale()
    {
        return SaleFaker.Generate();
    }

    public static Sale GenerateInvvalidTotalAmountSale()
    {
        return SaleFaker
            .RuleFor(s => s.TotalAmount, 0)
            .Generate();
    }

    public static Sale GenerateValidCanceledSale()
    {
        return SaleFaker
            .RuleFor(s => s.IsCancelled, true)
            .Generate();
    }

    public static string GenerateValidSaleNumber()
    {
        return Guid.NewGuid().ToString();
    }

    public static decimal GenerateValidTotalAmount()
    {
        return new Faker().Finance.Amount(1, 1000);
    }

    public static bool GenerateValidIsCancelled()
    {
        return new Faker().Random.Bool();
    }

    public static Guid GenerateValidCustomerId()
    {
        return Guid.NewGuid();
    }

    public static Guid GenerateValidBranchId()
    {
        return Guid.NewGuid();
    }

    public static List<SaleItem> GenerateValidSaleItems()
    {
        var saleItem = new Faker<SaleItem>()
                .RuleFor(s => s.ProductId, Guid.NewGuid()) // Generate a random GUID for ProductId
                .RuleFor(s => s.Product, ProductTestData.GenerateValidProduct()) // Generate a valid Product
                .RuleFor(s => s.Quantity, f => f.Random.Int(1, 100)) // Generate a random quantity between 1 and 100
                .RuleFor(s => s.Discount, f => f.Random.Decimal(0, 50)) // Generate a discount between 0 and 50
                .RuleFor(s => s.TotalAmount, (f, s) => Math.Round((f.Random.Decimal(10, 100) * s.Quantity) - s.Discount, 2)) // Calculate total amount based on quantity and discount
                .RuleFor(s => s.SaleId, _saleId) // Generate a random GUID for SaleId
                .RuleFor(s => s.CreatedBy, f => f.Internet.UserName()) // Generate a random username for CreatedBy
                .RuleFor(s => s.CreatedAt, f => f.Date.Past(1)) // Generate a random date within the past year
                .RuleFor(s => s.UpdatedAt, f => f.Date.Recent(1)) // Generate a recent date for UpdatedAt
                .RuleFor(s => s.UpdatedBy, f => f.Internet.UserName().OrNull(f)); // Generate a random username for UpdatedBy or null

        var validSaleItems = new List<SaleItem> { saleItem, saleItem };

        return validSaleItems;
    }

    public static string GenerateInvalidSaleNumber() { return string.Empty; }

    public static decimal GenerateInvalidTotalAmount() { return -1; }

    public static Guid GenerateInvalidCustomerId() { return Guid.Empty; }

    public static Guid GenerateInvalidBranchId() { return Guid.Empty; }

    public static List<SaleItem> GenerateInvalidSaleItems()
    {
        return
        [
            new SaleItem
            {
                ProductId = Guid.Empty,
                Quantity = -1,
                CreatedBy = string.Empty,
                Product = ProductTestData.GenerateValidProduct(),
                Sale = SaleTestData.GenerateValidSale()
            }
        ];
    }

    public static string GenerateInvalidCreatedBy() { return string.Empty; }

    public static DateTime GenerateInvalidCreatedAt() { return DateTime.MinValue; }

    public static DateTime? GenerateInvalidUpdatedAt() { return null; }

    public static string GenerateInvalidUpdatedBy() { return string.Empty; }
}
