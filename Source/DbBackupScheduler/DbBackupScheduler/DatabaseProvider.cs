using System.Collections.Generic;
using System.Configuration;
using Microsoft.SqlServer.Management.Smo;

namespace DbBackup
{
    public class DatabaseProvider
    {
        public static List<Database> GetDatabasesToBackup()
        {
            List<Database> databases = new List<Database>();

            var backupDatabases = ConfigurationManager.AppSettings["BackupDatabases"];
            var databaseNames = backupDatabases.Split(',');

            foreach (var databaseName in databaseNames)
            {
                databases.Add(new Database { Name = databaseName });
            }

            return databases;
        }
    }
}