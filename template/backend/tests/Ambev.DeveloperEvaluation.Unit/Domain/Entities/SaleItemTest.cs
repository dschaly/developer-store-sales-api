using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the SaleItem entity class.
/// Tests cover discount application, total amount calculation, and validation scenarios.
/// </summary>
public class SaleItemTests
{
    /// <summary>
    /// Tests that validation passes when all sale item properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale item data")]
    public void Given_ValidSaleItemData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange

        var saleItem = new Faker<SaleItem>()
            .RuleFor(s => s.ProductId, f => f.Random.Guid()) // Generate a random GUID for ProductId
            .RuleFor(s => s.Product, ProductTestData.GenerateValidProduct()) // Generate a valid Product
            .RuleFor(s => s.Quantity, f => f.Random.Int(1, 20)) // Generate a random quantity between 1 and 100
            .RuleFor(s => s.Discount, f => f.Random.Decimal(0, 40)) // Generate a discount between 0 and 50
            .RuleFor(s => s.TotalAmount, 155.20m) // Calculate total amount based on quantity and discount
            .RuleFor(s => s.SaleId, Guid.NewGuid()) // Generate a random GUID for SaleId
            .RuleFor(s => s.CreatedBy, f => f.Internet.UserName()) // Generate a random username for CreatedBy
            .RuleFor(s => s.CreatedAt, f => f.Date.Past(1)) // Generate a random date within the past year
            .RuleFor(s => s.UpdatedAt, f => f.Date.Recent(1)) // Generate a recent date for UpdatedAt
            .RuleFor(s => s.UpdatedBy, f => f.Internet.UserName().OrNull(f))
            .Generate(); 

        // Act
        var result = saleItem.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that applying a valid discount updates the Discount property.
    /// </summary>
    [Fact(DisplayName = "Applying a valid discount should update the Discount property")]
    public void Given_ValidDiscount_When_Applied_Then_ShouldUpdateDiscount()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();
        decimal validDiscount = saleItem.Product.Price * saleItem.Quantity * 0.1m; // 10% discount

        // Act
        saleItem.ApplyDiscount(validDiscount, saleItem.Product.Price);

        // Assert
        Assert.Equal(validDiscount, saleItem.Discount);
    }

    /// <summary>
    /// Tests that applying an invalid discount throws an exception.
    /// </summary>
    [Fact(DisplayName = "Applying an invalid discount should throw an exception")]
    public void Given_InvalidDiscount_When_Applied_Then_ShouldThrowException()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();
        decimal invalidDiscount = (saleItem.Product.Price * saleItem.Quantity) + 1; // More than total price

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => saleItem.ApplyDiscount(invalidDiscount, saleItem.Product.Price));
    }

    /// <summary>
    /// Tests that calculating the total amount updates the TotalAmount property correctly.
    /// </summary>
    [Fact(DisplayName = "Calculating total amount should update the TotalAmount property correctly")]
    public void Given_SaleItem_When_CalculateTotalAmount_Then_ShouldUpdateTotalAmount()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();
        decimal expectedTotal = Math.Round((saleItem.Product.Price * saleItem.Quantity) - saleItem.Discount, 2);

        // Act
        saleItem.CalculateTotalAmount(saleItem.Product.Price);

        // Assert
        Assert.Equal(expectedTotal, saleItem.TotalAmount);
    }

    /// <summary>
    /// Tests that validation fails when sale item discount is invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid discount.")]
    public void Given_InvalidDiscountSaleItemData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateInvalidDiscountSaleItem(); // Invalid: discount negative value

        // Act
        var result = saleItem.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, e => e.PropertyName == "Discount");
    }

    /// <summary>
    /// Tests that validation fails when sale item total amount invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid total amount")]
    public void Given_InvalidTotalAmountSaleItemData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateInvalidTotalAmountSaleItem(); // Invalid: total amount negative value

        // Act
        var result = saleItem.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, e => e.PropertyName == "TotalAmount");
    }
}
