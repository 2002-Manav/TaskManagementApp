using System;

namespace TaskManagement.Core.DTOs
{
    public class TaskRequest
    {
        // Properties 
        public int TaskId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? AssignedUserId { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }

        // constructor
        public TaskRequest(int taskId, string title, string description, Guid? assignedUserId, DateTime? dueDate, string status)
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
