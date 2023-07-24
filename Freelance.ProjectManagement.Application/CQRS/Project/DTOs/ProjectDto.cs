using Freelance.ProjectManagement.Core.Enums;

namespace Freelance.ProjectManagement.Application.CQRS.Project.DTOs
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Start { get; set; }    

        public DateTime? End { get; set; }

        public ProjectStatus Status { get; set; }
    }
}
