using System;
using System.IO;
using System.Windows.Forms;

namespace VKR
{
    public static class Logger
    {
        private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sync_log.txt");

        public static void Log(string message)
        {
            try
            {
                MessageBox.Show("Лог: " + LogFilePath);

                File.AppendAllText(LogFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} — {message}\n");
            }
            catch { /* игнорируем */ }
        }
    }
}
