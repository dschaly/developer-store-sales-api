using FluentValidation;

/// <summary>
/// Validator for GetUserCommand
/// </summary>
namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

public class ListUserCommandValidator : AbstractValidator<ListUserCommand>
{
    /// <summary>
    /// Initializes validation rules for GetUserCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - UserName: Must not not exceed 50 characters.
    /// </remarks>
    public ListUserCommandValidator()
    {
        RuleFor(x => x.UserName)
            .MaximumLength(50)
            .WithMessage("he {PropertyName} must not exceed {MaxLength} characters.");
    }
}