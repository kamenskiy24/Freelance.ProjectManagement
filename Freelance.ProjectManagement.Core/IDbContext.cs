using Freelance.ProjectManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Task = Freelance.ProjectManagement.Core.Entities.Task;

namespace Freelance.ProjectManagement.Core
{
    public interface IDbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
