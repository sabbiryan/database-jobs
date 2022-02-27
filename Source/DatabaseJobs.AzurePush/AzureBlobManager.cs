using System.Configuration;
using System.IO;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DatabaseJobs.Shared.Enums;
using DbBackup.Shared;

namespace DatabaseJobs.AzurePush
{
    public class AzureBlobManager
    {
        private const string BackupBlobContainerName = "backups";
        private const string BackupBlobContainerFolderName = "databases";


        private static BlobContainerClient CloudBlobContainer()
        {
            ConnectionStringSettingsCollection connectionStringSettings = ConfigurationManager.ConnectionStrings;
            string storageConnection = connectionStringSettings[1].ConnectionString;

            BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnection);

            //create a container
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(BackupBlobContainerName);

            //create a container if it is not already exists
            blobContainerClient.CreateIfNotExists();

            //set permission to public
            blobContainerClient.SetAccessPolicy(PublicAccessType.Blob);


            return blobContainerClient;
        }


        public static void Push(string filePath)
        {
            if(!AppSettings.PushToAzureStorage) return;

            var blobContainer = CloudBlobContainer();

            
            var fileName = Path.GetFileName(filePath);
            

            var fileExtension = Path.GetExtension(filePath);
            string contentType = "";
            if (fileExtension.ToLower().Equals($".{nameof(BackupFileType.Bak).ToLower()}"))
            {
                contentType = "application/octet-stream";
            }
            else if (fileExtension.ToLower().Equals($".{nameof(BackupFileType.Zip).ToLower()}"))
            {
                contentType = "application/zip";
            }


            string blobName = $"{BackupBlobContainerFolderName}/{fileName}";

            //get Blob reference

            BlobClient blob = blobContainer.GetBlobClient(blobName);

            blob.Upload(filePath, new BlobHttpHeaders()
            {
                ContentType = contentType
            });

        }


        public static void Delete(string filePath)
        {
            if (!AppSettings.PushToAzureStorage) return;

            var blobContainer = CloudBlobContainer();

            var fileName = Path.GetFileName(filePath);

            string blobName = $"{BackupBlobContainerFolderName}/{fileName}";

            //get blob reference

            BlobClient blob = blobContainer.GetBlobClient(blobName);
            blob.DeleteIfExists();
            
        }

    }
}
