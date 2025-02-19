using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Handler for processing UpdateUserCommand requests
/// </summary>
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUser _user;

    /// <summary>
    /// Initializes a new instance of UpdateUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="passwordHasher">The Password Hasher</param>
    /// <param name="user">The Authenticated User</param>
    public UpdateUserHandler(IUserRepository userRepository, IMapper mapper, IUser user, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _user = user;
        _passwordHasher = passwordHasher;
    }

    /// <summary>
    /// Handles the UpdateUserCommand request
    /// </summary>
    /// <param name="command">The UpdateUserCommand command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated user details</returns>
    public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var entity = await _userRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new InvalidOperationException($"Entity with id {command.Id} not found");

        _mapper.Map(command, entity);
        entity.Password = _passwordHasher.HashPassword(command.Password);

        entity.UpdatedBy = _user.Username;
        entity.UpdatedAt = DateTime.UtcNow;

        var updatedUser = await _userRepository.UpdateAsync(entity, cancellationToken);

        var result = _mapper.Map<UpdateUserResult>(updatedUser);

        return result;
    }
}