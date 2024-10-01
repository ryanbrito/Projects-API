using AutoMapper;
using ProjectsApi.Domain.Models;
using ProjectsApi.Infrastructure.Project;

public class ProjectMongoDbProfile : Profile
{
    public ProjectMongoDbProfile()
    {
        CreateMap<ProjectModel, ProjectMongoDbDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId.ToString()));

        CreateMap<ProjectMongoDbDto, ProjectModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
            .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => Guid.Parse(src.TenantId)));
    }
}
