using Microsoft.EntityFrameworkCore;
using TaskManager.Server.Models;

namespace TaskManager.Server.AppDb
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskAssignment>()
                .HasKey(t => new {t.UserId, t.TaskId});
            modelBuilder.Entity<TaskAssignment>()
                .HasOne(t => t.Task)
                .WithMany(ta => ta.TaskAssignments)
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade); 
            modelBuilder.Entity<TaskAssignment>()
                .HasOne(t => t.User)
                .WithMany((System.Linq.Expressions.Expression<Func<User, IEnumerable<TaskAssignment>?>>?)(u => u.TaskAssigments))
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TaskItem>()
                .Property(ta => ta.Status)
                .HasConversion<string>();
            modelBuilder.Entity<TaskItem>()
                .Property(ta => ta.Priority)
                .HasConversion<string>();
        }

    }
}
