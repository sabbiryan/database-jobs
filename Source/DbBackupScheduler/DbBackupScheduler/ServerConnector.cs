using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbBackup
{
    public class ServerConnector
    {


        public static Server Connect()
        {
            var serverName = ConfigurationManager.AppSettings["ServerName"];

            Server myServer = new Server(serverName);
            myServer.ConnectionContext.LoginSecure = true;
            myServer.ConnectionContext.Connect();
            return myServer;
        }


    }
}
