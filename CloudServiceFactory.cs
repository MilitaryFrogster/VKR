using System;
using System.Threading.Tasks;

namespace VKR
{
    public static class CloudServiceFactory
    {
        public static ICloudService Create(ConnectedAccount acc)
        {
            if (acc.Cloud == "Google Drive")
            {
                var service = GoogleDriveAuth.CreateDriveServiceFromStoreAsync(acc.Email).Result;
                return new GoogleDriveService(service);
            }
            else if (acc.Cloud == "Dropbox")
            {
                return new DropboxService(acc.AccessToken);
            }
            else
            {
                throw new NotSupportedException("Облако не поддерживается");
            }
        }
    }
}
