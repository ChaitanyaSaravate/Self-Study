using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.Abstractions
{
    public class PushProperties
    {
        public enum AuditLogPushProperties
        {
            EventType,
            UserId,
            ActiveRole,
            EventTimeStamp,
            ActiveSubjectId,
            ActiveSubjectType,
            CorrelationId,
            DomainId
        }
    }
}
