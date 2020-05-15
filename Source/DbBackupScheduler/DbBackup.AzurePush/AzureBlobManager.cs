using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbBackup.Shared;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DbBackup.AzurePush
{
    public class AzureBlobManager
    {
        private const string BackupBlobContainerName = "backups";
        private const string BackupBlobContainerFolderName = "databases";


        private static CloudBlobContainer CloudBlobContainer()
        {
            ConnectionStringSettingsCollection connectionStringSettings = ConfigurationManager.ConnectionStrings;
            string storageConnection = connectionStringSettings[1].ConnectionString;

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnection);

            //create a block blob
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            //create a container
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(BackupBlobContainerName);

            //create a container if it is not already exists

            if (blobContainer.CreateIfNotExists())
            {
                blobContainer.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }

            return blobContainer;
        }


        public static void Push(string filePath)
        {
            if(!AppSettings.PushToAzureStorage) return;

            var blobContainer = CloudBlobContainer();

            var fileName = Path.GetFileName(filePath);

            string blobName = $"{BackupBlobContainerFolderName}/{fileName}";

            //get Blob reference

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName); 
            blockBlob.Properties.ContentType = "application/zip";

            using (var fileStream = File.OpenRead(filePath))
            {
                blockBlob.UploadFromStream(fileStream);
                fileStream.Flush();
            }

        }


        public static void Delete(string filePath)
        {
            if (!AppSettings.PushToAzureStorage) return;

            var blobContainer = CloudBlobContainer();

            var fileName = Path.GetFileName(filePath);

            string blobName = $"{BackupBlobContainerFolderName}/{fileName}";

            //get Blob reference

            CloudBlockBlob blockBlob = blobContainer.GetBlockBlobReference(blobName);
            blockBlob.DeleteIfExists();
            
        }

    }
}
