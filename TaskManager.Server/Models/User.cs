namespace TaskManager.Server.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string? Login {  get; set; }
        public ICollection<TaskAssignment> TaskAssigments { get; set; }

    }
}
