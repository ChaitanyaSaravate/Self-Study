using System.Collections.Generic;
using System.Data;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Logging.SerilogClient.Extensions
{
    public static class SerilogConfigurationExtension
    {
        /// <summary>
        /// Adds a basic Serilog configuration which can be done while building the Host.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="applicationName"></param>
        /// <param name="solutionArea"></param>
        /// <returns></returns>
        public static IHostBuilder ConfigureSeriLog(this IHostBuilder builder, string applicationName, string solutionArea = "Tieto_Education")
        {
            //TODO: Try configuring LoggerConfiguration using external configuration file instead of through code.

            return builder.UseSerilog((hostBuilderContext, loggerConfig) =>
            {
                // Many configuration settings are added just to try them out to see their effects.
                
                // Common Logger configuration
                loggerConfig.MinimumLevel.Verbose();
                loggerConfig.Enrich.FromLogContext();

                loggerConfig.MinimumLevel.Override("Microsoft", LogEventLevel.Error);
                loggerConfig.MinimumLevel.Override("System", LogEventLevel.Error);

                // Fixed properties
                loggerConfig.Enrich.WithProperty("SolutionArea", solutionArea);
                loggerConfig.Enrich.WithProperty("AppName", applicationName);
                loggerConfig.Enrich.With(new AuditLogEventEnricher());
                //loggerConfig.Enrich.WithProperty("AdditionalDetails",
                //    "Some additional details for which separate field or column is not available.");

                // Custom templates for File Sinks
                string templateWithSeparatePlaceholderForEachPropertyEnricher =
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SolutionArea} {AppName} {UserId} {UserRole} {CorrelationId} {Properties} {Message:lj}{NewLine}{Exception}";

                string templateWithOnePlaceholderForAllPropertyEnrichers = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Properties} {Message:lj}{NewLine}{Exception}";

                // Diagnostic logs to File sink using "WriteTo"
                loggerConfig.WriteTo.File("C:\\Logs\\Serilog_Diagnostic.txt", outputTemplate: templateWithSeparatePlaceholderForEachPropertyEnricher);

                // Audit logs to File sink using "AuditTo"
                loggerConfig.AuditTo.File("C:\\Logs\\Serilog_Audit.txt", LogEventLevel.Information, outputTemplate: templateWithOnePlaceholderForAllPropertyEnrichers);


                // TODO: Read connection string from configuration.
                string connectionString = "Server=WL357485\\EDU_LOCAL;Database=SerilogPoC;User Id=sa;Password=TestPassword@1";


                string logTableWithSeparateColumnForEachPropertyEnricher = "AuditLogs_Readable";
                var auditTableColumnOptions = new ColumnOptions();
                auditTableColumnOptions.Store.Remove(StandardColumn.Exception);
                auditTableColumnOptions.TimeStamp.ConvertToUtc = true;
                // auditTableColumnOptions.Store.Add(StandardColumn.LogEvent); // Logs the entire log event object as a JSON.
                auditTableColumnOptions.PrimaryKey = auditTableColumnOptions.Id;
                auditTableColumnOptions.AdditionalColumns = new List<SqlColumn>
                    {
                        new SqlColumn { ColumnName = "EventType", AllowNull = true, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },
                        new SqlColumn { ColumnName = "DomainId", AllowNull = false, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },
                        new SqlColumn { ColumnName = "SolutionArea", AllowNull = false, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },
                        new SqlColumn { ColumnName = "AppName", AllowNull = false, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },
                        new SqlColumn { ColumnName = "UserId", AllowNull = false, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = false },
                        new SqlColumn { ColumnName = "UserRole", AllowNull = false, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = false },
                        new SqlColumn { ColumnName = "SourceContext", AllowNull = true, DataLength = 400, DataType = SqlDbType.NVarChar, NonClusteredIndex = false },
                        new SqlColumn { ColumnName = "ActiveSubjectType", AllowNull = true, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },
                        new SqlColumn { ColumnName = "ActiveSubjectId", AllowNull = true, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },
                        new SqlColumn { ColumnName = "CorrelationId", AllowNull = true, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },

                    };
                auditTableColumnOptions.Properties.ExcludeAdditionalProperties = true;
                // Audit logs to MSSQL sink .. Logging to the table having separate column for each enricher property
                loggerConfig.AuditTo.MSSqlServer(connectionString, logTableWithSeparateColumnForEachPropertyEnricher,
                    columnOptions: auditTableColumnOptions, restrictedToMinimumLevel: LogEventLevel.Information, autoCreateSqlTable: true);


                string logTableWithCommonEnricherColumn = "AuditLogs_NonReadable";
                var auditColumnOptionsWithoutAdditionalColumns = new ColumnOptions();
                auditColumnOptionsWithoutAdditionalColumns.Store.Remove(StandardColumn.Exception);
                // auditColumnOptionsWithoutAdditionalColumns.Store.Add(StandardColumn.LogEvent); // Logs the entire log event object as a JSON.
                auditColumnOptionsWithoutAdditionalColumns.PrimaryKey = auditTableColumnOptions.Id;
                auditColumnOptionsWithoutAdditionalColumns.TimeStamp.NonClusteredIndex = true;

                // Audit logs to MSSQL sink .. Logging to the table having NO separate column for each enricher property
                loggerConfig.AuditTo.MSSqlServer(connectionString, logTableWithCommonEnricherColumn,
                    columnOptions: auditColumnOptionsWithoutAdditionalColumns,
                    restrictedToMinimumLevel: LogEventLevel.Information, autoCreateSqlTable: true);
            });
        }
    }
}
