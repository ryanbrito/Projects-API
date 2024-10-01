using Newtonsoft.Json;

namespace ProjectsApi.Application.Dtos.Shared
{
    public class Authorization
    {
        /// <summary>
        /// Your Tenant Id
        /// </summary>
        public required Guid TenantId { get; set; }

        /// <summary>
        /// Sets the Request correlation ID.
        /// </summary>
        public Guid CorrelationId { get; set; } = Guid.NewGuid();
    }
}
