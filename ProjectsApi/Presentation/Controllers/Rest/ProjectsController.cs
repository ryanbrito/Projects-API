using Microsoft.AspNetCore.Mvc;
using ProjectsApi.Application;
using ProjectsApi.Application.Dtos;
using ProjectsApi.Application.Dtos.Shared;
using Swashbuckle.AspNetCore.Annotations;

namespace FilmesApi.Presentation.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private ProjectsAppService _projectsAppService;

        public ProjectsController(ProjectsAppService projectsAppService)
        {
            _projectsAppService = projectsAppService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateProjectResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Creates a new project")]
        public async Task<IActionResult> CreateProject(
            [FromHeader(Name = "X-Tenant-Id"), SwaggerParameter(Required = true)] Guid tenantId,
            [FromHeader(Name = "X-Correlation-Id")] Guid? correlationId,
            [FromBody] CreateProjectRequestDto request)
        {
            var authorization = new Authorization
            {
                TenantId = tenantId,
                CorrelationId = correlationId ?? Guid.NewGuid()
            };
            var response = await _projectsAppService.CreateProjectAsync(authorization, request);
            return Ok(response);
        }
    }
}