using System;

namespace Logging.Abstractions
{
    public interface IAuditLogger<T>
    {
        void LogBusinessEvent(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate);
        
        void LogBusinessEvent<T>(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, T propertyValue);

        void LogBusinessEvent<T0, T1>(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, T0 propertyValue0, T1 propertyValue1);

        void LogBusinessEvent<T0, T1, T2>(ApplicationLogEvents.AuditEventTypes auditEventType, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);

        void LogBusinessEvent(ApplicationLogEvents.AuditEventTypes auditEventType,string messageTemplate, params object[] propertyValues);

        //void LogBusinessEvent(ApplicationLogEvents.AuditEventTypes auditEventType,Exception exception, string messageTemplate);

        //void LogBusinessEvent<T>(ApplicationLogEvents.AuditEventTypes auditEventType, Exception exception, string messageTemplate, T propertyValue);

        //void LogBusinessEvent<T0, T1>(ApplicationLogEvents.AuditEventTypes auditEventType, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1);

        //void LogBusinessEvent<T0, T1, T2>(ApplicationLogEvents.AuditEventTypes auditEventType, Exception exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2);

        //void LogBusinessEvent(ApplicationLogEvents.AuditEventTypes auditEventType, Exception exception, string messageTemplate, params object[] propertyValues);
    }
}
