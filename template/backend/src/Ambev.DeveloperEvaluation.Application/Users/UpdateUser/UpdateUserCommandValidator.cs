using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Validator for UpdateUserCommand that defines validation rules for product creation command.
/// </summary>
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateUserCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// - Title: Must not be empty and length between 3 and 50 characters
    /// - Price: Must not be empty and must be greater than zero
    /// - Category: Must not be empty and length between 3 and 50 characters
    /// - Description: Must not be empty and must be null or empty and have a maximum length of 500
    /// - Image: Must not be empty and must be null or empty and have a maximum length of 250
    /// - Rating: Must not be valid (using RatingValidator)
    /// </remarks>
    public UpdateUserCommandValidator()
    {
        RuleFor(o => o.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
        RuleFor(user => user.Email)
            .SetValidator(new EmailValidator());
        RuleFor(user => user.Username)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.")
            .Length(3, 50)
            .WithMessage("The {PropertyName} must be between {MinLength} and {MaxLength} characters long.");
        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator());
        RuleFor(user => user.Phone)
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("The phone format is invalid.");
        RuleFor(user => user.Status)
            .NotEqual(UserStatus.Unknown)
            .WithMessage("The {PropertName} is invalid.");
        RuleFor(user => user.Role)
            .NotEqual(UserRole.None)
            .WithMessage("The {PropertName} is invalid.");
    }
}