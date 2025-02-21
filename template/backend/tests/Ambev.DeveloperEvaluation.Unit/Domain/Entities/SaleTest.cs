using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover validation scenarios including required fields and formats.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale data")]
    public void Given_ValidSaleData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();

        // Act
        var result = sale.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when sale properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid sale data")]
    public void Given_InvalidSaleData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = new Sale
        {
            CustomerId = Guid.Empty, // Invalid: CustomerId cannot be empty
            BranchId = Guid.Empty, // Invalid: BranchId cannot be empty
            SaleItems = [], // Invalid: SaleItems cannot be empty
            CreatedBy = "", // Invalid: CreatedBy cannot be empty
            CreatedAt = default, // Invalid: CreatedAt must have a valid value
            Customer = CustomerTestData.GenerateValidCustomer(),
            Branch = BranchTestData.GenerateInvalidBranch()
        };

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when TotalAmount is zero.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when TotalAmount is zero")]
    public void Given_ZeroTotalAmount_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = SaleTestData.GenerateInvvalidTotalAmountSale();

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName.Contains("TotalAmount"));
    }

    /// <summary>
    /// Tests that validation fails when CustomerId is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when CustomerId is empty")]
    public void Given_EmptyCustomerId_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.CustomerId = Guid.Empty; // Invalid: empty CustomerId

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "CustomerId");
    }

    /// <summary>
    /// Tests that validation fails when BranchId is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when BranchId is empty")]
    public void Given_EmptyBranchId_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.BranchId = Guid.Empty; // Invalid: empty BranchId

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "BranchId");
    }

    /// <summary>
    /// Tests that validation fails when SaleItems is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when SaleItems is empty")]
    public void Given_EmptySaleItems_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.SaleItems = []; // Invalid: empty SaleItems

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "SaleItems");
    }

    /// <summary>
    /// Tests that validation fails when CreatedBy is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when CreatedBy is empty")]
    public void Given_EmptyCreatedBy_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.CreatedBy = ""; // Invalid: empty CreatedBy

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "CreatedBy");
    }

    /// <summary>
    /// Tests that validation fails when CreatedAt is default (empty DateTime).
    /// </summary>
    [Fact(DisplayName = "Validation should fail when CreatedAt is default")]
    public void Given_DefaultCreatedAt_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidSale();
        sale.CreatedAt = default; // Invalid: default DateTime

        // Act
        var result = sale.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "CreatedAt");
    }

    /// <summary>
    /// Tests that validation fails when trying to Cancel an already canceled Sale.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when trying to Cancel an already canceled Sale")]
    public void Given_AlreadyCanceledSale_When_Validated_Then_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var sale = SaleTestData.GenerateValidCanceledSale();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => sale.CancelSale());
    }
}
