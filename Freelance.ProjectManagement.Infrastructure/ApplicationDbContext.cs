using Freelance.ProjectManagement.Core;
using Freelance.ProjectManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Task = Freelance.ProjectManagement.Core.Entities.Task;

namespace Freelance.ProjectManagement.Infrastructure
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // make sure the database is created
        }
    }
}
