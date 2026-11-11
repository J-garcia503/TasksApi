
using Microsoft.AspNetCore.Mvc;
using TasksApi.Services;
using TaskModel = TasksApi.Models.Task;

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


        [ProducesResponseType(
            typeof(IEnumerable<TaskModel>),
            StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetTasks()
        {
            var tasks = await _taskService.GetAllAsync();

            return Ok(tasks);
        }

        [ProducesResponseType(
            typeof(TaskModel),
                StatusCodes.Status200OK)]
                [ProducesResponseType(
                StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetTask(int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [ProducesResponseType(
            typeof(TaskModel),
            StatusCodes.Status201Created)]
            [ProducesResponseType(
            StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<TaskModel>> CreateTask(TaskModel task)
        {
            var createdTask = await _taskService.CreateAsync(task);

            return CreatedAtAction(
                nameof(GetTask),
                new { id = createdTask.Id },
                createdTask
            );
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, TaskModel task)
        {
            if (id != task.Id)
            {
                return BadRequest(new
                {
                    message = "El ID de la URL no coincide con el ID de la tarea."
                });
            }

            var updated = await _taskService.UpdateAsync(id, task);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _taskService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}


