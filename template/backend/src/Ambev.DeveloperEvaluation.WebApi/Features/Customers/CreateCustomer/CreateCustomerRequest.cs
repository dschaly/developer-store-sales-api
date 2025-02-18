namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;

/// <summary>
/// Represents a request to create a new customer in the system.
/// </summary>
public class CreateCustomerRequest
{
    /// <summary>
    /// Gets or sets the customer name. 
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the customer e-mail address. Must be unique and contain only valid characters.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}