using System.Collections.Generic;

namespace Common
{
    public class RunnableSelection
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<Selection> Selections { get; set; }

        private DataAccessBase<RunnableSelection> _dataAccess;

        public RunnableSelection(DataAccessBase<RunnableSelection> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<RunnableSelection> GetAllSelections()
        {
            return _dataAccess.ReadAll();
        }
    }
}
