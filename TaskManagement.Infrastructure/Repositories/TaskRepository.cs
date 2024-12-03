using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Domain.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.DatabaseContext;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        // Constructor
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// create task by taskId
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns></returns>
        public async Task<int> CreateTaskAsync(Tasks newTask)
        {
            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();
            return newTask.TaskId; // Return the TaskId after creating the task
        }

        /// <summary>
        /// delete a task by taskId
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                return await _context.SaveChangesAsync() > 0;
            }
            return false; 
        }

        /// <summary>
        /// get a task by taskId
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<Tasks?> GetTaskByIdAsync(int taskId)
        {
            return await _context.Tasks.FindAsync(taskId); // Get the task by TaskId
        }

        /// <summary>
        /// get all tasks 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Tasks>> GetTasksAsync()
        {
            return await _context.Tasks.ToListAsync(); // Return all tasks
        }

        /// <summary>
        /// search a task using keyword
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Tasks>> SearchTasksAsync(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return await _context.Tasks.ToListAsync(); 
            }

            // Filter tasks with keyword 
            return await _context.Tasks
                .Where(t => t.Title.Contains(keyword) || t.Description.Contains(keyword))
                .ToListAsync();
        }

        /// <summary>
        /// update a task by taskId
        /// </summary>
        /// <param name="existingTask"></param>
        /// <returns></returns>
        public async Task<bool> UpdateTaskAsync(Tasks existingTask)
        {
            _context.Tasks.Update(existingTask);
            // Return true when updates
            return await _context.SaveChangesAsync() > 0; 
        }
    }
}
