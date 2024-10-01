using AutoMapper;
using ProjectsApi.Application.Dtos;
using ProjectsApi.Application.Dtos.Shared;
using ProjectsApi.Domain.Models;
using ProjectsApi.Domain.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace ProjectsApi.Application
{
    public class ProjectsAppService
    {
        private IProjectRepository _projectRepository;
        private IMapper _mapper;

        public ProjectsAppService(IProjectRepository projectRepository, IMapper mapper) 
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<CreateProjectResponseDto> CreateProjectAsync(Authorization authorization, CreateProjectRequestDto request)
        {
            var project = _mapper.Map<ProjectModel>(request);
            project.TenantId = authorization.TenantId;

            var projectOld = await _projectRepository.ReadAsync(
                tenantId : authorization.TenantId,
                projectId : project.Id
            );

            if (projectOld != null)
            {
                var createResponse = new CreateProjectResponseDto
                {
                    Id = project.Id.ToString(),
                    PublicKey = project.PublicKey,
                    Messages = new List<Message>
                    {
                        MessagesContent.ERROR_MESSAGES["prj-503"]
                    }
                };
                return createResponse;
            }

            bool created = await _projectRepository.CreateAsync(project);
            if (created)
            {
                var createResponse = new CreateProjectResponseDto
                {
                    Id = project.Id.ToString(),
                    PublicKey = project.PublicKey,
                    Messages = new List<Message>
                    {
                        MessagesContent.SUCCESS_MESSAGES["prj-001"]
                    }
                };
                return createResponse;
            }
            else
            {
                var createResponse = new CreateProjectResponseDto
                {
                    Id = project.Id.ToString(),
                    PublicKey = "",
                    Messages = new List<Message>
                    {
                        MessagesContent.ERROR_MESSAGES["prj-500"]
                    }
                };
                return createResponse;
            };
        }
    }
}
