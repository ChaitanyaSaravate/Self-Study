using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IRepository<T>
    {
        T Search(string id);

        void Add(T entity);

        IEnumerable<T> GetAll();
    }   
}
