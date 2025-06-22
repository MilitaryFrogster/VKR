using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VKR
{
    public class SyncTaskManager
    {
        private List<SyncTask> tasks = new List<SyncTask>();
        private Timer timer;

        public event Action<SyncTask> TaskExecuted;

        public void AddTask(SyncTask task)
        {
            task.LastRun = DateTime.Now;
            tasks.Add(task);
            ExecuteTask(task); // запуск сразу
            SyncTaskStorage.SaveTasks(tasks);

        }

        public void RemoveTask(string name)
        {
            tasks = tasks.Where(t => t.Name != name).ToList();
            SyncTaskStorage.SaveTasks(tasks);

        }
        public void LoadAllTasks()
        {
            tasks = SyncTaskStorage.LoadTasks();
        }

        public List<SyncTask> GetTasks() => tasks;

        public void Start()
        {
            timer = new Timer(_ => CheckAndRunTasks(), null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private async void CheckAndRunTasks()
        {
            foreach (var task in tasks)
            {
                if (DateTime.Now >= task.NextRun)
                {
                    await SyncEngine.ExecuteTaskAsync(task);

                    task.LastRun = DateTime.Now;
                    task.NextRun = CalculateNextRun(task);

                    TaskExecuted?.Invoke(task);
                }
            }

            SyncTaskStorage.SaveTasks(tasks);
        }

        private DateTime CalculateNextRun(SyncTask task)
        {
            if (task.Schedule == "Каждый час")
                return task.LastRun.AddHours(1);
            if (task.Schedule == "Каждые 3 часа")
                return task.LastRun.AddHours(3);
            if (task.Schedule == "Каждый день")
                return task.LastRun.AddDays(1);
            if (task.Schedule == "Каждую неделю")
                return task.LastRun.AddDays(7);
            if (task.Schedule == "Каждое первое число месяца")
                return new DateTime(task.LastRun.Year, task.LastRun.Month, 1).AddMonths(1);

            return task.LastRun.AddHours(1); // по умолчанию
        }



        public List<SyncTask> GetAllTasks()
        {
            return tasks.ToList(); // или tasks.Values.ToList(), если используется словарь
        }

        private void ExecuteTask(SyncTask task)
        {
            // Здесь вызывается синхронизация
            // Пример: SyncEngine.Sync(task.SourcePath, task.DestinationPath, task.UseEncryption);
            TaskExecuted?.Invoke(task);
        }

        public SyncTask GetTaskByName(string name)
        {
            return tasks.FirstOrDefault(t => t.Name == name);
        }

    }
}
