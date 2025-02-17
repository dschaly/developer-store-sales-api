namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;

/// <summary>
/// API response model for CreateBranch operation
/// </summary>
public class CreateBranchResponse
{
    /// <summary>
    /// The unique identifier of the created branch
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The branch's full name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The branch's address
    /// </summary>
    public string Address { get; set; } = string.Empty;
}