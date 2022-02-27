using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbBackup.Maintenance
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
