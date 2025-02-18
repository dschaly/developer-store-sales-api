namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;

/// <summary>
/// Represents a request to update a new branch in the system.
/// </summary>
public class UpdateBranchRequest
{
    /// <summary>
    /// The unique identifier of the created branch
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the branch name. Must be unique and contain only valid characters.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the branch address.
    /// </summary>
    public string Address { get; set; } = string.Empty;
}