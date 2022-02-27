using System;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseJobs.Shared.Extensions
{
    public static class FillingExtensions
    {
        public static string GetBackupFileName(this Database database, DateTime date)
        {
            string directory = DirectoryProvider.GetBackupDirectory();

            string day = date.Day < 10 ? $"0{date.Day}" : $"{date.Day}";
            string month = date.Month < 10 ? $"0{date.Month}" : $"{date.Month}";


            string fileName = directory + database.Name + "_" + date.Date.Year + "-" + month + "-" + day + ".bak";

            return fileName;
        }
    }
}
