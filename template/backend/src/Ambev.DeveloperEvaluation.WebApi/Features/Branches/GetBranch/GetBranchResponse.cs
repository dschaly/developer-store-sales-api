using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;

/// <summary>
/// API response model for GetBranch operation
/// </summary>
public class GetBranchResponse : BaseResponse
{
    /// <summary>
    /// The branch's name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The branch's address
    /// </summary>
    public string Address { get; set; } = string.Empty;
}