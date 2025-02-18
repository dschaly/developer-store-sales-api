using Ambev.DeveloperEvaluation.Domain.Entities;
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

    public UpdateBranchHandler(IBranchRepository branchRepository, IMapper mapper)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
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

        var branch = _mapper.Map<Branch>(command);
        branch.UpdatedAt = DateTime.UtcNow;

        var updatedBranch = await _branchRepository.UpdateAsync(branch, cancellationToken);

        var result = _mapper.Map<UpdateBranchResult>(updatedBranch);

        return result;
    }
}