using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

/// <summary>
/// Command for creating a new branch.
/// </summary>
/// <remarks>
/// This command is used to capture the required data for creating a branch, 
/// including name and address. 
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request 
/// that returns a <see cref="CreateBranchResult"/>.
/// 
/// The data provided in this command is validated using the 
/// <see cref="CreateUserCommandValidator"/> which extends 
/// <see cref="AbstractValidator{T}"/> to ensure that the fields are correctly 
/// populated and follow the required rules.
/// </remarks>
public class UpdateBranchCommand : IRequest<UpdateBranchResult>
{
    /// <summary>
    /// The unique identifier of the branch
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the branch.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of the branch.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateBranchCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
