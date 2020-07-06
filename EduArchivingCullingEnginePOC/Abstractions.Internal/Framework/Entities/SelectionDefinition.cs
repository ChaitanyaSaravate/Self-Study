using System;
using System.Collections.Generic;

namespace Abstractions.Internal.Framework.Entities
{
    public class SelectionDefinition
    {
        public string Title { get; set; }

        public int Id { get; set; }

        public SupportedOperationType SupportedOperationType { get; set; }

        public IList<EduEntity> EntitiesToArchiveCull { get; set; }

        public SupportedEduEntityGroups EduEntityGroup { get; set; }

        public SupportedSchoolDomains SchoolDomain { get; set; }
    }
}
