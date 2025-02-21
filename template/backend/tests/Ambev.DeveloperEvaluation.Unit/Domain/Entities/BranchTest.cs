using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public class BranchTests
{
    /// <summary>
    /// Tests that a branch is successfully created with valid data.
    /// </summary>
    [Fact(DisplayName = "Branch should be created with valid data")]
    public void Given_ValidBranchData_When_Created_Then_ShouldBeValid()
    {
        // Arrange
        var branch = BranchTestData.GenerateValidBranch();

        // Act
        var result = branch.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when branch properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid branch data")]
    public void Given_InvalidBranchData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var branch = new Branch
        {
            Id = Guid.Empty, // Invalid: empty GUID
            Name = "", // Invalid: empty name
            Address = "", // Invalid: empty address
            CreatedBy = "", // Invalid: empty CreatedBy
            CreatedAt = default // Invalid: default DateTime
        };

        // Act
        var result = branch.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when customer CreatedBy field is too long.
    /// </summary>
    [Fact(DisplayName = "Validation should fail when CreatedBy exceeds max length")]
    public void Given_CreatedByTooLong_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var branch = new Branch
        {
            Name = "Valid Name",
            Address = BranchTestData.GenerateValidAddress(),
            CreatedBy = new string('A', 101), // Invalid: exceeds 100 characters
        };

        // Act
        var result = branch.Validate();

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
        var branch = new Branch
        {
            Name = "Valid Name",
            Address = BranchTestData.GenerateValidAddress(),
            CreatedAt = DateTime.UtcNow.AddDays(1),// Invalid: in the future
            CreatedBy = "", // Invalid: empty
        };

        // Act
        var result = branch.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Contains(result.Errors, e => e.PropertyName == "CreatedAt");
    }
}
