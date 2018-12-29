using System;
using System.Configuration;
using System.IO;

namespace DbBackup
{
    public class DirectoryProvider
    {
        public static string GetBackupDirectory()
        {
            bool isUseBaseBackupDirectory;
            bool.TryParse(ConfigurationManager.AppSettings["UseBaseBackupDirectory"], out isUseBaseBackupDirectory);

            var backupDirectory = isUseBaseBackupDirectory
                ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups")
                : ConfigurationManager.AppSettings["BackupDirectory"];

            string directory = CheckDirectory(backupDirectory);


            return directory;
        }


        private static string CheckDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return directoryPath;
        }

    }
}