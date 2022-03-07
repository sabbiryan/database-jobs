using System;
using Microsoft.SqlServer.Management.Smo;

namespace DatabaseJobs.Shared
{
    public class ServerConnector
    {


        public static Server Connect()
        {
            Console.WriteLine("Connecting to SQL Server...");

            var serverName = AppSettings.ServerName;

            Server myServer = new Server(serverName);
            myServer.ConnectionContext.LoginSecure = true;
            myServer.ConnectionContext.Connect();
            return myServer;
        }


    }
}
