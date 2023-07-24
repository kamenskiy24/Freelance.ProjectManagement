using Freelance.ProjectManagement.Core.Entities.Base;
using Freelance.ProjectManagement.Core.Enums;

namespace Freelance.ProjectManagement.Core.Entities
{
    public class Project : BaseEntity<int>
    {
        public string Name { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? End { get; set; }

        public ProjectStatus Status { get; set; }
    }
}
