using System;
using System.Collections.Generic;
using DatabaseJobs.Shared.Dtos;
using DbBackup.Shared;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseJobs.Shared
{
    public class DatabaseProvider
    {
        private static List<DatabaseServerDto> databaseServers = new List<DatabaseServerDto>();

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

       

        public static List<DatabaseServerDto> GetDatabseConnections(Server server)
        {
            Console.WriteLine("Collecting connection strings of databases...");

            databaseServers.Clear();

            var backupAllDatabases = AppSettings.BackupAllDatabases;

            if (backupAllDatabases)
            {
                foreach (Database database in server.Databases)
                {
                    if (IgnoreSystemDb(database)) continue;

                    var databaseServer = BuildConnectionString(server, database.Name);

                    databaseServers.Add(databaseServer);
                }
            }
            else
            {
                var backupDatabases = AppSettings.BackupDatabases;
                var databaseNames = backupDatabases.Split(',');

                foreach (var databaseName in databaseNames)
                {
                    var databaseServer = BuildConnectionString(server, databaseName);

                    databaseServers.Add(databaseServer);
                }

            }
            

            return databaseServers;
        }

        private static DatabaseServerDto BuildConnectionString(Server server, string databaseName)
        {

            return new DatabaseServerDto(server.Name, databaseName);

            //return $"Data Source={server.Name};Initial Catalog={databaseName};Integrated Security=true;";
        }

    }
}