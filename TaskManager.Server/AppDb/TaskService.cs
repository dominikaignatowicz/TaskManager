using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Server.DTOs;
using TaskManager.Server.Models;

namespace TaskManager.Server.AppDb
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }
        public async Task<TaskItem> CreateTaskAsync(TaskItem task, List<Guid> userIds)
        {
            _context.Tasks.Add(task);

            var assignments = userIds.Select(userId => new TaskAssignment
            {
                TaskId = task.Id,
                UserId = userId
            });

            await _context.TaskAssignments.AddRangeAsync(assignments);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<bool> UpdateTaskAsync(Guid taskId, CreateTaskDto dto)
        {
            var task = await _context.Tasks
                .Include(t => t.TaskAssignments)
                .FirstOrDefaultAsync(t => t.Id == taskId);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.DueDate = dto.DueDate;
            task.Status = dto.Status;
            task.Priority = dto.Priority;

            _context.TaskAssignments.RemoveRange(task.TaskAssignments);

            var newAssignment = dto.AssignedUserIds.Select(userId => new TaskAssignment
            {
                TaskId = taskId,
                UserId = userId
            });

            await _context.TaskAssignments.AddRangeAsync(newAssignment);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<TaskItem>> GetTasksForUserAsync(Guid userId)
        {
            return await _context.TaskAssignments
                .Where(ta => ta.UserId == userId)
                .Select(ta => ta.Task)
                .ToListAsync();
        }

    }
}
