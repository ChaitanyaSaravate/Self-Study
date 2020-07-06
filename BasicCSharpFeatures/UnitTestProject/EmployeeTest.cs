using System;
using Business;
using Common;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EveryEmployeeMustHaveNameAddressAndCompany_Test()
        {
            var mockEmpRepo = new Mock<IEmployeeRepository<Employee>>();
            mockEmpRepo.Setup(employees => employees.GetLastEmployeeId()).Returns(10);
            
            EmployeeManager employeeManager = new EmployeeManager(mockEmpRepo.Object);
            employeeManager.Create(" ", "Pune", GetCompany());
            employeeManager.Create("Chaitanya", " ", GetCompany());
            employeeManager.Create("Chaitanya", "Pune", null);
        }

        [TestMethod]
        public void CheckIfAllPropertiesAreSetCorrectly_Test()
        {
            var mockEmpRepo = new Mock<IEmployeeRepository<Employee>>();
            mockEmpRepo.Setup(employees => employees.GetLastEmployeeId()).Returns(10);

            EmployeeManager employeeManager = new EmployeeManager(mockEmpRepo.Object);
            var company = GetCompany();
            var newEmp = employeeManager.Create("Chaitanya", "Pune", company);

            Assert.AreEqual("Chaitanya", newEmp.Name);
            Assert.AreEqual("Pune", newEmp.Address);
            Assert.AreSame(company, newEmp.Company);
        }

        [TestMethod]
        public void NewEmployeeIdMustBeGreaterThanTheLastEmployeeId_Test()
        {
            var mockEmpRepo = new Mock<IEmployeeRepository<Employee>>();
            mockEmpRepo.Setup(employees => employees.GetLastEmployeeId()).Returns(10);

            EmployeeManager employeeManager = new EmployeeManager(mockEmpRepo.Object);
            var newEmp = employeeManager.Create("Chaitanya", "Pune", GetCompany());
            Assert.AreEqual(11, newEmp.EmployeeId);
        }

        private Company GetCompany(string name = "Tieto", IndustryType industryType = IndustryType.IT)
        {
            var mockCompanyRepo = new Mock<IRepository<Company>>();
            mockCompanyRepo.Setup(r => r.Search(name)).Returns(new Company { Name = name, IndustryType = industryType });

            return mockCompanyRepo.Object.Search(name);
        }
    }
}
