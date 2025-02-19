using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Profile for mapping between User entity and UpdateUserResponse
/// </summary>
public class UpdateUserProfile : Profile
{
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserCommand, User>()
            .ForMember(o => o.CreatedAt, opt => opt.Ignore())
            .ForMember(o => o.CreatedBy, opt => opt.Ignore())
            .ForMember(o => o.UpdatedAt, opt => opt.Ignore())
            .ForMember(o => o.UpdatedBy, opt => opt.Ignore());
        CreateMap<User, UpdateUserResult>();
    }
}