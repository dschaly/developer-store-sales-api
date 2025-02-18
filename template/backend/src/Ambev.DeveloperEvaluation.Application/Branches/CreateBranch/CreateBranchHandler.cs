using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Handler for processing CreateBranchCommand requests
/// </summary>
public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, CreateBranchResult>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    /// <summary>
    /// Initializes a new instance of CreateBranchHandler
    /// </summary>
    /// <param name="branchRepository">The branch repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CreateBranchHandler(IBranchRepository branchRepository, IMapper mapper, IUser user)
    {
        _branchRepository = branchRepository;
        _mapper = mapper;
        _user = user;
    }

    /// <summary>
    /// Handles the CreateBranchCommand request
    /// </summary>
    /// <param name="command">The CreateBranchCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created Branch Id</returns>
    public async Task<CreateBranchResult> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateBranchCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingBranch = await _branchRepository.GetByNameAsync(command.Name, cancellationToken);
        if (existingBranch is not null)
            throw new InvalidOperationException($"Branch with name {command.Name} already exists");

        var branch = _mapper.Map<Branch>(command);

        branch.CreatedBy = _user.Username;

        var createdBranch = await _branchRepository.CreateAsync(branch, cancellationToken);
        var result = _mapper.Map<CreateBranchResult>(createdBranch);

        return result;
    }
}