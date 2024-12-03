using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.Core.Services
{
    public class UpdateTaskService : IUpdateTaskService
    {
        private readonly ITaskRepository _taskRepository;

        public UpdateTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Service to update a task using taskId
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<bool> UpdateTask(TaskRequest request)
        {
            // Validate TaskId 
            var existingTask = await _taskRepository.GetTaskByIdAsync(request.TaskId);
            if (existingTask == null)
                throw new KeyNotFoundException("Task not found.");

            // Validate fields
            if (string.IsNullOrWhiteSpace(request.Title))
                throw new ArgumentException("Title cannot be null or empty.");

            if (!request.AssignedUserId.HasValue || request.AssignedUserId == Guid.Empty)
                throw new ArgumentException("AssignedUserId is required.");

            if (!request.DueDate.HasValue || request.DueDate <= DateTime.MinValue)
                throw new ArgumentException("Valid DueDate is required.");

            //mapping
            existingTask.Title = request.Title;
            existingTask.Description = request.Description;
            existingTask.AssignedUserId = request.AssignedUserId.Value;
            existingTask.DueDate = request.DueDate.Value;

         
            return await _taskRepository.UpdateTaskAsync(existingTask);
        }
    }
}
