using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public class DataAccessBase<T> where T : class
    {
        public virtual IEnumerable<T> ReadAll()
        {
            throw new NotImplementedException();
        }

        public virtual uint Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(uint entityId)
        {
            throw new NotImplementedException();
        }
    }
}
