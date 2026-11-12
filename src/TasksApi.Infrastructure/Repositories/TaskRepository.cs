using Microsoft.EntityFrameworkCore;
using TasksApi.Domain.Entities;
using TasksApi.Domain.Repositories;
using TasksApi.Infrastructure.Persistence;

namespace TasksApi.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Tasks.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
        {
            await _context.Tasks.AddAsync(task, cancellationToken);
        }

        public void Remove(TaskItem task)
        {
            _context.Tasks.Remove(task);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
