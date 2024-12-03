using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Core.Domain.Entities
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(300, ErrorMessage = "Description cannot exceed 300 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "AssignedUserId is required.")]
        public Guid AssignedUserId { get; set; }

        [Required(ErrorMessage = "Due date is required.")]
        [DataType(DataType.Date, ErrorMessage = "DueDate should be in a valid date format.")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; }
    }
}
