using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;

namespace TaskManagement.Core.Interfaces
{
    public interface ISearchTaskService
    {
        /// <summary>
        /// Search a task
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<List<TaskResponse>> SearchTasks(string keyword);
    }
}
