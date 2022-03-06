using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using DatabaseJobs.Shared.Dtos;
using DbBackup.Shared;

namespace DatabaseJobs.Maintenance
{
    public static class ShrinkJob
    {

        public static void Shrink(List<DatabaseConnectionDto> databaseConnections)
        {
            if (!AppSettings.EnableShrink) return;

            Console.WriteLine("Starting shrink...");

            foreach (var databaseServer in databaseConnections)
            {
                using (var con = new SqlConnection(databaseServer.ConnectionString))
                {
                    Console.WriteLine($"Shrinking...{databaseServer.ConnectionString}");

                    con.Query($@"DBCC SHRINKDATABASE ('{databaseServer.DatabaseName}', 10)", commandTimeout: 24 * 60 * 60);
                }
            }

            IndexOrganizer.Rebuild(databaseConnections);
        }
    }
}
