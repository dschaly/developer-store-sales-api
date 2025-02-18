namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer;

/// <summary>
/// Represents a request to update a new customer in the system.
/// </summary>
public class UpdateCustomerRequest
{
    /// <summary>
    /// The unique identifier of the created customer
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the customer name. Must be unique and contain only valid characters.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer address.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}