using TaskModel = TasksApi.Models.Task;

namespace TasksApi.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskModel>> GetAllAsync();

        Task<TaskModel?> GetByIdAsync(int id);

        Task<TaskModel> CreateAsync(TaskModel task);

        Task<bool> UpdateAsync(int id, TaskModel task);

        Task<bool> DeleteAsync(int id);
    }
}

