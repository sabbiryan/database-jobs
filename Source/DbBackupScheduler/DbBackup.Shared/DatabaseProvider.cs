using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;

namespace DbBackup.Shared
{
    public class DatabaseProvider
    {
        public static List<Database> GetDatabasesToBackup(Server server)
        {
            List<Database> databases = new List<Database>();


            var backupAllDatabases = AppSettings.BackupAllDatabases;

            if (backupAllDatabases)
            {
                foreach (Database database in server.Databases)
                {
                    if(string.Equals(database.Name.ToLower(), "master")) continue;

                    databases.Add(new Database { Name = database.Name });
                }
                
            }
            else
            {
                var backupDatabases = AppSettings.BackupDatabases;
                var databaseNames = backupDatabases.Split(',');

                foreach (var databaseName in databaseNames)
                {
                    databases.Add(new Database { Name = databaseName });
                }
                
            }
            

            return databases;
        }


        public static string GetBackupName(string databaseName)
        {


            return "";
        }
    }
}