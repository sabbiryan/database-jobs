using DbBackup.AzurePush;
using DbBackup.Shared;
using DbBackup.Shared.Extensions;
using DbBackup.Zipper.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseJobs.Backup
{
    public static class BackupCleaner
    {
        public static void CleanAllBackups()
        {
            //Clean older backups less then settings value
            if (AppSettings.RemoveBackupAfterXDays.HasValue && AppSettings.RemoveBackupAfterXDays > 0)
            {
                var backupRemoveTillDate = DateTime.Today.AddDays(-AppSettings.RemoveBackupAfterXDays.GetValueOrDefault());

                var backupDirectory = DirectoryProvider.GetBackupDirectory();

                FileInfo[] directoryFiles = new DirectoryInfo(backupDirectory).GetFiles();

                foreach (var directoryFile in directoryFiles)
                {
                    if (directoryFile.CreationTime.Date <= backupRemoveTillDate.Date)
                    {
                        CleanLocalBackup(directoryFile.FullName);
                        CleanCloudBackup(directoryFile.FullName);

                    }
                }
            }
        }



        public static bool CleanLocalBackup(string fileName)
        {

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            return true;
        }


        public static bool CleanCloudBackup(string fileName)
        {
            if (AppSettings.PushToAzureStorage)
            {
                AzureBlobManager.Delete(fileName);
            }

            return true;
        }


       
    }
}
