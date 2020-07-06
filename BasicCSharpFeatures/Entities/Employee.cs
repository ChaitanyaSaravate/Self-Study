using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Employee : Person
    {
        public int EmployeeId { get; set; }

        public Company Company { get; set; } 
    }

}
