using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace VKR
{
    public static class StartupHelper
    {
        private const string AppName = "CloudSyncApp";

        public static void RegisterInStartup()
        {
            try
            {
                string exePath = Application.ExecutablePath;
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key.GetValue(AppName) == null)
                {
                    key.SetValue(AppName, $"\"{exePath}\" --background");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка добавления в автозагрузку: " + ex.Message);
            }
        }
    }
}
