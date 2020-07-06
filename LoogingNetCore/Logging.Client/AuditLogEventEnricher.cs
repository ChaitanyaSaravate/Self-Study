using System.Threading;
using Logging.Abstractions;
using Serilog.Core;
using Serilog.Events;

namespace Logging.SerilogClient
{
    public class AuditLogEventEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var userPrincipal = Thread.CurrentPrincipal;

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.UserId.ToString(), "User Id 1"));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.ActiveRole.ToString(), "Test User")); 
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.DomainId.ToString(), "Test Domain"));

            logEvent.RemovePropertyIfPresent("SourceContext");
        }
    }
}
