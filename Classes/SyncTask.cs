using System;

namespace VKR
{
    public class SyncTask
    {
        public string Name { get; set; } // Название задачи
        public string SourcePath { get; set; } // Откуда
        public string DestinationPath { get; set; } // Куда
        public string Schedule { get; set; } // Расписание, например "1 час", "1 день"
        public bool UseEncryption { get; set; } // Включено ли шифрование
        public DateTime LastRun { get; set; } // Последнее выполнение
        public DateTime NextRun { get; set; }
        public string SourceAbsolutePath { get; set; } // Абсолютный путь к папке на ПК
        public string DestinationAbsolutePath { get; set; } // Абсолютный путь (если приёмник — ПК)




        public TimeSpan GetInterval()
        {
            switch (Schedule)
            {
                case "Каждый час":
                    return TimeSpan.FromHours(1);
                case "Каждые 3 часа":
                    return TimeSpan.FromHours(3);
                case "Каждый день":
                    return TimeSpan.FromDays(1);
                default:
                    return TimeSpan.FromHours(1);
            }
        }

    }
}
