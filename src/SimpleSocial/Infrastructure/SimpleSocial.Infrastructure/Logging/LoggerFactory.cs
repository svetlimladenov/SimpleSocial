using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace SimpleSocial.Infrastructure.Logging
{
    public static class LoggerFactory
    {
        public static Serilog.Core.Logger CreateLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .ConfigureFileSink(configuration)
                .CreateLogger();
        }

        private static LoggerConfiguration ConfigureFileSink(this LoggerConfiguration loggerConfiguration, IConfiguration configuration)
        {
            const string section = "SimpleSocialLogging:Files";
            const string enabled = "Enabled";
            const string path = "Path";


            var fileConfiguration = configuration.GetSection(section);
            if (fileConfiguration.GetValue<bool>(enabled))
            {
                var fileDestination = fileConfiguration.GetValue<string>(path);
                loggerConfiguration.WriteTo.File(fileDestination, rollingInterval: RollingInterval.Day);
            }

            return loggerConfiguration;
        }
    }
}
