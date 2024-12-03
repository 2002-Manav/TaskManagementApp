using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Core.DTOs
{
    public class TaskStatusRequest
    {
        //properties
        public int TaskId { get; set; }
        public string Status { get; set; }

        //cosntructer
        public TaskStatusRequest(int taskId, string status) { 
        
            TaskId = taskId;
            Status = status;
        }
    }
}
