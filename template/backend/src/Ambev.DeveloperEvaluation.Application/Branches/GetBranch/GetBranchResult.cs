namespace Ambev.DeveloperEvaluation.Application.Branches.GetBranch;

/// <summary>
/// Response model for GetBranch operation
/// </summary>
public class GetBranchResult
{
    /// <summary>
    /// The unique identifier of the branch
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