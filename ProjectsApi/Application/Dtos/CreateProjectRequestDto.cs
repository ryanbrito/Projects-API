namespace ProjectsApi.Application.Dtos
{
    /// <summary>
    /// A DTO responsible for handling Create Project Request Data
    /// </summary>
    public class CreateProjectRequestDto
    {
        /// <summary>
        /// The title/name of your project
        /// </summary>
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
    }
}
