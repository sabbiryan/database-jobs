using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseJobs.Shared.Dtos
{
    public class DatabaseServerDto
    {
        public DatabaseServerDto(string serverName, string databaseName)
        {
            ServerName = serverName;
            DatabaseName = databaseName;
        }

        public string ServerName { get; set; }
        public string DatabaseName { get; set; }
        public string ConnectionString => $"Data Source={ServerName};Initial Catalog={DatabaseName};Integrated Security=true;";
    }
}
