using TasksApi.Application.DTOs;
using TasksApi.Application.Interfaces;
using TasksApi.Domain.Entities;
using TasksApi.Domain.Repositories;

namespace TasksApi.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;

        public TaskService(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var tasks = await _repository.GetAllAsync(cancellationToken);
            return tasks.Select(ToDto);
        }

        public async Task<TaskDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var task = await _repository.GetByIdAsync(id, cancellationToken);
            return task is null ? null : ToDto(task);
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken = default)
        {
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = dto.IsCompleted
            };

            await _repository.AddAsync(task, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return ToDto(task);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskDto dto, CancellationToken cancellationToken = default)
        {
            var task = await _repository.GetByIdAsync(id, cancellationToken);

            if (task is null)
                return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.IsCompleted = dto.IsCompleted;

            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var task = await _repository.GetByIdAsync(id, cancellationToken);

            if (task is null)
                return false;

            _repository.Remove(task);
            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }

        private static TaskDto ToDto(TaskItem task) => new()
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            IsCompleted = task.IsCompleted,
            CreatedAt = task.CreatedAt
        };
    }
}
