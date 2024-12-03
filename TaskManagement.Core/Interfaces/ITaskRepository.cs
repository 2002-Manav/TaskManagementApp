using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Core.Domain.Entities;

public interface ITaskRepository
{
    /// <summary>
    /// to get all tasks
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Tasks>> GetTasksAsync();

    /// <summary>
    /// to get user by UserId
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    Task<Tasks?> GetTaskByIdAsync(int taskId);

    /// <summary>
    /// to search a task
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    Task<IEnumerable<Tasks>> SearchTasksAsync(string keyword);

    /// <summary>
    /// to update user by UserId
    /// </summary>
    /// <param name="existingTask"></param>
    /// <returns></returns>
    Task<bool> UpdateTaskAsync(Tasks existingTask);

    /// <summary>
    /// delete a user by UserId
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    Task<bool> DeleteTaskAsync(int taskId); 

    /// <summary>
    /// create a user by UserId
    /// </summary>
    /// <param name="newTask"></param>
    /// <returns></returns>
    Task<int> CreateTaskAsync(Tasks newTask);
}
