using TaskManager.Server.Models;

namespace TaskManager.Server.DTOs
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public Models.TaskStatus Status { get; set; } = Models.TaskStatus.InProgess;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public List<Guid> AssignedUserIds { get; set; } = new();
    }
}
