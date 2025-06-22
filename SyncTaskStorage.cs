using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace VKR
{
    public static class SyncTaskStorage
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sync_tasks.json");

        public static void SaveTasks(List<SyncTask> tasks)
        {
            var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        public static List<SyncTask> LoadTasks()
        {
            if (!File.Exists(FilePath))
                return new List<SyncTask>();

            var json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<SyncTask>>(json) ?? new List<SyncTask>();
        }
    }
}
