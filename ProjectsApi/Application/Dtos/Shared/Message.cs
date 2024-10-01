using System.ComponentModel.DataAnnotations;

namespace ProjectsApi.Application.Dtos.Shared
{
    public class Message
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public Message(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }

    public static class MessagesContent
    {
        public static readonly Dictionary<string, Message> SUCCESS_MESSAGES = new()
        {
            { "prj-001", new Message("prj-001", "Project created successfully") },
            { "prj-002", new Message("prj-002", "Successfully retrieved Project(s)") },
            { "prj-003", new Message("prj-003", "Successfully updated Project") },
            { "prj-004", new Message("prj-004", "Successfully deleted Project") }
        };

        public static readonly Dictionary<string, Message> ERROR_MESSAGES = new()
        {
            { "prj-500", new Message("prj-500", "Error creating the project") },
            { "prj-501", new Message("prj-501", "X-Tenant-Id not provided. It is a required field.") },
            { "prj-502", new Message("prj-502", "Project name not provided. It is a required field.") },
            { "prj-503", new Message("prj-503", "Project already exists. Please, provide a new one") },
            { "prj-504", new Message("prj-504", "Exceeded Max Limit of Records (Projects) per page.") },
            { "prj-505", new Message("prj-505", "Project ID not provided. It is a required field.") },
            { "prj-506", new Message("prj-506", "Tags should be an array") },
            { "prj-507", new Message("prj-507", "Project(s) not found") },
            { "prj-508", new Message("prj-508", "Error Updating Project") },
            { "prj-509", new Message("prj-509", "Error Deleting Project") }
        };
    }
}
