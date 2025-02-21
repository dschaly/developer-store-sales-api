using Ambev.DeveloperEvaluation.Application.Common;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

/// <summary>
/// Represents the response returned after successfully updating a new branch.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the recently updated user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateBranchResult : BaseResult
{
    /// <summary>
    /// Gets the name of the branch.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets the address of the branch.
    /// </summary>
    public string Address { get; set; } = string.Empty;
}