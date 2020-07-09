using System;
using System.Collections.Generic;

namespace Abstractions.Internal.Framework.Entities
{
    /// <summary>
    /// This is equivalent of "Object Group" in the real ArchivingCulling solution.
    /// </summary>
    public class EduEntityGroup
    {
        public IList<EduEntity> Entities { get; set; }

        public SupportedEduEntityGroups EntityGroup { get; set; }

        public SupportedSchoolDomains SchoolDomain { get; set; }

        public bool PerformOperationOnGroupLevel { get; set; }
    }
}
