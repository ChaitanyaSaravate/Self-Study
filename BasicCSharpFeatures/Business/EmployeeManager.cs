using Common;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class EmployeeManager
    {
        IEmployeeRepository<Employee> _repository;

        public EmployeeManager(IEmployeeRepository<Employee> repository)
        {
            _repository = repository;
        }

        public Employee Create(string name, string address, Company company)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(address) || company == null)
            {
                throw new ArgumentException("Either name or address of a new employee is not provided");
            }

            Employee emp = new Employee { Name = name, Address = address, Company = company };

            emp.EmployeeId = this.GetEmployeeId() + 1;
            this._repository.Add(emp);
            return emp;
        }

        private int GetEmployeeId()
        {
            return this._repository.GetLastEmployeeId();
        }
    }
}
