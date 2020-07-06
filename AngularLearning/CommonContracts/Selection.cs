using System.Collections.Generic;

namespace Common
{
    public class Selection
    {
        public uint Id { get; set; }
        public SchoolDomains SchoolDomain { get; set; }
        public IList<EntityTypes> EntityTypes { get; set; }
    }
}
