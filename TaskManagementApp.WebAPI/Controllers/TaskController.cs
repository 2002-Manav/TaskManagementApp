using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Interfaces;
using System.Threading.Tasks;
using System.Linq;
using TaskManagement.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // Get all tasks
        /// <summary>
        /// To Get All Tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTasks([FromQuery] Guid? userId, [FromQuery] string? status)
        {
            var tasks = await _taskRepository.GetTasksAsync();
            return Ok(tasks);
        }

        // Create new task
        /// <summary>
        /// To create a task for a User
        /// </summary>
        /// <param name="taskRequest"></param>
        /// <returns></returns>
        /// 
        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTask([FromBody] TaskRequest taskRequest)
        {
            if (taskRequest == null)
            {
                return BadRequest("Task data is required.");
            }



            Tasks task = new Tasks
            {
                Title = taskRequest.Title,
                Description = taskRequest.Description,
                AssignedUserId = taskRequest.AssignedUserId.GetValueOrDefault(),
                DueDate = taskRequest.DueDate.GetValueOrDefault(),
                Status = taskRequest.Status

            };

            var result = await _taskRepository.CreateTaskAsync(task);
            if (result > 0)
            {
                return CreatedAtAction(nameof(GetTaskById), new { taskId = result }, taskRequest);
            }

            return BadRequest("Task could not be created.");
        }

        // Get a task by ID
        /// <summary>
        /// To get a task by taskId
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            var task = await _taskRepository.GetTaskByIdAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // Update an existing task
        /// <summary>
        /// To update a task using taskId
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="taskRequest"></param>
        /// <returns></returns>
        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] TaskRequest taskRequest)
        {
            if (taskRequest == null || taskId != taskRequest.TaskId)
            {
                return BadRequest("Task ID mismatch.");
            }

           

            Tasks task = new Tasks
            {
                TaskId = taskRequest.TaskId,
                Title = taskRequest.Title,
                Description = taskRequest.Description,
                AssignedUserId = taskRequest.AssignedUserId.GetValueOrDefault(), 
                DueDate = taskRequest.DueDate.GetValueOrDefault(), 
                Status = taskRequest.Status
            };

            var result = await _taskRepository.UpdateTaskAsync(task);
            if (result)
            {
                return NoContent();
            }

            return NotFound("Task not found.");
        }

        // Delete a task by ID
        /// <summary>
        /// To delete a task using taskId
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            var result = await _taskRepository.DeleteTaskAsync(taskId);
            if (result)
            {
                return NoContent();
            }

            return NotFound("Task not found.");
        }

        // Search tasks by keyword
        /// <summary>
        /// To Search a task by a keyword(from taskname or decription)
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchTasks([FromQuery] string keyword)
        {
            var tasks = await _taskRepository.SearchTasksAsync(keyword);
            if (tasks == null || !tasks.Any())
            {
                return NotFound("No tasks found.");
            }

            return Ok(tasks);
        }

        
    }
}
