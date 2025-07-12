using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.Server.AppDb;
using TaskManager.Server.DTOs;
using TaskManager.Server.Models;

namespace TaskManager.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        public TaskController(TaskService taskService, UserService userService)
        {
            _taskService = taskService;
            _userService = userService;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
        [Authorize]
        [HttpGet("myTasks")]
        public async Task<ActionResult<List<TaskItem>>> GetUserTasksAll()
        {
            var userId = GetUserId();
            var tasks = await _taskService.GetTasksForUserAsync(userId);
            return Ok(tasks);   

        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskById(Guid id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return Ok(task);
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<TaskItem>> CreateTask([FromBody] CreateTaskDto dto) {
            var userId = GetUserId();
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                DueDate = dto.DueDate,
                Priority = dto.Priority,
                Status = dto.Status,

            };
            
            dto.AssignedUserIds.Add(userId);
            var created = await _taskService.CreateTaskAsync(task, dto.AssignedUserIds);
            return Ok(created); 
            
        }
        [Authorize]
        [HttpPost("{id}")]
        public async Task<ActionResult<TaskItem>> EditTask(Guid taskId, [FromBody] CreateTaskDto dto)
        {
            var userId = GetUserId();
            var task = await _taskService.UpdateTaskAsync(taskId, dto);
            if (!task) return NotFound();
            return Ok(task);
        }
    }
}
