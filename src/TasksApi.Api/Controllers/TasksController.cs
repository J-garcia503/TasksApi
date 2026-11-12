
using Microsoft.AspNetCore.Mvc;
using TasksApi.Application.Interfaces;
using TaskEntity = TasksApi.Domain.Entities.Task;

namespace TasksApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskEntity>), 200)]
        public async Task<ActionResult<IEnumerable<TaskEntity>>> GetTasks()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskEntity), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TaskEntity>> GetTask(int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TaskEntity), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<TaskEntity>> CreateTask(TaskEntity task)
        {
            var createdTask = await _taskService.CreateAsync(task);

            return CreatedAtAction(
                nameof(GetTask),
                new { id = createdTask.Id },
                createdTask
            );
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateTask(int id, TaskEntity task)
        {
            if (id != task.Id)
                return BadRequest(new { message = "El ID de la URL no coincide con el ID de la tarea." });

            var updated = await _taskService.UpdateAsync(id, task);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}