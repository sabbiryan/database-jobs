using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;

namespace DbBackup
{
    public class LoggerConfig
    {

        public static void Register()
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logFilePath, LogEventLevel.Error, rollingInterval: RollingInterval.Day)
                .CreateLogger();

        }

    }
}
