using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Customer entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class CustomerTests
{
    /// <summary>
    /// Tests that validation passes when all customer properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid customer data")]
    public void Given_ValidCustomerData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var customer = CustomerTestData.GenerateValidCustomer();

        // Act
        var result = customer.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when customer properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid customer data")]
    public void Given_InvalidCustomerData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "", // Invalid: empty
            Email = CustomerTestData.GenerateInvalidEmail(), // Invalid: not a valid email
            CreatedBy = "", // Invalid: empty
        };

        // Act
        var result = customer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when customer name is too short.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for short customer name")]
    public void Given_ShortName_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "AB", // Invalid: too short
            Email = CustomerTestData.GenerateValidEmail(),
            CreatedBy = "", // Invalid: empty
        };

        // Act
        var result = customer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, e => e.PropertyName == "Name");
    }

    /// <summary>
    /// Tests that validation fails when customer email is invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid customer email")]
    public void Given_InvalidEmail_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "Valid Name",
            Email = CustomerTestData.GenerateInvalidEmail(), // Invalid email
            CreatedBy = "", // Invalid: empty
        };

        // Act
        var result = customer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, e => e.PropertyName == "Email");
    }

    /// <summary>
    /// Tests that validation fails when customer CreatedBy field is too long.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when CreatedBy exceeds max length")]
    public void Given_CreatedByTooLong_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "Valid Name",
            Email = CustomerTestData.GenerateValidEmail(),
            CreatedBy = new string('A', 101), // Invalid: exceeds 100 characters
        };

        // Act
        var result = customer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, e => e.PropertyName == "CreatedBy");
    }

    /// <summary>
    /// Tests that validation fails when customer CreatedAt is in the future.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when CreatedAt is in the future")]
    public void Given_CreatedAtInFuture_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var customer = new Customer
        {
            Name = "Valid Name",
            Email = CustomerTestData.GenerateValidEmail(),
            CreatedAt = DateTime.UtcNow.AddDays(1),// Invalid: in the future
            CreatedBy = "", // Invalid: empty
        };

        // Act
        var result = customer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, e => e.PropertyName == "CreatedAt");
    }
}
