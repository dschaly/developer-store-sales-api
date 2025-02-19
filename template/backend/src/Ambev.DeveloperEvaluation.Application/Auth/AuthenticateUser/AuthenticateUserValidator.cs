using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser
{
    /// <summary>
    /// Validator for AuthenticateUserCommand that defines validation rules for authentication.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Password: Must meet security requirements (using PasswordValidator)
    /// </remarks>
    public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("The {PropertyName} is required.")
                .EmailAddress()
                .WithMessage("The provided E-mail is invalid.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("The {PropertyName} is required.")
                .MinimumLength(6)
                .WithMessage("The {PropertyName} must be at least {MinLength} characters long.");
        }
    }
}