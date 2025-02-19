using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers;

/// <summary>
/// Profile for mapping between User entity and GetUserResult
/// </summary>
public class ListUserProfile : Profile
{
    public ListUserProfile()
    {
        CreateMap<User, UserResult>();
    }
}