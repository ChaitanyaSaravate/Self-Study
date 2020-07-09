using System;
using System.Collections.Generic;

namespace Abstractions.Internal.Framework.Entities
{
    /// <summary>
    /// This is equivalent of "Object" in the real ArchivingCulling solution.
    /// </summary>
    public class EduEntity
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public SupportedEduEntityTypes ParentEntity { get; set; }

        public SupportedEduEntityTypes EntityType { get; set; }

        public IEnumerable<string> ArchiveEndpoints { get; set; }

        public IEnumerable<string> CullingEndpoints { get; set; }
    }
}
