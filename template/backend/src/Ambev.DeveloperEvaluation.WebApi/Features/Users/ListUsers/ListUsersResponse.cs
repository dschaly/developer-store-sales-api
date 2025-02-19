using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// API response model for GetUser operation
/// </summary>
public class ListUserResponse
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public List<UserResponse>? Data { get; set; }
}

public class UserResponse : BaseResult
{
    /// <summary>
    /// Gets the user's full name.
    /// Must not be null or empty and should contain both first and last names.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets the user's email address.
    /// Must be a valid email format and is used as a unique identifier for authentication.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets the user's phone number.
    /// Must be a valid phone number format following the pattern (XX) XXXXX-XXXX.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets the hashed password for authentication.
    /// Password must meet security requirements: minimum 8 characters, at least one uppercase letter,
    /// one lowercase letter, one number, and one special character.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets the user's role in the system.
    /// Determines the user's permissions and access levels.
    /// </summary>
    public string Role { get; set; } = string.Empty;

    /// <summary>
    /// Gets the user's current status.
    /// Indicates whether the user is active, inactive, or blocked in the system.
    /// </summary>
    public string Status { get; set; } = string.Empty;
}