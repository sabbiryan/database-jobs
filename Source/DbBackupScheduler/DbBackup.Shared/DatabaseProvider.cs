using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;

namespace DbBackup.Shared
{
    public class DatabaseProvider
    {
        public static List<Database> GetDatabasesToBackup()
        {
            List<Database> databases = new List<Database>();

            var backupDatabases = AppSettings.BackupDatabases;
            var databaseNames = backupDatabases.Split(',');

            foreach (var databaseName in databaseNames)
            {
                databases.Add(new Database { Name = databaseName });
            }

            return databases;
        }


        public static string GetBackupName(string databaseName)
        {


            return "";
        }
    }
}