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

            Server server = new Server(serverName);

            server.ConnectionContext.LoginSecure = true;
            server.ConnectionContext.Connect();

            return server;
        }


    }
}
