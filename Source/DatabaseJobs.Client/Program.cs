using System.Collections.Generic;
using DatabaseJobs.Backup;
using DatabaseJobs.Maintenance;
using DatabaseJobs.Shared;
using DatabaseJobs.Shared.Dtos;
using DbBackup;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseJobs
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoggerConfig.Register();

            Server server = ServerConnector.Connect();
           
            List<DatabaseServerDto> databaseServers = DatabaseProvider.GetDatabseConnections(server);

            ShrinkJob.Shrink(databaseServers);

            IndexOrganizer.Reorganize(databaseServers);

            List<Database> databases = DatabaseProvider.GetDatabasesToBackup(server);

            BackupBuilder.GenerateBackups(server, databases);

            BackupCleaner.CleanAllBackups();


            //// Get the service on the local machine
            //using (TaskService ts = new TaskService())
            //{
            //    // Create a new task definition and assign properties
            //    TaskDefinition td = ts.NewTask();
            //    td.RegistrationInfo.Description = "Does something";

            //    // Create a trigger that will fire the task at this time every other day
            //    td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });

            //    // Create an action that will launch Notepad whenever the trigger fires
            //    td.Actions.Add(new ExecAction("notepad.exe", "c:\\test.log", null));

            //    // Register the task in the root folder
            //    ts.RootFolder.RegisterTaskDefinition(@"Test", td);

            //    // Remove the task we just created
            //    ts.RootFolder.DeleteTask("Test");
            //}
        }

    }
}
