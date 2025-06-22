using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace VKR
{
    public interface ICloudService
    {
        string CloudName { get; }

        Task<List<CloudFile>> ListFilesAsync(string parentId);

        Task<CloudFile> CreateFolderAsync(string name, string parentId);  

        Task UploadFileAsync(string localPath, string parentId);          

        Task UploadFolderAsync(string localFolderPath, string parentId);  

        Task DeleteFileOrFolderAsync(string fileId);

        Task<byte[]> DownloadFileAsync(string fileId);

        Task UploadFileAsync(Stream content, string fileName, string parentId);

        Task<Stream> OpenDownloadStreamAsync(string fileId);

        Task UploadFileAsync(string localPath, string parentId, bool encrypt);

        Task UploadFolderAsync(string localFolderPath, string parentId, bool encrypt);



    }
}
