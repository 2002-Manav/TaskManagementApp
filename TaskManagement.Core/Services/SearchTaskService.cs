using TaskManagement.Core.DTOs;
using TaskManagement.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class SearchTaskService : ISearchTaskService
{
    private readonly ITaskRepository _taskRepository;

    public SearchTaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Service to search a task using taskId
    /// </summary>
    /// <param name="keyword"></param>
    /// <returns></returns>
    public async Task<List<TaskResponse>> SearchTasks(string keyword)
    {
        
        var tasks = await _taskRepository.SearchTasksAsync(keyword);

        return tasks.Select(task => new TaskResponse
        {
            TaskId = task.TaskId,
            Title = task.Title,
            Description = task.Description,
            AssignedUserId = task.AssignedUserId,
            DueDate = task.DueDate,
            Status = task.Status.ToString() 
        }).ToList();
    }
}
