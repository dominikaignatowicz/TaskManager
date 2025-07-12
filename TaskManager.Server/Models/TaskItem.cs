namespace TaskManager.Server.Models
{
    public class TaskItem
    {
        public Guid Id {  get; set; } =Guid.NewGuid();  
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate {  get; set; } 
        public TaskStatus Status { get; set; } = TaskStatus.InProgess;
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;

        public Guid CreatedByUserId { get; set; }
        public User CreatedBy {  get; set; }

        public ICollection<TaskAssignment> TaskAssignments { get; set; }
    }
}
