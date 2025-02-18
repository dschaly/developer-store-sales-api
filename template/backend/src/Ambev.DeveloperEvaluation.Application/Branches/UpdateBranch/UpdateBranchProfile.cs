using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branches.UpdateBranch;

/// <summary>
/// Profile for mapping between Branch entity and UpdateBranchResponse
/// </summary>
public class UpdateBranchProfile : Profile
{
    public UpdateBranchProfile()
    {
        CreateMap<UpdateBranchCommand, Branch>()
            .ForMember(o => o.CreatedAt, opt => opt.Ignore())
            .ForMember(o => o.CreatedBy, opt => opt.Ignore())
            .ForMember(o => o.UpdatedAt, opt => opt.Ignore())
            .ForMember(o => o.UpdatedBy, opt => opt.Ignore());
        CreateMap<Branch, UpdateBranchResult>();
    }
}