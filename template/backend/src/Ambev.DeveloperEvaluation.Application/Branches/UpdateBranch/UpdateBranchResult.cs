namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

/// <summary>
/// Represents the response returned after successfully updating a new branch.
/// </summary>
/// <remarks>
/// This response contains the unique identifier of the recently updated user,
/// which can be used for subsequent operations or reference.
/// </remarks>
public class UpdateBranchResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the recently updated branch.
    /// </summary>
    /// <value>A GUID that uniquely identifies the updated branch in the system.</value>
    public Guid Id { get; set; }
}