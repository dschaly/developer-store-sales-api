using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Handler for processing ListUserCommand requests
/// </summary>
public class ListUserHandler : IRequestHandler<ListUserCommand, ListUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ListUserHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the ListUserCommand request
    /// </summary>
    /// <param name="command">The ListUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The users paginated list</returns>
    public async Task<ListUserResult> Handle(ListUserCommand command, CancellationToken cancellationToken)
    {
        var validator = new ListUserCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        int page = command.Page ?? 1;
        int size = command.Size ?? 10;

        var query = await _userRepository.Query(cancellationToken);

        if (query is null)
        {
            return new ListUserResult
            {
                Data = [],
                TotalCount = 0,
                CurrentPage = page,
                TotalPages = 0
            };
        }

        query = ApplyFiltering(query, command);

        if (!string.IsNullOrWhiteSpace(command.Order))
        {
            query = QueryingUtil<User>.ApplyOrdering(query, command.Order);
        }

        int totalCount = await query.CountAsync(cancellationToken);

        var users = query.Skip((page - 1) * size).Take(size);

        var result = new ListUserResult
        {
            Data = _mapper.Map<IEnumerable<UserResult>>(users),
            TotalCount = totalCount,
            CurrentPage = page,
            TotalPages = (int)Math.Ceiling(totalCount / (double)size)
        };

        return result;
    }

    public static IQueryable<User> ApplyFiltering(IQueryable<User> query, ListUserCommand command)
    {
        if (!string.IsNullOrWhiteSpace(command.UserName))
            query = query.Where(p => EF.Functions.Like(EF.Property<string>(p, "UserName"), $"%{command.UserName}%"));

        return query;
    }
}