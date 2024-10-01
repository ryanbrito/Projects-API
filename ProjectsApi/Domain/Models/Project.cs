namespace ProjectsApi.Domain.Models
{
    /// <summary>
    /// Represents a project in the Codessey context.
    /// </summary>
    public class ProjectModel
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string? PublicKey { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? DeletedBy { get; set; }
    }
}
