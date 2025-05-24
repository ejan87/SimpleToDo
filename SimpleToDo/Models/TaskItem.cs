using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleToDo.Models
{
    public class TaskItem
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public override string ToString()
        {
            return $"{(IsCompleted ? "[X]" : "[ ]")} {Title} - {DueDate:dd.MM.yyyy}";
        }
    }
}
