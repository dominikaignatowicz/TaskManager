namespace TaskManager.Server.Models
{
    public class TaskAssignment
    {
        public Guid TaskId { get; set; }
        public TaskItem Task { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime AssigmentAt { get; set; } = DateTime.UtcNow;
    }
}

