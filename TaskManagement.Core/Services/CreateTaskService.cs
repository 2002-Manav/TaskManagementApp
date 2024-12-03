using TaskManagement.Core.DTOs;
using TaskManagement.Core.Interfaces;
using Task = TaskManagement.Core.Domain.Entities.Tasks;

namespace TaskManagement.Core.Services
{
    public class CreateTaskService : ICreateTaskService
    {
        private readonly ITaskRepository _taskRepository;
        public CreateTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// Service to create a task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        async Task<int> ICreateTaskService.CreateTask(TaskRequest request)
        {
            var newTask = new Task
            {
                Title = request.Title,
                Description = request.Description,
                AssignedUserId = (Guid)request.AssignedUserId,
                DueDate = (DateTime)request.DueDate,
                Status = "Pending"
            };

            return await _taskRepository.CreateTaskAsync(newTask);
        }
    }
}
