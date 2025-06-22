using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;



namespace VKR
{
    public static class BackgroundSyncRunner
    {
        public static async void Run()
        {
            try
            {
                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                await scheduler.Start();

                var tasks = SyncApp.TaskManager.GetAllTasks();

                foreach (var task in tasks)
                {
                    var job = JobBuilder.Create<SyncTaskJob>()
                        .WithIdentity(task.Name)
                        .UsingJobData("taskName", task.Name)
                        .Build();

                    var trigger = CronUtils.CreateTrigger(task.Schedule);

                    await scheduler.ScheduleJob(job, trigger);
                }

                // ⏳ Важно: блокируем поток, чтобы приложение не завершилось
                await Task.Delay(-1);
            }
            catch (Exception ex)
            {
                // Можно залогировать, если есть логгер
            }
        }
    }

}
