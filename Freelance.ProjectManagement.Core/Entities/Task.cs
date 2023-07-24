using Freelance.ProjectManagement.Core.Entities.Base;
using TaskStatus = Freelance.ProjectManagement.Core.Enums.TaskStatus;

namespace Freelance.ProjectManagement.Core.Entities
{
    public class Task : BaseEntity<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public int Priority { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
