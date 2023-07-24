using TaskStatus = Freelance.ProjectManagement.Core.Enums.TaskStatus;

namespace Freelance.ProjectManagement.Application.CQRS.Task.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public TaskStatus Status { get; set; }
    }
}
