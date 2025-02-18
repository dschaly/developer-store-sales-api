namespace Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;

/// <summary>
/// Represents the response returned after successfully updating a new customer.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the recently updated customer,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateCustomerResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the recently updated customer.
    /// </summary>
    /// <value>A GUID that uniquely identifies the updated customer in the system.</value>
    public Guid Id { get; set; }
}