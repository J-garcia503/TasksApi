

using TaskEntity = TasksApi.Domain.Entities.Task;

namespace TasksApi.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntity>> GetAllAsync();

        Task<TaskEntity?> GetByIdAsync(int id);

        Task<TaskEntity> CreateAsync(TaskEntity task);

        Task<bool> UpdateAsync(int id, TaskEntity task);

        Task<bool> DeleteAsync(int id);
    }
}




