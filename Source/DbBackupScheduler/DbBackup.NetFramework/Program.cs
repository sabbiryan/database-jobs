using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbBackup.Shared;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Serilog;
using Serilog.Core;


namespace DbBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoggerConfig.Register();

            Server server = ServerConnector.Connect();

            List<Database> databases = DatabaseProvider.GetDatabasesToBackup();

            string directory = DirectoryProvider.GetBackupDirectory();


            BackupBuilder.GenerateBackups(server, databases, directory);
        }





    }
}
