using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.ListUsers;

/// <summary>
/// Validator for GetUserRequest
/// </summary>
public class ListUserRequestValidator : AbstractValidator<ListUserRequest>
{
    /// <summary>
    /// Initializes validation rules for GetUserRequest
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Must not exceed 50 characters
    /// - Category: Must not exceed 50 characters
    /// </remarks>
    public ListUserRequestValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
    }
}