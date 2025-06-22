using Quartz;
using System;
using System.Windows;
using Quartz;


namespace VKR
{
    public static class CronUtils
    {
        public static ITrigger CreateTrigger(string schedule)
        {
            string cron;

            switch (schedule)
            {
                case "Каждый час":
                    cron = "0 0 * ? * *"; break;
                case "Каждые 3 часа":
                    cron = "0 0 0/3 ? * *"; break;
                case "Каждый день":
                    cron = "0 0 12 * * ?"; break;
                case "Каждую неделю":
                    cron = "0 0 12 ? * MON"; break;
                case "Каждое первое число месяца":
                    cron = "0 0 12 1 * ?"; break;
                default:
                    cron = "0 0 * ? * *"; break;
            }

            return TriggerBuilder.Create()
                .WithCronSchedule(cron)
                .Build();
        }
    }
}
