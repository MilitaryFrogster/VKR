using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VKR
{
    public static class AccountStorage
    {
        private static readonly string _file = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory, "accounts.json");
        private static List<ConnectedAccount> _cache;

        public static List<ConnectedAccount> LoadAccounts()
        {
            if (_cache != null) return _cache;
            if (File.Exists(_file))
            {
                var txt = File.ReadAllText(_file);
                _cache = JsonConvert.DeserializeObject<List<ConnectedAccount>>(txt)
                         ?? new List<ConnectedAccount>();
            }
            else
            {
                _cache = new List<ConnectedAccount>();
            }
            return _cache;
        }

        public static void Save()
        {
            var txt = JsonConvert.SerializeObject(_cache, Formatting.Indented);
            File.WriteAllText(_file, txt);
        }

        public static void Add(ConnectedAccount acc)
        {
            LoadAccounts().Add(acc);
            Save();
        }

        // Добавлено: метод SaveAccounts
        public static void SaveAccounts(List<ConnectedAccount> accounts)
        {
            _cache = accounts;
            Save();
        }
        public static ConnectedAccount FindByCloud(string cloud)
        {
            var accounts = LoadAccounts();
            return accounts.FirstOrDefault(a => a.Cloud == cloud);
        }

    }
}
