using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Logging.Abstractions;
using Logging.SerilogClient.Extensions;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using Sputnik.Database.Abstractions;

namespace Logging.SerilogClient
{
    public class AuditLogger<T> : IAuditLogger<T>
    {
        private readonly IOptions<SerilogSqlSinkOptions> _options;
        private readonly IDatabaseManagement _metaDatabaseManagement;
        private Serilog.ILogger _logger = Log.ForContext<T>();

        // public AuditLogger(IDatabaseManagement metaDatabaseManagement)
        public AuditLogger(IOptions<SerilogSqlSinkOptions> serilogOptions)
        {
            _options = serilogOptions;
            string v = _options.Value.ConnectionString;
        }

        public void LogBusinessEvent(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate)
        {
            //using (LogContext.Push(new AuditLogEventEnricher()))
            using (LogContext.PushProperty(PushProperties.AuditLogPushProperties.EventType.ToString(), auditEventType.ToString()))
            {
                _logger.Information(messageTemplate);
            }
        }

        public void LogBusinessEvent<T1>(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, T1 propertyValue)
        {
            // using (LogContext.Push(new AuditLogEventEnricher()))
            using (LogContext.PushProperty(PushProperties.AuditLogPushProperties.EventType.ToString(), auditEventType.ToString()))
            {
                _logger.Information(messageTemplate, propertyValue);
            }
        }

        public void LogBusinessEvent<T0, T1>(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1)
        {
            // using (LogContext.Push(new AuditLogEventEnricher()))
            using (LogContext.PushProperty(PushProperties.AuditLogPushProperties.EventType.ToString(), auditEventType.ToString()))
            {
                _logger.Information(messageTemplate, propertyValue0, propertyValue1);
            }
        }

        public void LogBusinessEvent<T0, T1, T2>(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1, T2 propertyValue2)
        {
            // using (LogContext.Push(new AuditLogEventEnricher()))
            using (LogContext.PushProperty(PushProperties.AuditLogPushProperties.EventType.ToString(), auditEventType.ToString()))
            {
                _logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }
        }

        public void LogBusinessEvent(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, params object[] propertyValues)
        {
            // using (LogContext.Push(new AuditLogEventEnricher()))
            using (LogContext.PushProperty(PushProperties.AuditLogPushProperties.EventType.ToString(), auditEventType.ToString()))
            {
                _logger.Information(messageTemplate, propertyValues);
            }
        }

        private LoggerConfiguration GetLoggerConfiguration()
        {
            var loggerConfiguration = new LoggerConfiguration();

            string metaConnectionString = GetDatabaseConnectionString().GetAwaiter().GetResult();

            string logTableCreatedManually = "AuditLogs";
            var auditTableManualColumnOptions = new ColumnOptions();
            auditTableManualColumnOptions.TimeStamp.ConvertToUtc = true;
            // auditTableColumnOptions.Store.Add(StandardColumn.LogEvent); // Logs the entire log event object as a JSON.
            auditTableManualColumnOptions.PrimaryKey = auditTableManualColumnOptions.Id;

            // Custom templates for File Sinks
            string templateWithSeparatePlaceholderForEachPropertyEnricher_Manually =
                "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {CustomerId} {SolutionArea} {SystemName} {UserId} {UserName} {UserRole} {CorrelationId} {Properties} {Message:lj}{NewLine}{Exception}";
            auditTableManualColumnOptions.AdditionalColumns = new List<SqlColumn>
                    {
                        new SqlColumn { ColumnName = "EventType", AllowNull = true, DataLength = 50, DataType = SqlDbType.NVarChar, NonClusteredIndex = true },
                        new SqlColumn { ColumnName = "CustomerId", AllowNull = false, DataType = SqlDbType.UniqueIdentifier, NonClusteredIndex = true },
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
            loggerConfiguration.AuditTo.MSSqlServer(metaConnectionString, logTableCreatedManually,
                columnOptions: auditTableManualColumnOptions, restrictedToMinimumLevel: LogEventLevel.Information, autoCreateSqlTable: true);

            return loggerConfiguration;
        }

        private async Task<string> GetDatabaseConnectionString()
        {
            //#if DEBUG
            //            return $"Server=.;Database=HWE_ArchivingCulling_13072020;Integrated Security=SSPI;persist security info=True;";
            //#endif

            const string DatabaseIdentifier = "ArchivingCulling";
            var (Success, DatabaseConfiguration) = await _metaDatabaseManagement.TryGetDatabaseConfigurationAsync(DatabaseIdentifier);

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