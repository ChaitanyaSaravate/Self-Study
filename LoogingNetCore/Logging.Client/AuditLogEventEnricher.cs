using System;
using System.Threading;
using Logging.Abstractions;
using Microsoft.AspNetCore.Http;
using Serilog.Core;
using Serilog.Events;

namespace Logging.SerilogClient
{
    public class AuditLogEventEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditLogEventEnricher(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuditLogEventEnricher()
        {
           
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var userPrincipal = Thread.CurrentPrincipal;

            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.UserId.ToString(), "User Id 1"));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.UserName.ToString(), "User Name 1"));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.ActiveRole.ToString(), "Test User")); 
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.DomainId.ToString(), "Test DomainId"));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.CorrelationId.ToString(), Guid.NewGuid()));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.CustomerId.ToString(), Guid.NewGuid()));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.SystemName.ToString(), "Logging Sample"));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.SolutionArea.ToString(), "Tieto Education"));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty(PushProperties.AuditLogPushProperties.ServerId.ToString(), "Server ID"));

            //logEvent.RemovePropertyIfPresent("SourceContext");
        }
    }
}
