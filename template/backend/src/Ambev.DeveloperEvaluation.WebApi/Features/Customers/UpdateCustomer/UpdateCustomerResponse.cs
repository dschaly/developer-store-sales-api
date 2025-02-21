using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer;

/// <summary>
/// API response model for UpdateCustomer operation
/// </summary>
public class UpdateCustomerResponse : BaseResponse
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