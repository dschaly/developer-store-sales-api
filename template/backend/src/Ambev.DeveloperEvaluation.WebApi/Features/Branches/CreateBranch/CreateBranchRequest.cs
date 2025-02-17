namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;

/// <summary>
/// Represents a request to create a new branch in the system.
/// </summary>
public class CreateBranchRequest
{
    /// <summary>
    /// Gets or sets the branch name. Must be unique and contain only valid characters.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch address.
    /// </summary>
    public string Address { get; set; } = string.Empty;
}