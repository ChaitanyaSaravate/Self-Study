using System;
using System.Collections.Generic;

namespace Abstractions.Internal.Framework.Entities
{
    public class EduEntity
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public SupportedEduEntityTypes EntityType { get; set; }

        public IEnumerable<string> ArchiveEndpoints { get; set; }

        public IEnumerable<string> CullingEndpoints { get; set; }
    }
}
