using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Core.Logging
{
    public static class LoggingExtension
    {

        /// <summary>
        /// Clears all providers, and use only SeriLog. Configs are received from Serilog.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        public static void UseOnlyStandardLogging(this ILoggingBuilder builder, IConfiguration configuration)
        {
            builder.ClearProviders();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration, "Serilog")
               .CreateLogger();

            builder.AddSerilog();
        }
    }
}