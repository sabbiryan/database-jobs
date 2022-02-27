using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;

namespace DbBackup.Shared
{
    public class DatabaseProvider
    {
        private static bool IgnoreSystemDb(Database database)
        {
            if (database.IsSystemObject) return true;
            return false;
        }


        public static List<Database> GetDatabasesToBackup(Server server)
        {
            Console.WriteLine("Collecting databases to take backups...");

            List<Database> databases = new List<Database>();


            var backupAllDatabases = AppSettings.BackupAllDatabases;

            if (backupAllDatabases)
            {
                foreach (Database database in server.Databases)
                {
                    if (IgnoreSystemDb(database)) continue;

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

       

        public static List<string> GetConnectionStrings(Server server)
        {
            Console.WriteLine("Collecting connection strings of databases...");

            var connectionStrings = new List<string>();

            var backupAllDatabases = AppSettings.BackupAllDatabases;

            if (backupAllDatabases)
            {
                foreach (Database database in server.Databases)
                {
                    if (IgnoreSystemDb(database)) continue;

                    var connectionString = BuildConnectionString(server, database.Name);

                    connectionStrings.Add(connectionString);
                }
            }
            else
            {
                var backupDatabases = AppSettings.BackupDatabases;
                var databaseNames = backupDatabases.Split(',');

                foreach (var databaseName in databaseNames)
                {
                    var connectionString = BuildConnectionString(server, databaseName);

                    connectionStrings.Add(connectionString);
                }

            }
            

            return connectionStrings;
        }

        private static string BuildConnectionString(Server server, string databaseName)
        {
            
            return $"Data Source={server.Name};Initial Catalog={databaseName};Integrated Security=true;";

            //return $"Database={databaseName};{server.ConnectionContext.ConnectionString}";
        }

    }
}