using Quartz;
using System;
using System.Threading.Tasks;



namespace VKR
{
    public class SyncTaskJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string taskName = context.JobDetail.JobDataMap.GetString("taskName");
            var task = SyncApp.TaskManager.GetTaskByName(taskName);

            if (task != null)
            {
                try
                {
                    await SyncEngine.ExecuteTaskAsync(task);
                    SyncTaskStorage.SaveTasks(SyncApp.TaskManager.GetTasks()); // сохранить LastRun
                }
                catch (Exception ex)
                {
                    Logger.Log($"Ошибка при выполнении фоновой задачи '{taskName}': {ex.Message}");
                }
            }
            else
            {
                Logger.Log($"Фоновая задача не найдена: {taskName}");
            }
        }



    }
}
