using TaskManagement.Core.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaskManagement.Infrastructure.Services
{
    public class DeleteTaskService : IDeleteTaskService
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        /// <summary>
        /// Service to Delete a task using taskId
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var existingTask = await _taskRepository.GetTaskByIdAsync(taskId);

            // if not found
            if (existingTask == null)
                return false;

            try
            {
                // return result
                return await _taskRepository.DeleteTaskAsync(taskId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the task.", ex);
            }
        }

 
    }
}
