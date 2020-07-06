using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IEmployeeRepository<T> : IRepository<T> where T : Employee
    {
        int GetLastEmployeeId();
    }
}
