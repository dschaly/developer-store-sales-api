using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

/// <summary>
/// Response model for GetCustomer operation
/// </summary>
public class GetCustomerResult : BaseResult
{
    /// <summary>
    /// The Customer's name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The Customer's e-mail address
    /// </summary>
    public string Email { get; set; } = string.Empty;
}