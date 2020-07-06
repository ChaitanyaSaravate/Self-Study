using System.Collections.Generic;
using Common;

namespace Business
{
    public class RunnableSelection
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<Selection> Selections { get; set; }
    }
}
