using System;
using System.Collections.Generic;

namespace Abstractions.Internal.Framework.Entities
{
    public class EduEntityGroup
    {
        public IList<EduEntity>  Entities { get; set; }

        public SupportedEduEntityGroups EntityGroup { get; set; }

        public SupportedSchoolDomains SchoolDomain { get; set; }
    }
}
