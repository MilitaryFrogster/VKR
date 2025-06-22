using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace VKR
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            StartupHelper.RegisterInStartup();

            if (args.Contains("--background"))
            {
                // Запускаем фоновый режим без UI
                SyncApp.TaskManager.LoadAllTasks();
                SyncApp.TaskManager.Start();
                BackgroundSyncRunner.Run();
                return;
            }
            SyncApp.TaskManager.LoadAllTasks();
            if (KeyStorage.KeyFilesExist() && KeyStorage.TryLoadMasterKey())
            {
                Application.Run(new Form2()); // основная форма
            }
            else
            {
                Application.Run(new Form1()); // форма ввода пароля
            }
        }
    }
}
