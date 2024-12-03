using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;

namespace TaskManagement.Core.Interfaces
{
    public interface ICreateTaskService
    {
       
        /// <summary>
        /// Create a task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<int> CreateTask(TaskRequest request);
    }
}
