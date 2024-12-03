using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.DTOs
{
    public class TaskResponse
    {
        public int TaskId { get; set; } 
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid AssignedUserId { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }

       
        public TaskResponse() { }

      
        public TaskResponse(int taskId, string title, string description, Guid assignedUserId, DateTime? dueDate, string status)
        {
            TaskId = taskId;
            Title = title;
            Description = description;
            AssignedUserId = assignedUserId;
            DueDate = dueDate;
            Status = status;
        }
    }

}
