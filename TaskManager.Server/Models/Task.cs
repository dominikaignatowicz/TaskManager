namespace TaskManager.Server.Models
{
    public class Task
    {
        public Guid Id {  get; set; } =Guid.NewGuid();  
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate {  get; set; }
        public string Status { get; set; }
    }
}
