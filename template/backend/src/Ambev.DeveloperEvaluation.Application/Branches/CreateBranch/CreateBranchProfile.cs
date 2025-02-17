using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;

/// <summary>
/// Profile for mapping between Branch entity and CreateBranchResponse
/// </summary>
public class CreateBranchProfile : Profile
{
    public CreateBranchProfile()
    {
        CreateMap<CreateBranchCommand, Branch>();
        CreateMap<Branch, CreateBranchResult>();
    }
}