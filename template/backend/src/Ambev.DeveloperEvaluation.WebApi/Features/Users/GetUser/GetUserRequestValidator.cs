using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetUser;

/// <summary>
/// Validator for GetUserRequest
/// </summary>
public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
{
    /// <summary>
    /// Initializes validation rules for GetUserRequest
    /// </summary>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public GetUserRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.");
    }
}
