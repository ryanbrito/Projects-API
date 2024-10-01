using MongoDB.Driver;
using ProjectsApi.Domain.Models;
using ProjectsApi.Infrastructure.Project;


namespace ProjectsApi.Infrastructure
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<ProjectMongoDbDto> Projects => _database.GetCollection<ProjectMongoDbDto>("Projects");
    }
}
