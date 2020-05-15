using System;
using System.Configuration;

namespace DbBackup.Shared
{
    public class AppSettings
    {
        public static string ServerName => ConfigurationManager.AppSettings["ServerName"];
        public static string BackupDatabases => ConfigurationManager.AppSettings["BackupDatabases"];
        public static bool UseRootBackupDirectory => Convert.ToBoolean(ConfigurationManager.AppSettings["UseRootBackupDirectory"]);
        public static string BackupDirectoryPath => ConfigurationManager.AppSettings["BackupDirectoryPath"];
        public static int? RemoveBackupAfterXDays => GetRemoveBackupAfterDays();


        private static int? GetRemoveBackupAfterDays()
        {
            var backupRemoveDays = ConfigurationManager.AppSettings["RemoveBackupAfterXDays"];
            int.TryParse(backupRemoveDays, out int days);
            return days;
        }

    }
}
