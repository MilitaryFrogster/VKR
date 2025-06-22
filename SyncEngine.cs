using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace VKR
{
    public static class SyncEngine
    {
        public static async Task ExecuteTaskAsync(SyncTask task)
        {
            try
            {
                var source = CloudServiceResolver.Resolve(task.SourcePath, task.SourceAbsolutePath);
                var destination = CloudServiceResolver.Resolve(task.DestinationPath, task.DestinationAbsolutePath);

                Logger.Log($"Source resolved: isComputer={source.IsComputer}, path={source.Path}");
                Logger.Log($"Destination resolved: isComputer={destination.IsComputer}, path={destination.Path}");

                var sourcePath = source.Path;
                var destinationPath = destination.Path;
                var destinationService = destination.Service;

                Logger.Log($"Начата задача {task.Name}: {sourcePath} -> {destinationPath}");

                if (File.Exists(sourcePath))
                {
                    Logger.Log($"Файл: {sourcePath}");
                    await destinationService.UploadFileAsync(sourcePath, destinationPath, task.UseEncryption);
                }
                else if (Directory.Exists(sourcePath))
                {
                    Logger.Log("Обнаружена папка. Начинаем загрузку содержимого...");
                    Logger.Log($"Parent ID для загрузки: {destinationPath}");

                    var existingFiles = await destinationService.ListFilesAsync(destinationPath);
                    var existingFileNames = new HashSet<string>(existingFiles.Where(f => !f.IsFolder).Select(f => f.Name));

                    foreach (var file in Directory.GetFiles(sourcePath))
                    {
                        string fileName = Path.GetFileName(file);

                        if (existingFileNames.Contains(fileName))
                        {
                            var targetFile = existingFiles.FirstOrDefault(f => f.Name == fileName);
                            if (targetFile != null)
                            {
                                var remoteBytes = await destinationService.DownloadFileAsync(targetFile.Id);
                                var remoteHash = ComputeMD5(remoteBytes);
                                var localHash = ComputeMD5(File.ReadAllBytes(file));

                                if (localHash == remoteHash)
                                {
                                    Logger.Log($"Пропущен идентичный файл: {fileName}");
                                    continue;
                                }
                                else
                                {
                                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");

                                    string nameWithoutExt = Path.GetFileNameWithoutExtension(file);
                                    string ext = Path.GetExtension(file);
                                    string newFileName = $"{nameWithoutExt}_{timestamp}{ext}";

                                    string tempPath = Path.Combine(Path.GetTempPath(), newFileName);
                                    File.Copy(file, tempPath, true);

                                    Logger.Log($"Загрузка переименованного файла: {newFileName}");
                                    await destinationService.UploadFileAsync(tempPath, destinationPath, task.UseEncryption);

                                    File.Delete(tempPath);
                                    continue;
                                }
                            }
                        }

                        Logger.Log($"Загрузка файла: {fileName}");
                        await destinationService.UploadFileAsync(file, destinationPath, task.UseEncryption);
                    }

                }
                else
                {
                    Logger.Log("Источник не найден: " + sourcePath);
                    throw new NotSupportedException("Этот тип синхронизации пока не поддерживается.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Ошибка синхронизации задачи " + task.Name + ": " + ex.Message);
            }
        }

        private static string ComputeMD5(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
}
