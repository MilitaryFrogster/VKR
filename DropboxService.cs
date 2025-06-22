using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VKR
{
    public class DropboxService : ICloudService
    {
        private readonly DropboxClient _client;

        public string CloudName => "Dropbox";

        public DropboxService(string accessToken)
        {
            _client = new DropboxClient(accessToken);
        }

        public async Task<List<CloudFile>> ListFilesAsync(string path)
        {

            Console.WriteLine("ListFilesAsync path = " + path);

            if (string.IsNullOrWhiteSpace(path) || path == "root")
                path = string.Empty;
            else if (!path.StartsWith("/"))
                path = "/" + path;

            var result = await _client.Files.ListFolderAsync(path);
            var list = new List<CloudFile>();

            foreach (var entry in result.Entries)
            {
                list.Add(new CloudFile
                {
                    Id = entry.PathLower,   // ✅ Используй PathLower — оно есть и у файлов и у папок
                    Name = entry.Name,
                    IsFolder = entry.IsFolder,
                    Path = entry.PathDisplay, // для открытия вложенных папок
                    Size = entry.IsFile ? (long)entry.AsFile.Size : 0
                }) ;
            }

            return list;
        }

        public async Task<Stream> OpenDownloadStreamAsync(string fileId)
        {
            var response = await _client.Files.DownloadAsync(fileId);
            return await response.GetContentAsStreamAsync();
        }



        public async Task UploadFileAsync(string localPath, string parentId, bool encrypt)
        {
            string uploadName = Path.GetFileName(localPath);
            Logger.Log($"Dropbox: Загрузка файла {localPath} (encrypt={encrypt}) в {parentId}");


            byte[] content;

            if (encrypt)
            {
                uploadName += ".enc";
                content = EncryptionHelper.EncryptFile(localPath);
            }
            else
            {
                content = File.ReadAllBytes(localPath);
            }

            using (var mem = new MemoryStream(content))
            {
                string dropboxPath = (string.IsNullOrWhiteSpace(parentId) || parentId == "root")
                ? $"/{uploadName}"
                : $"{parentId.TrimEnd('/')}/{uploadName}";

                await _client.Files.UploadAsync(
                    dropboxPath,
                    WriteMode.Overwrite.Instance,
                    body: mem
                );
            }
        }
        public async Task UploadFileAsync(string localPath, string parentPath)
        {
            parentPath = NormalizePath(parentPath);
            using (var stream = File.OpenRead(localPath))
            {
                string destPath = CombineDropboxPath(parentPath, Path.GetFileName(localPath));
                await _client.Files.UploadAsync(destPath, WriteMode.Overwrite.Instance, body: stream);
            }
        }

        public async Task UploadFolderAsync(string localFolderPath, string parentPath)
        {
            parentPath = NormalizePath(parentPath);
            string folderName = Path.GetFileName(localFolderPath);

            // ✅ создаём папку и получаем объект с правильным Path
            CloudFile created = await CreateFolderAsync(folderName, parentPath);
            string folderPath = created.Path;  // используем Path от Dropbox API

            foreach (var file in Directory.GetFiles(localFolderPath))
            {
                Logger.Log($"Dropbox: Загружается файл {file}");
                await UploadFileAsync(file, folderPath);
            }

            foreach (var dir in Directory.GetDirectories(localFolderPath))
            {
                Logger.Log($"Dropbox: Рекурсивный заход в подпапку {dir}");
                await UploadFolderAsync(dir, folderPath);  // рекурсивный вызов тоже с правильным путем
            }

        }

        public async Task DeleteFileOrFolderAsync(string path)
        {
            path = NormalizePath(path);
            await _client.Files.DeleteV2Async(path);
        }

        public async Task<CloudFile> CreateFolderAsync(string name, string parentPath)
        {
            parentPath = NormalizePath(parentPath);
            string fullPath = CombineDropboxPath(parentPath, name);
            var result = await _client.Files.CreateFolderV2Async(fullPath);
            return new CloudFile
            {
                Id = result.Metadata.PathLower,
                Name = result.Metadata.Name,
                IsFolder = true,
                Path = result.Metadata.PathDisplay // ❗ ДОБАВЬ ЭТО
            };
        }

        private string CombineDropboxPath(string parent, string name)
        {
            if (string.IsNullOrEmpty(parent) || parent == "/")
                return $"/{name}";
            return $"{parent.TrimEnd('/')}/{name}";
        }

        private string NormalizePath(string path, bool forListFolder = false)
        {
            if (string.IsNullOrWhiteSpace(path) || path.Trim().ToLower() == "root")
                return forListFolder ? "" : "/";

            if (!path.StartsWith("/"))
                return "/" + path;

            return path;
        }
        public async Task<byte[]> DownloadFileAsync(string path)
        {
            using (var response = await _client.Files.DownloadAsync(path))
            {
                return await response.GetContentAsByteArrayAsync();
            }
        }

        public async Task UploadFileAsync(Stream stream, string fileName, string parentPath)
        {
            const int chunkSize = 8 * 1024 * 1024; // 8 MB
            string fullPath = (string.IsNullOrEmpty(parentPath) || parentPath == "/")
                ? $"/{fileName}"
                : $"{parentPath.TrimEnd('/')}/{fileName}";

            if (stream.CanSeek && stream.Length < 150 * 1024 * 1024)
            {
                // Маленький файл — UploadAsync
                await _client.Files.UploadAsync(
                    fullPath,
                    WriteMode.Overwrite.Instance,
                    body: stream);
            }
            else
            {
                // Большой файл — Upload Session
                byte[] buffer = new byte[chunkSize];
                ulong uploaded = 0;
                UploadSessionStartResult sessionStartResult = null;

                // Start session
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                using (var memStream = new MemoryStream(buffer, 0, bytesRead))
                {
                    sessionStartResult = await _client.Files.UploadSessionStartAsync(body: memStream);
                    uploaded += (ulong)bytesRead;
                }

                var cursor = new UploadSessionCursor(sessionStartResult.SessionId, uploaded);

                // Upload chunks
                while (true)
                {
                    bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (bytesRead == 0)
                    {
                        // Финальный блок
                        await _client.Files.UploadSessionFinishAsync(
                            new UploadSessionFinishArg(cursor, new CommitInfo(fullPath)),
                            body: new MemoryStream());
                        break;
                    }

                    using (var memStream = new MemoryStream(buffer, 0, bytesRead))
                    {
                        if (stream.CanSeek && ((ulong)stream.Length - uploaded) <= (ulong)chunkSize)
                        {
                            // Finish
                            await _client.Files.UploadSessionFinishAsync(
                                new UploadSessionFinishArg(cursor, new CommitInfo(fullPath)),
                                body: memStream);
                            return;
                        }
                        else
                        {
                            // Append
                            await _client.Files.UploadSessionAppendV2Async(cursor, body: memStream);
                            uploaded += (ulong)bytesRead;
                            cursor = new UploadSessionCursor(sessionStartResult.SessionId, uploaded);
                        }
                    }
                }
            }
        }

        public async Task UploadFolderAsync(string localFolderPath, string parentPath, bool encrypt)
        {
            parentPath = NormalizePath(parentPath);
            string folderName = Path.GetFileName(localFolderPath);

            CloudFile created = await CreateFolderAsync(folderName, parentPath);
            string folderPath = created.Path;

            foreach (var file in Directory.GetFiles(localFolderPath))
            {
                Logger.Log($"Dropbox: Загружается файл {file}");

                bool alreadyEncrypted = file.EndsWith(".enc", StringComparison.OrdinalIgnoreCase);
                bool actualEncrypt = encrypt && !alreadyEncrypted;

                await UploadFileAsync(file, folderPath, actualEncrypt);
            }


            foreach (var dir in Directory.GetDirectories(localFolderPath))
            {
                Logger.Log($"Dropbox: Рекурсивный заход в подпапку {dir}");
                await UploadFolderAsync(dir, folderPath, encrypt); // <--- рекурсивно с шифрованием
            }
        }

    }
}
