using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleToDo.Models;

namespace SimpleToDo.Data
{
    public class TaskStorage
    {
        private const string FileName = "tasks.json";

        public static List<TaskItem> LoadTasks()
        {
            if (!File.Exists(FileName))
                return new List<TaskItem>();

            var json = File.ReadAllText(FileName);
            return JsonConvert.DeserializeObject<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        public static void SaveTasks(List<TaskItem> tasks)
        {
            var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }
    }
}
