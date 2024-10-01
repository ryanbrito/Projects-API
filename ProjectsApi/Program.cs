using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProjectsApi.Application;
using ProjectsApi.Application.Profiles;
using ProjectsApi.Domain.Repositories;
using ProjectsApi.Infrastructure;
using ProjectsApi.Infrastructure.Project;

var builder = WebApplication.CreateBuilder(args);

// Mapper
var config = new MapperConfiguration(cfg => {
    cfg.AddProfile<ProjectDtoProfile>();
    cfg.AddProfile<ProjectMongoDbProfile>();
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton<IMapper>(mapper);


var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings");
var mongoDbContext = new MongoDbContext(
        mongoDbSettings["ConnectionString"],
        mongoDbSettings["DatabaseName"]
    );
builder.Services.AddScoped<IProjectRepository, ProjectMongoDbRepository>(); // Use AddScoped
builder.Services.AddSingleton(mongoDbContext);

// Add services to the container.
builder.Services.AddScoped<ProjectsAppService>();


// Add Controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CODESSEY.API", Version = "v1" });

    // Enable annotations
    c.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
