using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Command for retrieving a list of all users 
/// </summary>
public class ListUserCommand : IRequest<ListUserResult>
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
    /// The User's userName
    /// </summary>
    public string? UserName { get; set; }
}