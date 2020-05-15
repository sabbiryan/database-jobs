using Microsoft.SqlServer.Management.Smo;

namespace DbBackup.Shared
{
    public class ServerConnector
    {


        public static Server Connect()
        {
            var serverName = AppSettings.ServerName;

            Server myServer = new Server(serverName);
            myServer.ConnectionContext.LoginSecure = true;
            myServer.ConnectionContext.Connect();
            return myServer;
        }


    }
}
