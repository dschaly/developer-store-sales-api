using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Product entity class.
/// Tests cover validation scenarios including required fields and formats.
/// </summary>
public class ProductTests
{
    /// <summary>
    /// Tests that validation passes when all product properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid product data")]
    public void Given_ValidProductData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act
        var result = product.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when product properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid product data")]
    public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = new Product
        {
            Title = "", // Invalid: empty
            Price = 0, // Invalid: price cannot be zero
            Description = "", // Invalid: empty
            Category = "", // Invalid: empty
            Image = "", // Invalid: empty
            Rating = new Rating(5, 0), // Invalid: negative rating
            CreatedBy = "", // Invalid: empty CreatedBy
            CreatedAt = default, // Invalid: default DateTime
        };

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when the Title property is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when Title is empty")]
    public void Given_EmptyTitle_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Title = ""; // Invalid: empty Title

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }

    /// <summary>
    /// Tests that validation fails when the Price property is zero.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when Price is zero")]
    public void Given_ZeroPrice_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Price = 0; // Invalid: Price cannot be zero

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Price");
    }

    /// <summary>
    /// Tests that validation fails when the Description property is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when Description is empty")]
    public void Given_EmptyDescription_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Description = ""; // Invalid: empty Description

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Description");
    }

    /// <summary>
    /// Tests that validation fails when the Category property is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when Category is empty")]
    public void Given_EmptyCategory_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Category = ""; // Invalid: empty Category

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Category");
    }

    /// <summary>
    /// Tests that validation fails when the Image property is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when Image is empty")]
    public void Given_EmptyImage_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Image = ""; // Invalid: empty Image

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Image");
    }

    /// <summary>
    /// Tests that validation fails when the Rating Rate is negative.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when Rating Rate is negative")]
    public void Given_NegativeRatingRate_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Rating = new Rating(-1, 0); // Invalid: negative Rating

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Rating.Rate");
    }

    /// <summary>
    /// Tests that validation fails when the Rating Count is negative.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when Rating Count is negative")]
    public void Given_NegativeRatingCount_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.Rating = new Rating(5, -1); // Invalid: negative Rating

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Rating.Count");
    }

    /// <summary>
    /// Tests that validation fails when CreatedBy is empty.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when CreatedBy is empty")]
    public void Given_EmptyCreatedBy_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();
        product.CreatedBy = ""; // Invalid: empty CreatedBy

        // Act
        var result = product.Validate();

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
        var product = ProductTestData.GenerateValidProduct();
        product.CreatedAt = default; // Invalid: default DateTime

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "CreatedAt");
    }
}