namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// Request model for getting a product list
/// </summary>
public class ListUserRequest
{
    /// <summary>
    /// The unique identifier of the User to retrieve
    /// </summary>
    public int? Page { get; set; }

    /// <summary>
    /// The unique identifier of the User to retrieve
    /// </summary>
    public int? Size { get; set; }

    /// <summary>
    /// The unique identifier of the User to retrieve
    /// </summary>
    public string? Order { get; set; }

    /// <summary>
    /// The User's title Filter
    /// </summary>
    public string? UserName { get; set; }
}