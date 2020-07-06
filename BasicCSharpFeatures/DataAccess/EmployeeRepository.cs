using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Entities;

namespace DataAccess
{
    public class EmployeeRepository : IEmployeeRepository<Employee>
    {
        private IList<Employee> employees;

        public EmployeeRepository()
        {
            var tieto = new Company { Name = "Tieto", IndustryType = IndustryType.IT };
            var mahindra = new Company { Name = "Mahindra", IndustryType = IndustryType.Automobile };

            employees = new List<Employee>
            {
                new Employee{Name="Chaitanya", Address = "Pune", EmployeeId = 1, Company = tieto},
                new Employee{Name="Amey", Address = "Kolhapur", EmployeeId = 2, Company = tieto},
                new Employee{Name="Priyanka", Address = "Pune", EmployeeId = 3, Company = mahindra},
                new Employee{Name="Dattatray", Address = "Asurle", EmployeeId = 4, Company = mahindra}
            };
        }

        public Employee Search(string id)
        {
            return employees.First(e => e.EmployeeId.Equals(id));
        }

        public void Add(Employee entity)
        {
            this.employees.Add(entity);
        }

        public IEnumerable<Employee> GetAll()
        {
            return this.employees;
        }

        public int GetLastEmployeeId()
        {
            return this.employees.Max(emp => emp.EmployeeId);
        }

        public void GetVariousEmployees()
        {
            employees.First(e => e.Company.IndustryType == IndustryType.Pharma);
            employees.Select(employee => employee.Company.IndustryType == IndustryType.IT);
        }
    }
}
