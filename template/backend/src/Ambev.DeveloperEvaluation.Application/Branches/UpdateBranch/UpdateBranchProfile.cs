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
        CreateMap<UpdateBranchCommand, Branch>();
        CreateMap<Branch, UpdateBranchResult>();
    }
}