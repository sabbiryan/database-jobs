using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;

namespace DatabaseJobs.Maintenance
{
    public static class ShrinkJob
    {
        public static void Shrink(List<string> connectionStrings)
        {
            Console.WriteLine("Starting shrink...");

            foreach (var connectionString in connectionStrings)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    Console.WriteLine($"Shrinking...{connectionString}");

                    con.Query("DBCC SHRINKDATABASE (UserDB, 10)");
                }
            }
        }
    }
}
