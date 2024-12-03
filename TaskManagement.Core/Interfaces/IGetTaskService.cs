using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;

namespace TaskManagement.Core.Interfaces
{
    public interface IGetTaskService
    {
        /// <summary>
        /// Get a task
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<List<TaskResponse>> GetTasks(Guid? userId = null, string? status = null);
    }
}
