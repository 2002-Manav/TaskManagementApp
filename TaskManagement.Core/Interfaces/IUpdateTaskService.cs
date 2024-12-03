using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Services;

namespace TaskManagement.Core.Interfaces
{
    public interface IUpdateTaskService
    {
        /// <summary>
        /// Update a task
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<bool> UpdateTask(TaskRequest request);
    }
}
