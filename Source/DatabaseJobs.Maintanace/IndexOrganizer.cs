using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using DbBackup.Shared;

namespace DatabaseJobs.Maintenance
{
    public static class IndexOrganizer
    {
        private const string SpReorganize = @"Exec sp_msforeachtable 'ALTER INDEX ALL ON ? Reorganize'";
        private const string SpRebuild = @"Exec sp_msforeachtable 'ALTER INDEX ALL ON ? Rebuild'";

        public static void  Reorganize(List<string> connectionStrings)
        {
            if(!AppSettings.EnableIndexMaintenance) return;

            Console.WriteLine("Starting index reorganize...");

            foreach (var connectionString in connectionStrings)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    Console.WriteLine($"Reorganizing...{connectionString}");

                    con.Query(SpReorganize, commandTimeout: 24 * 60 * 60);
                }
            }
        }
        
        
        public static void Rebuild(List<string> connectionStrings)
        {
            if (!AppSettings.EnableIndexMaintenance) return;

            Console.WriteLine("Starting index rebuild...");

            foreach (var connectionString in connectionStrings)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    Console.WriteLine($"Rebuilding...{connectionString}");

                    con.Query(SpRebuild, commandTimeout: 24 * 60 * 60);
                }
            }
        }
    }
}
