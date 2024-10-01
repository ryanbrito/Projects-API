using ProjectsApi.Application.Dtos.Shared;

namespace ProjectsApi.Application.Dtos
{
    public class CreateProjectResponseDto
    {
        public required string Id { get; set; } = string.Empty;
        public required string PublicKey { get; set; } = string.Empty;
        public required List<Message> Messages { get; set; } = new List<Message>();
    }
}
