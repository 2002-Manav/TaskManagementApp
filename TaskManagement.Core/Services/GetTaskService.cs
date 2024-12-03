using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.Services
{
    public class GetTaskService : IGetTaskService
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Service to get all tasks
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<List<TaskResponse>> GetTasks(Guid? userId = null, string? status = null)
        {
            var tasks = await _taskRepository.GetTasksAsync();

            // Filter by userId &  status
            if (userId.HasValue)
            {
                tasks = tasks.Where(task => task.AssignedUserId == userId.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                tasks = tasks.Where(task =>
                    task.Status.ToString().Equals(status, StringComparison.OrdinalIgnoreCase));
            }

            // Convert to TaskResponse DTO
            return tasks.Select(task => new TaskResponse
            {
                TaskId = task.TaskId,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                AssignedUserId = task.AssignedUserId,
                Status = task.Status.ToString() 
            }).ToList();
        }

  
    }
}
