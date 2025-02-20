using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Represents a request to update a new Sale in the system.
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// The unique identifier of the created Sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the Sale title. Must be unique and contain only valid characters.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the Sale address.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the description of the Sale.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets the category of the Sale.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets the image of the Sale.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets the rating of the Sale. Must be a valid Rating format
    /// </summary>
    public Rating Rating { get; set; } = new Rating(0, 0);
}