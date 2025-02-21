using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;

/// <summary>
/// Represents the response returned after successfully updating a new customer.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the recently updated user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateCustomerResult : BaseResult
{
    /// <summary>
    /// Gets the name of the customer.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets the email address of the customer.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}