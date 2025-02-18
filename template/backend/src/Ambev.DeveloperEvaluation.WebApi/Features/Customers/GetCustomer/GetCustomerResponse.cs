using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetCustomer;

/// <summary>
/// API response model for GetCustomer operation
/// </summary>
public class GetCustomerResponse : BaseResponse
{
    /// <summary>
    /// The customer's name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The customer's address
    /// </summary>
    public string Email { get; set; } = string.Empty;
}