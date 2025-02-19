using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

/// <summary>
/// Handler for processing UpdateBranchCommand requests
/// </summary>
public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchResult>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    /// <summary>
    /// Initializes a new instance of UpdateBranchHandler
    /// </summary>
    /// <param name="branchRepository">The branch repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="user">The Authenticated User</param>
    public UpdateBranchHandler(IBranchRepository branchRepository, IMapper mapper, IUser user)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
        _user = user;
    }

    /// <summary>
    /// Handles the UpdateBranchCommand request
    /// </summary>
    /// <param name="command">The UpdateBranchCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated branch details</returns>
    public async Task<UpdateBranchResult> Handle(UpdateBranchCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateBranchCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var entity = await _branchRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Entity with id {command.Id} not found");

        _mapper.Map(command, entity);

        entity.UpdatedAt = DateTime.UtcNow;
        entity.UpdatedBy = _user.Username;

        var updatedBranch = await _branchRepository.UpdateAsync(entity, cancellationToken);

        var result = _mapper.Map<UpdateBranchResult>(updatedBranch);

        return result;
    }
}