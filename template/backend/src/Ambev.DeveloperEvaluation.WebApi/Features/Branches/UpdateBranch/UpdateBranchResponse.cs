using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.UpdateBranch;

/// <summary>
/// API response model for UpdateBranch operation
/// </summary>
public class UpdateBranchResponse : BaseResponse
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