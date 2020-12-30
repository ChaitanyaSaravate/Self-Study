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
            UserRole,
            EventTimeStamp,
            ActiveSubjectId,
            ActiveSubjectType,
            CorrelationId,
            DomainId,
            CustomerId,
            SystemName,
            SolutionArea,
            UserName,
            ServerId
        }
    }
}
