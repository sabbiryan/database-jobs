using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseJobs.Backup;
using DatabaseJobs.Maintenance;
using DatabaseJobs.Shared;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseJobs
{
    public class DatabaseJobs
    {
        public void Run()
        {
            Server server = ServerConnector.Connect();

            List<Database> databases = DatabaseProvider.GetDatabasesToBackup(server);

            List<string> connectionStrings = DatabaseProvider.GetConnectionStrings(server);

            IndexOrganizer.Reorganize(connectionStrings);

            BackupBuilder.GenerateBackups(server, databases);

            BackupCleaner.CleanAllBackups();
        }

    }
}
