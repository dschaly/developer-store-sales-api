namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Represents the response returned after successfully updating a new sale.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the recently updated sale,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateSaleResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the recently updated sale.
    /// </summary>
    /// <value>A GUID that uniquely identifies the updated sale in the system.</value>
    public Guid Id { get; set; }
}