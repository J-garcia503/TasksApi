using System.ComponentModel.DataAnnotations;

namespace TasksApi.DTOs
{
    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
    }
}

