using AutoMapper;
using ProjectsApi.Application.Dtos;
using ProjectsApi.Domain.Models;

namespace ProjectsApi.Application.Profiles
{
    public class ProjectDtoProfile : Profile
    {
        public ProjectDtoProfile()
        {
            CreateMap<CreateProjectRequestDto, ProjectModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.TenantId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
