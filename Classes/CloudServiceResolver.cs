using System;
using System.Collections.Generic;
using System.IO;

namespace VKR
{
    public static class CloudServiceResolver
    {
        public static ResolvedPath Resolve(string rawPath, string absolutePath = null)
        {
            if (rawPath.StartsWith("Компьютер:"))
            {
                string localPath = absolutePath ?? rawPath.Substring("Компьютер:".Length).Trim();
                return new ResolvedPath(null, true, localPath);
            }

            if (rawPath.StartsWith("Google Drive:"))
            {
                string folderId = absolutePath ?? rawPath.Substring("Google Drive:".Length).Trim();
                var acc = AccountStorage.FindByCloud("Google Drive");
                if (acc != null)
                {
                    var service = CloudServiceFactory.Create(acc);
                    return new ResolvedPath(service, false, folderId);
                }
                else
                {
                    throw new Exception("Аккаунт Google Drive не найден.");
                }
            }

            if (rawPath.StartsWith("Dropbox:"))
            {
                string path = absolutePath ?? rawPath.Substring("Dropbox:".Length).Trim();
                var acc = AccountStorage.FindByCloud("Dropbox");
                if (acc != null)
                {
                    var service = CloudServiceFactory.Create(acc);
                    return new ResolvedPath(service, false, path);
                }
                else
                {
                    throw new Exception("Аккаунт Dropbox не найден.");
                }
            }

            throw new Exception("Не удалось распознать путь: " + rawPath);

        }
    }

}
