using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Validator for CreateUserCommand that defines validation rules for user creation command.
/// </summary>
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateUserCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Username: Required, must be between 3 and 50 characters
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// - Phone: Must match international format (+X XXXXXXXXXX)
    /// - Status: Cannot be set to Unknown
    /// - Role: Cannot be set to None
    /// </remarks>
    public CreateUserCommandValidator()
    {
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