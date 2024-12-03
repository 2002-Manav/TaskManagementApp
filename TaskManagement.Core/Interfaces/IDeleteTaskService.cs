using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.Interfaces
{
    public interface IDeleteTaskService
    {
        /// <summary>
        /// Delete a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        Task<bool> DeleteTaskAsync(int taskId);
    }
}
