
using Microsoft.EntityFrameworkCore;
using TaskEntity = TasksApi.Domain.Entities.Task;

namespace TasksApi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }
    }
}



