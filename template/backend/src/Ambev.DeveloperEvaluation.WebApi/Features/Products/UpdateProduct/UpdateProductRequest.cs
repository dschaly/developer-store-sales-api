﻿using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Represents a request to update a new product in the system.
/// </summary>
public class UpdateProductRequest
{
    /// <summary>
    /// The unique identifier of the created product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the product title. Must be unique and contain only valid characters.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product address.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets the category of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets the image of the product.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets the rating of the product. Must be a valid Rating format
    /// </summary>
    public Rating Rating { get; set; } = new Rating(0, 0);
}