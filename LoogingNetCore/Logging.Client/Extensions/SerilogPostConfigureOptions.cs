using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Logging.SerilogClient.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Sputnik.Database.Abstractions;

namespace Logging.SerilogClient
{
    /// <summary>
    /// Reads Database connection string from Meta database using IPostConfigureOptions.
    /// I had to go this way mainly because Sputnik 2 depedencies/services will not be available earlier.
    /// If I had no dependency on Sputnik 2 Meta, then we could have added LoggerConfiguration in Startup.cs too. 
    /// </summary>
    public class SerilogPostConfigureOptions : IPostConfigureOptions<SerilogSqlSinkOptions>
    {
        private readonly IServiceProvider _serviceProvider;
        private IDatabaseManagement _metaDatabaseConfiguration;

        public SerilogPostConfigureOptions(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        ///  Reads connection string to connect to the module's database using Meta configuration and then configures the SQL sink. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void PostConfigure(string name, SerilogSqlSinkOptions options)
        {
            _metaDatabaseConfiguration = _serviceProvider.GetRequiredService<IDatabaseManagement>();
            options.ConnectionString = GetDatabaseConnectionString(options.ModuleName).GetAwaiter().GetResult();
            ConfigureSqlSinkUsingEduSputnik(options);
        }

        private void ConfigureSqlSinkUsingEduSputnik(SerilogSqlSinkOptions options)
        {
            LoggerConfiguration loggerConfiguration = new LoggerConfiguration();
            string logTableCreatedManually = "AuditLogs";
            var auditTableManualColumnOptions = new ColumnOptions();
            auditTableManualColumnOptions.TimeStamp.ConvertToUtc = true;
            // auditTableColumnOptions.Store.Add(StandardColumn.LogEvent); // Logs the entire log event object as a JSON.
            auditTableManualColumnOptions.PrimaryKey = auditTableManualColumnOptions.Id;
            auditTableManualColumnOptions.AdditionalColumns = new List<SqlColumn>
                    {
                        new SqlColumn { ColumnName = "EventType", AllowNull = true, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },
                        new SqlColumn { ColumnName = "CustomerId", AllowNull = false, DataType = SqlDbType.UniqueIdentifier, NonClusteredIndex = false},
                        new SqlColumn { ColumnName = "DomainId", AllowNull = false, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },
                        new SqlColumn { ColumnName = "SolutionArea", AllowNull = false, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = false },
                        new SqlColumn { ColumnName = "SystemName", AllowNull = false, DataLength = 100, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },
                        new SqlColumn { ColumnName = "UserId", AllowNull = false, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = false },
                        new SqlColumn { ColumnName = "UserName", AllowNull = false, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = false },
                        new SqlColumn { ColumnName = "UserRole", AllowNull = true, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = false },
                        new SqlColumn { ColumnName = "SchoolDomain", AllowNull = true, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = false },
                        new SqlColumn { ColumnName = "ServerId", AllowNull = false, DataLength = 400, DataType = SqlDbType.NVarChar, NonClusteredIndex = false },
                        new SqlColumn { ColumnName = "CorrelationId", AllowNull = true, DataType = SqlDbType.UniqueIdentifier, NonClusteredIndex = false },
                    };

            auditTableManualColumnOptions.Properties.ExcludeAdditionalProperties = true;

            // Audit logs to MSSQL sink .. Logging to the table having separate column for each enricher property and the table which is NOT created automatically; which was created using SQL script directly.
            loggerConfiguration.AuditTo.MSSqlServer(options.ConnectionString, logTableCreatedManually,
                columnOptions: auditTableManualColumnOptions, restrictedToMinimumLevel: LogEventLevel.Information, autoCreateSqlTable: true);

          //  loggerConfiguration.Enrich.With(new AuditLogEventEnricher(_serviceProvider.GetService<IHttpContextAccessor>()));
            //loggerConfiguration.Enrich.With(new AuditLogEventEnricher());
            Log.Logger = loggerConfiguration.CreateLogger();
        }

        private async Task<string> GetDatabaseConnectionString(string DatabaseIdentifier)
        {
            var (Success, DatabaseConfiguration) = await _metaDatabaseConfiguration.TryGetDatabaseConfigurationAsync(DatabaseIdentifier);
            if (Success)
            {
                return
                    $"Server={DatabaseConfiguration.DataSource};" +
                    $"Database={ DatabaseConfiguration.DatabaseName};" +
                    $"User Id={ DatabaseConfiguration.ApplicationUserId};" +
                    $"Password={DatabaseConfiguration.ApplicationUserPassword};";
            }

            throw new Exception("Exception while getting database connection string.");
        }
    }
}
