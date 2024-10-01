using ProjectsApi.Domain.Repositories;
using ProjectsApi.Domain.Models;
using MongoDB.Driver;
using Elevar.Collections;
using AutoMapper;

namespace ProjectsApi.Infrastructure.Project
{
    public class ProjectMongoDbRepository : IProjectRepository
    {
        private readonly MongoDbContext _context;
        private readonly IMapper _mapper;

        public ProjectMongoDbRepository(MongoDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(ProjectModel project)
        {
            var projectMongoDoc = _mapper.Map<ProjectMongoDbDto>(project);
            try
            {
                await _context.Projects.InsertOneAsync(projectMongoDoc);
                return true;
            }
            catch (MongoWriteException ex)
            {
                Console.WriteLine($"Insert failed: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public async Task<PagedList<ProjectModel>> ReadAllAsync(Guid tenantId, int pageNumber = 1, int pageSize = 100) 
        {
            var filter = Builders<ProjectMongoDbDto>.Filter.Eq(p => p.TenantId, tenantId.ToString());
            var projectsDocs = await _context.Projects.Find(filter)
                .Skip((pageNumber -1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
            var projects = _mapper.Map<IEnumerable<ProjectModel>>(projectsDocs);

            var totalRecords = await _context.Projects.CountDocumentsAsync(filter);

            var projecsPaginated = new PagedList<ProjectModel>(projects, ((int)totalRecords));
            return projecsPaginated;
        }

        public async Task<ProjectModel>? ReadAsync(Guid tenantId, Guid projectId)
        {
            var filter = Builders<ProjectMongoDbDto>.Filter.And(
                Builders<ProjectMongoDbDto>.Filter.Eq(p => p.TenantId, tenantId.ToString()),
                Builders<ProjectMongoDbDto>.Filter.Eq(p => p.Id, projectId.ToString())
            );

            var projectDoc = await _context.Projects.Find(filter).FirstOrDefaultAsync();
            var project = _mapper.Map<ProjectModel>(projectDoc);
            return project;
        }

        public async Task<bool> UpdateAsync(ProjectModel project)
        {
            var projectDoc = _mapper.Map<ProjectMongoDbDto>(project);
            var filter = Builders<ProjectMongoDbDto>.Filter.And(
                Builders<ProjectMongoDbDto>.Filter.Eq(p => p.TenantId, project.TenantId.ToString()),
                Builders<ProjectMongoDbDto>.Filter.Eq(p => p.Id, project.Id.ToString())
            );
            var updateResult = await _context.Projects.ReplaceOneAsync(filter, projectDoc);
            return updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(Guid tenantId, Guid projectId)
        {
            var filter = Builders<ProjectMongoDbDto>.Filter.And(
                Builders<ProjectMongoDbDto>.Filter.Eq(p => p.TenantId, tenantId.ToString()),
                Builders<ProjectMongoDbDto>.Filter.Eq(p => p.Id, projectId.ToString())
            );
            var deleteResult = await _context.Projects.DeleteOneAsync(filter);
            return deleteResult.DeletedCount > 0;
        }
    }
}
