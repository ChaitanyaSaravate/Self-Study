using Logging.Abstractions;
using Serilog;
using Serilog.Context;

namespace Logging.SerilogClient
{
    public class AuditLogger<T> : IAuditLogger<T>
    {
        private Serilog.ILogger _logger = Log.ForContext<T>();

        public AuditLogger()
        {
            
        }

        public void LogBusinessEvent(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate)
        {
            using (LogContext.Push(new AuditLogEventEnricher()))
            using (LogContext.PushProperty(PushProperties.AuditLogPushProperties.EventType.ToString(), auditEventType.ToString()))
            {
                _logger.Information(messageTemplate);
            }
        }

        public void LogBusinessEvent<T1>(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, T1 propertyValue)
        {
            using (LogContext.Push(new AuditLogEventEnricher()))
            using (LogContext.PushProperty(PushProperties.AuditLogPushProperties.EventType.ToString(), auditEventType.ToString()))
            {
                _logger.Information(messageTemplate, propertyValue);
            }
        }

        public void LogBusinessEvent<T0, T1>(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1)
        {
            using (LogContext.Push(new AuditLogEventEnricher()))
            using (LogContext.PushProperty(PushProperties.AuditLogPushProperties.EventType.ToString(), auditEventType.ToString()))
            {
                _logger.Information(messageTemplate, propertyValue0, propertyValue1);
            }
        }

        public void LogBusinessEvent<T0, T1, T2>(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, T0 propertyValue0,
            T1 propertyValue1, T2 propertyValue2)
        {
            using (LogContext.Push(new AuditLogEventEnricher()))
            using (LogContext.PushProperty(PushProperties.AuditLogPushProperties.EventType.ToString(), auditEventType.ToString()))
            {
                _logger.Information(messageTemplate, propertyValue0, propertyValue1, propertyValue2);
            }
        }

        public void LogBusinessEvent(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, params object[] propertyValues)
        {
            using (LogContext.Push(new AuditLogEventEnricher()))
            using (LogContext.PushProperty(PushProperties.AuditLogPushProperties.EventType.ToString(), auditEventType.ToString()))
            {
                _logger.Information(messageTemplate, propertyValues);
            }
        }
    }
}