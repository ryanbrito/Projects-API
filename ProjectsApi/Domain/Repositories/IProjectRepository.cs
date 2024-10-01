using Elevar.Collections;
using ProjectsApi.Domain.Models;

namespace ProjectsApi.Domain.Repositories
{
    public interface IProjectRepository
    {
        public Task<bool> CreateAsync(ProjectModel project);
        public Task<PagedList<ProjectModel>> ReadAllAsync(Guid tenantId, int pageNumber = 1, int pageSize = 100);
        public Task<ProjectModel>? ReadAsync(Guid tenantId, Guid projectId);
        public Task<bool> UpdateAsync(ProjectModel project);
        public Task<bool> DeleteAsync(Guid tenantId, Guid projectId);
    }
}