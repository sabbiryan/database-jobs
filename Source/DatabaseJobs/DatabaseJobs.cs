using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseJobs.Backup;
using DatabaseJobs.Maintenance;
using DatabaseJobs.Shared;
using DatabaseJobs.Shared.Dtos;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseJobs
{
    public class DatabaseJobs
    {
        public void Run()
        {
            Server server = ServerConnector.Connect();
           
            List<DatabaseServerDto> databaseServers = DatabaseProvider.GetDatabseConnections(server);

            ShrinkJob.Shrink(databaseServers);

            IndexOrganizer.Reorganize(databaseServers);

            List<Database> databases = DatabaseProvider.GetDatabasesToBackup(server);

            BackupBuilder.GenerateBackups(server, databases);

            BackupCleaner.CleanAllBackups();
        }

    }
}
