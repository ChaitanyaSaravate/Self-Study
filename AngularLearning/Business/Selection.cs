using System.Collections.Generic;
using Common;

namespace Business
{
    public class Selection
    {
        public uint Id { get; set; }
        public SchoolDomains SchoolDomain { get; set; }
        public IList<EntityTypes> EntityTypes { get; set; }
    }
}
