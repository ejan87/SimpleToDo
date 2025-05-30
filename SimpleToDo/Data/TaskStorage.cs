﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SimpleToDo.Models;
using System.Collections.ObjectModel;

namespace SimpleToDo.Data
{
    public class TaskStorage
    {
        private const string FileName = "tasks.json";

        public static ObservableCollection<TaskItem> LoadTasks()
        {
            if (!File.Exists(FileName))
                return new ObservableCollection<TaskItem>();

            var json = File.ReadAllText(FileName);
            return JsonConvert.DeserializeObject<ObservableCollection<TaskItem>>(json) ?? new ObservableCollection<TaskItem>();
        }

        public static void SaveTasks(ObservableCollection<TaskItem> tasks)
        {
            var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(FileName, json);
        }
    }
}
