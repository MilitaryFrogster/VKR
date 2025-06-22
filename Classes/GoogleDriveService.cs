using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using File = Google.Apis.Drive.v3.Data.File;


namespace VKR
{
    public class GoogleDriveService : ICloudService
    {
        private readonly DriveService _svc;
        public string CloudName => "Google Drive";

        public GoogleDriveService(DriveService svc)
        {
            _svc = svc;
        }

        // Получаем только один уровень (для Lazy-load в TreeView)
        public async Task<List<CloudFile>> ListFilesAsync(string parentId)
        {
            if (string.IsNullOrEmpty(parentId) || parentId == "root")
                parentId = "root";  // Google API корректно обрабатывает "root"

            var res = new List<CloudFile>();
            var req = _svc.Files.List();
            req.Q = $"'{parentId}' in parents and trashed = false";
            req.Fields = "files(id,name,mimeType)";
            try
            {
                var fl = await req.ExecuteAsync();
                foreach (var f in fl.Files)
                {
                    res.Add(new CloudFile
                    {
                        Id = f.Id,
                        Name = f.Name,
                        IsFolder = f.MimeType == "application/vnd.google-apps.folder",
                        Size = f.Size.HasValue ? f.Size.Value : 0

                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при получении содержимого папки Google Drive: {ex.Message}");
            }
            return res;
        }
        public async Task<CloudFile> CreateFolderAsync(string name, string parentId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = name,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string> { parentId }
            };

            var request = _svc.Files.Create(fileMetadata);
            request.Fields = "id, name";

            var created = await request.ExecuteAsync();

            return new CloudFile
            {
                Id = created.Id,
                Name = created.Name,
                IsFolder = true
            };
        }


        public async Task<Stream> OpenDownloadStreamAsync(string fileId)
        {
            var request = _svc.Files.Get(fileId);
            var stream = new MemoryStream();
            await request.DownloadAsync(stream);
            stream.Position = 0;
            return stream;
        }


        public async Task DeleteFileOrFolderAsync(string fileId)
        {
            try
            {
                await _svc.Files.Delete(fileId).ExecuteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при удалении файла или папки: " + ex.Message, ex);
            }
        }

        // Для соответствия ICloudService, но нам не нужен здесь
        public Task<List<string>> ListFilesAsync()
            => throw new System.NotImplementedException();

        public async Task UploadFileAsync(string localPath, string parentId, bool encrypt)
        {
            Logger.Log($"GoogleDrive: Загрузка файла {localPath} (encrypt={encrypt}) в {parentId}");

            var fileMetadata = new File
            {
                Name = Path.GetFileName(localPath),
                Parents = new List<string> { parentId }
            };

            Stream stream;
            string mimeType;

            if (encrypt)
            {
                byte[] encryptedData = EncryptionHelper.EncryptFile(localPath);
                stream = new MemoryStream(encryptedData);
                fileMetadata.Name += ".enc";
                mimeType = "application/octet-stream";
            }
            else
            {
                stream = new FileStream(localPath, FileMode.Open, FileAccess.Read);
                mimeType = MimeMapping.MimeUtility.GetMimeMapping(localPath);
            }

            using (stream)
            {
                var request = _svc.Files.Create(fileMetadata, stream, mimeType);
                request.Fields = "id";
                await request.UploadAsync();
            }
        }


        public async Task UploadFileAsync(string localPath, string parentId)
        {
            await UploadFileAsync(localPath, parentId, false);
        }

        public async Task UploadFolderAsync(string localFolderPath, string parentId)
        {
            string folderName = Path.GetFileName(localFolderPath);
            var folder = await CreateFolderAsync(folderName, parentId);

            Logger.Log($"GoogleDrive: Начинается загрузка папки {localFolderPath} в {parentId}");


            foreach (var filePath in Directory.GetFiles(localFolderPath))
            {

                Logger.Log($"GoogleDrive: Загружается файл {filePath}");

                await UploadFileAsync(filePath, folder.Id);
            }

            foreach (var subDir in Directory.GetDirectories(localFolderPath))
            {

                Logger.Log($"GoogleDrive: Рекурсивный заход в подпапку {subDir}");

                await UploadFolderAsync(subDir, folder.Id); // рекурсивно
            }
        }

        public async Task<byte[]> DownloadFileAsync(string fileId)
        {
            var request = _svc.Files.Get(fileId);
            using (var ms = new MemoryStream())
            {
                await request.DownloadAsync(ms);
                return ms.ToArray();
            }
        }
        public async Task UploadFileAsync(Stream stream, string fileName, string parentId)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName,
                Parents = new List<string> { parentId }
            };

            // Выбираем MimeType
            string mimeType = "application/octet-stream";

            var request = _svc.Files.Create(fileMetadata, stream, mimeType);
            request.Fields = "id, name";

            // ВАЖНО: включаем Resumable Upload
            request.ChunkSize = ResumableUpload.MinimumChunkSize * 2; // например, 512 * 1024 * 2 = 1 MB или больше

            await request.UploadAsync();
        }

        public async Task UploadFolderAsync(string localFolderPath, string parentId, bool encrypt)
        {
            string folderName = Path.GetFileName(localFolderPath);
            var folder = await CreateFolderAsync(folderName, parentId);

            foreach (var filePath in Directory.GetFiles(localFolderPath))
            {
                Logger.Log($"GoogleDrive: Загружается файл {filePath}");

                bool alreadyEncrypted = filePath.EndsWith(".enc", StringComparison.OrdinalIgnoreCase);
                bool actualEncrypt = encrypt && !alreadyEncrypted;

                await UploadFileAsync(filePath, folder.Id, actualEncrypt);
            }


            foreach (var subDir in Directory.GetDirectories(localFolderPath))
            {
                Logger.Log($"GoogleDrive: Рекурсивный заход в подпапку {subDir}");
                await UploadFolderAsync(subDir, folder.Id, encrypt); // <-- рекурсивно с шифрованием
            }
        }


    }
}
