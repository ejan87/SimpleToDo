using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.Models
{
    public class TaskItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem()
        {
            Title = string.Empty;
            Description = string.Empty;
        }

        public override string ToString()
        {
            return $"{(IsCompleted ? "[X]" : "[ ]")} {Title} - {DueDate:dd.MM.yyyy}";
        }
    }
}
