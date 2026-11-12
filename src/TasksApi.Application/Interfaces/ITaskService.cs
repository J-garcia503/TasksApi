using TasksApi.Application.DTOs;

namespace TasksApi.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TaskDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<TaskDto> CreateAsync(CreateTaskDto dto, CancellationToken cancellationToken = default);

        Task<bool> UpdateAsync(int id, UpdateTaskDto dto, CancellationToken cancellationToken = default);

        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
