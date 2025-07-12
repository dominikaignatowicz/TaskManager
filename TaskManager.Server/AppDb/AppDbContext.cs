using Microsoft.EntityFrameworkCore;
using TaskManager.Server.Models;

namespace TaskManager.Server.AppDb
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskAssigment> TaskAssigments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskAssigment>()
                .HasKey(t => new {t.UserId, t.TaskId});
            modelBuilder.Entity<TaskAssigment>()
                .HasOne(t => t.Task)
                .WithMany(ta => ta.TaskAssigments)
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade); 
            modelBuilder.Entity<TaskAssigment>()
                .HasOne(t => t.User)
                .WithMany(u => u.TaskAssigments)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
